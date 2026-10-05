---
type: Database Relationship
title: Sales Relationships
description: Relationships between sales and customer, employee, and product data.
resource: database://OkfSalesDemo/relationships
tags: [relationship, join]
---

# Relationships

Sales.CustomerId -> Customers.CustomerId
Sales.EmployeeId -> Employees.EmployeeId
Sales.SaleId -> SaleDetails.SaleId
SaleDetails.ProductId -> Products.ProductId

# Common joins

Sales s JOIN Employees e ON e.EmployeeId = s.EmployeeId

Sales s JOIN Customers c ON c.CustomerId = s.CustomerId

Sales s JOIN SaleDetails sd ON sd.SaleId = s.SaleId
JOIN Products p ON p.ProductId = sd.ProductId
