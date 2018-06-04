/*
  CreateChildDataView.cs illustrates how to create a
  child DataView
*/

using System;
using System.Data;
using System.Data.SqlClient;

class CreateChildDataView
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
      "FROM Customers;" +
      "SELECT OrderID, CustomerID " +
      "FROM Orders;";
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    mySqlDataAdapter.Fill(myDataSet);
    mySqlConnection.Close();
    myDataSet.Tables["Table"].TableName = "Customers";
    myDataSet.Tables["Table1"].TableName = "Orders";
    DataTable customersDT = myDataSet.Tables["Customers"];
    DataTable ordersDT = myDataSet.Tables["Orders"];

    // add a DataRelation object to myDataSet
    DataRelation customersOrdersDataRel =
      new DataRelation(
        "CustomersOrders",
        customersDT.Columns["CustomerID"],
        ordersDT.Columns["CustomerID"]
      );
    myDataSet.Relations.Add(
      customersOrdersDataRel
    );

    // create a DataView object named customersDV
    DataView customersDV = new DataView();
    customersDV.Table = customersDT;
    customersDV.RowFilter = "Country = 'UK'";
    customersDV.Sort = "CustomerID";

    // display the first row in the customersDV DataView object
    Console.WriteLine("Customer:");
    for (int count = 0; count < customersDV.Table.Columns.Count; count++)
    {
      Console.WriteLine(customersDV[0][count]);
    }

    // create a child DataView named ordersDV that views
    // the child rows for the first customer in customersDV
    DataView ordersDV = customersDV[0].CreateChildView("CustomersOrders");

    // display the child rows in the customersDV DataView object
    Console.WriteLine("\nOrderID's of the orders placed by this customer:");
    foreach (DataRowView ordersDRV in ordersDV)
    {
      Console.WriteLine(ordersDRV["OrderID"]);
    }
  }
}