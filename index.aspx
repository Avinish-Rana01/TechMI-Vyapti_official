<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Markss_Retail_Suite.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type"/>
    <meta content="IE=edge" http-equiv="X-UA-Compatible"/>
    <meta content="width=device-width, initial-scale=1.0" name="viewport"/>
    <link href="images/techmi_icon.png" rel="shortcut icon" type="image/x-icon"/>
    <title>Vyapti | Markss Infotech Ltd</title>

    <link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css"/>
    <link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css"/>
    <link href="fonts/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />

    <asp:PlaceHolder ID="phLoginCss" runat="server" Visible="true">
        <link rel="stylesheet" type="text/css" href="assets/Login_files/css/main.css"/>
        <style type="text/css">
            body {
                background: #f9f9f991;
                height: 100%;
            }
            #form1 {
                height: 100%;
                margin: 0;
            }
        </style>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phHubCss" runat="server" Visible="false">
        <link href="css/main.css" rel="stylesheet" />
        <link href="css/util.css" rel="stylesheet" />
        <link href="css/bootstrap.min.css" rel="stylesheet" />
        <style type="text/css">
            /* Original Card Disabled Styling */
            .card-disabled {
                opacity: 0.52 !important;
                cursor: not-allowed !important;
                background-color: #f7f7f7 !important;
            }

            .card-disabled:hover {
                box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23) !important;
                transform: none !important;
            }

            .card-disabled .icon-img {
                filter: grayscale(100%);
            }

            .card-disabled .icon-text {
                color: #9ca3af !important;
            }

            .card-clickable {
                cursor: pointer;
            }
        </style>
    </asp:PlaceHolder>

    <script type="text/javascript" src="assets/js/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="assets/plugins/global/plugins.bundle.js"></script>
