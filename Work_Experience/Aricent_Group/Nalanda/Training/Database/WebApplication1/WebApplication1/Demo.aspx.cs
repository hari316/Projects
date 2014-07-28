using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace WebApplication1
{
  
    public partial class Demo : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
              
            conn.ConnectionString = constr;
            SqlDataReader rdr = null;
            try
            {
               // Open the connection
                conn.Open();

                // Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("select * from Employee", conn);

                // get query results
                rdr = cmd.ExecuteReader();

                // Load into data table
                DataTable dt = new DataTable();
                dt.Load(rdr);

               // GridView1.DataSource = dt;
                //GridView1.DataBind();
                // print the CustomerName of each record
              /*  while (rdr.Read())
                {
                    Response.Write(rdr[0]);
                }

               */
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox Fname = GridView1.FooterRow.FindControl("FnameTb") as TextBox;
            TextBox Lname = GridView1.FooterRow.FindControl("LnameTb") as TextBox;
            TextBox Ssn = GridView1.FooterRow.FindControl("SsnTb") as TextBox;
            TextBox Designation = GridView1.FooterRow.FindControl("DesignationTb") as TextBox;
            TextBox Dno = GridView1.FooterRow.FindControl("DnoTb") as TextBox;
            TextBox Super_ssn = GridView1.FooterRow.FindControl("Super_ssn") as TextBox;

            SqlDataSource1.InsertParameters["Fname"].DefaultValue = FnameTb.Text;
            SqlDataSource1.InsertParameters["Lname"].DefaultValue = LnameTb.Text;
            SqlDataSource1.InsertParameters["Ssn"].DefaultValue = SsnTb.Text;
            SqlDataSource1.InsertParameters["Designation"].DefaultValue = DesignationTb.Text;
            SqlDataSource1.InsertParameters["Dno"].DefaultValue = DnoTb.Text;
            SqlDataSource1.InsertParameters["Super_ssn"].DefaultValue = Super_ssnTb.Text;

            SqlDataSource1.Insert();


        }
    }
}
