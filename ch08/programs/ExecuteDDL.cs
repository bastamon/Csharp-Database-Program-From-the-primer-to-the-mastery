/*
  ExecuteDDL.cs illustrates how to use the ExecuteNonQuery()
  method to run DDL statements
*/

using System;
using System.Data;
using System.Data.SqlClient;

class ExecuteDDL
{

  public static void Main()
  {

    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

    // set the CommandText property of the SqlCommand object to
    // a CREATE TABLE statement
    mySqlCommand.CommandText =
      "CREATE TABLE MyPersons (" +
      "  PersonID int CONSTRAINT PK_Persons PRIMARY KEY," +
      "  FirstName nvarchar(15) NOT NULL," +
      "  LastName nvarchar(15) NOT NULL," +
      "  DateOfBirth datetime" +
      ")";

    mySqlConnection.Open();

    // call the ExecuteNonQuery() method of the SqlCommand object
    // to run the CREATE TABLE statement
    Console.WriteLine("Creating MyPersons table");
    int result = mySqlCommand.ExecuteNonQuery();
    Console.WriteLine("mySqlCommand.ExecuteNonQuery() = " + result);

    // set the CommandText property of the SqlCommand object to
    // an ALTER TABLE statement
    mySqlCommand.CommandText =
      "ALTER TABLE MyPersons " +
      "ADD EmployerID nchar(5) CONSTRAINT FK_Persons_Customers " +
      "REFERENCES Customers(CustomerID)";

    // call the ExecuteNonQuery() method of the SqlCommand object
    // to run the ALTER TABLE statement
    Console.WriteLine("Altering MyPersons table");
    result = mySqlCommand.ExecuteNonQuery();
    Console.WriteLine("mySqlCommand.ExecuteNonQuery() = " + result);

    // set the CommandText property of the SqlCommand object to
    // a DROP TABLE statement
    mySqlCommand.CommandText = "DROP TABLE MyPersons";

    // call the ExecuteNonQuery() method of the SqlCommand object
    // to run the DROP TABLE statement
    Console.WriteLine("Dropping MyPersons table");
    result = mySqlCommand.ExecuteNonQuery();
    Console.WriteLine("mySqlCommand.ExecuteNonQuery() = " + result);

    mySqlConnection.Close();

  }

}