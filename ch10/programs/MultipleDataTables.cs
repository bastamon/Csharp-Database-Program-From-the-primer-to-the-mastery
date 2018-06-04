/*
  MutlipleDataTables.cs illustrates how to populate a DataSet
  with multiple DataTable objects using multiple SELECT statements
*/

using System;
using System.Data;
using System.Data.SqlClient;

class MultipleDataTables
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    // create a SqlCommand object and set its CommandText property
    // to mutliple SELECT statements
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT TOP 2 ProductID, ProductName, UnitPrice " +
      "FROM Products " +
      "ORDER BY ProductID;" +
      "SELECT CustomerID, CompanyName " +
      "FROM Customers " +
      "WHERE CustomerID = 'ALFKI';";

    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    int numberOfRows = mySqlDataAdapter.Fill(myDataSet);
    Console.WriteLine("numberOfRows = " + numberOfRows);
    mySqlConnection.Close();

    // change the TableName property of the DataTable objects
    myDataSet.Tables["Table"].TableName = "Products";
    myDataSet.Tables["Table1"].TableName = "Customers";

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