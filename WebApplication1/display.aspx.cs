using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class display : System.Web.UI.Page
    {
        static string stateIs = "Karnataka";
        static string mediumIs = "Native";
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Naveev\\Documents\\Visual Studio 2017\\Projects\\WebApplication1\\WebApplication1\\App_Data\\Database1.mdf\"; Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            State_Diff.Visible = false;
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void StateNameChanged(object sender, EventArgs e)
        {
            stateIs = sender.ToString();
            GetData();
        }

        protected void MediumNameChanged(object sender, EventArgs e)
        {
            mediumIs = sender.ToString();
            GetData();
        }

        protected void GetData()
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Naveev\\Documents\\Visual Studio 2017\\Projects\\WebApplication1\\WebApplication1\\App_Data\\Database1.mdf\"; Integrated Security=True");
            string mystr = Request["Id"].ToString();
            if(mystr == null)
            {
                mystr = "000";
            }
            else
            {
                State_Diff.Visible = true;
            }
            if (mystr[0] == '0')
            {
                int classIs;
                if(mystr[2] == 'A')
                {
                    classIs = 10;
                }
                else if (mystr[2] == 'B')
                {
                    classIs = 11;
                }
                else
                {
                    classIs = mystr[2] - '0';
                }
                if (mystr[1] == '0')
                {
                    SqlCommand cmd = new SqlCommand("select * from SchoolState where StateName = " + stateIs + " Class = " + classIs + " MediumName = " + mediumIs, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                }
                else if (mystr[1] == '1')
                {
                    SqlCommand cmd = new SqlCommand("select * from SchoolCBSE where Class = " + classIs, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select * from SchoolICSE where Class = " + classIs, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                }
            }
            else
            {

            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("register.aspx");
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void BindData()
        {
            DataSet ds = new DataSet();
            DataTable FromTable = new DataTable();
            conn.Open();
            string cmdstr = "select * from testtable";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            DataList1.DataSource = ds.Tables[0];
            DataList1.DataBind();
        }
    }
}