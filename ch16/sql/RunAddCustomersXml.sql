/*
  RunAddCustomersXml.sql runs the AddCustomersXml() procedure
*/

-- define the XML document
DECLARE @NewCustomers nvarchar(4000)
SET @NewCustomers = N'
<Northwind>
  <Customers>
    <CustomerID>T1COM</CustomerID>
    <CompanyName>Test 1 Company</CompanyName>
  </Customers>
  <Customers>
    <CustomerID>T2COM</CustomerID>
    <CompanyName>Test 2 Company</CompanyName>
  </Customers>
</Northwind>'

-- run the AddCustomersXml() procedure
EXECUTE AddCustomersXml @MyCustomersXmlDoc=@NewCustomers

-- display the new rows
SELECT CustomerID, CompanyName
FROM Customers
WHERE CustomerID IN ('T1COM', 'T2COM')

-- delete the new rows
DELETE FROM Customers
WHERE CustomerID IN ('T1COM', 'T2COM')