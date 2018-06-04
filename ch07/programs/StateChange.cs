/*
  StateChange.cs illustrates how to use the StateChange event
*/

using System;
using System.Data;
using System.Data.SqlClient;

class StateChange
{
  // define the StateChangeHandler() method to handle the
  // StateChange event
  public static void StateChangeHandler(
    object mySender, StateChangeEventArgs myEvent
  )
  {
    Console.WriteLine(
      "mySqlConnection State has changed from " +
      myEvent.OriginalState + " to " +
      myEvent.CurrentState
    );
  }

  public static void Main()
  {
    // create a SqlConnection object
    SqlConnection mySqlConnection =
      new SqlConnection("server=localhost;database=Northwind;uid=sa;pwd=sa");

    // monitor the StateChange event using the StateChangeHandler() method
    mySqlConnection.StateChange +=
      new StateChangeEventHandler(StateChangeHandler);

    // open mySqlConnection, causing the State to change from Closed
    // to Open
    Console.WriteLine("Calling mySqlConnection.Open()");
    mySqlConnection.Open();

    // close mySqlConnection, causing the State to change from Open
    // to Closed
    Console.WriteLine("Calling mySqlConnection.Close()");
    mySqlConnection.Close();
  }
}