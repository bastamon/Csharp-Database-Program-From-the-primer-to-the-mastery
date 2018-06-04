/*
  Savepoint.sql illustrates how to use a savepoint
*/

USE Northwind

-- step 1: begin the transaction
BEGIN TRANSACTION

-- step 2: insert a row into the Customers table
INSERT INTO Customers (
  CustomerID, CompanyName
) VALUES (
  'J8COM', 'J8 Company'
)

-- step 3: set a savepoint
SAVE TRANSACTION SaveCustomer

-- step 4: insert a row into the Orders table
INSERT INTO Orders (
  CustomerID
) VALUES (
  'J8COM'
);

-- step 5: rollback to the savepoint set in step 3
ROLLBACK TRANSACTION SaveCustomer

-- step 6: commit the transaction
COMMIT TRANSACTION

-- step 7: select the new row from the Customers table
SELECT CustomerID, CompanyName
FROM Customers
WHERE CustomerID = 'J8COM'

-- step 8: attempt to select the row from the Orders table
-- that was rolled back in step 5
SELECT OrderID, CustomerID
FROM Orders
WHERE CustomerID = 'J8COM'

-- step 9: delete the new row from the Customers table
DELETE FROM Customers
WHERE CustomerID = 'J8COM'