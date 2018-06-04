/*
  UsingParameters.cs illustrates how to run an INSERT
  statement that uses parameters
*/

using System;
using System.Data;
using System.Data.SqlClient;

class UsingParameters
{

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    mySqlConnection.Open();

    // step 1: create a Command object containing a SQL statement
    // with parameter placeholders
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
    mySqlCommand.CommandText =
      "INSERT INTO Customers (" +
      "  CustomerID, CompanyName, ContactName" +
      ") VALUES (" +
      "  @CustomerID, @CompanyName, @ContactName" +
      ")";

    // step 2: add parameters to the Command object
    mySqlCommand.Parameters.Add("@CustomerID", SqlDbType.NChar, 5);
    mySqlCommand.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 40);
    mySqlCommand.Parameters.Add("@ContactName", SqlDbType.NVarChar, 30);

    // step 3: set the parameters to specified values
    mySqlCommand.Parameters["@CustomerID"].Value = "J4COM";
    mySqlCommand.Parameters["@CompanyName"].Value = "J4 Company";
    mySqlCommand.Parameters["@ContactName"].IsNullable = true;
    mySqlCommand.Parameters["@ContactName"].Value = DBNull.Value;

    // step 4: execute the command
    mySqlCommand.ExecuteNonQuery();
    Console.WriteLine("Successfully added row to Customers table");

    mySqlConnection.Close();

  }

}