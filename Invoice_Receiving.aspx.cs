using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite
{
    public partial class Invoice_Receiving : System.Web.UI.Page
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
                finalGrid();
            }
        }
        private void bindGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("PONo", typeof(string)),
                new DataColumn ("InvoiceNo", typeof(string)),
                new DataColumn ("InvoiceAmt", typeof(string)),
                new DataColumn ("InvoiceDate", typeof(string)),
                new DataColumn ("POLineItem", typeof(string)),
                new DataColumn ("POLineItemQty", typeof(string)),
                new DataColumn ("POLineItemAmt", typeof(string)),
                new DataColumn ("InvoiceQty", typeof(string)),
                new DataColumn ("TransporterName", typeof(string)),
                new DataColumn ("VehicleNo", typeof(string)),
            });

            dt1.Rows.Add("PO12345", "INV001", "1000", "2025-05-15", "ABC", "1", "1000", "1", "XYZ Transport", "MH-01-AD-0001");

            grdInvoice.DataSource = dt1;
            grdInvoice.DataBind();
            grdInvoice.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        private void finalGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("GRNNo", typeof(string)),
                new DataColumn ("PONo", typeof(string)),
                new DataColumn ("InvoiceNo", typeof(string)),
                new DataColumn ("InvoiceAmt", typeof(string)),
                new DataColumn ("InvoiceDate", typeof(string)),
                new DataColumn ("POLineItem", typeof(string)),
                new DataColumn ("POLineItemQty", typeof(string)),
                new DataColumn ("POLineItemAmt", typeof(string)),
                new DataColumn ("InvoiceQty", typeof(string)),
                new DataColumn ("TransporterName", typeof(string)),
                new DataColumn ("VehicleNo", typeof(string)),
                new DataColumn ("CreatedBy", typeof(string)),
                new DataColumn ("CreatedDateTime", typeof(string)),
            });

            dt1.Rows.Add("GRN001", "PO12345", "INV001", "1000", "2025-05-15", "ABC", "1", "1000", "1", "XYZ Transport", "MH-01-AD-0001", "Admin", "2025-05-15 12:40:10");

            grdFinalInvoiceGrid.DataSource = dt1;
            grdFinalInvoiceGrid.DataBind();
            grdFinalInvoiceGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}