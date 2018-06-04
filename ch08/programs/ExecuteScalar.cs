/*
  ExecuteScalar.cs illustrates how to use the ExecuteScalar()
  method to run a SELECT statement that returns a single value
*/

using System;
using System.Data;
using System.Data.SqlClient;

class ExecuteScalar
{

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT COUNT(*) " +
      "FROM Products";
    mySqlConnection.Open();

    // call the ExecuteScalar() method of the SqlCommand object
    // to run the SELECT statement
    int returnValue = (int) mySqlCommand.ExecuteScalar();
    Console.WriteLine("mySqlCommand.ExecuteScalar() = " +
      returnValue);

    mySqlConnection.Close();

  }

}