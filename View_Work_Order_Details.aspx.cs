using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite
{
    public partial class View_Work_Order_Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MenuText"] != null)
                {
                    lblMenuName.Text = Session["MenuText"].ToString();
                }
                bindGrid();
                viewDetailsGrid();
            }
        }
        private void bindGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("WONo", typeof(string)),
                new DataColumn ("VendorName", typeof(string)),
                new DataColumn ("WODate", typeof(string)),
                new DataColumn ("WOStatus", typeof(string)),
            });

            dt1.Rows.Add("WO2500002", "Amazon", "2025-12-9","Pending");
            dt1.Rows.Add("WO2500003", "Amazon", "2025-12-9", "In-Transit");
            dt1.Rows.Add("WO2500004", "Amazon", "2025-12-9", "Completed");

            grdWODetails.DataSource = dt1;
            grdWODetails.DataBind();
            grdWODetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        
        private void viewDetailsGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("LineItem", typeof(string)),
                new DataColumn ("WOQty", typeof(string)),
                new DataColumn ("TagSupply", typeof(string)),
            });

            dt1.Rows.Add("ABC", "1", "YES");

            grdViewDetails.DataSource = dt1;
            grdViewDetails.DataBind();
            grdViewDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void View(object sender, EventArgs e)
        {
            bindGrid();
            viewDetailsGrid();
            popup.Show();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            bindGrid();
        }
    }
}