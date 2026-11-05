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
    public partial class Work_Order_Creation : System.Web.UI.Page
    {
        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_HQ_OPERATION";
        //DataSet ds;
        public static int WO_ID = 0;
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
                    Bind_Vendor();
                    Bind_WO_Line_Item();
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
                grdFinalPOGrid.DataSource = null;
                grdFinalPOGrid.DataBind();
                grdFinalPOGrid.Visible = false;
                grdAddPO.Visible = true;
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ACTION", "WO_GET_ADD_LINE_ITEM"));
                parameters.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());
                grdAddPO.DataSource = ds.Tables[0];
                grdAddPO.DataBind();
                if (grdAddPO.Rows.Count > 0)
                {
                    grdAddPO.HeaderRow.TableSection = TableRowSection.TableHeader;
                    ddlVendorName.Enabled = false;
                    txtPODate.ReadOnly = true;
                    btnCreateWO.Visible = true;
                    btnClear.Visible = false;
                    grid_div.Visible = true;
                    grid_final_div.Visible = false;
                    chkTagSupply.Enabled = false;
                }
                else
                {
                    ddlVendorName.Enabled = true;
                    txtPODate.ReadOnly = false;
                    btnCreateWO.Visible = false;
                    btnClear.Visible = true;
                    grid_div.Visible = false;
                    grid_final_div.Visible = false;
                    chkTagSupply.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'Something Went Wrong Try Again...!')", true);
                return;
            }
        }
        private void Bind_Vendor()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "GET_WO_USER_VENDOR_DATA"));
                GetDtl_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                DataTable dt = ds.Tables[0];
                ddlVendorName.DataSource = dt;
                ddlVendorName.DataBind();
                ddlVendorName.DataTextField = "VENDOR_NAME";
                ddlVendorName.DataValueField = "VENDOR_ID";
                ddlVendorName.DataBind();
                ddlVendorName.Items.Insert(0, new ListItem("Select Vendor", ""));
                if (ds.Tables[1].Rows.Count > 0)
                {
                    ddlVendorName.Items.FindByValue(ds.Tables[1].Rows[0]["VENDOR_ID"].ToString()).Selected = true;
                }
                txtPODate.Text = ds.Tables[1].Rows[0]["WO_DATE"].ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        private void Bind_WO_Line_Item()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "GET_MATERIAL_LINE_ITEM"));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                DataTable dt = ds.Tables[0];
                ddlLineItem.DataSource = dt;
                ddlLineItem.DataBind();
                ddlLineItem.DataTextField = "MATERIAL_LINE_ITEM";
                ddlLineItem.DataValueField = "MATERIAL_ID";
                ddlLineItem.DataBind();
                ddlLineItem.Items.Insert(0, new ListItem("Select WO Line Item", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        private void finalGrid(string MAX_WO_NO)
        {
            try
            {
                grdFinalPOGrid.Visible = true;
                grdAddPO.Visible = false;
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ACTION", "WO_GET_COMPLETED_DETAILS"));
                parameters.Add(new SqlParameter("@WO_NO", MAX_WO_NO.Trim().ToString()));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());
                grdFinalPOGrid.DataSource = ds.Tables[0];
                grdFinalPOGrid.DataBind();
                if (grdFinalPOGrid.Rows.Count > 0)
                {
                    grdFinalPOGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grdFinalPOGrid.Visible = true;
                    grdAddPO.Visible = false;
                    grid_div.Visible = false;
                    grid_final_div.Visible = true;
                }
                else
                {
                    ddlVendorName.Enabled = true;
                    txtPODate.ReadOnly = false;
                    grid_div.Visible = false;
                    grid_final_div.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'Something Went Wrong Try Again...!')", true);
                return;
            }
        }
        private void viewDetailsGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("LineItem", typeof(string)),
                new DataColumn ("WOQty", typeof(string)),
                new DataColumn ("TagSupply", typeof(string)),
            });

            dt1.Rows.Add("ABC", "1", "YES");

            grdViewDetails.DataSource = dt1;
            grdViewDetails.DataBind();
            grdViewDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void View(object sender, EventArgs e)
        {
            bindGrid();
            finalGrid("");
            viewDetailsGrid();
            popup.Show();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string date = "";
                if (txtPODate.Text.Trim() != "")
                {
                    string dt1 = txtPODate.Text.Trim().ToString();
                    DateTime dt = Convert.ToDateTime(txtPODate.Text.Trim().ToString());
                    date = dt.ToString("yyyy-MM-dd");
                }
                else
                { }
                List<SqlParameter> Ins_Para = new List<SqlParameter>();
                Ins_Para.Add(new SqlParameter("@ACTION", "WO_Add_Line_Item"));
                Ins_Para.Add(new SqlParameter("@VENDOR_ID", ddlVendorName.SelectedValue.Trim()));                
                if (date == "")
                {
                    Ins_Para.Add(new SqlParameter("@TAG_PO_DATE", null));
                }
                else
                {
                    Ins_Para.Add(new SqlParameter("@TAG_PO_DATE", Convert.ToDateTime(date.ToString())));
                }
                //Ins_Para.Add(new SqlParameter("@EAN_ID", ddlLineItem.SelectedValue.Trim()));
                Ins_Para.Add(new SqlParameter("@EAN_ID", ddlEAN.SelectedValue.Trim()));
                Ins_Para.Add(new SqlParameter("@WO_LINE_ITEM_QTY", txtWOQty.Text.Trim()));
                Ins_Para.Add(new SqlParameter("@WO_IS_BARCODE", chkTagSupply.Checked ? 1 : 0));
                Ins_Para.Add(new SqlParameter("@MATERIAL_ID", ddlLineItem.SelectedValue));
                Ins_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                Ins_Para.Add(new SqlParameter("@EAN", ddlEAN.SelectedItem.Text.Trim()));
                SqlParameter parm_Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 100);
                parm_Msg.Direction = ParameterDirection.Output;
                Ins_Para.Add(parm_Msg);

                SqlParameter parm_Code = new SqlParameter("@CODE", SqlDbType.NVarChar, 100);
                parm_Code.Direction = ParameterDirection.Output;
                Ins_Para.Add(parm_Code);

                int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Ins_Para.ToArray());
                string msg = parm_Msg.Value.ToString();
                string code = parm_Code.Value.ToString();
                if (msg != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                    ddlLineItem.SelectedIndex = -1;
                    txtWOQty.Text = "";
                    ddlEAN.Items.Clear();
                    ddlEAN.Items.Insert(0, new ListItem("Select EAN", ""));

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            finally
            {
                bindGrid();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            bindGrid();
            //grid_final_div.Visible = true;
            if (grdFinalPOGrid.Rows.Count > 0)
            {
                grdFinalPOGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            //finalGrid("");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            grid_div.Visible = false;
            grdAddPO.Visible = false;
            grdFinalPOGrid.Visible = false;
            btnCreateWO.Visible = false;
            grdFinalPOGrid.Visible = false;
            ddlVendorName.SelectedIndex = -1;
            ddlLineItem.SelectedIndex = -1;
            txtPODate.Text = "";
            txtWOQty.Text = "";
            ddlEAN.SelectedIndex = -1;

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                id = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetDel_Para = new List<SqlParameter>();
                GetDel_Para.Add(new SqlParameter("@Action", "WO_DELETE_ADD_LINE_ITEM"));
                GetDel_Para.Add(new SqlParameter("@WO_DTL_ID", id));
                GetDel_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, GetDel_Para.ToArray());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Delete Successfully...!')", true);
                bindGrid();
                id = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }

        protected void btnCreateWO_Click(object sender, EventArgs e)
        {
            string MAX_WO_NO = "";
            try
            {               
                List<SqlParameter> Ins_Para = new List<SqlParameter>();
                Ins_Para.Add(new SqlParameter("@ACTION", "WO_SUBMIT_WO_GENERATION"));               
                Ins_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                Ins_Para.Add(new SqlParameter("@WO_IS_BARCODE", chkTagSupply.Checked ? 1 : 0));

                SqlParameter parm_Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 100);
                parm_Msg.Direction = ParameterDirection.Output;
                Ins_Para.Add(parm_Msg);

                SqlParameter parm_Code = new SqlParameter("@CODE", SqlDbType.NVarChar, 100);
                parm_Code.Direction = ParameterDirection.Output;
                Ins_Para.Add(parm_Code);

                SqlParameter parm_MAX = new SqlParameter("@MaxWoNo", SqlDbType.NVarChar, 100);
                parm_MAX.Direction = ParameterDirection.Output;
                Ins_Para.Add(parm_MAX);

                int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Ins_Para.ToArray());
                string msg = parm_Msg.Value.ToString();
                string code = parm_Code.Value.ToString();
                MAX_WO_NO = parm_MAX.Value.ToString();
                if (msg != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                    ddlLineItem.SelectedIndex = -1;
                    txtWOQty.Text = "";
                    ddlVendorName.SelectedIndex = -1;
                    txtPODate.Text = "";
                    //chkTagSupply.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            finally
            {
                bindGrid();
                finalGrid(MAX_WO_NO);
            }
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
                if (grdFinalPOGrid.Rows.Count > 0)
                {
                    grdFinalPOGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void ddlLineItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlEAN.Items.Clear();
                ddlEAN.Items.Insert(0, new ListItem("Select EAN", ""));

                if (ddlLineItem.SelectedIndex <= 0)
                {
                    return;
                }

                int materialId = Convert.ToInt32(ddlLineItem.SelectedValue);

                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(
                    new SqlParameter("@ACTION", "GET_EAN_BY_MATERIAL")
                );

                parameters.Add(
                    new SqlParameter("@MATERIAL_ID", materialId)
                );

                DataSet ds = SqlHelper.ExecuteDataset(
                    connstr,
                    CommandType.StoredProcedure,
                    Sp_Name,
                    parameters.ToArray()
                );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEAN.DataTextField = "EAN";
                    ddlEAN.DataValueField = "EAN_ID";

                    ddlEAN.DataSource = ds.Tables[0];
                    ddlEAN.DataBind();

                    ddlEAN.Items.Insert(
                        0,
                        new ListItem("Select EAN", "")
                    );
                }

                bindGrid();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.ToString() + "')",
                    true
                );
            }
        }

    }
}