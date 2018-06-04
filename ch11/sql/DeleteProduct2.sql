/*
  DeleteProduct2.sql creates a procedure that removes a row
  from the Products table
*/

CREATE PROCEDURE DeleteProduct2
  @OldProductID int,
  @OldProductName nvarchar(40),
  @OldUnitPrice money
AS

  -- delete the row from the Products table
  DELETE FROM Products
  WHERE ProductID = @OldProductID
  AND ProductName = @OldProductName
  AND UnitPrice = @OldUnitPrice

  -- use SET NOCOUNT ON to suppress the return of the
  -- number of rows affected by the INSERT statement
  SET NOCOUNT ON

  -- add a row to the Audit table
  IF @@ROWCOUNT = 1
    INSERT INTO ProductAudit (
      Action
    ) VALUES (
      'Product deleted with ProductID of ' +
      CONVERT(nvarchar, @OldProductID)
    )
  ELSE
    INSERT INTO ProductAudit (
      Action
    ) VALUES (
      'Product with ProductID of ' +
      CONVERT(nvarchar, @OldProductID) +
      ' was not deleted'
    )