/*
  NestedXml.cs illustrates how setting the Nested property
  of a DataRelation to true causes the the child rows to be nested within the
  parent rows in the output XML
*/

using System;
using System.Data;
using System.Data.SqlClient;

class NestedXml
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT TOP 2 CustomerID, CompanyName " +
      "FROM Customers " +
      "ORDER BY CustomerID;" +
      "SELECT OrderID, CustomerID, ShipCountry " +
      "FROM Orders " +
      "WHERE CustomerID IN ( " +
      "  SELECT TOP 2 CustomerID " +
      "  FROM Customers " +
      "  ORDER BY CustomerID " +
      ")";
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    int numberOfRows = mySqlDataAdapter.Fill(myDataSet);
    Console.WriteLine("numberOfRows = " + numberOfRows);
    mySqlConnection.Close();
    DataTable customersDT = myDataSet.Tables["Table"];
    DataTable ordersDT = myDataSet.Tables["Table1"];

    // create a DataRelation object named customersOrdersDataRel
    DataRelation customersOrdersDataRel =
      new DataRelation(
        "CustomersOrders",
        customersDT.Columns["CustomerID"],
        ordersDT.Columns["CustomerID"]
      );
    myDataSet.Relations.Add(
      customersOrdersDataRel
    );

    // write the XML out to a file
    Console.WriteLine("Writing XML out to file nonNestedXmlFile.xml");
    myDataSet.WriteXml("nonNestedXmlFile.xml");

    // set the DataRelation object's Nested property to true
    // (causes child rows to be nested in the parent rows of the
    // XML output)
    myDataSet.Relations["CustomersOrders"].Nested = true;

    // write the XML out again (this time the child rows are nested
    // within the parent rows)
    Console.WriteLine("Writing XML out to file nestedXmlFile.xml");
    myDataSet.WriteXml("nestedXmlFile.xml");
  }
}