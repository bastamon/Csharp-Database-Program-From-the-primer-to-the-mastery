/*
  ProductsToBeReordered.sql creates an inline table-valued function to
  return the rows from the Products table whose UnitsInStock column
  is less than or equal to the reorder level passed as a parameter
  to the function
*/

CREATE FUNCTION ProductsToBeReordered(@ReorderLevel int)
RETURNS table
AS
RETURN
(
  SELECT *
  FROM Products
  WHERE UnitsInStock <= @ReorderLevel
)