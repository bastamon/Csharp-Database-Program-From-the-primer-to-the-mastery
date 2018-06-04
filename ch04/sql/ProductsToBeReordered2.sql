/*
  ProductsToBeReordered2.sql creates an inline table-valued
  function that returns the rows from the Products table
  whose UnitsInStock column is less than or equal to the
  reorder level passed as a parameter to the function
*/

CREATE FUNCTION ProductsToBeReordered2(@ReorderLevel int)
RETURNS @MyProducts table
(
  ProductID int,
  ProductName nvarchar(40),
  UnitsInStock smallint,
  Reorder nvarchar(3)
)
AS
BEGIN

  -- retrieve rows from the Products table and
  -- insert them into the MyProducts table,
  -- setting the Reorder column to 'No'
  INSERT INTO @MyProducts
    SELECT ProductID, ProductName, UnitsInStock, 'No'
    FROM Products;

  -- update the MyProducts table, setting the
  -- Reorder column to 'Yes' when the UnitsInStock
  -- column is less than or equal to @ReorderLevel
  UPDATE @MyProducts
  SET Reorder = 'Yes'
  WHERE UnitsInStock <= @ReorderLevel

  RETURN

END