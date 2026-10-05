---
type: SQL Server Table
title: Sale Details
description: Line items belonging to each sales transaction.
resource: database://OkfSalesDemo/SaleDetails
tags: [sale-detail, product, quantity]
---

# Schema

| Column | Type |
|---|---|
| SaleDetailId | INT |
| SaleId | INT |
| ProductId | INT |
| Quantity | INT |
| UnitPrice | DECIMAL(18,2) |

# Business rules

Use this table for product quantity and product-level revenue.
For transaction-level revenue prefer Sales.NetAmount.

# Relationships

Sales.Id -> SaleDetails.SaleId
Products.Id -> SaleDetails.ProductId
