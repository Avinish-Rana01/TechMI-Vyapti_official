using Patel.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.HQ_Operations
{
    public partial class View_Work_Order_Details : System.Web.UI.Page
    {
        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_HQ_OPERATION";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER_CONFIG_TYPE"] != null && Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null)
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
                parameters.Add(new SqlParameter("@ACTION", "VIEW_WO_DETAILS"));
                //parameters.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());
                grdWODetails.DataSource = ds.Tables[0];
                grdWODetails.DataBind();
                if (grdWODetails.Rows.Count > 0)
                {
                    grdWODetails.HeaderRow.TableSection = TableRowSection.TableHeader;                   
                }                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'Something Went Wrong Try Again...!')", true);
                return;
            }           
        }

       
        protected void View(object sender, EventArgs e)
        {
            bindGrid();
           // viewDetailsGrid();
            popup.Show();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            bindGrid();
        }
        protected void lnkWONo_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
                LinkButton lnk_rejection = ((LinkButton)row.FindControl("lnkWONo"));
                string max_no = lnk_rejection.Text.ToString().Trim();

                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "WO_VIEW_WO_LINE_ITEM"));
                GetDtl_Para.Add(new SqlParameter("@WO_NO", max_no));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                grdViewDetails.DataSource = ds.Tables[0];
                grdViewDetails.DataBind();
                if (grdViewDetails.Rows.Count > 0)
                {
                    lblWONumber.Text = ds.Tables[0].Rows[0]["WO_No"].ToString();
                    lblVendorName.Text = ds.Tables[0].Rows[0]["VENDOR_NAME"].ToString();
                    lblPODate.Text = ds.Tables[0].Rows[0]["WODt"].ToString();
                    grdViewDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

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
                if (grdWODetails.Rows.Count > 0)
                {
                    grdWODetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdViewDetails.Rows.Count > 0)
                {
                    grdViewDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
    }
}