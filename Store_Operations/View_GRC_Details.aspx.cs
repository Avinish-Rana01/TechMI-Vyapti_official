using Patel.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.Store_Operations
{
    public partial class View_GRC_Details : System.Web.UI.Page
    {

        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_HU_DETAILS";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER_TYPE"] != null && Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null)
                {
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
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ACTION", "VIEW_GRC_DETAILS_STOREWISE"));
                parameters.Add(new SqlParameter("@STORE_ID", Session["STORE_ID"]));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());
                grdFinalHUGrid.DataSource = ds.Tables[0];
                grdFinalHUGrid.DataBind();
                if (grdFinalHUGrid.Rows.Count > 0)
                {
                    grdFinalHUGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.Replace("'", "\\'") + "-something went wrong try again...!')", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            if (grdFinalHUGrid.Rows.Count > 0)
            {
                grdFinalHUGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            //FillStore();
        }

        protected void lnkHU_No_Click(object sender, EventArgs e)
        {
            popup.Show();
            try
            {
                string _VAR_HU_NO = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "GRC_VIEW_HU_LINE_ITEM_DETAILS"));
                GetDtl_Para.Add(new SqlParameter("@HU_NO", _VAR_HU_NO.ToString().Trim()));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                DataTable dt = ds.Tables[0];
                grdViewDetails.DataSource = dt;
                grdViewDetails.DataBind();
                if (grdViewDetails.Rows.Count > 0)
                {
                    //grid_div.Visible = false;
                    grid_final_div.Visible = true;
                    lblHUNumber.Text = _VAR_HU_NO.ToString();
                    lblWarehouse.Text = dt.Rows[0]["WAREHOUSE_NAME"].ToString();
                    lblStore.Text = dt.Rows[0]["STORE_NAME"].ToString();
                    lblHUDate.Text = dt.Rows[0]["HU_Date"].ToString();
                    grdViewDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    //grid_div.Visible = false;
                    grid_final_div.Visible = false;
                    grdViewDetails.DataSource = null;
                    grdViewDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            finally
            {
                popup.Show();
                if (grdFinalHUGrid.Rows.Count > 0)
                {
                    grdFinalHUGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }


    }
}