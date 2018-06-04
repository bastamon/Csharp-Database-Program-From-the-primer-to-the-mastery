/*
  FillSchema.cs illustrates how to read schema information
  using the FillSchema() method of a DataAdapter object
*/

using System;
using System.Data;
using System.Data.SqlClient;

class FillSchema
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
    mySqlDataAdapter.FillSchema(myDataSet, SchemaType.Mapped);
    mySqlConnection.Close();
    myDataSet.Tables["Table"].TableName = "Products";
    myDataSet.Tables["Table1"].TableName = "Orders";
    myDataSet.Tables["Table2"].TableName = "Order Details";

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

      // display the constraints
      foreach (Constraint myConstraint in myDataTable.Constraints)
      {
        Console.WriteLine("myConstraint.IsPrimaryKey = " + ((UniqueConstraint) myConstraint).IsPrimaryKey);
        foreach (DataColumn myDataColumn in ((UniqueConstraint) myConstraint).Columns)
        {
          Console.WriteLine("myDataColumn.ColumnName = " + myDataColumn.ColumnName);
        }
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