---
type: SQL Server Table
title: Products
description: Product catalog used by sale details.
resource: database://OkfSalesDemo/Products
tags: [product, catalog]
---

# Schema

| Column | Type |
|---|---|
| ProductId | INT |
| ProductName | NVARCHAR(100) |
| Category | NVARCHAR(100) |
| UnitPrice | DECIMAL(18,2) |
| IsActive | BIT |

# Relationship

SaleDetails.ProductId -> Products.ProductId
