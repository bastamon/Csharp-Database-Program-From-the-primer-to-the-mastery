/*
  AddCustomersXml.sql creates a procedure that uses OPENXML()
  to read customers from an XML document and then inserts them
  into the Customers table
*/

CREATE PROCEDURE AddCustomersXml
  @MyCustomersXmlDoc nvarchar(4000)
AS

  -- declare the XmlDocumentId handle
  DECLARE @XmlDocumentId int

  -- prepare the XML document
  EXECUTE sp_xml_preparedocument @XmlDocumentId OUTPUT, @MyCustomersXmlDoc

  -- read the customers from the XML document using OPENXML()
  -- and insert them into the Customers table
  INSERT INTO Customers
  SELECT *
  FROM OPENXML(@XmlDocumentId, N'/Northwind/Customers', 2)
  WITH Customers

  -- remove the XML document from memory
  EXECUTE sp_xml_removedocument @XmlDocumentId