using Patel.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.Vendor_Management_System
{
    public partial class Vendor_Management : System.Web.UI.MasterPage
    {
        string ConnStr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        string SP_Name = "[SP_MASTER]";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null && Session["USER_TYPE"]!=null)
                {
                    lblVendorMenuName.Value = Session["MenuText"].ToString();
                    lbl_Username.Text = Session["UserName"].ToString();
                    lbl_User.Text = Session["UserName"].ToString();
                    lbl_Designation.Text = Session["DESIGNATION"].ToString();
                    lbl_Department.Text = Session["DEPT_NAME"].ToString();
                    lbl_Emailid.Text = Session["EMAIL_ID"].ToString();
                    BIND_ACCESS_MENU(Session["MenuText"].ToString());
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }                
            }
        }
        public void BIND_ACCESS_MENU(string Module)
        {
            try
            {
                if (Session["USER_TYPE"].ToString().Trim() == "Tag Vendor")
                {
                    Tag_Creation_Id.Visible = true;
                    Work_Order_Menu_Id.Visible = false;
                    Tag_Receive_Menu_Id.Visible = false;
                    ASN_Menu_Id.Visible = false;
                }
                else if (Session["USER_TYPE"].ToString().Trim() == "Vendor")
                {
                    Tag_Creation_Id.Visible = false;
                    Work_Order_Menu_Id.Visible = true;
                    Tag_Receive_Menu_Id.Visible = true;
                    ASN_Menu_Id.Visible = true;
                }
                //List<SqlParameter> GetMenu_Para = new List<SqlParameter>();
                //GetMenu_Para.Add(new SqlParameter("@Action", "GET_ACCESS_MENU_SUB_MENU"));
                //GetMenu_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"].ToString().Trim()));
                //GetMenu_Para.Add(new SqlParameter("@USER_MODULE_NAME", Module.ToString().Trim()));
                //DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, SP_Name, GetMenu_Para.ToArray());
                //if (ds.Tables[0].Rows.Count != 0)
                //{
                //    string Menu_ = ds.Tables[0].Rows[0]["MENU_NAMES"].ToString();
                //    string sub_menu_ = ds.Tables[0].Rows[0]["SUBMENU_NAMES"].ToString();

                //    // menu
                //    if (!string.IsNullOrEmpty(Menu_))
                //    {
                //        string[] SelectedMenu = Menu_.Split(',');
                //        if (Session["USER_TYPE"].ToString().Trim() == "Super Admin")
                //        {
                //            if (SelectedMenu.Any(x => x.Trim().Equals("Work Order Details", StringComparison.OrdinalIgnoreCase)))
                //            {
                //                Work_Order_Menu_Id.Visible = true;
                //            }
                //            if (SelectedMenu.Any(x => x.Trim().Equals("Tag Receive Acknowledgement", StringComparison.OrdinalIgnoreCase)))
                //            {
                //                Tag_Receive_Menu_Id.Visible = true;
                //            }
                //            if (SelectedMenu.Any(x => x.Trim().Equals("ASN Details", StringComparison.OrdinalIgnoreCase)))
                //            {
                //                ASN_Menu_Id.Visible = true;
                //            }
                //        }
                //        else if (Session["USER_TYPE"].ToString().Trim() == "Tag Vendor")
                //        {
                //            if (SelectedMenu.Any(x => x.Trim().Equals("Tag Creation", StringComparison.OrdinalIgnoreCase)))
                //            {
                //                Tag_Creation_Id.Visible = true;
                //            }
                //        }
                //        else if (Session["USER_TYPE"].ToString().Trim() == "Vendor")
                //        {
                //            if (SelectedMenu.Any(x => x.Trim().Equals("Work Order Details", StringComparison.OrdinalIgnoreCase)))
                //            {
                //                Work_Order_Menu_Id.Visible = true;
                //            }
                //            if (SelectedMenu.Any(x => x.Trim().Equals("Tag Receive Acknowledgement", StringComparison.OrdinalIgnoreCase)))
                //            {
                //                Tag_Receive_Menu_Id.Visible = true;
                //            }
                //            if (SelectedMenu.Any(x => x.Trim().Equals("ASN Details", StringComparison.OrdinalIgnoreCase)))
                //            {
                //                ASN_Menu_Id.Visible = true;
                //            }
                //        }                        
                //     }
                //    // sub menu
                //    if (!string.IsNullOrEmpty(sub_menu_))
                //    {
                //        string[] SelectedSubMenu = sub_menu_.Split(',');
                //    }
                //}
                //else
                //{
                //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Username or Password!!! ')", true);
                //    return;
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
        protected void lnkLogout_Click1(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("../index.aspx", true);
        }
    }
}