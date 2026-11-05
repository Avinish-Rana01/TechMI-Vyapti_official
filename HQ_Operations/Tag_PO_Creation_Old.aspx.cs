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
    public partial class Tag_PO_Creation_Old : System.Web.UI.Page
    {
        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_HQ_OPERATION";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER_CONFIG_TYPE"] != null  && Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null)
                {
                    //if (Session["MenuText"] != null)
                    //{
                    //    //lblMenuName.Text = Session["MenuText"].ToString();
                    //}
                    Bind_WO_NO();
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }                              
            }
        }
        private void Bind_WO_NO()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "TAG_PO_CREATION_WO_DDL"));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    ddlWorkOrdNo.DataSource = dt;
                    ddlWorkOrdNo.DataBind();
                    ddlWorkOrdNo.DataTextField = "WO_NO";
                    ddlWorkOrdNo.DataValueField = "WO_ID";
                    ddlWorkOrdNo.DataBind();
                    ddlWorkOrdNo.Items.Insert(0, new ListItem("Select Work Order No", ""));
                    if (ds.Tables[1].Rows.Count>0)
                    {
                        ddlWorkOrdNo.Items.FindByValue(ds.Tables[1].Rows[0]["WO_ID"].ToString()).Selected = true;
                        ddlWorkOrdNo.Enabled = false;
                        Show_Tag_PO_Details();
                    } 
                    else
                    {
                        Div_grd.Visible = false;
                        ddlWorkOrdNo.Enabled = true;
                    }
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
        }
        private void TagPOGrid()
        {            
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ACTION", "TAG_PO_WO_GET_ADD_LINE_ITEM_DTL"));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());
                if(ds.Tables[0].Rows.Count>0)
                {
                    grdTagPO.DataSource = ds.Tables[0];
                    grdTagPO.DataBind();
                    ddlVendorName.Enabled = false;
                    ddlWorkOrdNo.Enabled = false;
                    grdTagPO.HeaderRow.TableSection = TableRowSection.TableHeader;
                    btnCreateTagPO.Visible = true;
                    //btnClear.Visible = false;
                } 
                else
                {
                    ddlWorkOrdNo.Enabled = true;
                    ddlVendorName.Enabled = true;
                    grdTagPO.DataSource = null;
                    grdTagPO.DataBind();
                    btnCreateTagPO.Visible = false;
                    //btnClear.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'Something Went Wrong Try Again...!')", true);
                return;
            }            
        }
        private void finalGrid(string tag_po_no)
        {           
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ACTION", "TAG_PO_WO_COMPLETED_DETAILS"));
                parameters.Add(new SqlParameter("@TAG_PO_NO", tag_po_no));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdFinalTagPO.DataSource = ds.Tables[0];
                    grdFinalTagPO.DataBind();
                    grdFinalTagPO.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Div_grd2.Visible = true;
                    Div_grd.Visible = false;
                }
                else
                {
                    grdFinalTagPO.DataSource = null;
                    grdFinalTagPO.DataBind();
                    Div_grd2.Visible = false;
                    Div_grd.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'Something Went Wrong Try Again...!')", true);
                return;
            }
        }
        private void viewDetailsGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("LineItem", typeof(string)),
                new DataColumn ("WOQty", typeof(string)),
            });

            dt1.Rows.Add("ABC", "1");

            grdViewDetails.DataSource = dt1;
            grdViewDetails.DataBind();
            grdViewDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();

            if (grdWODetail.Rows.Count > 0)
            {
                grdWODetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            TagPOGrid();

            if (grdFinalTagPO.Rows.Count > 0)
            {
                grdFinalTagPO.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //clearFields();
            txtPODate.Text = "";
            txtRequiredQty.Text = "";
            ddlVendorName.SelectedIndex = -1;
            ddlVendorName.Focus();
        }
        private void clearFields()
        {
            Div_grd.Visible = true;
            grdWODetail.Visible = true;
            detail_row.Visible = true;                 
            txtWOQty.Text = "";
            txtRequiredQty.Text = "";
            txtRemQty.Text = "";
            Div_grd2.Visible = false;
            //bindGrid();
            if (grdWODetail.Rows.Count > 0)
            {
                grdWODetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            TagPOGrid();
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                id = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetDel_Para = new List<SqlParameter>();
                GetDel_Para.Add(new SqlParameter("@Action", "TAG_PO_DELETE_ADD_LINE_ITEM"));
                GetDel_Para.Add(new SqlParameter("@TAG_PO_DTL_ID", id));
                GetDel_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, GetDel_Para.ToArray());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Delete Successfully...!')", true);
                //bindGrid();
                id = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
            finally
            {              
               
                if (grdWODetail.Rows.Count > 0)
                {
                    grdWODetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                TagPOGrid();
            }            
        }
        protected void ddlWorkOrdNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlWorkOrdNo.SelectedIndex>0)
            {
                Show_Tag_PO_Details();
                grdFinalTagPO.DataSource = null;
                grdFinalTagPO.DataBind();
            }
        }
        //public void Show_Tag_PO_Details()
        //{
        //    try
        //    {
        //        List<SqlParameter> parameters = new List<SqlParameter>();
        //        parameters.Add(new SqlParameter("@ACTION", "TAG_PO_WO_Dtl"));
        //        parameters.Add(new SqlParameter("@WO_ID", ddlWorkOrdNo.SelectedValue));
        //        DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            grdWODetail.DataSource = ds.Tables[0];
        //            grdWODetail.DataBind();
        //            decimal totalWOQty = 0, POQty=0;

        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                if (row["WO_Line_Item_Qty"] != DBNull.Value)
        //                {
        //                    totalWOQty += Convert.ToDecimal(row["WO_Line_Item_Qty"]);
        //                }
        //            }
        //            txtWOQty.Text = totalWOQty.ToString("0.##");

        //            POQty = Convert.ToInt32(ds.Tables[3].Rows[0].ItemArray[0]);
        //            txtRemQty.Text = (totalWOQty - POQty).ToString("0.##");

        //            Div_grd.Visible = true;

        //            if (grdWODetail.Rows.Count > 0)
        //            {
        //                grdWODetail.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            }
        //        }
        //        else
        //        {
        //            grdWODetail.DataSource = null;
        //            grdWODetail.DataBind();

        //            Div_grd.Visible = false;
        //            txtWOQty.Text = "0";
        //        }
        //        if (ds.Tables[1].Rows.Count > 0)
        //        {
        //            DataTable dt = ds.Tables[1];
        //            ddlVendorName.DataSource = dt;
        //            ddlVendorName.DataBind();
        //            ddlVendorName.DataTextField = "VENDOR_NAME";
        //            ddlVendorName.DataValueField = "VENDOR_ID";
        //            ddlVendorName.DataBind();
        //            ddlVendorName.Items.Insert(0, new ListItem("Select Vendor Name", ""));
        //            if (ds.Tables[2].Rows.Count > 0)
        //            {
        //                ddlVendorName.Items.FindByValue(ds.Tables[2].Rows[0]["VENDOR_ID"].ToString()).Selected = true;
        //                ddlVendorName.Enabled = false;
        //                txtPODate.Text = ds.Tables[2].Rows[0]["PO_Dt"].ToString();
        //            }
        //            else
        //            {
        //                txtPODate.Text = "";
        //                ddlVendorName.Enabled = true;
        //            }
        //        }
        //        else
        //        {
        //            ddlVendorName.Items.Clear();
        //        }
        //        //if (ds.Tables[3].Rows.Count > 0)
        //        //{
        //        //    DataTable dt = ds.Tables[3];
        //        //    ddlLineItem.DataSource = dt;
        //        //    ddlLineItem.DataBind();
        //        //    ddlLineItem.DataTextField = "WO_Line_Item";
        //        //    ddlLineItem.DataValueField = "EAN_ID";
        //        //    ddlLineItem.DataBind();
        //        //    ddlLineItem.Items.Insert(0, new ListItem("Select Line Item", ""));
        //        //}
        //        TagPOGrid();
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'Something Went Wrong Try Again...!')", true);
        //        return;
        //    }
        //    finally
        //    {
        //        //txtPODate.Text = "";
        //        //txtEAN.Text = "";
        //        txtRequiredQty.Text = "";
        //        //txtWOQty.Text = "";
        //    }
        //}

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try              
            {
                //if (Session["USER_ID"].ToString().Trim() != "" || Session["USER_ID"] != null)
                 if (Session["USER_ID"] != null && Session["USER_ID"].ToString().Trim() != "")
                    {
                    string date = "";
                    if (txtPODate.Text.Trim() != "")
                    {
                        string dt1 = txtPODate.Text.Trim().ToString();
                        DateTime dt = Convert.ToDateTime(txtPODate.Text.Trim().ToString());
                        date = dt.ToString("yyyy-MM-dd");
                    }
                    else
                    { }
                    List<SqlParameter> Ins_Para = new List<SqlParameter>();
                    Ins_Para.Add(new SqlParameter("@ACTION", "TAG_PO_WO__ADD_LINE_ITEM"));
                    Ins_Para.Add(new SqlParameter("@WO_ID", ddlWorkOrdNo.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@VENDOR_ID", ddlVendorName.SelectedValue.Trim()));
                    if (date == "")
                    {
                        Ins_Para.Add(new SqlParameter("@TAG_PO_DATE", null));
                    }
                    else
                    {
                        Ins_Para.Add(new SqlParameter("@TAG_PO_DATE", Convert.ToDateTime(date.ToString())));
                    }
                    //Ins_Para.Add(new SqlParameter("@EAN_ID", ddlLineItem.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@TAG_PO_Line_Item_Qty", txtRequiredQty.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@User_ID", Session["USER_ID"]));
                    //Ins_Para.Add(new SqlParameter("@EAN", txtEAN.Text.Trim()));

                    SqlParameter parm_Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 100);
                    parm_Msg.Direction = ParameterDirection.Output;
                    Ins_Para.Add(parm_Msg);

                    SqlParameter parm_Code = new SqlParameter("@CODE", SqlDbType.NVarChar, 100);
                    parm_Code.Direction = ParameterDirection.Output;
                    Ins_Para.Add(parm_Code);

                    int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Ins_Para.ToArray());
                    string msg = parm_Msg.Value.ToString();
                    string code = parm_Code.Value.ToString();
                    if (msg != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                        
                    }
                    if (code == "1")
                    {
                        clearFields();
                        GetWOQuantity();
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
                TagPOGrid();
            }
        }
        protected void btnCreateTagPO_Click(object sender, EventArgs e)
        {
            string MAX_PO_NO = "";
            try
            {
                if (Session["USER_ID"].ToString().Trim() != "" || Session["USER_ID"] != null)
                {
                    List<SqlParameter> Ins_Para = new List<SqlParameter>();
                    Ins_Para.Add(new SqlParameter("@ACTION", "TAG_PO_Generation"));
                    Ins_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                    Ins_Para.Add(new SqlParameter("@WO_ID", ddlWorkOrdNo.SelectedValue));
                    Ins_Para.Add(new SqlParameter("@VENDOR_ID", ddlVendorName.SelectedValue));
                    Ins_Para.Add(new SqlParameter("@TAG_PO_DATE", txtPODate.Text));
                    Ins_Para.Add(new SqlParameter("@TAG_PO_Line_Item_Qty", Convert.ToInt32(txtRequiredQty.Text.Trim())));

                    SqlParameter parm_Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 100);
                    parm_Msg.Direction = ParameterDirection.Output;
                    Ins_Para.Add(parm_Msg);

                    SqlParameter parm_Code = new SqlParameter("@CODE", SqlDbType.NVarChar, 100);
                    parm_Code.Direction = ParameterDirection.Output;
                    Ins_Para.Add(parm_Code);

                    SqlParameter parm_MAX = new SqlParameter("@MaxPONo", SqlDbType.NVarChar, 100);
                    parm_MAX.Direction = ParameterDirection.Output;
                    Ins_Para.Add(parm_MAX);

                    int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Ins_Para.ToArray());
                    string msg = parm_Msg.Value.ToString();
                    string code = parm_Code.Value.ToString();
                    MAX_PO_NO = parm_MAX.Value.ToString();
                    if (msg != "")
                    {
                        if (code == "1")
                        {

                            //string msg_po = "";
                            //string code_po = "";
                            //foreach (GridViewRow row in grdTagPO.Rows)
                            //{
                            //    LinkButton lnk = (LinkButton)row.FindControl("lnkDelete");
                            //    string tag_po_dtl_id = "";
                            //    if (lnk != null)
                            //    {
                            //        string text = lnk.Text;
                            //        string value = lnk.CommandArgument;
                            //        tag_po_dtl_id = value.ToString();
                            //    }

                            //    List<SqlParameter> UP_Para = new List<SqlParameter>();
                            //    UP_Para.Add(new SqlParameter("@ACTION", "UPDATE_PO_QTY"));
                            //    UP_Para.Add(new SqlParameter("@TAG_PO_NO", MAX_PO_NO.Trim()));
                            //    UP_Para.Add(new SqlParameter("@TAG_PO_DTL_ID", tag_po_dtl_id));

                            //    SqlParameter UP_parm_Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 100);
                            //    UP_parm_Msg.Direction = ParameterDirection.Output;
                            //    UP_Para.Add(UP_parm_Msg);

                            //    SqlParameter UP_parm_Code = new SqlParameter("@CODE", SqlDbType.NVarChar, 100);
                            //    UP_parm_Code.Direction = ParameterDirection.Output;
                            //    UP_Para.Add(UP_parm_Code);

                            //    int K = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, UP_Para.ToArray());

                            //    msg_po = UP_parm_Msg.Value.ToString();
                            //    code_po = UP_parm_Code.Value.ToString();
                            //}
                            //if (msg_po != "")
                            //{
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                            txtWOQty.Text = "";
                            ddlVendorName.SelectedIndex = -1;
                            txtPODate.Text = "";
                            Bind_WO_NO();
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Something went wrong try again..!')", true);

                            //}
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Something went wrong try again..!')", true);
                        }
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
                finalGrid(MAX_PO_NO);
                Show_Tag_PO_Details();
            }
        }

        protected void lnkPONo_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkPONo = (LinkButton)sender;
                string tagPONo = lnkPONo.CommandArgument.Trim();

                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "TAG_PO_View_Line_Item"));
                GetDtl_Para.Add(new SqlParameter("@TAG_PO_NO", tagPONo));

                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    grdViewDetails.DataSource = dt;
                    grdViewDetails.DataBind();

                    lblWONumber.Text = dt.Rows[0]["WO_No"].ToString();
                    lblVendorName.Text = dt.Rows[0]["VENDOR_NAME"].ToString();
                    lblPODate.Text = dt.Rows[0]["TAG_PO_DATE"].ToString();

                    if (grdViewDetails.Rows.Count > 0)
                    {
                        grdViewDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                else
                {
                    grdViewDetails.DataSource = null;
                    grdViewDetails.DataBind();

                    lblWONumber.Text = "";
                    lblVendorName.Text = "";
                    lblPODate.Text = "";
                }

                popup.Show();

                ScriptManager.RegisterStartupScript(
                    UpdatePanel1,
                    UpdatePanel1.GetType(),
                    "ShowPopup",
                    "$find('popupBehavior').show();",
                    true
                );
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
            finally
            {
                if (grdFinalTagPO.Rows.Count > 0)
                {
                    grdFinalTagPO.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }


        public void Show_Tag_PO_Details()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ddlWorkOrdNo.SelectedValue))
                {
                    txtWOQty.Text = "0";
                    txtRemQty.Text = "0";
                    grdWODetail.DataSource = null;
                    grdWODetail.DataBind();
                    Div_grd.Visible = false;
                    return;
                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ACTION", "TAG_PO_WO_Dtl"));
                parameters.Add(new SqlParameter("@WO_ID", ddlWorkOrdNo.SelectedValue.Trim()));

                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdWODetail.DataSource = ds.Tables[0];
                    grdWODetail.DataBind();
                    Div_grd.Visible = true;

                    if (grdWODetail.Rows.Count > 0)
                        grdWODetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    grdWODetail.DataSource = null;
                    grdWODetail.DataBind();
                    Div_grd.Visible = false;
                }

                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[1];
                    ddlVendorName.DataSource = dt;
                    ddlVendorName.DataTextField = "VENDOR_NAME";
                    ddlVendorName.DataValueField = "VENDOR_ID";
                    ddlVendorName.DataBind();
                    ddlVendorName.Items.Insert(0, new ListItem("Select Vendor Name", ""));

                    if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                    {
                        ListItem vendorItem = ddlVendorName.Items.FindByValue(ds.Tables[2].Rows[0]["VENDOR_ID"].ToString());

                        if (vendorItem != null)
                            vendorItem.Selected = true;

                        ddlVendorName.Enabled = false;
                        txtPODate.Text = ds.Tables[2].Rows[0]["PO_Dt"].ToString();
                    }
                    else
                    {
                        txtPODate.Text = "";
                        ddlVendorName.Enabled = true;
                    }
                }
                else
                {
                    ddlVendorName.Items.Clear();
                }

                GetWOQuantity();
                TagPOGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.Replace("'", "\\'") + "-Something Went Wrong Try Again...!')", true);
            }
            finally
            {
                txtRequiredQty.Text = "";
            }
        }

        private void GetWOQuantity()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ddlWorkOrdNo.SelectedValue))
                {
                    txtWOQty.Text = "0";
                    txtRemQty.Text = "0";
                    return;
                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ACTION", "TAG_PO_GET_WO_QTY"));
                parameters.Add(new SqlParameter("@WO_ID", ddlWorkOrdNo.SelectedValue.Trim()));

                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, parameters.ToArray());

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    txtWOQty.Text = row["WO_QTY"].ToString();
                    txtRemQty.Text = row["REMAINING_QTY"].ToString();
                }
                else
                {
                    txtWOQty.Text = "0";
                    txtRemQty.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.Replace("'", "\\'") + "')", true);
            }
        }
    }
}