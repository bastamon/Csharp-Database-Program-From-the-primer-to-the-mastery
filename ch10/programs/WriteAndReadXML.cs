/*
  WriteAndReadXml.cs illustrates how to write and read XML files
*/

using System;
using System.Data;
using System.Data.SqlClient;

class WriteAndReadXML
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT TOP 2 CustomerID, CompanyName, ContactName, Address " +
      "FROM Customers " +
      "ORDER BY CustomerID";
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    Console.WriteLine("Retrieving rows from the Customers table");
    mySqlDataAdapter.Fill(myDataSet, "Customers");
    mySqlConnection.Close();

    // use the WriteXml() method to write the DataSet out to an
    // XML file
    Console.WriteLine("Writing rows out to an XML file named " +
      "myXmlFile.xml using the WriteXml() method");
    myDataSet.WriteXml("myXmlFile.xml");

    Console.WriteLine("Writing schema out to an XML file named " +
      "myXmlFile2.xml using the WriteXml() method");
    myDataSet.WriteXml("myXmlFile2.xml", XmlWriteMode.WriteSchema);

    // use the WriteXmlSchema() method to write the schema of the
    // DataSet out to an XML file
    Console.WriteLine("Writing schema out to an XML file named " +
      "myXmlSchemaFile.xml using the WriteXmlSchema() method");
    myDataSet.WriteXmlSchema("myXmlSchemaFile.xml");

    // use the Clear() method to clear the current rows in the DataSet
    myDataSet.Clear();

    // use the ReadXml() method to read the contents of the XML file
    // into the DataSet
    Console.WriteLine("Reading rows from myXmlFile.xml " +
      "using the ReadXml() method");
    myDataSet.ReadXml("myXmlFile.xml");

    DataTable myDataTable = myDataSet.Tables["Customers"];
    foreach (DataRow myDataRow in myDataTable.Rows)
    {
      Console.WriteLine("CustomerID = " + myDataRow["CustomerID"]);
      Console.WriteLine("CompanyName = " + myDataRow["CompanyName"]);
      Console.WriteLine("ContactName = " + myDataRow["ContactName"]);
      Console.WriteLine("Address = " + myDataRow["Address"]);
    }
  }
}