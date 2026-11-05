using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite
{
    public partial class Inventory_Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MenuText"] != null)
                {
                    //lblMenuName.Text = Session["MenuText"].ToString();
                }
                bindWODetails();
            }
        }
        private void bindWODetails()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("WOStatus", typeof(string)),
                new DataColumn ("Count", typeof(string)),
            });

            dt1.Rows.Add("Total WO Received", "3");
            dt1.Rows.Add("Pending WO", "1");
            dt1.Rows.Add("In-Transit WO", "1");
            dt1.Rows.Add("Completed WO", "1");

            grdWODetails.DataSource = dt1;
            grdWODetails.DataBind();
            grdWODetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}