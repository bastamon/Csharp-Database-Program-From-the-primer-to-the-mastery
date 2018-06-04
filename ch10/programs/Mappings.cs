/*
  Mappings.cs illustrates how to map table and column names
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

class Mappings
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT CustomerID AS MyCustomer, CompanyName, Address " +
      "FROM Customers AS Cust " +
      "WHERE CustomerID = 'ALFKI'";
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    mySqlDataAdapter.Fill(myDataSet, "Customers");
    mySqlConnection.Close();

    // create a DataTableMapping object
    DataTableMapping myDataTableMapping =
      mySqlDataAdapter.TableMappings.Add("Customers", "Cust");

    // change the TableName property of the DataTable object
    myDataSet.Tables["Customers"].TableName = "Cust";

    // display the DataSetTable and SourceTable properties
    Console.WriteLine("myDataTableMapping.DataSetTable = " +
      myDataTableMapping.DataSetTable);
    Console.WriteLine("myDataTableMapping.SourceTable = " +
      myDataTableMapping.SourceTable);

    // map the CustomerID column to MyCustomer
    myDataTableMapping.ColumnMappings.Add("CustomerID", "MyCustomer");

    DataTable myDataTable = myDataSet.Tables["Cust"];
    foreach (DataRow myDataRow in myDataTable.Rows)
    {
      Console.WriteLine("CustomerID = " + myDataRow["MyCustomer"]);
      Console.WriteLine("CompanyName = " + myDataRow["CompanyName"]);
      Console.WriteLine("Address = " + myDataRow["Address"]);
    }
  }
}