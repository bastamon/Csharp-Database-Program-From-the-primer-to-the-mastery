/*
  ProductAudit.sql creates a table that is used to
  store the results of triggers that audit modifications
  to the Products table
*/

USE Northwind

CREATE TABLE ProductAudit (
  ID int IDENTITY(1, 1) PRIMARY KEY,
  Action nvarchar(100) NOT NULL,
  PerformedBy nvarchar(15) NOT NULL DEFAULT User,
  TookPlace datetime NOT NULL DEFAULT GetDate()
)