---
type: Business Metric
title: Daily Sales
description: Completed sales revenue for a requested calendar day.
resource: metric://daily-sales
tags: [metric, daily-sales, revenue]
---

# Computation

SUM(Sales.NetAmount)

WHERE Sales.Status = 'Completed'

For a day use:
SaleDate >= start-of-day
AND SaleDate < start-of-next-day

Do not include cancelled transactions.
