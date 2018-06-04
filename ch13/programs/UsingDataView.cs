/*
  UsingDataView.cs illustrates the use of a DataView object to
  filter and sort rows
*/

using System;
using System.Data;
using System.Data.SqlClient;

class UsingDataView
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

    // set up the filter and sort expressions
    string filterExpression = "Country = 'UK'";
    string sortExpression = "CustomerID ASC, CompanyName DESC";
    DataViewRowState rowStateFilter = DataViewRowState.OriginalRows;

    // create a DataView object named customersDV
    DataView customersDV = new DataView();
    customersDV.Table = customersDT;
    customersDV.RowFilter = filterExpression;
    customersDV.Sort = sortExpression;
    customersDV.RowStateFilter = rowStateFilter;

    // display the rows in the customersDV DataView object
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