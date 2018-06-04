USE Northwind

SELECT
  1 AS Tag,
  0 AS Parent,
  CustomerID AS [Customer!1!CustomerID],
  CompanyName AS [Customer!1!CompanyName],
  ContactName AS [Customer!1!ContactName],
  NULL AS [Order!2!OrderID!element],
  NULL AS [Order!2!OrderDate!element]
FROM Customers
WHERE CustomerID = 'ALFKI'

UNION ALL

SELECT
  2 AS Tag,
  1 AS Parent,
  C.CustomerID,
  C.CompanyName,
  C.ContactName,
  O.OrderID,
  O.OrderDate
FROM Customers C, Orders O
WHERE C.CustomerID = O.CustomerID
AND C.CustomerID = 'ALFKI'

FOR XML EXPLICIT