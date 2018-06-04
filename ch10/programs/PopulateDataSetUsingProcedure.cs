/*
  PopulateDataSetUsingProcedure.cs illustrates how to populate a
  DataSet object using a stored procedure
*/

using System;
using System.Data;
using System.Data.SqlClient;

class PopulateDataSetUsingProcedure
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    // create a SqlCommand object and set its CommandText property
    // to call the CustOrderHist() stored procedure
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "EXECUTE CustOrderHist @CustomerID";
    mySqlCommand.Parameters.Add(
      "@CustomerID", SqlDbType.NVarChar, 5).Value = "ALFKI";

    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    Console.WriteLine("Retrieving rows from the CustOrderHist() Procedure");
    int numberOfRows = mySqlDataAdapter.Fill(myDataSet, "CustOrderHist");
    Console.WriteLine("numberOfRows = " + numberOfRows);
    mySqlConnection.Close();

    DataTable myDataTable = myDataSet.Tables["CustOrderHist"];
    foreach (DataRow myDataRow in myDataTable.Rows)
    {
      Console.WriteLine("ProductName = " + myDataRow["ProductName"]);
      Console.WriteLine("Total = " + myDataRow["Total"]);
    }
  }
}