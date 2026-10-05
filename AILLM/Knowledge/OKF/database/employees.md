---
type: SQL Server Table
title: Employees
description: Employee master data including salespeople.
resource: database://OkfSalesDemo/Employees
tags: [employee, salesperson, sales]
---

# Schema

| Column | Type |
|---|---|
| EmployeeId | INT |
| EmployeeName | NVARCHAR(100) |
| Department | NVARCHAR(100) |
| IsActive | BIT |

# Business meaning

When the user says seller, salesperson, or employee sales, join Sales to Employees.

# Relationship

Sales.EmployeeId -> Employees.EmployeeId
