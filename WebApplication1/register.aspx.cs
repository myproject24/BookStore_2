using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class register : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Naveev\\Documents\\Visual Studio 2017\\Projects\\WebApplication1\\WebApplication1\\App_Data\\Database1.mdf\"; Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from RegisterInfo", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bool f1 = false;
            foreach (DataRow row in dt.Rows)
            {
                string uname = row["Username"].ToString();
                if(uname == Username.Text)
                {
                    f1 = true;
                    break;
                }
            }
            if (!Password1.Text.Equals(Password2.Text)) 
            {
                FailureText.Text = "Username already exists!!";
                ErrorMessage.Visible = true;
            }
            else if (f1)
            {
                FailureText.Text = "Passwords do not match!!";
                ErrorMessage.Visible = true;
            }
            else
            {
                cmd = new SqlCommand("insert into RegisterInfo(Username,FirstName,LastName,Password) values("+Username.Text+","+Firstname.Text+","+Lastname.Text+","+Password1.Text+")", conn);
                cmd.ExecuteNonQuery();
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
    }
}