using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.HQ_Operations
{
    public partial class Inter_Branch_Transfer : System.Web.UI.Page
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
                new DataColumn ("TransferID", typeof(string)),
                new DataColumn ("FromLoc", typeof(string)),
                new DataColumn ("ToLoc", typeof(string)),
                new DataColumn ("RequestDate", typeof(string)),
                new DataColumn ("DispatchDate", typeof(string)),
                new DataColumn ("ExpectedDate", typeof(string)),
                new DataColumn ("VehicleReq", typeof(string)),
                new DataColumn ("TransporterName", typeof(string)),
                new DataColumn ("VehicleNo", typeof(string)),
                new DataColumn ("ContactPersonName", typeof(string)),
                new DataColumn ("ContactPersonNo", typeof(string)),
                new DataColumn ("ItemCode", typeof(string)),
                new DataColumn ("ItemName", typeof(string)),
                new DataColumn ("Category", typeof(string)),
                new DataColumn ("Brand", typeof(string)),
                new DataColumn ("ReqQty", typeof(string)),
                new DataColumn ("ApprovedQty", typeof(string)),
                new DataColumn ("DispatchedQty", typeof(string)),
                new DataColumn ("Remarks", typeof(string)),
                new DataColumn ("Status", typeof(string)),
                new DataColumn ("RequestedBy", typeof(string)),
                new DataColumn ("RequestedDateTime", typeof(string)),
                new DataColumn ("DispatchBy", typeof(string)),
                new DataColumn ("DispatchDateTime", typeof(string)),
                new DataColumn ("ReceiveBy", typeof(string)),
                new DataColumn ("ReceiveDateTime", typeof(string)),
            });

            dt1.Rows.Add("Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data");

            grdInterBranchTransfer.DataSource = dt1;
            grdInterBranchTransfer.DataBind();
            grdInterBranchTransfer.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}