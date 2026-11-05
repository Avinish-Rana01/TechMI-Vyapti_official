<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Markss_Retail_Suite.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type"/>
    <meta content="IE=edge" http-equiv="X-UA-Compatible"/>
    <meta content="width=device-width, initial-scale=1.0" name="viewport"/>
    <link href="images/techmi_icon.png" rel="shortcut icon" type="image/x-icon"/>
   <%-- <title>Markss Infotech Ltd</title>--%>
     <title>Vyapti | Markss Infotech Ltd</title>
     <link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css"/>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/util.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css"/>
   <link href="fonts/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <style>

    </style>
</head>
<body>
    <form id="form1" runat="server">
		<div class="container">
            <div class="col-md-12 flex-c pt-4  p-b-30">
			    <img src="images/vyapti_logo_white.png" alt="Logo" title="Markss Infotech Ltd" style="max-width:200px"/>
            </div>
			<div class="row">
                <div class="col-md-12">
                    <div class="login100-form-title p-b-25">
                        <%--RETAIL SUITE--%>
                    </div>
                </div>
                <%--<div class="col-md-6 col-lg-3">
                    <a href="login.aspx?menu=Dashboards">
                        <div class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow">
                            <img src="assets/media/icons/dashboard.png" class="icon-img mb-2"/> 
                            <span class="icon-text">Dashboards</span>
                        </div>
                    </a>
                </div>--%>
                <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                    <a href="login.aspx?menu=System Configuration">
                        <div class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow">
                            <img src="assets/media/icons/process.png" class="icon-img mb-2"/> 
                            <span class="icon-text">System Configuration</span>
                        </div>
                    </a>
                </div>
                <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                     <a href="login.aspx?menu=HQ Operations">
                         <div class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow">
                            <img src="assets/media/icons/ho_operations.png" class="icon-img mb-2" />
                               <span class="icon-text">HQ Operations</span>
                        </div>
                    </a>
                </div>
                <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                     <a href="login.aspx?menu=Vendor Management System">
                        <div class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow">
                            <img src="assets/media/icons/team.png" class="icon-img mb-2" />
                            <span class="icon-text">Vendor Management System</span>
                        </div>
                    </a>
                </div>
                <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                    <a href="login.aspx?menu=Warehouse Management System">
                         <div class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow">
                            <img src="assets/media/icons/inventory-management.png" class="icon-img mb-2" />
                            <span class="icon-text">Warehouse Management System</span>
                        </div>
                    </a>
                </div>
                <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                     <a href="login.aspx?menu=Store Operations">
                     <%--<a href="<%= AntiTheft %>">--%>
                         <div class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow">
                            <img src="assets/media/icons/store_operations.png" class="icon-img mb-2" />
                            <span class="icon-text">Store Operations</span>
                        </div>
                    </a>
                </div>


				<div class="col-12 col-md-6 col-lg-4 col-xl-4">
                     <%--<a href="login.aspx?menu=Reports & Analytics">--%>
                    <a href="<%= VyptiReport %>">
                         <div class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow">
                            <img src="assets/media/icons/seo-report.png" class="icon-img mb-2" />
                            <span class="icon-text">Reports & Analytics</span>
                        </div>
                    </a>
                </div>

                <div class="col-12 col-md-6 col-lg-4 col-xl-4" hidden>
                     <%--<a href="login.aspx?menu=Reports & Analytics">--%>
                    <a href="<%= AntiTheft %>">
                         <div class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow">
                            <img src="assets/media/icons/Anth-theft.png" class="icon-img mb-2" />
                            <span class="icon-text">Anti-Theft</span>
                        </div>
                    </a>
                </div>

                 <div class="col-12 col-md-6 col-lg-4 col-xl-4" hidden>
                     <%--<a href="login.aspx?menu=Reports & Analytics">--%>
                    <a href="<%= POSCheckout %>">
                         <div class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow">
                            <img src="assets/media/icons/pos-checkout.jpg" class="icon-img mb-2" />
                            <span class="icon-text">POS Checkout</span>
                        </div>
                    </a>
                </div>


			</div>
		</div>
        <div class="footer py-2 d-flex flex-lg-column " id="kt_footer" style="background:none">
            <!--begin::Container-->
            <div class=" container-xxl  d-flex flex-column flex-md-row justify-content-end">
                <ul class="menu menu-gray-600 menu-hover-primary fw-semibold order-1">
                    <li class="menu-item text-white">Version 1.0</li>
                </ul>
                <!--end::Menu-->
            </div>
            <!--end::Container-->
        </div>
	</form>
</body>
</html>
