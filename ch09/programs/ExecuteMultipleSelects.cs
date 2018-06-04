/*
  ExecuteMultipleSelects.cs illustrates how to execute
  multiple SELECT statements using a SqlCommand object
  and read the results using a SqlDataReader object
*/

using System;
using System.Data;
using System.Data.SqlClient;

class ExecuteSelect
{

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

    // set the CommandText property of the SqlCommand object to
    // the mutliple SELECT statements
    mySqlCommand.CommandText =
      "SELECT TOP 5 ProductID, ProductName " +
      "FROM Products " +
      "ORDER BY ProductID;" +
      "SELECT TOP 3 CustomerID, CompanyName " +
      "FROM Customers " +
      "ORDER BY CustomerID;" +
      "SELECT TOP 6 OrderID, CustomerID " +
      "FROM Orders " +
      "ORDER BY OrderID;";

    mySqlConnection.Open();

    SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

    // read the result sets from the SqlDataReader object using
    // the Read() and NextResult() methods
    do
    {
      while (mySqlDataReader.Read())
      {
        Console.WriteLine("mySqlDataReader[0] = " +
          mySqlDataReader[0]);
        Console.WriteLine("mySqlDataReader[1] = " +
          mySqlDataReader[1]);
      }
      Console.WriteLine("");  // visually split the results
    } while (mySqlDataReader.NextResult());

    mySqlDataReader.Close();
    mySqlConnection.Close();

  }

}