/*
  OleDbConnectionAccess.cs illustrates how to use an
  OleDbConnection object to connect to an Access database
*/

using System;
using System.Data;
using System.Data.OleDb;

class OleDbConnectionAccess
{
  public static void Main()
  {
    // formulate a string containing the details of the
    // database connection
    string connectionString =
      "provider=Microsoft.Jet.OLEDB.4.0;" +
      "data source=F:\\Program Files\\Microsoft Office\\Office\\Samples\\Northwind.mdb";

    // create an OleDbConnection object to connect to the
    // database, passing the connection string to the constructor
    OleDbConnection myOleDbConnection =
      new OleDbConnection(connectionString);

    // create an OleDbCommand object
    OleDbCommand myOleDbCommand = myOleDbConnection.CreateCommand();

    // set the CommandText property of the OleDbCommand object to
    // a SQL SELECT statement that retrieves a row from the Customers table
    myOleDbCommand.CommandText =
      "SELECT CustomerID, CompanyName, ContactName, Address " +
      "FROM Customers " +
      "WHERE CustomerID = 'ALFKI'";

    // open the database connection using the
    // Open() method of the OleDbConnection object
    myOleDbConnection.Open();

    // create an OleDbDataReader object and call the ExecuteReader()
    // method of the OleDbCommand object to run the SELECT statement
    OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();

    // read the row from the OleDbDataReader object using
    // the Read() method
    myOleDbDataReader.Read();

    // display the column values
    Console.WriteLine("myOleDbDataReader[\"CustomerID\"] = " +
      myOleDbDataReader["CustomerID"]);
    Console.WriteLine("myOleDbDataReader[\"CompanyName\"] = " +
      myOleDbDataReader["CompanyName"]);
    Console.WriteLine("myOleDbDataReader[\"ContactName\"] = " +
      myOleDbDataReader["ContactName"]);
    Console.WriteLine("myOleDbDataReader[\"Address\"] = " +
      myOleDbDataReader["Address"]);

    // close the OleDbDataReader object using the Close() method
    myOleDbDataReader.Close();

    // close the OleDbConnection object using the Close() method
    myOleDbConnection.Close();
  }
}