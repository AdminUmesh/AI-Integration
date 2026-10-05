---
type: SQL Server Table
title: Customers
description: Customer master data used by sales transactions.
resource: database://OkfSalesDemo/Customers
tags: [customer, sales]
---

# Schema

| Column | Type |
|---|---|
| CustomerId | INT |
| CustomerName | NVARCHAR(100) |
| City | NVARCHAR(100) |
| IsActive | BIT |

# Relationship

Sales.CustomerId -> Customers.CustomerId
