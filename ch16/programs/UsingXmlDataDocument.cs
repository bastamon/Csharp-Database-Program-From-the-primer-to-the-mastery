/*
  UsingXmlDataDocument.cs illustrates how to use an XmlDataDocument
  object
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

class UsingXmlDataDocument
{
  public static void DisplayDataRows(DataTable myDataTable)
  {
    Console.WriteLine("\n\nCustomer DataRow objects in customersDT:");
    foreach (DataRow myDataRow in myDataTable.Rows)
    {
      foreach (DataColumn myDataColumn in myDataTable.Columns)
      {
        Console.WriteLine(myDataColumn + " = " +
          myDataRow[myDataColumn]);
      }
    }
  }

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
    DataTable customersDT = myDataSet.Tables["Customers"];

    // step 2: display the DataRow objects in customersDT using
    // DisplayDataRows()
    DisplayDataRows(customersDT);

    // step 3: create an XmlDataDocument object, passing myDataSet
    // to the constructor; this associates myDataSet with the
    // XmlDataDocument
    XmlDataDocument myXDD = new XmlDataDocument(myDataSet);

    // step 4: display the XML document in myXDD
    Console.WriteLine("\nXML document in myXDD:");
    myXDD.Save(Console.Out);

    // step 5: add a customer DataRow to customersDT with a CustomerID
    // of J9COM
    Console.WriteLine("\n\nAdding new DataRow to customersDT with CustomerID of J9COM");
    DataRow myDataRow = customersDT.NewRow();
    myDataRow["CustomerID"] = "J9COM";
    myDataRow["CompanyName"] = "J9 Company";
    myDataRow["Country"] = "UK";
    customersDT.Rows.Add(myDataRow);

    // step 6: retrieve the J9COM node using GetElementFromRow()
    Console.WriteLine("\nRetrieving J9COM node using GetElementFromRow()");
    XmlNode myXmlNode = myXDD.GetElementFromRow(myDataRow);
    Console.WriteLine("CustomerID = " + myXmlNode.ChildNodes[0].InnerText);
    Console.WriteLine("CompanyName = " + myXmlNode.ChildNodes[1].InnerText);
    Console.WriteLine("Country = " + myXmlNode.ChildNodes[2].InnerText);

    // step 7: set J9COM node's Country to USA, first setting
    // EnforceConstraints to false
    Console.WriteLine("\nSetting J9COM node's Country to USA");
    myDataSet.EnforceConstraints = false;
    myXmlNode.ChildNodes[2].InnerText = "USA";

    // step 8: retrieve the ANATR XmlNode using SelectSingleNode()
    Console.WriteLine("\nRetrieving ANATR node using SelectSingleNode()");
    myXmlNode =
      myXDD.SelectSingleNode(
        "/NewDataSet/Customers[CustomerID=\"ANATR\"]"
      );

    // step 9: retrieve the ANATR DataRow using GetRowFromElement()
    Console.WriteLine("\nRetrieving ANATR DataRow using GetRowFromElement()");
    myDataRow =
      myXDD.GetRowFromElement((XmlElement) myXmlNode);
    foreach (DataColumn myDataColumn in customersDT.Columns)
    {
      Console.WriteLine(myDataColumn + " = " +
        myDataRow[myDataColumn]);
    }

    // step 10: remove the ANATR node using RemoveAll()
    Console.WriteLine("\nRemoving ANATR node");
    myXmlNode.RemoveAll();

    // step 11: display the XML document in myXDD using Save()
    Console.WriteLine("\nXML document in myXDD:");
    myXDD.Save(Console.Out);

    // step 12: display the DataRow objects in customersDT using
    // DisplayDataRows()
    DisplayDataRows(customersDT);
  }
}