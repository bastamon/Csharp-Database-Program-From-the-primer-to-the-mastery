/*
  FirstExample.cs illustrates how to:
  1. Connect to the SQL Server Northwind database.
  2. Retrieve a row from the Customers table using
     a SQL SELECT statement.
  3. Display the columns from the row.
  4. Close the database connection.
*/

using System;
using System.Data.SqlClient;

class FirstExample
{
  public static void Main()
  {
    try
    {
      // step 1: create a SqlConnection object to connect to the
      // SQL Server Northwind database
      SqlConnection mySqlConnection =
        new SqlConnection(
          "server=localhost;database=Northwind;uid=sa;pwd=sa"
        );

      // step 2: create a SqlCommand object
      SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

      // step 3: set the CommandText property of the SqlCommand object to
      // a SQL SELECT statement that retrieves a row from the Customers table
      mySqlCommand.CommandText =
        "SELECT CustomerID, CompanyName, ContactName, Address " +
        "FROM Customers " +
        "WHERE CustomerID = 'ALFKI'";

      // step 4: open the database connection using the
      // Open() method of the SqlConnection object
      mySqlConnection.Open();

      // step 5: create a SqlDataReader object and call the ExecuteReader()
      // method of the SqlCommand object to run the SELECT statement
      SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

      // step 6: read the row from the SqlDataReader object using
      // the Read() method
      mySqlDataReader.Read();

      // step 7: display the column values
      Console.WriteLine("mySqlDataReader[\"CustomerID\"] = " +
        mySqlDataReader["CustomerID"]);
      Console.WriteLine("mySqlDataReader[\"CompanyName\"] = " +
        mySqlDataReader["CompanyName"]);
      Console.WriteLine("mySqlDataReader[\"ContactName\"] = " +
        mySqlDataReader["ContactName"]);
      Console.WriteLine("mySqlDataReader[\"Address\"] = " +
        mySqlDataReader["Address"]);

      // step 8: close the SqlDataReader object using the Close() method
      mySqlDataReader.Close();

      // step 9: close the SqlConnection object using the Close() method
      mySqlConnection.Close();
    }
    catch (SqlException e)
    {
      Console.WriteLine("A SqlException was thrown");
      Console.WriteLine("Number = " + e.Number);
      Console.WriteLine("Message = " + e.Message);
      Console.WriteLine("StackTrace:\n" + e.StackTrace);
    }
  }
}