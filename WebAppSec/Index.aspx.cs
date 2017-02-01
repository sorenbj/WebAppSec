using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BotDetect;

namespace WebAppSec
{
    public partial class Index : System.Web.UI.Page
    {
        // Variables used in session
        int userlevel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                {
                    TextBoxUserName.Text = Request.Cookies["UserName"].Value;
                    TextBoxPassword.Attributes["value"] = Request.Cookies["Password"].Value;
                }
            }
        }

        protected void submitEventMethod(object sender, EventArgs e)
        {
            try
            {
                if (CheckBoxRememberMe.Checked)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                }

                Response.Cookies["UserName"].Value = TextBoxUserName.Text.Trim();
                Response.Cookies["Password"].Value = TextBoxPassword.Text.Trim();

                LoginWithPasswordHash();
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message;
            }
            
        }

        private void LoginWithPasswordHash()
        {
            List<string> salthashList = null;
            List<string> nameList = null;

            bool isHuman = ExampleCaptcha.Validate(CaptchaCodeTextBox.Text);

            if (!isHuman)
            {
                // clear previous user input
                CaptchaCodeTextBox.Text = "";
                CaptchaErrorLabel.Text = "Text do not match";
            }
            else
            {
                SqlConnection conn = new SqlConnection(@"data source = .\SQLEXPRESS; integrated security = true; database = BirdAndOwner");
                SqlCommand cmd = null;
                SqlDataReader rdr = null;
                string sqlsel = "SELECT slowhashSalt, Firstname, Lastname FROM Users WHERE Username = @Username";

                try
                {

                    conn.Open();

                    cmd = new SqlCommand(sqlsel, conn);
                    cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
                    cmd.Parameters["@Username"].Value = Server.HtmlEncode(TextBoxUserName.Text);

                    rdr = cmd.ExecuteReader();

                    while (rdr.HasRows && rdr.Read())
                    {
                        if (salthashList == null)
                        {
                            salthashList = new List<string>();
                            nameList = new List<string>();
                        }

                        string saltHashes = rdr.GetString(rdr.GetOrdinal("slowhashSalt"));
                        salthashList.Add(saltHashes);

                        string fullName = rdr.GetString(rdr.GetOrdinal("Firstname")) + " " + rdr.GetString(rdr.GetOrdinal("Lastname"));
                        nameList.Add(fullName);
                    }

                    rdr.Close();

                    if (salthashList != null)
                    {
                        for (int i = 0; i < salthashList.Count; i++)
                        {
                            string inputPassword = Server.HtmlEncode(TextBoxPassword.Text);
                            bool validUser = PasswordStorage.VerifyPassword(inputPassword, salthashList[i]);
                            if (validUser == true)
                            {
                                userlevel = 1;
                                //set session parameters
                                Session["Level"] = userlevel;
                                Session["Username"] = nameList[i];
                                Response.BufferOutput = true;
                                Response.Redirect("LoggedIn.aspx");
                            }
                            else
                            {
                                LabelMessage.Text = "Invalid username or password";
                                userlevel = 0;
                                //set session parameters
                                Session["Level"] = userlevel;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LabelMessage.Text = ex.Message;
                    LabelMessage.Text = "Error occurred - please try again";
                }
            }
        }

        //private void DoSql()
        //{
        //    SqlConnection conn = new SqlConnection(@"data source = .\SQLEXPRESS; integrated security = true; database = BirdAndOwner");
        //    SqlCommand cmd = null;
        //    SqlDataReader rdr = null;
        //    string sqlsel = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

        //    try
        //    {
        //        conn.Open();

        //        cmd = new SqlCommand(sqlsel, conn);

        //        // Add parameters to SQL command

        //        cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
        //        cmd.Parameters.Add("@Password", SqlDbType.NVarChar);

        //        cmd.Parameters["@Username"].Value = TextBoxUserName.Text;
        //        cmd.Parameters["@Password"].Value = TextBoxPassword.Text;

        //        rdr = cmd.ExecuteReader();

        //        String name = "";

        //        while (rdr.HasRows && rdr.Read())
        //        {
        //            name = rdr.GetString(rdr.GetOrdinal("Firstname")) + " " + rdr.GetString(rdr.GetOrdinal("Lastname"));
        //        }

        //        if (rdr.HasRows)
        //        {
        //            Session["Username"] = name;
        //            Response.BufferOutput = true;
        //            Response.Redirect("LoggedIn.aspx");
        //        }
        //        else
        //        {
        //            LabelMessage.Text = "Invalid username or password";
        //        }

        //        rdr.Close();

        //        LabelMessage.Text = "User logged in ";
        //    }

        //    catch (Exception ex)
        //    {
        //        LabelMessage.Text = ex.Message;
        //    }

        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
    }
}