using AILLM.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace AILLM.Services
{
    public class SqlAgentService
    {
        private readonly GroqConfig _config;
        private readonly IHttpClientFactory _http;
        private readonly OkfKnowledgeService _okf;
        private readonly SqlSafetyService _safety;
        private readonly IConfiguration _configuration;

        public SqlAgentService(
            IOptions<GroqConfig> config,
            IHttpClientFactory http,
            OkfKnowledgeService okf,
            SqlSafetyService safety,
            IConfiguration configuration)
        {
            _config = config.Value;
            _http = http;
            _okf = okf;
            _safety = safety;
            _configuration = configuration;
        }

        public async Task<string> AskAsync(string question)
        {
            // 1. Progressive disclosure: send only index.md first.
            var index = await _okf.GetIndexAsync();
            var navigation = await SelectConceptsAsync(question, index);

            // 2. Load only the concepts selected by the LLM.
            var knowledge = await _okf.GetConceptsAsync(navigation.Concepts);

            // 3. Generate one read-only SQL query using the selected OKF knowledge.
            var plan = await GenerateSqlAsync(question, knowledge);
            _safety.Validate(plan.Sql);

            // 4. Execute live database query with Dapper.
            await using var db = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            var rows = (await db.QueryAsync(
                plan.Sql,
                commandTimeout: 30)).ToList();

            // 5. Give only the actual database result to the final LLM call.
            return await SummarizeAsync(question, rows);
        }

        private async Task<AgentPlan> SelectConceptsAsync( string question, string index)
        {
            var prompt = $"""
              You are an OKF navigation agent.
             
              User question:
              {question}
             
              OKF root index:
              {index}
             
              Select only the concept documents required to answer the question.
             
              Return JSON only:
                "intent": "short intent",
                "concepts": [
                  "database/sales.md",
                  "business/highest-seller.md"
             
              Use paths exactly as shown in the index.
              Do not invent paths.
              """;

            return await CallJsonAsync<AgentPlan>(prompt);
        }

        private async Task<SqlPlan> GenerateSqlAsync( string question, string knowledge)
        {
            var prompt = $"""
        You are a SQL Server analytics agent.

        User question:
        {question}

        Relevant OKF knowledge:
        {knowledge}

        Generate exactly ONE read-only SQL Server SELECT query.

        Rules:
        - SELECT or WITH only.
        - No INSERT, UPDATE, DELETE, DROP, ALTER, EXEC, MERGE, TRUNCATE, etc.
        - No multiple statements.
        - Use only tables/columns documented by OKF.
        - Follow the business rules documented by OKF.
        - Do not use SELECT *.
        - For "today", use:
          SaleDate >= CAST(GETDATE() AS date)
          AND SaleDate < DATEADD(day, 1, CAST(GETDATE() AS date))
        - Return only the query in the JSON field.
        - Do not use markdown fences.

        - Return JSON only:
        -  "intent": "short description",
        -"sql": "SELECT ..."
        
        """;

            return await CallJsonAsync<SqlPlan>(prompt);
        }

        private async Task<string> SummarizeAsync( string question, object rows)
        {
            var result = JsonSerializer.Serialize(rows);

            var prompt = $"""
        Answer the user's question using only this database result.

        Question:
        {question}

        Database result:
        {result}

        Rules:
        - Never invent values.
        - If there are no rows, say no matching data was found.
        - Give a concise business-friendly answer.
        """;

            return await CallTextAsync(prompt);
        }

        private async Task<T> CallJsonAsync<T>(string prompt)
        {
            var text = await CallTextAsync(prompt);

            return JsonSerializer.Deserialize<T>(
                text,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })
                ?? throw new InvalidOperationException(
                    "LLM returned invalid JSON.");
        }

        private async Task<string> CallTextAsync(string prompt)
        {
            var client = _http.CreateClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", _config.ApiKey);

            var body = new
            {
                model = _config.Model,
                messages = new[]
                {
                new
                {
                    role = "system",
                    content = "Follow the requested output format exactly."
                },
                new
                {
                    role = "user",
                    content = prompt
                }
            },
                temperature = 0
            };

            var json = JsonSerializer.Serialize(body);

            using var content = new StringContent(
                json, Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(
                _config.EndPoint, content);

            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception(
                    $"Groq API Error: {responseText}");

            using var doc = JsonDocument.Parse(responseText);

            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString()
                ?? throw new InvalidOperationException(
                    "LLM returned empty content.");
        }
    }

}
