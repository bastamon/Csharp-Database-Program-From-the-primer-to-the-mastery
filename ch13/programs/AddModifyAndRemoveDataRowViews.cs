/*
  AddModifyAndRemoveDataRowViews.cs illustrates how to
  add, modify, and remove DataRowView objects from a DataView
*/

using System;
using System.Data;
using System.Data.SqlClient;

class AddModifyAndRemoveDataRowViews
{
  public static void DisplayDataRow(
    DataRow myDataRow,
    DataTable myDataTable
  )
  {
    Console.WriteLine("\nIn DisplayDataRow()");
    foreach (DataColumn myDataColumn in myDataTable.Columns)
    {
      Console.WriteLine(myDataColumn + " = " +
        myDataRow[myDataColumn]);
    }
  }

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

    // set up the filter expression
    string filterExpression = "Country = 'UK'";

    // create a DataView object named customersDV
    DataView customersDV = new DataView();
    customersDV.Table = customersDT;
    customersDV.RowFilter = filterExpression;

    // add a new DataRowView (adds a DataRow to the DataTable)
    Console.WriteLine("\nCalling customersDV.AddNew()");
    DataRowView customerDRV = customersDV.AddNew();
    customerDRV["CustomerID"] = "J7COM";
    customerDRV["CompanyName"] = "J7 Company";
    customerDRV["Country"] = "UK";
    Console.WriteLine("customerDRV[\"CustomerID\"] = " +
      customerDRV["CustomerID"]);
    Console.WriteLine("customerDRV[\"CompanyName\"] = " +
      customerDRV["CompanyName"]);
    Console.WriteLine("customerDRV[\"Country\"] = " +
      customerDRV["Country"]);
    Console.WriteLine("customerDRV.IsNew = " + customerDRV.IsNew);
    Console.WriteLine("customerDRV.IsEdit = " + customerDRV.IsEdit);
    customerDRV.EndEdit();

    // get and display the underlying DataRow
    DataRow customerDR = customerDRV.Row;
    DisplayDataRow(customerDR, customersDT);

    // modify the CompanyName of customerDRV
    Console.WriteLine("\nSetting customersDV[0][\"CompanyName\"] to Widgets Inc.");
    customersDV[0].BeginEdit();
    customersDV[0]["CompanyName"] = "Widgets Inc.";
    Console.WriteLine("customersDV[0][\"CustomerID\"] = " +
      customersDV[0]["CustomerID"]);
    Console.WriteLine("customersDV[0][\"CompanyName\"] = " +
      customersDV[0]["CompanyName"]);
    Console.WriteLine("customersDV[0].IsNew = " + customersDV[0].IsNew);
    Console.WriteLine("customersDV[0].IsEdit = " + customersDV[0].IsEdit);
    customersDV[0].EndEdit();

    // display the underlying DataRow
    DisplayDataRow(customersDV[0].Row, customersDT);

    // remove the second DataRowView from customersDV
    Console.WriteLine("\ncustomersDV[1][\"CustomerID\"] = " +
      customersDV[1]["CustomerID"]);
    Console.WriteLine("\nCalling customersDV.Delete(1)");
    customersDV.Delete(1);
    Console.WriteLine("customersDV[1].IsNew = " + customersDV[1].IsNew);
    Console.WriteLine("customersDV[1].IsEdit = " + customersDV[1].IsEdit);

    // remove the third DataRowView from customersDV
    Console.WriteLine("\ncustomersDV[2][\"CustomerID\"] = " +
      customersDV[2]["CustomerID"]);
    Console.WriteLine("\nCalling customersDV[2].Delete()");
    customersDV[2].Delete();

    // call the AcceptChanges() method of customersDT to
    // make the deletes permanent in customersDT
    customersDT.AcceptChanges();

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