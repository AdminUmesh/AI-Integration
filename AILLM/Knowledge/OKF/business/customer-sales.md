---
type: Business Metric
title: Customer Sales
description: Completed NetAmount aggregated for a requested customer and period.
resource: metric://customer-sales
tags: [metric, customer]
---

# Computation

Join Customers to Sales on CustomerId.
Filter Status = 'Completed'.
Aggregate Sales.NetAmount.
