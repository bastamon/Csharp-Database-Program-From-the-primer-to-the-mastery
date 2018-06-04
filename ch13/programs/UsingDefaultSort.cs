/*
  UsingDefaultSort.cs illustrates how to use the default
  sort algorithm
*/

using System;
using System.Data;
using System.Data.SqlClient;

class UsingDefaultSort
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT CustomerID, CompanyName, Country " +
      "FROM Customers";
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    mySqlDataAdapter.Fill(myDataSet, "Customers");
    mySqlConnection.Close();
    DataTable customersDT = myDataSet.Tables["Customers"];

    // set the PrimaryKey property of customersDT
    customersDT.PrimaryKey =
      new DataColumn[]
      {
        customersDT.Columns["CustomerID"]
      };

    // create a DataView object named customersDV
    DataView customersDV = new DataView();
    customersDV.Table = customersDT;
    customersDV.RowFilter = "Country = 'UK'";

    // set the ApplyDefaultProperty of customersDV to true
    customersDV.ApplyDefaultSort = true;
    Console.WriteLine("customersDV.Sort = " + customersDV.Sort);

    // display the rows in the customersDV DataView object
    Console.WriteLine("\nDataRowView objects in customersDV:\n");
    foreach (DataRowView myDataRowView in customersDV)
    {
      for (int count = 0; count < customersDV.Table.Columns.Count; count++)
      {
        Console.WriteLine(myDataRowView[count]);
      }
      Console.WriteLine("");
    }
  }
}