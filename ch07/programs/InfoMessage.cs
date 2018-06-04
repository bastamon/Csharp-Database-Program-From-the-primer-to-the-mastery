/*
  InfoMessage.cs illustrates how to use the InfoMessage event
*/

using System;
using System.Data;
using System.Data.SqlClient;

class InfoMessage
{
  // define the InfoMessageHandler() method to handle the
  // InfoMessage event
  public static void InfoMessageHandler(
    object mySender, SqlInfoMessageEventArgs myEvent
  )
  {
    Console.WriteLine(
      "The following message was produced:\n" +
      myEvent.Errors[0]
    );
  }

  public static void Main()
  {
    // create a SqlConnection object
    SqlConnection mySqlConnection =
      new SqlConnection("server=localhost;database=Northwind;uid=sa;pwd=sa");

    // monitor the InfoMessage event using the InfoMessageHandler() method
    mySqlConnection.InfoMessage +=
      new SqlInfoMessageEventHandler(InfoMessageHandler);

    // open mySqlConnection
    mySqlConnection.Open();

    // create a SqlCommand object
    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

    // run a PRINT statement
    mySqlCommand.CommandText =
      "PRINT 'This is the message from the PRINT statement'";
    mySqlCommand.ExecuteNonQuery();

    // run a RAISERROR statement
    mySqlCommand.CommandText =
      "RAISERROR('This is the message from the RAISERROR statement', 10, 1)";
    mySqlCommand.ExecuteNonQuery();

    // close mySqlConnection
    mySqlConnection.Close();
  }
}