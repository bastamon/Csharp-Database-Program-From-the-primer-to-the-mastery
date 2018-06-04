/*
  CustomersFromCountry.sql creates a procedure that
  retrieves rows from the Customers table whose
  Country matches the @MyCountry parameter
*/

CREATE PROCEDURE CustomersFromCountry
  @MyCountry nvarchar(15)
AS
  SELECT *
  FROM Customers
  WHERE Country = @MyCountry
  FOR XML AUTO