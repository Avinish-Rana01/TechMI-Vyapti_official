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
    public partial class Tag_Creation : System.Web.UI.Page
    {
        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_VMS";
        //DataSet ds;
        public static int WO_ID = 0;
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
                    Bind_Tag_PO();
                    //bindGrid();
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
        }
        private void Bind_Tag_PO()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "GET_TAG_CREATION_VENDOR_DATA"));
                GetDtl_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                DataTable dt = ds.Tables[0];
                ddlTagPONo.DataSource = dt;
                ddlTagPONo.DataBind();
                ddlTagPONo.DataTextField = "TAG_PO_NO";
                ddlTagPONo.DataValueField = "TAG_PO_ID";
                ddlTagPONo.DataBind();
                ddlTagPONo.Items.Insert(0, new ListItem("Select Tag PO", "")); 
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
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "GET_TAG_CREATION_VENDOR_DETAILS"));
                GetDtl_Para.Add(new SqlParameter("@TAG_PO_ID", ddlTagPONo.SelectedValue));
                //GetDtl_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                DataTable dt = ds.Tables[0];
                grdChallanDetail.DataSource = dt;
                grdChallanDetail.DataBind();
                if (grdChallanDetail.Rows.Count > 0)
                {
                    Div_grd.Visible = true;
                    Div_grd2.Visible = false;
                    grdChallanDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    Div_grd2.Visible = false;
                    Div_grd.Visible = false;
                    grdChallanDetail.DataSource = null;
                    grdChallanDetail.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }
        private void ChallanGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("PONo", typeof(string)),
                new DataColumn ("EPC_Count", typeof(string))
            });

            dt1.Rows.Add("Data", "Data");

            grdFileData.DataSource = dt1;
            grdFileData.DataBind();
            grdFileData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        private void finalGrid(string Challan)
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "GET_TAG_CHALLAN_DETAILS"));
                GetDtl_Para.Add(new SqlParameter("@CHALLAN_NO", Challan.ToString().Trim()));
                //GetDtl_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                DataTable dt = ds.Tables[0];
                grdFinalChallan.DataSource = dt;
                grdFinalChallan.DataBind();
                if (grdFinalChallan.Rows.Count > 0)
                {
                    Div_grd.Visible = false;
                    Div_grd2.Visible = true;
                    grdFinalChallan.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    Div_grd2.Visible = false;
                    Div_grd.Visible = false;
                    grdFinalChallan.DataSource = null;
                    grdFinalChallan.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }           
        }
        private void viewDetailsGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("PONo", typeof(string)),
                new DataColumn ("PODate", typeof(string)),
                new DataColumn ("TotPOTagQty", typeof(string)),
                new DataColumn ("POTagVerifyQty", typeof(string)),
                new DataColumn ("POTagRemQty", typeof(string)),
            });

            dt1.Rows.Add("Data", "Data", "Data", "Data", "Data");

            grdViewDetails.DataSource = dt1;
            grdViewDetails.DataBind();
            grdViewDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void View(object sender, EventArgs e)
        {
            bindGrid();
            ChallanGrid();
            //finalGrid();
            viewDetailsGrid();
            popup.Show();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            Div_grd2.Visible = true;
            Div_grd.Visible = false;
            if (grdFinalChallan.Rows.Count > 0)
            {
                grdFinalChallan.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void ddlTagPONo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTagPONo.SelectedIndex>0)
            {
                bindGrid();                           
            }
            else
            {               
                grdChallanDetail.DataSource = null;
                grdChallanDetail.DataBind();                
            }
            grdViewDetails.DataSource = null;
            grdViewDetails.DataBind();
            grdFileData.DataSource = null;
            grdFileData.DataBind();
            grdFinalChallan.DataSource = null;
            grdFinalChallan.DataBind();
            btnCreateChallan.Visible = false;
        }
        protected void btnFileClear_Click(object sender, EventArgs e)
        {
            if (grdChallanDetail.Rows.Count > 0)
            {                
                grdChallanDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void btnFileUpload_Click(object sender, EventArgs e)
        {           
            DataTable dt = null;
            try
            {
                if (flpUpload.HasFile)
                {
                    string fileExt = Path.GetExtension(flpUpload.FileName).ToLower();
                    if (fileExt == ".csv")
                    {
                        string folderPath = Server.MapPath("~/Uploads/");

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                       
                        string filePath = Server.MapPath("~/Uploads/" + flpUpload.FileName);
                        flpUpload.SaveAs(filePath);

                        dt = ReadCsvFile(filePath);
                        int columnCount = dt.Columns.Count;
                        if(columnCount==1)
                        {
                            int dt_remaining_cnt = dt.Rows.Count;
                            string value = grdChallanDetail.Rows[0].Cells[5].Text;
                            int grd_remaining_cnt = Convert.ToInt32(value);
                            if (grd_remaining_cnt >= dt_remaining_cnt)
                            {
                                string csvValues = string.Join(",", dt.AsEnumerable().Select(r => r["EPC"].ToString()).Distinct());
                                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                                GetDtl_Para.Add(new SqlParameter("@ACTION", "CHECK_EPC_VERIFY"));
                                GetDtl_Para.Add(new SqlParameter("@EPC_LIST", csvValues.Trim()));
                                //GetDtl_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Duplicate EPC Found')", true);
                                    grdFileData.DataSource = null;
                                    grdFileData.DataBind();
                                    Div_grd.Visible = true;
                                    grdFileData.Visible = false;
                                    btnCreateChallan.Visible = false;
                                }
                                else
                                {
                                    DataTable dt_csv = new DataTable();
                                    dt_csv.Columns.Add("PONO", typeof(string));
                                    dt_csv.Columns.Add("EPC_Count", typeof(int));

                                    if (dt.Rows.Count > 0)
                                    {
                                        DataRow dr = dt_csv.NewRow();
                                        dr["PONO"] = ddlTagPONo.SelectedItem.Text.Trim();
                                        dr["EPC_Count"] = dt.Rows.Count;

                                        dt_csv.Rows.Add(dr);
                                        btnCreateChallan.Visible = true;
                                    }
                                    grdFileData.Visible = true;
                                    grdFileData.DataSource = dt_csv;
                                    grdFileData.DataBind();
                                    Div_grd.Visible = true;
                                    Session["DT_FileData"] = dt;
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload file successfully')", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Required Quantity Cannot Be Greater Than Tag PO Quantity.')", true);
                                return;
                            }
                            
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid file. Please upload a valid file.')", true);                            
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please upload a valid csv file.')", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);               
                return;
            }
            finally
            {
                if (grdChallanDetail.Rows.Count != 0)
                {
                    grdChallanDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdFileData.Rows.Count != 0)
                {
                    grdFileData.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        public string GET_MAX_CHALLAN()
        {
            string Get_Max_Challan_No = "";
            try
            {
                List<SqlParameter> Sel_Para = new List<SqlParameter>();
                Sel_Para.Add(new SqlParameter("@ACTION", "TAG_CREATION_GET_MAX_CHALLAN"));               
                DataSet ds = Patel.Data.SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, Sel_Para.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Get_Max_Challan_No = ds.Tables[0].Rows[0]["CHALLAN_NO"].ToString();
                }
                else
                {
                    Get_Max_Challan_No = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
            }

            return Get_Max_Challan_No;


        }
        protected void btnCreateChallan_Click(object sender, EventArgs e)
        {
            string VAR_CHALLAN_NO = "";
            try
            {
                if (Session["USER_TYPE"].ToString() != "" && Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null)
                {
                    if (Session["DT_FileData"] != null)
                    {
                        DataTable dt = Session["DT_FileData"] as DataTable;                        
                        VAR_CHALLAN_NO = GET_MAX_CHALLAN();
                       
                        foreach (DataRow row in dt.Rows)
                        {
                            string epc = row["EPC"].ToString();
                            List<SqlParameter> Ins_Para = new List<SqlParameter>();
                            Ins_Para.Add(new SqlParameter("@ACTION", "TAG_CREATION_VENDOR_PO_INSERT_EPC"));
                            Ins_Para.Add(new SqlParameter("@TAG_PO_NO", ddlTagPONo.SelectedItem.Text.Trim()));
                            Ins_Para.Add(new SqlParameter("@EPC", epc.ToString().Trim()));
                            Ins_Para.Add(new SqlParameter("@CHALLAN_NO", VAR_CHALLAN_NO.ToString().Trim()));
                            Ins_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"]))); 
                            int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Ins_Para.ToArray());                           
                        }
                        List<SqlParameter> Up_qTY_Para = new List<SqlParameter>();
                        Up_qTY_Para.Add(new SqlParameter("@ACTION", "TAG_UPDATE_QTY"));
                        Up_qTY_Para.Add(new SqlParameter("@TAG_PO_NO", ddlTagPONo.SelectedItem.Text.Trim()));                       
                        Up_qTY_Para.Add(new SqlParameter("@CHALLAN_NO", VAR_CHALLAN_NO.ToString().Trim()));
                        Up_qTY_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));

                        SqlParameter parm_Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 100);
                        parm_Msg.Direction = ParameterDirection.Output;
                        Up_qTY_Para.Add(parm_Msg);

                        int q_i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Up_qTY_Para.ToArray());
                        string msg = parm_Msg.Value.ToString();
                        if (msg != "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                            Session["DT_FileData"] = null;
                        }                        
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('something went wrong try again Upload File...!')", true);                        
                    }
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            finally
            {
                finalGrid(VAR_CHALLAN_NO);
                Bind_Tag_PO();
            }
        }
        protected void lnkChallanNo_Click(object sender, EventArgs e)
        {
            try
            {
                string _VAR_CHALLAN_NO = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "TAG_CREATION_VIEW_GRID"));
                GetDtl_Para.Add(new SqlParameter("@CHALLAN_NO", _VAR_CHALLAN_NO.ToString().Trim()));                
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                DataTable dt = ds.Tables[0];
                grdViewDetails.DataSource = dt;
                grdViewDetails.DataBind();
                if (grdViewDetails.Rows.Count > 0)
                {
                    Div_grd.Visible = false;
                    Div_grd2.Visible = true;
                    lblChallanNumber.Text = _VAR_CHALLAN_NO.ToString();
                    lblPODate.Text = dt.Rows[0]["TAG_PO_DATE"].ToString();
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
                if (grdFinalChallan.Rows.Count > 0)
                {
                    grdFinalChallan.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
    }
}