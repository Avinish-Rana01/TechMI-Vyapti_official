using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Patel.Data;

namespace Markss_Retail_Suite
{
    public partial class index : System.Web.UI.Page
    {
        private readonly string ConnStr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private readonly string SP_Name = "[SP_MASTER]";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user already has an active authenticated session
                if ((Session["User_ID"] != null || Session["USER_ID"] != null) && Session["UserName"] != null)
                {
                    ShowModuleHub();
                }
                else
                {
                    ShowLoginForm();
                }
            }
        }

        private void ShowLoginForm()
        {
            phLoginCss.Visible = true;
            phHubCss.Visible = false;
            pnlLogin.Visible = true;
            pnlModules.Visible = false;
        }

        private void ShowModuleHub()
        {
            phLoginCss.Visible = false;
            phHubCss.Visible = true;
            pnlLogin.Visible = false;
            pnlModules.Visible = true;

            lblLoggedUser.Text = Session["UserName"] != null ? Session["UserName"].ToString() : "User";

            string roleDisplay = Convert.ToString(Session["ROLE_NAME"]);
            string vendorName = Convert.ToString(Session["VENDOR_NAME"]);

            if (string.IsNullOrWhiteSpace(roleDisplay))
            {
                roleDisplay = Convert.ToString(Session["USER_TYPE"]);
            }

            //lblRole.Text = !string.IsNullOrWhiteSpace(roleDisplay) ? roleDisplay : "Staff";
            lblRole.Text = roleDisplay?.Equals("Vendor", StringComparison.OrdinalIgnoreCase) == true ? $"{vendorName}" : (string.IsNullOrWhiteSpace(roleDisplay) 
                ? "Staff" : roleDisplay);

            ConfigureCardsByPermissions();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertEmpty", "alert('Please enter both User Name and Password!');", true);
                    return;
                }

                // 1. Query user assigned modules directly from database
                HashSet<string> allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                string firstNonVmsModule = "";
                bool isVendorAccount = false;

                try
                {
                    string moduleQuery = @"
                        SELECT U.USER_ID, STRING_AGG(MM.MODULE_NAME, ',') AS MODULE_NAMES 
                        FROM dbo.TBL_USER_MASTER U  
                        CROSS APPLY STRING_SPLIT(U.MODULE_ID, ',') S  
                        INNER JOIN dbo.TBL_MODULE_MASTER MM ON MM.MOD_ID = TRY_CAST(S.value AS INT)  
                        WHERE U.USER_NAME = @USER_NAME
                        GROUP BY U.USER_ID";

                    List<SqlParameter> qParams = new List<SqlParameter> { new SqlParameter("@USER_NAME", username) };
                    DataSet dsModules = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, moduleQuery, qParams.ToArray());

                    if (dsModules != null && dsModules.Tables.Count > 0 && dsModules.Tables[0].Rows.Count > 0)
                    {
                        string modString = dsModules.Tables[0].Rows[0]["MODULE_NAMES"] != DBNull.Value 
                            ? dsModules.Tables[0].Rows[0]["MODULE_NAMES"].ToString() 
                            : "";

                        foreach (string m in modString.Split(','))
                        {
                            if (!string.IsNullOrWhiteSpace(m))
                            {
                                string clean = m.Trim();
                                allowed.Add(clean);
                                if (string.IsNullOrEmpty(firstNonVmsModule) &&
                                    !clean.Equals("Vendor Management System", StringComparison.OrdinalIgnoreCase))
                                {
                                    firstNonVmsModule = clean;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Check if this is a vendor account in TBL_VENDOR_MASTER
                        string vendorCheckQuery = "SELECT COUNT(*) FROM dbo.TBL_VENDOR_MASTER WHERE USER_NAME = @USER_NAME";
                        List<SqlParameter> vParams = new List<SqlParameter> { new SqlParameter("@USER_NAME", username) };
                        object vendorCountObj = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, vendorCheckQuery, vParams.ToArray());
                        if (vendorCountObj != null && Convert.ToInt32(vendorCountObj) > 0)
                        {
                            isVendorAccount = true;
                            allowed.Add("Vendor Management System");
                        }
                    }
                }
                catch
                {
                    // Fallback
                }

                // 2. Execute SP_MASTER LOGIN_CHECK (VMS route targets TBL_VENDOR_MASTER; other routes target TBL_USER_MASTER)
                string targetModule = isVendorAccount ? "Vendor Management System" : (!string.IsNullOrEmpty(firstNonVmsModule) ? firstNonVmsModule : "HQ Operations");

                List<SqlParameter> loginParams = new List<SqlParameter>
                {
                    new SqlParameter("@Action", "LOGIN_CHECK"),
                    new SqlParameter("@USER_NAME", username),
                    new SqlParameter("@PASSWORD", password),
                    new SqlParameter("@USER_MODULE_NAME", targetModule)
                };

                DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, SP_Name, loginParams.ToArray());

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    string code = row["CODE"] != DBNull.Value ? row["CODE"].ToString() : "";
                    string msg = row["MSG"] != DBNull.Value ? row["MSG"].ToString() : "";

                    if (code == "1")
                    {
                        // Store unified session attributes
                        Session["USER_ID"] = row["USER_ID"].ToString();
                        Session["User_ID"] = row["USER_ID"].ToString();
                        Session["UserName"] = row["USER_NAME"].ToString();
                        Session["USER_CONFIG_TYPE"] = row["USER_CONFIG_TYPE"] != DBNull.Value ? row["USER_CONFIG_TYPE"].ToString() : "";
                        Session["EMAIL_ID"] = row["EMAIL_ID"] != DBNull.Value ? row["EMAIL_ID"].ToString() : "";
                        Session["DEPT_NAME"] = row["DEPT_NAME"] != DBNull.Value ? row["DEPT_NAME"].ToString() : "";
                        Session["USER_TYPE"] = row["USER_TYPE"] != DBNull.Value ? row["USER_TYPE"].ToString() : "";
                        Session["DESIGNATION"] = row["DESIGNATION"] != DBNull.Value ? row["DESIGNATION"].ToString() : "";
                        

                        if (Convert.ToString(Session["USER_TYPE"]) != "Tag Vendor" && Convert.ToString(Session["USER_TYPE"]) != "Vendor")
                        {
                            Session["ROLE_NAME"] = row["ROLE_NAME"] != DBNull.Value ? row["ROLE_NAME"].ToString() : "";
                            Session["STORE_NAME"] = row["STORE_NAME"] != DBNull.Value ? row["STORE_NAME"].ToString() : "";
                            Session["ROLE_ID"] = row["ROLE_ID"] != DBNull.Value ? row["ROLE_ID"].ToString() : "";
                            Session["STORE_ID"] = row["STORE_ID"] != DBNull.Value ? row["STORE_ID"].ToString() : "";
                            Session["WAREHOUSE_ID"] = row["WAREHOUSE_ID"] != DBNull.Value ? row["WAREHOUSE_ID"].ToString() : "";
                            //Session["VENDOR_NAME"] = row["VENDOR_NAME"] != DBNull.Value ? row["VENDOR_NAME"].ToString() : "";
                        }
                        else
                        {
                            Session["VENDOR_NAME"] = row["VENDOR_NAME"] != DBNull.Value ? row["VENDOR_NAME"].ToString() : "";
                        }

                        // Store actual allowed modules from database into session
                        Session["UserAllowedModules"] = allowed;

                        ShowModuleHub();
                    }
                    else if (code == "2")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMsg", "alert('" + msg.Replace("'", "\\'") + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMsg", "alert('" + (string.IsNullOrEmpty(msg) ? "Invalid Username or Password!" : msg.Replace("'", "\\'")) + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMsg", "alert('Invalid Username or Password!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertError", "alert('Login Error: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        private void ConfigureCardsByPermissions()
        {
            HashSet<string> allowed = Session["UserAllowedModules"] as HashSet<string> ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            SetCardState(cardConfig, allowed.Contains("System Configuration"));
            SetCardState(cardHQ, allowed.Contains("HQ Operations"));
            SetCardState(cardVMS, allowed.Contains("Vendor Management System"));
            SetCardState(cardWMS, allowed.Contains("Warehouse Management System"));
            SetCardState(cardStore, allowed.Contains("Store Operations"));
            SetCardState(cardReports, allowed.Contains("Reports & Analytics"));
        }

        private void SetCardState(HtmlGenericControl card, bool hasAccess)
        {
            if (hasAccess)
            {
                // Original active card
                card.Attributes["class"] = "col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow card-clickable";
            }
            else
            {
                // Original card with disabled click & visual cue
                card.Attributes["class"] = "col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow card-disabled";
            }
        }

        protected void btnDirectNavigate_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || (Session["User_ID"] == null && Session["USER_ID"] == null))
            {
                ShowLoginForm();
                return;
            }

            string selectedModule = hfSelectedModule.Value;
            HashSet<string> allowed = Session["UserAllowedModules"] as HashSet<string> ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Server-side security barrier
            if (!allowed.Contains(selectedModule))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "accessDenied", "Swal.fire('Access Denied', 'You do not have permission to access " + selectedModule.Replace("'", "\\'") + "', 'error');", true);
                return;
            }

            // Set session context for Master page recognition
            Session["MenuText"] = selectedModule;

            // Direct route into the selected module
            switch (selectedModule)
            {
                case "System Configuration":
                    Response.Redirect("Configuration_Module/User_Master.aspx");
                    break;
                case "HQ Operations":
                    Response.Redirect("HQ_Operations/HQ_Dashboard.aspx");
                    break;
                case "Vendor Management System":
                    Response.Redirect("Vendor_Management_System/Vendor_Dashboard.aspx");
                    break;
                case "Warehouse Management System":
                    Response.Redirect("Warehouse_Management_System/Warehouse_Dashboard.aspx");
                    break;
                case "Store Operations":
                    Response.Redirect("Store_Operations/Store_Operations_Dashboard.aspx");
                    break;
                case "Reports & Analytics":
                    Response.Redirect(VyptiReport);
                    break;
                default:
                    Response.Redirect("index.aspx");
                    break;
            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("index.aspx");
        }

        protected string VyptiReport
        {
            get
            {
                return ConfigurationManager.AppSettings["Vyapti-Report"] ?? "Reports_Dashboard.aspx";
            }
        }

        protected string AntiTheft
        {
            get
            {
                return ConfigurationManager.AppSettings["Anti-Theft"] ?? "#";
            }
        }

        protected string POSCheckout
        {
            get
            {
                return ConfigurationManager.AppSettings["POS-Checkout"] ?? "#";
            }
        }
    }
}
