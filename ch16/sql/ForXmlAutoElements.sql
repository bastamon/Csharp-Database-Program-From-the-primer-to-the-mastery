USE Northwind
SELECT TOP 2 CustomerID, CompanyName, ContactName
FROM Customers
ORDER BY CustomerID
FOR XML AUTO, ELEMENTS