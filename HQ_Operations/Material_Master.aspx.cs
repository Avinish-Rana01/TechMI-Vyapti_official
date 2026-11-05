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
    public partial class Material_Master : System.Web.UI.Page
    {

        #region class level field declarations

        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_MASTER";
        DataSet ds;
        public static int MATERIAL_ID = 0;

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
                    Bind_Category();
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
        }

        private void Bind_Category()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_CATEGORY_MASTER"));
                ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                DataTable dt = ds.Tables[0];

                ddlCategory.DataSource = dt;
                ddlCategory.DataBind();
                ddlCategory.DataTextField = "CAT_NAME";
                ddlCategory.DataValueField = "CAT_ID";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        private void Bind_ArticleCategory()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_ARTICLE_CATEGORY_MASTER"));
                ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                DataTable dt = ds.Tables[0];

                ddlCategory.DataSource = dt;
                ddlCategory.DataBind();
                ddlCategory.DataTextField = "CAT_NAME";
                ddlCategory.DataValueField = "CAT_ID";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));
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
                parameters.Add(new SqlParameter("@ACTION", "SHOW_MATERIAL_MASTER"));

                ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());

                grdMaterialMaster.DataSource = ds.Tables[0];
                grdMaterialMaster.DataBind();
                if (grdMaterialMaster.Rows.Count > 0)
                {
                    Div_grd.Visible = true;
                    grdMaterialMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        //protected void lnkEdit_Click(object sender, EventArgs e)
        //{
        //    btnSubmit.Text = "Update";

        //    LinkButton btn = (LinkButton)sender;
        //    GridViewRow row = (GridViewRow)btn.NamingContainer;

        //    hdnID.Value = row.Cells[1].Text;
        //    string mat_id = hdnID.Value;

        //    Session["MATERIAL_ID"] = mat_id;

        //    string catcode = row.Cells[2].Text;

        //    //string catname = row.Cells[3].Text;
        //    //Bind_ArticleCategory();
        //    ////ddlCategory.Items.FindByText(catename.ToString().Trim()).Selected = true;
        //    //ddlCategory.Items.FindByValue(catname).Selected = true;

        //    mat_id = Server.HtmlDecode(row.Cells[1].Text).Trim();

        //    Bind_ArticleCategory();

        //    ddlCategory.ClearSelection();

        //    ListItem item = ddlCategory.Items.FindByText(catid);

        //    if (item != null)
        //    {
        //        item.Selected = true;
        //    }

        //    //string catname = row.Cells[3].Text.Trim();

        //    //Bind_ArticleCategory();
        //    //ddlCategory.ClearSelection();

        //    //ListItem li1 = ddlCategory.Items
        //    //    .Cast<ListItem>()
        //    //    .FirstOrDefault(x =>
        //    //        x.Text.Trim().Equals(catname, StringComparison.OrdinalIgnoreCase)
        //    //    );

        //    //if (li1 != null)
        //    //{
        //    //    li1.Selected = true;
        //    //}

        //    //txtMaterialCode.Text = row.Cells[3].Text.Trim()=="&nbsp;" ? "": row.Cells[3].Text.Trim();
        //    txtArticle.Text = row.Cells[4].Text.Trim() == "&nbsp;" ? "" : row.Cells[4].Text.Trim();
        //    txtarticleDesc.Text = row.Cells[5].Text.Trim() == "&nbsp;" ? "" : row.Cells[5].Text.Trim();
        //    //ddlGender.SelectedValue = row.Cells[6].Text.Trim() == "&nbsp;" ? "" : row.Cells[6].Text.Trim();
        //    //ddlSize.SelectedValue = row.Cells[7].Text.Trim() == "&nbsp;" ? "" : row.Cells[7].Text.Trim();
        //    //txtFabric.Text = row.Cells[7].Text.Trim() == "&nbsp;" ? "" : row.Cells[7].Text.Trim();
        //    //txtMRP.Text = row.Cells[8].Text.Trim() == "&nbsp;" ? "" : row.Cells[8].Text.Trim();
        //    //txtEffectiveFromDate.Text = row.Cells[9].Text.Trim() == "&nbsp;" ? "" : row.Cells[9].Text.Trim();
        //    string status = row.Cells[6].Text.Trim() == "&nbsp;" ? "" : row.Cells[6].Text.Trim();
        //    ddlStatus.ClearSelection();
        //    ListItem li = ddlStatus.Items.FindByText(status);
        //    if (li != null)
        //    {
        //        li.Selected = true;
        //    }

        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSidebarEdit", "openSidebar('single');", true);
        //    if (grdMaterialMaster.Rows.Count > 0)
        //    {
        //        grdMaterialMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    }
        //}

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            btnSubmit.Text = "Update";

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Material ID
            hdnID.Value = Server.HtmlDecode(row.Cells[1].Text).Trim();

            string mat_id = hdnID.Value;

            Session["MATERIAL_ID"] = mat_id;

            // Category ID
            //string catid = Server.HtmlDecode(row.Cells[2].Text).Trim();
            string catid = grdMaterialMaster.DataKeys[row.RowIndex].Value.ToString();

            // Bind Category Dropdown
            Bind_ArticleCategory();

            // Select Category
            ddlCategory.ClearSelection();

            ListItem item = ddlCategory.Items.FindByValue(catid);

            if (item != null)
            {
                item.Selected = true;
            }

            // Article
            txtArticle.Text = Server.HtmlDecode(row.Cells[4].Text).Trim() == "&nbsp;" ? "" : Server.HtmlDecode(row.Cells[4].Text).Trim();

            // Description
            txtarticleDesc.Text = Server.HtmlDecode(row.Cells[5].Text).Trim() == "&nbsp;" ? "" : Server.HtmlDecode(row.Cells[5].Text).Trim();

            // Status
            string status = Server.HtmlDecode(row.Cells[6].Text).Trim();

            if (status == "&nbsp;")
                status = "";

            ddlStatus.ClearSelection();

            ListItem li = ddlStatus.Items.FindByText(status);

            if (li != null)
            {
                li.Selected = true;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSidebarEdit", "openSidebar('single');", true);

            if (grdMaterialMaster.Rows.Count > 0)
            {
                grdMaterialMaster.HeaderRow.TableSection =
                    TableRowSection.TableHeader;
            }
        }

        protected void lnlActive_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                id = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetUp_Para = new List<SqlParameter>();
                GetUp_Para.Add(new SqlParameter("@Action", "MATERIAL_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@MATERIAL_ID", id));
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
                GetUp_Para.Add(new SqlParameter("@Action", "MATERIAL_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@MATERIAL_ID", id));

                int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, GetUp_Para.ToArray());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully...!')", true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Status Changed To In-Active Successfully...!')", true);
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
                string date = "";
                //if (txtEffectiveFromDate.Text.Trim() != "")
                //{
                //    string dt1 = txtEffectiveFromDate.Text.Trim().ToString();
                //    DateTime dt = Convert.ToDateTime(txtEffectiveFromDate.Text.Trim().ToString());
                //    date = dt.ToString("yyyy-MM-dd");
                //}
                //else
                //{ }
                if (btnSubmit.Text == "Submit")
                {
                    List<SqlParameter> Ins_Para = new List<SqlParameter>();
                    Ins_Para.Add(new SqlParameter("@ACTION", "INSERT_MATERIAL_MASTER"));
                    Ins_Para.Add(new SqlParameter("@CAT_ID", ddlCategory.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@ARTICLE_NO", txtArticle.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@ARTICLE_DESCRIPTION", txtarticleDesc.Text.Trim()));
                    // Ins_Para.Add(new SqlParameter("@MATERIAL_CODE", txtMaterialCode.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@STATUS", ddlStatus.SelectedValue.Trim()));
                    // Ins_Para.Add(new SqlParameter("@GENDER", ddlGender.SelectedValue.Trim()));                    
                    //Ins_Para.Add(new SqlParameter("@FABRIC", txtFabric.Text.Trim()));
                    //Ins_Para.Add(new SqlParameter("@MRP", txtMRP.Text.Trim()));
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

                    MATERIAL_ID = 0;
                }
                else if (btnSubmit.Text == "Update")
                {

                    int mat_id = Convert.ToInt32(Session["MATERIAL_ID"]);
                    int userid = Convert.ToInt32(Session["USER_ID"]);

                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ACTION", "UPDATE_MATERIAL_MASTER"));
                    param.Add(new SqlParameter("@MATERIAL_ID", mat_id));
                    param.Add(new SqlParameter("@CAT_ID", ddlCategory.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@ARTICLE_NO", txtArticle.Text.Trim()));
                    param.Add(new SqlParameter("@ARTICLE_DESCRIPTION", txtarticleDesc.Text.Trim()));
                    // param.Add(new SqlParameter("@MATERIAL_CODE", txtMaterialCode.Text.Trim()));
                    param.Add(new SqlParameter("@STATUS", ddlStatus.SelectedValue.Trim()));
                    //param.Add(new SqlParameter("@GENDER", ddlGender.SelectedValue.Trim()));

                    // param.Add(new SqlParameter("@FABRIC", txtFabric.Text.Trim()));
                    //param.Add(new SqlParameter("@MRP", txtMRP.Text.Trim()));                    

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
            finally
            {
                bindGrid();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void grdMaterialMaster_RowDataBound(object sender, GridViewRowEventArgs e)
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
            txtArticle.Text = "";
            ddlCategory.SelectedIndex = -1;
            ddlStatus.SelectedIndex = -1;
            txtarticleDesc.Text = "";
            //txtMaterialCode.Text = "";
            //txtEffectiveFromDate.Text = "";
            //ddlGender.SelectedIndex = -1;
            //txtFabric.Text = "";
            //txtMRP.Text = "";
            ddlCategory.Focus();
            if (grdMaterialMaster.Rows.Count > 0)
            {
                grdMaterialMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            btnSubmit.Text = "Submit";
        }
        #endregion method for clearing fields
    }
}