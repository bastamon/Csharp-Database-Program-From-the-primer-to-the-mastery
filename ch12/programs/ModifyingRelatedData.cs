/*
  ModifyingRelatedData.cs illustrates how to add, update,
  and delete DataRow objects in related parent and child DataTable
  objects, and then push those changes to the database
*/

using System;
using System.Data;
using System.Data.SqlClient;

class ModifyingRelatedData
{
  public static void SetupCustomersDA(
    SqlDataAdapter customersDA,
    SqlConnection mySqlConnection
  )
  {
    Console.WriteLine("\nIn SetupCustomersDA()");

    // create a SqlCommand object to hold the SELECT
    SqlCommand customersSelectCommand = mySqlConnection.CreateCommand();
    customersSelectCommand.CommandText =
      "SELECT CustomerID, CompanyName " +
      "FROM Customers";

    // create a SqlCommand object to hold the INSERT
    SqlCommand customersInsertCommand = mySqlConnection.CreateCommand();
    customersInsertCommand.CommandText =
      "INSERT INTO Customers (" +
      "  CustomerID, CompanyName " +
      ") VALUES (" +
      "  @CustomerID, @CompanyName" +
      ")";
    customersInsertCommand.Parameters.Add("@CustomerID", SqlDbType.NChar,
      5, "CustomerID");
    customersInsertCommand.Parameters.Add("@CompanyName", SqlDbType.NVarChar,
      40, "CompanyName");

    // create a SqlCommand object to hold the UPDATE
    SqlCommand customersUpdateCommand = mySqlConnection.CreateCommand();
    customersUpdateCommand.CommandText =
      "UPDATE Customers " +
      "SET " +
      "  CompanyName = @NewCompanyName " +
      "WHERE CustomerID = @OldCustomerID " +
      "AND CompanyName = @OldCompanyName";
    customersUpdateCommand.Parameters.Add("@NewCompanyName",
      SqlDbType.NVarChar, 40, "CompanyName");
    customersUpdateCommand.Parameters.Add("@OldCustomerID",
      SqlDbType.NChar, 5, "CustomerID");
    customersUpdateCommand.Parameters.Add("@OldCompanyName",
      SqlDbType.NVarChar, 40, "CompanyName");
    customersUpdateCommand.Parameters["@OldCustomerID"].SourceVersion =
      DataRowVersion.Original;
    customersUpdateCommand.Parameters["@OldCompanyName"].SourceVersion =
      DataRowVersion.Original;

    // create a SqlCommand object to hold the DELETE
    SqlCommand customersDeleteCommand = mySqlConnection.CreateCommand();
    customersDeleteCommand.CommandText =
      "DELETE FROM Customers " +
      "WHERE CustomerID = @OldCustomerID " +
      "AND CompanyName = @OldCompanyName";
    customersDeleteCommand.Parameters.Add("@OldCustomerID",
      SqlDbType.NChar, 5, "CustomerID");
    customersDeleteCommand.Parameters.Add("@OldCompanyName",
      SqlDbType.NVarChar, 40, "CompanyName");
    customersDeleteCommand.Parameters["@OldCustomerID"].SourceVersion =
      DataRowVersion.Original;
    customersDeleteCommand.Parameters["@OldCompanyName"].SourceVersion =
      DataRowVersion.Original;

    // set the customersDA properties
    // to the SqlCommand objects previously created
    customersDA.SelectCommand = customersSelectCommand;
    customersDA.InsertCommand = customersInsertCommand;
    customersDA.UpdateCommand = customersUpdateCommand;
    customersDA.DeleteCommand = customersDeleteCommand;
  }

