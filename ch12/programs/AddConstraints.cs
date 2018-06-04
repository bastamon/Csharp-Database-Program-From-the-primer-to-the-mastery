/*
  AddConstraints.cs illustrates how to add a UniqueConstraint
  and a ForeignKeyConstraint to DataTable objects
*/

using System;
using System.Data;
using System.Data.SqlClient;

class AddConstraints
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT CustomerID, CompanyName " +
      "FROM Customers " +
      "WHERE CustomerID = 'ALFKI'" +
      "SELECT OrderID, CustomerID " +
      "FROM Orders " +
      "WHERE CustomerID = 'ALFKI';";
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

    // add a UniqueConstraint to customersDT
    UniqueConstraint myUC =
      new UniqueConstraint(
        "UniqueConstraintCustomerID",
        customersDT.Columns["CustomerID"],
        true
      );
    customersDT.Constraints.Add(myUC);

    // add a ForeignKeyConstraint on the CustomerID DataColumn
    // of ordersDT to the CustomerID DataColumn of customersDT
    ForeignKeyConstraint myFKC =
      new ForeignKeyConstraint(
        "ForeignKeyConstraintCustomersOrders",
        customersDT.Columns["CustomerID"],
        ordersDT.Columns["CustomerID"]
      );
    ordersDT.Constraints.Add(myFKC);

    // get the UniqueConstraint from customersDT
    // and display its properties
    Console.WriteLine("\ncustomersDT Unique constraint details:");
    myUC =
      (UniqueConstraint) customersDT.Constraints["UniqueConstraintCustomerID"];
    Console.WriteLine("Columns:");
    foreach (DataColumn myDataColumn in myUC.Columns)
    {
      Console.WriteLine("  " + myDataColumn);
    }
    Console.WriteLine("myUC.ConstraintName = " + myUC.ConstraintName);
    Console.WriteLine("myUC.IsPrimaryKey = " + myUC.IsPrimaryKey);
    Console.WriteLine("myUC.Table = " + myUC.Table);

    // get the ForeignKeyConstraint from ordersDT
    // and display its properties
    Console.WriteLine("\nordersDT Foreign key constraint details:");
    myFKC =
      (ForeignKeyConstraint)
        ordersDT.Constraints["ForeignKeyConstraintCustomersOrders"];
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
  }
}