/*
  PopulateDataSetUsingRange.cs illustrates how to populate a DataSet
  object with a range of rows from a SELECT statement
*/

using System;
using System.Data;
using System.Data.SqlClient;

class PopulateDataSetUsingRange
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    // create a SqlCommand object and set its CommandText property
    // to a SELECT statement that retrieves the top 5 rows from
    // the Products table
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT TOP 5 ProductID, ProductName, UnitPrice " +
      "FROM Products " +
      "ORDER BY ProductID";
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();

    // use the Fill() method of the SqlDataAdapter object to
    // retrieve the rows from the table, storing a range of rows
    // in a DataTable of the DataSet object
    Console.WriteLine("Retrieving rows from the Products table");
    int numberOfRows = mySqlDataAdapter.Fill(myDataSet, 1, 3, "Products");
    Console.WriteLine("numberOfRows = " + numberOfRows);

    mySqlConnection.Close();

    DataTable myDataTable = myDataSet.Tables["Products"];

    foreach (DataRow myDataRow in myDataTable.Rows)
    {
      Console.WriteLine("ProductID = " + myDataRow["ProductID"]);
      Console.WriteLine("ProductName = " + myDataRow["ProductName"]);
      Console.WriteLine("UnitPrice = " + myDataRow["UnitPrice"]);
    }
  }
}