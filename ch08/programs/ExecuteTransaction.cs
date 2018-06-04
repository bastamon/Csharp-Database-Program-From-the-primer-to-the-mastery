/*
  ExecuteTransaction.cs illustrates the use of a transaction
*/

using System;
using System.Data;
using System.Data.SqlClient;

class ExecuteTransaction
{

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    mySqlConnection.Open();

    // step 1: create a SqlTransaction object and start the transaction
    // by calling the BeginTransaction() method of the SqlConnection
    // object
    SqlTransaction mySqlTransaction =
      mySqlConnection.BeginTransaction();

    // step 2: create a SqlCommand object to hold a SQL statement
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

    // step 3: set the Transaction property for the SqlCommand object
    mySqlCommand.Transaction = mySqlTransaction;

    // step 4: set the CommandText property of the SqlCommand object to
    // the first INSERT statement
    mySqlCommand.CommandText =
      "INSERT INTO Customers (" +
      "  CustomerID, CompanyName" +
      ") VALUES (" +
      "  'J3COM', 'Jason Price Corporation'" +
      ")";

    // step 5: run the first INSERT statement
    Console.WriteLine("Running first INSERT statement");
    mySqlCommand.ExecuteNonQuery();

    // step 6: set the CommandText property of the SqlCommand object to
    // the second INSERT statement
    mySqlCommand.CommandText =
      "INSERT INTO Orders (" +
      "  CustomerID" +
      ") VALUES (" +
      "  'J3COM'" +
      ")";

    // step 7: run the second INSERT statement
    Console.WriteLine("Running second INSERT statement");
    mySqlCommand.ExecuteNonQuery();

    // step 8: commit the transaction using the Commit() method
    // of the SqlTransaction object
    Console.WriteLine("Committing transaction");
    mySqlTransaction.Commit();

    mySqlConnection.Close();

  }

}