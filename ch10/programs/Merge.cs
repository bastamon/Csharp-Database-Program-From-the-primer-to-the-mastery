/*
  Merge.cs illustrates how to use the Merge() method
*/

using System;
using System.Data;
using System.Data.SqlClient;

class Merge
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

    // populate myDataSet with three rows from the Customers table
    mySqlCommand.CommandText =
      "SELECT CustomerID, CompanyName, ContactName, Address " +
      "FROM Customers " +
      "WHERE CustomerID IN ('ALFKI', 'ANATR', 'ANTON')";
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    mySqlDataAdapter.Fill(myDataSet, "Customers");

    // populate myDataSet2 with two rows from the Customers table
    mySqlCommand.CommandText =
      "SELECT CustomerID, CompanyName, ContactName, Address " +
      "FROM Customers " +
      "WHERE CustomerID IN ('AROUT', 'BERGS')";
    DataSet myDataSet2 = new DataSet();
    mySqlDataAdapter.Fill(myDataSet2, "Customers2");


    // populate myDataSet3 with five rows from the Products table
    mySqlCommand.CommandText =
      "SELECT TOP 5 ProductID, ProductName, UnitPrice " +
      "FROM Products " +
      "ORDER BY ProductID";
    DataSet myDataSet3 = new DataSet();
    mySqlDataAdapter.Fill(myDataSet3, "Products");

    mySqlConnection.Close();

    // merge myDataSet2 into myDataSet
    myDataSet.Merge(myDataSet2);

    // merge myDataSet3 into myDataSet
    myDataSet.Merge(myDataSet3, true, MissingSchemaAction.Add);

    // display the rows in myDataSet
    foreach (DataTable myDataTable in myDataSet.Tables)
    {
      Console.WriteLine("\nReading from the " + myDataTable + " DataTable");
      foreach (DataRow myDataRow in myDataTable.Rows)
      {
        foreach (DataColumn myDataColumn in myDataTable.Columns)
        {
          Console.WriteLine(myDataColumn + " = " +
            myDataRow[myDataColumn]);
        }
      }
    }
  }
}