/*
  StronglyTypedColumnValues.cs illustrates how to read
  column values as C# types using the Get* methods
*/

using System;
using System.Data;
using System.Data.SqlClient;

class StronglyTypedColumnValues
{

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

    mySqlCommand.CommandText =
      "SELECT TOP 5 ProductID, ProductName, UnitPrice, " +
      "UnitsInStock, Discontinued " +
      "FROM Products " +
      "ORDER BY ProductID";

    mySqlConnection.Open();

    SqlDataReader productsSqlDataReader =
      mySqlCommand.ExecuteReader();

    int productIDColPos =
      productsSqlDataReader.GetOrdinal("ProductID");
    int productNameColPos =
      productsSqlDataReader.GetOrdinal("ProductName");
    int unitPriceColPos =
      productsSqlDataReader.GetOrdinal("UnitPrice");
    int unitsInStockColPos =
      productsSqlDataReader.GetOrdinal("UnitsInStock");
    int discontinuedColPos =
      productsSqlDataReader.GetOrdinal("Discontinued");

    // use the GetFieldType() method of the DataReader object
    // to obtain the .NET type of a column
    Console.WriteLine("ProductID .NET type = " +
      productsSqlDataReader.GetFieldType(productIDColPos));
    Console.WriteLine("ProductName .NET type = " +
      productsSqlDataReader.GetFieldType(productNameColPos));
    Console.WriteLine("UnitPrice .NET type = " +
      productsSqlDataReader.GetFieldType(unitPriceColPos));
    Console.WriteLine("UnitsInStock .NET type = " +
      productsSqlDataReader.GetFieldType(unitsInStockColPos));
    Console.WriteLine("Discontinued .NET type = " +
      productsSqlDataReader.GetFieldType(discontinuedColPos));

    // use the GetDataTypeName() method of the DataReader object
    // to obtain the database type of a column
    Console.WriteLine("ProductID database type = " +
      productsSqlDataReader.GetDataTypeName(productIDColPos));
    Console.WriteLine("ProductName database type = " +
      productsSqlDataReader.GetDataTypeName(productNameColPos));
    Console.WriteLine("UnitPrice database type = " +
      productsSqlDataReader.GetDataTypeName(unitPriceColPos));
    Console.WriteLine("UnitsInStock database type = " +
      productsSqlDataReader.GetDataTypeName(unitsInStockColPos));
    Console.WriteLine("Discontinued database type = " +
      productsSqlDataReader.GetDataTypeName(discontinuedColPos));

    // read the column values using Get* methods that
    // return specific C# types
    while (productsSqlDataReader.Read())
    {
      int productID =
        productsSqlDataReader.GetInt32(productIDColPos);
      Console.WriteLine("productID = " + productID);

      string productName =
        productsSqlDataReader.GetString(productNameColPos);
      Console.WriteLine("productName = " + productName);

      decimal unitPrice =
        productsSqlDataReader.GetDecimal(unitPriceColPos);
      Console.WriteLine("unitPrice = " + unitPrice);

      short unitsInStock =
        productsSqlDataReader.GetInt16(unitsInStockColPos);
      Console.WriteLine("unitsInStock = " + unitsInStock);

      bool discontinued =
        productsSqlDataReader.GetBoolean(discontinuedColPos);
      Console.WriteLine("discontinued = " + discontinued);
    }

    productsSqlDataReader.Close();
    mySqlConnection.Close();

  }

}