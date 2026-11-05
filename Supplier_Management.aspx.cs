using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite
{
    public partial class Supplier_Management : System.Web.UI.Page
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
                txtVendorName.Focus();
            }
        }

        private void bindGrid()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]{
                new DataColumn ("VendorName", typeof(string)),
                new DataColumn ("VendorCode", typeof(string)),
                new DataColumn ("ContactPersonName", typeof(string)),
                new DataColumn ("Address", typeof(string)),
                new DataColumn ("EmailID", typeof(string)),
                new DataColumn ("Phone", typeof(string)),
                new DataColumn ("GST", typeof(string)),
            });

            dt1.Rows.Add("Markss Infotech Ltd", "M001", "XYZ","Charkop, Kandivali (West)", "abc@markss.com","1234567890", "27AAAAP0267H2ZN");

            grdSupplierDetails.DataSource = dt1;
            grdSupplierDetails.DataBind();
            grdSupplierDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Div_grd.Visible = true;
            bindGrid();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Markss Retail Suite", "alert('Data Inserted Successfully');", true);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtVendorName.Text = "";
            txtVendorCode.Text = "";
            txtContactPerson.Text = "";
            txtAddress.Text = "";
            txtEmailID.Text = "";
            txtPhone.Text = "";
            txtGST.Text = "";
            txtVendorName.Focus();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            Div_grd.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Markss Retail Suite", "alert('Record Deleted Successfully');", true);
        }
    }
}