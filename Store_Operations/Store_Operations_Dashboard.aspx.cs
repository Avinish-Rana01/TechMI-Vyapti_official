using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.Store_Operations
{
    public partial class Store_Operations_Dashboard : System.Web.UI.Page
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
                    bindWODetails();
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
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

            //grdWODetails.DataSource = dt1;
            //grdWODetails.DataBind();
            //grdWODetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }


    }
}