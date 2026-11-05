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
    public partial class Location_Master : System.Web.UI.Page
    {

        #region class level field declarations

        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_MASTER";
        DataSet ds;
        public static int LOC_ID = 0;

        #endregion class level field declarations

        #region page load and grid bind method

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
                    Bind_State();
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
                parameters.Add(new SqlParameter("@ACTION", "SHOW_LOCATION_MASTER"));

                ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());

                grdLocationMaster.DataSource = ds.Tables[0];
                grdLocationMaster.DataBind();
                if (grdLocationMaster.Rows.Count > 0)
                {
                    grdLocationMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'Something Went Wrong Try Again...!')", true);
                return;
            }
        }

        #endregion page load and grid bind method

        #region button click and grid row data bound events

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            btnSubmit.Text = "Update";

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            hdnID.Value = row.Cells[1].Text;

            string locid = hdnID.Value;

            Session["LOC_ID"] = locid;

            //for location type dropdown binding
          
            string loctype = row.Cells[2].Text;
            ddlLocType.ClearSelection();
            ListItem li = ddlLocType.Items.FindByText(loctype);
            if (li != null)
            {
                li.Selected = true;
            }

            txtLocationCode.Text = row.Cells[3].Text;
            txtLocationName.Text = row.Cells[4].Text;

            //for state dropdown binding
            Bind_State();
            string state = row.Cells[5].Text;
            ddlState.ClearSelection();
            ListItem li1 = ddlState.Items.FindByText(state);
            if (li1 != null)
            {
                li1.Selected = true;
            }

            //for city dropdown binding
            Bind_City();
            string city = row.Cells[6].Text;
            ddlCity.ClearSelection();
            ListItem li2 = ddlCity.Items.FindByText(city);
            if (li2 != null)
            {
                li2.Selected = true;
            }

            //for status dropdown binding
            string status = row.Cells[7].Text.Trim();
            ddlStatus.ClearSelection();
            ListItem li3 = ddlStatus.Items.FindByText(status);
            if (li3 != null)
            {
                li3.Selected = true;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSidebarEdit", "openSidebar('single');", true);
            if (grdLocationMaster.Rows.Count > 0)
            {
                grdLocationMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSubmit.Text == "Submit")
                {

                    List<SqlParameter> Ins_Para = new List<SqlParameter>();
                    Ins_Para.Add(new SqlParameter("@ACTION", "INSERT_LOCATION_MASTER"));
                    Ins_Para.Add(new SqlParameter("@LOC_TYPE", ddlLocType.SelectedItem.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@LOC_CODE", txtLocationCode.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@LOC_NAME", txtLocationName.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@CITY_ID", ddlCity.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@STATE_ID", ddlState.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@STATUS", ddlStatus.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));

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
                    }

                    LOC_ID = 0;
                }
                else if (btnSubmit.Text == "Update")
                {

                    int locid = Convert.ToInt32(Session["LOC_ID"]);
                    int userid = Convert.ToInt32(Session["USER_ID"]);

                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ACTION", "UPDATE_LOCATION_MASTER"));
                    param.Add(new SqlParameter("@LOC_ID", locid));
                    param.Add(new SqlParameter("@LOC_TYPE", ddlLocType.SelectedItem.Text.Trim()));
                    param.Add(new SqlParameter("@LOC_CODE", txtLocationCode.Text.Trim()));
                    param.Add(new SqlParameter("@LOC_NAME", txtLocationName.Text.Trim()));
                    param.Add(new SqlParameter("@CITY_ID", ddlCity.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@STATE_ID", ddlState.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@STATUS", ddlStatus.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@USER_ID", userid));

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
                    }
                    btnSubmit.Text = "Submit";
                }
                else
                {
                    btnSubmit.Text = "Submit";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('something went wrong try again...!')", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                btnSubmit.Text = "Submit";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            bindGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {

        }

        protected void btnFileClear_Click(object sender, EventArgs e)
        {

        }

        protected void lnlActive_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                id = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetUp_Para = new List<SqlParameter>();
                GetUp_Para.Add(new SqlParameter("@Action", "LOCATION_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@LOC_ID", id));
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
                GetUp_Para.Add(new SqlParameter("@Action", "LOCATION_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@LOC_ID", id));

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

        protected void grdLocationMaster_RowDataBound(object sender, GridViewRowEventArgs e)
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

        #endregion button click and grid row data bound events

        #region method for clearing fields

        public void Clear()
        {
            txtLocationCode.Text = "";
            txtLocationName.Text = "";
            txtModLocCode.Text = "";
            txtModLocName.Text = "";
            ddlLocType.SelectedValue = "";
            ddlCity.SelectedValue = "";
            ddlState.SelectedValue = "";
            ddlStatus.SelectedValue = "";
        }


        #endregion method for clearing fields

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedIndex > 0)
            {
                Bind_City();
            }
            if (grdLocationMaster.Rows.Count > 0)
            {
                grdLocationMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}