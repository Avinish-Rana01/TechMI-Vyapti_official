using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite
{
    public partial class Dashboards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MenuText"] != null)
                {
                    lblMenuName.Text = "All Dashboards";
                }
            }
        }
        protected void HO_Dashboard_Click(object sender, EventArgs e)
        {
            var dashboards = Master.FindControl("Dashboards");
            var ho = Master.FindControl("HO_Dashboard");
            var vendor = Master.FindControl("Vendor_Management_Menu");
            var warehouse = Master.FindControl("Warehouse_Management");
            var store = Master.FindControl("Store_Operations");
            var reports = Master.FindControl("Reports_Analytics");

            if (dashboards != null) dashboards.Visible = false;
            if (vendor != null) vendor.Visible = false;
            if (warehouse != null) warehouse.Visible = false;
            if (store != null) store.Visible = false;
            if (reports != null) reports.Visible = false;

            if (ho != null) ho.Visible = true;

            Response.Redirect("HO_Dashboard.aspx");
        }

        protected void Vendor_Dashboard_Click(object sender, EventArgs e)
        {
            var dashboards = Master.FindControl("Dashboards");
            var inventory = Master.FindControl("Inventory_Management");
            var vendor = Master.FindControl("Vendor_Management_Menu");
            var warehouse = Master.FindControl("Warehouse_Management");
            var store = Master.FindControl("Store_Operations");
            var reports = Master.FindControl("Reports_Analytics");

            if (dashboards != null) dashboards.Visible = false;
            if (inventory != null) inventory.Visible = false;
            if (warehouse != null) warehouse.Visible = false;
            if (store != null) store.Visible = false;
            if (reports != null) reports.Visible = false;

            if (vendor != null) vendor.Visible = true;

            Response.Redirect("Vendor_Dashboard.aspx");
        }

        protected void Warehouse_Dashboard_Click(object sender, EventArgs e)
        {
            var dashboards = Master.FindControl("Dashboards");
            var inventory = Master.FindControl("Inventory_Management");
            var vendor = Master.FindControl("Vendor_Management_Menu");
            var warehouse = Master.FindControl("Warehouse_Management");
            var store = Master.FindControl("Store_Operations");
            var reports = Master.FindControl("Reports_Analytics");

            if (dashboards != null) dashboards.Visible = false;
            if (inventory != null) inventory.Visible = false;
            if (vendor != null) vendor.Visible = false;
            if (store != null) store.Visible = false;
            if (reports != null) reports.Visible = false;

            if (warehouse != null) warehouse.Visible = true;

            Response.Redirect("Warehouse_Dashboard.aspx");
        }

        protected void Store_Dashboard_Click(object sender, EventArgs e)
        {
            var dashboards = Master.FindControl("Dashboards");
            var inventory = Master.FindControl("Inventory_Management");
            var vendor = Master.FindControl("Vendor_Management_Menu");
            var warehouse = Master.FindControl("Warehouse_Management");
            var store = Master.FindControl("Store_Operations");
            var reports = Master.FindControl("Reports_Analytics");

            if (dashboards != null) dashboards.Visible = false;
            if (vendor != null) vendor.Visible = false;
            if (warehouse != null) warehouse.Visible = false;
            if (store != null) store.Visible = true;
            if (reports != null) reports.Visible = false;
            if (inventory != null) inventory.Visible = true;

            Response.Redirect("Store_Dashboard.aspx");
        }

        protected void Reports_Dashboard_Click(object sender, EventArgs e)
        {
            var dashboards = Master.FindControl("Dashboards");
            var inventory = Master.FindControl("Inventory_Management");
            var vendor = Master.FindControl("Vendor_Management_Menu");
            var warehouse = Master.FindControl("Warehouse_Management");
            var store = Master.FindControl("Store_Operations");
            var reports = Master.FindControl("Reports_Analytics");

            if (dashboards != null) dashboards.Visible = false;
            if (vendor != null) vendor.Visible = false;
            if (warehouse != null) warehouse.Visible = false;
            if (store != null) store.Visible = false;
            if (reports != null) reports.Visible = true;
            if (inventory != null) inventory.Visible = true;

            Response.Redirect("Reports_Dashboard.aspx");
        }
       
    }
}