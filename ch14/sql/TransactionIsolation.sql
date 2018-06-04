/*
  TransactionIsolation.sql illustrates how to set the
  transaction isolation level
*/

USE Northwind

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

  BEGIN TRANSACTION

    SELECT CustomerID, CompanyName
    FROM Customers
    WHERE CustomerID IN ('ALFKI', 'J8COM')

    INSERT INTO Customers (
      CustomerID, CompanyName
    ) VALUES (
      'J8COM', 'J8 Company'
    )

    UPDATE Customers
    SET CompanyName = 'Widgets Inc.'
    WHERE CustomerID = 'ALFKI'

    SELECT CustomerID, CompanyName
    FROM Customers
    WHERE CustomerID IN ('ALFKI', 'J8COM')

  COMMIT TRANSACTION

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

  BEGIN TRANSACTION

    UPDATE Customers
    SET CompanyName = 'Alfreds Futterkiste'
    WHERE CustomerID = 'ALFKI'

    DELETE FROM Customers
    WHERE CustomerID = 'J8COM'

    SELECT CustomerID, CompanyName
    FROM Customers
    WHERE CustomerID IN ('ALFKI', 'J8COM')

  COMMIT TRANSACTION