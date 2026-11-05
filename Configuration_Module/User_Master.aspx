<%@ Page Title="" Language="C#" MasterPageFile="~/Configuration_Module/Config_Module.Master" AutoEventWireup="true" CodeBehind="User_Master.aspx.cs" Inherits="Markss_Retail_Suite.Configuration_Module.User_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Config Settings | User Master</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }

        .module-panel {
            background: #f9f9f9;
            padding: 12px;
            margin-top: 10px;
            border-left: 2px solid #0d6efd;
            border-bottom: 1px solid #ccc;
        }

        .module-table {
            width: 100%;
            border-collapse: collapse;
        }

            .module-table td {
                vertical-align: middle;
                white-space: nowrap;
                text-align: left;
            }



        .password-wrapper {
            position: relative;
            width: 100%;
        }

        .password-input {
            padding-right: 50px !important;
        }

            /* Edge ka default eye completely hide */
            .password-input::-ms-reveal,
            .password-input::-ms-clear {
                display: none !important;
            }

            /* Chrome/Edge password reveal */
            .password-input::-webkit-credentials-auto-fill-button {
                visibility: hidden;
                display: none !important;
            }

        .password-eye {
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            cursor: pointer;
            z-index: 100;
            width: 25px;
            height: 25px;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #555;
            font-size: 18px;
        }

            .password-eye:hover {
                color: #0d6efd;
            }

            .password-eye i {
                pointer-events: none;
            }

        .module-table .aspNetDisabled {
            background: none;
            padding: 0;
            border: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span page-id="User_Master" id="find_page"></span>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="toolbar py-5 pb-lg-2" id="kt_toolbar">
        <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
            <div class="page-title d-flex flex-column me-3 flex-start">
                <h1 class="d-flex text-white m-0 fs-3">
                    <asp:Label ID="lblMenuName" runat="server" Text="System Configuration"></asp:Label>
                    <span class="h-20px border-gray-500 border-start mx-3"></span>
                    <asp:Label ID="lblPageName" runat="server" Text="User Master"></asp:Label>
                </h1>
            </div>

            <div class="page-title d-flex me-3">
                <asp:RadioButton ID="rdbSingleEntry" runat="server" GroupName="status" Text="Add New User" CssClass="sidebar-toggler" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfSidebarOpen" runat="server" Value="false" />
    <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
        <div class="content flex-row-fluid" id="kt_content">
            <div class="v-sidebar-wrapper">
                <div class="v-sidebar">
                    <div class="sidebar-header">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <h4 class="mb-0">
                                    <asp:Label ID="lblTitle" runat="server" Text="USER DETAILS" Visible="true"></asp:Label>
                                </h4>
                                <button type="button" class="sidebar-close-btn" id="sidebar-close-btn">
                                    <i class="fa fa-times fa-2x"></i>
                                </button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <hr class="sidebar-hr">
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="row">

                                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
                                                Employee Name<span class="text-danger">*</span>
                                            </label>

                                            <asp:DropDownList
                                                ID="ddlEmployeeName"
                                                runat="server"
                                                Class="form-control form-control-solid custom-select2"
                                                AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"
                                                required="required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Access Role<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlRole" runat="server" class="form-control form-control-solid custom-select2" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AutoPostBack="true" Enabled="false" required="required">
                                                <%-- <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Super Admin"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Admin"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="User"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-4" id="dvStoreWarehouse" runat="server" visible="false">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700" id="lblStore" runat="server" visible="false">Store<span class="text-danger">*</span></label>
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700" id="lblWarehouse" runat="server" visible="false">Warehouse<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlStoreWarehouse" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <%-- <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtName" runat="server" class="form-control form-control-solid" placeholder="Enter Name" required="required"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Department<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" class="form-control form-control-solid custom-select2" Enabled="false" required="required">
                                                <%--  <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Software"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Sales"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Support"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <%--<asp:TextBox ID="txtDepartment" runat="server" class="form-control form-control-solid" placeholder="Enter Department" required="required"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Designation<span class="text-danger">*</span> </label>
                                            <asp:DropDownList
                                                ID="ddlDesignation"
                                                runat="server"
                                                Class="form-control form-control-solid custom-select2"
                                                Enabled="false"
                                                required="required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <%-- <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Email ID<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtEmailID" runat="server" class="form-control form-control-solid" placeholder="Enter Email ID" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Mobile No<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtMobileNo" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Mobile No" required="required"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Username<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtUsername" runat="server" class="form-control form-control-solid" placeholder="Enter Username" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--<div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Password<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPassword" runat="server" type="password" class="password form-control form-control-solid" placeholder="Enter Password" required="required"></asp:TextBox>
                                        </div>
                                        <span id="alert_msg"></span>
                                    </div>--%>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">

                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
                                                Password<span class="text-danger">*</span>
                                            </label>

                                            <div class="password-wrapper">

                                                <asp:TextBox
                                                    ID="txtPassword"
                                                    runat="server"
                                                    type="Password"
                                                    Class="password form-control form-control-solid password-input"
                                                    placeholder="Enter Password"
                                                    required="required" autocomplete="new-password">
                                                </asp:TextBox>



                                                <span
                                                    class="password-eye"
                                                    data-password-id="<%= txtPassword.ClientID %>">
                                                    <i class="fa fa-eye"></i>
                                                </span>

                                            </div>

                                        </div>

                                        <span id="alert_msg"></span>
                                    </div>



                                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Confirm Password<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtConfirmPass" runat="server" type="password" class="confirm_password form-control form-control-solid password-input" placeholder="Enter Confirm Password" required="required" autocomplete="new-password"></asp:TextBox>
                                        </div>
                                        <span id="alert_msg2"></span>
                                    </div>
                          
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
                                                Status<span class="text-danger">*</span>
                                            </label>

                                            <asp:DropDownList
                                                ID="ddlStatus"
                                                runat="server"
                                                CssClass="form-control form-control-solid custom-select2"
                                                required="required">

                                                <%--<asp:ListItem Value="" Text="Select Status"></asp:ListItem>--%>
                                                <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="col-12 col-md-12 col-lg-12 col-xl-12">
                                <h5 class="hr-sect2">Module Access</h5>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="radio-black">
                                            <%--<asp:RadioButton ID="rdbHQManualEntry" runat="server" GroupName="status" Text="Manual Entry" />
                                                    <asp:RadioButton ID="rdbHQERP" runat="server" GroupName="status" Text="SAP/ERP Integration" />--%>
                                            <asp:RadioButtonList ID="rblHQConfig" runat="server" RepeatDirection="Horizontal" CssClass="module_radio">
                                                <asp:ListItem Text="Manual Entry" Value="Manual Entry" />
                                                <asp:ListItem Text="SAP/ERP Integration" Value="SAP/ERP Integration" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <br />

                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkAllMain" runat="server" Text="Select All Modules" AutoPostBack="true" OnCheckedChanged="chkAllMain_CheckedChanged" />
                                        <br />

                                        <table class="module-table">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkConfiguration" runat="server" AutoPostBack="true" Text="System Configuration" OnCheckedChanged="chkConfiguration_CheckedChanged" /></td>
                                                <td>
                                                    <asp:CheckBox ID="chkHQ" runat="server" Text="HQ Operations" AutoPostBack="true" OnCheckedChanged="chkHQ_CheckedChanged" /></td>
                                                <%--<td><asp:CheckBox ID="chkVMS" runat="server" Text="Vendor Management System" AutoPostBack="true" OnCheckedChanged="chkVMS_CheckedChanged1" /></td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkWMS" runat="server" Text="Warehouse Management System" AutoPostBack="true" OnCheckedChanged="chkWMS_CheckedChanged1" /></td>
                                                <td>
                                                    <asp:CheckBox ID="chkStore" runat="server" Text="Store Operations" AutoPostBack="true" OnCheckedChanged="chkStore_CheckedChanged" /></td>
                                                <td>
                                                    <asp:CheckBox ID="chkReports" runat="server" Text="Reports & Analytics" AutoPostBack="true" OnCheckedChanged="chkReports_CheckedChanged" /></td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="pnlHQ" runat="server" Visible="false" CssClass="module-panel">

                                            <h6 class="hr-sect2">HQ Operations</h6>

                                            <asp:CheckBox ID="chkMDM" runat="server" Text="Master Data Management" AutoPostBack="true" OnCheckedChanged="chkMDM_CheckedChanged" />
                                            <asp:CheckBoxList ID="chkMDMSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="Category Master" />
                                                <asp:ListItem Text="Material Master" />
                                                <asp:ListItem Text="EAN Master" />
                                                <asp:ListItem Text="Department Master" />
                                                <asp:ListItem Text="Designation Master" />
                                                <asp:ListItem Text="Employee Master" />
                                                <asp:ListItem Text="Vendor Management" />
                                                <asp:ListItem Text="Store Registration" />
                                                <asp:ListItem Text="Warehouse Registration" />

                                                <%-- <asp:ListItem Text="Brand Master" />
                                                <asp:ListItem Text="HSN Master" />
                                                <asp:ListItem Text="Product Master" />
                                                <asp:ListItem Text="Location Master" />--%>
                                                <%--<asp:ListItem Text="Reader Master" />--%>
                                            </asp:CheckBoxList>
                                            <hr />

                                            <asp:CheckBox ID="chkProcurement" runat="server" Text="Procurement Management" AutoPostBack="true" OnCheckedChanged="chkProcurement_CheckedChanged" />

                                            <asp:CheckBoxList ID="chkProcurementSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="Work Order Creation" />
                                                <asp:ListItem Text="Tag PO Creation" />
                                                <%-- <asp:ListItem Text="Tag Creation" />--%>
                                                <asp:ListItem Text="View Work Order Details" />
                                            </asp:CheckBoxList>

                                            <hr />

                                            <asp:CheckBox ID="chkWDM" runat="server" Text="Warehouse & Management" AutoPostBack="true" OnCheckedChanged="chkWDM_CheckedChanged" />

                                            <asp:CheckBoxList ID="chkWDMSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="HU Creation" />
                                                <asp:ListItem Text="View HU Details" />
                                                <%--<asp:ListItem Text="Inter-Branch Transfer" />
                                                <asp:ListItem Text="Logistics Planning" />--%>
                                            </asp:CheckBoxList>

                                        </asp:Panel>

                                        <%--<asp:Panel ID="pnlVMS" runat="server" Visible="false" CssClass="module-panel">

                                            <h6 class="hr-sect2">Vendor Management System</h6>
                                             <asp:CheckBox ID="chkTagCreation" runat="server" Text="Tag Creation" />
                                            <br />
                                            <asp:CheckBox ID="chkWOD" runat="server" Text="Work Order Details" />
                                            <br />
                                            <asp:CheckBox ID="chkTRA" runat="server" Text="Tag Receive Acknowledgement" />
                                            <br />
                                            <asp:CheckBox ID="chkASN" runat="server" Text="ASN Details" />

                                        </asp:Panel>--%>

                                        <asp:Panel ID="pnlWMS" runat="server" Visible="false" CssClass="module-panel">

                                            <h6 class="hr-sect2">Warehouse Management System</h6>
                                            <asp:CheckBox ID="chkInbOP" runat="server" Text="Inbound Operations" AutoPostBack="true" OnCheckedChanged="chkInbOP_CheckedChanged" />

                                            <asp:CheckBoxList ID="chkInbOPSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="Inbound / Receiving (GRN)" />
                                                <asp:ListItem Text="Quality Check (QC)" />
                                                <asp:ListItem Text="Put-Away" />
                                            </asp:CheckBoxList>

                                            <hr />

                                            <asp:CheckBox ID="chkInvCon" runat="server" Text="Inventory Control" AutoPostBack="true" OnCheckedChanged="chkInvCon_CheckedChanged" />

                                            <asp:CheckBoxList ID="chkInvConSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="Inventory (Live Stock)" />
                                                <asp:ListItem Text="Stock Reservation" />
                                                <asp:ListItem Text="Stock Adjustment" />
                                                <asp:ListItem Text="Cycle Count / Physical Verification" />
                                            </asp:CheckBoxList>

                                            <hr />

                                            <asp:CheckBox ID="chkOutOP" runat="server" Text="Outbound Operations" AutoPostBack="true" OnCheckedChanged="chkOutOP_CheckedChanged" />

                                            <asp:CheckBoxList ID="chkOutOPSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="Picking" />
                                                <asp:ListItem Text="Packing" />
                                                <asp:ListItem Text="Dispatch / Outbound" />
                                            </asp:CheckBoxList>

                                            <hr />

                                            <asp:CheckBox ID="chkInterMov" runat="server" Text="Internal Movements" AutoPostBack="true" OnCheckedChanged="chkInterMov_CheckedChanged" />

                                            <asp:CheckBoxList ID="chkInterMovSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="Inter-Warehouse / Inter-Store Transfer" />
                                            </asp:CheckBoxList>

                                            <hr />

                                            <asp:CheckBox ID="chkRevLog" runat="server" Text="Reverse Logistics" AutoPostBack="true" OnCheckedChanged="chkRevLog_CheckedChanged" />

                                            <asp:CheckBoxList ID="chkRevLogSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="Returns (Store / Customer / RTV)" />
                                            </asp:CheckBoxList>

                                              <hr />

                                            <asp:CheckBox ID="chkWHInwOp" runat="server" Text="Inward Operations" AutoPostBack="true" OnCheckedChanged="chkWHInwOp_CheckedChanged" />

                                            <asp:CheckBoxList ID="chkWHInwOpSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="View HU Details" />
                                            </asp:CheckBoxList>


                                        </asp:Panel>

                                        <asp:Panel ID="pnlStore" runat="server" Visible="false" CssClass="module-panel">
                                            <h6 class="hr-sect2">Store Operations</h6>

                                            <asp:CheckBox ID="chkInwOp" runat="server" Text="Inward Operations" AutoPostBack="true" OnCheckedChanged="chkInwOp_CheckedChanged" />

                                            <asp:CheckBoxList ID="chkInwOpSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="View GRC Details" />
                                            </asp:CheckBoxList>

                                             <hr />

                                            <asp:CheckBox ID="chkOutwardOps" runat="server" Text="Outward Operations" AutoPostBack="true" OnCheckedChanged="chkOutwardOps_CheckedChanged" />

                                            <asp:CheckBoxList ID="chkOutwardOpsSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="POS Checkout" />
                                            </asp:CheckBoxList>

                                            <hr />

                                            <asp:CheckBox ID="chkAntiTheft" runat="server" Text="Anti Theft" AutoPostBack="true" OnCheckedChanged="chkAntiTheft_CheckedChanged" />

                                            <asp:CheckBoxList ID="chkAntiTheftSub" runat="server" CssClass="checkbox_list">
                                                <asp:ListItem Text="Loss Prevention" />
                                                
                                            </asp:CheckBoxList>

                                        </asp:Panel>

                                        <asp:Panel ID="pnlReports" runat="server" Visible="false" CssClass="module-panel">
                                            <h6 class="hr-sect2">Reports & Analytics</h6>
                                        </asp:Panel>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <div class="mt-1 col-md-12 col-lg-12 flex flex-center justify-content-center">
                                        <asp:Button ID="btnSubmit" runat="server" class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Submit" UseSubmitBehavior="false" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnClear" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear" UseSubmitBehavior="false" CausesValidation="false" OnClick="btnClear_Click" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

            <div class="v-sidebar-fade"></div>

            <div class="card mt-3  mb-15" id="Div_grd" runat="server" visible="true">
                <div class="card-body">
                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdUserMaster" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%" Visible="true" OnRowDataBound="grdUserMaster_RowDataBound">
                            
                                <Columns>
    <asp:TemplateField HeaderText="Sr.No">
        <ItemTemplate>
            <%# Container.DataItemIndex + 1 %>
        </ItemTemplate>
    </asp:TemplateField>

    <asp:BoundField DataField="ROLE_NAME" HeaderText="Access Role" />

    <asp:BoundField DataField="EMP_NAME" HeaderText="EMP_NAME" />

    <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" />

    <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" />

    <asp:BoundField DataField="USER_NAME" HeaderText="Username" />

    <asp:TemplateField HeaderText="Module Access">
        <ItemTemplate>
            <asp:LinkButton
                ID="lnkViewModel"
                runat="server"
                CssClass="text-theme-green"
                OnClick="lnkViewModel_Click"
                CommandArgument='<%# Eval("USER_ID") %>'>
                <i class="fa fa-eye text-theme-green"></i> View Module Access
            </asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>

    <asp:BoundField DataField="STATUS" HeaderText="Status" />

    <asp:TemplateField HeaderText="Action">
        <ItemTemplate>
            <asp:LinkButton
                ID="lnkEdit"
                runat="server"
                CssClass="text-theme-green"
                OnClick="lnkEdit_Click"
                CommandArgument='<%# Eval("USER_ID") %>'>
                <i class="fa fa-edit text-theme-green"></i> Edit
            </asp:LinkButton>

            &nbsp;&nbsp; | &nbsp;

            <asp:LinkButton
                ID="lnlActive"
                runat="server"
                CssClass="text-theme-green"
                OnClick="lnlActive_Click"
                CommandArgument='<%# Eval("USER_ID") %>'
                OnClientClick="return confirm('Are you sure you want to active this User?');">
                <i class="fa fa-check text-theme-green"></i> Active
            </asp:LinkButton>

            <asp:LinkButton
                ID="lnkInactive"
                runat="server"
                CssClass="text-theme-red"
                OnClick="lnkInactive_Click"
                CommandArgument='<%# Eval("USER_ID") %>'
                OnClientClick="return confirm('Are you sure you want to inactive this User?');">
                <i class="fa fa-ban text-theme-red"></i> Inactive
            </asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>
</Columns>


                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>


                    <asp:UpdatePanel
                        ID="UpdatePanel2"
                        runat="server">

                        <ContentTemplate>

                            <asp:GridView
                                ID="grdUserMaster"
                                runat="server"
                                AutoGenerateColumns="false"
                                class="table_modal display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0"
                                Width="100%"
                                Visible="true"
                                OnRowDataBound="grdUserMaster_RowDataBound">

                                <Columns>

                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:BoundField
                                        DataField="EMP_NAME"
                                        HeaderText="EMPLOYEE NAME" />

                                    <asp:BoundField
                                        DataField="ROLE_NAME"
                                        HeaderText="Access Role" />

                                    <asp:BoundField
                                        DataField="ST_WH_NAME"
                                        HeaderText="Store/Warehouse Name" />

                                     <asp:BoundField
                                         DataField="DEPT_NAME"
                                         HeaderText="Department" />

                                    <asp:BoundField
                                        DataField="DESIGNATION_NAME"
                                        HeaderText="Designation" />

                                    <asp:BoundField
                                        DataField="USER_NAME"
                                        HeaderText="Username" />

                                    <asp:TemplateField HeaderText="Module Access">
                                        <ItemTemplate>

                                            <asp:LinkButton
                                                ID="lnkViewModel"
                                                runat="server"
                                                CssClass="text-theme-green"
                                                OnClick="lnkViewModel_Click"
                                                CommandArgument='<%# Eval("USER_ID") %>'>

                            <i class="fa fa-eye text-theme-green"></i>
                            View Module Access

                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField
                                        DataField="STATUS"
                                        HeaderText="Status" />

                                    <asp:TemplateField HeaderText="Action">

                                        <ItemTemplate>

                                            <asp:LinkButton
                                                ID="lnkEdit"
                                                runat="server"
                                                CssClass="text-theme-green"
                                                OnClick="lnkEdit_Click"
                                                CommandArgument='<%# Eval("USER_ID") %>'>

                            <i class="fa fa-edit text-theme-green"></i>
                            Edit

                                            </asp:LinkButton>

                                            &nbsp;&nbsp; | &nbsp;

                        <asp:LinkButton
                            ID="lnlActive"
                            runat="server"
                            CssClass="text-theme-green"
                            OnClick="lnlActive_Click"
                            CommandArgument='<%# Eval("USER_ID") %>'
                            OnClientClick="return confirm('Are you sure you want to active this User?');">

                            <i class="fa fa-check text-theme-green"></i>
                            Active

                        </asp:LinkButton>

                                            <asp:LinkButton
                                                ID="lnkInactive"
                                                runat="server"
                                                CssClass="text-theme-red"
                                                OnClick="lnkInactive_Click"
                                                CommandArgument='<%# Eval("USER_ID") %>'
                                                OnClientClick="return confirm('Are you sure you want to inactive this User?');">

                            <i class="fa fa-ban text-theme-red"></i>
                            Inactive

                                            </asp:LinkButton>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>

                        </ContentTemplate>

                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 95%;">
                <h2 class="modal_heading">View Module Access Data</h2>
                <%-- <asp:Button ID="btnCancel" runat="server" UseSubmitBehavior="false" Text="X" class="btn modal_closeBtn text-white" OnClick="btnCancel_Click" Style="background: #f00; margin-top: 3px; padding: 5px 12px;" />--%>
                <asp:Button
                    ID="btnCancel"
                    runat="server"
                    UseSubmitBehavior="false"
                    Text="X"
                    Class="btn modal_closeBtn text-white" OnClick="btnCancel_Click"
                    OnClientClick="$('[id$=hfSidebarOpen]').val('false'); $('body').removeClass('sidebar-open');"
                    Style="background: #f00; margin-top: 3px; padding: 5px 12px;" />

                <div class="modal-body mt-2">
                    <div class="row">
                        <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                            <div class="mb-3">
                                <h6 class="text-start">Access Authenctication :
                                <asp:Label ID="lblAccessAuth" runat="server" Font-Bold="true" Text="Super Admin" CssClass="f-s-16"></asp:Label></h6>
                            </div>
                        </div>
                        <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                            <div class="mb-3">
                                <h6 class="text-start">Username :
                                <asp:Label ID="lblUsername" runat="server" Font-Bold="true" Text="ABC" CssClass="f-s-16"></asp:Label></h6>
                            </div>
                        </div>
                        <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                            <div class="mb-3">
                                <h6 class="text-start">Department :
                                <asp:Label ID="lblDepartment" runat="server" Font-Bold="true" Text="Data" CssClass="f-s-16"></asp:Label></h6>
                            </div>
                        </div>
                        <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                            <div class="mb-3">
                                <h6 class="text-start">Designation :
                                <asp:Label ID="lblDesignation" runat="server" Font-Bold="true" Text="Data" CssClass="f-s-16"></asp:Label></h6>
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:GridView ID="grdViewModules" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                        CellSpacing="0" Width="100%" Visible="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MODULE_NAME" HeaderText="Module Name" />
                            <asp:BoundField DataField="MENU_NAME" HeaderText="Menu" />
                            <asp:BoundField DataField="SUB_MENU_NAME" HeaderText="Sub Menu" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
            <cc1:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
                BehaviorID="popupBehavior"
                PopupControlID="pnlAddEdit"
                TargetControlID="lnkFake"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.8;">
                <span style="border-width: 0px; position: fixed; padding: 10px; background-color: #FFFFFF; font-size: 25px; left: 43%; top: 40%; border-radius: 50px;">Please Wait...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script src="js/User_Master.js"></script>
    <script src="../HQ_Operations/js/Sidebar_Panel.js"></script>
    <script src="js/Validation.js"></script>
</asp:Content>
