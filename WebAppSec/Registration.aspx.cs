using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppSec
{
    public partial class Registration : System.Web.UI.Page
    {
        int userlevel;

        protected void Page_Load(object sender, EventArgs e)
        {
            LabelErrFirstName.Text = "";
            LabelErrLastName.Text = "";
            LabelErrEmail.Text = "";
            LabelErrUserName.Text = "";
            LabelErrComparePassword.Text = "";
            LabelErrPassword.Text = "";        
        }

        protected void registerEventMethod(object sender, EventArgs e)
        {
            try
            {
                //registerUser();
                registerUserWithHash();
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message;
            }
        }

        private void registerUserWithHash()
        {
            bool methodStatus = true;

            // validte firstname
            if (InputValidate.ValidateName(TextBoxFirstName.Text) == false)
            {
                methodStatus = false;
                LabelErrFirstName.Text = "Firstname must contain min 1 character";
            }

            // Validate lastname
            if (InputValidate.ValidateName(TextBoxLastName.Text) == false)
            {
                methodStatus = false;
                LabelErrLastName.Text = "Lastname must contain min 1 character";
            }

            // Validate lastname
            if (InputValidate.ValidateEmail(TextBoxEmail.Text) == false)
            {
                methodStatus = false;
                LabelErrEmail.Text = "Please enter a valid email-address";
            }

            // Validate username is between 5 and 30 characters
            if (InputValidate.ValidateUsername(TextBoxUserName.Text) == false)
            {
                methodStatus = false;
                LabelErrUserName.Text = "Username must be between 5 and 20 characters";
            }

            //// Validate password
            //if (InputValidate.ValidatePassword(TextBoxPassword.Text) == false)
            //{
            //    methodStatus = false;
            //    LabelErrPassword.Text = "Password must contain: Minimum 11 characters at least 1 UpperCase letter, 1 LowerCase letter, 1 Number and 1 Special Character";
            //}


            if (InputValidate.ComparePassword(TextBoxPassword.Text, TextBoxConfirmPassword.Text) == false)
            {
                methodStatus = false;
                LabelErrComparePassword.Text = "Passwords do not match";

            }
          
            if (methodStatus == true)
            {
                SqlConnection conn = new SqlConnection(@"data source = .\SQLEXPRESS; integrated security = true; database = BirdAndOwner");
                SqlCommand cmd = null;
                string sqlins = "INSERT INTO Users(FirstName,LastName,Email,Username,slowhashSalt) VALUES(@FirstName,@LastName,@Email,@Username,@slowhashSalt)";

                try
                {
                    conn.Open();

                    cmd = new SqlCommand(sqlins, conn);

                    // Add parameters to SQL command
                    cmd.Parameters.Add("@FirstName", SqlDbType.Text);
                    cmd.Parameters.Add("@LastName", SqlDbType.Text);
                    cmd.Parameters.Add("@Email", SqlDbType.Text);
                    cmd.Parameters.Add("@Username", SqlDbType.Text);

                    // add value to the parameters
                    string firstnameInput = Server.HtmlEncode(TextBoxFirstName.Text);
                    cmd.Parameters["@FirstName"].Value = firstnameInput;
                    string lastnameInput = Server.HtmlEncode(TextBoxLastName.Text);
                    cmd.Parameters["@LastName"].Value = lastnameInput;
                    string emailInput = Server.HtmlEncode(TextBoxEmail.Text);
                    cmd.Parameters["@Email"].Value = emailInput;
                    string usernameInput = Server.HtmlEncode(TextBoxUserName.Text);
                    cmd.Parameters["@Username"].Value = usernameInput;

                    int commaIndex;
                    string saltHashReturned, extractedString, hashMethod, salt, hash, iteration, lenght;

                    // generate the salt and hash password
                    string passwordInput = Server.HtmlEncode(TextBoxPassword.Text);
                    saltHashReturned = PasswordStorage.CreateHash(passwordInput);

                    // extract hashmethod SHA1
                    commaIndex = saltHashReturned.IndexOf(":");
                    extractedString = saltHashReturned.Substring(0, commaIndex);
                    hashMethod = extractedString;

                    // extract iterations
                    commaIndex = saltHashReturned.IndexOf(":");
                    extractedString = saltHashReturned.Substring(commaIndex + 1);
                    commaIndex = extractedString.IndexOf(":");
                    iteration = extractedString.Substring(0, commaIndex);

                    commaIndex = extractedString.IndexOf(":");
                    extractedString = extractedString.Substring(commaIndex + 1);
                    lenght = extractedString;

                    commaIndex = extractedString.IndexOf(":");
                    lenght = extractedString.Substring(0, commaIndex);

                    commaIndex = extractedString.IndexOf(":");
                    extractedString = extractedString.Substring(commaIndex + 1);
                    salt = extractedString;

                    commaIndex = extractedString.IndexOf(":");
                    salt = extractedString.Substring(0, commaIndex);

                    commaIndex = extractedString.IndexOf(":");
                    salt = extractedString.Substring(0, commaIndex);

                    commaIndex = extractedString.IndexOf(":");
                    extractedString = extractedString.Substring(commaIndex + 1);
                    hash = extractedString;

                    // from the first : to the second : is the iteration
                    // from the second : to the third : is the length
                    // from the third : to the fourth : is the salt
                    // from the fourth: to the end is the hash

                    // Add parameters and value to SQL command
                    cmd.Parameters.Add("@slowhashSalt", SqlDbType.Text);
                    cmd.Parameters["@slowhashSalt"].Value = saltHashReturned;

                    cmd.ExecuteReader();

                    LabelMessage.Text = "User created";

                    userlevel = 1;
                    //set session parameters
                    Session["Level"] = userlevel;
                    Session["Username"] = firstnameInput + " " + lastnameInput;
                    Response.BufferOutput = true;
                    Response.Redirect("LoggedIn.aspx");
                }

                catch (Exception ex)
                {
                    LabelMessage.Text = ex.Message;
                    LabelMessage.Text = "Error occurred - please try again";
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void registerUser()
        {
            // not using hash and salt
            SqlConnection conn = new SqlConnection(@"data source = .\SQLEXPRESS; integrated security = true; database = BirdAndOwner");
            SqlCommand cmd = null;
            string sqlins = "INSERT INTO Users(FirstName,LastName,Email,Username,Password) VALUES(@FirstName,@LastName,@Email,@Username,@Password)";

            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlins, conn);

                // Add parameters to SQL command
                cmd.Parameters.Add("@FirstName", SqlDbType.Text);
                cmd.Parameters.Add("@LastName", SqlDbType.Text);
                cmd.Parameters.Add("@Email", SqlDbType.Text);
                cmd.Parameters.Add("@Username", SqlDbType.Text);
                cmd.Parameters.Add("@Password", SqlDbType.Text);

                cmd.Parameters["@FirstName"].Value = TextBoxFirstName.Text;
                cmd.Parameters["@LastName"].Value = TextBoxLastName.Text;
                cmd.Parameters["@Email"].Value = TextBoxEmail.Text;
                cmd.Parameters["@Username"].Value = TextBoxUserName.Text;
                cmd.Parameters["@Password"].Value = TextBoxPassword.Text;

                cmd.ExecuteNonQuery();

                LabelMessage.Text = "User created";
            }

            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message;
            }

            finally
            {
                conn.Close();
            }
        }
    }
}