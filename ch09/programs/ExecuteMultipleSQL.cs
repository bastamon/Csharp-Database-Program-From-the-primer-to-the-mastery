/*
  ExecuteMultipleSQL.cs illustrates how to execute
  multiple SQL statements using a SqlCommand object
*/

using System;
using System.Data;
using System.Data.SqlClient;

class ExecuteMultipleSQL
{

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

    // set the CommandText property of the SqlCommand object to
    // the INSERT, UPDATE, and DELETE statements
    mySqlCommand.CommandText =
      "INSERT INTO Customers (CustomerID, CompanyName) " +
      "VALUES ('J5COM', 'Jason 5 Company');" +
      "SELECT CustomerID, CompanyName " +
      "FROM Customers " +
      "WHERE CustomerID = 'J5COM';" +
      "UPDATE Customers " +
      "SET CompanyName = 'Another Jason Company' " +
      "WHERE CustomerID = 'J5COM';" +
      "SELECT CustomerID, CompanyName " +
      "FROM Customers " +
      "WHERE CustomerID = 'J5COM';" +
      "DELETE FROM Customers " +
      "WHERE CustomerID = 'J5COM';";

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