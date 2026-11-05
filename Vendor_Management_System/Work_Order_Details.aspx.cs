using Patel.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.Vendor_Management_System
{
    public partial class Work_Order_Details : System.Web.UI.Page
    {
        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_VMS";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER_TYPE"] != null &&
                    Session["MenuText"] != null &&
                    Session["UserName"] != null &&
                    Session["User_ID"] != null)
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
                parameters.Add(new SqlParameter("@ACTION", "VMM_PO_VIEW_WO_LINE_ITEM"));
                parameters.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));

                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());

                grdDetails.DataSource = null;
                grdDetails.DataBind();

                if (ds != null &&
                    ds.Tables.Count > 0 &&
                    ds.Tables[0].Rows.Count > 0)
                {
                    grdDetails.DataSource = ds.Tables[0];
                    grdDetails.DataBind();

                    Div_grd.Visible = true;

                    if (grdDetails.Rows.Count > 0)
                    {
                        grdDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                else
                {
                    Div_grd.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Div_grd.Visible = false;

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "')",
                    true);
            }
        }

        protected void lnkPONo_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
                LinkButton lnkPONo = (LinkButton)row.FindControl("lnkPONo");

                string poNo = lnkPONo.CommandArgument.ToString().Trim();

                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "PO_VIEW_PO_LINE_ITEM"));
                GetDtl_Para.Add(new SqlParameter("@PO_NO", poNo));
                GetDtl_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));

                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                grdPODetails.DataSource = null;
                grdPODetails.DataBind();

                if (ds != null &&
                    ds.Tables.Count > 0 &&
                    ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    lblPONumber.Text = dt.Rows[0]["PO_NO"].ToString();
                    lblVendorName.Text = dt.Rows[0]["VENDOR_NAME"].ToString();
                    lblPODate.Text = dt.Rows[0]["WODt"].ToString();

                    grdPODetails.DataSource = dt;
                    grdPODetails.DataBind();

                    if (grdPODetails.Rows.Count > 0)
                    {
                        grdPODetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    popup.Show();
                }
                else
                {
                    lblPONumber.Text = poNo;
                    lblVendorName.Text = "";
                    lblPODate.Text = "";

                    popup.Show();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "-something went wrong try again...!')",
                    true);
            }
            finally
            {
                if (grdDetails.Rows.Count > 0)
                {
                    grdDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grdPODetails.Rows.Count > 0)
                {
                    grdPODetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            popup.Hide();
            bindGrid();

        }
    }
}