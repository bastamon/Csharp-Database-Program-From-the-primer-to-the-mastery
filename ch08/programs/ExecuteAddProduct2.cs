/*
  ExecuteAddProduct2.cs illustrates how to call the SQL Server
  AddProduct2() stored procedure
*/

using System;
using System.Data;
using System.Data.SqlClient;

class ExecuteAddProduct2
{

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );
    mySqlConnection.Open();

    // step 1: create a Command object and set its CommandText
    // property to an EXECUTE statement containing the stored
    // procedure call
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "EXECUTE @MyProductID = AddProduct2 @MyProductName, " +
      "@MySupplierID, @MyCategoryID, @MyQuantityPerUnit, " +
      "@MyUnitPrice, @MyUnitsInStock, @MyUnitsOnOrder, " +
      "@MyReorderLevel, @MyDiscontinued";

    // step 2: add the required parameters to the Command object
    mySqlCommand.Parameters.Add("@MyProductID", SqlDbType.Int);
    mySqlCommand.Parameters["@MyProductID"].Direction =
      ParameterDirection.Output;
    mySqlCommand.Parameters.Add(
      "@MyProductName", SqlDbType.NVarChar, 40).Value = "Widget";
    mySqlCommand.Parameters.Add(
      "@MySupplierID", SqlDbType.Int).Value = 1;
    mySqlCommand.Parameters.Add(
      "@MyCategoryID", SqlDbType.Int).Value = 1;
    mySqlCommand.Parameters.Add(
      "@MyQuantityPerUnit", SqlDbType.NVarChar, 20).Value = "1 per box";
    mySqlCommand.Parameters.Add(
      "@MyUnitPrice", SqlDbType.Money).Value = 5.99;
    mySqlCommand.Parameters.Add(
      "@MyUnitsInStock", SqlDbType.SmallInt).Value = 10;
    mySqlCommand.Parameters.Add(
      "@MyUnitsOnOrder", SqlDbType.SmallInt).Value = 5;
    mySqlCommand.Parameters.Add(
      "@MyReorderLevel", SqlDbType.SmallInt).Value = 5;
    mySqlCommand.Parameters.Add(
      "@MyDiscontinued", SqlDbType.Bit).Value = 1;

    // step 3: execute the Command object using the
    // ExecuteNonQuery() method
    mySqlCommand.ExecuteNonQuery();

    // step 4: read the value of the output parameter
    Console.WriteLine("New ProductID = " +
      mySqlCommand.Parameters["@MyProductID"].Value);

    mySqlConnection.Close();

  }

}