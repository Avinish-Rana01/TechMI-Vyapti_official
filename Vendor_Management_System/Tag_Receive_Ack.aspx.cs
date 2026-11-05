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

namespace Markss_Retail_Suite.Vendor_Management_System
{
    public partial class Tag_Receive_Ack : System.Web.UI.Page
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
                    FillWONo();
                    //bindGrid();
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
        }
        private void FillWONo()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "TAG_RECV_DDL_WO_NO"));
                GetDtl_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlWorkOrdNo.DataSource = ds.Tables[0].DefaultView;
                    ddlWorkOrdNo.DataBind();
                    ddlWorkOrdNo.DataTextField = "WO_No";
                    ddlWorkOrdNo.DataValueField = "WO_No";
                    ddlWorkOrdNo.DataBind();
                    ddlWorkOrdNo.Items.Insert(0, new ListItem("Select WO Number", ""));
                }
                else
                {
                    ddlWorkOrdNo.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            finally
            {
                grdChallanDtl.DataSource = null;
                grdChallanDtl.DataBind();
                Div_grd.Visible = false;
            }
        }
        private void FillChallaNo()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "TAG_RECV_DDL_CHALLAN"));
                GetDtl_Para.Add(new SqlParameter("@WO_NO", ddlWorkOrdNo.SelectedItem.Text.Trim()));
                GetDtl_Para.Add(new SqlParameter("@USER_ID", Convert.ToInt32(Session["User_ID"])));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name , GetDtl_Para.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlChallanNo.DataSource = ds.Tables[0].DefaultView;
                    ddlChallanNo.DataBind();
                    ddlChallanNo.DataTextField = "CHALLAN_NO";
                    ddlChallanNo.DataValueField = "CHALLAN_NO";
                    ddlChallanNo.DataBind();
                    ddlChallanNo.Items.Insert(0, new ListItem("Select ChallanNo.", ""));
                }
                else
                {
                    ddlChallanNo.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        private void bindGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("ChallanNo", typeof(string)),
                new DataColumn ("VENDOR_NAME", typeof(string)),
                new DataColumn ("WO_No", typeof(string)),
                new DataColumn ("WO_Dt", typeof(string)),
                new DataColumn ("PO_NO", typeof(string)),
                new DataColumn ("PO_Dt", typeof(string)),
                new DataColumn ("PO_Tag_Qty", typeof(string)),
                new DataColumn ("PO_Tag_Verify_Qty", typeof(string)),
            });

            dt1.Rows.Add("CH100001", "ACCU-TECH MEASUREMENT SYSTEMS", "WO2500002", "2025-05-15", "PO1001", "2025-05-15", "1", "1");

            grdChallanDtl.DataSource = dt1;
            grdChallanDtl.DataBind();
            grdChallanDtl.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        private void finalGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("Grn_No", typeof(string)),
                new DataColumn ("PO_No", typeof(string)),
                new DataColumn ("Invoice_No", typeof(string)),
                new DataColumn ("Invoice_Amt", typeof(string)),
                new DataColumn ("Invoice_Dt", typeof(string)),
                new DataColumn ("PO_Line_Item", typeof(string)),
                new DataColumn ("PO_Line_Item_Qty", typeof(string)),
                new DataColumn ("PO_Line_Item_Amt", typeof(string)),
                new DataColumn ("Invoice_Line_Item_Qty", typeof(string)),
                new DataColumn ("Transporter_Name", typeof(string)),
                new DataColumn ("Vehicle_No", typeof(string)),
                new DataColumn ("User_Name", typeof(string)),
                new DataColumn ("Entry_Dt", typeof(string)),
            });

            dt1.Rows.Add("GRN001", "PO12345", "INV001", "1000", "2025-05-15", "ABC", "1", "1000", "1", "XYZ Transport", "MH-01-AD-0001", "Admin", "2025-05-15 12:40:10");

            grdFinalInvoiceGrid.DataSource = dt1;
            grdFinalInvoiceGrid.DataBind();
            grdFinalInvoiceGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnTagRecivedConfrmation_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdChallanDtl.Rows.Count != 0)
                {
                    List<SqlParameter> Ins_Para = new List<SqlParameter>();
                    Ins_Para.Add(new SqlParameter("@ACTION", "TAG_RECV_DDL_CHALLAN_CONFIRMATION"));
                    //Ins_Para.Add(new SqlParameter("@Invoice_Id", Convert.ToInt32(ddlInvoiceNo.SelectedValue)));
                    Ins_Para.Add(new SqlParameter("@CHALLAN_NO", ddlChallanNo.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));                    

                    int rowsAffected = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Ins_Para.ToArray());
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully received')", true);
                    ddlWorkOrdNo.SelectedIndex = -1;
                    ddlChallanNo.Items.Clear();
                    FillWONo();
                    GetChallanNoDtl();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Found...!')", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                //GetInvoiceDtl();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            finally
            {
                //GetGrnDtl();
                if (grdChallanDtl.Rows.Count != 0)
                {
                    grdChallanDtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdFinalInvoiceGrid.Rows.Count != 0)
                {
                    grdFinalInvoiceGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void ddlWorkOrdNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Div_grd.Visible = false;
            grdChallanDtl.DataSource = null;
            grdChallanDtl.DataBind();
            if (ddlWorkOrdNo.SelectedIndex > 0)
            {
                FillChallaNo();
            }
            else
            {
                ddlChallanNo.Items.Clear();
            }
        }
        private void GetChallanNoDtl()
        {
            try
            {
                List<SqlParameter> GetChallanDtl_Para = new List<SqlParameter>();
                GetChallanDtl_Para.Add(new SqlParameter("@ACTION", "TAG_RECV_DDL_CHALLAN_DTL"));
                GetChallanDtl_Para.Add(new SqlParameter("@CHALLAN_NO", ddlChallanNo.Text.Trim()));
                GetChallanDtl_Para.Add(new SqlParameter("@WO_NO", ddlWorkOrdNo.Text.Trim()));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetChallanDtl_Para.ToArray());
                if (ds.Tables[0].Rows.Count != 0)
                {
                    DataTable firstTable = ds.Tables[0];
                    grdChallanDtl.DataSource = firstTable;
                    grdChallanDtl.DataBind();
                    grdChallanDtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                    btnTagRecivedConfrmation.Visible = true;
                    Div_grd.Visible = true;
                }
                else
                {
                    Div_grd.Visible = false;
                    DataTable firstTable = ds.Tables[0];
                    grdChallanDtl.DataSource = firstTable;
                    grdChallanDtl.DataBind();
                    if (grdChallanDtl.Rows.Count != 0)
                    {
                        grdChallanDtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                if (grdFinalInvoiceGrid.Rows.Count != 0)
                {
                    grdFinalInvoiceGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        protected void ddlChallanNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlChallanNo.SelectedIndex > 0)
                {
                    GetChallanNoDtl();
                }
                else
                {
                    Div_grd.Visible = false;
                    grdChallanDtl.DataSource = null;
                    grdChallanDtl.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            finally
            {
                if (grdChallanDtl.Rows.Count != 0)
                {
                    grdChallanDtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdFinalInvoiceGrid.Rows.Count != 0)
                {
                    grdFinalInvoiceGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
    }
}