using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.Configuration_Module
{
    public partial class Config_Module : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER_CONFIG_TYPE"] != null && Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null)
                {                    
                    lblHQMenuName.Value = Session["MenuText"].ToString();
                    lbl_Username.Text = Session["UserName"].ToString();
                    lbl_User.Text = Session["UserName"].ToString();
                    lbl_Designation.Text = Session["DESIGNATION"].ToString();
                    lbl_Department.Text = Session["DEPT_NAME"].ToString();
                    lbl_Emailid.Text = Session["EMAIL_ID"].ToString();
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("../index.aspx", true);
        }
    }
}