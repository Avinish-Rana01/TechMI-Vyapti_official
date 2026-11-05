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
    public partial class HSN_Master : System.Web.UI.Page
    {

        #region class level field declarations

        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_MASTER";
        DataSet ds;
        public static int HSN_ID = 0;

        #endregion class level field declarations

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
                parameters.Add(new SqlParameter("@ACTION", "SHOW_HSN_MASTER"));

                ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());

                grdHSNMaster.DataSource = ds.Tables[0];
                grdHSNMaster.DataBind();
                if (grdHSNMaster.Rows.Count > 0)
                {
                    Div_grd.Visible = true;
                    grdHSNMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
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

            hdnID.Value = row.Cells[1].Text;

            string hsnid = hdnID.Value;

            Session["HSN_ID"] = hsnid;
            txtHSNCode.Text = row.Cells[2].Text;
            txtHSNDesc.Text = row.Cells[3].Text;
            txtGST.Text = row.Cells[4].Text;
            txtEffectiveDate.Text = row.Cells[5].Text;

            string status = row.Cells[6].Text.Trim();
            ddlStatus.ClearSelection();

            ListItem li = ddlStatus.Items.FindByText(status);
            if (li != null)
            {
                li.Selected = true;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSidebarEdit", "openSidebar('single');", true);
            if (grdHSNMaster.Rows.Count > 0)
            {
                grdHSNMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }


        }

        protected void lnlActive_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                id = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetUp_Para = new List<SqlParameter>();
                GetUp_Para.Add(new SqlParameter("@Action", "HSN_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@HSN_ID", id));
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
                GetUp_Para.Add(new SqlParameter("@Action", "HSN_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@HSN_ID", id));

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

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {

        }

        protected void btnFileClear_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSubmit.Text == "Submit")
                {

                    List<SqlParameter> Ins_Para = new List<SqlParameter>();
                    Ins_Para.Add(new SqlParameter("@ACTION", "INSERT_HSN_MASTER"));
                    Ins_Para.Add(new SqlParameter("@HSN_CODE", txtHSNCode.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@HSN_DESCRIPTION", txtHSNDesc.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@GST", txtGST.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@EFFECTIVE_DATE", txtEffectiveDate.Text.Trim()));
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


                    HSN_ID = 0;
                }
                else if (btnSubmit.Text == "Update")
                {

                    int hsnid = Convert.ToInt32(Session["HSN_ID"]);
                    int userid = Convert.ToInt32(Session["USER_ID"]);

                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ACTION", "UPDATE_HSN_MASTER"));
                    param.Add(new SqlParameter("@HSN_ID", hsnid));
                    param.Add(new SqlParameter("@HSN_CODE", txtHSNCode.Text.Trim()));
                    param.Add(new SqlParameter("@HSN_DESCRIPTION", txtHSNDesc.Text.Trim()));
                    param.Add(new SqlParameter("@GST", txtGST.Text.Trim()));
                    param.Add(new SqlParameter("@EFFECTIVE_DATE", txtEffectiveDate.Text.Trim()));
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
            finally {
                bindGrid();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        //#region method for clearing fields

        public void Clear()
        {
            txtHSNCode.Text = "";
            txtHSNDesc.Text = "";
            txtGST.Text = "";
            txtEffectiveDate.Text = "";
            ddlStatus.SelectedIndex = -1;
            btnSubmit.Text = "Submit";
            bindGrid();
        }


        //#endregion method for clearing fields

        protected void grdHSNMaster_RowDataBound(object sender, GridViewRowEventArgs e)
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


    }
}