  public static void SetupOrdersDA(
    SqlDataAdapter ordersDA,
    SqlConnection mySqlConnection
  )
  {
    Console.WriteLine("\nIn SetupOrdersDA()");

    // create a SqlCommand object to hold the SELECT
    SqlCommand ordersSelectCommand = mySqlConnection.CreateCommand();
    ordersSelectCommand.CommandText =
      "SELECT OrderID, CustomerID, ShipCountry " +
      "FROM Orders";

    // create a SqlCommand object to hold the INSERT
    SqlCommand ordersInsertCommand = mySqlConnection.CreateCommand();
    ordersInsertCommand.CommandText =
      "INSERT INTO Orders (" +
      "  CustomerID, ShipCountry " +
      ") VALUES (" +
      "  @CustomerID, @ShipCountry" +
      ");" +
      "SELECT @OrderID = SCOPE_IDENTITY();";
    ordersInsertCommand.Parameters.Add("@CustomerID", SqlDbType.NChar,
      5, "CustomerID");
    ordersInsertCommand.Parameters.Add("@ShipCountry", SqlDbType.NVarChar,
      15, "ShipCountry");
    ordersInsertCommand.Parameters.Add("@OrderID", SqlDbType.Int,
      0, "OrderID");
    ordersInsertCommand.Parameters["@OrderID"].Direction =
      ParameterDirection.Output;

    // create a SqlCommand object to hold the UPDATE
    SqlCommand ordersUpdateCommand = mySqlConnection.CreateCommand();
    ordersUpdateCommand.CommandText =
      "UPDATE Orders " +
      "SET " +
      "  ShipCountry = @NewShipCountry " +
      "WHERE OrderID = @OldOrderID " +
      "AND CustomerID = @OldCustomerID " +
      "AND ShipCountry = @OldShipCountry";
    ordersUpdateCommand.Parameters.Add("@NewShipCountry",
      SqlDbType.NVarChar, 15, "ShipCountry");
    ordersUpdateCommand.Parameters.Add("@OldOrderID",
      SqlDbType.Int, 0, "OrderID");
    ordersUpdateCommand.Parameters.Add("@OldCustomerID",
      SqlDbType.NChar, 5, "CustomerID");
    ordersUpdateCommand.Parameters.Add("@OldShipCountry",
      SqlDbType.NVarChar, 15, "ShipCountry");
    ordersUpdateCommand.Parameters["@OldOrderID"].SourceVersion =
      DataRowVersion.Original;
    ordersUpdateCommand.Parameters["@OldCustomerID"].SourceVersion =
      DataRowVersion.Original;
    ordersUpdateCommand.Parameters["@OldShipCountry"].SourceVersion =
      DataRowVersion.Original;

    // create a SqlCommand object to hold the DELETE
    SqlCommand ordersDeleteCommand = mySqlConnection.CreateCommand();
    ordersDeleteCommand.CommandText =
      "DELETE FROM Orders " +
      "WHERE OrderID = @OldOrderID " +
      "AND CustomerID = @OldCustomerID " +
      "AND ShipCountry = @OldShipCountry";
    ordersDeleteCommand.Parameters.Add("@OldOrderID", SqlDbType.Int,
      0, "OrderID");
    ordersDeleteCommand.Parameters.Add("@OldCustomerID",
      SqlDbType.NChar, 5, "CustomerID");
    ordersDeleteCommand.Parameters.Add("@OldShipCountry",
      SqlDbType.NVarChar, 15, "ShipCountry");
    ordersDeleteCommand.Parameters["@OldOrderID"].SourceVersion =
      DataRowVersion.Original;
    ordersDeleteCommand.Parameters["@OldCustomerID"].SourceVersion =
      DataRowVersion.Original;
    ordersDeleteCommand.Parameters["@OldShipCountry"].SourceVersion =
      DataRowVersion.Original;

    // set the ordersDA properties
    // to the SqlCommand objects previously created
    ordersDA.SelectCommand = ordersSelectCommand;
    ordersDA.InsertCommand = ordersInsertCommand;
    ordersDA.UpdateCommand = ordersUpdateCommand;
    ordersDA.DeleteCommand = ordersDeleteCommand;
  }

  public static void DisplayDataRow(
    DataRow myDataRow,
    DataTable myDataTable
  )
  {
    Console.WriteLine("\nIn DisplayDataRow()");
    foreach (DataColumn myDataColumn in myDataTable.Columns)
    {
      Console.WriteLine(myDataColumn + " = " +
        myDataRow[myDataColumn]);
    }
  }

  public static void PushChangesToDatabase(
    SqlConnection mySqlConnection,
    SqlDataAdapter customersDA,
    SqlDataAdapter ordersDA,
    DataTable customersDT,
    DataTable ordersDT
  )
  {
    Console.WriteLine("\nIn PushChangesToDatabase()");

    mySqlConnection.Open();

    // push the new rows in customersDT to the database
    Console.WriteLine("Pushing new rows in customersDT to database");
    DataRow[] newCustomersDRArray =
      customersDT.Select("", "", DataViewRowState.Added);
    int numOfRows = customersDA.Update(newCustomersDRArray);
    Console.WriteLine("numOfRows = " + numOfRows);

    // push the new rows in ordersDT to the database
    Console.WriteLine("Pushing new rows in ordersDT to database");
    DataRow[] newOrdersDRArray =
      ordersDT.Select("", "", DataViewRowState.Added);
    numOfRows = ordersDA.Update(newOrdersDRArray);
    Console.WriteLine("numOfRows = " + numOfRows);

    // push the modified rows in customersDT to the database
    Console.WriteLine("Pushing modified rows in customersDT to database");
    DataRow[] modifiedCustomersDRArray =
      customersDT.Select("", "", DataViewRowState.ModifiedCurrent);
    numOfRows = customersDA.Update(modifiedCustomersDRArray);
    Console.WriteLine("numOfRows = " + numOfRows);

    // push the modified rows in ordersDT to the database
    Console.WriteLine("Pushing modified rows in ordersDT to database");
    DataRow[] modifiedOrdersDRArray =
      ordersDT.Select("", "", DataViewRowState.ModifiedCurrent);
    numOfRows = ordersDA.Update(modifiedOrdersDRArray);
    Console.WriteLine("numOfRows = " + numOfRows);

    // push the deletes in ordersDT to the database
    Console.WriteLine("Pushing deletes in ordersDT to database");
    DataRow[] deletedOrdersDRArray =
      ordersDT.Select("", "", DataViewRowState.Deleted);
    numOfRows = ordersDA.Update(deletedOrdersDRArray);
    Console.WriteLine("numOfRows = " + numOfRows);

    // push the deletes in customersDT to the database
    Console.WriteLine("Pushing deletes in customersDT to database");
    DataRow[] deletedCustomersDRArray =
      customersDT.Select("", "", DataViewRowState.Deleted);
    numOfRows = customersDA.Update(deletedCustomersDRArray);
    Console.WriteLine("numOfRows = " + numOfRows);

    mySqlConnection.Close();
  }

