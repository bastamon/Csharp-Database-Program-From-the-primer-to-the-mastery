/*
  Block.cs illustrates how a serializable command locks
  the rows it retrieves so that a second transaction
  cannot get a lock to update one of these retrieved rows
  that has already been locked
*/

using System;
using System.Data;
using System.Data.SqlClient;

class Block
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

  public static void Main()
  {
    // create and open two SqlConnection objects
    SqlConnection serConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );
    SqlConnection rcConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );
    serConnection.Open();
    rcConnection.Open();

    // create the first SqlTransaction object and start the transaction
    // by calling the BeginTransaction() method of the SqlConnection
    // object, passing the IsolationLevel of Serializable to the method
    SqlTransaction serializableTrans =
      serConnection.BeginTransaction(IsolationLevel.Serializable);

    // create a SqlCommand and set its Transaction property
    // to serializableTrans
    SqlCommand serializableCommand =
      serConnection.CreateCommand();
    serializableCommand.Transaction = serializableTrans;

    // call the DisplayRows() method to display rows from
    // the Customers table;
    // this causes the rows to be locked, if you comment
    // out the following line then the INSERT and UPDATE
    // performed later by the second transaction will succeed
    DisplayRows(serializableCommand);  // *

    // create the second SqlTransaction object
    SqlTransaction readCommittedTrans =
      rcConnection.BeginTransaction(IsolationLevel.ReadCommitted);

    // create a SqlCommand and set its Transaction property
    // to readCommittedTrans
    SqlCommand readCommittedCommand =
      rcConnection.CreateCommand();
    readCommittedCommand.Transaction = readCommittedTrans;

    // set the lock timeout to 1 second using the
    // SET LOCK_TIMEOUT command
    readCommittedCommand.CommandText = "SET LOCK_TIMEOUT 1000";
    readCommittedCommand.ExecuteNonQuery();

    try
    {
      // insert a new row into the Customers table
      Console.WriteLine("Inserting new row into Customers table " +
        "with CustomerID of J8COM");
      readCommittedCommand.CommandText =
        "INSERT INTO Customers ( " +
        "CustomerID, CompanyName " +
        ") VALUES ( " +
        "  'J8COM', 'J8 Company' " +
        ")";
      int numberOfRows = readCommittedCommand.ExecuteNonQuery();
      Console.WriteLine("Number of rows inserted = " + numberOfRows);

      // update the ALFKI row in the Customers table
      Console.WriteLine("Setting CompanyName to 'Widgets Inc.' for " +
        "for row with CustomerID of ALFKI");
      readCommittedCommand.CommandText =
        "UPDATE Customers " +
        "SET CompanyName = 'Widgets Inc.' " +
        "WHERE CustomerID = 'ALFKI'";
      numberOfRows = readCommittedCommand.ExecuteNonQuery();
      Console.WriteLine("Number of rows updated = " + numberOfRows);

      // display the new rows and rollback the changes
      DisplayRows(readCommittedCommand);
      Console.WriteLine("Rolling back changes");
      readCommittedTrans.Rollback();
    }
    catch (SqlException e)
    {
      Console.WriteLine(e);
    }
    finally
    {
      serConnection.Close();
      rcConnection.Close();
    }
  }
}