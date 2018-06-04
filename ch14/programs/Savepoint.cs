/*
  Savepoint.cs illustrates how to set a savepoint in a transaction
*/

using System;
using System.Data;
using System.Data.SqlClient;

class Savepoint
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );
    mySqlConnection.Open();

    // step 1: create a SqlTransaction object
    SqlTransaction mySqlTransaction =
      mySqlConnection.BeginTransaction();

    // step 2: create a SqlCommand and set its Transaction property
    // to mySqlTransaction
    SqlCommand mySqlCommand =
      mySqlConnection.CreateCommand();
    mySqlCommand.Transaction = mySqlTransaction;

    // step 3: insert a row into the Customers table
    Console.WriteLine("Inserting a row into the Customers table " +
      "with a CustomerID of J8COM");
    mySqlCommand.CommandText =
      "INSERT INTO Customers ( " +
      "  CustomerID, CompanyName " +
      ") VALUES ( " +
      "  'J8COM', 'J8 Company' " +
      ")";
    int numberOfRows = mySqlCommand.ExecuteNonQuery();
    Console.WriteLine("Number of rows inserted = " + numberOfRows);

    // step 4: set a savepoint by calling the Save() method of
    // mySqlTransaction, passing the name "SaveCustomer" to
    // the Save() method
    mySqlTransaction.Save("SaveCustomer");

    // step 5: insert a row into the Orders table
    Console.WriteLine("Inserting a row into the Orders table " +
      "with a CustomerID of J8COM");
    mySqlCommand.CommandText =
      "INSERT INTO Orders ( " +
      "  CustomerID " +
      ") VALUES ( " +
      "  'J8COM' " +
      ")";
    numberOfRows = mySqlCommand.ExecuteNonQuery();
    Console.WriteLine("Number of rows inserted = " + numberOfRows);

    // step 6: rollback to the savepoint set in step 4
    Console.WriteLine("Performing a rollback to the savepoint");
    mySqlTransaction.Rollback("SaveCustomer");

    // step 7: display the new row added to the Customers table
    mySqlCommand.CommandText =
      "SELECT CustomerID, CompanyName " +
      "FROM Customers " +
      "WHERE CustomerID = 'J8COM'";
    SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
    while (mySqlDataReader.Read())
    {
      Console.WriteLine("mySqlDataReader[\"CustomerID\"] = " +
        mySqlDataReader["CustomerID"]);
      Console.WriteLine("mySqlDataReader[\"CompanyName\"] = " +
        mySqlDataReader["CompanyName"]);
    }
    mySqlDataReader.Close();

    // step 8: delete the new row from the Customers table
    Console.WriteLine("Deleting row with CustomerID of J8COM");
    mySqlCommand.CommandText =
      "DELETE FROM Customers " +
      "WHERE CustomerID = 'J8COM'";
    numberOfRows = mySqlCommand.ExecuteNonQuery();
    Console.WriteLine("Number of rows deleted = " + numberOfRows);

    // step 9: commit the transaction
    Console.WriteLine("Committing the transaction");
    mySqlTransaction.Commit();

    mySqlConnection.Close();
  }
}