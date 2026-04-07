/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [SalesItemId]
      ,[Quantity]
      ,[UnitPrice]
      ,[TotalPrice]
      ,[SaleId]
      ,[ProductId]
  FROM [POS_System].[dbo].[SaleItems]


  select s.CustomerId, SUM(si.Quantity) as TotalQuantity, Sum(si.TotalPrice) as TotalRenevue 
  from [POS_System].[dbo].[SaleItems] si join [POS_System].[dbo].[Sales] s on si.SaleId = s.SaleId where si.TotalPrice > 0
  group by s.CustomerId order by TotalRenevue DESC;

  select p.ProductId, p.ProductName, SUM(si.Quantity) as TotalQuantity, SUM(si.TotalPrice) as TotalRenevue  
  from [POS_System].[dbo].[SaleItems] si join [POS_System].[dbo].[Products] p on si.ProductId = p.ProductId 
  group by p.ProductId, p.ProductName order by TotalRenevue Desc;

  select CAST(s.SaleDate as date) as SaleDate, Sum(si.TotalPrice) as TotalRenevue 
  from [POS_System].[dbo].[SaleItems] si join [POS_System].[dbo].[Sales] s on si.SaleId = s.SaleId
  group by CAST(s.SaleDate as date) order by TotalRenevue DESC;

SELECT 
    p.ProductId,
    p.ProductName,
    p.CostPrice,
    p.SellingPrice,
    (p.SellingPrice - p.CostPrice) AS ProfitMargin
FROM [POS_System].[dbo].[Products] p;

