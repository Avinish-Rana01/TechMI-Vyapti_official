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
    public partial class Designation_Master : System.Web.UI.Page
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
                Session["DESIGNATION_ID"] = null;

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

            if (grdDesignationMaster.HeaderRow != null)
            {
                grdDesignationMaster.UseAccessibleHeader = true;

                grdDesignationMaster.HeaderRow.TableSection =
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
                            DESIGNATION_ID,
                            DESIGNATION_NAME,
                            CASE
                                WHEN STATUS = 1
                                    THEN 'Active'
                                ELSE 'Inactive'
                            END AS STATUS_TEXT
                        FROM TBL_DESIGNATION_MASTER
                        ORDER BY DESIGNATION_ID DESC";


                    using (SqlDataAdapter da =
                        new SqlDataAdapter(query, con))
                    {
                        DataTable dt =
                            new DataTable();

                        da.Fill(dt);

                        grdDesignationMaster.DataSource = dt;

                        grdDesignationMaster.DataBind();
                    }
                }


                // Keep Grid Header

                if (grdDesignationMaster.Rows.Count > 0 &&
                    grdDesignationMaster.HeaderRow != null)
                {
                    grdDesignationMaster.UseAccessibleHeader = true;

                    grdDesignationMaster.HeaderRow.TableSection =
                        TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Error while loading designation records: "
                    + ex.Message);
            }
        }


        // ============================================================
        // INSERT / UPDATE
        // ============================================================

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // ====================================================
                // VALIDATION
                // ====================================================

                if (!Page.IsValid)
                {
                    return;
                }


                string designationName =
                    txtDesignationName.Text.Trim();


                string status =
                    ddlStatus.SelectedValue.Trim();


                if (string.IsNullOrWhiteSpace(
                    designationName))
                {
                    ShowMessage(
                        "Please enter Designation Name.");

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
                    InsertDesignation(
                        designationName,
                        status);


                    ShowMessage(
                        "Designation added successfully.");


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
                    string designationId = "";


                    // First try Session

                    if (Session["DESIGNATION_ID"] != null)
                    {
                        designationId =
                            Session["DESIGNATION_ID"]
                                .ToString()
                                .Trim();
                    }


                    // Fallback to HiddenField

                    if (string.IsNullOrEmpty(
                        designationId))
                    {
                        designationId =
                            hdnID.Value
                                .Trim();
                    }


                    // No ID

                    if (string.IsNullOrEmpty(
                        designationId))
                    {
                        ShowMessage(
                            "Designation ID not found. Please select Edit again.");

                        btnSubmit.Text = "Submit";

                        return;
                    }


                    int designationIdValue;


                    if (!int.TryParse(
                        designationId,
                        out designationIdValue))
                    {
                        ShowMessage(
                            "Invalid Designation ID.");

                        btnSubmit.Text = "Submit";

                        return;
                    }


                    UpdateDesignation(
                        designationIdValue,
                        designationName,
                        status);


                    ShowMessage(
                        "Designation updated successfully.");


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
        // INSERT DESIGNATION
        // ============================================================

        private void InsertDesignation(
            string designationName,
            string status)
        {
            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                string query = @"
                    INSERT INTO TBL_DESIGNATION_MASTER
                    (
                        DESIGNATION_NAME,
                        CREATED_BY,
                        CREATED_DATE,
                        STATUS
                    )
                    VALUES
                    (
                        @DESIGNATION_NAME,
                        @CREATED_BY,
                        GETDATE(),
                        @STATUS
                    )";


                using (SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con))
                {
                    cmd.Parameters.AddWithValue(
                        "@DESIGNATION_NAME",
                        designationName);


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
        // UPDATE DESIGNATION
        // ============================================================

        private void UpdateDesignation(
            int designationId,
            string designationName,
            string status)
        {
            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                string query = @"
                    UPDATE TBL_DESIGNATION_MASTER
                    SET
                        DESIGNATION_NAME = @DESIGNATION_NAME,
                        STATUS = @STATUS
                    WHERE DESIGNATION_ID = @DESIGNATION_ID";


                using (SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con))
                {
                    cmd.Parameters.AddWithValue(
                        "@DESIGNATION_NAME",
                        designationName);


                    cmd.Parameters.AddWithValue(
                        "@STATUS",
                        Convert.ToInt32(status));


                    cmd.Parameters.AddWithValue(
                        "@DESIGNATION_ID",
                        designationId);


                    con.Open();


                    int rowsAffected =
                        cmd.ExecuteNonQuery();


                    if (rowsAffected == 0)
                    {
                        throw new Exception(
                            "Designation record was not found.");
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
            txtDesignationName.Text = "";


            ddlStatus.ClearSelection();


            if (ddlStatus.Items.Count > 0)
            {
                ddlStatus.SelectedIndex = 0;
            }


            hdnID.Value = "";


            Session["DESIGNATION_ID"] = null;


            // IMPORTANT
            // After update button becomes Submit

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
                // GET DESIGNATION ID
                //
                // Cells[0] = Sr.No
                // Cells[1] = DESIGNATION_ID
                // Cells[2] = DESIGNATION_NAME
                // Cells[3] = STATUS
                // Cells[4] = ACTION
                // ====================================================

                string designationId =
                    Server.HtmlDecode(
                        row.Cells[1].Text)
                    .Trim();


                if (string.IsNullOrEmpty(
                    designationId) ||
                    designationId == "&nbsp;")
                {
                    ShowMessage(
                        "Designation ID not found.");

                    btnSubmit.Text = "Submit";

                    return;
                }


                // ====================================================
                // STORE ID
                // ====================================================

                hdnID.Value =
                    designationId;


                Session["DESIGNATION_ID"] =
                    designationId;


                // ====================================================
                // DESIGNATION NAME
                // ====================================================

                txtDesignationName.Text =
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
                // SAME AS DEPARTMENT / CATEGORY
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

                if (grdDesignationMaster.Rows.Count > 0 &&
                    grdDesignationMaster.HeaderRow != null)
                {
                    grdDesignationMaster.UseAccessibleHeader =
                        true;

                    grdDesignationMaster.HeaderRow.TableSection =
                        TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                btnSubmit.Text = "Submit";

                ShowMessage(
                    "Unable to edit designation: "
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
                    "Designation status changed to Active successfully.");


                ClearControls();

                BindGrid();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Unable to activate designation: "
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
                    "Designation status changed to Inactive successfully.");


                ClearControls();

                BindGrid();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Unable to deactivate designation: "
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


            int designationId =
                Convert.ToInt32(
                    btn.CommandArgument);


            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                string query = @"
                    UPDATE TBL_DESIGNATION_MASTER
                    SET STATUS = @STATUS
                    WHERE DESIGNATION_ID = @DESIGNATION_ID";


                using (SqlCommand cmd =
                    new SqlCommand(
                        query,
                        con))
                {
                    cmd.Parameters.AddWithValue(
                        "@STATUS",
                        status);


                    cmd.Parameters.AddWithValue(
                        "@DESIGNATION_ID",
                        designationId);


                    con.Open();


                    int rowsAffected =
                        cmd.ExecuteNonQuery();


                    if (rowsAffected == 0)
                    {
                        throw new Exception(
                            "Designation record not found.");
                    }
                }
            }
        }


        // ============================================================
        // ROW DATA BOUND
        // ============================================================

        protected void grdDesignationMaster_RowDataBound(
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
                    "DESIGNATION_NAME"))
                {
                    ShowMessage(
                        "Excel column DESIGNATION_NAME not found.");

                    return;
                }


                using (SqlConnection con =
                    new SqlConnection(connStr))
                {
                    con.Open();


                    foreach (DataRow row
                        in dtExcel.Rows)
                    {
                        string designationName =
                            row["DESIGNATION_NAME"]
                                .ToString()
                                .Trim();


                        if (string.IsNullOrWhiteSpace(
                            designationName))
                        {
                            continue;
                        }


                        // Default Active

                        int status = 1;


                        // =================================================
                        // STATUS
                        // =================================================

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


                        // =================================================
                        // INSERT
                        // =================================================

                        string insertQuery = @"
                            INSERT INTO TBL_DESIGNATION_MASTER
                            (
                                DESIGNATION_NAME,
                                CREATED_BY,
                                CREATED_DATE,
                                STATUS
                            )
                            VALUES
                            (
                                @DESIGNATION_NAME,
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
                                "@DESIGNATION_NAME",
                                designationName);


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
                    "Designations uploaded successfully.");


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