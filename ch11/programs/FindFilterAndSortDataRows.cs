/*
  FindFilterAndSortDataRows.cs illustrates how to find, filter,
  and sort DataRow objects
*/

using System;
using System.Data;
using System.Data.SqlClient;

class FindFilterAndSortDataRows
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT TOP 10 ProductID, ProductName " +
      "FROM Products " +
      "ORDER BY ProductID;" +
      "SELECT TOP 10 OrderID, ProductID, UnitPrice, Quantity " +
      "FROM [Order Details] " +
      "ORDER BY OrderID";
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    mySqlDataAdapter.Fill(myDataSet);
    mySqlConnection.Close();
    myDataSet.Tables["Table"].TableName = "Products";
    myDataSet.Tables["Table1"].TableName = "Order Details";

    // set the PrimaryKey property for the Products DataTable
    // to the ProductID column
    DataTable productsDataTable = myDataSet.Tables["Products"];
    productsDataTable.PrimaryKey =
      new DataColumn[]
      {
        productsDataTable.Columns["ProductID"]
      };

    // set the PrimaryKey property for the Order Details DataTable
    // to the OrderID and ProductID columns
    DataTable orderDetailsDataTable = myDataSet.Tables["Order Details"];
    orderDetailsDataTable.Constraints.Add(
      "Primary key constraint on the OrderID and ProductID columns",
      new DataColumn[]
      {
        orderDetailsDataTable.Columns["OrderID"],
        orderDetailsDataTable.Columns["ProductID"]
      },
      true
    );

    // find product with ProductID of 3 using the Find() method
    // to locate the DataRow using its primary key value
    Console.WriteLine("Using the Find() method to locate DataRow object " +
      "with a ProductID of 3");
    DataRow productDataRow = productsDataTable.Rows.Find("3");
    foreach (DataColumn myDataColumn in productsDataTable.Columns)
    {
      Console.WriteLine(myDataColumn + " = " + productDataRow[myDataColumn]);
    }

    // find order with OrderID of 10248 and ProductID of 11 using
    // the Find() method
    Console.WriteLine("Using the Find() method to locate DataRow object " +
      "with an OrderID of 10248 and a ProductID of 11");
    object[] orderDetails =
      new object[]
      {
        10248,
        11
      };
    DataRow orderDetailDataRow = orderDetailsDataTable.Rows.Find(orderDetails);
    foreach (DataColumn myDataColumn in orderDetailsDataTable.Columns)
    {
      Console.WriteLine(myDataColumn + " = " + orderDetailDataRow[myDataColumn]);
    }

    // filter and sort the DataRow objects in productsDataTable
    // using the Select() method
    Console.WriteLine("Using the Select() method to filter and sort DataRow objects");
    DataRow[] productDataRows =
      productsDataTable.Select("ProductID <= 5", "ProductID DESC",
        DataViewRowState.OriginalRows);
    foreach (DataRow myDataRow in productDataRows)
    {
      foreach (DataColumn myDataColumn in productsDataTable.Columns)
      {
        Console.WriteLine(myDataColumn + " = " + myDataRow[myDataColumn]);
      }
    }

    // filter and sort the DataRow objects in productsDataTable
    // using the Select() method
    Console.WriteLine("Using the Select() method to filter and sort DataRow objects");
    productDataRows =
      productsDataTable.Select("ProductName LIKE 'Cha*'",
        "ProductID ASC, ProductName DESC");
    foreach (DataRow myDataRow in productDataRows)
    {
      foreach (DataColumn myDataColumn in productsDataTable.Columns)
      {
        Console.WriteLine(myDataColumn + " = " + myDataRow[myDataColumn]);
      }
    }
  }
}