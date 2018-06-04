/*
  ExecuteInsertUpdateDelete.cs illustrates how to use the
  ExecuteNonQuery() method to run INSERT, UPDATE,
  and DELETE statements
*/

using System;
using System.Data;
using System.Data.SqlClient;

class ExecuteInsertUpdateDelete
{

  public static void DisplayRow(
    SqlCommand mySqlCommand, string CustomerID
  )
  {

    mySqlCommand.CommandText =
      "SELECT CustomerID, CompanyName " +
      "FROM Customers " +
      "WHERE CustomerID = '" + CustomerID + "'";

    SqlDataReader mySqlDataReader =
      mySqlCommand.ExecuteReader();

    while (mySqlDataReader.Read())
    {
      Console.WriteLine("mySqlDataReader[\"CustomerID\"] = " +
        mySqlDataReader["CustomerID"]);
      Console.WriteLine("mySqlDataReader[\"CompanyName\"] = " +
        mySqlDataReader["CompanyName"]);
    }

    mySqlDataReader.Close();

  }

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    // create a SqlCommand object and set its Commandtext property
    // to an INSERT statement
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "INSERT INTO Customers (" +
      "  CustomerID, CompanyName" +
      ") VALUES (" +
      "  'J2COM', 'Jason Price Corporation'" +
      ")";

    mySqlConnection.Open();

    // call the ExecuteNonQuery() method of the SqlCommand object
    // to run the INSERT statement
    int numberOfRows = mySqlCommand.ExecuteNonQuery();
    Console.WriteLine("Number of rows added = " + numberOfRows);
    DisplayRow(mySqlCommand, "J2COM");

    // set the CommandText property of the SqlCommand object to
    // an UPDATE statement
    mySqlCommand.CommandText =
      "UPDATE Customers " +
      "SET CompanyName = 'New Company' " +
      "WHERE CustomerID = 'J2COM'";

    // call the ExecuteNonQuery() method of the SqlCommand object
    // to run the UPDATE statement
    numberOfRows = mySqlCommand.ExecuteNonQuery();
    Console.WriteLine("Number of rows updated = " + numberOfRows);
    DisplayRow(mySqlCommand, "J2COM");

    // set the CommandText property of the SqlCommand object to
    // a DELETE statement
    mySqlCommand.CommandText =
      "DELETE FROM Customers " +
      "WHERE CustomerID = 'J2COM'";

    // call the ExecuteNonQuery() method of the SqlCommand object
    // to run the DELETE statement
    numberOfRows = mySqlCommand.ExecuteNonQuery();
    Console.WriteLine("Number of rows deleted = " + numberOfRows);

    mySqlConnection.Close();

  }

}