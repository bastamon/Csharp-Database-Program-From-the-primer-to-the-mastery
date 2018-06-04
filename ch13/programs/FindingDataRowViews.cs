/*
  FindingDataRowViews.cs illustrates the use of the Find() and
  FindRows() methods of a DataView to find DataRowView objects
*/

using System;
using System.Data;
using System.Data.SqlClient;

class FindingDataRowViews
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
    string sortExpression = "CustomerID";
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

    // use the Find() method of customersDV to find the index of
    // the DataRowView whose CustomerID is BSBEV
    int index = customersDV.Find("BSBEV");
    Console.WriteLine("BSBEV found at index " + index + "\n");

    // use the FindRows() method of customersDV to find the DataRowView
    // whose CustomerID is BSBEV
    DataRowView[] customersDRVs = customersDV.FindRows("BSBEV");
    foreach (DataRowView myDataRowView in customersDRVs)
    {
      for (int count = 0; count < customersDV.Table.Columns.Count; count++)
      {
        Console.WriteLine(myDataRowView[count]);
      }
      Console.WriteLine("");
    }
  }
}