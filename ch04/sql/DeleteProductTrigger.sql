/*
  DeleteProductTrigger.sql creates a trigger that fires
  after a DELETE statement is performed on the
  Products table
*/

CREATE TRIGGER DeleteProductTrigger
ON Products
AFTER DELETE
AS

  -- don't return the number of rows affected
  SET NOCOUNT ON

  -- declare an int variable to store the
  -- ProductID
  DECLARE @NewProductID int

  -- get the ProductID of the row that
  -- was removed from the Products table
  SELECT @NewProductID = ProductID
  FROM deleted

  -- add a row to the ProductAudit table
  INSERT INTO ProductAudit (
    Action
  ) VALUES (
    'Product #' +
      CONVERT(nvarchar, @NewProductID) +
      ' was removed'
  )