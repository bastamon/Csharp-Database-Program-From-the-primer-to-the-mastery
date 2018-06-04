/*
  AddProduct3.sql creates a procedure that adds a row to the
  Products table using values passed as parameters to the
  procedure. The procedure returns the ProductID of the new row
  using a RETURN statement and returns a result set containing
  the new row
*/

CREATE PROCEDURE AddProduct3
  @MyProductName nvarchar(40),
  @MySupplierID int,
  @MyCategoryID int,
  @MyQuantityPerUnit nvarchar(20),
  @MyUnitPrice money,
  @MyUnitsInStock smallint,
  @MyUnitsOnOrder smallint,
  @MyReorderLevel smallint,
  @MyDiscontinued bit
AS

  -- declare the @MyProductID variable
  DECLARE @MyProductID int

  -- insert a row into the Products table
  INSERT INTO Products (
    ProductName, SupplierID, CategoryID, QuantityPerUnit,
    UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel,
    Discontinued
  ) VALUES (
    @MyProductName, @MySupplierID, @MyCategoryID, @MyQuantityPerUnit,
    @MyUnitPrice, @MyUnitsInStock, @MyUnitsOnOrder, @MyReorderLevel,
    @MyDiscontinued
  )

  -- use the SCOPE_IDENTITY() function to get the last
  -- identity value inserted into a table performed within
  -- the current database session and stored procedure,
  -- so SCOPE_IDENTITY returns the ProductID for the new row
  -- in the Products table in this case
  SET @MyProductID = SCOPE_IDENTITY()

  -- return the result set
  SELECT ProductName, UnitPrice
  FROM Products
  WHERE ProductID = @MyProductID

  -- return @MyProductID
  RETURN @MyProductID