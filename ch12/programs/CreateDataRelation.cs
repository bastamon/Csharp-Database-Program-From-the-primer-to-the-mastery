/*
  CreateDataRelation.cs illustrates how to create a
  DataRelation between two DataTable objects and then
  navigate the rows in the two DataTable objects
*/

using System;
using System.Data;
using System.Data.SqlClient;

class CreateDataRelation
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT TOP 2 CustomerID, CompanyName " +
      "FROM Customers " +
      "ORDER BY CustomerID;" +
      "SELECT OrderID, CustomerID " +
      "FROM Orders " +
      "WHERE CustomerID IN (" +
      "  SELECT TOP 2 CustomerID " +
      "  FROM Customers " +
      "  ORDER BY CustomerID" +
      ")";
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

    // create a DataRelation object named customersOrdersDataRel
    DataRelation customersOrdersDataRel =
      new DataRelation(
        "CustomersOrders",
        customersDT.Columns["CustomerID"],
        ordersDT.Columns["CustomerID"]
      );

    // use the Add() method through the Relations property
    // to add the customersOrdersDataRel DataRelation to
    // myDataSet
    myDataSet.Relations.Add(
      customersOrdersDataRel
    );

    // get the DataRelation just added to myDataSet
    customersOrdersDataRel =
      myDataSet.Relations["CustomersOrders"];

    // get the UniqueConstraint from customersOrdersDataRel
    // and display its properties
    Console.WriteLine("\nUnique constraint details:");
    UniqueConstraint myUC =
      customersOrdersDataRel.ParentKeyConstraint;
    Console.WriteLine("Columns:");
    foreach (DataColumn myDataColumn in myUC.Columns)
    {
      Console.WriteLine("  " + myDataColumn);
    }
    Console.WriteLine("myUC.ConstraintName = " + myUC.ConstraintName);
    Console.WriteLine("myUC.IsPrimaryKey = " + myUC.IsPrimaryKey);
    Console.WriteLine("myUC.Table = " + myUC.Table);

    // get the ForeignKeyConstraint from customersOrdersDataRel
    // and display its properties
    Console.WriteLine("\nForeign key constraint details:");
    ForeignKeyConstraint myFKC =
      customersOrdersDataRel.ChildKeyConstraint;
    Console.WriteLine("myFKC.AcceptRejectRule = " + myFKC.AcceptRejectRule);
    Console.WriteLine("Columns:");
    foreach (DataColumn myDataColumn in myFKC.Columns)
    {
      Console.WriteLine("  " + myDataColumn);
    }
    Console.WriteLine("myFKC.ConstraintName = " + myFKC.ConstraintName);
    Console.WriteLine("myFKC.DeleteRule = " + myFKC.DeleteRule);
    Console.WriteLine("RelatedColumns:");
    foreach (DataColumn relatedDataColumn in myFKC.RelatedColumns)
    {
      Console.WriteLine("  " + relatedDataColumn);
    }
    Console.WriteLine("myFKC.RelatedTable = " + myFKC.RelatedTable);
    Console.WriteLine("myFKC.Table = " + myFKC.Table);
    Console.WriteLine("myFKC.UpdateRule = " + myFKC.UpdateRule);

    // use the GetChildRows() method to navigate to DataRow objects
    // in the child DataTable (ordersDT in this case)
    foreach (DataRow customerDR in customersDT.Rows)
    {
      Console.WriteLine("\nCustomerID = " + customerDR["CustomerID"]);
      Console.WriteLine("CompanyName = " + customerDR["CompanyName"]);

      DataRow[] ordersDRs = customerDR.GetChildRows("CustomersOrders");
      Console.WriteLine("This customer placed the following orders:");
      foreach (DataRow orderDR in ordersDRs)
      {
        Console.WriteLine("  OrderID = " + orderDR["OrderID"]);
      }
    }

    // use the GetParentRow() method to navigate to DataRow object
    // in the parent DataTable (customersDT in this case)
    DataRow parentCustomerDR = ordersDT.Rows[0].GetParentRow("CustomersOrders");
    Console.WriteLine("\nOrder with OrderID of " + ordersDT.Rows[0]["OrderID"] +
      " was placed by the following customer:");
    Console.WriteLine("  CustomerID = " + parentCustomerDR["CustomerID"]);
  }
}