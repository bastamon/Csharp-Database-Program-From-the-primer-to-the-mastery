/*
  Deadlock.cs illustrates how two transactions can
  deadlock each other
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

class Deadlock
{
  // create two SqlConnection objects
  public static SqlConnection t1Connection =
    new SqlConnection(
      "server=localhost;database=Northwind;uid=sa;pwd=sa"
    );
  public static SqlConnection t2Connection =
    new SqlConnection(
      "server=localhost;database=Northwind;uid=sa;pwd=sa"
    );

  // declare two SqlTransaction objects
  public static SqlTransaction t1Trans;
  public static SqlTransaction t2Trans;

  // declare two SqlCommand objects
  public static SqlCommand t1Command;
  public static SqlCommand t2Command;

  public static void UpdateCustomerT1()
  {
    // update the row with a CustomerID of ALFKI
    // in the Customers table using t1Command
    Console.WriteLine("Setting CompanyName to 'Widgets Inc.' " +
      "for row with CustomerID of ALFKI using t1Command");
    t1Command.CommandText =
      "UPDATE Customers " +
      "SET CompanyName = 'Widgets Inc.' " +
      "WHERE CustomerID = 'ALFKI'";
    int numberOfRows = t1Command.ExecuteNonQuery();
    Console.WriteLine("Number of rows updated = " + numberOfRows);
  }

  public static void UpdateProductT2()
  {
    // update the row with a ProductID of 1
    // in the Products table using t2Command
    Console.WriteLine("Setting ProductName to 'Widget' " +
      "for the row with ProductID of 1 using t2Command");
    t2Command.CommandText =
      "UPDATE Products " +
      "SET ProductName = 'Widget' " +
      "WHERE ProductID = 1";
    int numberOfRows = t2Command.ExecuteNonQuery();
    Console.WriteLine("Number of rows updated = " + numberOfRows);
  }

  public static void UpdateProductT1()
  {
    // update the row with a ProductID of 1
    // in the Products table using t1Command
    Console.WriteLine("Setting ProductName to 'Chai' " +
      "for the row with ProductID of 1 using t1Command");
    t1Command.CommandText =
      "UPDATE Products " +
      "SET ProductName = 'Chai' " +
      "WHERE ProductID = 1";
    int numberOfRows = t1Command.ExecuteNonQuery();
    Console.WriteLine("Number of rows updated = " + numberOfRows);
  }

  public static void UpdateCustomerT2()
  {
    // update the row with a CustomerID of ALFKI
    // in the Customers table using t2Command
    Console.WriteLine("Setting CompanyName to 'Alfreds Futterkiste' " +
      "for row with CustomerID of ALFKI using t2Command");
    t2Command.CommandText =
      "UPDATE Customers " +
      "SET CompanyName = 'Alfreds Futterkiste' " +
      "WHERE CustomerID = 'ALFKI'";
    int numberOfRows = t2Command.ExecuteNonQuery();
    Console.WriteLine("Number of rows updated = " + numberOfRows);
  }

  public static void Main()
  {
    // open the first connection, begin the first transaction,
    // and set the lock timeout to 5 seconds
    t1Connection.Open();
    t1Trans = t1Connection.BeginTransaction();
    t1Command = t1Connection.CreateCommand();
    t1Command.Transaction = t1Trans;
    t1Command.CommandText = "SET LOCK_TIMEOUT 5000";
    t1Command.ExecuteNonQuery();

    // open the second connection, begin the second transaction,
    // and set the lock timeout to 5 seconds
    t2Connection.Open();
    t2Trans = t2Connection.BeginTransaction();
    t2Command = t2Connection.CreateCommand();
    t2Command.Transaction = t2Trans;
    t2Command.CommandText = "SET LOCK_TIMEOUT 5000";
    t2Command.ExecuteNonQuery();

    // set DEADLOCK_PRIORITY to LOW for the second transaction
    // so that it is the transaction that is rolled back
    t2Command.CommandText = "SET DEADLOCK_PRIORITY LOW";
    t2Command.ExecuteNonQuery();

    // create four threads that will perform the interleaved updates
    Thread updateCustThreadT1 = new Thread(new ThreadStart(UpdateCustomerT1));
    Thread updateProdThreadT2 = new Thread(new ThreadStart(UpdateProductT2));
    Thread updateProdThreadT1 = new Thread(new ThreadStart(UpdateProductT1));
    Thread updateCustThreadT2 = new Thread(new ThreadStart(UpdateCustomerT2));

    // start the threads to actually perform the interleaved updates
    updateCustThreadT1.Start();
    updateProdThreadT2.Start();
    updateProdThreadT1.Start();
    updateCustThreadT2.Start();
  }
}