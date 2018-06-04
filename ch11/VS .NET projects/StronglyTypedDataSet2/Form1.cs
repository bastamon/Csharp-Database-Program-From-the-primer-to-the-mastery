using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace WindowsApplication4
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Data.SqlClient.SqlConnection sqlConnection1;
		private System.Data.SqlClient.SqlCommand sqlCommand1;
		private System.Windows.Forms.ListView listView1;
		private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		private WindowsApplication4.MyDataSet myDataSet1;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();
			this.listView1 = new System.Windows.Forms.ListView();
			this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
			this.myDataSet1 = new WindowsApplication4.MyDataSet();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
			((System.ComponentModel.ISupportInitialize)(this.myDataSet1)).BeginInit();
			this.SuspendLayout();
			// 
			// sqlConnection1
			// 
			this.sqlConnection1.ConnectionString = "data source=localhost;initial catalog=Northwind;persist security info=False;user " +
				"id=sa;pwd=sa;workstation id=JMPRICE-DT1;packet size=4096";
			// 
			// sqlCommand1
			// 
			this.sqlCommand1.CommandText = "SELECT CustomerID, CompanyName, ContactName FROM Customers";
			this.sqlCommand1.Connection = this.sqlConnection1;
			// 
			// listView1
			// 
			this.listView1.Location = new System.Drawing.Point(8, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(296, 256);
			this.listView1.TabIndex = 0;
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.DeleteCommand = this.sqlDeleteCommand1;
			this.sqlDataAdapter1.InsertCommand = this.sqlInsertCommand1;
			this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "Customers", new System.Data.Common.DataColumnMapping[] {
																																																				   new System.Data.Common.DataColumnMapping("CustomerID", "CustomerID"),
																																																				   new System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"),
																																																				   new System.Data.Common.DataColumnMapping("Address", "Address")})});
			this.sqlDataAdapter1.UpdateCommand = this.sqlUpdateCommand1;
			// 
			// myDataSet1
			// 
			this.myDataSet1.DataSetName = "MyDataSet";
			this.myDataSet1.Locale = new System.Globalization.CultureInfo("en-US");
			this.myDataSet1.Namespace = "http://tempuri.org/MyDataSet.xsd";
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "SELECT CustomerID, CompanyName, Address FROM Customers";
			this.sqlSelectCommand1.Connection = this.sqlConnection1;
			// 
			// sqlInsertCommand1
			// 
			this.sqlInsertCommand1.CommandText = "INSERT INTO Customers(CustomerID, CompanyName, Address) VALUES (@CustomerID, @Com" +
				"panyName, @Address); SELECT CustomerID, CompanyName, Address FROM Customers WHER" +
				"E (CustomerID = @CustomerID)";
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar, 5, "CustomerID"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar, 40, "CompanyName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 60, "Address"));
			this.sqlInsertCommand1.Connection = this.sqlConnection1;
			// 
			// sqlUpdateCommand1
			// 
			this.sqlUpdateCommand1.CommandText = @"UPDATE Customers SET CustomerID = @CustomerID, CompanyName = @CompanyName, Address = @Address WHERE (CustomerID = @Original_CustomerID) AND (Address = @Original_Address OR @Original_Address IS NULL AND Address IS NULL) AND (CompanyName = @Original_CompanyName); SELECT CustomerID, CompanyName, Address FROM Customers WHERE (CustomerID = @CustomerID)";
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar, 5, "CustomerID"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar, 40, "CompanyName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 60, "Address"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CustomerID", System.Data.SqlDbType.NVarChar, 5, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CustomerID", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Address", System.Data.SqlDbType.NVarChar, 60, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Address", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.NVarChar, 40, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CompanyName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Connection = this.sqlConnection1;

			// 
			// sqlDeleteCommand1
			// 
			this.sqlDeleteCommand1.CommandText = "DELETE FROM Customers WHERE (CustomerID = @Original_CustomerID) AND (Address = @O" +
				"riginal_Address OR @Original_Address IS NULL AND Address IS NULL) AND (CompanyNa" +
				"me = @Original_CompanyName)";
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CustomerID", System.Data.SqlDbType.NVarChar, 5, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CustomerID", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Address", System.Data.SqlDbType.NVarChar, 60, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Address", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.NVarChar, 40, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CompanyName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Connection = this.sqlConnection1;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 277);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1});
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.myDataSet1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
            // populate the DataSet with the CustomerID, CompanyName,
			// and Address columns from the Customers table
			sqlConnection1.Open();
			sqlDataAdapter1.Fill(myDataSet1, "Customers");

			// get the Customers DataTable
			MyDataSet.CustomersDataTable myDataTable =
				myDataSet1.Customers;

			// create a new DataRow in myDataTable using the
			// NewCustomersRow() method of myDataTable
			MyDataSet.CustomersRow myDataRow =
				myDataTable.NewCustomersRow();

			// set the CustomerID, CompanyName, and Address
			// of myDataRow
			myDataRow.CustomerID = "J5COM";
			myDataRow.CompanyName = "J5 Company";
			myDataRow.Address = "1 Main Street";

			// add the new row to myDataTable using the
			// AddCustomersRow() method
			myDataTable.AddCustomersRow(myDataRow);

			// push the new row to the database using
			// the Update() method of sqlDataAdapter1
			sqlDataAdapter1.Update(myDataTable);

			// find the row using the FindByCustomerID()
			// method of myDataTable
			myDataRow = myDataTable.FindByCustomerID("J5COM");

			// modify the CompanyName and Address of myDataRow
			myDataRow.CompanyName = "Widgets Inc.";
			myDataRow.Address = "1 Any Street";

			// push the modification to the database
			sqlDataAdapter1.Update(myDataTable);

			// display the DataRow objects in myDataTable
			// in the listView1 object
			foreach (MyDataSet.CustomersRow myDataRow2 in myDataTable.Rows)
			{
				listView1.Items.Add(myDataRow2.CustomerID);
				listView1.Items.Add(myDataRow2.CompanyName);

				// if the Address is null, set Address to "Unknown"
				if (myDataRow2.IsAddressNull()== true)
				{
					myDataRow2.Address = "Unknown";
				}
				listView1.Items.Add(myDataRow2.Address);
			}

			// find and remove the new row using the
			// FindByCustomerID() and RemoveCustomersRow() methods
			// of myDataTable
			myDataRow = myDataTable.FindByCustomerID("J5COM");
			myDataTable.RemoveCustomersRow(myDataRow);

			// push the delete to the database
			sqlDataAdapter1.Update(myDataTable);

			sqlConnection1.Close();
		}
	}
}
