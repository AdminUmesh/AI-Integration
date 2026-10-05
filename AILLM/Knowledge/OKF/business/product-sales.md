---
type: Business Metric
title: Product Sales
description: Product quantity and revenue analysis using sale details.
resource: metric://product-sales
tags: [metric, product, quantity]
---

# Computation

Use SaleDetails joined to Products and Sales.

Only include Sales.Status = 'Completed'.

Quantity = SUM(SaleDetails.Quantity).

Product revenue = SUM(SaleDetails.Quantity * SaleDetails.UnitPrice).

Transaction revenue = Sales.NetAmount.
