/*
  ExecuteSelect.cs illustrates how to execute a SELECT
  statement using a SqlCommand object
*/

using System;
using System.Data;
using System.Data.SqlClient;

class ExecuteSelect
{

  public static void Main()
  {

    // create a SqlConnection object to connect to the database
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    // create a SqlCommand object
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

    // set the CommandText property of the SqlCommand object to
    // the SELECT statement
    mySqlCommand.CommandText =
      "SELECT TOP 5 CustomerID, CompanyName, ContactName, Address " +
      "FROM Customers " +
      "ORDER BY CustomerID";

    // open the database connection using the
    // Open() method of the SqlConnection object
    mySqlConnection.Open();

    // create a SqlDataReader object and call the ExecuteReader()
    // method of the SqlCommand object to run the SQL SELECT statement
    SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

    // read the rows from the SqlDataReader object using
    // the Read() method
    while (mySqlDataReader.Read())
    {
      Console.WriteLine("mySqlDataReader[\"CustomerID\"] = " +
        mySqlDataReader["CustomerID"]);
      Console.WriteLine("mySqlDataReader[\"CompanyName\"] = " +
        mySqlDataReader["CompanyName"]);
      Console.WriteLine("mySqlDataReader[\"ContactName\"] = " +
        mySqlDataReader["ContactName"]);
      Console.WriteLine("mySqlDataReader[\"Address\"] = " +
        mySqlDataReader["Address"]);
    }

    // close the SqlDataReader object using the Close() method
    mySqlDataReader.Close();

    // close the SqlConnection object using the Close() method
    mySqlConnection.Close();

  }

}