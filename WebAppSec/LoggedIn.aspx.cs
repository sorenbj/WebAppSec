using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppSec
{
    public partial class LoggedIn : System.Web.UI.Page
    {
        string name;
        int userlevel;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetNoStore();

            try
            {
                name = Convert.ToString(Session["Username"]);
                userlevel = (int)Session["Level"];

                if (name == null)
                {
                    Response.BufferOutput = true;
                    Response.Redirect("Index.aspx");
                }
                else
                {
                    LabelUser.Text = name;
                }
            }
            catch (NullReferenceException nre)
            {
                userlevel = 0;
                Session["Level"] = userlevel;
                LabelUser.Text = nre.Message;
            }
            finally
            {
                SetUserLevel();
            }      
        }

        protected void LogoutEventMethod(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Session["Level"] = 0;
            Session.Abandon();

            Response.BufferOutput = true;
            Response.Redirect("Index.aspx");
        }

        private void SetUserLevel()
        {
            if (userlevel == 1)
            {
                LabelMessage.Text = "You are logged in";
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }
    }
}