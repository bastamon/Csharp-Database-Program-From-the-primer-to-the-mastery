/*
  UsingXmlDocument.cs illustrates the use of an XmlDocument object
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

class UsingXmlDocument
{
  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "SELECT TOP 2 CustomerID, CompanyName, Country " +
      "FROM Customers " +
      "ORDER BY CustomerID";
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySqlCommand;

    // step 1: create a DataSet object and fill it with the top 2 rows
    // from the Customers table
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    mySqlDataAdapter.Fill(myDataSet, "Customers");
    mySqlConnection.Close();

    // step 2: create an XmlDocument object and load it with the XML from
    // the DataSet; the GetXml() method returns the rows in
    // myDataSet as a string containing a complete XML document; and
    // the LoadXml() method loads myXmlDocument with the XML document
    // string returned by GetXml()
    XmlDocument myXmlDocument = new XmlDocument();
    myXmlDocument.LoadXml(myDataSet.GetXml());

    // step 3: display the XML in myXmlDocument using the Save() method
    Console.WriteLine("Contents of myXmlDocument:");
    myXmlDocument.Save(Console.Out);

    // step 4: retrieve the XmlNode objects in myXmlDocument using the
    // SelectNodes() method; you pass an XPath expression to SelectNodes()
    Console.WriteLine("\n\nCustomers:");
    foreach (XmlNode myXmlNode in
      myXmlDocument.SelectNodes("/NewDataSet/Customers"))
    {
      Console.WriteLine("CustomerID = " +
        myXmlNode.ChildNodes[0].InnerText);
      Console.WriteLine("CompanyName = " +
        myXmlNode.ChildNodes[1].InnerText);
      Console.WriteLine("Country = " +
        myXmlNode.ChildNodes[2].InnerText);
    }

    // step 5: retrieve the XmlNode for the ANATR customer using
    // the SelectSingleNode() method; you pass an XPath
    // expression to SelectSingleNode
    Console.WriteLine("\nRetrieving node with CustomerID of ANATR");
    XmlNode myXmlNode2 =
      myXmlDocument.SelectSingleNode(
        "/NewDataSet/Customers[CustomerID=\"ANATR\"]"
      );
    Console.WriteLine("CustomerID = " +
      myXmlNode2.ChildNodes[0].InnerText);
    Console.WriteLine("CompanyName = " +
      myXmlNode2.ChildNodes[1].InnerText);
    Console.WriteLine("Country = " +
      myXmlNode2.ChildNodes[2].InnerText);
  }
}