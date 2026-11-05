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
    public partial class Department_Master : System.Web.UI.Page
    {
        #region Class Level

        private readonly string connStr =
            ConfigurationManager
                .ConnectionStrings["sqlconn"]
                .ConnectionString;

        #endregion


        // ============================================================
        // PAGE LOAD
        // ============================================================

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["DEPT_ID"] = null;

                btnSubmit.Text = "Submit";

                BindGrid();
            }
        }


        // ============================================================
        // PRE RENDER
        // IMPORTANT FOR DATATABLE HEADER
        // ============================================================

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (grdDepartmentMaster.HeaderRow != null)
            {
                grdDepartmentMaster.UseAccessibleHeader = true;

                grdDepartmentMaster.HeaderRow.TableSection =
                    TableRowSection.TableHeader;
            }
        }


        // ============================================================
        // BIND GRID
        // ============================================================

        private void BindGrid()
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(connStr))
                {
                    string query = @"
                        SELECT
                            DEPT_ID,
                            DEPT_NAME,
                            CASE
                                WHEN STATUS = 1
                                    THEN 'Active'
                                ELSE 'Inactive'
                            END AS STATUS_TEXT
                        FROM TBL_DEPARTMENT_MASTER
                        ORDER BY DEPT_ID DESC";


                    using (SqlDataAdapter da =
                        new SqlDataAdapter(query, con))
                    {
                        DataTable dt =
                            new DataTable();

                        da.Fill(dt);

                        grdDepartmentMaster.DataSource = dt;

                        grdDepartmentMaster.DataBind();
                    }
                }


                // Keep Grid Header
                if (grdDepartmentMaster.Rows.Count > 0 &&
                    grdDepartmentMaster.HeaderRow != null)
                {
                    grdDepartmentMaster.UseAccessibleHeader = true;

                    grdDepartmentMaster.HeaderRow.TableSection =
                        TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Error while loading department records: "
                    + ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string departmentName =
                    txtDepartmentName.Text.Trim();


                string status =
                    ddlStatus.SelectedValue.Trim();


                if (string.IsNullOrWhiteSpace(
                    departmentName))
                {
                    ShowMessage(
                        "Please enter Department Name.");

                    return;
                }


                if (string.IsNullOrEmpty(status))
                {
                    ShowMessage(
                        "Please select Status.");

                    return;
                }


                // ====================================================
                // INSERT
                // ====================================================

                if (btnSubmit.Text.Trim()
                    .Equals(
                        "Submit",
                        StringComparison.OrdinalIgnoreCase))
                {
                    InsertDepartment(
                        departmentName,
                        status);

                    ShowMessage(
                        "Department added successfully.");

                    ClearControls();

                    BindGrid();

                    return;
                }


                // ====================================================
                // UPDATE
                // ====================================================

                if (btnSubmit.Text.Trim()
                    .Equals(
                        "Update",
                        StringComparison.OrdinalIgnoreCase))
                {
                    string deptId = "";


                    // First try Session
                    if (Session["DEPT_ID"] != null)
                    {
                        deptId =
                            Session["DEPT_ID"]
                                .ToString()
                                .Trim();
                    }


                    // Fallback to HiddenField
                    if (string.IsNullOrEmpty(deptId))
                    {
                        deptId =
                            hdnID.Value
                                .Trim();
                    }


                    // No ID means update is not possible
                    if (string.IsNullOrEmpty(deptId))
                    {
                        ShowMessage(
                            "Department ID not found. Please select Edit again.");

                        btnSubmit.Text = "Submit";

                        return;
                    }


                    int departmentId;


                    if (!int.TryParse(
                        deptId,
                        out departmentId))
                    {
                        ShowMessage(
                            "Invalid Department ID.");

                        btnSubmit.Text = "Submit";

                        return;
                    }


                    UpdateDepartment(
                        departmentId,
                        departmentName,
                        status);


                    ShowMessage(
                        "Department updated successfully.");


                    // IMPORTANT
                    // Reset edit mode after update
                    ClearControls();

                    BindGrid();

                    return;
                }


                // ====================================================
                // UNKNOWN BUTTON STATE
                // ====================================================

                btnSubmit.Text = "Submit";

                ShowMessage(
                    "Something went wrong. Please try again.");
            }
            catch (Exception ex)
            {
                btnSubmit.Text = "Submit";

                ShowMessage(
                    "Something went wrong: "
                    + ex.Message);
            }
        }


        // ============================================================
        // INSERT DEPARTMENT
        // ============================================================

        private void InsertDepartment(
            string departmentName,
            string status)
        {
            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                string query = @"
                    INSERT INTO TBL_DEPARTMENT_MASTER
                    (
                        DEPT_NAME,
                        CREATED_BY,
                        CREATED_DATE,
                        STATUS
                    )
                    VALUES
                    (
                        @DEPT_NAME,
                        @CREATED_BY,
                        GETDATE(),
                        @STATUS
                    )";


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@DEPT_NAME",
                        departmentName);


                    string createdBy = "0";


                    if (Session["USER_ID"] != null)
                    {
                        createdBy =
                            Session["USER_ID"]
                                .ToString();
                    }


                    cmd.Parameters.AddWithValue(
                        "@CREATED_BY",
                        createdBy);


                    cmd.Parameters.AddWithValue(
                        "@STATUS",
                        Convert.ToInt32(status));


                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        // ============================================================
        // UPDATE DEPARTMENT
        // ============================================================

        private void UpdateDepartment(
            int departmentId,
            string departmentName,
            string status)
        {
            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                string query = @"
                    UPDATE TBL_DEPARTMENT_MASTER
                    SET
                        DEPT_NAME = @DEPT_NAME,
                        STATUS = @STATUS
                    WHERE DEPT_ID = @DEPT_ID";


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@DEPT_NAME",
                        departmentName);


                    cmd.Parameters.AddWithValue(
                        "@STATUS",
                        Convert.ToInt32(status));


                    cmd.Parameters.AddWithValue(
                        "@DEPT_ID",
                        departmentId);


                    con.Open();

                    int rowsAffected =
                        cmd.ExecuteNonQuery();


                    if (rowsAffected == 0)
                    {
                        throw new Exception(
                            "Department record was not found.");
                    }
                }
            }
        }


        // ============================================================
        // CLEAR BUTTON
        // ============================================================

        protected void btnClear_Click(
            object sender,
            EventArgs e)
        {
            ClearControls();
        }


        // ============================================================
        // CLEAR CONTROLS
        // ============================================================

        private void ClearControls()
        {
            txtDepartmentName.Text = "";


            ddlStatus.ClearSelection();


            if (ddlStatus.Items.Count > 0)
            {
                ddlStatus.SelectedIndex = 0;
            }


            hdnID.Value = "";


            Session["DEPT_ID"] = null;


            // VERY IMPORTANT
            // After update, button becomes Submit again
            btnSubmit.Text = "Submit";
        }


        // ============================================================
        // EDIT
        // ============================================================

        protected void lnkEdit_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                LinkButton btn =
                    sender as LinkButton;


                if (btn == null)
                {
                    return;
                }


                // ====================================================
                // GET ROW
                // ====================================================

                GridViewRow row =
                    btn.NamingContainer
                    as GridViewRow;


                if (row == null)
                {
                    return;
                }


                // ====================================================
                // CHANGE BUTTON TO UPDATE
                // ====================================================

                btnSubmit.Text = "Update";


                // ====================================================
                // GET DEPARTMENT ID
                //
                // Column indexes:
                //
                // Cells[0] = Sr.No
                // Cells[1] = DEPT_ID (hidden)
                // Cells[2] = DEPT_NAME
                // Cells[3] = STATUS
                // Cells[4] = ACTION
                // ====================================================

                string deptId =
                    Server.HtmlDecode(
                        row.Cells[1].Text)
                    .Trim();


                if (string.IsNullOrEmpty(deptId) ||
                    deptId == "&nbsp;")
                {
                    ShowMessage(
                        "Department ID not found.");

                    btnSubmit.Text = "Submit";

                    return;
                }


                // ====================================================
                // STORE ID
                // ====================================================

                hdnID.Value =
                    deptId;


                Session["DEPT_ID"] =
                    deptId;


                // ====================================================
                // DEPARTMENT NAME
                // ====================================================

                txtDepartmentName.Text =
                    Server.HtmlDecode(
                        row.Cells[2].Text)
                    .Trim();


                // ====================================================
                // STATUS
                // ====================================================

                string status =
                    Server.HtmlDecode(
                        row.Cells[3].Text)
                    .Trim();


                ddlStatus.ClearSelection();


                ListItem li =
                    ddlStatus.Items.FindByText(
                        status);


                if (li != null)
                {
                    li.Selected = true;
                }


                // ====================================================
                // OPEN SIDEBAR
                // SAME AS CATEGORY MASTER
                // ====================================================

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "OpenSidebarEdit",
                    "openSidebar('single');",
                    true);


                // ====================================================
                // KEEP GRID HEADER
                // ====================================================

                if (grdDepartmentMaster.Rows.Count > 0 &&
                    grdDepartmentMaster.HeaderRow != null)
                {
                    grdDepartmentMaster.UseAccessibleHeader =
                        true;

                    grdDepartmentMaster.HeaderRow.TableSection =
                        TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                btnSubmit.Text = "Submit";

                ShowMessage(
                    "Unable to edit department: "
                    + ex.Message);
            }
        }


        // ============================================================
        // ACTIVE
        // ============================================================

        protected void lnkActive_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                ChangeStatus(
                    sender,
                    1);


                ShowMessage(
                    "Department status changed to Active successfully.");

                ClearControls();

                BindGrid();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Unable to activate department: "
                    + ex.Message);
            }
        }


        // ============================================================
        // INACTIVE
        // ============================================================

        protected void lnkInactive_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                ChangeStatus(
                    sender,
                    0);


                ShowMessage(
                    "Department status changed to Inactive successfully.");

                ClearControls();

                BindGrid();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Unable to deactivate department: "
                    + ex.Message);
            }
        }


        // ============================================================
        // CHANGE STATUS
        // ============================================================

        private void ChangeStatus(
            object sender,
            int status)
        {
            LinkButton btn =
                sender as LinkButton;


            if (btn == null)
            {
                return;
            }


            int deptId =
                Convert.ToInt32(
                    btn.CommandArgument);


            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                string query = @"
                    UPDATE TBL_DEPARTMENT_MASTER
                    SET STATUS = @STATUS
                    WHERE DEPT_ID = @DEPT_ID";


                using (SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con))
                {
                    cmd.Parameters.AddWithValue(
                        "@STATUS",
                        status);


                    cmd.Parameters.AddWithValue(
                        "@DEPT_ID",
                        deptId);


                    con.Open();


                    int rowsAffected =
                        cmd.ExecuteNonQuery();


                    if (rowsAffected == 0)
                    {
                        throw new Exception(
                            "Department record not found.");
                    }
                }
            }
        }


        // ============================================================
        // ROW DATA BOUND
        // ============================================================

        protected void grdDepartmentMaster_RowDataBound(
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


            // ========================================================
            // ACTIVE RECORD
            // ========================================================

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


            // ========================================================
            // INACTIVE RECORD
            // ========================================================

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
        // MULTIPLE ENTRY - EXCEL UPLOAD
        // ============================================================

        protected void btnFileUpload_Click(
            object sender,
            EventArgs e)
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


            string filePath =
                Path.Combine(
                    uploadFolder,
                    fileName);


            flpUpload.SaveAs(filePath);


            try
            {
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
                // CHECK COLUMN
                // ====================================================

                if (!dtExcel.Columns.Contains(
                    "DEPT_NAME"))
                {
                    ShowMessage(
                        "Excel column DEPT_NAME not found.");

                    return;
                }


                using (SqlConnection con =
                    new SqlConnection(connStr))
                {
                    con.Open();


                    foreach (DataRow row
                        in dtExcel.Rows)
                    {
                        string departmentName =
                            row["DEPT_NAME"]
                                .ToString()
                                .Trim();


                        if (string.IsNullOrWhiteSpace(
                            departmentName))
                        {
                            continue;
                        }


                        // Default Active
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


                        string insertQuery = @"
                            INSERT INTO TBL_DEPARTMENT_MASTER
                            (
                                DEPT_NAME,
                                CREATED_BY,
                                CREATED_DATE,
                                STATUS
                            )
                            VALUES
                            (
                                @DEPT_NAME,
                                @CREATED_BY,
                                GETDATE(),
                                @STATUS
                            )";


                        using (SqlCommand cmd =
                            new SqlCommand(
                                insertQuery,
                                con))
                        {
                            cmd.Parameters.AddWithValue(
                                "@DEPT_NAME",
                                departmentName);


                            string createdBy = "0";


                            if (Session["USER_ID"] != null)
                            {
                                createdBy =
                                    Session["USER_ID"]
                                        .ToString();
                            }


                            cmd.Parameters.AddWithValue(
                                "@CREATED_BY",
                                createdBy);


                            cmd.Parameters.AddWithValue(
                                "@STATUS",
                                status);


                            cmd.ExecuteNonQuery();
                        }
                    }
                }


                ShowMessage(
                    "Departments uploaded successfully.");

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
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
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
        // SHOW POPUP MESSAGE
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