</head>
<body id="kt_body" class='<%= pnlLogin.Visible ? "auth-bg" : "" %>'>
    <form id="form1" runat="server" class="form w-100">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <!-- ======================================================== -->
        <!-- VIEW A: ORIGINAL SPLIT-SCREEN LOGIN UI (from login.aspx) -->
        <!-- ======================================================== -->
        <asp:Panel ID="pnlLogin" runat="server" Visible="true" style="height:100%;">
            <div class="d-flex flex-column flex-root" style="min-height:100vh;">
                <div class="d-flex flex-column flex-lg-row flex-column-fluid">
                    <div class="d-flex flex-column flex-column-fluid flex-center w-lg-50 p-5">
                        <div class="d-flex justify-content-between flex-column-fluid flex-column w-100 mw-450px">
                            <div class="d-flex flex-stack py-2 justify-content-center">
                                <img src="images/vyapti_logo.png" alt="logo" title="Markss Infotech Ltd" class="img-fluid" style="max-width:150px"/>
                            </div>
                            <div class="py-10">
                                <div class="mb-5">
                                    <h3 class="text-center login-title text-uppercase">RETAIL SUITE</h3>
                                </div>
                                <div class="hr-sect mb-5">
                                    SIGN IN
                                </div>
                                <div class="wrap-input100 validate-input mb-10" data-validate="Please Enter User Name">
                                    <asp:TextBox ID="txtUsername" type="text" runat="server" class="input100" autofocus="autofocus" required="required" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                    <span class="focus-input100"></span>
                                    <span class="label-input100">User Name</span>
                                </div>
                                <div class="wrap-input100 validate-input mb-5" data-validate="Password is required">
                                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" class="input100" required="required" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                    <span class="focus-input100"></span>
                                    <span class="label-input100">Password</span>
                                </div>

                                <div class="flex-sb-m w-full p-t-3 p-b-20 mb-5">
                                    <div class="contact100-form-checkbox">
                                        <input class="input-checkbox100" id="ckb1" type="checkbox" name="remember-me" onclick="togglePasswordVisibility()"/>
                                        <label class="label-checkbox100" for="ckb1">
                                            Show Password
                                        </label>
                                    </div>
                                </div>

                                <div class="container-login100-form-btn mb-5">
                                    <asp:Button ID="btnLogin" runat="server" class="login100-form-btn" Text="SIGN IN" OnClick="btnLogin_Click"></asp:Button>
                                </div>

                                <div class="text-center p-t-15 pb-5">
                                    <span class="txt2">
                                        Copyright &copy;
                                        <script type="text/javascript">document.write(new Date().getFullYear())</script>
                                        | V 1.0 | Design &amp; Developed by <br /><a href="http://www.markss.com/" target="_blank" style="text-decoration: underline;">Markss Infotech Limited</a>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="d-none d-lg-flex flex-lg-row-fluid w-50 bgi-size-cover bgi-position-y-center bgi-position-x-start bgi-no-repeat" 
                        style="background-image: url(assets/media/auth/login_bg01.png)">
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- ======================================================== -->
        <!-- VIEW B: AUTHENTICATED MODULE HUB (Initial Default UI)    -->
        <!-- ======================================================== -->
        <asp:Panel ID="pnlModules" runat="server" Visible="false">
            <div class="container">
                <!-- User Role & Sign Out -->
                <div class="d-flex justify-content-end align-items-center pt-3 gap-3">
                    <div class="text-end text-white">
                        <div class="fw-bold fs-6"><asp:Label ID="lblLoggedUser" runat="server" Text="Admin" /></div>
                        <small class="text-white-50"><asp:Label ID="lblRole" runat="server" Text="Role" /></small>
                    </div>
                    <asp:LinkButton ID="btnSignOut" runat="server" CssClass="btn btn-sm px-3 py-2 fw-semibold text-white" style="background-color:#EF5350; border-color:#EF5350;" OnClick="btnSignOut_Click">
                        <i class="fa fa-sign-out me-1"></i> Sign Out
                    </asp:LinkButton>
                </div>

                <div class="col-md-12 flex-c pt-2 p-b-30">
                    <img src="images/vyapti_logo_white.png" alt="Logo" title="Markss Infotech Ltd" style="max-width:200px"/>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="login100-form-title p-b-25">
                            <%--RETAIL SUITE--%>
                        </div>
                    </div>

                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                        <div id="cardConfig" runat="server" class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow card-clickable" onclick="handleCardClick('System Configuration', this)">
                            <img src="assets/media/icons/process.png" class="icon-img mb-2" alt="System Configuration" /> 
                            <span class="icon-text">System Configuration</span>
                        </div>
                    </div>

                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                        <div id="cardHQ" runat="server" class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow card-clickable" onclick="handleCardClick('HQ Operations', this)">
                            <img src="assets/media/icons/ho_operations.png" class="icon-img mb-2" alt="HQ Operations" /> 
                            <span class="icon-text">HQ Operations</span>
                        </div>
                    </div>

                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                        <div id="cardVMS" runat="server" class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow card-clickable" onclick="handleCardClick('Vendor Management System', this)">
                            <img src="assets/media/icons/team.png" class="icon-img mb-2" alt="Vendor Management System" /> 
                            <span class="icon-text">Vendor Management System</span>
                        </div>
                    </div>

                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                        <div id="cardWMS" runat="server" class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow card-clickable" onclick="handleCardClick('Warehouse Management System', this)">
                            <img src="assets/media/icons/inventory-management.png" class="icon-img mb-2" alt="Warehouse Management System" /> 
                            <span class="icon-text">Warehouse Management System</span>
                        </div>
                    </div>

                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                        <div id="cardStore" runat="server" class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow card-clickable" onclick="handleCardClick('Store Operations', this)">
                            <img src="assets/media/icons/store_operations.png" class="icon-img mb-2" alt="Store Operations" /> 
                            <span class="icon-text">Store Operations</span>
                        </div>
                    </div>

                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                        <div id="cardReports" runat="server" class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow card-clickable" onclick="handleCardClick('Reports & Analytics', this)">
                            <img src="assets/media/icons/seo-report.png" class="icon-img mb-2" alt="Reports & Analytics" /> 
                            <span class="icon-text">Reports & Analytics</span>
                        </div>
                    </div>

                    <div class="col-12 col-md-6 col-lg-4 col-xl-4" hidden>
                        <a href="<%= AntiTheft %>">
                            <div class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow">
                                <img src="assets/media/icons/Anth-theft.png" class="icon-img mb-2" />
                                <span class="icon-text">Anti-Theft</span>
                            </div>
                        </a>
                    </div>

                    <div class="col-12 col-md-6 col-lg-4 col-xl-4" hidden>
                        <a href="<%= POSCheckout %>">
                            <div class="col d-flex flex-column px-3 py-4 rounded-2 me-7 mb-3 card w-100 card-box-shadow">
                                <img src="assets/media/icons/pos-checkout.jpg" class="icon-img mb-2" />
                                <span class="icon-text">POS Checkout</span>
                            </div>
                        </a>
                    </div>
                </div>
            </div>

            <div class="footer py-2 d-flex flex-lg-column" id="kt_footer" style="background:none">
                <!--begin::Container-->
                <div class="container-xxl d-flex flex-column flex-md-row justify-content-end">
                    <ul class="menu menu-gray-600 menu-hover-primary fw-semibold order-1">
                        <li class="menu-item text-white">Version 1.0</li>
                    </ul>
                    <!--end::Menu-->
                </div>
                <!--end::Container-->
            </div>
        </asp:Panel>

        <!-- Hidden navigation trigger for authorized card clicks -->
        <asp:HiddenField ID="hfSelectedModule" runat="server" />
        <asp:Button ID="btnDirectNavigate" runat="server" OnClick="btnDirectNavigate_Click" style="display:none;" />
    </form>

    <script type="text/javascript" src="assets/Login_files/js/main.js"></script>

    <script type="text/javascript">
        if (typeof jQuery !== 'undefined') {
            jQuery(document).ready(function ($) {
                $('.input100').each(function () {
                    if ($(this).val().trim() !== "") {
                        $(this).addClass('has-val');
                    }
                    $(this).on('input blur change', function () {
                        if ($(this).val().trim() !== "") {
                            $(this).addClass('has-val');
                        } else {
                            $(this).removeClass('has-val');
                        }
                    });
                });
            });
        }

        function togglePasswordVisibility() {
            var passInput = document.getElementById('<%= txtPassword.ClientID %>');
            if (passInput) {
                passInput.type = (passInput.type === 'password') ? 'text' : 'password';
            }
        }

        function handleCardClick(moduleName, cardElement) {
            // If the card is disabled for this user, block click and show message
            if (cardElement.classList.contains('card-disabled')) {
                if (typeof Swal !== 'undefined') {
                    Swal.fire({
                        title: 'Access Restricted',
                        text: "You don't have access to this feature.",
                        icon: 'warning',
                        confirmButtonText: 'OK',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn btn-primary px-4 py-2'
                        }
                    });
                } else {
                    alert("You don't have access to this feature.");
                }
                return false;
            }

            // If module is allowed, trigger server-side direct navigation
            var hf = document.getElementById('<%= hfSelectedModule.ClientID %>');
            var btn = document.getElementById('<%= btnDirectNavigate.ClientID %>');
            if (hf && btn) {
                hf.value = moduleName;
                btn.click();
            }
        }
    </script>
</body>
</html>
