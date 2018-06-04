/*
  TransactionIsolation.cs illustrates how to set the
  transaction isolation level
*/

using System;
using System.Data;
using System.Data.SqlClient;

class TransactionIsolation
{
  public static void DisplayRows(
    SqlCommand mySqlCommand
  )
  {
    mySqlCommand.CommandText =
      "SELECT CustomerID, CompanyName " +
      "FROM Customers " +
      "WHERE CustomerID IN  ('ALFKI', 'J8COM')";
    SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
    while (mySqlDataReader.Read())
    {
      Console.WriteLine("mySqlDataReader[\"CustomerID\"] = " +
        mySqlDataReader["CustomerID"]);
      Console.WriteLine("mySqlDataReader[\"CompanyName\"] = " +
        mySqlDataReader["CompanyName"]);
    }
    mySqlDataReader.Close();
  }

  public static void PerformSerializableTransaction(
    SqlConnection mySqlConnection
  )
  {
    Console.WriteLine("\nIn PerformSerializableTransaction()");

    // create a SqlTransaction object and start the transaction
    // by calling the BeginTransaction() method of the SqlConnection
    // object, passing the IsolationLevel of Serializable to the method
    SqlTransaction serializableTrans =
      mySqlConnection.BeginTransaction(IsolationLevel.Serializable);

    // create a SqlCommand and set its Transaction property
    // to serializableTrans
    SqlCommand serializableCommand =
      mySqlConnection.CreateCommand();
    serializableCommand.Transaction = serializableTrans;

    // call the DisplayRows() method to display rows from
    // the Customers table
    DisplayRows(serializableCommand);

    // insert a new row into the Customers table
    Console.WriteLine("Inserting new row into Customers table " +
      "with CustomerID of J8COM");
    serializableCommand.CommandText =
      "INSERT INTO Customers ( " +
      "CustomerID, CompanyName " +
      ") VALUES ( " +
      "  'J8COM', 'J8 Company' " +
      ")";
    int numberOfRows = serializableCommand.ExecuteNonQuery();
    Console.WriteLine("Number of rows inserted = " + numberOfRows);

    // update a row in the Customers table
    Console.WriteLine("Setting CompanyName to 'Widgets Inc.' for " +
      "row with CustomerID of ALFKI");
    serializableCommand.CommandText =
      "UPDATE Customers " +
      "SET CompanyName = 'Widgets Inc.' " +
      "WHERE CustomerID = 'ALFKI'";
    numberOfRows = serializableCommand.ExecuteNonQuery();
    Console.WriteLine("Number of rows updated = " + numberOfRows);

    DisplayRows(serializableCommand);

    // commit the transaction
    serializableTrans.Commit();
  }

  public static void PerformReadCommittedTransaction(
    SqlConnection mySqlConnection
  )
  {
    Console.WriteLine("\nIn PerformReadCommittedTransaction()");

    // create a SqlTransaction object and start the transaction
    // by calling the BeginTransaction() method of the SqlConnection
    // object, passing the IsolationLevel of ReadCommitted to the method
    // (ReadCommitted is actually the default)
    SqlTransaction readCommittedTrans =
      mySqlConnection.BeginTransaction(IsolationLevel.ReadCommitted);

    // create a SqlCommand and set its Transaction property
    // to readCommittedTrans
    SqlCommand readCommittedCommand =
      mySqlConnection.CreateCommand();
    readCommittedCommand.Transaction = readCommittedTrans;

    // update a row in the Customers table
    Console.WriteLine("Setting CompanyName to 'Alfreds Futterkiste' " +
      "for row with CustomerID of ALFKI");
    readCommittedCommand.CommandText =
      "UPDATE Customers " +
      "SET CompanyName = 'Alfreds Futterkiste' " +
      "WHERE CustomerID = 'ALFKI'";
    int numberOfRows = readCommittedCommand.ExecuteNonQuery();
    Console.WriteLine("Number of rows updated = " + numberOfRows);

    // delete the new row from the Customers table
    Console.WriteLine("Deleting row with CustomerID of J8COM");
    readCommittedCommand.CommandText =
      "DELETE FROM Customers " +
      "WHERE CustomerID = 'J8COM'";
    numberOfRows = readCommittedCommand.ExecuteNonQuery();
    Console.WriteLine("Number of rows deleted = " + numberOfRows);

    DisplayRows(readCommittedCommand);

    // commit the transaction
    readCommittedTrans.Commit();
  }

  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );
    mySqlConnection.Open();
    PerformSerializableTransaction(mySqlConnection);
    PerformReadCommittedTransaction(mySqlConnection);
    mySqlConnection.Close();
  }
}