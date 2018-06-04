/*
  SingleRowCommandBehavior.cs illustrates how to control
  the command behavior to return a single row
*/

using System;
using System.Data;
using System.Data.SqlClient;

class SingleRowCommandBehavior
{

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT ProductID, ProductName, QuantityPerUnit, UnitPrice " +
      "FROM Products";

    mySqlConnection.Open();

    // pass the CommandBehavior.SingleRow value to the
    // ExecuteReader() method, indicating that the Command object
    // only returns a single row
    SqlDataReader mySqlDataReader =
      mySqlCommand.ExecuteReader(CommandBehavior.SingleRow);

    while (mySqlDataReader.Read())
    {
      Console.WriteLine("mySqlDataReader[\"ProductID\"] = " +
        mySqlDataReader["ProductID"]);
      Console.WriteLine("mySqlDataReader[\"ProductName\"] = " +
        mySqlDataReader["ProductName"]);
      Console.WriteLine("mySqlDataReader[\"QuantityPerUnit\"] = " +
        mySqlDataReader["QuantityPerUnit"]);
      Console.WriteLine("mySqlDataReader[\"UnitPrice\"] = " +
        mySqlDataReader["UnitPrice"]);
    }

    mySqlDataReader.Close();
    mySqlConnection.Close();

  }

}