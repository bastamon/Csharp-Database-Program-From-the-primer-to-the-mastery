/*
  StronglyTypedColumnValuesSql.cs illustrates how to read
  column values as Sql* types using the GetSql* methods
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

class StronglyTypedColumnValuesSql
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

    // read the column values using GetSql* methods that
    // return specific Sql* types
    while (productsSqlDataReader.Read())
    {
      SqlInt32 productID =
        productsSqlDataReader.GetSqlInt32(productIDColPos);
      Console.WriteLine("productID = " + productID);

      SqlString productName =
        productsSqlDataReader.GetSqlString(productNameColPos);
      Console.WriteLine("productName = " + productName);

      SqlMoney unitPrice =
        productsSqlDataReader.GetSqlMoney(unitPriceColPos);
      Console.WriteLine("unitPrice = " + unitPrice);

      SqlInt16 unitsInStock =
        productsSqlDataReader.GetSqlInt16(unitsInStockColPos);
      Console.WriteLine("unitsInStock = " + unitsInStock);

      SqlBoolean discontinued =
        productsSqlDataReader.GetSqlBoolean(discontinuedColPos);
      Console.WriteLine("discontinued = " + discontinued);
    }

    productsSqlDataReader.Close();
    mySqlConnection.Close();

  }

}