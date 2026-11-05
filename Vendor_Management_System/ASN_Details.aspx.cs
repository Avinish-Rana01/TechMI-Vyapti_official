using Patel.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite.Vendor_Management_System
{
    public partial class ASN_Details : System.Web.UI.Page
    {
        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_VMS";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER_TYPE"] != null && Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null)
                {
                    //if (Session["MenuText"] != null)
                    //{
                    //    //lblMenuName.Text = Session["MenuText"].ToString();
                    //}
                    //FillWONo();
                    //bindGrid();

                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
                Bind_PO();
                
            }
        }
      

        private void Bind_PO()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "VMM_VIEW_PO_DETAILS"));
                GetDtl_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));

                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                DataTable dt = ds.Tables[0];

                ddlPONo.DataSource = dt;
                ddlPONo.DataTextField = "PO_NO";
                ddlPONo.DataValueField = "WO_NO";
                ddlPONo.DataBind();

                ddlPONo.Items.Insert(0, new ListItem("Select PO No", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                    "alert('" + ex.Message + "')", true);
            }
        }


        protected void ddlPONo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlWOLineItem.Items.Clear();
                ddlWOLineItem.Items.Insert(0, new ListItem("Select Line Item", ""));
                ddlEAN.Items.Clear();
                ddlEAN.Items.Insert(0, new ListItem("Select EAN", ""));
                //txtEAN.Text = "";
                txtWOLineQty.Text = "";
                txtRemainingQty.Text = "";
                txtASNQty.Text = "";

                Div_grd.Visible = false;
                grdAddASN.Visible = false;
                btnGenerateASN.Visible = false;
                div_upload_id.Visible = false;

                string poNo = ddlPONo.SelectedItem.Text.Trim();

                if (string.IsNullOrWhiteSpace(poNo))
                    return;

                List<SqlParameter> Para = new List<SqlParameter>();
                Para.Add(new SqlParameter("@ACTION", "BIND_PO_LINE_ITEM_DDL"));
                Para.Add(new SqlParameter("@PO_NO", poNo));
                Para.Add(new SqlParameter("@USER_ID", Convert.ToInt32(Session["USER_ID"])));

                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, Para.ToArray());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    ddlWOLineItem.DataSource = dt;
                    ddlWOLineItem.DataTextField = "WO_Line_Item";
                    ddlWOLineItem.DataValueField = "Material_Id";
                    ddlWOLineItem.DataBind();
                    ddlWOLineItem.Items.Insert(0, new ListItem("Select Line Item", ""));
                }
                else
                {
                    ddlWOLineItem.Items.Clear();
                    ddlWOLineItem.Items.Insert(0, new ListItem("No Line Item Found", ""));
                }

                GetWONODtl();

                if (grdAddASN.Rows.Count > 0)
                {
                    grdAddASN.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grdFinalASNGrid.Rows.Count > 0)
                {
                    grdFinalASNGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + ex.Message.Replace("'", "\\'") + "')", true);
            }
        }

        protected void ddlWOLineItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlEAN.Items.Clear();
                ddlEAN.Items.Insert(0, new ListItem("Select EAN", ""));
                txtWOLineQty.Text = "";
                txtRemainingQty.Text = "";
                txtASNQty.Text = "";

                string poNo = ddlPONo.SelectedItem.Text.Trim();
                string materialId = ddlWOLineItem.SelectedValue.Trim();

                if (string.IsNullOrWhiteSpace(poNo) || string.IsNullOrWhiteSpace(materialId))
                    return;

                List<SqlParameter> Para = new List<SqlParameter>();
                Para.Add(new SqlParameter("@ACTION", "BIND_PO_LINE_ITEM_EAN_DDL"));
                Para.Add(new SqlParameter("@PO_NO", poNo));
                Para.Add(new SqlParameter("@MATERIAL_ID", materialId));
                Para.Add(new SqlParameter("@USER_ID", Convert.ToInt32(Session["User_ID"])));

                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, Para.ToArray());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    ddlEAN.DataSource = dt;
                    ddlEAN.DataTextField = "EAN_Code";
                    ddlEAN.DataValueField = "EAN_Id";
                    ddlEAN.DataBind();
                    ddlEAN.Items.Insert(0, new ListItem("Select EAN", ""));
                }
                else
                {
                    ddlEAN.Items.Clear();
                    ddlEAN.Items.Insert(0, new ListItem("No EAN Found", ""));
                }
                GetWONODtl();

                if (grdAddASN.Rows.Count > 0)
                {
                    grdAddASN.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grdFinalASNGrid.Rows.Count > 0)
                {
                    grdFinalASNGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + ex.Message.Replace("'", "\\'") + "')", true);
            }
        }
        protected void lnkASN_No_Click(object sender, EventArgs e)
        {
            popup.Show();
            try
            {
                string _VAR_ASN_NO = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "ASN_VIEW_WO_LINE_ITEM"));
                GetDtl_Para.Add(new SqlParameter("@ASN_NO", _VAR_ASN_NO.ToString().Trim()));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                DataTable dt = ds.Tables[0];
                grdViewDetails.DataSource = dt;
                grdViewDetails.DataBind();
                if (grdViewDetails.Rows.Count > 0)
                {
                    Div_grd.Visible = false;
                    Div_grd2.Visible = true;
                    lblASNNumber.Text = _VAR_ASN_NO.ToString();
                    lblWONo.Text = dt.Rows[0]["PO_NO"].ToString();
                    lblASNDate.Text = dt.Rows[0]["ASN_Date"].ToString();
                    grdViewDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    Div_grd2.Visible = false;
                    Div_grd.Visible = false;
                    grdViewDetails.DataSource = null;
                    grdViewDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            finally
            {
                popup.Show();
                if (grdFinalASNGrid.Rows.Count > 0)
                {
                    grdFinalASNGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            if (grdFinalASNGrid.Rows.Count > 0)
            {
                grdFinalASNGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        private void GetWONODtl()
        {
            try
            {
                List<SqlParameter> GetASNDtl_Para = new List<SqlParameter>();
                GetASNDtl_Para.Add(new SqlParameter("@ACTION", "ASN_GET_LINE_ITEM_DTL"));
                GetASNDtl_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["USER_ID"])));
                //GetASNDtl_Para.Add(new SqlParameter("@WO_NO", ddlPONo.Text.Trim()));
                GetASNDtl_Para.Add(new SqlParameter("@PO_NO", ddlPONo.SelectedItem.Text.Trim()));


                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetASNDtl_Para.ToArray());
                if (ds.Tables[0].Rows.Count != 0)
                {
                    DataTable firstTable = ds.Tables[0];
                    grdAddASN.DataSource = firstTable;
                    grdAddASN.DataBind();
                    grdAddASN.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Div_grd.Visible = true;
                    grdAddASN.Visible = true;
                    grdFinalASNGrid.Visible = false;
                    btnGenerateASN.Visible = true;
                    div_upload_id.Visible = true;
                }
                else
                {
                    grdAddASN.Visible = true;
                    grdFinalASNGrid.Visible = false;
                    Div_grd.Visible = false;
                    DataTable firstTable = ds.Tables[0];
                    grdAddASN.DataSource = firstTable;
                    grdAddASN.DataBind();
                    if (grdAddASN.Rows.Count != 0)
                    {
                        btnGenerateASN.Visible = true;
                        div_upload_id.Visible = true;
                        grdAddASN.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        btnGenerateASN.Visible = false;
                        div_upload_id.Visible = false;
                    }
                }
                if (grdFinalASNGrid.Rows.Count != 0)
                {
                    grdFinalASNGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        
        public void Clear()
        {
            ddlPONo.SelectedIndex = -1;
            ddlWOLineItem.Items.Clear();
            txtASNQty.Text = "";
            txtWOLineQty.Text = "";
            txtRemainingQty.Text = "";
            ddlEAN.SelectedIndex = -1;
            grdFinalASNGrid.Visible = false;
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnGenerateASN.Visible = false;
            Div_grd.Visible = false;
            grdAddASN.DataSource = null;
            grdAddASN.DataBind();
            grdFinalASNGrid.DataSource = null;
            grdFinalASNGrid.DataBind();
        }
        protected void btnGenerateASN_Click(object sender, EventArgs e)
        {
            //return;
            string VarAsnNo = "";
            DataTable dt = null;
            try
            {
                if (ddlReceivingType.SelectedItem.Text == "Tag Inward") // TAG INWARD MEANS WITHOUT ENCODE
                {
                    if (FileUpload1.HasFile)
                    {
                        string fileExt = Path.GetExtension(FileUpload1.FileName).ToLower();

                        if (fileExt == ".csv")
                        {
                            string folderPath = Server.MapPath("~/Uploads/");

                            // Check if folder exists, if not create it
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }
                            // Save file temporarily
                            string filePath = Server.MapPath("~/Uploads/" + FileUpload1.FileName);
                            FileUpload1.SaveAs(filePath);

                            // Read CSV into DataTable
                            dt = ReadCsvFile(filePath);
                            int columnCount = dt.Columns.Count;
                            if (columnCount == 2)
                            {                              
                                bool exists = dt.AsEnumerable().Any(row => row.Field<string>("WO Number") == ddlPONo.SelectedItem.Text.Trim());

                                if (exists)
                                {
                                    int qty = 0, count=0;
                                    count = dt.Rows.Count;
                                    foreach (GridViewRow row in grdAddASN.Rows)
                                    {
                                        if (row.RowType == DataControlRowType.DataRow)
                                        {
                                            qty += Convert.ToInt32(row.Cells[4].Text);
                                            //string qtyText = row.Cells[4].Text;

                                            //int.TryParse(qtyText, out qty);
                                            
                                            //if (qty != count)
                                            //{
                                            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Missing EPC value for Line Item: " + lineItem + ".')", true);
                                            //    GetWONODtl();
                                            //    return;
                                            //}
                                        }
                                    }
                                    if (qty != count)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Missing EPC value for Work Order: " + ddlPONo.SelectedItem.Text.Trim() + ".')", true);
                                        GetWONODtl();
                                        return;
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid file. Work order missing.')", true);
                                    GetWONODtl();
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid file. Please upload a valid file.')", true);
                                GetWONODtl();
                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please upload only CSV files.')", true);
                            GetWONODtl();
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select a file to upload.')", true);
                        GetWONODtl();
                        return;
                    }

                    if (grdAddASN.Rows.Count != 0)
                    {
                        int upload_count = 0;
                        //foreach (GridViewRow row in grdAddASN.Rows)
                        //{
                        //    if (row.RowType == DataControlRowType.DataRow)
                        //    {
                                //string lineItem = row.Cells[2].Text;

                                //DataTable get_lineitem_data = dt.AsEnumerable()
                                //    .Where(r => r.Field<string>("Line Item") == lineItem)
                                //    .CopyToDataTable();

                                //if (get_lineitem_data != null)
                                //{
                                    foreach (DataRow _data_row in dt.Rows)
                                    {
                                        string WO_NO = _data_row["WO Number"].ToString().Trim();
                                        //string line_item = _data_row["Line Item"].ToString().Trim();
                                        //string Qty = _data_row["Qty"].ToString();
                                        string EPC = _data_row["EPC"].ToString().Trim();

                                        List<SqlParameter> Upload_Para = new List<SqlParameter>();
                                        Upload_Para.Add(new SqlParameter("@ACTION", "ADD_ASN_TAG_DATA"));
                                        Upload_Para.Add(new SqlParameter("@WO_NO", WO_NO.Trim()));
                                        //Upload_Para.Add(new SqlParameter("@ASN_Line_Item", line_item.Trim()));
                                        Upload_Para.Add(new SqlParameter("@EPC", EPC.Trim()));
                                        Upload_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));

                                        int rowsAffected = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Upload_Para.ToArray());
                                        upload_count++;
                                    }
                                //}
                            //}
                       // }
                        //return;
                        if (upload_count != 0)
                        {
                            string VarMsg = "";
                            string VarCode = "";
                            VarAsnNo = GET_MAX_ASN();
                            // UPDATE QTY
                            foreach (GridViewRow row in grdAddASN.Rows)
                            {
                                string asnNo = row.Cells[0].Text;
                                string item = row.Cells[1].Text;
                                string qty = row.Cells[2].Text;

                                // your processing
                                List<SqlParameter> Ins_Para = new List<SqlParameter>();
                                Ins_Para.Add(new SqlParameter("@ACTION", "ASN_SUBMIT_ASN_GENERATION"));
                                Ins_Para.Add(new SqlParameter("@WO_NO", ddlPONo.Text.Trim()));
                                Ins_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));

                                Ins_Para.Add(new SqlParameter("@ASN_NO", VarAsnNo.ToString().Trim()));
                                Ins_Para.Add(new SqlParameter("@RECEIVING_TYPE", "0"));
                                SqlParameter Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 500);
                                Msg.Direction = ParameterDirection.Output;
                                Ins_Para.Add(Msg);

                                SqlParameter Code = new SqlParameter("@CODE", SqlDbType.NVarChar, 1);
                                Code.Direction = ParameterDirection.Output;
                                Ins_Para.Add(Code);

                                //SqlParameter AsnNo = new SqlParameter("@Max_ASN_No", SqlDbType.NVarChar, 20);
                                //AsnNo.Direction = ParameterDirection.Output;
                                //Ins_Para.Add(AsnNo);

                                int rowsAffected = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Ins_Para.ToArray());
                                VarMsg = Msg.Value.ToString();
                                VarCode = Code.Value.ToString();
                            }
                            if (VarCode == "1")
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + VarMsg + "')", true);
                                //FillWONo();
                                Bind_PO();
                                Clear();
                                GetASNDtl(VarAsnNo);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + VarMsg + "')", true);
                                GetWONODtl();
                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File upload failed. Please check the file and try again')", true);
                            GetWONODtl();
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Found...!')", true);
                        GetWONODtl();
                        return;
                    }
                }
                else if (ddlReceivingType.SelectedItem.Text == "Garments Inward")  // GARMENTS INWARD MEANS WITH ENCODE
                {
                    string VarMsg = "";
                    string VarCode = "";
                    VarAsnNo = GET_MAX_ASN();
                    // UPDATE QTY
                    foreach (GridViewRow row in grdAddASN.Rows)
                    {
                        string asnNo = row.Cells[0].Text;
                        string item = row.Cells[1].Text;
                        string qty = row.Cells[2].Text;

                        // your processing
                        List<SqlParameter> Ins_Para = new List<SqlParameter>();
                        Ins_Para.Add(new SqlParameter("@ACTION", "ASN_SUBMIT_ASN_GENERATION"));
                        Ins_Para.Add(new SqlParameter("@WO_NO", ddlPONo.SelectedValue.Trim()));
                        Ins_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));

                        Ins_Para.Add(new SqlParameter("@ASN_NO", VarAsnNo.ToString().Trim()));
                        Ins_Para.Add(new SqlParameter("@RECEIVING_TYPE", "1"));
                        SqlParameter Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 500);
                        Msg.Direction = ParameterDirection.Output;
                        Ins_Para.Add(Msg);

                        SqlParameter Code = new SqlParameter("@CODE", SqlDbType.NVarChar, 1);
                        Code.Direction = ParameterDirection.Output;
                        Ins_Para.Add(Code);

                        int rowsAffected = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Ins_Para.ToArray());
                        VarMsg = Msg.Value.ToString();
                        VarCode = Code.Value.ToString();
                    }
                    if (VarCode == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + VarMsg + "')", true);
                        //FillWONo();
                        Bind_PO();
                        Clear();
                        GetASNDtl(VarAsnNo);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + VarMsg + "')", true);
                        GetWONODtl();
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('something went wrong try again...!')", true);
                    GetWONODtl();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                GetWONODtl();
                return;
            }
            finally
            {
                if (grdAddASN.Rows.Count != 0)
                {
                    grdAddASN.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdFinalASNGrid.Rows.Count != 0)
                {
                    grdFinalASNGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
        private DataTable ReadCsvFile(string filePath)
        {
            DataTable dt = new DataTable();
            bool isHeader = true;

            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] values = line.Split(',');

                if (isHeader)
                {
                    foreach (string header in values)
                    {
                        dt.Columns.Add(header.Trim());
                    }
                    isHeader = false;
                }
                else
                {
                    dt.Rows.Add(values);
                }
            }

            return dt;
        }
        public string GET_MAX_ASN()
        {
            string Get_Max_ASN_No = "";
            try
            {
                List<SqlParameter> Sel_Para = new List<SqlParameter>();
                Sel_Para.Add(new SqlParameter("@ACTION", "GET_MAX_ASN_GENERATION"));
                DataSet ds = Patel.Data.SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, Sel_Para.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Get_Max_ASN_No = ds.Tables[0].Rows[0]["ASN_NO"].ToString();
                }
                else
                {
                    Get_Max_ASN_No = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
            }

            return Get_Max_ASN_No;
        }
        private void GetASNDtl(string ASN_NUMBER)
        {
            try
            {
                List<SqlParameter> GetASNDtl_Para = new List<SqlParameter>();
                GetASNDtl_Para.Add(new SqlParameter("@ACTION", "ASN_GENERATION_COMPLETED_DTL"));
                //GetASNDtl_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));
                GetASNDtl_Para.Add(new SqlParameter("@ASN_NO", ASN_NUMBER.Trim()));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name , GetASNDtl_Para.ToArray());
                if (ds.Tables[0].Rows.Count != 0)
                {
                    DataTable firstTable = ds.Tables[0];
                    grdFinalASNGrid.DataSource = firstTable;
                    grdFinalASNGrid.DataBind();
                    grdFinalASNGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Div_grd2.Visible = true;
                    Div_grd.Visible = false;
                    grdFinalASNGrid.Visible = true;
                    grdAddASN.Visible = false;
                    div_upload_id.Visible = false;
                    btnGenerateASN.Visible = false;
                }
                else
                {
                    grdFinalASNGrid.Visible = true;
                    grdAddASN.Visible = false;
                    Div_grd2.Visible = false;
                    Div_grd.Visible = false;
                    DataTable firstTable = ds.Tables[0];
                    grdFinalASNGrid.DataSource = firstTable;
                    grdFinalASNGrid.DataBind();
                    div_upload_id.Visible = false;
                    btnGenerateASN.Visible = false;
                }
                if (grdFinalASNGrid.Rows.Count != 0)
                {
                    grdFinalASNGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        protected void btnFileClear_Click(object sender, EventArgs e)
        {
            GetWONODtl();
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                id = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetDel_Para = new List<SqlParameter>();
                GetDel_Para.Add(new SqlParameter("@ACTION", "ASN_DELETE_ADD_LINE_ITEM"));
                GetDel_Para.Add(new SqlParameter("@ASN_Detail_ID", id));
                GetDel_Para.Add(new SqlParameter("@User_ID", Session["USER_ID"]));
                int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, GetDel_Para.ToArray());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully...!')", true);
                GetWONODtl();
                id = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        protected void ddlReceivingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReceivingType.SelectedItem.Text == "Garments Inward") // Garments Inward MEANS WITH ENCODE
            {
                div_upload.Visible = false;
                btnFileClear.Visible = false;
                download_template_id.Visible = false;
            }
            else
            {
                div_upload.Visible = true;
                btnFileClear.Visible = true;
                download_template_id.Visible = true;
            }
            GetWONODtl();
        }

        protected void ddlEAN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtWOLineQty.Text = "";
                txtRemainingQty.Text = "";
                txtASNQty.Text = "";

                string poNo = ddlPONo.SelectedItem.Text.Trim();
                string materialId = ddlWOLineItem.SelectedValue.Trim();
                string eanId = ddlEAN.SelectedValue.Trim();

                if (string.IsNullOrWhiteSpace(poNo) || string.IsNullOrWhiteSpace(materialId) || string.IsNullOrWhiteSpace(eanId))
                    return;

                List<SqlParameter> Para = new List<SqlParameter>();
                Para.Add(new SqlParameter("@ACTION", "VIEW_PO_LINE_ITEM_DETAILS"));
                Para.Add(new SqlParameter("@PO_NO", poNo));
                Para.Add(new SqlParameter("@MATERIAL_ID", materialId));
                Para.Add(new SqlParameter("@EAN_ID", eanId));
                Para.Add(new SqlParameter("@USER_ID", Convert.ToInt32(Session["User_ID"])));

                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, Para.ToArray());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    txtWOLineQty.Text = row["PO_QTY"].ToString();
                    txtRemainingQty.Text = row["Remaining_Qty"].ToString();
                }
                else
                {
                    txtWOLineQty.Text = "0";
                    txtRemainingQty.Text = "0";
                }
                GetWONODtl();

                if (grdAddASN.Rows.Count > 0)
                {
                    grdAddASN.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grdFinalASNGrid.Rows.Count > 0)
                {
                    grdFinalASNGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + ex.Message.Replace("'", "\\'") + "')", true);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //string poNo = ddlPONo.SelectedValue.Trim();
                string poNo = ddlPONo.SelectedItem.Text.Trim();
                string materialId = ddlWOLineItem.SelectedValue.Trim();
                string eanId = ddlEAN.SelectedValue.Trim();
                string asnQty = txtASNQty.Text.Trim();
                string woLineQty = txtWOLineQty.Text.Trim();

                if (string.IsNullOrWhiteSpace(poNo))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage",
                        "alert('Please select PO.');", true);
                    return;
                }

                if (string.IsNullOrWhiteSpace(materialId))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage",
                        "alert('Please select Line Item.');", true);
                    return;
                }

                if (string.IsNullOrWhiteSpace(eanId))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage",
                        "alert('Please select EAN.');", true);
                    return;
                }

                if (string.IsNullOrWhiteSpace(asnQty))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage",
                        "alert('Please enter ASN Qty.');", true);
                    return;
                }

                List<SqlParameter> Ins_Para = new List<SqlParameter>();

                Ins_Para.Add(new SqlParameter("@ACTION", "ADD_ASN_LINE_ITEM"));
                Ins_Para.Add(new SqlParameter("@PO_NO", poNo));
                Ins_Para.Add(new SqlParameter("@MATERIAL_ID", materialId));
                Ins_Para.Add(new SqlParameter("@EAN_ID", eanId));
                Ins_Para.Add(new SqlParameter("@ASN_Line_Item_Qty", woLineQty));
                Ins_Para.Add(new SqlParameter("@ASN_Qty", asnQty));
                Ins_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["USER_ID"])));

                SqlParameter Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 500);
                Msg.Direction = ParameterDirection.Output;
                Ins_Para.Add(Msg);

                SqlParameter Code = new SqlParameter("@CODE", SqlDbType.NVarChar, 1);
                Code.Direction = ParameterDirection.Output;
                Ins_Para.Add(Code);

                SqlHelper.ExecuteNonQuery(
                    connstr,
                    CommandType.StoredProcedure,
                    Sp_Name,
                    Ins_Para.ToArray()
                );

                string VarMsg = Convert.ToString(Msg.Value);
                string VarCode = Convert.ToString(Code.Value);

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    GetType(),
                    "alertMessage",
                    "alert('" + VarMsg.Replace("'", "\\'") + "');",
                    true
                );

                if (VarCode == "1")
                {
                    //ddlPONo.SelectedIndex = 0;
                    ddlWOLineItem.SelectedIndex = 0;
                    ddlEAN.Items.Clear();
                    ddlEAN.Items.Insert(0, new ListItem("Select EAN", ""));
                    txtWOLineQty.Text = "";
                    txtRemainingQty.Text = "";
                    txtASNQty.Text = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "');",
                    true
                );
            }
            finally
            {
                GetWONODtl();

                if (grdAddASN.Rows.Count > 0)
                {
                    grdAddASN.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grdFinalASNGrid.Rows.Count > 0)
                {
                    grdFinalASNGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
    }
}