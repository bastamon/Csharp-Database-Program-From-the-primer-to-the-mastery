/*
  MutlipleDataTables2.cs illustrates how to populate a DataSet
  object with multiple DataTable objects by changing the
  CommandText property of a DataAdapter object's SelectCommand
*/

using System;
using System.Data;
using System.Data.SqlClient;

class MultipleDataTables2
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT TOP 2 ProductID, ProductName, UnitPrice " +
      "FROM Products " +
      "ORDER BY ProductID";
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    int numberOfRows = mySqlDataAdapter.Fill(myDataSet, "Products");
    Console.WriteLine("numberOfRows = " + numberOfRows);

    // change the CommandText property of the SelectCommand
    mySqlDataAdapter.SelectCommand.CommandText =
      "SELECT CustomerID, CompanyName " +
      "FROM Customers " +
      "WHERE CustomerID = 'ALFKI'";
    numberOfRows = mySqlDataAdapter.Fill(myDataSet, "Customers");
    Console.WriteLine("numberOfRows = " + numberOfRows);

    mySqlConnection.Close();

    foreach (DataTable myDataTable in myDataSet.Tables) {
      Console.WriteLine("\nReading from the " +
        myDataTable.TableName + " DataTable");
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