using Patel.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.Warehouse_Management_System
{
    public partial class View_Receiving_Details : System.Web.UI.Page
    {
        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_WMS";
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
                parameters.Add(new SqlParameter("@ACTION", "SHOW_RECEIVING_DETAILS"));
                //parameters.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
                if (grdDetails.Rows.Count > 0)
                {
                    grdDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Div_grd.Visible = true;
                }
                else 
                {
                    Div_grd.Visible = false;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'Something Went Wrong Try Again...!')", true);
                return;
            }
        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            bindGrid();            
        }



        protected void lnkASN_No_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["USER_CONFIG_TYPE"] != null && Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null)
                {
                    GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
                    LinkButton lnkASN_No = (LinkButton)row.FindControl("lnkASN_No");

                    string ASN_no = lnkASN_No.CommandArgument.ToString().Trim();

                    List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                    GetDtl_Para.Add(new SqlParameter("@ACTION", "VIEW_RECEIVING_DETAILS"));
                    GetDtl_Para.Add(new SqlParameter("@ASN_NO", ASN_no));

                    DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                    grdPODetails.DataSource = null;
                    grdPODetails.DataBind();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        lblASNNumber.Text = dt.Rows[0]["ASN_NUMBER"].ToString();
                        lblVendorName.Text = dt.Rows[0]["VENDOR_NAME"].ToString();
                        lblASNDate.Text = dt.Rows[0]["ASN_DATE"].ToString();

                        if (dt.Columns.Contains("Total_Receive_Qty"))
                        {
                            lbl_total_received_qty.Text = dt.Rows[0]["Total_Receive_Qty"].ToString();
                        }
                        else
                        {
                            lbl_total_received_qty.Text = "0";
                        }

                        grdPODetails.DataSource = dt;
                        grdPODetails.DataBind();

                        if (grdPODetails.Rows.Count > 0)
                        {
                            grdPODetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    }
                    else
                    {
                        lblASNNumber.Text = ASN_no;
                        lblVendorName.Text = "";
                        lblASNDate.Text = "";
                        lbl_total_received_qty.Text = "0";
                    }
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.Replace("'", "\\'") + " - something went wrong try again...!');", true);
            }
            finally
            {
                popup.Show();

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
    }
}