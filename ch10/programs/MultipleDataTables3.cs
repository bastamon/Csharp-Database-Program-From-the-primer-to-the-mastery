/*
  MutlipleDataTables3.cs illustrates how to populate a DataSet
  object with multiple DataTable objects using multiple
  DataAdapter objects to populate the same DataSet object
*/

using System;
using System.Data;
using System.Data.SqlClient;

class MultipleDataTables3
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
    SqlDataAdapter mySqlDataAdapter1 = new SqlDataAdapter();
    mySqlDataAdapter1.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    int numberOfRows = mySqlDataAdapter1.Fill(myDataSet, "Products");
    Console.WriteLine("numberOfRows = " + numberOfRows);

    // create another DataAdapter object
    SqlDataAdapter mySqlDataAdapter2 = new SqlDataAdapter();
    mySqlDataAdapter2.SelectCommand = mySqlCommand;
    mySqlDataAdapter2.SelectCommand.CommandText =
      "SELECT CustomerID, CompanyName " +
      "FROM Customers " +
      "WHERE CustomerID = 'ALFKI'";
    numberOfRows = mySqlDataAdapter2.Fill(myDataSet, "Customers");
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