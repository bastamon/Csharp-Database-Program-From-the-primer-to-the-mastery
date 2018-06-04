[WebMethod]
public DataSet RetrieveCustomers(string myCountry)
{
  SqlConnection mySqlConnection =
    new SqlConnection("server=localhost;database=Northwind;uid=sa;pwd=sa");
  string selectString =
    "SELECT CustomerID, CompanyName, Country " +
    "FROM Customers " +
    "WHERE Country LIKE '" + myCountry + "'";
  SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
  mySqlCommand.CommandText = selectString;
  SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
  mySqlDataAdapter.SelectCommand = mySqlCommand;
  DataSet myDataSet = new DataSet();
  mySqlConnection.Open();
  mySqlDataAdapter.Fill(myDataSet, "Customers");
  mySqlConnection.Close();
  return myDataSet;
}