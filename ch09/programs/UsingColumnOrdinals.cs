/*
  UsingColumnOrdinals.cs illustrates how to use the GetOrdinal()
  method of a DataReader object to get the numeric positions of
  a column
*/

using System;
using System.Data;
using System.Data.SqlClient;

class UsingColumnOrdinals
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

    // use the GetOrdinal() method of the DataReader object
    // to get the numeric positions of the columns
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

    while (productsSqlDataReader.Read())
    {
      Console.WriteLine("ProductID = " +
        productsSqlDataReader[productIDColPos]);
      Console.WriteLine("ProductName = " +
        productsSqlDataReader[productNameColPos]);
      Console.WriteLine("UnitPrice = " +
        productsSqlDataReader[unitPriceColPos]);
      Console.WriteLine("UnitsInStock = " +
        productsSqlDataReader[unitsInStockColPos]);
      Console.WriteLine("Discontinued = " +
        productsSqlDataReader[discontinuedColPos]);
    }

    productsSqlDataReader.Close();
    mySqlConnection.Close();

  }

}