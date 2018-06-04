/*
  AddModifyAndRemoveDataRows.cs illustrates how to add, modify,
  and remove DataRow objects from a DataTable and then push
  those changes to the database
*/

using System;
using System.Data;
using System.Data.SqlClient;

class AddModifyAndRemoveDataRows
{

  public static void DisplayDataRow(
    DataRow myDataRow,
    DataTable myDataTable)
  {
    Console.WriteLine("\nIn DisplayDataRow()");
    foreach (DataColumn myDataColumn in myDataTable.Columns)
    {
      Console.WriteLine(myDataColumn + " = " +
        myDataRow[myDataColumn]);
    }
  }

  public static void AddDataRow(
    DataTable myDataTable,
    SqlDataAdapter mySqlDataAdapter,
    SqlConnection mySqlConnection
  )
  {
    Console.WriteLine("\nIn AddDataRow()");

    // step 1: use the NewRow() method of the DataTable to
    // create a new DataRow
    Console.WriteLine("Calling myDataTable.NewRow()");
    DataRow myNewDataRow = myDataTable.NewRow();
    Console.WriteLine("myNewDataRow.RowState = " +
      myNewDataRow.RowState);

    // step 2: set the values for the DataColumn objects of
    // the new DataRow
    myNewDataRow["CustomerID"] = "J5COM";
    myNewDataRow["CompanyName"] = "J5 Company";
    myNewDataRow["Address"] = "1 Main Street";

    // step 3: use the Add() method through the Rows property
    // to add the new DataRow to the DataTable
    Console.WriteLine("Calling myDataTable.Rows.Add()");
    myDataTable.Rows.Add(myNewDataRow);
    Console.WriteLine("myNewDataRow.RowState = " +
      myNewDataRow.RowState);

    // step 4: use the Update() method to push the new
    // row to the database
    Console.WriteLine("Calling mySqlDataAdapter.Update()");
    mySqlConnection.Open();
    int numOfRows = mySqlDataAdapter.Update(myDataTable);
    mySqlConnection.Close();
    Console.WriteLine("numOfRows = " + numOfRows);
    Console.WriteLine("myNewDataRow.RowState = " +
      myNewDataRow.RowState);

    DisplayDataRow(myNewDataRow, myDataTable);
  }

  public static void ModifyDataRow(
    DataTable myDataTable,
    SqlDataAdapter mySqlDataAdapter,
    SqlConnection mySqlConnection
  )
  {
    Console.WriteLine("\nIn ModifyDataRow()");

    // step 1: set the PrimaryKey property of the DataTable
    myDataTable.PrimaryKey =
      new DataColumn[]
      {
        myDataTable.Columns["CustomerID"]
      };

    // step 2: use the Find() method to locate the DataRow
    // in the DataTable using the primary key value
    DataRow myEditDataRow = myDataTable.Rows.Find("J5COM");

    // step 3: change the DataColumn values of the DataRow
    myEditDataRow["CompanyName"] = "Widgets Inc.";
    myEditDataRow["Address"] = "1 Any Street";
    Console.WriteLine("myEditDataRow.RowState = " +
      myEditDataRow.RowState);
    Console.WriteLine("myEditDataRow[\"CustomerID\", " +
      "DataRowVersion.Original] = " +
      myEditDataRow["CustomerID", DataRowVersion.Original]);
    Console.WriteLine("myEditDataRow[\"CompanyName\", " +
      "DataRowVersion.Original] = " +
      myEditDataRow["CompanyName", DataRowVersion.Original]);
    Console.WriteLine("myEditDataRow[\"Address\", " +
      "DataRowVersion.Original] = " +
      myEditDataRow["Address", DataRowVersion.Original]);
    Console.WriteLine("myEditDataRow[\"CompanyName\", " +
      "DataRowVersion.Current] = " +
      myEditDataRow["CompanyName", DataRowVersion.Current]);
    Console.WriteLine("myEditDataRow[\"Address\", " +
      "DataRowVersion.Current] = " +
      myEditDataRow["Address", DataRowVersion.Current]);

    // step 4: use the Update() method to push the modified
    // row to the database
    Console.WriteLine("Calling mySqlDataAdapter.Update()");
    mySqlConnection.Open();
    int numOfRows = mySqlDataAdapter.Update(myDataTable);
    mySqlConnection.Close();
    Console.WriteLine("numOfRows = " + numOfRows);
    Console.WriteLine("myEditDataRow.RowState = " +
      myEditDataRow.RowState);

    DisplayDataRow(myEditDataRow, myDataTable);
  }

