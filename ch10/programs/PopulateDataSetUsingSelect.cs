/*
  PopulateDataSetUsingSelect.cs illustrates how to populate a DataSet
  object using a SELECT statement
*/

using System;
using System.Data;
using System.Data.SqlClient;

class PopulateDataSetUsingSelect
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

    // create a SqlDataAdapter object and set its SelectCommand
    // property to the SqlCommand object
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;

    // create a DataSet object
    DataSet myDataSet = new DataSet();

    // open the database connection
    mySqlConnection.Open();

    // use the Fill() method of the SqlDataAdapter object to
    // retrieve the rows from the table, storing the rows locally
    // in a DataTable of the DataSet object
    Console.WriteLine("Retrieving rows from the Products table");
    int numberOfRows = mySqlDataAdapter.Fill(myDataSet, "Products");
    Console.WriteLine("numberOfRows = " + numberOfRows);

    // close the database connection
    mySqlConnection.Close();

    // get the DataTable object from the DataSet object
    DataTable myDataTable = myDataSet.Tables["Products"];

    // display the column values for each row in the DataTable,
    // using a DataRow object to access each row in the DataTable
    foreach (DataRow myDataRow in myDataTable.Rows)
    {
      Console.WriteLine("ProductID = " + myDataRow["ProductID"]);
      Console.WriteLine("ProductName = " + myDataRow["ProductName"]);
      Console.WriteLine("UnitPrice = " + myDataRow["UnitPrice"]);
    }
  }
}