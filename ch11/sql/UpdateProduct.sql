/*
  UpdateProduct.sql creates a procedure that modifies a row
  in the Products table using values passed as parameters
  to the procedure
*/

CREATE PROCEDURE UpdateProduct
  @OldProductID int,
  @NewProductName nvarchar(40),
  @NewUnitPrice money,
  @OldProductName nvarchar(40),
  @OldUnitPrice money
AS

  -- update the row in the Products table
  UPDATE Products
  SET
    ProductName = @NewProductName,
    UnitPrice = @NewUnitPrice
  WHERE ProductID = @OldProductID
  AND ProductName = @OldProductName
  AND UnitPrice = @OldUnitPrice