  public static void RemoveDataRow(
    DataTable myDataTable,
    SqlDataAdapter mySqlDataAdapter,
    SqlConnection mySqlConnection
  )
  {
    Console.WriteLine("\nIn RemoveDataRow()");

    // step 1: set the PrimaryKey property of the DataTable
    myDataTable.PrimaryKey =
      new DataColumn[]
      {
        myDataTable.Columns["CustomerID"]
      };

    // step 2: use the Find() method to locate the DataRow
    DataRow myRemoveDataRow = myDataTable.Rows.Find("J5COM");

    // step 3: use the Delete() method to remove the DataRow
    Console.WriteLine("Calling myRemoveDataRow.Delete()");
    myRemoveDataRow.Delete();
    Console.WriteLine("myRemoveDataRow.RowState = " +
      myRemoveDataRow.RowState);

    // step 4: use the Update() method to remove the deleted
    // row from the database
    Console.WriteLine("Calling mySqlDataAdapter.Update()");
    mySqlConnection.Open();
    int numOfRows = mySqlDataAdapter.Update(myDataTable);
    mySqlConnection.Close();
    Console.WriteLine("numOfRows = " + numOfRows);
    Console.WriteLine("myRemoveDataRow.RowState = " +
      myRemoveDataRow.RowState);
  }

  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    // create a SqlCommand object to hold the SELECT
    SqlCommand mySelectCommand = mySqlConnection.CreateCommand();
    mySelectCommand.CommandText =
      "SELECT CustomerID, CompanyName, Address " +
      "FROM Customers " +
      "ORDER BY CustomerID";

    // create a SqlCommand object to hold the INSERT
    SqlCommand myInsertCommand = mySqlConnection.CreateCommand();
    myInsertCommand.CommandText =
      "INSERT INTO Customers (" +
      "  CustomerID, CompanyName, Address" +
      ") VALUES (" +
      "  @CustomerID, @CompanyName, @Address" +
      ")";
    myInsertCommand.Parameters.Add("@CustomerID", SqlDbType.NChar,
      5, "CustomerID");
    myInsertCommand.Parameters.Add("@CompanyName", SqlDbType.NVarChar,
      40, "CompanyName");
    myInsertCommand.Parameters.Add("@Address", SqlDbType.NVarChar,
      60, "Address");

    // create a SqlCommand object to hold the UPDATE
    SqlCommand myUpdateCommand = mySqlConnection.CreateCommand();
    myUpdateCommand.CommandText =
      "UPDATE Customers " +
      "SET " +
      "  CompanyName = @NewCompanyName, " +
      "  Address = @NewAddress " +
      "WHERE CustomerID = @OldCustomerID " +
      "AND CompanyName = @OldCompanyName " +
      "AND Address = @OldAddress";
    myUpdateCommand.Parameters.Add("@NewCompanyName", SqlDbType.NVarChar,
      40, "CompanyName");
    myUpdateCommand.Parameters.Add("@NewAddress", SqlDbType.NVarChar,
      60, "Address");
    myUpdateCommand.Parameters.Add("@OldCustomerID", SqlDbType.NChar,
      5, "CustomerID");
    myUpdateCommand.Parameters.Add("@OldCompanyName", SqlDbType.NVarChar,
      40, "CompanyName");
    myUpdateCommand.Parameters.Add("@OldAddress", SqlDbType.NVarChar,
      60, "Address");
    myUpdateCommand.Parameters["@OldCustomerID"].SourceVersion =
      DataRowVersion.Original;
    myUpdateCommand.Parameters["@OldCompanyName"].SourceVersion =
      DataRowVersion.Original;
    myUpdateCommand.Parameters["@OldAddress"].SourceVersion =
      DataRowVersion.Original;

    // create a SqlCommand object to hold the DELETE
    SqlCommand myDeleteCommand = mySqlConnection.CreateCommand();
    myDeleteCommand.CommandText =
      "DELETE FROM Customers " +
      "WHERE CustomerID = @OldCustomerID " +
      "AND CompanyName = @OldCompanyName " +
      "AND Address = @OldAddress";
    myDeleteCommand.Parameters.Add("@OldCustomerID", SqlDbType.NChar,
      5, "CustomerID");
    myDeleteCommand.Parameters.Add("@OldCompanyName", SqlDbType.NVarChar,
      40, "CompanyName");
    myDeleteCommand.Parameters.Add("@OldAddress", SqlDbType.NVarChar,
      60, "Address");
    myDeleteCommand.Parameters["@OldCustomerID"].SourceVersion =
      DataRowVersion.Original;
    myDeleteCommand.Parameters["@OldCompanyName"].SourceVersion =
      DataRowVersion.Original;
    myDeleteCommand.Parameters["@OldAddress"].SourceVersion =
      DataRowVersion.Original;

    // create a SqlDataAdapter and set its properties
    // to the SqlCommand objects previously created
    SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
    mySqlDataAdapter.SelectCommand = mySelectCommand;
    mySqlDataAdapter.InsertCommand = myInsertCommand;
    mySqlDataAdapter.UpdateCommand = myUpdateCommand;
    mySqlDataAdapter.DeleteCommand = myDeleteCommand;

    // create a DataSet and fill it using the Fill() method
    DataSet myDataSet = new DataSet();
    Console.WriteLine("Calling mySqlDataAdapter.Fill()");
    mySqlConnection.Open();
    int numOfRows =
      mySqlDataAdapter.Fill(myDataSet, "Customers");
    mySqlConnection.Close();
    Console.WriteLine("numOfRows = " + numOfRows);

    // read the Customers DataTable from myDataSet
    DataTable customersDataTable = myDataSet.Tables["Customers"];

    // add a new row to customersDataTable
    AddDataRow(customersDataTable, mySqlDataAdapter,
      mySqlConnection);

    // modify the new row in customersDataTable
    ModifyDataRow(customersDataTable, mySqlDataAdapter,
      mySqlConnection);

    // remove the new row from customersDataTable
    RemoveDataRow(customersDataTable, mySqlDataAdapter,
      mySqlConnection);
  }
}