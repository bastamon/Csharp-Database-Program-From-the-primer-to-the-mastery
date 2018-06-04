/*
  AddRestrictions.cs illustrates how to add constraints to
  DataTable objects and add restrictions to DataColumn objects
*/

using System;
using System.Data;
using System.Data.SqlClient;

class AddRestrictions
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT ProductID, ProductName " +
      "FROM Products;" +
      "SELECT OrderID " +
      "FROM Orders;" +
      "SELECT OrderID, ProductID, UnitPrice " +
      "FROM [Order Details];";
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    mySqlDataAdapter.Fill(myDataSet);
    mySqlConnection.Close();
    myDataSet.Tables["Table"].TableName = "Products";
    myDataSet.Tables["Table1"].TableName = "Orders";
    myDataSet.Tables["Table2"].TableName = "Order Details";

    // set the PrimaryKey property for the Products DataTable
    // to the ProductID column
    DataTable productsDataTable = myDataSet.Tables["Products"];
    DataColumn[] productsPrimaryKey =
      new DataColumn[]
      {
        productsDataTable.Columns["ProductID"]
      };
    productsDataTable.PrimaryKey = productsPrimaryKey;

    // set the PrimaryKey property for the Orders DataTable
    // to the OrderID column
    myDataSet.Tables["Orders"].PrimaryKey =
      new DataColumn[]
      {
        myDataSet.Tables["Orders"].Columns["OrderID"]
      };

    // set the PrimaryKey property for the Order Details DataTable
    // to the OrderID and ProductID columns
    myDataSet.Tables["Order Details"].Constraints.Add(
      "Primary key constraint on the OrderID and ProductID columns",
      new DataColumn[]
      {
        myDataSet.Tables["Order Details"].Columns["OrderID"],
        myDataSet.Tables["Order Details"].Columns["ProductID"]
      },
      true
    );

    // add a foreign key constraint on the OrderID column
    // of Order Details to the OrderID column of Orders
    ForeignKeyConstraint myFKC = new ForeignKeyConstraint(
      myDataSet.Tables["Orders"].Columns["OrderID"],
      myDataSet.Tables["Order Details"].Columns["OrderID"]
    );
    myDataSet.Tables["Order Details"].Constraints.Add(myFKC);

    // add a foreign key constraint on the ProductID column
    // of Order Details to the ProductID column of Products
    myDataSet.Tables["Order Details"].Constraints.Add(
      "Foreign key constraint to ProductID DataColumn of the " +
      "Products DataTable",
      myDataSet.Tables["Products"].Columns["ProductID"],
      myDataSet.Tables["Order Details"].Columns["ProductID"]
    );

    // set the AllowDBNull, AutoIncrement, AutoIncrementSeed,
    // AutoIncrementStep, ReadOnly, and Unique properties for
    // the ProductID DataColumn of the Products DataTable
    DataColumn productIDDataColumn =
      myDataSet.Tables["Products"].Columns["ProductID"];
    productIDDataColumn.AllowDBNull = false;
    productIDDataColumn.AutoIncrement = true;
    productIDDataColumn.AutoIncrementSeed = -1;
    productIDDataColumn.AutoIncrementStep = -1;
    productIDDataColumn.ReadOnly = true;
    productIDDataColumn.Unique = true;

    // set the MaxLength property for the ProductName DataColumn
    // of the Products DataTable
    myDataSet.Tables["Products"].Columns["ProductName"].MaxLength = 40;

    // display the details of the DataColumn objects for
    // the DataTable objects
    foreach (DataTable myDataTable in myDataSet.Tables)
    {
      Console.WriteLine("\n\nReading from the " +
        myDataTable + " DataTable:\n");

      // display the primary key
      foreach (DataColumn myPrimaryKey in myDataTable.PrimaryKey)
      {
        Console.WriteLine("myPrimaryKey = " + myPrimaryKey);
      }

      // display some of the details for each column
      foreach (DataColumn myDataColumn in myDataTable.Columns)
      {
        Console.WriteLine("\nmyDataColumn.ColumnName = " +
          myDataColumn.ColumnName);
        Console.WriteLine("myDataColumn.DataType = " +
          myDataColumn.DataType);

        Console.WriteLine("myDataColumn.AllowDBNull = " +
          myDataColumn.AllowDBNull);
        Console.WriteLine("myDataColumn.AutoIncrement = " +
          myDataColumn.AutoIncrement);
        Console.WriteLine("myDataColumn.AutoIncrementSeed = " +
          myDataColumn.AutoIncrementSeed);
        Console.WriteLine("myDataColumn.AutoIncrementStep = " +
          myDataColumn.AutoIncrementStep);
        Console.WriteLine("myDataColumn.MaxLength = " +
          myDataColumn.MaxLength);
        Console.WriteLine("myDataColumn.ReadOnly = " +
          myDataColumn.ReadOnly);
        Console.WriteLine("myDataColumn.Unique = " +
          myDataColumn.Unique);
      }
    }
  }
}