  public static void Main()
  {
    SqlConnection mySqlConnection =
      new SqlConnection(
        "server=localhost;database=Northwind;uid=sa;pwd=sa"
      );

    // setup the two DataAdapter objects
    SqlDataAdapter customersDA = new SqlDataAdapter();
    SetupCustomersDA(customersDA, mySqlConnection);
    SqlDataAdapter ordersDA = new SqlDataAdapter();
    SetupOrdersDA(ordersDA, mySqlConnection);

    // create and populate a DataSet with the rows
    // from the Customers and Orders tables
    DataSet myDataSet = new DataSet();
    mySqlConnection.Open();
    customersDA.Fill(myDataSet, "Customers");
    ordersDA.Fill(myDataSet, "Orders");
    mySqlConnection.Close();
    DataTable customersDT = myDataSet.Tables["Customers"];
    DataTable ordersDT = myDataSet.Tables["Orders"];

    // set the PrimaryKey property of customersDT
    customersDT.PrimaryKey =
      new DataColumn[]
      {
        customersDT.Columns["CustomerID"]
      };

    // set the PrimaryKey property of ordersDT
    ordersDT.PrimaryKey =
      new DataColumn[]
      {
        ordersDT.Columns["OrderID"]
      };

    // set the AllowDBNull, AutoIncrement, AutoIncrementSeed,
    // AutoIncrementStep, ReadOnly, and Unique properties for
    // the OrderID DataColumn of ordersDT
    ordersDT.Columns["OrderID"].AllowDBNull = false;
    ordersDT.Columns["OrderID"].AutoIncrement = true;
    ordersDT.Columns["OrderID"].AutoIncrementSeed = -1;
    ordersDT.Columns["OrderID"].AutoIncrementStep = -1;
    ordersDT.Columns["OrderID"].ReadOnly = true;
    ordersDT.Columns["OrderID"].Unique = true;

    // create a DataRelation object named customersOrdersDataRel
    DataRelation customersOrdersDataRel =
      new DataRelation(
        "CustomersOrders",
        customersDT.Columns["CustomerID"],
        ordersDT.Columns["CustomerID"]
      );
    myDataSet.Relations.Add(
      customersOrdersDataRel
    );

    // add a DataRow to customersDT
    Console.WriteLine("\nAdding customerDR to customersDT");
    DataRow customerDR = customersDT.NewRow();
    customerDR["CustomerID"] = "J6COM";
    customerDR["CompanyName"] = "J6 Company";
    customersDT.Rows.Add(customerDR);
    DisplayDataRow(customerDR, customersDT);

    // add a DataRow to ordersDT
    Console.WriteLine("\nAdding orderDR to ordersDT");
    DataRow orderDR = ordersDT.NewRow();
    orderDR["CustomerID"] = "J6COM";
    orderDR["ShipCountry"] = "England";
    ordersDT.Rows.Add(orderDR);
    DisplayDataRow(orderDR, ordersDT);

    // push the changes to the database
    PushChangesToDatabase(mySqlConnection,
      customersDA, ordersDA, customersDT, ordersDT);

    // update the new DataRow objects and push the changes
    // to the database
    Console.WriteLine("\nUpdating CompanyName of customerDR to Widgets Inc.");
    customerDR["CompanyName"] = "Widgets Inc.";
    DisplayDataRow(customerDR, customersDT);
    Console.WriteLine("\nUpdating ShipCompany of orderDR to USA");
    orderDR["ShipCountry"] = "USA";
    DisplayDataRow(orderDR, ordersDT);
    PushChangesToDatabase(mySqlConnection,
      customersDA, ordersDA, customersDT, ordersDT);

    // remove customerDR (because the DeleteRule is by default set to
    // Cascade, orderDR is also deleted)
    Console.WriteLine("\nDeleting customerDR (and also child orderDR)");
    customerDR.Delete();
    PushChangesToDatabase(mySqlConnection,
      customersDA, ordersDA, customersDT, ordersDT);
  }
}