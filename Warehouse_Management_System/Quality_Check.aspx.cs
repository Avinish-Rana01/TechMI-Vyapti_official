using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.Warehouse_Management_System
{
    public partial class Quality_Check : System.Web.UI.Page
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
                new DataColumn ("QCNo", typeof(string)),
                new DataColumn ("GRNNo", typeof(string)),
                new DataColumn ("QCUser", typeof(string)),
                new DataColumn ("QCDate", typeof(string)),
                new DataColumn ("ItemCode", typeof(string)),
                new DataColumn ("ItemName", typeof(string)),
                new DataColumn ("CheckedQty", typeof(string)),
                new DataColumn ("AcceptedQty", typeof(string)),
                new DataColumn ("RejectedQty", typeof(string)),
                new DataColumn ("RejectionReason", typeof(string)),
                new DataColumn ("QCStatus", typeof(string)),
            });

            dt1.Rows.Add("Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data");

            grdDetails.DataSource = dt1;
            grdDetails.DataBind();
            grdDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}