using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.HQ_Operations
{
    public partial class Tag_Creation : System.Web.UI.Page
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
                    ChallanGrid();
                    finalGrid();
                    viewDetailsGrid();
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
                new DataColumn ("PONo", typeof(string)),
                new DataColumn ("PODate", typeof(string)),
                new DataColumn ("TotPOTagQty", typeof(string)),
                new DataColumn ("POTagVerifyQty", typeof(string)),
                new DataColumn ("POTagRemQty", typeof(string))
            });

            dt1.Rows.Add("Data", "Data", "Data", "Data", "Data");

            grdChallanDetail.DataSource = dt1;
            grdChallanDetail.DataBind();
            grdChallanDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        private void ChallanGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("PONo", typeof(string)),
                new DataColumn ("EPC_Count", typeof(string))
            });

            dt1.Rows.Add("Data", "Data");

            grdFileData.DataSource = dt1;
            grdFileData.DataBind();
            grdFileData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        private void finalGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("CreatedDate", typeof(string))
            });

            dt1.Rows.Add("Data");

            grdFinalChallan.DataSource = dt1;
            grdFinalChallan.DataBind();
            grdFinalChallan.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        private void viewDetailsGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("PONo", typeof(string)),
                new DataColumn ("PODate", typeof(string)),
                new DataColumn ("TotPOTagQty", typeof(string)),
                new DataColumn ("POTagVerifyQty", typeof(string)),
                new DataColumn ("POTagRemQty", typeof(string)),
            });

            dt1.Rows.Add("Data", "Data", "Data", "Data", "Data");

            grdViewDetails.DataSource = dt1;
            grdViewDetails.DataBind();
            grdViewDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void View(object sender, EventArgs e)
        {

            bindGrid();
            ChallanGrid();
            finalGrid();
            viewDetailsGrid();
            popup.Show();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            bindGrid();
            ChallanGrid();
            finalGrid();
        }
    }
}