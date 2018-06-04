/*
  DiscountPrice.sql creates a scalar function to
  return the new price of an item given the original
  price and a discount factor
*/

CREATE FUNCTION DiscountPrice(@OriginalPrice money, @Discount float)
RETURNS money
AS
BEGIN
  RETURN @OriginalPrice * @Discount
END