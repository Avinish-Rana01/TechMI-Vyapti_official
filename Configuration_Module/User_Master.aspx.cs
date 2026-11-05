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

namespace Markss_Retail_Suite.Configuration_Module
{
    public partial class User_Master : System.Web.UI.Page
    {
        string ConnStr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        string SP_Name = "[SP_MASTER1]";
        string Employee_SP_Name = "[SP_EMPLOYEE_MASTER]";
        public static int USER_ID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER_CONFIG_TYPE"] != null && Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null)
                {
                    //lblMenuName.Text = Session["MenuText"].ToString();
                    FillEmployee();
                    FillRole();
                    FillDepartment();
                    FillDesignation();
                    BindGrid();

                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
        }
        private void BindGrid()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "SHOW_USER_MASTER"));
                DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, SP_Name, GetDtl_Para.ToArray());
                grdUserMaster.DataSource = ds.Tables[0].DefaultView;
                grdUserMaster.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        private void FillRole()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_ROLE_MASTER"));
                DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, SP_Name, GetDtl_Para.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlRole.DataSource = ds.Tables[0].DefaultView;
                    // ddlRole.DataBind();
                    ddlRole.DataTextField = "ROLE_NAME";
                    ddlRole.DataValueField = "ROLE_ID";
                    ddlRole.DataBind();
                    ddlRole.Items.Insert(0, new ListItem("Select Role", ""));
                }
                else
                {
                    //ddlRole.Items.Clear();
                    ddlRole.DataSource = ds.Tables[0];
                    ddlRole.DataTextField = "ROLE_NAME";
                    ddlRole.DataValueField = "ROLE_ID";
                    ddlRole.DataBind();
                    ddlRole.Items.Insert(0, new ListItem("Select Role", ""));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        private void FillDepartment()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_DEPARTMENT_MASTER"));
                DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, SP_Name, GetDtl_Para.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepartment.DataSource = ds.Tables[0].DefaultView;
                    // ddlDepartment.DataBind();
                    ddlDepartment.DataTextField = "DEPT_NAME";
                    ddlDepartment.DataValueField = "DEPT_ID";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));
                }
                //else
                //{
                //    ddlDepartment.Items.Clear();
                //}
                else
                {
                    ddlDepartment.DataSource = ds.Tables[0];
                    ddlDepartment.DataTextField = "DEPT_NAME";
                    ddlDepartment.DataValueField = "DEPT_ID";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }

        private void FillDesignation()
        {
            try
            {
                List<SqlParameter> GetDtl_Para =
                    new List<SqlParameter>();

                GetDtl_Para.Add(new SqlParameter("@ACTION", "DD_DESIGNATION_MASTER"));

                DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, SP_Name, GetDtl_Para.ToArray());


                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDesignation.DataSource = ds.Tables[0].DefaultView;
                    // ddlDepartment.DataBind();
                    ddlDesignation.DataTextField = "DESIGNATION_NAME";
                    ddlDesignation.DataValueField = "DESIGNATION_ID";
                    ddlDesignation.DataBind();
                    ddlDesignation.Items.Insert(0, new ListItem("Select Designation", ""));
                }
                else
                {
                    ddlDesignation.DataSource = ds.Tables[0];
                    ddlDesignation.DataTextField = "DESIGNATION_NAME";
                    ddlDesignation.DataValueField = "DESIGNATION_ID";
                    ddlDesignation.DataBind();
                    ddlDesignation.Items.Insert(0, new ListItem("Select Designation", ""));
                }


                //ddlDesignation.Items.Clear();

                //ddlDesignation.DataSource =
                //    ds.Tables[0];

                //ddlDesignation.DataTextField =
                //    "DESIGNATION_NAME";

                //ddlDesignation.DataValueField =
                //    "DESIGNATION_ID";

                //ddlDesignation.DataBind();

                //ddlDesignation.Items.Insert(
                //    0,
                //    new ListItem(
                //        "Select Designation",
                //        ""
                //    )
                //);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "-something went wrong try again...!')", true);
            }
        }
        private void popupGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("ModuleName", typeof(string)),
                new DataColumn ("Menu", typeof(string)),
                new DataColumn ("SubMenu", typeof(string)),

            });

            dt1.Rows.Add("HQ Operations", "Master Data Management", "Brand Master, Category Master");

            grdViewModules.DataSource = dt1;
            grdViewModules.DataBind();
            grdViewModules.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
       
        public void Clear()
        {
            USER_ID = 0;
            btnSubmit.Text = "Submit";

            //ddlEmployeeName.SelectedIndex = 0;

            ddlEmployeeName.ClearSelection();

            if (ddlEmployeeName.Items.Count > 0)
            {
                ddlEmployeeName.Items[0].Selected = true;
            }

            dvStoreWarehouse.Visible = false;
            //ddlStoreWarehouse.SelectedIndex = 0;
            if (ddlStoreWarehouse.Items.Count > 0)
            {
                ddlStoreWarehouse.Items[0].Selected = true;
            }
            ddlStoreWarehouse.ClearSelection();
            ddlRole.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;

            ddlRole.Enabled = false;
            ddlDepartment.Enabled = false;
            ddlDesignation.Enabled = false;

            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConfirmPass.Text = "";

            rblHQConfig.ClearSelection();

            chkAllMain.Checked = false;
            chkConfiguration.Checked = false;
            chkHQ.Checked = false;
            chkWMS.Checked = false;
            chkStore.Checked = false;
            chkReports.Checked = false;

            chkMDM.Checked = false;
            chkProcurement.Checked = false;
            chkWDM.Checked = false;
            chkInbOP.Checked = false;
            chkInvCon.Checked = false;
            chkOutOP.Checked = false;
            chkInterMov.Checked = false;
            chkRevLog.Checked = false;

            chkMDMSub.ClearSelection();
            chkProcurementSub.ClearSelection();
            chkWDMSub.ClearSelection();
            chkInbOPSub.ClearSelection();
            chkInvConSub.ClearSelection();
            chkOutOPSub.ClearSelection();
            chkInterMovSub.ClearSelection();
            chkRevLogSub.ClearSelection();

            pnlHQ.Visible = false;
            pnlWMS.Visible = false;
            pnlStore.Visible = false;
            pnlReports.Visible = false;

            ScriptManager.RegisterStartupScript(
                Page,
                Page.GetType(),
                "ClearUserFields",
                "$(\"input[id*='txtUsername'], input[id*='txtPassword'], input[id*='txtConfirmPass']\").val('');" +
                "$(\"select[id*='ddlEmployeeName'], select[id*='ddlRole'], select[id*='ddlDepartment'], select[id*='ddlDesignation'], select[id*='ddlStatus']\").prop('selectedIndex', 0);",
                true
            );
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string var_module = "";
                string var_menu = "";
                string var_sub_menu = "";

                if (string.IsNullOrEmpty(ddlEmployeeName.SelectedValue))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Employee')", true);
                    return;
                }

                int employeeId = Convert.ToInt32(ddlEmployeeName.SelectedValue);

                int roleId = string.IsNullOrEmpty(ddlRole.SelectedValue) ? 0 : Convert.ToInt32(ddlRole.SelectedValue);
                int storeId = 0;
                int warehouseId = 0;

                if (roleId == 7) // STORE USER
                {
                    if (string.IsNullOrEmpty(ddlStoreWarehouse.SelectedValue))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Store')", true);
                        return;
                    }

                    storeId = Convert.ToInt32(ddlStoreWarehouse.SelectedValue);
                }
                else if (roleId == 8) // WAREHOUSE USER
                {
                    if (string.IsNullOrEmpty(ddlStoreWarehouse.SelectedValue))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Warehouse')", true);
                        return;
                    }

                    warehouseId = Convert.ToInt32(ddlStoreWarehouse.SelectedValue);
                }

                if (chkConfiguration.Checked)
                    var_module += chkConfiguration.Text + ",";

                if (chkHQ.Checked)
                    var_module += chkHQ.Text + ",";

                if (chkWMS.Checked)
                    var_module += chkWMS.Text + ",";

                if (chkStore.Checked)
                    var_module += chkStore.Text + ",";

                if (chkReports.Checked)
                    var_module += chkReports.Text + ",";

                if (chkMDM.Checked)
                    var_menu += chkMDM.Text + ",";

                if (chkProcurement.Checked)
                    var_menu += chkProcurement.Text + ",";

                if (chkWDM.Checked)
                    var_menu += chkWDM.Text + ",";

                if (chkInbOP.Checked)
                    var_menu += chkInbOP.Text + ",";

                if (chkInvCon.Checked)
                    var_menu += chkInvCon.Text + ",";

                if (chkOutOP.Checked)
                    var_menu += chkOutOP.Text + ",";

                if (chkInterMov.Checked)
                    var_menu += chkInterMov.Text + ",";

                if (chkRevLog.Checked)
                    var_menu += chkRevLog.Text + ",";

                if (chkInwOp.Checked)
                    var_menu += chkInwOp.Text + ",";

                if (chkWHInwOp.Checked)
                    var_menu += chkWHInwOp.Text + ",";

                if (chkOutwardOps.Checked)
                    var_menu += chkOutwardOps.Text + ",";

                var allSelected = chkMDMSub.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value)
                    .Concat(chkProcurementSub.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value))
                    .Concat(chkWDMSub.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value))
                    .Concat(chkWDMSub.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value))
                    .Concat(chkInvConSub.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value))
                    .Concat(chkOutOPSub.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value))
                    .Concat(chkInterMovSub.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value))
                    .Concat(chkRevLogSub.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value))
                    .Concat(chkInwOpSub.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value))
                    .Concat(chkWHInwOpSub.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value))
                    .Concat(chkOutwardOpsSub.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));

                var_sub_menu = string.Join(",", allSelected);

                string user_config_type = string.Join(",", rblHQConfig.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));

                if (USER_ID == 0)
                {
                    List<SqlParameter> Ins_Para = new List<SqlParameter>();

                    Ins_Para.Add(new SqlParameter("@ACTION", "INSERT_USER_MASTER"));
                    Ins_Para.Add(new SqlParameter("@EMPLOYEE_ID", employeeId));
                    Ins_Para.Add(new SqlParameter("@USER_NAME", txtUsername.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@PASSWORD", txtPassword.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@USER_MODULE_NAME", var_module.TrimEnd(',')));
                    Ins_Para.Add(new SqlParameter("@USER_MENU_NAME", var_menu.TrimEnd(',')));
                    Ins_Para.Add(new SqlParameter("@USER_SUB_MENU_NAME", var_sub_menu.TrimEnd(',')));
                    Ins_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));
                    Ins_Para.Add(new SqlParameter("@USER_CONFIG_TYPE", user_config_type.Trim()));
                    Ins_Para.Add(new SqlParameter("@STORE_ID", storeId));
                    Ins_Para.Add(new SqlParameter("@WAREHOUSE_ID", warehouseId));
                    //if (lblStore.Visible == true)
                    //{
                    //    Ins_Para.Add(new SqlParameter("@STORE_ID", ddlStoreWarehouse.SelectedValue));
                    //}
                    //else if (lblWarehouse.Visible == true)
                    //{
                    //    Ins_Para.Add(new SqlParameter("@WAREHOUSE_ID", ddlStoreWarehouse.SelectedValue));
                    //}

                    SqlParameter Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 500);
                    Msg.Direction = ParameterDirection.Output;
                    Ins_Para.Add(Msg);

                    SqlParameter parm = new SqlParameter("@USER_RETURN", SqlDbType.NVarChar);
                    parm.Direction = ParameterDirection.ReturnValue;
                    Ins_Para.Add(parm);

                    SqlHelper.ExecuteNonQuery(ConnStr, CommandType.StoredProcedure, SP_Name, Ins_Para.ToArray());

                    string VarMsg = Convert.ToString(Msg.Value);
                    string VarCode = Convert.ToString(parm.Value);

                    if (VarCode == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + VarMsg + "')", true);
                        chkAllMain_CheckedChanged(sender, e);
                        Clear();
                        rdbSingleEntry.Checked = false;
                        chkAllMain.Checked = false;

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + VarMsg + "')", true);
                        txtUsername.Text = "";
                        return;
                    }
                }

                else
                {
                    List<SqlParameter> Update_Para = new List<SqlParameter>();

                    Update_Para.Add(new SqlParameter("@ACTION", "UPDATE_USER_MASTER"));
                    Update_Para.Add(new SqlParameter("@EMPLOYEE_ID", employeeId));

                    //int roleId = string.IsNullOrEmpty(ddlRole.SelectedValue) ? 0 : Convert.ToInt32(ddlRole.SelectedValue);
                    int departmentId = string.IsNullOrEmpty(ddlDepartment.SelectedValue) ? 0 : Convert.ToInt32(ddlDepartment.SelectedValue);
                    int designationId = string.IsNullOrEmpty(ddlDesignation.SelectedValue) ? 0 : Convert.ToInt32(ddlDesignation.SelectedValue);
                    int status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? 0 : Convert.ToInt32(ddlStatus.SelectedValue);

                    Update_Para.Add(new SqlParameter("@ROLE_ID", roleId));
                    Update_Para.Add(new SqlParameter("@DEPT_ID", departmentId));
                    Update_Para.Add(new SqlParameter("@DESIGNATION_ID", designationId));
                    Update_Para.Add(new SqlParameter("@DESIGNATION", ddlDesignation.SelectedItem != null ? ddlDesignation.SelectedItem.Text.Trim() : ""));
                    Update_Para.Add(new SqlParameter("@USER_NAME", txtUsername.Text.Trim()));
                    Update_Para.Add(new SqlParameter("@PASSWORD", txtPassword.Text.Trim()));
                    Update_Para.Add(new SqlParameter("@STATUS", status));
                    Update_Para.Add(new SqlParameter("@USER_MODULE_NAME", var_module.TrimEnd(',')));
                    Update_Para.Add(new SqlParameter("@USER_MENU_NAME", var_menu.TrimEnd(',')));
                    Update_Para.Add(new SqlParameter("@USER_SUB_MENU_NAME", var_sub_menu.TrimEnd(',')));
                    Update_Para.Add(new SqlParameter("@USER_CONFIG_TYPE", user_config_type.Trim()));
                    Update_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));
                    Update_Para.Add(new SqlParameter("@User_Reg_ID", USER_ID));
                    Update_Para.Add(new SqlParameter("@STORE_ID", storeId));
                    Update_Para.Add(new SqlParameter("@WAREHOUSE_ID", warehouseId));

                    //if (lblStore.Visible == true)
                    //{
                    //    Update_Para.Add(new SqlParameter("@STORE_ID", ddlStoreWarehouse.SelectedValue));
                    //}
                    //else if (lblWarehouse.Visible == true)
                    //{
                    //    Update_Para.Add(new SqlParameter("@WAREHOUSE_ID", ddlStoreWarehouse.SelectedValue));
                    //}

                    SqlParameter Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
                    Update_Para.Add(Msg);

                    SqlHelper.ExecuteNonQuery(ConnStr, CommandType.StoredProcedure, SP_Name, Update_Para.ToArray());

                    string VarMsg = Convert.ToString(Msg.Value);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + VarMsg.Replace("'", "\\'") + "')", true);

                    chkAllMain_CheckedChanged(sender, e);
                    Clear();
                    rdbSingleEntry.Checked = false;
                    chkAllMain.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.Replace("'", "\\'") + "-something went wrong try again...!')", true);
                return;
            }
            finally
            {
                BindGrid();

                if (grdUserMaster.Rows.Count != 0)
                    grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            BindGrid();

        }


        protected void View(object sender, EventArgs e)
        {
            popup.Show();
            popupGrid();
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            Clear();
            rdbSingleEntry.Checked = false;
            BindGrid();
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                btnSubmit.Text = "Update";

                LinkButton lnkEdit = sender as LinkButton;

                if (lnkEdit == null)
                {
                    return;
                }

                USER_ID = Convert.ToInt32(lnkEdit.CommandArgument);

                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();

                GetDtl_Para.Add(new SqlParameter("@ACTION", "GET_USER_CONFIG_DETAILS"));

                GetDtl_Para.Add(new SqlParameter("@USER_ID", USER_ID));

                DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, SP_Name, GetDtl_Para.ToArray());

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                DataRow dr = ds.Tables[0].Rows[0];

                // =========================================================
                // EMPLOYEE
                // =========================================================

                string employeeId = dr["EMPLOYEE_ID"] == DBNull.Value ? "" : dr["EMPLOYEE_ID"].ToString().Trim();

                ddlEmployeeName.ClearSelection();

                if (!string.IsNullOrEmpty(employeeId))
                {
                    ListItem employeeItem = ddlEmployeeName.Items.FindByValue(employeeId);

                    if (employeeItem != null)
                    {
                        employeeItem.Selected = true;
                    }
                }

                // =========================================================
                // ROLE
                // =========================================================

                string roleId = dr["ROLE_ID"] == DBNull.Value ? "" : dr["ROLE_ID"].ToString().Trim();

                ddlRole.ClearSelection();

                if (!string.IsNullOrEmpty(roleId))
                {
                    ListItem roleItem = ddlRole.Items.FindByValue(roleId);

                    if (roleItem != null)
                    {
                        roleItem.Selected = true;
                    }
                }

                string storeId = dr["STORE_ID"] == DBNull.Value ? "" : dr["STORE_ID"].ToString().Trim();

                string warehouseId = dr["WAREHOUSE_ID"] == DBNull.Value ? "" : dr["WAREHOUSE_ID"].ToString().Trim();

                if (roleId == "7")
                {
                    dvStoreWarehouse.Visible = true;
                    lblStore.Visible = true;
                    lblWarehouse.Visible = false;

                    BindStore();

                    if (!string.IsNullOrEmpty(storeId) &&
                        ddlStoreWarehouse.Items.FindByValue(storeId) != null)
                    {
                        ddlStoreWarehouse.SelectedValue = storeId;
                    }
                }
                else if (roleId == "8")
                {
                    dvStoreWarehouse.Visible = true;
                    lblStore.Visible = false;
                    lblWarehouse.Visible = true;

                    BindWarehouse();

                    if (!string.IsNullOrEmpty(warehouseId) &&
                        ddlStoreWarehouse.Items.FindByValue(warehouseId) != null)
                    {
                        ddlStoreWarehouse.SelectedValue = warehouseId;
                    }
                }
                else
                {
                    dvStoreWarehouse.Visible = false;
                    lblStore.Visible = false;
                    lblWarehouse.Visible = false;
                    ddlStoreWarehouse.Items.Clear();
                }

                // =========================================================
                // DEPARTMENT
                // =========================================================

                string departmentId = dr["DEPARTMENT_ID"] == DBNull.Value ? "" : dr["DEPARTMENT_ID"].ToString().Trim();

                ddlDepartment.ClearSelection();

                if (!string.IsNullOrEmpty(departmentId))
                {
                    ListItem departmentItem = ddlDepartment.Items.FindByValue(departmentId);

                    if (departmentItem != null)
                    {
                        departmentItem.Selected = true;
                    }
                }

                // =========================================================
                // DESIGNATION
                // =========================================================

                string designationId = dr["DESIGNATION_ID"] == DBNull.Value ? "" : dr["DESIGNATION_ID"].ToString().Trim();

                ddlDesignation.ClearSelection();

                if (!string.IsNullOrEmpty(designationId))
                {
                    ListItem designationItem = ddlDesignation.Items.FindByValue(designationId);

                    if (designationItem != null)
                    {
                        designationItem.Selected = true;
                    }
                }

                // =========================================================
                // USERNAME
                // =========================================================

                txtUsername.Text = dr["USER_NAME"] == DBNull.Value ? "" : dr["USER_NAME"].ToString().Trim();

                // =========================================================
                // PASSWORD
                // =========================================================

                string password = "";

                if (dr.Table.Columns.Contains("PASSWORD") && dr["PASSWORD"] != DBNull.Value)
                {
                    password = Convert.ToString(dr["PASSWORD"]);
                }

                // Password
                txtPassword.Text = password;
                txtPassword.Attributes["value"] = password;

                // Confirm Password
                txtConfirmPass.Text = password;
                txtConfirmPass.Attributes["value"] = password;

                // =========================================================
                // STATUS
                // =========================================================

                string status = dr["STATUS"] == DBNull.Value ? "" : dr["STATUS"].ToString().Trim();

                ddlStatus.ClearSelection();

                if (status == "1")
                {
                    ddlStatus.SelectedValue = "1";
                }
                else if (status == "0")
                {
                    ddlStatus.SelectedValue = "0";
                }
                else
                {
                    ddlStatus.SelectedValue = "";
                }

                // =========================================================
                // USER CONFIG TYPE
                // =========================================================

                string userConfigType = dr["USER_CONFIG_TYPE"] == DBNull.Value ? "" : dr["USER_CONFIG_TYPE"].ToString().Trim();

                if (!string.IsNullOrEmpty(userConfigType))
                {
                    ListItem configItem =
                        rblHQConfig.Items.FindByValue(userConfigType);

                    if (configItem != null)
                    {
                        rblHQConfig.ClearSelection();
                        configItem.Selected = true;
                    }
                }

                // =========================================================
                // RESET MAIN MODULE CHECKBOXES
                // =========================================================

                chkConfiguration.Checked = false;
                chkHQ.Checked = false;
                chkWMS.Checked = false;
                chkStore.Checked = false;
                chkReports.Checked = false;
                chkAllMain.Checked = false;

                pnlHQ.Visible = false;
                pnlWMS.Visible = false;
                pnlStore.Visible = false;
                pnlReports.Visible = false;

                // =========================================================
                // MODULES
                // =========================================================

                string moduleNames = dr["MODULE_NAMES"] == DBNull.Value ? "" : dr["MODULE_NAMES"].ToString().Trim();

                if (!string.IsNullOrEmpty(moduleNames))
                {
                    string[] selectedModules = moduleNames.Split(',');

                    foreach (string module in selectedModules)
                    {
                        string moduleName = module.Trim();

                        if (chkConfiguration.Text.Trim().Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkConfiguration.Checked = true;
                        }

                        if (chkHQ.Text.Trim().Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkHQ.Checked = true;
                            pnlHQ.Visible = true;
                        }

                        if (chkWMS.Text.Trim().Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkWMS.Checked = true;
                            pnlWMS.Visible = true;
                        }

                        if (chkStore.Text.Trim().Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkStore.Checked = true;
                            pnlStore.Visible = true;
                        }

                        if (chkReports.Text.Trim().Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkReports.Checked = true;
                            pnlReports.Visible = true;
                        }
                    }
                }

                // =========================================================
                // SELECT ALL MODULE
                // =========================================================

                chkAllMain.Checked = chkConfiguration.Checked && chkHQ.Checked && chkWMS.Checked && chkStore.Checked && chkReports.Checked;

                // =========================================================
                // RESET MAIN MENUS
                // =========================================================

                chkMDM.Checked = false;
                chkProcurement.Checked = false;
                chkWDM.Checked = false;
                chkInbOP.Checked = false;
                chkInvCon.Checked = false;
                chkOutOP.Checked = false;
                chkInterMov.Checked = false;
                chkRevLog.Checked = false;
                chkInwOp.Checked = false;
                chkWHInwOp.Checked = false;
                chkOutwardOps.Checked = false;
                chkAntiTheft.Checked = false;

                // =========================================================
                // MENUS
                // =========================================================

                string menuNames = dr["MENU_NAMES"] == DBNull.Value ? "" : dr["MENU_NAMES"].ToString().Trim();

                if (!string.IsNullOrEmpty(menuNames))
                {
                    string[] selectedMenus = menuNames.Split(',');

                    foreach (string menu in selectedMenus)
                    {
                        string menuName = menu.Trim();

                        if (chkMDM.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkMDM.Checked = true;
                        }

                        if (chkProcurement.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkProcurement.Checked = true;
                        }

                        if (chkWDM.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkWDM.Checked = true;
                        }

                        if (chkInbOP.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkInbOP.Checked = true;
                        }

                        if (chkInvCon.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkInvCon.Checked = true;
                        }

                        if (chkOutOP.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkOutOP.Checked = true;
                        }

                        if (chkInterMov.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkInterMov.Checked = true;
                        }

                        if (chkRevLog.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkRevLog.Checked = true;
                        }

                        if (chkInwOp.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkInwOp.Checked = true;
                        }

                        if (chkWHInwOp.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkWHInwOp.Checked = true;
                        }

                        if (chkOutwardOps.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkOutwardOps.Checked = true;
                        }

                        if (chkAntiTheft.Text.Trim().Equals(menuName, StringComparison.OrdinalIgnoreCase))
                        {
                            chkAntiTheft.Checked = true;
                        }

                    }
                }

                // =========================================================
                // RESET ALL SUB MENUS
                // =========================================================

                foreach (ListItem item in chkMDMSub.Items)
                    item.Selected = false;

                foreach (ListItem item in chkProcurementSub.Items)
                    item.Selected = false;

                foreach (ListItem item in chkWDMSub.Items)
                    item.Selected = false;

                foreach (ListItem item in chkInbOPSub.Items)
                    item.Selected = false;

                foreach (ListItem item in chkInvConSub.Items)
                    item.Selected = false;

                foreach (ListItem item in chkOutOPSub.Items)
                    item.Selected = false;

                foreach (ListItem item in chkInterMovSub.Items)
                    item.Selected = false;

                foreach (ListItem item in chkRevLogSub.Items)
                    item.Selected = false;

                foreach (ListItem item in chkInwOpSub.Items)
                    item.Selected = false;

                foreach (ListItem item in chkWHInwOpSub.Items)
                    item.Selected = false;

                foreach (ListItem item in chkOutwardOpsSub.Items)
                    item.Selected = false;

                foreach (ListItem item in chkAntiTheftSub.Items)
                    item.Selected = false;

                // =========================================================
                // SUB MENUS
                // =========================================================

                string subMenuNames = dr["SUB_MENU_NAMES"] == DBNull.Value ? "" : dr["SUB_MENU_NAMES"].ToString().Trim();

                if (!string.IsNullOrEmpty(subMenuNames))
                {
                    string[] selectedSubMenus = subMenuNames.Split(',');

                    foreach (ListItem item in chkMDMSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                    foreach (ListItem item in chkProcurementSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                    foreach (ListItem item in chkWDMSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                    foreach (ListItem item in chkInbOPSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                    foreach (ListItem item in chkInvConSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                    foreach (ListItem item in chkOutOPSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                    foreach (ListItem item in chkInterMovSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                    foreach (ListItem item in chkRevLogSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                    foreach (ListItem item in chkInwOpSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                    foreach (ListItem item in chkWHInwOpSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                    foreach (ListItem item in chkOutwardOpsSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                    foreach (ListItem item in chkAntiTheftSub.Items)
                    {
                        item.Selected = selectedSubMenus.Any(x => x.Trim().Equals(item.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.Replace("'", "\\'") + "-something went wrong try again...!')", true);

                return;
            }
            finally
            {
                btnSubmit.Text = "Update";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSidebarEdit", "openSidebar('single');", true);

                if (grdUserMaster.Rows.Count > 0)
                {
                    grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }

        }


        private void UpdateSelectAllMain()
        {
            chkAllMain.Checked = new[]
            {
                 chkConfiguration, chkHQ, chkWMS, chkStore, chkReports
                //chkConfiguration, chkHQ, chkVMS, chkWMS, chkStore, chkReports
            }
            //.Where(c => c.Visible)
            .All(c => c.Checked);
        }

        private void ToggleSubModules(CheckBoxList list, bool value)
        {

            if (list == null)
                return;

            foreach (ListItem item in list.Items)
            {
                item.Selected = value;
            }
        }


        protected void chkHQ_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedIndex > 0)
            {
                pnlHQ.Visible = chkHQ.Checked;
                if (!chkHQ.Checked) ClearPanelSelections(pnlHQ);
                UpdateSelectAllMain();
            }
            else
            {
                chkHQ.Checked = false;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Role')", true);
            }
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            //pnlHQ.Visible = chkHQ.Checked;
            //if (!chkHQ.Checked) ClearPanelSelections(pnlHQ);
            //UpdateSelectAllMain();
            //if (grdUserMaster.Rows.Count > 0)
            //{
            //    grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}
        }

        protected void chkVMS_CheckedChanged1(object sender, EventArgs e)
        {
            if (ddlRole.SelectedIndex > 0)
            {
                //pnlVMS.Visible = chkVMS.Checked;
                //if (!chkVMS.Checked) ClearPanelSelections(pnlVMS);
                UpdateSelectAllMain();
            }
            else
            {
                //chkVMS.Checked = false;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Role')", true);
            }
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            ////pnlVMS.Visible = chkVMS.Checked;
            ////if (!chkVMS.Checked) ClearPanelSelections(pnlVMS);
            //UpdateSelectAllMain();
            //if (grdUserMaster.Rows.Count > 0)
            //{
            //    grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}
        }

        protected void chkWMS_CheckedChanged1(object sender, EventArgs e)
        {
            if (ddlRole.SelectedIndex > 0)
            {
                pnlWMS.Visible = chkWMS.Checked;
                if (!chkWMS.Checked) ClearPanelSelections(pnlWMS);
                UpdateSelectAllMain();
            }
            else
            {
                chkWMS.Checked = false;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Role')", true);
            }
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            //pnlWMS.Visible = chkWMS.Checked;
            //if (!chkWMS.Checked) ClearPanelSelections(pnlWMS);
            //UpdateSelectAllMain();
            //if (grdUserMaster.Rows.Count > 0)
            //{
            //    grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}
        }

        protected void chkStore_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedIndex > 0)
            {
                pnlStore.Visible = chkStore.Checked;
                if (!chkStore.Checked) ClearPanelSelections(pnlStore);
                UpdateSelectAllMain();
            }
            else
            {
                chkStore.Checked = false;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Role')", true);
            }
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            //pnlStore.Visible = chkStore.Checked;
            //if (!chkStore.Checked) ClearPanelSelections(pnlStore);
            //UpdateSelectAllMain();
            //if (grdUserMaster.Rows.Count > 0)
            //{
            //    grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}
        }

        protected void chkReports_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedIndex > 0)
            {
                pnlReports.Visible = chkReports.Checked;
                if (!chkReports.Checked) ClearPanelSelections(pnlReports);
                UpdateSelectAllMain();
            }
            else
            {
                chkReports.Checked = false;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Role')", true);
            }
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            //pnlReports.Visible = chkReports.Checked;
            //if (!chkReports.Checked) ClearPanelSelections(pnlReports);
            //UpdateSelectAllMain();
            //if (grdUserMaster.Rows.Count > 0)
            //{
            //    grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}
        }

        private void ClearPanelSelections(Control panel)
        {
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is CheckBox cb)
                    cb.Checked = false;

                if (ctrl is CheckBoxList cbl)
                {
                    foreach (ListItem item in cbl.Items)
                        item.Selected = false;
                }
            }
        }

        public void chkAll_Module()
        {
            bool val = chkAllMain.Checked;
            if (val)
            {
                pnlHQ.Visible = false;
                pnlWMS.Visible = false;
                pnlStore.Visible = false;
                pnlReports.Visible = false;

                chkConfiguration.Checked = false;
                ClearPanelSelections(pnlHQ);
                //ClearPanelSelections(pnlVMS);
                ClearPanelSelections(pnlWMS);
                ClearPanelSelections(pnlStore);
                ClearPanelSelections(pnlReports);

                if (ddlRole.SelectedItem.Text.Trim().Contains("Super Admin"))
                {
                    chkConfiguration.Visible = val;
                    chkHQ.Visible = val;
                    chkWMS.Visible = val;
                    chkStore.Visible = val;
                    chkReports.Visible = val;

                    chkConfiguration.Checked = val;
                    chkHQ.Checked = val;
                    chkWMS.Checked = val;
                    chkStore.Checked = val;
                    chkReports.Checked = val;

                    // PANELS
                    pnlHQ.Visible = val;

                    chkMDM.Checked = true;
                    ToggleSubModules(chkMDMSub, true);

                    chkProcurement.Checked = true;
                    ToggleSubModules(chkProcurementSub, true);

                    chkWDM.Checked = true;
                    ToggleSubModules(chkWDMSub, true);

                    pnlWMS.Visible = val;

                    chkInbOP.Checked = true;
                    ToggleSubModules(chkInbOPSub, true);

                    chkInvCon.Checked = true;
                    ToggleSubModules(chkInvConSub, true);

                    chkOutOP.Checked = true;
                    ToggleSubModules(chkOutOPSub, true);

                    chkInterMov.Checked = true;
                    ToggleSubModules(chkInterMovSub, true);

                    chkRevLog.Checked = true;
                    ToggleSubModules(chkRevLogSub, true);

                    chkWHInwOp.Checked = true;
                    ToggleSubModules(chkWHInwOpSub, true);


                    //Store Opeartions Menu
                    pnlStore.Visible = val;

                    chkInwOp.Checked = true;
                    ToggleSubModules(chkInwOpSub, true);

                    chkOutwardOps.Checked = true;
                    ToggleSubModules(chkOutwardOpsSub, true);

                    chkAntiTheft.Checked = true;
                    ToggleSubModules(chkAntiTheftSub, true);


                }
                else if (ddlRole.SelectedItem.Text.Trim().Contains("HQ"))
                {
                    chkConfiguration.Visible = false;
                    chkHQ.Visible = val;
                    chkWMS.Visible = false;
                    chkStore.Visible = false;
                    chkReports.Visible = val;

                    chkConfiguration.Checked = false;
                    chkHQ.Checked = val;
                    chkWMS.Checked = false;
                    chkStore.Checked = false;
                    chkReports.Checked = val;

                    // PANELS
                    pnlHQ.Visible = val;

                    chkMDM.Checked = true;
                    ToggleSubModules(chkMDMSub, true);

                    chkProcurement.Checked = true;
                    ToggleSubModules(chkProcurementSub, true);

                    chkWDM.Checked = true;
                    ToggleSubModules(chkWDMSub, true);
                }
                else if (ddlRole.SelectedItem.Text.Trim().Contains("Warehouse"))
                {
                    chkConfiguration.Visible = false;
                    chkHQ.Visible = false;
                    chkWMS.Visible = val;
                    chkStore.Visible = false;
                    chkReports.Visible = val;

                    chkConfiguration.Checked = false;
                    chkHQ.Checked = false;
                    chkWMS.Checked = val;
                    chkStore.Checked = false;
                    chkReports.Checked = val;
                    // PANELS               
                    pnlWMS.Visible = val;

                    chkInbOP.Checked = true;
                    ToggleSubModules(chkInbOPSub, true);

                    chkInvCon.Checked = true;
                    ToggleSubModules(chkInvConSub, true);

                    chkOutOP.Checked = true;
                    ToggleSubModules(chkOutOPSub, true);

                    chkInterMov.Checked = true;
                    ToggleSubModules(chkInterMovSub, true);

                    chkRevLog.Checked = true;
                    ToggleSubModules(chkRevLogSub, true);

                    chkWHInwOp.Checked = true;
                    ToggleSubModules(chkWHInwOpSub, true);


                }
                else if (ddlRole.SelectedItem.Text.Trim().Contains("Store"))
                {
                    chkConfiguration.Visible = false;
                    chkHQ.Visible = false;
                    chkWMS.Visible = false;
                    chkStore.Visible = val;
                    chkReports.Visible = val;

                    chkConfiguration.Checked = false;
                    chkHQ.Checked = false;
                    chkWMS.Checked = false;
                    chkStore.Checked = val;
                    chkReports.Checked = val;
                    
                    
                    // PANELS 
                    pnlStore.Visible = val;

                    chkInwOp.Checked = true;
                    ToggleSubModules(chkInwOpSub, true);

                    chkOutwardOps.Checked = true;
                    ToggleSubModules(chkOutwardOpsSub, true);

                    chkAntiTheft.Checked = true;
                    ToggleSubModules(chkAntiTheftSub, true);

                }

                // PANELS                
                pnlReports.Visible = val;

                // Add WMS / Store / Reports sub-modules here
            }
            else
            {
                chkConfiguration.Checked = false;
                chkConfiguration.Checked = val;
                chkHQ.Checked = val;
                chkWMS.Checked = val;
                chkStore.Checked = val;
                chkReports.Checked = val;
                ClearPanelSelections(pnlHQ);
                //ClearPanelSelections(pnlVMS);
                ClearPanelSelections(pnlWMS);
                ClearPanelSelections(pnlStore);
                ClearPanelSelections(pnlReports);
            }
        }

        protected void chkAllMain_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlEmployeeName.SelectedIndex > 0)
            {
                chkAll_Module();
            }
            else
            {
                chkAllMain.Checked = false;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Employee')", true);
                //return;
            }
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            //// MAIN MODULES
            //chkConfiguration.Checked = val;
            //chkHQ.Checked = val;
            ////chkVMS.Checked = val;
            //chkWMS.Checked = val;
            //chkStore.Checked = val;
            //chkReports.Checked = val;

            //// PANELS
            //pnlHQ.Visible = val;
            ////pnlVMS.Visible = val;
            //pnlWMS.Visible = val;
            //pnlStore.Visible = val;
            //pnlReports.Visible = val;

            //if (val)
            //{
            //    chkConfiguration.Checked = true;

            //    chkMDM.Checked = true;
            //    ToggleSubModules(chkMDMSub, true);

            //    chkProcurement.Checked = true;
            //    ToggleSubModules(chkProcurementSub, true);

            //    chkWDM.Checked = true;
            //    ToggleSubModules(chkWDMSub, true);

            //   // chkTagCreation.Checked = true;
            //    //chkWOD.Checked = true;
            //    //chkTRA.Checked = true;
            //    //chkASN.Checked = true;

            //    chkInbOP.Checked = true;
            //    ToggleSubModules(chkInbOPSub, true);

            //    chkInvCon.Checked = true;
            //    ToggleSubModules(chkInvConSub, true);

            //    chkOutOP.Checked = true;
            //    ToggleSubModules(chkOutOPSub, true);

            //    chkInterMov.Checked = true;
            //    ToggleSubModules(chkInterMovSub, true);

            //    chkRevLog.Checked = true;
            //    ToggleSubModules(chkRevLogSub, true);

            //    // Add WMS / Store / Reports sub-modules here
            //}
            //else
            //{
            //    chkConfiguration.Checked = false;
            //    ClearPanelSelections(pnlHQ);
            //    //ClearPanelSelections(pnlVMS);
            //    ClearPanelSelections(pnlWMS);
            //    ClearPanelSelections(pnlStore);
            //    ClearPanelSelections(pnlReports);
            //}
            //if (grdUserMaster.Rows.Count > 0)
            //{
            //    grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}
        }

        protected void chkMDM_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkMDMSub, chkMDM.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void chkWDM_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkWDMSub, chkWDM.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void chkProcurement_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkProcurementSub, chkProcurement.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void chkInbOP_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkInbOPSub, chkInbOP.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void chkInvCon_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkInvConSub, chkInvCon.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void chkOutOP_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkOutOPSub, chkOutOP.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void chkInterMov_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkInterMovSub, chkInterMov.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void chkRevLog_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkRevLogSub, chkRevLog.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void grdUserMaster_RowDataBound(object sender, GridViewRowEventArgs e)
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
                string id = Convert.ToString((sender as LinkButton).CommandArgument);
                GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent;

                List<SqlParameter> GetUp_Para = new List<SqlParameter>();
                GetUp_Para.Add(new SqlParameter("@Action", "USER_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@USER_ID", id));
                int i = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.StoredProcedure, SP_Name, GetUp_Para.ToArray());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Status Changed To Active Successfully...!')", true);
                Clear();
                BindGrid();
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
                string id = Convert.ToString((sender as LinkButton).CommandArgument);
                GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent;

                List<SqlParameter> GetUp_Para = new List<SqlParameter>();
                GetUp_Para.Add(new SqlParameter("@Action", "USER_STATUSUPDATE"));
                GetUp_Para.Add(new SqlParameter("@USER_ID", id));
                int i = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.StoredProcedure, SP_Name, GetUp_Para.ToArray());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Status Changed To In-Active Successfully...!')", true);
                Clear();
                BindGrid();
                id = "";

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }

        protected void lnkViewModel_Click(object sender, EventArgs e)
        {
            try
            {
                popup.Show();
                USER_ID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent;
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "VIEW_USER_CONFIG_DETAILS"));
                GetDtl_Para.Add(new SqlParameter("@USER_ID", USER_ID));
                DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, SP_Name, GetDtl_Para.ToArray());
                grdViewModules.DataSource = ds.Tables[0].DefaultView;
                grdViewModules.DataBind();
                lblAccessAuth.Text = ds.Tables[0].Rows[0]["ROLE_NAME"].ToString().Trim();
                lblUsername.Text = ds.Tables[0].Rows[0]["USER_NAME"].ToString().Trim();
                lblDepartment.Text = ds.Tables[0].Rows[0]["DEPT_NAME"].ToString().Trim();
                lblDesignation.Text = ds.Tables[0].Rows[0]["DESIGNATION"].ToString().Trim();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            finally
            {
                if (grdViewModules.Rows.Count > 0)
                {
                    grdViewModules.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdUserMaster.Rows.Count > 0)
                {
                    grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }


        
        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlStoreWarehouse.Items.Clear();

                if (Convert.ToInt32(ddlRole.SelectedValue) == 7)   // STORE USER
                {
                    dvStoreWarehouse.Visible = true;

                    lblStore.Visible = true;
                    lblWarehouse.Visible = false;

                    BindStore();
                }
                else if (Convert.ToInt32(ddlRole.SelectedValue) == 8)   // WAREHOUSE USER
                {
                    dvStoreWarehouse.Visible = true;

                    lblStore.Visible = false;
                    lblWarehouse.Visible = true;

                    BindWarehouse();
                }
                else
                {
                    dvStoreWarehouse.Visible = false;

                    lblStore.Visible = false;
                    lblWarehouse.Visible = false;
                }

                if (ddlRole.SelectedItem.Text.Trim().Contains("Super Admin"))
                {
                    chkConfiguration.Enabled = true;
                    chkHQ.Enabled = true;
                    chkWMS.Enabled = true;
                    chkStore.Enabled = true;
                    chkReports.Enabled = true;
                }
                else if (ddlRole.SelectedItem.Text.Trim().Contains("HQ"))
                {
                    chkConfiguration.Enabled = false;
                    chkHQ.Enabled = true;
                    chkWMS.Enabled = false;
                    chkStore.Enabled = false;
                    chkReports.Enabled = true;
                }
                else if (ddlRole.SelectedItem.Text.Trim().Contains("Warehouse"))
                {
                    chkConfiguration.Enabled = false;
                    chkHQ.Enabled = false;
                    chkWMS.Enabled = true;
                    chkStore.Enabled = false;
                    chkReports.Enabled = true;
                }
                else if (ddlRole.SelectedItem.Text.Trim().Contains("Store"))
                {
                    chkConfiguration.Enabled = false;
                    chkHQ.Enabled = false;
                    chkWMS.Enabled = false;
                    chkStore.Enabled = true;
                    chkReports.Enabled = true;
                }
                //chkAllMain_CheckedChanged(sender, e);
                chkAll_Module();

                if (grdUserMaster.Rows.Count > 0)
                {
                    grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "')",
                    true
                );
            }
        }

        private void BindStore()
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@ACTION", "DD_STORE_MASTER"));

                DataSet ds = SqlHelper.ExecuteDataset(
                    ConnStr,
                    CommandType.StoredProcedure,
                    SP_Name,
                    parameters.ToArray()
                );

                ddlStoreWarehouse.DataSource = ds.Tables[0];
                ddlStoreWarehouse.DataTextField = "STORE_NAME";
                ddlStoreWarehouse.DataValueField = "STORE_ID";
                ddlStoreWarehouse.DataBind();

                ddlStoreWarehouse.Items.Insert(0, new ListItem(" Select Store ", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "')",
                    true
                );
            }
        }

        private void BindWarehouse()
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@ACTION", "DD_WAREHOUSE_MASTER"));

                DataSet ds = SqlHelper.ExecuteDataset(
                    ConnStr,
                    CommandType.StoredProcedure,
                    SP_Name,
                    parameters.ToArray()
                );

                ddlStoreWarehouse.DataSource = ds.Tables[0];
                ddlStoreWarehouse.DataTextField = "WAREHOUSE_NAME";
                ddlStoreWarehouse.DataValueField = "WAREHOUSE_ID";
                ddlStoreWarehouse.DataBind();

                ddlStoreWarehouse.Items.Insert(0, new ListItem(" Select Warehouse ", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "')",
                    true
                );
            }
        }


        protected void chkConfiguration_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedIndex > 0)
            {

            }
            else
            {
                chkConfiguration.Checked = false;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Role')", true);
            }
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


        private void FillEmployee()
        {
            try
            {
                List<SqlParameter> Para = new List<SqlParameter>();

                Para.Add(
                    new SqlParameter("@Action", "GET_DROPDOWN"));

                DataSet ds = SqlHelper.ExecuteDataset(
                    ConnStr,
                    CommandType.StoredProcedure,
                    Employee_SP_Name,
                    Para.ToArray());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEmployeeName.DataSource = ds.Tables[0];
                    ddlEmployeeName.DataTextField = "EMP_NAME";
                    ddlEmployeeName.DataValueField = "EMP_ID";
                    ddlEmployeeName.DataBind();

                    ddlEmployeeName.Items.Insert(
                        0,
                        new ListItem("Select Employee", ""));
                }
                else
                {
                    ddlEmployeeName.Items.Clear();

                    ddlEmployeeName.Items.Insert(
                        0,
                        new ListItem("Select Employee", ""));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "alertMessage",
                    "alert('" + ex.Message + "')",
                    true);
            }
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlEmployeeName.SelectedValue))
                {
                    ddlRole.SelectedIndex = 0;
                    ddlDepartment.SelectedIndex = 0;
                    ddlDesignation.SelectedIndex = 0;

                    ddlRole.Enabled = false;
                    ddlDepartment.Enabled = false;
                    ddlDesignation.Enabled = false;

                    return;
                }

                List<SqlParameter> Para = new List<SqlParameter>();
                Para.Add(new SqlParameter("@ACTION", "GET_EMPLOYEE_DETAILS"));
                Para.Add(new SqlParameter("@EMP_ID", Convert.ToInt32(ddlEmployeeName.SelectedValue)));

                DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "[SP_EMPLOYEE_MASTER]", Para.ToArray());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    ddlRole.SelectedValue = row["EMP_ROLEID"].ToString();
                    if (ddlRole.SelectedIndex > 0)
                    {
                        ddlRole_SelectedIndexChanged(null, null);
                    }
                    ddlDepartment.SelectedValue = row["EMP_DEPARTMENTID"].ToString();
                    ddlDesignation.SelectedValue = row["EMP_DESIGNATIONID"].ToString();

                    ddlRole.Enabled = false;
                    ddlDepartment.Enabled = false;
                    ddlDesignation.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.Replace("'", "\\'") + "')", true);
            }
        }

        protected void chkInwOp_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkInwOpSub, chkInwOp.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void chkWHInwOp_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkWHInwOpSub, chkWHInwOp.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void chkOutOp_CheckedChanged1(object sender, EventArgs e)
        {
            //ToggleSubModules(chkOutOpSub, chkOutOp.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void chkOutwardOps_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkOutwardOpsSub, chkOutwardOps.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void chkAntiTheft_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSubModules(chkAntiTheftSub, chkAntiTheft.Checked);
            if (grdUserMaster.Rows.Count > 0)
            {
                grdUserMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

    }
}