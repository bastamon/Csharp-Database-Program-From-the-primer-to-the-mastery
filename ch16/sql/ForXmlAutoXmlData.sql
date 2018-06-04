USE Northwind
SELECT TOP 2 ProductID, ProductName, UnitPrice
FROM Products
ORDER BY ProductID
FOR XML AUTO, XMLDATA