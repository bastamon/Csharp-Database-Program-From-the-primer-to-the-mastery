/*
  AddProduct.sql creates a procedure that adds a row to the
  Products table using values passed as parameters to the
  procedure. The procedure returns the ProductID of the new row
  in an OUTPUT parameter named @MyProductID
*/

CREATE PROCEDURE AddProduct
  @MyProductID int OUTPUT,
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
  SELECT @MyProductID = SCOPE_IDENTITY()