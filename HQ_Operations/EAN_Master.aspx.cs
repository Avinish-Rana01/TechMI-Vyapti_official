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
    public partial class EAN_Master : System.Web.UI.Page
    {

        #region class level field declarations

        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_MASTER";
        DataSet ds;
        public static int EAN_ID = 0;

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
                    Bind_Material();
                    Bind_Category();
                    Bind_UOM();
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
                parameters.Add(new SqlParameter("@ACTION", "SHOW_EAN_MASTER"));

                ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());

                grdEANMaster.DataSource = ds.Tables[0];
                grdEANMaster.DataBind();
                if (grdEANMaster.Rows.Count > 0)
                {
                    Div_grd.Visible = true;
                    grdEANMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        private void Bind_Material()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                //GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_MATERIAL_MASTER"));
                GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_ARTICLE_NO"));
                ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                DataTable dt = ds.Tables[0];

                ddlMaterial.DataSource = dt;
                ddlMaterial.DataBind();
                //ddlMaterial.DataTextField = "MATERIAL_CODE";
                //ddlMaterial.DataValueField = "MATERIAL_ID";

                ddlMaterial.DataTextField = "ARTICLE_NO";
                ddlMaterial.DataValueField = "MATERIAL_ID";
                ddlMaterial.DataBind();
                ddlMaterial.Items.Insert(0, new ListItem("Select Article No", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            btnSubmit.Text = "Update";

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Grid column order:
            // [0] Sr.No  [1] EAN_ID(hidden)  [2] ARTICLE_NO  [3] ARTICLE_DESCRIPTION
            // [4] SIZE   [5] EAN              [6] CAT_CODE    [7] CAT_NAME
            // [8] UOM_NAME  [9] MRP  [10] EFFECTIVE_FROM_DATE  [11] STATUS

            hdnID.Value = row.Cells[1].Text;
            Session["EAN_ID"] = hdnID.Value;

            // Fetch the full EAN record from DB to get MATERIAL_ID
            //try
            //{
            //    List<SqlParameter> para = new List<SqlParameter>();
            //    para.Add(new SqlParameter("@ACTION", "GET_EAN_BY_ID"));
            //    para.Add(new SqlParameter("@EAN_ID", hdnID.Value));
            //    DataSet dsEAN = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, para.ToArray());

            //    if (dsEAN != null && dsEAN.Tables.Count > 0 && dsEAN.Tables[0].Rows.Count > 0)
            //    {
            //        DataRow dr = dsEAN.Tables[0].Rows[0];

            //        // Material — set by MATERIAL_ID value
            //        //ddlMaterial.ClearSelection();
            //        string materialId = dr["MATERIAL_ID"].ToString();
            //        if (ddlMaterial.Items.FindByValue(materialId) != null)
            //            ddlMaterial.SelectedValue = materialId;
            //    }
            //}
            //catch
            //{
            //    // Fallback: try matching ARTICLE_NO text in the dropdown
            //    string articleNo = row.Cells[2].Text.Trim();
            //    ddlMaterial.ClearSelection();
            //    ListItem li_M = ddlMaterial.Items.FindByText(articleNo);
            //    if (li_M != null) li_M.Selected = true;
            //}

            string articleNo = grdEANMaster.DataKeys[row.RowIndex].Values["MATERIAL_ID"].ToString(); 
            ddlMaterial.ClearSelection();
            ListItem li_M = ddlMaterial.Items.FindByValue(articleNo);
            if (li_M != null) li_M.Selected = true;

            // UOM — column index 8
            string uom = row.Cells[8].Text.Trim();
            ddlUOM.ClearSelection();
            ListItem li_UOM = ddlUOM.Items.FindByText(uom);
            if (li_UOM != null) li_UOM.Selected = true;

            // Category — column index 7 (CAT_NAME)
            string category = grdEANMaster.DataKeys[row.RowIndex].Values["CAT_ID"].ToString();
            Bind_Category();
            ddlCategory.ClearSelection();
            ListItem li_Cat = ddlCategory.Items.FindByValue(category);
            if (li_Cat != null) li_Cat.Selected = true;

            // Size — column index 4 (grid stores raw value: S, M, L)
            string sizeVal = row.Cells[4].Text.Trim();
            ddlSize.ClearSelection();
            if (sizeVal != "&nbsp;" && sizeVal != "")
            {
                ListItem li_Size = ddlSize.Items.FindByValue(sizeVal);
                if (li_Size != null) li_Size.Selected = true;
            }

            // EAN — column index 5
            txtEAN.Text = row.Cells[5].Text.Trim() == "&nbsp;" ? "" : row.Cells[5].Text.Trim();

            // MRP — column index 9
            txtMRP.Text = row.Cells[9].Text.Trim() == "&nbsp;" ? "" : row.Cells[9].Text.Trim();

            // Effective From Date — column index 10
            // TextMode="Date" requires value in yyyy-MM-dd format
            string effectiveFromRaw = row.Cells[10].Text.Trim();
            if (!string.IsNullOrEmpty(effectiveFromRaw) && effectiveFromRaw != "&nbsp;")
            {
                DateTime effDate;
                if (DateTime.TryParse(effectiveFromRaw, out effDate))
                {
                    txtEffective.Text = effDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtEffective.Text = effectiveFromRaw;
                }
            }
            else
            {
                txtEffective.Text = "";
            }

            // Status — column index 11
            string status = row.Cells[11].Text.Trim();
            ddlStatus.ClearSelection();
            ListItem li_Status = ddlStatus.Items.FindByText(status);
            if (li_Status != null) li_Status.Selected = true;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSidebarEdit", "openSidebar('single');", true);
            if (grdEANMaster.Rows.Count > 0)
            {
                grdEANMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string date = "";
                if (txtEffective.Text.Trim() != "")
                {
                    string dt1 = txtEffective.Text.Trim().ToString();
                    DateTime dt = Convert.ToDateTime(txtEffective.Text.Trim().ToString());
                    date = dt.ToString("yyyy-MM-dd");
                }
                else
                { }
                if (btnSubmit.Text == "Submit")
                {
                    List<SqlParameter> Ins_Para = new List<SqlParameter>();
                    Ins_Para.Add(new SqlParameter("@ACTION", "INSERT_EAN_MASTER"));
                    Ins_Para.Add(new SqlParameter("@MATERIAL_ID",ddlMaterial.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@EAN", txtEAN.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@CAT_ID", ddlCategory.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@UOM_ID", ddlUOM.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@STATUS", ddlStatus.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));
                    Ins_Para.Add(new SqlParameter("@SIZE", ddlSize.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@MRP", txtMRP.Text.Trim()));
                    if (date == "")
                    {
                        Ins_Para.Add(new SqlParameter("@EFFECTIVE_FROM_DATE", null));
                    }
                    else
                    {
                        Ins_Para.Add(new SqlParameter("@EFFECTIVE_FROM_DATE", Convert.ToDateTime(date.ToString())));
                    }
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


                    EAN_ID = 0;
                }
                else if (btnSubmit.Text == "Update")
                {

                    int eanid = Convert.ToInt32(Session["EAN_ID"]);
                    int userid = Convert.ToInt32(Session["USER_ID"]);

                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ACTION", "UPDATE_EAN_MASTER"));
                    param.Add(new SqlParameter("@EAN_ID", eanid));
                    param.Add(new SqlParameter("@MATERIAL_ID", ddlMaterial.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@EAN", txtEAN.Text.Trim()));
                    param.Add(new SqlParameter("@CAT_ID", ddlCategory.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@UOM_ID", ddlUOM.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@STATUS", ddlStatus.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@USER_ID", userid));
                    param.Add(new SqlParameter("@SIZE", ddlSize.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@MRP", txtMRP.Text.Trim()));
                    if (date == "")
                    {
                        param.Add(new SqlParameter("@EFFECTIVE_FROM_DATE", null));
                    }
                    else
                    {
                        param.Add(new SqlParameter("@EFFECTIVE_FROM_DATE", Convert.ToDateTime(date.ToString())));
                    }
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
                GetUp_Para.Add(new SqlParameter("@Action", "EAN_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@EAN_ID", id));
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
                GetUp_Para.Add(new SqlParameter("@Action", "EAN_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@EAN_ID", id));

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

        protected void grdEANMaster_RowDataBound(object sender, GridViewRowEventArgs e)
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

        #region method for clearing fields

        public void Clear()
        {
            txtEAN.Text = "";
            ddlMaterial.SelectedIndex = -1;
            ddlCategory.SelectedIndex = -1;
            ddlUOM.SelectedValue = "";
            ddlStatus.SelectedIndex = -1;
            ddlSize.SelectedIndex = -1;
            txtMRP.Text = "";
            txtEffective.Text = "";
            ddlMaterial.Focus();
            btnSubmit.Text = "Submit";
            if (grdEANMaster.Rows.Count > 0)
            {
                grdEANMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        #endregion method for clearing fields

        #region dropdown binding methods

        private void Bind_Category()
        {
            int articleno = 0;

            try
            {
                articleno = Convert.ToInt32(ddlMaterial.SelectedValue);
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_CATEGORY_MASTER"));
                GetDtl_Para.Add(new SqlParameter("@MATERIAL_ID", articleno));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                if (ds.Tables[0].Rows.Count > 0) 
                {
                    DataTable dt = ds.Tables[0];

                    ddlCategory.DataSource = dt;
                    ddlCategory.DataBind();
                    ddlCategory.DataTextField = "CAT_NAME";
                    ddlCategory.DataValueField = "CAT_ID";
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));

                    ddlCategory.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    
                }

                //DataTable dt = ds.Tables[0];

                //ddlCategory.DataSource = dt;
                //ddlCategory.DataBind();
                //ddlCategory.DataTextField = "CAT_NAME";
                //ddlCategory.DataValueField = "CAT_ID";
                //ddlCategory.DataBind();
                //ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }

        private void Bind_UOM()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_UOM_MASTER"));
                ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                DataTable dt = ds.Tables[0];

                ddlUOM.DataSource = dt;
                ddlUOM.DataBind();
                ddlUOM.DataTextField = "UOM_NAME";
                ddlUOM.DataValueField = "UOM_ID";
                ddlUOM.DataBind();
                ddlUOM.Items.Insert(0, new ListItem("Select UOM", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }

        #endregion

        protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_Category();
            bindGrid();


        }

        


    }
}
