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
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 277);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1});
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
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
			sqlConnection1.Open();
			System.Data.SqlClient.SqlDataReader mySqlDataReader =
				sqlCommand1.ExecuteReader();
			while (mySqlDataReader.Read())
			{
				listView1.Items.Add(mySqlDataReader["CustomerID"].ToString());
				listView1.Items.Add(mySqlDataReader["CompanyName"].ToString());
				listView1.Items.Add(mySqlDataReader["ContactName"].ToString());
			}
			mySqlDataReader.Close();
			sqlConnection1.Close();
		}
	}
}
