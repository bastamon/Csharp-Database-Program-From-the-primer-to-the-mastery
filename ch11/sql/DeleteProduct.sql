/*
  DeleteProduct.sql creates a procedure that removes a row
  from the Products table
*/

CREATE PROCEDURE DeleteProduct
  @OldProductID int,
  @OldProductName nvarchar(40),
  @OldUnitPrice money
AS

  -- delete the row from the Products table
  DELETE FROM Products
  WHERE ProductID = @OldProductID
  AND ProductName = @OldProductName
  AND UnitPrice = @OldUnitPrice