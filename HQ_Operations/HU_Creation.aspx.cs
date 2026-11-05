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

namespace Markss_Retail_Suite.HQ_Operations
{
    public partial class HU_Creation : System.Web.UI.Page
    {
        private readonly string connstr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string Sp_Name = "SP_HU_DETAILS";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER_TYPE"] != null && Session["MenuText"] != null && Session["UserName"] != null && Session["User_ID"] != null)
                {
                    FillWarehouse();
                    FillStore();
                    FillHU_Line_Item();
                    GetHUDtl();
                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
        }

        private void FillWarehouse()
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@ACTION", "GET_WAREHOUSE_DATA"),
            new SqlParameter("@USER_ID", Convert.ToInt32(Session["User_ID"]))
        };

                DataSet ds = SqlHelper.ExecuteDataset(
                    connstr,
                    CommandType.StoredProcedure,
                    Sp_Name,
                    parameters.ToArray()
                );

                ddlWarehouse.Items.Clear();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlWarehouse.DataSource = ds.Tables[0];
                    ddlWarehouse.DataTextField = "WAREHOUSE_NAME";
                    ddlWarehouse.DataValueField = "WAREHOUSE_ID";
                    ddlWarehouse.DataBind();

                    ddlWarehouse.SelectedIndex = 0;
                    ddlWarehouse.Enabled = false;
                }
                else
                {
                    ddlWarehouse.Items.Insert(0, new ListItem("Select Warehouse", ""));
                    ddlWarehouse.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + " - something went wrong try again...!')",
                    true
                );
            }
        }
        
        private void FillStore()
        {
            try
            {
                List<SqlParameter> Para = new List<SqlParameter>();
                Para.Add(new SqlParameter("@ACTION", "GET_STORE_DATA"));
                Para.Add(new SqlParameter("@USER_ID", Convert.ToInt32(Session["User_ID"])));

                DataSet ds = SqlHelper.ExecuteDataset(
                    connstr,
                    CommandType.StoredProcedure,
                    Sp_Name,
                    Para.ToArray()
                );

                ddlStore.Items.Clear();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlStore.DataSource = ds.Tables[0];
                    ddlStore.DataTextField = "STORE_NAME";
                    ddlStore.DataValueField = "STORE_ID";
                    ddlStore.DataBind();

                    ddlStore.Items.Insert(0, new ListItem("Select Store", ""));
                    ddlStore.Enabled = true;
                }
                else
                {
                    ddlStore.Items.Insert(0, new ListItem("Select Store", ""));
                    ddlStore.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "')",
                    true
                );
            }
        }

        private void FillHU_Line_Item()
        {
            try
            {
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "GET_MATERIAL_LINE_ITEM"));

                DataSet ds = SqlHelper.ExecuteDataset(
                    connstr,
                    CommandType.StoredProcedure,
                    Sp_Name,
                    GetDtl_Para.ToArray()
                );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlHULineItem.DataSource = ds.Tables[0];
                    ddlHULineItem.DataTextField = "MATERIAL_LINE_ITEM";
                    ddlHULineItem.DataValueField = "MATERIAL_ID";
                    ddlHULineItem.DataBind();

                    ddlHULineItem.Items.Insert(0, new ListItem("Select Line Item", ""));
                }
                else
                {
                    ddlHULineItem.Items.Clear();
                    ddlHULineItem.Items.Insert(0, new ListItem("Select Line Item", ""));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "-something went wrong try again...!')",
                    true
                );
            }
        }



        //private void GetHUDtl()
        //{
        //    try
        //    {
        //        List<SqlParameter> GetASNDtl_Para = new List<SqlParameter>();
        //        GetASNDtl_Para.Add(new SqlParameter("@ACTION", "HU_GET_ADD_LINE_ITEM"));
        //        GetASNDtl_Para.Add(new SqlParameter("@USER_ID", Convert.ToInt32(Session["User_ID"])));
        //        //GetASNDtl_Para.Add(new SqlParameter("@WO_NO", ddlWorkOrdNo.Text.Trim()));
        //        DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetASNDtl_Para.ToArray());
        //        if (ds.Tables[0].Rows.Count != 0)
        //        {
        //            DataTable firstTable = ds.Tables[0];
        //            grdAddHU.DataSource = firstTable;
        //            grdAddHU.DataBind();
        //            grdAddHU.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            grid_div.Visible = true;
        //            grdAddHU.Visible = true;
        //            grdFinalHUGrid.Visible = false;
        //            btnGenerateHU.Visible = true;
        //            ddlWarehouse.Enabled = false;
        //            ddlStore.Enabled = false;
        //            ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][2]);
        //        }
        //        else
        //        {
        //            //ddlWarehouse.SelectedIndex = -1;
        //            ddlWarehouse.Enabled = false;
        //            //ddlStore.SelectedIndex = -1;
        //            ddlStore.Enabled = true;
        //            grdAddHU.Visible = true;
        //            grdFinalHUGrid.Visible = false;
        //            grid_div.Visible = false;
        //            DataTable firstTable = ds.Tables[0];
        //            grdAddHU.DataSource = firstTable;
        //            grdAddHU.DataBind();
        //            if (grdAddHU.Rows.Count != 0)
        //            {
        //                btnGenerateHU.Visible = true;
        //                grdAddHU.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            }
        //            else
        //            {
        //                btnGenerateHU.Visible = false;
        //            }
        //        }
        //        if (grdFinalHUGrid.Rows.Count != 0)
        //        {
        //            grdFinalHUGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
        //        return;
        //    }
        //}


        private void GetHUDtl(bool resetStoreIfEmpty = true)
        {
            try
            {
                List<SqlParameter> GetASNDtl_Para = new List<SqlParameter>();
                GetASNDtl_Para.Add(new SqlParameter("@ACTION", "HU_GET_ADD_LINE_ITEM"));
                GetASNDtl_Para.Add(new SqlParameter("@USER_ID", Convert.ToInt32(Session["USER_ID"])));

                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetASNDtl_Para.ToArray());

                ddlWarehouse.Enabled = false;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable firstTable = ds.Tables[0];
                    grdAddHU.DataSource = firstTable;
                    grdAddHU.DataBind();
                    grdAddHU.HeaderRow.TableSection = TableRowSection.TableHeader;

                    grid_div.Visible = true;
                    grdAddHU.Visible = true;
                    grdFinalHUGrid.Visible = false;
                    btnGenerateHU.Visible = true;

                    ddlStore.Enabled = false;
                    ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][2]);
                }
                else
                {
                    if (resetStoreIfEmpty)
                    {
                        ddlStore.SelectedIndex = -1;
                        ddlStore.Enabled = true;
                    }

                    grdAddHU.Visible = true;
                    grdFinalHUGrid.Visible = false;
                    grid_div.Visible = false;

                    grdAddHU.DataSource = ds.Tables[0];
                    grdAddHU.DataBind();

                    btnGenerateHU.Visible = false;
                }

                if (grdFinalHUGrid.Rows.Count != 0)
                {
                    grdFinalHUGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.Replace("'", "\\'") + "-something went wrong try again...!')", true);
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            if (grdFinalHUGrid.Rows.Count > 0)
            {
                grdFinalHUGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            FillStore();
        }

        protected void ddlHULineItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlEAN.Items.Clear();
                ddlEAN.Items.Insert(0, new ListItem("Select EAN", ""));

                string materialId = ddlHULineItem.SelectedValue.Trim();

                if (string.IsNullOrWhiteSpace(materialId))
                    return;

                List<SqlParameter> Para = new List<SqlParameter>();
                Para.Add(new SqlParameter("@ACTION", "GET_EAN"));
                Para.Add(new SqlParameter("@MATERIAL_ID", materialId));

                DataSet ds = SqlHelper.ExecuteDataset(
                    connstr,
                    CommandType.StoredProcedure,
                    Sp_Name,
                    Para.ToArray()
                );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEAN.DataSource = ds.Tables[0];
                    ddlEAN.DataTextField = "EAN";
                    ddlEAN.DataValueField = "EAN_ID";
                    ddlEAN.DataBind();

                    ddlEAN.Items.Insert(0, new ListItem("Select EAN", ""));
                }
                else
                {
                    ddlEAN.Items.Clear();
                    ddlEAN.Items.Insert(0, new ListItem("No EAN Found", ""));
                }
                GetHUDtl(false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    GetType(),
                    "alertMessage",
                    "alert('" + ex.Message.Replace("'", "\\'") + "')",
                    true
                );
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["User_ID"] != null)
                {
                    List<SqlParameter> Ins_Para = new List<SqlParameter>();
                    Ins_Para.Add(new SqlParameter("@ACTION", "HU_Add_Line_Item"));
                    Ins_Para.Add(new SqlParameter("@WAREHOUSE_ID", ddlWarehouse.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@STORED_ID", ddlStore.SelectedValue.Trim()));
                    //Ins_Para.Add(new SqlParameter("@EAN_ID", ddlHULineItem.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@MATERIAL_ID", ddlHULineItem.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@EAN_ID", ddlEAN.SelectedValue.Trim()));
                    Ins_Para.Add(new SqlParameter("@HU_LINE_ITEM_QTY", txtHUQty.Text.Trim()));
                    Ins_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));
                    //Ins_Para.Add(new SqlParameter("@EAN", txtEAN.Text.Trim()));

                    SqlParameter Msg = new SqlParameter("@MESSAGE", SqlDbType.NVarChar, 500);
                    Msg.Direction = ParameterDirection.Output;
                    Ins_Para.Add(Msg);

                    SqlParameter Code = new SqlParameter("@CODE", SqlDbType.NVarChar, 1);
                    Code.Direction = ParameterDirection.Output;
                    Ins_Para.Add(Code);

                    int rowsAffected = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, Ins_Para.ToArray());

                    string VarMsg = Msg.Value.ToString();
                    string VarCode = Code.Value.ToString();
                    if (VarCode == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + VarMsg + "')", true);
                        ddlHULineItem.SelectedIndex = -1;
                        ddlEAN.Items.Clear();
                        ddlEAN.Items.Insert(0, new ListItem("Select EAN", ""));
                        //txtEAN.Text = "";
                        txtHUQty.Text = "";
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + VarMsg + "')", true);
                        return;
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
                GetHUDtl();
                if (grdAddHU.Rows.Count != 0)
                {
                    grdAddHU.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdFinalHUGrid.Rows.Count != 0)
                {
                    grdFinalHUGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
        public void Clear()
        {
            ddlHULineItem.SelectedIndex = 0;
            ddlEAN.Items.Clear();
            ddlEAN.Items.Insert(0, new ListItem("Select EAN", ""));
            txtHUQty.Text = "";
            grdFinalHUGrid.Visible = false;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnGenerateHU.Visible = false;
            grid_div.Visible = false;
            grdAddHU.DataSource = null;
            grdAddHU.DataBind();
            grdFinalHUGrid.DataSource = null;
            grdFinalHUGrid.DataBind();
        }

        public string GET_MAX_HU()
        {
            string Get_Max_HU_No = "";
            try
            {
                List<SqlParameter> Sel_Para = new List<SqlParameter>();
                Sel_Para.Add(new SqlParameter("@ACTION", "GET_MAX_HU_GENERATION"));
                DataSet ds = Patel.Data.SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, Sel_Para.ToArray());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Get_Max_HU_No = ds.Tables[0].Rows[0]["HU_NO"].ToString();
                }
                else
                {
                    Get_Max_HU_No = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
            }

            return Get_Max_HU_No;
        }

        protected void btnGenerateHU_Click(object sender, EventArgs e)
        {
            string VarHUNo = "";
            try
            {
                if (Session["User_ID"] != null)
                {
                    if (grdAddHU.Rows.Count != 0)
                {
                    string VarMsg = "";
                    string VarCode = "";
                    VarHUNo = GET_MAX_HU();
                    // UPDATE QTY
                    foreach (GridViewRow row in grdAddHU.Rows)
                    {
                        // your processing
                        List<SqlParameter> Ins_Para = new List<SqlParameter>();
                        Ins_Para.Add(new SqlParameter("@ACTION", "HU_SUBMIT_HU_GENERATION"));
                        //Ins_Para.Add(new SqlParameter("@WO_NO", ddlWorkOrdNo.Text.Trim()));
                        Ins_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));

                        Ins_Para.Add(new SqlParameter("@HU_NO", VarHUNo.ToString().Trim()));

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
                        Clear();
                        FillWarehouse();
                        FillStore();                        
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + VarMsg + "')", true);
                        GetHUDtl();
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Found...!')", true);
                    GetHUDtl();
                    return;
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
                GetHUDtl();
                return;
            }
            finally
            {
                GetHUDtl();
                Get_HU_Complete_Grid_Dtl(VarHUNo);
                if (grdAddHU.Rows.Count != 0)
                {
                    grdAddHU.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdFinalHUGrid.Rows.Count != 0)
                {
                    grdFinalHUGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void Get_HU_Complete_Grid_Dtl(string HU_NUMBER)
        {
            try
            {
                List<SqlParameter> GetASNDtl_Para = new List<SqlParameter>();
                GetASNDtl_Para.Add(new SqlParameter("@ACTION", "HU_GET_COMPLETED_DETAILS"));
                //GetASNDtl_Para.Add(new SqlParameter("@User_ID", Convert.ToInt32(Session["User_ID"])));
                GetASNDtl_Para.Add(new SqlParameter("@HU_NO", HU_NUMBER.Trim()));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetASNDtl_Para.ToArray());
                if (ds.Tables[0].Rows.Count != 0)
                {
                    DataTable firstTable = ds.Tables[0];
                    grdFinalHUGrid.DataSource = firstTable;
                    grdFinalHUGrid.DataBind();
                    grdFinalHUGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grid_final_div.Visible = true;
                    grid_div.Visible = false;
                    grdFinalHUGrid.Visible = true;
                    grdAddHU.Visible = false;
                    btnGenerateHU.Visible = false;
                }
                else
                {
                    grdFinalHUGrid.Visible = true;
                    grdAddHU.Visible = false;
                    grid_final_div.Visible = false;
                    grid_div.Visible = false;
                    DataTable firstTable = ds.Tables[0];
                    grdFinalHUGrid.DataSource = firstTable;
                    grdFinalHUGrid.DataBind();
                    btnGenerateHU.Visible = false;
                }
                if (grdFinalHUGrid.Rows.Count != 0)
                {
                    grdFinalHUGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                id = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetDel_Para = new List<SqlParameter>();
                GetDel_Para.Add(new SqlParameter("@ACTION", "HU_DELETE_ADD_LINE_ITEM"));
                GetDel_Para.Add(new SqlParameter("@HU_DETAILS_ID", id));
                GetDel_Para.Add(new SqlParameter("@USER_ID", Session["USER_ID"]));
                int i = SqlHelper.ExecuteNonQuery(connstr, CommandType.StoredProcedure, Sp_Name, GetDel_Para.ToArray());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully...!')", true);
                GetHUDtl();
                id = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "'-something went wrong try again...!')", true);
                return;
            }
        }

        protected void lnkHU_No_Click(object sender, EventArgs e)
        {
            popup.Show();
            try
            {
                string _VAR_HU_NO = Convert.ToString((sender as LinkButton).CommandArgument);
                List<SqlParameter> GetDtl_Para = new List<SqlParameter>();
                GetDtl_Para.Add(new SqlParameter("@ACTION", "HU_VIEW_HU_LINE_ITEM"));
                GetDtl_Para.Add(new SqlParameter("@HU_NO", _VAR_HU_NO.ToString().Trim()));
                DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, Sp_Name, GetDtl_Para.ToArray());
                DataTable dt = ds.Tables[0];
                grdViewDetails.DataSource = dt;
                grdViewDetails.DataBind();
                if (grdViewDetails.Rows.Count > 0)
                {
                    grid_div.Visible = false;
                    grid_final_div.Visible = true;
                    lblHUNumber.Text = _VAR_HU_NO.ToString();
                    lblWarehouse.Text = dt.Rows[0]["WAREHOUSE_NAME"].ToString();
                    lblStore.Text = dt.Rows[0]["STORE_NAME"].ToString();
                    lblHUDate.Text = dt.Rows[0]["HU_Date"].ToString();
                    grdViewDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    grid_div.Visible = false;
                    grid_final_div.Visible = false;
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
                if (grdFinalHUGrid.Rows.Count > 0)
                {
                    grdFinalHUGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

    }
}