/*
  ExecuteXmlReader.cs illustrates how to use the ExecuteXmlReader()
  method to run a SELECT statement that returns XML
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

class ExecuteXmlReader
{

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

    // set the CommandText property of the SqlCommand object to
    // a SELECT statement that retrieves XML
    mySqlCommand.CommandText =
      "SELECT TOP 5 ProductID, ProductName, UnitPrice " +
      "FROM Products " +
      "ORDER BY ProductID " +
      "FOR XML AUTO";

    mySqlConnection.Open();

    // create a SqlDataReader object and call the ExecuteReader()
    // method of the SqlCommand object to run the SELECT statement
    XmlReader myXmlReader = mySqlCommand.ExecuteXmlReader();

    // read the rows from the XmlReader object using the Read() method
    myXmlReader.Read();
    while (!myXmlReader.EOF)
    {
      Console.WriteLine(myXmlReader.ReadOuterXml());
    }

    myXmlReader.Close();
    mySqlConnection.Close();

  }

}