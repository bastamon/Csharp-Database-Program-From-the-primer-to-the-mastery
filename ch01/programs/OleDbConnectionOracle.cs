/*
  OleDbConnectionOracle.cs illustrates how to use an
  OleDbConnection object to connect to an Oracle database
*/

using System;
using System.Data;
using System.Data.OleDb;

class OleDbConnectionOracle
{
  public static void Main()
  {
    // formulate a string containing the details of the
    // database connection
    string connectionString =
      "provider=MSDAORA;data source=ORCL;user id=SCOTT;password=TIGER";

    // create an OleDbConnection object to connect to the
    // database, passing the connection string to the constructor
    OleDbConnection myOleDbConnection =
      new OleDbConnection(connectionString);

    // create an OleDbCommand object
    OleDbCommand myOleDbCommand = myOleDbConnection.CreateCommand();

    // set the CommandText property of the OleDbCommand object to
    // a SQL SELECT statement that retrieves a row from the emp table
    myOleDbCommand.CommandText =
      "SELECT empno, ename, sal " +
      "FROM emp " +
      "WHERE empno = 7369";

    // open the database connection using the
    // Open() method of the SqlConnection object
    myOleDbConnection.Open();

    // create an OleDbDataReader object and call the ExecuteReader()
    // method of the OleDbCommand object to run the SELECT statement
    OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();

    // read the row from the OleDbDataReader object using
    // the Read() method
    myOleDbDataReader.Read();

    // display the column values
    Console.WriteLine("myOleDbDataReader[\"empno\"] = " +
      myOleDbDataReader["empno"]);
    Console.WriteLine("myOleDbDataReader[\"ename\"] = " +
      myOleDbDataReader["ename"]);
    Console.WriteLine("myOleDbDataReader[\"sal\"] = " +
      myOleDbDataReader["sal"]);

    // close the OleDbDataReader object using the Close() method
    myOleDbDataReader.Close();

    // close the OleDbConnection object using the Close() method
    myOleDbConnection.Close();
  }
}