using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;

namespace AssignmentBy192444U
{
    public partial class Login : System.Web.UI.Page
    {    
        string dbConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
        protected string getHash(string username)
        {
            string hsah = null;
            SqlConnection dbConn = new SqlConnection(dbConnStr);
            string sql = "select PHash FROM Users WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, dbConn);
            command.Parameters.AddWithValue("@EMAIL", username);
            try
            {
                dbConn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PHash"] != null)
                        {
                            if (reader["PHash"] != DBNull.Value)
                            {
                                hsah = reader["PHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { dbConn.Close(); }
            return hsah;
        }

        protected string getSalt(string username)
        {
            string tlas = null;
            SqlConnection dbConn = new SqlConnection(dbConnStr);
            string sql = "select PSalt FROM Users WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, dbConn);
            command.Parameters.AddWithValue("@EMAIL", username);
            try
            {
                dbConn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PSalt"] != null)
                        {
                            if (reader["PSalt"] != DBNull.Value)
                            {
                                tlas = reader["PSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { dbConn.Close(); }
            return tlas;
        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            string pass = TbLoginPass.Text.ToString().Trim();
            string userid = TbUserName.Text.ToString().Trim();
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getHash(userid);
            string dbSalt = getSalt(userid);
            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string saltedPass = pass + dbSalt;
                    byte[] saltedHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(saltedPass));
                    string userHash = Convert.ToBase64String(saltedHash);
                    if (userHash.Equals(dbHash))
                    {
                        Session["LoggedIn"] = TbLoginPass.Text.Trim();
                        string guid = Guid.NewGuid().ToString();
                        Session["AuTok"] = guid;
                        Response.Cookies.Add(new HttpCookie("AuTok", guid));
                        Response.Redirect("Succeded.aspx", false);
                    }
                    else
                    {
                        errorMsg = "Email or password not right. Please key in again.";
                        LbError.Text = errorMsg;
                    }
                }
            }
            catch
            {

            }
        }

    }
}