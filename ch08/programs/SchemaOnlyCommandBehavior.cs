/*
  SchemaOnlyCommandBehavior.cs illustrates how to read a table schema
*/

using System;
using System.Data;
using System.Data.SqlClient;

class SchemaOnlyCommandBehavior
{

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT ProductID, ProductName, UnitPrice " +
      "FROM Products " +
      "WHERE ProductID = 1";

    mySqlConnection.Open();

    // pass the CommandBehavior.SchemaOnly constant to the
    // ExecuteReader() method to get the schema
    SqlDataReader productsSqlDataReader =
      mySqlCommand.ExecuteReader(CommandBehavior.SchemaOnly);

    // read the DataTable containing the schema from the DataReader
    DataTable myDataTable = productsSqlDataReader.GetSchemaTable();

    // display the rows and columns in the DataTable
    foreach (DataRow myDataRow in myDataTable.Rows)
    {
      Console.WriteLine("\nNew column details follow:");
      foreach (DataColumn myDataColumn in myDataTable.Columns)
      {
        Console.WriteLine(myDataColumn + " = " +
          myDataRow[myDataColumn]);
        if (myDataColumn.ToString() == "ProviderType")
        {
          Console.WriteLine(myDataColumn + " = " +
            ((System.Data.SqlDbType) myDataRow[myDataColumn]));
        }
      }
    }

    productsSqlDataReader.Close();
    mySqlConnection.Close();

  }

}