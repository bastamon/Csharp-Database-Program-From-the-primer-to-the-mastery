/*
  ProductCursor.sql uses a cursor to display
  the ProductID, ProductName, and UnitPrice columns
  from the Products table
*/

USE Northwind

-- step 1: declare the variables
DECLARE @MyProductID int
DECLARE @MyProductName nvarchar(40)
DECLARE @MyUnitPrice money

-- step 2: declare the cursor
DECLARE ProductCursor CURSOR FOR
SELECT ProductID, ProductName, UnitPrice
FROM Products
WHERE ProductID <= 10

-- step 3: open the cursor
OPEN ProductCursor

-- step 4: fetch the rows from the cursor
FETCH NEXT FROM ProductCursor
INTO @MyProductID, @MyProductname, @MyUnitPrice
PRINT '@MyProductID = ' + CONVERT(nvarchar, @MyProductID)
PRINT '@MyProductName = ' + CONVERT(nvarchar, @MyProductName)
PRINT '@MyUnitPrice = ' + CONVERT(nvarchar, @MyUnitPrice)
WHILE @@FETCH_STATUS = 0
BEGIN
  FETCH NEXT FROM ProductCursor
  INTO @MyProductID, @MyProductName, @MyUnitPrice
  PRINT '@MyProductID = ' + CONVERT(nvarchar, @MyProductID)
  PRINT '@MyProductName = ' + CONVERT(nvarchar, @MyProductName)
  PRINT '@MyUnitPrice = ' + CONVERT(nvarchar, @MyUnitPrice)
END

-- step 5: close the cursor
CLOSE ProductCursor
DEALLOCATE ProductCursor


