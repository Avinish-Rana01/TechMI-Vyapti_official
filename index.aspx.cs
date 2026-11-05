using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Markss_Retail_Suite
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected string AntiTheft
        {
            get
            {
                return ConfigurationManager.AppSettings["Anti-Theft"];
            }
        }

        protected string VyptiReport
        {
            get
            {
                return ConfigurationManager.AppSettings["Vyapti-Report"];
            }
        }


        protected string POSCheckout
        {
            get
            {
                return ConfigurationManager.AppSettings["POS-Checkout"];
            }
        }




    }
}