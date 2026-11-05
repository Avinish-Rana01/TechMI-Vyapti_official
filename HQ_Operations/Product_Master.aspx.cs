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
    public partial class Product_Master : System.Web.UI.Page
    {

        #region class level field declarations

        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_MASTER";
        DataSet ds;
        public static int PRODUCT_ID = 0;

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
                    //Bind_Brand();
                    Bind_HSNCode();
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
                parameters.Add(new SqlParameter("@ACTION", "SHOW_PRODUCT_MASTER"));

                ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());

                grdProductMaster.DataSource = ds.Tables[0];
                grdProductMaster.DataBind();
                if (grdProductMaster.Rows.Count > 0)
                {
                    grdProductMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
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

            string prodid = hdnID.Value;

            Session["PRODUCT_ID"] = prodid;

            txtProductCode.Text = row.Cells[2].Text;
            txtProductName.Text = row.Cells[3].Text;
            txtArticleNo.Text = row.Cells[4].Text;
            txtEAN.Text = row.Cells[5].Text;

            //for category dropdown binding
            string category = row.Cells[6].Text;
            ddlCategory.ClearSelection();
            ListItem li = ddlCategory.Items.FindByText(category);
            if (li != null)
            {
                li.Selected = true;
            }


            //for brand dropdown binding
            string brand = row.Cells[7].Text;
            ddlBrand.ClearSelection();
            ListItem li1 = ddlBrand.Items.FindByText(brand);
            if (li1 != null)
            {
                li1.Selected = true;
            }

            //for UOM dropdown binding
            string uom = row.Cells[8].Text;
            ddlUOM.ClearSelection();
            ListItem li2 = ddlUOM.Items.FindByText(uom);
            if (li2 != null)
            {
                li2.Selected = true;
            }

            //for HSN Code dropdown binding
            string hsncode = row.Cells[9].Text;
            ddlHSNCode.ClearSelection();
            ListItem li3 = ddlHSNCode.Items.FindByText(hsncode);
            if (li3 != null)
            {
                li3.Selected = true;
            }

            txtCostPrice.Text = row.Cells[10].Text;
            txtMRP.Text = row.Cells[11].Text;
            txtSellingPrice.Text = row.Cells[12].Text;

            //for status dropdown binding
            string status = row.Cells[13].Text.Trim();
            ddlStatus.ClearSelection();
            ListItem li4 = ddlStatus.Items.FindByText(status);
            if (li4 != null)
            {
                li4.Selected = true;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSidebarEdit", "openSidebar('single');", true);
            if (grdProductMaster.Rows.Count > 0)
            {
                grdProductMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSubmit.Text == "Submit")
                {
                    List<SqlParameter> Ins_Para = new List<SqlParameter>();
                    Ins_Para.Add(new SqlParameter("@ACTION", "INSERT_PRODUCT_MASTER"));
                    Ins_Para.Add(new SqlParameter("@PRODUCT_CODE", txtProductCode.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@PRODUCT_NAME", txtProductName.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@ARTICLE_NO", txtArticleNo.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@EAN", txtEAN.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@CAT_ID", ddlCategory.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@BRAND_ID", ddlBrand.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@UOM_ID", ddlUOM.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@HSN_ID", ddlHSNCode.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@COST_PRICE", txtCostPrice.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@MRP", txtMRP.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@SELLING_PRICE", txtSellingPrice.Text.Trim()));
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
                    }


                    PRODUCT_ID = 0;
                }
                else if (btnSubmit.Text == "Update")
                {

                    int proid = Convert.ToInt32(Session["PRODUCT_ID"]);
                    int userid = Convert.ToInt32(Session["USER_ID"]);

                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ACTION", "UPDATE_PRODUCT_MASTER"));
                    param.Add(new SqlParameter("@PRODUCT_ID", proid));
                    param.Add(new SqlParameter("@PRODUCT_CODE", txtProductCode.Text.Trim()));
                    param.Add(new SqlParameter("@PRODUCT_NAME", txtProductName.Text.Trim()));
                    param.Add(new SqlParameter("@ARTICLE_NO", txtArticleNo.Text.Trim()));
                    param.Add(new SqlParameter("@EAN", txtEAN.Text.Trim()));
                    param.Add(new SqlParameter("@CAT_ID", ddlCategory.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@BRAND_ID", ddlBrand.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@UOM_ID", ddlUOM.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@HSN_ID", ddlHSNCode.SelectedValue.Trim()));
                    param.Add(new SqlParameter("@COST_PRICE", Convert.ToDecimal(txtCostPrice.Text.Trim())));
                    param.Add(new SqlParameter("@MRP", Convert.ToDecimal(txtMRP.Text.Trim())));
                    param.Add(new SqlParameter("@SELLING_PRICE", Convert.ToDecimal(txtSellingPrice.Text.Trim())));
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
                GetUp_Para.Add(new SqlParameter("@Action", "PRODUCT_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@PRODUCT_ID", id));
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
                GetUp_Para.Add(new SqlParameter("@Action", "PRODUCT_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@PRODUCT_ID", id));

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

        protected void grdProductMaster_RowDataBound(object sender, GridViewRowEventArgs e)
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
            txtArticleNo.Text = "";
            txtCostPrice.Text = "";
            txtEAN.Text = "";
            txtMRP.Text = "";
            txtProductCode.Text = "";
            txtProductName.Text = "";
            txtSellingPrice.Text = "";
            ddlBrand.Items.Clear();
            ddlCategory.Items.Clear();
            ddlHSNCode.Items.Clear();
            ddlUOM.Items.Clear();
            ddlStatus.SelectedIndex = -1;
            if (grdProductMaster.Rows.Count > 0)
            {
                grdProductMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            Bind_Category();            
            Bind_HSNCode();
            Bind_UOM();
        }


        #endregion method for clearing fields


        #region dropdown binding methods

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


        private void Bind_Brand()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_BRAND_MASTER"));
                GetDtl_Para.Add(new SqlParameter("@CAT_ID", ddlCategory.SelectedValue));
                ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                DataTable dt = ds.Tables[0];

                ddlBrand.DataSource = dt;
                ddlBrand.DataBind();
                ddlBrand.DataTextField = "BRAND_NAME";
                ddlBrand.DataValueField = "BRAND_ID";
                ddlBrand.DataBind();
                ddlBrand.Items.Insert(0, new ListItem("Select Brand", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }


        private void Bind_HSNCode()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_HSN_MASTER"));
                ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                DataTable dt = ds.Tables[0];

                ddlHSNCode.DataSource = dt;
                ddlHSNCode.DataBind();
                ddlHSNCode.DataTextField = "HSN_CODE";
                ddlHSNCode.DataValueField = "HSN_ID";
                ddlHSNCode.DataBind();
                ddlHSNCode.Items.Insert(0, new ListItem("Select HSN Code", ""));
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

        #endregion dropdown binding methods

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedIndex > 0)
            {
                Bind_Brand();
            }
            if (grdProductMaster.Rows.Count > 0)
            {
                grdProductMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}