/*
  UsingDataViewManager.cs illustrates the use of a
  DataViewManager object
*/

using System;
using System.Data;
using System.Data.SqlClient;

class UsingDataViewManager
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

    // create a DataViewManager object named myDVM
    DataViewManager myDVM = new DataViewManager(myDataSet);

    // set the Sort and RowFilter properties for the Customers DataTable
    myDVM.DataViewSettings["Customers"].Sort = "CustomerID";
    myDVM.DataViewSettings["Customers"].RowFilter = "Country = 'UK'";

    // display the DataViewSettingCollectionString property of myDVM
    Console.WriteLine("myDVM.DataViewSettingCollectionString = " +
      myDVM.DataViewSettingCollectionString + "\n");

    // call the CreateDataView() method of myDVM to create a DataView
    // named customersDV for the customersDT DataTable
    DataView customersDV = myDVM.CreateDataView(customersDT);

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