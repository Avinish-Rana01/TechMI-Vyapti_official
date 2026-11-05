using Patel.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.HQ_Operations
{
    public partial class Vendor_Master : System.Web.UI.Page
    {
        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_MASTER";
        public static int VENDOR_ID = 0;
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
                    hdf_SAP_Value.Value = Session["USER_CONFIG_TYPE"].ToString().Trim();
                    if (Session["USER_CONFIG_TYPE"].ToString().Trim() == "Manual Entry")
                    {
                        rdbcheck.Visible = true;
                    }
                    else
                    {
                        rdbcheck.Visible = false;
                    }
                    bindGrid();
                    Bind_Vendor_Type();
                    Bind_State();
                    //ddlCity.Items.Insert(0, new ListItem("Select City", ""));
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
        }

        private void Bind_Vendor_Type()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_VENDOR_TYPE_MASTER"));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                DataTable dt = ds.Tables[0];

                ddlVendorType.DataSource = dt;
                ddlVendorType.DataBind();
                ddlVendorType.DataTextField = "VENDOR_TYPE";
                ddlVendorType.DataValueField = "VENDOR_TYPE_ID";
                ddlVendorType.DataBind();
                ddlVendorType.Items.Insert(0, new ListItem("Select Vendor Type", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }

        private void Bind_State()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "GET_STATE"));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                DataTable dt = ds.Tables[0];

                ddlState.DataSource = dt;
                ddlState.DataBind();
                ddlState.DataTextField = "STATE_NAME";
                ddlState.DataValueField = "STATE_ID";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select State", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }

        private void Bind_City()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "GET_CITY"));
                GetDtl_Para.Add(new SqlParameter("@STATE_ID", ddlState.SelectedValue));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                DataTable dt = ds.Tables[0];

                ddlCity.DataSource = dt;
                ddlCity.DataBind();
                ddlCity.DataTextField = "CITY_NAME";
                ddlCity.DataValueField = "CITY_ID";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("Select City", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        private void bindGrid()
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ACTION", "SHOW_VENDOR_MASTER"));

               DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());

                grdVendorMaster.DataSource = ds.Tables[0];
                grdVendorMaster.DataBind();
                if (grdVendorMaster.Rows.Count > 0)
                {
                    Div_grd.Visible = true;
                    grdVendorMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            btnSubmit.Text = "Update";
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            VENDOR_ID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            int rowIndex = row.RowIndex;
           
            string vendore_type_id = Convert.ToString(grdVendorMaster.DataKeys[rowIndex]["VENDOR_TYPE_ID"]);
            string state_id = Convert.ToString(grdVendorMaster.DataKeys[rowIndex]["STATE_ID"]);
            string city_id = Convert.ToString(grdVendorMaster.DataKeys[rowIndex]["CITY_ID"]);

            txtVendorCode.Text = row.Cells[1].Text.Replace("&nbsp;", "");
            txtVendorName.Text = row.Cells[2].Text.Replace("&nbsp;", "");
            //for vendor type dropdown binding
            string vendor_type = row.Cells[3].Text.Replace("&nbsp;", "");
            Bind_Vendor_Type();
            ddlVendorType.SelectedValue = vendore_type_id;

            txtVendorAddress.Text = row.Cells[4].Text.Replace("&nbsp;", "");
            txtVendorPan.Text = row.Cells[5].Text.Replace("&nbsp;", "");
            txtContactPersonName.Text = row.Cells[6].Text.Replace("&nbsp;", "");
            txtContactPersonNo.Text = row.Cells[7].Text.Replace("&nbsp;", "");
            txtVendorEmailID.Text= row.Cells[8].Text.Replace("&nbsp;", "");
            txtGSTBRN.Text = row.Cells[9].Text.Replace("&nbsp;", "");

            //for state dropdown binding
            string state = row.Cells[10].Text.Replace("&nbsp;", "");
            hdnState.Value = state;
            //ddlState.SelectedItem.Text = hdnState.Value;
            //ddlState.SelectedValue = state_id;

            hdnState.Value = state_id;
            hdnCity.Value = city_id;

            //for city dropdown binding
            string city = row.Cells[11].Text.Replace("&nbsp;", "");
            //Bind_City();
           // ddlCity.SelectedValue = city_id;

            txtPinCode.Text = row.Cells[12].Text.Replace("&nbsp;", "");
            txtUsername.Text = row.Cells[13].Text.Replace("&nbsp;", "");
            txtPassword.Text = row.Cells[14].Text.Replace("&nbsp;", "");
            txtConfirmPassword.Text = row.Cells[14].Text.Replace("&nbsp;", "");
            string chktag_value = row.Cells[15].Text.Replace("&nbsp;", "");
            chkIsTag.Checked = chktag_value == "YES";
            //chkIsTag.Checked = chktag_value == "YES" ? true:false ;
            
            //for status dropdown binding
            string status = row.Cells[16].Text.Trim().Replace("&nbsp;", "");
            ddlStatus.ClearSelection();
            ListItem li4 = ddlStatus.Items.FindByText(status);
            if (li4 != null)
            {
                li4.Selected = true;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSidebarEdit", "openSidebar('single');", true);
            if (grdVendorMaster.Rows.Count > 0)
            {
                grdVendorMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedIndex > 0)
            {
                Bind_City();
            }
            else
            {
                ddlCity.SelectedIndex = -1;
            }
            if (grdVendorMaster.Rows.Count > 0)
            {
                grdVendorMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        public void Clear()
        {
            txtVendorCode.Text = "";
            txtVendorName.Text = "";
            ddlVendorType.SelectedIndex = -1;
            txtVendorAddress.Text = "";
            txtVendorPan.Text = "";
            txtContactPersonName.Text = "";
            ddlStatus.SelectedValue = "";
            txtContactPersonNo.Text = "";
            txtVendorEmailID.Text = "";
            txtGSTBRN.Text = "";
           // ddlState.SelectedIndex = -1;
            //ddlCity.Items.Clear();
           // ddlCity.SelectedIndex = -1;
            txtPinCode.Text = "";
            VENDOR_ID = 0;
            txtPassword.Text = "";
            txtUsername.Text = "";
            txtConfirmPassword.Text = "";
            txtVendorCode.Focus();
            btnSubmit.Text = "Submit";
            chkIsTag.Checked = false;
            bindGrid();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (VENDOR_ID==0 || btnSubmit.Text.Trim() == "Submit")
                {
                    List<SqlParameter> Ins_Para = new List<SqlParameter>();
                    Ins_Para.Add(new SqlParameter("@ACTION", "INSERT_VENDOR_MASTER"));
                    Ins_Para.Add(new SqlParameter("@VENDOR_CODE", txtVendorCode.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@VENDOR_NAME", txtVendorName.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@VENDOR_TYPE_ID", ddlVendorType.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@ADDRESS", txtVendorAddress.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@PAN_NUMBER", txtVendorPan.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@CONTACT_PERSON_NAME", txtContactPersonName.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@CONTACT_PERSON_MOBILE_NO", txtContactPersonNo.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@EMAIL_ID", txtVendorEmailID.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@GST_NUMBER", txtGSTBRN.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@STATE_ID", ddlState.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@CITY_ID", hdnCity.Value.Trim()));
                    Ins_Para.Add(new SqlParameter("@PIN_CODE", txtPinCode.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@STATUS", ddlStatus.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@USER_NAME", txtUsername.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@PASSWORD", txtPassword.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                    Ins_Para.Add(new SqlParameter("@IS_TAG_FLAG", chkIsTag.Checked ? 1 : 0));
                    SqlParameter parm_Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 100);
                    parm_Msg.Direction = ParameterDirection.Output;
                    Ins_Para.Add(parm_Msg);

                    int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Ins_Para.ToArray());

                    string msg = parm_Msg.Value.ToString();
                    if (msg != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                        Clear();
                        rdbSingleEntry.Checked = false;
                        rdbMultipleEntry.Checked = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "resetForm", "resetValue();", true);
                    }
                    

                    VENDOR_ID = 0;
                }
                else 
                {
                    int userid = Convert.ToInt32(Session["USER_ID"]);
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ACTION", "UPDATE_VENDOR_MASTER"));
                    param.Add(new SqlParameter("@VENDOR_ID", VENDOR_ID));
                    param.Add(new SqlParameter("@VENDOR_CODE", txtVendorCode.Text.Trim()));
                    param.Add(new SqlParameter("@VENDOR_NAME", txtVendorName.Text.Trim()));
                    param.Add(new SqlParameter("@VENDOR_TYPE_ID", ddlVendorType.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@ADDRESS", txtVendorAddress.Text.Trim()));
                    param.Add(new SqlParameter("@PAN_NUMBER", txtVendorPan.Text.Trim()));
                    param.Add(new SqlParameter("@CONTACT_PERSON_NAME", txtContactPersonName.Text.Trim()));
                    param.Add(new SqlParameter("@CONTACT_PERSON_MOBILE_NO", txtContactPersonNo.Text.Trim()));
                    param.Add(new SqlParameter("@EMAIL_ID", txtVendorEmailID.Text.Trim()));
                    param.Add(new SqlParameter("@GST_NUMBER", txtGSTBRN.Text.Trim()));
                    param.Add(new SqlParameter("@STATE_ID", hdnState.Value.Trim()));
                    param.Add(new SqlParameter("@CITY_ID", hdnCity.Value.Trim()));
                    param.Add(new SqlParameter("@PIN_CODE", txtPinCode.Text.Trim()));
                    param.Add(new SqlParameter("@STATUS", ddlStatus.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@USER_NAME", txtUsername.Text.Trim()));
                    param.Add(new SqlParameter("@PASSWORD", txtPassword.Text.Trim()));
                    param.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                    param.Add(new SqlParameter("@IS_TAG_FLAG", chkIsTag.Checked ? 1 : 0));
                    SqlParameter parm_Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 100);
                    parm_Msg.Direction = ParameterDirection.Output;
                    param.Add(parm_Msg);

                    int rowsAffected = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, param.ToArray());

                    string msg = parm_Msg.Value.ToString();
                    if (msg != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                        Clear();
                        rdbSingleEntry.Checked = false;
                        rdbMultipleEntry.Checked = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "resetForm", "resetValue();", true);
                    }
                    btnSubmit.Text = "Submit";
                //}
                //else
                //{
                //    btnSubmit.Text = "Submit";
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('something went wrong try again...!')", true);
                //    return;

                }
            }
            catch (Exception ex)
            {
                btnSubmit.Text = "Submit";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            finally 
            { 
                bindGrid(); 
            }            
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void lnlActive_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                id = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetUp_Para = new List<SqlParameter>();
                GetUp_Para.Add(new SqlParameter("@Action", "VENDOR_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@VENDOR_ID", id));
                int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, GetUp_Para.ToArray());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Status Changed To Active Successfully...!')", true);
                Clear();
                bindGrid();
                id = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }

        protected void lnkInactive_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                id = Convert.ToString((sender as LinkButton).CommandArgument);
                GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
                List<SqlParameter> GetUp_Para = new List<SqlParameter>();
                GetUp_Para.Add(new SqlParameter("@Action", "VENDOR_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@VENDOR_ID", id));

                int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, GetUp_Para.ToArray());

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Status Changed To In-Active Successfully...!')", true);
                Clear();
                bindGrid();
                id = "";

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }

        protected void grdVendorMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton active = (LinkButton)e.Row.FindControl("lnlActive");
                LinkButton inactive = (LinkButton)e.Row.FindControl("lnkInactive");

                if (active == null || inactive == null)
                    return;

                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                if (status.Equals("Active", StringComparison.OrdinalIgnoreCase))
                {
                    active.Visible = false;
                }
                else
                {
                    inactive.Visible = false;
                }
            }
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<object> BindState()
        {
            string sqrcon = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
            string SP_NAME = "SP_MASTER";

            List<object> list = new List<object>();

                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@ACTION", "GET_STATE"));
                
                    DataSet ds = SqlHelper.ExecuteDataset(
                        sqrcon,
                        CommandType.StoredProcedure,
                        SP_NAME,
                        parameters.ToArray()
                    );

                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            list.Add(new
                            {
                                id = row["STATE_ID"].ToString(),
                                text = row["STATE_NAME"].ToString()
                            });
                        }
                    }
                }
                catch { }

                return list;
            }

        [WebMethod]
        public static List<object> BindCity(string state)
        {
            string sqrcon = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
            string SP_NAME = "SP_MASTER";

            List<object> list = new List<object>();
          
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ACTION", "GET_CITY"));
                parameters.Add(new SqlParameter("@STATE_ID", state));

                DataSet ds = SqlHelper.ExecuteDataset(
                    sqrcon,
                    CommandType.StoredProcedure,
                    SP_NAME,
                    parameters.ToArray()
                );

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        list.Add(new
                        {
                            id = row["CITY_ID"].ToString(),
                            text = row["CITY_NAME"].ToString()
                        });
                    }
                }
            }
            catch { }

            return list;
        }
    }
}