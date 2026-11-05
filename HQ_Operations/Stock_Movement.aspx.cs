using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.HQ_Operations
{
    public partial class Stock_Movement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER_CONFIG_TYPE"] != null && Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null)
                {
                    //if (Session["MenuText"] != null)
                    //{
                    //    //lblMenuName.Text = Session["MenuText"].ToString();
                    //}
                    bindGrid();
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
        }

        private void bindGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("StockMovID", typeof(string)),
                new DataColumn ("MovType", typeof(string)),
                new DataColumn ("MovDate", typeof(string)),
                new DataColumn ("WarehouseCode", typeof(string)),
                new DataColumn ("WarehouseName", typeof(string)),
                new DataColumn ("RefNo", typeof(string)),
                new DataColumn ("RefDate", typeof(string)),
                new DataColumn ("SourceType", typeof(string)),
                new DataColumn ("SourceName", typeof(string)),
                new DataColumn ("DestinationType", typeof(string)),
                new DataColumn ("DestinationName", typeof(string)),
                new DataColumn ("ItemCode", typeof(string)),
                new DataColumn ("ItemName", typeof(string)),
                new DataColumn ("Category", typeof(string)),
                new DataColumn ("Brand", typeof(string)),
                new DataColumn ("Qty", typeof(string)),
                new DataColumn ("ReasonCode", typeof(string)),
                new DataColumn ("Remarks", typeof(string)),
                new DataColumn ("Status", typeof(string)),
                new DataColumn ("CreatedBy", typeof(string)),
                new DataColumn ("CreatedDateTime", typeof(string)),
                new DataColumn ("ApprovedBy", typeof(string)),
                new DataColumn ("ApprovedDateTime", typeof(string)),
            });

            dt1.Rows.Add("Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data");

            grdStockMovement.DataSource = dt1;
            grdStockMovement.DataBind();
            grdStockMovement.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
       
    }
}