/*
  MySqlConnection.cs illustrates how to use a
  SqlConnection object to connect to a SQL Server database
*/

using System;
using System.Data;
using System.Data.SqlClient;

class MySqlConnection
{
  public static void Main()
  {
    // formulate a string containing the details of the
    // database connection
    string connectionString =
      "server=localhost;database=Northwind;uid=sa;pwd=sa";

    // create a SqlConnection object to connect to the
    // database, passing the connection string to the constructor
    SqlConnection mySqlConnection =
      new SqlConnection(connectionString);

    // open the database connection using the
    // Open() method of the SqlConnection object
    mySqlConnection.Open();

    // display the properties of the SqlConnection object
    Console.WriteLine("mySqlConnection.ConnectionString = " +
      mySqlConnection.ConnectionString);
    Console.WriteLine("mySqlConnection.ConnectionTimeout = " +
      mySqlConnection.ConnectionTimeout);
    Console.WriteLine("mySqlConnection.Database = " +
      mySqlConnection.Database);
    Console.WriteLine("mySqlConnection.DataSource = " +
      mySqlConnection.DataSource);
    Console.WriteLine("mySqlConnection.PacketSize = " +
      mySqlConnection.PacketSize);
    Console.WriteLine("mySqlConnection.ServerVersion = " +
      mySqlConnection.ServerVersion);
    Console.WriteLine("mySqlConnection.State = " +
      mySqlConnection.State);
    Console.WriteLine("mySqlConnection.WorkstationId = " +
      mySqlConnection.WorkstationId);

    // close the database connection using the Close() method
    // of the SqlConnection object
    mySqlConnection.Close();
  }
}