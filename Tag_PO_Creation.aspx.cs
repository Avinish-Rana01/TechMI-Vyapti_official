using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite
{
    public partial class Tag_PO_Creation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MenuText"] != null)
                {
                    lblMenuName.Text = Session["MenuText"].ToString();
                }
                bindGrid();
                TagPOGrid();
                finalGrid();
                viewDetailsGrid();
            }
        }

        private void bindGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("WONo", typeof(string)),
                new DataColumn ("VendorName", typeof(string)),
                new DataColumn ("WODate", typeof(string)),
                new DataColumn ("WOLineItem", typeof(string)),
                new DataColumn ("WOLineItemQty", typeof(string)),
                new DataColumn ("WOLineItemPendingQty", typeof(string)),
            });

            dt1.Rows.Add("WO2500002", "Amazon", "2025-12-9", "ABC", "1","1");

            grdWODetail.DataSource = dt1;
            grdWODetail.DataBind();
            grdWODetail.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        private void TagPOGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("VendorName", typeof(string)),
                new DataColumn ("WOQty", typeof(string)),
            });

            dt1.Rows.Add("Amazon", "1");

            grdTagPO.DataSource = dt1;
            grdTagPO.DataBind();
            grdTagPO.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        private void finalGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("WONo", typeof(string)),
                new DataColumn ("PODate", typeof(string)),
            });

            dt1.Rows.Add("WO2500002", "2025-12-9");

            grdFinalTagPO.DataSource = dt1;
            grdFinalTagPO.DataBind();
            grdFinalTagPO.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        protected void View(object sender, EventArgs e)
        {

            bindGrid();
            TagPOGrid();
            finalGrid();
            viewDetailsGrid();
            popup.Show();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup.Hide();
            bindGrid();
            TagPOGrid();
            finalGrid();
        }

        protected void ddlWorkOrdNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlWorkOrdNo.SelectedIndex > 0)
            {
                Div_grd.Visible = true;
                grdWODetail.Visible = true;
                detail_row.Visible = true;
                grdTagPO.Visible = false;
                btnCreateTagPO.Visible = false;
                Div_grd2.Visible = false;
                bindGrid();
                TagPOGrid();
            }
            else
            {
                Div_grd.Visible = false;
                grdWODetail.Visible = false;
                detail_row.Visible = false;
                grdTagPO.Visible = false;
                btnCreateTagPO.Visible = false;
                Div_grd2.Visible = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Div_grd.Visible = true;
            grdWODetail.Visible = true;
            detail_row.Visible = true;
            grdTagPO.Visible = true;
            btnCreateTagPO.Visible = true;
            Div_grd2.Visible = false;
            bindGrid();
            TagPOGrid();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Markss Retail Suite", "alert('Data Inserted Successfully');", true);
        }

        protected void btnCreateTagPO_Click(object sender, EventArgs e)
        {
            Div_grd.Visible = false;
            grdWODetail.Visible = false;
            detail_row.Visible = false;
            grdTagPO.Visible = false;
            btnCreateTagPO.Visible = false;
            Div_grd2.Visible = true;
            ddlWorkOrdNo.SelectedIndex = -1;
            ddlVendorName.SelectedIndex = -1;
            ddlLineItem.SelectedIndex = -1;
            txtPODate.Text = "";
            txtWOQty.Text = "";
            txtRequiredQty.Text = "";
            finalGrid();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Markss Retail Suite", "alert('Tag PO Created Successfully');", true);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void clearFields()
        {
            Div_grd.Visible = true;
            grdWODetail.Visible = true;
            detail_row.Visible = true;
            ddlVendorName.SelectedIndex = -1;
            ddlLineItem.SelectedIndex = -1;
            txtPODate.Text = "";
            txtWOQty.Text = "";
            txtRequiredQty.Text = "";
            Div_grd2.Visible = false;
            bindGrid();
            TagPOGrid();
        }

        protected void ddlLineItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtWOQty.Text = "1";
            bindGrid();
            TagPOGrid();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            grdTagPO.Visible = false;
            btnCreateTagPO.Visible = false;
            bindGrid();
        }
    }
}