using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.Warehouse_Management_System
{
    public partial class Inbound_Receiving : System.Web.UI.Page
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
            }
        }
        private void bindGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("GRNNo", typeof(string)),
                new DataColumn ("GRNDate", typeof(string)),
                new DataColumn ("PONo", typeof(string)),
                new DataColumn ("VendorCode", typeof(string)),
                new DataColumn ("VendorName", typeof(string)),
                new DataColumn ("WarehouseCode", typeof(string)),
                new DataColumn ("WarehouseName", typeof(string)),
                new DataColumn ("ItemCode", typeof(string)),
                new DataColumn ("ItemName", typeof(string)),
                new DataColumn ("ExpectedQty", typeof(string)),
                new DataColumn ("ReceivedQty", typeof(string)),
                new DataColumn ("ShortQty", typeof(string)),
                new DataColumn ("DamagedQty", typeof(string)),
                new DataColumn ("BatchNo", typeof(string)),
                new DataColumn ("ExpiryDate", typeof(string)),
                new DataColumn ("QCStatus", typeof(string)),
                new DataColumn ("GRNStatus", typeof(string)),
                new DataColumn ("CreatedBy", typeof(string)),
                new DataColumn ("CreatedDateTime", typeof(string)),
            });

            dt1.Rows.Add("Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data");

            grdDetails.DataSource = dt1;
            grdDetails.DataBind();
            grdDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}