/*
  UpdateUnitPriceProductTrigger.sql creates a trigger
  that fires after an UPDATE statement is performed on the 
  the UnitPrice column of the Products table.
  If the reduction of the unit price of a product is
  greater than 25% then a row is added to the ProductAudit table
  to audit the change.
*/

CREATE TRIGGER UpdateUnitPriceProductTrigger
ON Products
AFTER UPDATE
AS

  -- don't return the number of rows affected
  SET NOCOUNT ON

  -- only run the code if the UnitPrice column
  -- was modified
  IF UPDATE(UnitPrice)
  BEGIN

    -- declare an int variable to store the
    -- ProductID
    DECLARE @MyProductID int

    -- declare two money variables to store the
    -- old unit price and the new unit price
    DECLARE @OldUnitPrice money
    DECLARE @NewUnitPrice money

    -- declare a float variable to store the price
    -- reduction percentage
    DECLARE @PriceReductionPercentage float

    -- get the ProductID of the row that
    -- was modified from the inserted table
    SELECT @MyProductID = ProductID
    FROM inserted

    -- get the old unit price from the deleted table
    SELECT @OldUnitPrice = UnitPrice
    FROM deleted
    WHERE ProductID = @MyProductID

    -- get the new unit price from the inserted table
    SELECT @NewUnitPrice = UnitPrice
    FROM inserted

    -- calculate the price reduction percentage
    SET @PriceReductionPercentage =
      ((@OldUnitPrice - @NewUnitPrice) / @OldUnitPrice) * 100

    -- if the price reduction percentage is greater than 25%
    -- then audit the change by adding a row to the PriceAudit table
    IF (@PriceReductionPercentage > 25)
    BEGIN

      -- add a row to the ProductAudit table
      INSERT INTO ProductAudit (
        Action
      ) VALUES (
        'UnitPrice of ProductID #' +
          CONVERT(nvarchar, @MyProductID) +
        ' was reduced by ' +
          CONVERT(nvarchar, @PriceReductionPercentage) +
          '%'
      )

    END
  END