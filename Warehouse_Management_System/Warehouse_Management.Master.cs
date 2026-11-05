using Patel.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.Warehouse_Management_System
{
    public partial class Warehouse_Management : System.Web.UI.MasterPage
    {
        string ConnStr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        string SP_Name = "[SP_MASTER]";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null)
                {
                    lblWarehouseMenuName.Value = Session["MenuText"].ToString();
                    lbl_Username.Text = Session["UserName"].ToString();
                    lbl_User.Text = Session["UserName"].ToString();
                    lbl_Designation.Text = Session["DESIGNATION"] != null ? Session["DESIGNATION"].ToString() : "";
                    lbl_Department.Text = Session["DEPT_NAME"] != null ? Session["DEPT_NAME"].ToString() : "";
                    lbl_Emailid.Text = Session["EMAIL_ID"] != null ? Session["EMAIL_ID"].ToString() : "";

                    BIND_ACCESS_MENU(Session["MenuText"].ToString());
                }
                else
                {
                    Response.Redirect("../index.aspx", true);
                }
            }
        }


        public void BIND_ACCESS_MENU(string Module)
        {
            try
            {
                List<SqlParameter> GetMenu_Para = new List<SqlParameter>();
                GetMenu_Para.Add(new SqlParameter("@Action", "GET_ACCESS_MENU_SUB_MENU"));
                GetMenu_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"].ToString().Trim()));
                //GetMenu_Para.Add(new SqlParameter("@USER_NAME", Session["UserName"].ToString().Trim()));
                GetMenu_Para.Add(new SqlParameter("@USER_MODULE_NAME", Module.ToString().Trim()));
                DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, SP_Name, GetMenu_Para.ToArray());
                if (ds.Tables[0].Rows.Count != 0)
                {
                    string Menu_ = ds.Tables[0].Rows[0]["MENU_NAMES"].ToString();
                    string sub_menu_ = ds.Tables[0].Rows[0]["SUBMENU_NAMES"].ToString();

                    // menu
                    if (!string.IsNullOrEmpty(Menu_))
                    {
                        string[] SelectedMenu = Menu_.Split(',');
                        //foreach (string Menu in Menu_.Split(','))
                        //{
                        if (SelectedMenu.Any(x => x.Trim().Equals("Inward Operations", StringComparison.OrdinalIgnoreCase)))
                        {
                            Inward_Ops.Visible = true;
                        }
                        if (SelectedMenu.Any(x => x.Trim().Equals("Procurement Management", StringComparison.OrdinalIgnoreCase)))
                        {
                            //Procurement_Id.Visible = true;
                        }
                        if (SelectedMenu.Any(x => x.Trim().Equals("Warehouse & Management", StringComparison.OrdinalIgnoreCase)))
                        {
                            //Warehouse_Management_Id.Visible = true;
                        }

                    }
                    // sub menu
                    if (!string.IsNullOrEmpty(sub_menu_))
                    {
                        string[] SelectedSubMenu = sub_menu_.Split(',');
                        //foreach (string Submenu in sub_menu_.Split(','))
                        //{
                        if (SelectedSubMenu.Any(x => x.Trim().Equals("View HU Details", StringComparison.OrdinalIgnoreCase)))
                        {
                            View_HU_Dtl.Visible = true;
                        }
                        if (SelectedSubMenu.Any(x => x.Trim().Equals("Material Master", StringComparison.OrdinalIgnoreCase)))
                        {
                            //Material_Id.Visible = true;
                        }
                        if (SelectedSubMenu.Any(x => x.Trim().Equals("EAN Master", StringComparison.OrdinalIgnoreCase)))
                        {
                            //EAN_Id.Visible = true;
                        }




                    }
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Username or Password!!! ')", true);
                    return;
                }
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