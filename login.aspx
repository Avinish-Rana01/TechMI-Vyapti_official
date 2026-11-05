<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Markss_Retail_Suite.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type"/>
    <meta content="IE=edge" http-equiv="X-UA-Compatible"/>
    <meta content="width=device-width, initial-scale=1.0" name="viewport"/>
    <link href="images/techmi_icon.png" rel="shortcut icon" type="image/x-icon"/>
    <title>Vyapti | Markss Infotech Ltd | Login</title>
    <link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css"/>
    <link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css"/>
    <link href="fonts/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="assets/Login_files/css/main.css"/>
    <script type="text/javascript" src="assets/js/jquery-1.11.1.min.js"></script>
    <style type="text/css">
        .page_head:after{
            content:''!important;
        }
        .no-js #loader {
            display: none;
        }

        .js #loader {
            display: block;
            position: absolute;
            left: 100px;
            top: 0;
        }

        .se-pre-con {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url(assets/media/loader.svg) center no-repeat #fff;
        }
        body {
            background: #f9f9f991;
        }
    </style>
    <script type="text/javascript">
        $(window).load(function () {
            $(".se-pre-con").fadeOut("slow");;
        });
    </script>
</head>
<body id="kt_body"  class="auth-bg">
    <div class="se-pre-con"></div>
    <div class="d-flex flex-column flex-root">
		
    <div class="d-flex flex-column flex-lg-row flex-column-fluid">
        <div class="d-flex flex-column flex-column-fluid flex-center w-lg-50 p-5">         
        <div class="d-flex justify-content-between flex-column-fluid flex-column w-100 mw-450px">
            <div class="d-flex flex-stack py-2 justify-content-center">
               <img src="images/vyapti_logo.png" alt="logo" title="Markss Infotech Ltd" class="img-fluid" style="max-width:150px"/>
            </div>
            <div class="py-10">
            <form id="form1" runat="server" class="form w-100">
                <asp:ScriptManager ID="ScriptManager1" runat="server" >
                    </asp:ScriptManager>

                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                    <ContentTemplate>--%>
                    <div class="mb-5">
						<h3 class="text-center login-title"><asp:Label ID="lblMenuName" runat="server" CssClass="= text-uppercase" Text=""></asp:Label></h3>
					</div>
					<div class="hr-sect mb-5">
						SIGN IN
					</div>
					<div class="wrap-input100 validate-input mb-10" data-validate = "Please Enter User Name">
						<asp:TextBox ID="txtUsername" type="text" runat="server" class="input100" autofocus required="required" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
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
							<input class="input-checkbox100" id="ckb1" type="checkbox" name="remember-me" onclick="myFunction()"/>
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
                            | V 1.0 | Design & Developed by <br /><a href="http://www.markss.com/" target="_blank" style="text-decoration: underline;">Markss Infotech Limited</a>
						</span>
					</div>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </form>
            </div>         
            

        </div>
    </div>
    <div class="d-none d-lg-flex flex-lg-row-fluid w-50 bgi-size-cover bgi-position-y-center bgi-position-x-start bgi-no-repeat" 
        style="background-image: url(assets/media/auth/login_bg01.png)">  
    </div>
</div>
</div>

    <script type="text/javascript" src="assets/Login_files/vendor/jquery/jquery-3.2.1.min.js"></script>
	<script type="text/javascript" src="assets/Login_files/vendor/animsition/js/animsition.min.js"></script>
	<script type="text/javascript" src="assets/Login_files/vendor/bootstrap/js/popper.js"></script>
	<script type="text/javascript" src="assets/Login_files/vendor/bootstrap/js/bootstrap.min.js"></script>
	<%--<script type="text/javascript" src="assets/Login_files/vendor/daterangepicker/moment.min.js"></script>--%>
    <script type="text/javascript" src="assets/Login_files/js/main.js"></script>
    <script type="text/javascript" src="assets/js/moment-with-locales.min.js"></script>
    <script type="text/javascript" src="assets/js/modernizr.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.input100').each(function () {
                if ($(this).val().trim() !== "") {
                    $(this).addClass('has-val');
                }
            });
        });
        function myFunction() {
            var x = document.getElementById("txtPassword");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }
    </script>
</body>
</html>
