using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.HQ_Operations
{
    public partial class Logistics_Planning : System.Web.UI.Page
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
                new DataColumn ("LogisticsID", typeof(string)),
                new DataColumn ("PlanDate", typeof(string)),
                new DataColumn ("SourceWarehouse", typeof(string)),
                new DataColumn ("DestinationLoc", typeof(string)),
                new DataColumn ("MovementType", typeof(string)),
                new DataColumn ("PlannedDispatchDate", typeof(string)),
                new DataColumn ("ExpectedDate", typeof(string)),
                new DataColumn ("LoadType", typeof(string)),
                new DataColumn ("VehicleType", typeof(string)),
                new DataColumn ("VehicleCapacity", typeof(string)),
                new DataColumn ("TransporterName", typeof(string)),
                new DataColumn ("VehicleNo", typeof(string)),
                new DataColumn ("ContactPersonName", typeof(string)),
                new DataColumn ("ContactPersonNo", typeof(string)),
                new DataColumn ("TotalQty", typeof(string)),
                new DataColumn ("TotalWeight", typeof(string)),
                new DataColumn ("TotalValue", typeof(string)),
                new DataColumn ("Remarks", typeof(string)),
                new DataColumn ("Status", typeof(string)),
                new DataColumn ("PlannedBy", typeof(string)),
                new DataColumn ("ApprovedBy", typeof(string)),
            });

            dt1.Rows.Add("Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data", "Data");

            grdLogisticsPlanning.DataSource = dt1;
            grdLogisticsPlanning.DataBind();
            grdLogisticsPlanning.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}