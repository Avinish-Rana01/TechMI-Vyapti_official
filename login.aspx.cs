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

namespace Markss_Retail_Suite
{
    public partial class login : System.Web.UI.Page
    {
        string ConnStr = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        string SP_Name = "[SP_MASTER]";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string menu = Request.QueryString["menu"];
                if (!string.IsNullOrEmpty(menu))
                {
                    lblMenuName.Text = menu;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Session["MenuText"] = lblMenuName.Text;  
            //Response.Redirect("Dashboards.aspx");
            try
            {
                List<SqlParameter> GetLogin_Para = new List<SqlParameter>();
                GetLogin_Para.Add(new SqlParameter("@Action", "LOGIN_CHECK"));
                GetLogin_Para.Add(new SqlParameter("@USER_NAME", txtUsername.Text.Trim()));
                GetLogin_Para.Add(new SqlParameter("@PASSWORD", txtPassword.Text.Trim()));
                GetLogin_Para.Add(new SqlParameter("@USER_MODULE_NAME", lblMenuName.Text.Trim()));
                DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, SP_Name, GetLogin_Para.ToArray());
                if (ds.Tables[0].Rows.Count != 0)
                {
                    var Code = ds.Tables[0].Rows[0]["CODE"].ToString();
                    var Msg = ds.Tables[0].Rows[0]["MSG"].ToString();
                    if(Code=="1")
                    {
                        Session["USER_ID"] = ds.Tables[0].Rows[0]["USER_ID"].ToString();
                        Session["UserName"] = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
                        Session["USER_CONFIG_TYPE"] = ds.Tables[0].Rows[0]["USER_CONFIG_TYPE"].ToString();
                        Session["EMAIL_ID"] = ds.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                        Session["DEPT_NAME"] = ds.Tables[0].Rows[0]["DEPT_NAME"].ToString();
                        Session["USER_TYPE"] = ds.Tables[0].Rows[0]["USER_TYPE"].ToString();
                        Session["DESIGNATION"] = ds.Tables[0].Rows[0]["DESIGNATION"].ToString();

                        if (Convert.ToString(Session["USER_TYPE"]) == "Tag Vendor" || Convert.ToString(Session["USER_TYPE"]) == "Vendor")
                        {

                        }
                        else 
                        {
                            Session["ROLE_NAME"] = ds.Tables[0].Rows[0]["ROLE_NAME"].ToString();
                            Session["STORE_NAME"] = ds.Tables[0].Rows[0]["STORE_NAME"].ToString();

                            Session["ROLE_ID"] = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                            Session["STORE_ID"] = ds.Tables[0].Rows[0]["STORE_ID"].ToString();
                            Session["WAREHOUSE_ID"] = ds.Tables[0].Rows[0]["WAREHOUSE_ID"].ToString();
                        }

                        

                        string menuText = lblMenuName.Text;
                        Session["MenuText"] = menuText;
                        if (menuText == "System Configuration")
                        {
                            Response.Redirect("Configuration_Module/User_Master.aspx");
                        }
                        else if (menuText == "HQ Operations")
                        {
                            Response.Redirect("HQ_Operations/HQ_Dashboard.aspx");
                        }
                        else if (menuText == "Vendor Management System")
                        {
                            Response.Redirect("Vendor_Management_System/Vendor_Dashboard.aspx");
                        }
                        else if (menuText == "Warehouse Management System")
                        {
                            Response.Redirect("Warehouse_Management_System/Warehouse_Dashboard.aspx");
                        }
                        else if (menuText == "Store Operations")
                        {
                            //Response.Redirect("Store_Operations/Store_Dashboard.aspx");
                            Response.Redirect("Store_Operations/Store_Operations_Dashboard.aspx");
                        }
                        else if (menuText == "Reports & Analytics")
                        {
                            Response.Redirect("Reports_Dashboard.aspx");
                        }
                        else
                        {
                            Response.Redirect("login.aspx");
                        }
                        //Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                        //Session["Role"] = ds.Tables[0].Rows[0]["Role_Name"].ToString();
                        //Session["Plant"] = ds.Tables[0].Rows[0]["Plant_Code_ID"].ToString();
                        //Session["Plant_Name"] = ds.Tables[0].Rows[0]["Plant_Name"].ToString();
                        //Session["Emp_ID"] = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
                    }
                    else  if(Code=="2")
                    {
                        ScriptManager.RegisterStartupScript(this,this.GetType(),"alertRedirect","alert('" + Msg.Replace("'", "\\'") + "'); window.location='index.aspx';",true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Msg.ToString() + "')", true);                       
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Username or Password!!! ')", true);
                    //txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }           
        }
    }
}