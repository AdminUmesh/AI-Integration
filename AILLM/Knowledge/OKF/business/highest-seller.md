---
type: Business Metric
title: Highest Seller
description: Salesperson with the highest completed NetAmount in a requested period.
resource: metric://highest-seller
tags: [metric, salesperson, ranking]
---

# Computation

Join Employees to Sales using EmployeeId.

Filter Sales.Status = 'Completed'.

Filter SaleDate to the requested period.

GROUP BY EmployeeId, EmployeeName.

ORDER BY SUM(NetAmount) DESC.

TOP 1 returns the highest seller.
