using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AssignmentBy192444U
{
    public partial class Register : System.Web.UI.Page
    {
        string dbConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        private bool ValidName(string fName, string lName)
        {
            if (fName.Length != 0 || lName.Length != 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private bool ValidPw(string pass)
        {
            if (pass.Length < 8)
            {
                return false;
            }
            else if (Regex.IsMatch(pass, "[a-z]"))
            {
                return false;
            }
            else if (Regex.IsMatch(pass, "[A-Z]"))
            {
                return false;
            }
            else if (Regex.IsMatch(pass, "[0-9]"))
            {
                return false;
            }
            else if (Regex.IsMatch(pass, "[$@#&*!+=-]"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void CreateUser()
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(dbConnStr))
                {
                    string cmdStr = "INSERT INTO  Users (Email,FName,LName,DOB,PHash,PSalt,CardNum,SecNum,IV,Keye) VALUES(@Email, @FName, @LName, @DOB, @PHash, @PSalt, @CardNum, @SecNum, @IV, @Key)";
                    using (SqlCommand cmd = new SqlCommand(cmdStr))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", TbEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@FName", TbFName.Text.Trim());
                        cmd.Parameters.AddWithValue("@LName", TbLName.Text.Trim());
                        cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(TbDOB.Text.Trim()));
                        cmd.Parameters.AddWithValue("@PHash", finalHash);
                        cmd.Parameters.AddWithValue("@PSalt", salt);
                        cmd.Parameters.AddWithValue("@CardNum", Convert.ToBase64String(encrypteD(TbCardNum.Text.Trim())));
                        cmd.Parameters.AddWithValue("@SecNum", Convert.ToBase64String(encrypteD(TbSecNum.Text.Trim())));
                        cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                        cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                        cmd.Connection = dbConn;
                        dbConn.Open();
                        cmd.ExecuteNonQuery();
                        dbConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        protected byte[] encrypteD(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0,
               plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }
        private bool ValidCard(string num, string secNum)
        {
            bool can = true;
            try
            {
                Convert.ToInt32(num);
                Convert.ToInt32(secNum);
            }
            catch (Exception)
            {
                can = false;
            }
            return can;
        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            bool nameValid = ValidName(TbFName.Text, TbLName.Text);
            bool passValid = ValidPw(TbPass.Text);
            bool cardValid = ValidCard(TbCardNum.Text, TbSecNum.Text);

            /*if (passValid)
            {
                LbPassChecker.Text = "Sorry ah, you need at least 8 big letter, small letter, number and some of these symbol ('$' '@' '#' '&' '*' '!' '+' '=' '-') rojak together";
                LbPassChecker.ForeColor = System.Drawing.Color.Red;
            }
            else if (nameValid)
            {
                LbErrors.Text += "You don't have a first name and/or last name?";
            }
            else if (cardValid)
            {
                LbErrors.Text += "Card details had issues. Enter again tolong.";
            }
            else
            {*/
                //Hashing pw
                string pass = TbPass.Text;

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];
                rng.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);
                SHA512Managed hashing = new SHA512Managed();
                string saltedPass = pass + salt;
                byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pass));
                byte[] saltedHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(saltedPass));
                finalHash = Convert.ToBase64String(saltedHash);
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.GenerateKey();
                Key = cipher.Key;
                IV = cipher.IV;

                CreateUser();
                Response.Redirect("Login.aspx", false);
            //}
        }
    }
}