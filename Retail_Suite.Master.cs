using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite
{
    public partial class Retail_Suite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MenuText"] != null)
                {
                    lblMenuName.Value = Session["MenuText"].ToString();
                }
                else
                {
                    Response.Redirect("index.aspx");
                }

                if(lblMenuName.Value != null && lblMenuName.Value == "Dashboards")
                {
                    Dashboards.Visible = true;
                    HO_Operations.Visible = false;
                    Vendor_Management_Menu.Visible = false;
                    Warehouse_Management.Visible = false;
                    Store_Operations.Visible = false;
                    Reports_Analytics.Visible = false;
                }
                else if (lblMenuName.Value != null && lblMenuName.Value == "HQ Operations")
                {
                    Dashboards.Visible = false;
                    HO_Operations.Visible = true;
                    Vendor_Management_Menu.Visible = false;
                    Warehouse_Management.Visible = false;
                    Store_Operations.Visible = false;
                    Reports_Analytics.Visible = false;
                }
                else if(lblMenuName.Value != null && lblMenuName.Value == "Vendor Management System")
                {
                    Dashboards.Visible = false;
                    HO_Operations.Visible = false;
                    Vendor_Management_Menu.Visible = true;
                    Warehouse_Management.Visible = false;
                    Store_Operations.Visible = false;
                    Reports_Analytics.Visible = false;
                }
                else if (lblMenuName.Value != null && lblMenuName.Value == "Warehouse Management System")
                {
                    Dashboards.Visible = false;
                    HO_Operations.Visible = false;
                    Vendor_Management_Menu.Visible = false;
                    Warehouse_Management.Visible = true;
                    Store_Operations.Visible = false;
                    Reports_Analytics.Visible = false;
                }
                else if (lblMenuName.Value != null && lblMenuName.Value == "Store Operations")
                {
                    Dashboards.Visible = false;
                    HO_Operations.Visible = false;
                    Vendor_Management_Menu.Visible = false;
                    Warehouse_Management.Visible = false;
                    Store_Operations.Visible = true;
                    Reports_Analytics.Visible = false;
                }
                else if (lblMenuName.Value != null && lblMenuName.Value == "Reports & Analytics")
                {
                    Dashboards.Visible = false;
                    HO_Operations.Visible = false;
                    Vendor_Management_Menu.Visible = false;
                    Warehouse_Management.Visible = false;
                    Store_Operations.Visible = false;
                    Reports_Analytics.Visible = true;
                }
                else
                {
                    Dashboards.Visible = false;
                    HO_Operations.Visible = false;
                    Vendor_Management_Menu.Visible = false;
                    Warehouse_Management.Visible = false;
                    Store_Operations.Visible = false;
                    Reports_Analytics.Visible = false;
                }
            }
        }

        protected void lnkLogout_Click1(object sender, EventArgs e)
        {
            Session.Clear();
            Session["USER_CONFIG_TYPE"] = null;
            Response.Redirect("../index.aspx", true);
        }
    }
}