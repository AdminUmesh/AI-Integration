---
type: SQL Server Table
title: Sales
description: Sales transaction header containing customer, salesperson, date, status, and monetary totals.
resource: database://OkfSalesDemo/Sales
tags: [sales, transaction, revenue, analytics]
---

# Sales

One row represents one sales transaction.

# Schema

| Column | Type | Meaning |
|---|---|---|
| SaleId | INT | Primary key |
| CustomerId | INT | FK to Customers |
| EmployeeId | INT | FK to Employees |
| SaleDate | DATETIME2 | Date and time |
| Status | NVARCHAR(30) | Transaction status |
| PaymentMethod | NVARCHAR(30) | Payment method |
| GrossAmount | DECIMAL(18,2) | Amount before discount |
| DiscountAmount | DECIMAL(18,2) | Discount |
| NetAmount | DECIMAL(18,2) | GrossAmount - DiscountAmount |

# Business rules

Only Status = 'Completed' is normally included in sales analytics.
Cancelled transactions must not be counted.

For date ranges use:
SaleDate >= start AND SaleDate < end

# Relationships

[Customers](customers.md)
[Employees](employees.md)
[Sale Details](sale-details.md)
