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
    public partial class Store_Registration : System.Web.UI.Page
    {
        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_MASTER1";
        string Employee_SP_Name = "[SP_EMPLOYEE_MASTER]";
        public static int STORE_ID = 0;
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
                    Bind_State();
                    bindGrid();
                    FillStoreManager();
                    FillAreaManager();
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
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
                parameters.Add(new SqlParameter("@ACTION", "SHOW_STORE_MASTER"));

                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());

                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
                if (grdDetails.Rows.Count > 0)
                {
                    grdDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
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

            STORE_ID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            int rowIndex = row.RowIndex;

            //string warehouse_type_id = Convert.ToString(grdWarehouseMaster.DataKeys[rowIndex]["WAREHOUSE_TYPE_ID"]);
            string state_id = Convert.ToString(grdDetails.DataKeys[rowIndex]["STATE_ID"]);
            string city_id = Convert.ToString(grdDetails.DataKeys[rowIndex]["CITY_ID"]);

            string storeManagerId = Convert.ToString(grdDetails.DataKeys[rowIndex]["STORE_MANAGER_ID"]);
            string areaManagerId = Convert.ToString(grdDetails.DataKeys[rowIndex]["AREA_MANAGER_ID"]);

            txtStoreCode.Text = row.Cells[1].Text.Replace("&nbsp;", "");
            txtStoreName.Text = row.Cells[2].Text.Replace("&nbsp;", "");
            

            txtStoreAddress.Text = row.Cells[3].Text.Replace("&nbsp;", "");
            //txtStorePan.Text = row.Cells[4].Text.Replace("&nbsp;", "");
            txtContactPersonName.Text = row.Cells[4].Text.Replace("&nbsp;", "");
            txtContactPersonNo.Text = row.Cells[5].Text.Replace("&nbsp;", "");
            txtStoreEmailID.Text = row.Cells[6].Text.Replace("&nbsp;", "");
            txtGSTBRN.Text = row.Cells[9].Text.Replace("&nbsp;", "");

            // =====================================================
            // STORE MANAGER
            // =====================================================

            FillStoreManager();

            ddlStoreManager.ClearSelection();

            ListItem storeManagerItem = ddlStoreManager.Items.FindByValue(storeManagerId);
            if (storeManagerItem != null)
            {
                storeManagerItem.Selected = true;
            }


            // =====================================================
            // AREA MANAGER
            // =====================================================

            FillAreaManager();

            ddlAreaManager.ClearSelection();

            ListItem areaManagerItem = ddlAreaManager.Items.FindByValue(areaManagerId);


            if (areaManagerItem != null)
            {
                areaManagerItem.Selected = true;
            }


            //for STATE dropdown binding
            string state = row.Cells[10].Text.Replace("&nbsp;", "");
            hdnState.Value = state;
            
            hdnState.Value = state_id;
            hdnCity.Value = city_id;

            //for CITY dropdown binding
            string city = row.Cells[11].Text.Replace("&nbsp;", "");
            //Bind_City();
            //ddlCity.SelectedValue = city_id;

            txtPinCode.Text = row.Cells[12].Text.Replace("&nbsp;", "");
            //for status dropdown binding
            string status = row.Cells[13].Text.Trim().Replace("&nbsp;", "");
            ddlStatus.ClearSelection();
            ListItem li4 = ddlStatus.Items.FindByText(status);
            if (li4 != null)
            {
                li4.Selected = true;
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSidebarEdit", "openSidebar('single');", true);
            if (grdDetails.Rows.Count > 0)
            {
                grdDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (STORE_ID == 0)
                {
                    List<SqlParameter> Ins_Para = new List<SqlParameter>();
                    Ins_Para.Add(new SqlParameter("@ACTION", "INSERT_STORE_MASTER"));
                    Ins_Para.Add(new SqlParameter("@STORE_CODE", txtStoreCode.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@STORE_NAME", txtStoreName.Text.Trim()));                   
                    Ins_Para.Add(new SqlParameter("@ADDRESS", txtStoreAddress.Text.Trim()));
                    //Ins_Para.Add(new SqlParameter("@PAN_NUMBER", txtStorePan.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@CONTACT_PERSON_NAME", txtContactPersonName.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@CONTACT_PERSON_MOBILE_NO", txtContactPersonNo.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@EMAIL_ID", txtStoreEmailID.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@GST_NUMBER", txtGSTBRN.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@STATE_ID", ddlState.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@CITY_ID", hdnCity.Value.Trim()));
                    Ins_Para.Add(new SqlParameter("@PIN_CODE", txtPinCode.Text.Trim()));

                    Ins_Para.Add(new SqlParameter("@STORE_MANAGER_ID", ddlStoreManager.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@AREA_MANAGER_ID", ddlAreaManager.SelectedValue.Trim()));

                    Ins_Para.Add(new SqlParameter("@STATUS", ddlStatus.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
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

                    STORE_ID = 0;
                }
                else
                {

                    int userid = Convert.ToInt32(Session["USER_ID"]);
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ACTION", "UPDATE_STORE_MASTER"));
                    param.Add(new SqlParameter("@STORE_ID", STORE_ID));
                    param.Add(new SqlParameter("@STORE_CODE", txtStoreCode.Text.Trim()));
                    param.Add(new SqlParameter("@STORE_NAME", txtStoreName.Text.Trim()));
                    param.Add(new SqlParameter("@ADDRESS", txtStoreAddress.Text.Trim()));
                    //param.Add(new SqlParameter("@PAN_NUMBER", txtStorePan.Text.Trim()));
                    param.Add(new SqlParameter("@CONTACT_PERSON_NAME", txtContactPersonName.Text.Trim()));
                    param.Add(new SqlParameter("@CONTACT_PERSON_MOBILE_NO", txtContactPersonNo.Text.Trim()));
                    param.Add(new SqlParameter("@EMAIL_ID", txtStoreEmailID.Text.Trim()));
                    param.Add(new SqlParameter("@GST_NUMBER", txtGSTBRN.Text.Trim()));
                    param.Add(new SqlParameter("@STATE_ID", hdnState.Value.Trim()));
                    param.Add(new SqlParameter("@CITY_ID", hdnCity.Value.Trim()));
                    param.Add(new SqlParameter("@PIN_CODE", txtPinCode.Text.Trim()));
                    param.Add(new SqlParameter("@STATUS", ddlStatus.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@STORE_MANAGER_ID", ddlStoreManager.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@AREA_MANAGER_ID", ddlAreaManager.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
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
            if (grdDetails.Rows.Count > 0)
            {
                grdDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        public void Clear()
        {
            txtStoreCode.Text = "";
            txtStoreName.Text = "";
            txtStoreAddress.Text = "";
            //txtStorePan.Text = "";
            txtContactPersonName.Text = "";
            ddlStatus.SelectedValue = "";
            txtContactPersonNo.Text = "";
            txtStoreEmailID.Text = "";
            txtGSTBRN.Text = "";
            ddlState.SelectedIndex = -1;
            ddlCity.SelectedIndex = -1;
            txtPinCode.Text = "";
            STORE_ID = 0;
            btnSubmit.Text = "Submit";
            txtStoreCode.Focus();
            //bindGrid();
        }

        protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void lnlActive_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                id = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetUp_Para = new List<SqlParameter>();
                GetUp_Para.Add(new SqlParameter("@Action", "STORE_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@STORE_ID", id));
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
                GetUp_Para.Add(new SqlParameter("@Action", "STORE_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@STORE_ID", id));

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

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedIndex > 0)
            {
                Bind_City();
            }
            else
            {
                ddlCity.SelectedIndex = 0;
            }
            if (grdDetails.Rows.Count > 0)
            {
                grdDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
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




        private void FillStoreManager()
        {
            try
            {
                List<SqlParameter> Para = new List<SqlParameter>();

                Para.Add(
                    new SqlParameter("@Action", "GET_STORE_MANAGERS"));

                DataSet ds = SqlHelper.ExecuteDataset(
                    connstr,
                    CommandType.StoredProcedure,
                    Employee_SP_Name,
                    Para.ToArray());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlStoreManager.DataSource = ds.Tables[0];
                    ddlStoreManager.DataTextField = "EMP_NAME";
                    ddlStoreManager.DataValueField = "EMP_ID";
                    ddlStoreManager.DataBind();

                    ddlStoreManager.Items.Insert(
                        0,
                        new ListItem("Select Store Manager", ""));
                }
                else
                {
                    ddlStoreManager.Items.Clear();

                    ddlStoreManager.Items.Insert(
                        0,
                        new ListItem("Select Store Manager", ""));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "')",
                    true);
            }
        }

        private void FillAreaManager()
        {
            try
            {
                List<SqlParameter> Para = new List<SqlParameter>();

                Para.Add(
                    new SqlParameter("@Action", "GET_AREA_MANAGERS"));

                DataSet ds = SqlHelper.ExecuteDataset(
                    connstr,
                    CommandType.StoredProcedure,
                    Employee_SP_Name,
                    Para.ToArray());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAreaManager.DataSource = ds.Tables[0];
                    ddlAreaManager.DataTextField = "EMP_NAME";
                    ddlAreaManager.DataValueField = "EMP_ID";
                    ddlAreaManager.DataBind();

                    ddlAreaManager.Items.Insert(
                        0,
                        new ListItem("Select Area Manager", ""));
                }
                else
                {
                    ddlAreaManager.Items.Clear();

                    ddlAreaManager.Items.Insert(
                        0,
                        new ListItem("Select Area Manager", ""));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "')",
                    true);
            }
        }
    }
}