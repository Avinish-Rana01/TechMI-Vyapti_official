using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.HQ_Operations
{
    public partial class Employee_Master : System.Web.UI.Page
    {
        #region Connection

        private readonly string ConnStr =
            ConfigurationManager
                .ConnectionStrings["sqlconn"]
                .ConnectionString;

        private const string SP_Name = "SP_EMPLOYEE_MASTER";

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["EMP_ID"] = null;

                btnSubmit.Text = "Submit";

                BindRole();
                BindDepartment();
                BindDesignation();
                BindGrid();
                ClearControls();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            SetGridHeader();
        }

        private void SetGridHeader()
        {
            if (grdEmployeeMaster.HeaderRow != null)
            {
                grdEmployeeMaster.UseAccessibleHeader = true;

                grdEmployeeMaster.HeaderRow.TableSection =
                    TableRowSection.TableHeader;
            }
        }
        private void BindGrid()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnStr))
                using (SqlCommand cmd = new SqlCommand(SP_Name, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GET_ALL");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        grdEmployeeMaster.DataSource = dt;
                        grdEmployeeMaster.DataBind();
                    }
                }
                SetGridHeader();
            }
            catch (Exception ex)
            {
                ShowMessage("Error while loading employee records: " + ex.Message);
            }
        }
        private void BindRole()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnStr))
                using (SqlCommand cmd = new SqlCommand(SP_Name, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GET_ROLE");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlRole.Items.Clear();
                        ddlRole.Items.Add(new ListItem("Select Role", ""));
                        foreach (DataRow row in dt.Rows)
                        {
                            ddlRole.Items.Add(new ListItem(row["ROLE_NAME"].ToString(), row["ROLE_ID"].ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Unable to load Role: " + ex.Message);
            }
        }

        private void BindDepartment()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnStr))
                using (SqlCommand cmd = new SqlCommand(SP_Name, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GET_DEPARTMENT");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlDepartment.Items.Clear();
                        ddlDepartment.Items.Add(new ListItem("Select Department", ""));
                        foreach (DataRow row in dt.Rows)
                        {
                            ddlDepartment.Items.Add(new ListItem(row["DEPT_NAME"].ToString(), row["DEPT_ID"].ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Unable to load Department: " + ex.Message);
            }
        }
        private void BindDesignation()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnStr))
                using (SqlCommand cmd = new SqlCommand(SP_Name, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GET_DESIGNATION");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlDesignation.Items.Clear();
                        ddlDesignation.Items.Add(new ListItem("Select Designation", ""));
                        foreach (DataRow row in dt.Rows)
                        {
                            ddlDesignation.Items.Add(new ListItem(row["DESIGNATION_NAME"].ToString(), row["DESIGNATION_ID"].ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Unable to load Designation: " + ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                string empCode = txtEmpCode.Text.Trim();
                string empName = txtEmpName.Text.Trim();
                string empEmail = txtEmpEmailID.Text.Trim();
                string empContact = txtEmpContactNo.Text.Trim();
                string roleId = ddlRole.SelectedValue.Trim();
                string departmentId = ddlDepartment.SelectedValue.Trim();
                string designationId = ddlDesignation.SelectedValue.Trim();
                string status = ddlStatus.SelectedValue.Trim();

                if (string.IsNullOrWhiteSpace(empCode))
                {
                    ShowMessage("Please enter Employee Code.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(empName))
                {
                    ShowMessage("Please enter Employee Name.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(empEmail))
                {
                    ShowMessage("Please enter Employee Email ID.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(empContact))
                {
                    ShowMessage("Please enter Employee Contact No.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(roleId))
                {
                    ShowMessage("Please select Role.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(departmentId))
                {
                    ShowMessage("Please select Department.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(designationId))
                {
                    ShowMessage("Please select Designation.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(status))
                {
                    ShowMessage("Please select Status.");
                    return;
                }

                int roleIdValue;
                int departmentIdValue;
                int designationIdValue;
                int statusValue;

                if (!int.TryParse(roleId, out roleIdValue))
                {
                    ShowMessage("Invalid Role.");
                    return;
                }

                if (!int.TryParse(departmentId, out departmentIdValue))
                {
                    ShowMessage("Invalid Department.");
                    return;
                }

                if (!int.TryParse(designationId, out designationIdValue))
                {
                    ShowMessage("Invalid Designation.");
                    return;
                }

                if (!int.TryParse(status, out statusValue))
                {
                    ShowMessage("Invalid Status.");
                    return;
                }

                if (btnSubmit.Text.Trim().Equals("Submit", StringComparison.OrdinalIgnoreCase))
                {
                    bool isInserted = InsertEmployee(
                        empCode,
                        empName,
                        empEmail,
                        empContact,
                        roleIdValue,
                        departmentIdValue,
                        designationIdValue,
                        statusValue);

                    if (isInserted)
                    {
                        ShowMessage("Employee added successfully.");
                        ClearControls();
                        BindGrid();
                    }

                    return;
                }

                if (btnSubmit.Text.Trim().Equals("Update", StringComparison.OrdinalIgnoreCase))
                {
                    string empId = hdnID.Value.Trim();

                    if (string.IsNullOrWhiteSpace(empId) && Session["EMP_ID"] != null)
                        empId = Session["EMP_ID"].ToString().Trim();

                    int empIdValue;

                    if (!int.TryParse(empId, out empIdValue))
                    {
                        ShowMessage("Employee ID not found. Please select Edit again.");
                        btnSubmit.Text = "Submit";
                        return;
                    }

                    UpdateEmployee(
                        empIdValue,
                        empCode,
                        empName,
                        empEmail,
                        empContact,
                        roleIdValue,
                        departmentIdValue,
                        designationIdValue,
                        statusValue);

                    ShowMessage("Employee updated successfully.");
                    ClearControls();
                    BindGrid();

                    return;
                }

                btnSubmit.Text = "Submit";
                ShowMessage("Something went wrong. Please try again.");
            }
            catch (Exception ex)
            {
                btnSubmit.Text = "Submit";
                ShowMessage("Something went wrong: " + ex.Message);
            }
        }

        private bool InsertEmployee(
            string empCode,
            string empName,
            string empEmail,
            string empContact,
            int roleId,
            int departmentId,
            int designationId,
            int status)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(SP_Name, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@EMP_CODE", empCode);
                cmd.Parameters.AddWithValue("@EMP_NAME", empName);
                cmd.Parameters.AddWithValue("@EMP_EMAILID", empEmail);
                cmd.Parameters.AddWithValue("@EMP_CONTACTNO", empContact);
                cmd.Parameters.AddWithValue("@EMP_ROLEID", roleId);
                cmd.Parameters.AddWithValue("@EMP_DEPARTMENTID", departmentId);
                cmd.Parameters.AddWithValue("@EMP_DESIGNATIONID", designationId);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@CREATED_BY", GetUserId());

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        string result = dr["RESULT"].ToString();
                        string message = dr["MESSAGE"].ToString();

                        if (result.Equals("DUPLICATE", StringComparison.OrdinalIgnoreCase))
                        {
                            ShowMessage(message);
                            return false;
                        }

                        if (result.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase))
                            return true;
                    }
                }
            }

            return false;
        }

        private void UpdateEmployee(int empId, string empCode, string empName, string empEmail, string empContact, int roleId, int departmentId, int designationId, int status)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(SP_Name, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@EMP_ID", empId);
                cmd.Parameters.AddWithValue("@EMP_CODE", empCode);
                cmd.Parameters.AddWithValue("@EMP_NAME", empName);
                cmd.Parameters.AddWithValue("@EMP_EMAILID", empEmail);
                cmd.Parameters.AddWithValue("@EMP_CONTACTNO", empContact);
                cmd.Parameters.AddWithValue("@EMP_ROLEID", roleId);
                cmd.Parameters.AddWithValue("@EMP_DEPARTMENTID", departmentId);
                cmd.Parameters.AddWithValue("@EMP_DESIGNATIONID", designationId);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@MODIFY_BY", GetUserId());
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("Employee record was not found.");
                }
            }
        }

        private DataTable GetEmployeeById(int empId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(SP_Name, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GET_BY_ID");
                cmd.Parameters.AddWithValue("@EMP_ID", empId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = sender as LinkButton;
                if (btn == null) return;

                int empId;
                if (!int.TryParse(btn.CommandArgument, out empId))
                {
                    ShowMessage("Invalid Employee ID.");
                    return;
                }

                DataTable dt = GetEmployeeById(empId);
                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowMessage("Employee record not found.");
                    return;
                }

                DataRow row = dt.Rows[0];
                hdnID.Value = empId.ToString();
                Session["EMP_ID"] = empId;
                btnSubmit.Text = "Update";

                txtEmpCode.Text = row["EMP_CODE"].ToString().Trim();
                txtEmpName.Text = row["EMP_NAME"].ToString().Trim();
                txtEmpEmailID.Text = row["EMP_EMAILID"].ToString().Trim();
                txtEmpContactNo.Text = row["EMP_CONTACTNO"].ToString().Trim();

                ddlRole.ClearSelection();
                string roleId = row["EMP_ROLEID"].ToString().Trim();
                ListItem roleItem = ddlRole.Items.FindByValue(roleId);
                if (roleItem != null) roleItem.Selected = true;

                ddlDepartment.ClearSelection();
                string departmentId = row["EMP_DEPARTMENTID"].ToString().Trim();
                ListItem departmentItem = ddlDepartment.Items.FindByValue(departmentId);
                if (departmentItem != null) departmentItem.Selected = true;

                ddlDesignation.ClearSelection();
                string designationId = row["EMP_DESIGNATIONID"].ToString().Trim();
                ListItem designationItem = ddlDesignation.Items.FindByValue(designationId);
                if (designationItem != null) designationItem.Selected = true;

                ddlStatus.ClearSelection();
                string status = row["STATUS"].ToString().Trim();
                ListItem statusItem = ddlStatus.Items.FindByValue(status);
                if (statusItem != null) statusItem.Selected = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSidebarEdit", "openSidebar('single');", true);
            }
            catch (Exception ex)
            {
                btnSubmit.Text = "Submit";
                ShowMessage("Unable to edit employee: " + ex.Message);
            }
        }
        protected void lnkActive_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeStatus(sender, "ACTIVE");
                ShowMessage("Employee status changed to Active successfully.");
                ClearControls();
                BindGrid();
            }
            catch (Exception ex)
            {
                ShowMessage("Unable to activate employee: " + ex.Message);
            }
        }

        protected void lnkInactive_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                ChangeStatus(
                    sender,
                    "INACTIVE");


                ShowMessage(
                    "Employee status changed to Inactive successfully.");


                ClearControls();

                BindGrid();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Unable to deactivate employee: "
                    + ex.Message);
            }
        }


        // ============================================================
        // CHANGE STATUS
        // ============================================================

        private void ChangeStatus(
            object sender,
            string action)
        {
            LinkButton btn =
                sender as LinkButton;


            if (btn == null)
            {
                return;
            }


            int empId;


            if (!int.TryParse(
                btn.CommandArgument,
                out empId))
            {
                throw new Exception(
                    "Invalid Employee ID.");
            }


            using (SqlConnection con =
                new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd =
                    new SqlCommand(SP_Name, con))
                {
                    cmd.CommandType =
                        CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue(
                        "@Action",
                        action);


                    cmd.Parameters.AddWithValue(
                        "@EMP_ID",
                        empId);


                    cmd.Parameters.AddWithValue(
                        "@MODIFY_BY",
                        GetUserId());


                    con.Open();


                    int rowsAffected =
                        cmd.ExecuteNonQuery();


                    if (rowsAffected == 0)
                    {
                        throw new Exception(
                            "Employee record not found.");
                    }
                }
            }
        }


        // ============================================================
        // ROW DATA BOUND
        // ============================================================

        protected void grdEmployeeMaster_RowDataBound(
            object sender,
            GridViewRowEventArgs e)
        {
            if (e.Row.RowType !=
                DataControlRowType.DataRow)
            {
                return;
            }


            DataRowView row =
                e.Row.DataItem as DataRowView;


            if (row == null)
            {
                return;
            }


            string status =
                row["STATUS_TEXT"]
                    .ToString()
                    .Trim();


            LinkButton lnkActive =
                e.Row.FindControl(
                    "lnkActive")
                as LinkButton;


            LinkButton lnkInactive =
                e.Row.FindControl(
                    "lnkInactive")
                as LinkButton;


            if (status.Equals(
                "Active",
                StringComparison.OrdinalIgnoreCase))
            {
                if (lnkActive != null)
                {
                    lnkActive.Visible = false;
                }


                if (lnkInactive != null)
                {
                    lnkInactive.Visible = true;
                }
            }
            else
            {
                if (lnkActive != null)
                {
                    lnkActive.Visible = true;
                }


                if (lnkInactive != null)
                {
                    lnkInactive.Visible = false;
                }
            }
        }


        // ============================================================
        // CLEAR BUTTON
        // ============================================================

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }


        // ============================================================
        // CLEAR CONTROLS
        // ============================================================

        //private void ClearControls()
        //{
        //    txtEmpCode.Text = "";

        //    txtEmpName.Text = "";

        //    txtEmpEmailID.Text = "";

        //    txtEmpContactNo.Text = "";


        //    ddlRole.ClearSelection();

        //    if (ddlRole.Items.Count > 0)
        //    {
        //        ddlRole.SelectedIndex = 0;
        //    }


        //    ddlDepartment.ClearSelection();

        //    if (ddlDepartment.Items.Count > 0)
        //    {
        //        ddlDepartment.SelectedIndex = 0;
        //    }


        //    ddlDesignation.ClearSelection();

        //    if (ddlDesignation.Items.Count > 0)
        //    {
        //        ddlDesignation.SelectedIndex = 0;
        //    }


        //    ddlStatus.ClearSelection();

        //    if (ddlStatus.Items.Count > 0)
        //    {
        //        ddlStatus.SelectedIndex = 0;
        //    }


        //    hdnID.Value = "";

        //    Session["EMP_ID"] = null;


        //    btnSubmit.Text = "Submit";
        //}


        private void ClearControls()
        {
            txtEmpCode.Text = "";
            txtEmpName.Text = "";
            txtEmpEmailID.Text = "";
            txtEmpContactNo.Text = "";

            ddlRole.ClearSelection();

            if (ddlRole.Items.Count > 0)
                ddlRole.SelectedIndex = 0;

            ddlDepartment.ClearSelection();

            if (ddlDepartment.Items.Count > 0)
                ddlDepartment.SelectedIndex = 0;

            ddlDesignation.ClearSelection();

            if (ddlDesignation.Items.Count > 0)
                ddlDesignation.SelectedIndex = 0;

            ddlStatus.ClearSelection();

            hdnID.Value = "";
            Session["EMP_ID"] = null;

            btnSubmit.Text = "Submit";
        }

        // ============================================================
        // GET USER ID
        // ============================================================

        private int GetUserId()
        {
            int userId = 0;


            if (Session["USER_ID"] != null)
            {
                int.TryParse(
                    Session["USER_ID"].ToString(),
                    out userId);
            }


            return userId;
        }


        // ============================================================
        // MULTIPLE ENTRY - EXCEL UPLOAD
        // ============================================================

        protected void btnFileUpload_Click(
            object sender,
            EventArgs e)
        {
            string filePath = "";


            try
            {
                if (!flpUpload.HasFile)
                {
                    ShowMessage(
                        "Please select Excel file.");

                    return;
                }


                string extension =
                    Path.GetExtension(
                        flpUpload.FileName)
                    .ToLower();


                if (extension != ".xls" &&
                    extension != ".xlsx")
                {
                    ShowMessage(
                        "Please upload only .xls or .xlsx file.");

                    return;
                }


                string uploadFolder =
                    Server.MapPath(
                        "~/Uploads/");


                if (!Directory.Exists(
                    uploadFolder))
                {
                    Directory.CreateDirectory(
                        uploadFolder);
                }


                string fileName =
                    Guid.NewGuid().ToString()
                    + extension;


                filePath =
                    Path.Combine(
                        uploadFolder,
                        fileName);


                flpUpload.SaveAs(filePath);


                DataTable dtExcel =
                    ReadExcel(filePath);


                if (dtExcel == null ||
                    dtExcel.Rows.Count == 0)
                {
                    ShowMessage(
                        "Excel file contains no data.");

                    return;
                }


                // ====================================================
                // REQUIRED COLUMN CHECK
                // ====================================================

                string[] requiredColumns =
                {
                    "EMP_CODE",
                    "EMP_NAME",
                    "EMP_EMAILID",
                    "EMP_CONTACTNO",
                    "ROLE_NAME",
                    "DEPARTMENT_NAME",
                    "DESIGNATION_NAME"
                };


                foreach (string column
                    in requiredColumns)
                {
                    if (!dtExcel.Columns.Contains(
                        column))
                    {
                        ShowMessage(
                            "Excel column "
                            + column
                            + " not found.");

                        return;
                    }
                }


                using (SqlConnection con =
                    new SqlConnection(ConnStr))
                {
                    con.Open();


                    foreach (DataRow row
                        in dtExcel.Rows)
                    {
                        string empCode =
                            row["EMP_CODE"]
                                .ToString()
                                .Trim();


                        string empName =
                            row["EMP_NAME"]
                                .ToString()
                                .Trim();


                        if (string.IsNullOrWhiteSpace(
                            empCode) ||
                            string.IsNullOrWhiteSpace(
                                empName))
                        {
                            continue;
                        }


                        string empEmail =
                            row["EMP_EMAILID"]
                                .ToString()
                                .Trim();


                        string empContact =
                            row["EMP_CONTACTNO"]
                                .ToString()
                                .Trim();


                        string roleName =
                            row["ROLE_NAME"]
                                .ToString()
                                .Trim();


                        string departmentName =
                            row["DEPARTMENT_NAME"]
                                .ToString()
                                .Trim();


                        string designationName =
                            row["DESIGNATION_NAME"]
                                .ToString()
                                .Trim();


                        int roleId =
                            GetRoleId(
                                con,
                                roleName);


                        int departmentId =
                            GetDepartmentId(
                                con,
                                departmentName);


                        int designationId =
                            GetDesignationId(
                                con,
                                designationName);


                        if (roleId == 0)
                        {
                            throw new Exception(
                                "Role not found: "
                                + roleName);
                        }


                        if (departmentId == 0)
                        {
                            throw new Exception(
                                "Department not found: "
                                + departmentName);
                        }


                        if (designationId == 0)
                        {
                            throw new Exception(
                                "Designation not found: "
                                + designationName);
                        }


                        int status = 1;


                        if (dtExcel.Columns.Contains(
                            "STATUS"))
                        {
                            string statusValue =
                                row["STATUS"]
                                    .ToString()
                                    .Trim();


                            if (
                                statusValue.Equals(
                                    "Inactive",
                                    StringComparison.OrdinalIgnoreCase)
                                ||
                                statusValue == "0")
                            {
                                status = 0;
                            }
                        }


                        using (SqlCommand cmd =
                            new SqlCommand(
                                SP_Name,
                                con))
                        {
                            cmd.CommandType =
                                CommandType.StoredProcedure;


                            cmd.Parameters.AddWithValue(
                                "@Action",
                                "INSERT");


                            cmd.Parameters.AddWithValue(
                                "@EMP_CODE",
                                empCode);


                            cmd.Parameters.AddWithValue(
                                "@EMP_NAME",
                                empName);


                            cmd.Parameters.AddWithValue(
                                "@EMP_EMAILID",
                                empEmail);


                            cmd.Parameters.AddWithValue(
                                "@EMP_CONTACTNO",
                                empContact);


                            cmd.Parameters.AddWithValue(
                                "@EMP_ROLEID",
                                roleId);


                            cmd.Parameters.AddWithValue(
                                "@EMP_DEPARTMENTID",
                                departmentId);


                            cmd.Parameters.AddWithValue(
                                "@EMP_DESIGNATIONID",
                                designationId);


                            cmd.Parameters.AddWithValue(
                                "@STATUS",
                                status);


                            cmd.Parameters.AddWithValue(
                                "@CREATED_BY",
                                GetUserId());


                            cmd.ExecuteNonQuery();
                        }
                    }
                }


                ShowMessage(
                    "Employees uploaded successfully.");


                ClearControls();

                BindGrid();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Excel upload failed: "
                    + ex.Message);
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(filePath) &&
                    File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }


        // ============================================================
        // GET ROLE ID
        // ============================================================

        private int GetRoleId(
            SqlConnection con,
            string roleName)
        {
            string query = @"
                SELECT TOP 1 ROLE_ID
                FROM TBL_ROLE_MASTER
                WHERE ROLE_NAME = @ROLE_NAME";


            using (SqlCommand cmd =
                new SqlCommand(
                    query,
                    con))
            {
                cmd.Parameters.AddWithValue(
                    "@ROLE_NAME",
                    roleName);


                object result =
                    cmd.ExecuteScalar();


                if (result == null ||
                    result == DBNull.Value)
                {
                    return 0;
                }


                return Convert.ToInt32(result);
            }
        }


        // ============================================================
        // GET DEPARTMENT ID
        // ============================================================

        private int GetDepartmentId(
            SqlConnection con,
            string departmentName)
        {
            string query = @"
                SELECT TOP 1 DEPT_ID
                FROM TBL_DEPARTMENT_MASTER
                WHERE DEPT_NAME = @DEPT_NAME";


            using (SqlCommand cmd =
                new SqlCommand(
                    query,
                    con))
            {
                cmd.Parameters.AddWithValue(
                    "@DEPT_NAME",
                    departmentName);


                object result =
                    cmd.ExecuteScalar();


                if (result == null ||
                    result == DBNull.Value)
                {
                    return 0;
                }


                return Convert.ToInt32(result);
            }
        }


        // ============================================================
        // GET DESIGNATION ID
        // ============================================================

        private int GetDesignationId(
            SqlConnection con,
            string designationName)
        {
            string query = @"
                SELECT TOP 1 DESIGNATION_ID
                FROM TBL_DESIGNATION_MASTER
                WHERE DESIGNATION_NAME = @DESIGNATION_NAME";


            using (SqlCommand cmd =
                new SqlCommand(
                    query,
                    con))
            {
                cmd.Parameters.AddWithValue(
                    "@DESIGNATION_NAME",
                    designationName);


                object result =
                    cmd.ExecuteScalar();


                if (result == null ||
                    result == DBNull.Value)
                {
                    return 0;
                }


                return Convert.ToInt32(result);
            }
        }


        // ============================================================
        // READ EXCEL
        // ============================================================

        private DataTable ReadExcel(
            string filePath)
        {
            DataTable dt =
                new DataTable();


            string extension =
                Path.GetExtension(filePath)
                    .ToLower();


            string excelConnectionString;


            if (extension == ".xlsx")
            {
                excelConnectionString =
                    "Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=" + filePath + ";" +
                    "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";
            }
            else
            {
                excelConnectionString =
                    "Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=" + filePath + ";" +
                    "Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
            }


            using (OleDbConnection con =
                new OleDbConnection(
                    excelConnectionString))
            {
                con.Open();


                DataTable schema =
                    con.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables,
                        null);


                if (schema == null ||
                    schema.Rows.Count == 0)
                {
                    return dt;
                }


                string sheetName =
                    schema.Rows[0]["TABLE_NAME"]
                        .ToString();


                string query =
                    "SELECT * FROM [" +
                    sheetName +
                    "]";


                using (OleDbDataAdapter da =
                    new OleDbDataAdapter(
                        query,
                        con))
                {
                    da.Fill(dt);
                }
            }


            return dt;
        }


        // ============================================================
        // SHOW MESSAGE
        // ============================================================

        private void ShowMessage(
            string message)
        {
            string safeMessage =
                (message ?? "")
                .Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\r", "")
                .Replace("\n", "\\n");


            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "alertMessage",
                "alert('" + safeMessage + "');",
                true);
        }
    }
}