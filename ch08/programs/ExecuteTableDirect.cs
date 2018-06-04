/*
  ExecuteTableDirect.cs illustrates how to execute a
  TableDirect command
*/

using System;
using System.Data;
using System.Data.OleDb;

class ExecuteTableDirect
{

  public static void Main()
  {

    OleDbConnection myOleDbConnection =
      new OleDbConnection(
        "Provider=SQLOLEDB;server=localhost;database=Northwind;" +
        "uid=sa;pwd=sa"
      );
    OleDbCommand myOleDbCommand = myOleDbConnection.CreateCommand();

    // set the CommandType property of the OleDbCommand object to
    // TableDirect
    myOleDbCommand.CommandType = CommandType.TableDirect;

    // set the CommandText property of the OleDbCommand object to
    // the name of the table to retrieve from
    myOleDbCommand.CommandText = "Products";

    myOleDbConnection.Open();

    OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();

    // only read the first 5 rows from the OleDbDataReader object
    for (int count = 1; count <= 5; count++)
    {
      myOleDbDataReader.Read();
      Console.WriteLine("myOleDbDataReader[\"ProductID\"] = " +
        myOleDbDataReader["ProductID"]);
      Console.WriteLine("myOleDbDataReader[\"ProductName\"] = " +
        myOleDbDataReader["ProductName"]);
      Console.WriteLine("myOleDbDataReader[\"QuantityPerUnit\"] = " +
        myOleDbDataReader["QuantityPerUnit"]);
      Console.WriteLine("myOleDbDataReader[\"UnitPrice\"] = " +
        myOleDbDataReader["UnitPrice"]);
    }

    myOleDbDataReader.Close();
    myOleDbConnection.Close();

  }

}