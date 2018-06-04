USE Northwind
SELECT TOP 3 CustomerID, CompanyName, ContactName
FROM Customers
ORDER BY CustomerID
FOR XML AUTO