/*
  InsertProductTrigger.sql creates a trigger that fires
  after an INSERT statement is performed on the
  Products table
*/

CREATE TRIGGER InsertProductTrigger
ON Products
AFTER INSERT
AS

  -- don't return the number of rows affected
  SET NOCOUNT ON

  -- declare an int variable to store the new
  -- ProductID
  DECLARE @NewProductID int

  -- get the ProductID of the new row that
  -- was added to the Products table
  SELECT @NewProductID = ProductID
  FROM inserted

  -- add a row to the ProductAudit table
  INSERT INTO ProductAudit (
    Action
  ) VALUES (
    'Product added with ProductID of ' +
      CONVERT(nvarchar, @NewProductID)
  )