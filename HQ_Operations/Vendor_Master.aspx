<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="Vendor_Master.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.Vendor_Master" enableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HQ Operations | Vendor Master</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }

        .hiddencol
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span page-id="Master_Management" id="find_page"></span>
<span page-id2="Vendor_Master" id="find_page2"></span>

<asp:ScriptManager ID="ScriptManager1" runat="server" >
</asp:ScriptManager>

    <asp:HiddenField ID="hfSidebarOpen" runat="server"/>

    <div class="toolbar py-5 pb-lg-2" id="kt_toolbar">
        <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
            <div class="page-title d-flex flex-column me-3 flex-start">
               <h1 class="d-flex text-white m-0 fs-3">
                   <asp:Label ID="lblMenuName" runat="server" Text="HQ Operations"></asp:Label>
                   <span class="h-20px border-gray-500 border-start mx-3"></span>
                   <asp:Label ID="lblPageName" runat="server" Text="Vendor Management"></asp:Label>      
               </h1>
            </div>
             <asp:HiddenField ID="hdf_SAP_Value" runat="server" />
            <div class="page-title d-flex me-3" runat="server" id="rdbcheck">
                <asp:RadioButton ID="rdbSingleEntry" runat="server" GroupName="status" Text="Single Entry" CssClass="sidebar-toggler"/>
                <asp:RadioButton ID="rdbMultipleEntry" runat="server" GroupName="status" Text="Multiple Entry" CssClass="sidebar-toggler"/>
            </div>
        </div>
    </div>
    <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
        <div class="content flex-row-fluid" id="kt_content">
             <div class="v-sidebar-wrapper">
                <div class="v-sidebar">
                    <div class="sidebar-header">
                        <h4 class="mb-0">
                            <asp:Label ID="lblTitle" runat="server" Text="VENDOR DETAILS" Visible="true"></asp:Label>
                        </h4>
                        <button type="button" class="sidebar-close-btn" id="sidebar-close-btn" onclick="resetValue();">
                            <i class="fa fa-times fa-2x"></i>
                        </button>
                    </div>

                    <hr class="sidebar-hr">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                    <ContentTemplate>

                    <div id="divSingleEntry">
                        <div class="row">
                            <div class="col-md-6 col-lg-4">
                              <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Code<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtVendorCode" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor Code"></asp:TextBox>
                                    <div class="invalid-feedback">Vendor Code is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtVendorName" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor Name"></asp:TextBox>
                                    <div class="invalid-feedback">Vendor Name is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                               <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Type<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlVendorType" runat="server" class="form-control form-control-solid custom-select2">
                                       <%-- <asp:ListItem Value="" Text="Select Vendor Type"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Manufacturer"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Supplier"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Service Provider"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Transporter"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Contractor"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <div class="invalid-feedback">Vendor Type is required.</div>
                                </div>
                            </div>
                            <div class="col-md-12 col-lg-8">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Address<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtVendorAddress" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor Address"></asp:TextBox>
                                    <div class="invalid-feedback">Address is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                               <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor PAN/TRC<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtVendorPan" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor PAN/TRC"></asp:TextBox>
                                    <div class="invalid-feedback">PAN/TRC is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                               <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtContactPersonName" runat="server" class="form-control form-control-solid" placeholder="Enter Contact Person Name"></asp:TextBox>
                                    <div class="invalid-feedback">Name is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person Number<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtContactPersonNo" runat="server" class="number form-control form-control-solid" placeholder="Enter Contact Person Number" MaxLength="10"></asp:TextBox>
                                    <div class="invalid-feedback">Number is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                               <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Email ID<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtVendorEmailID" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor Email ID"></asp:TextBox>
                                    <div class="invalid-feedback">Email id is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">GST / BRN<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtGSTBRN" runat="server" class="form-control form-control-solid" placeholder="Enter GST / BRN"></asp:TextBox>
                                    <div class="invalid-feedback">GST / BRN is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                  <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">State<span class="text-danger">*</span></label>
                                     <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" class="form-control form-control-solid" required="required">
                                        <%--<asp:ListItem Value="" Text="Select State"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Maharashtra"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <div class="invalid-feedback">State is required.</div>
                                    <asp:HiddenField ID="hdnState" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                 <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">City<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlCity" runat="server" class="form-control form-control-solid" required="required">
                                        <%--<asp:ListItem Value="" Text="Select City"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Mumbai"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Pune"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Nagpur"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnCity" runat="server" />
                                    <div class="invalid-feedback">City is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Pincode<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtPinCode" runat="server" class="form-control form-control-solid" placeholder="Enter Pincode"></asp:TextBox>
                                    <div class="invalid-feedback">Pincode is required.</div>
                                </div>
                            </div>

                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">User Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtUsername" runat="server" class="form-control form-control-solid" placeholder="Enter User Name"></asp:TextBox>
                                    <div class="invalid-feedback">User Name is required.</div>
                                </div>
                            </div>

                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Password<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtPassword" runat="server" class="form-control form-control-solid" placeholder="Enter Password"></asp:TextBox>
                                    <div class="invalid-feedback">Password is required.</div>
                                </div>
                            </div>

                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Confirm Password<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control form-control-solid" placeholder="Enter Confirm Password"></asp:TextBox>
                                    <div class="invalid-feedback">Confirm Password is required.</div>
                                    <div class="error_msg text-danger mt-1"></div>
                                </div>
                            </div>

                            <div class="col-md-6 col-lg-4">
                               <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Status<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlStatus" runat="server" class="form-control form-control-solid custom-select2">
                                      <%--  <asp:ListItem Value="" Text="Select Status"></asp:ListItem>--%>
                                        <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="invalid-feedback">Status is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4 mt-10">
                                <div class="mb-3">
                                    <asp:CheckBox ID="chkIsTag" runat="server" Text="Is Tag Vendor"/>
                                </div>
                            </div>
                            <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">
                               <asp:Button ID="btnSubmit" runat="server"  class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Submit" UseSubmitBehavior="false" OnClick="btnSubmit_Click" OnClientClick="if (!validateSingleEntry()) { return false; }"/>
                                <asp:Button ID="btnClear" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear" UseSubmitBehavior="false" OnClick="btnClear_Click" OnClientClick="resetValue();"/>
                            </div>
                        </div>
                    </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                    <div id="divMultipleEntry" style="display:none;">
                        <div class="row">
                            <div class="col-md-6 col-lg-12">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Upload File<span class="text-danger">*</span></label>
                                    <asp:FileUpload ID="flpUpload" runat="server" class="form-control form-control-solid"/>
                                    <div class="invalid-feedback">Please Select file to upload.</div>
                                </div>
                            </div>
                        
                          <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">
                                  <asp:Button ID="btnFileUpload" runat="server" class="btnFile btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Upload File" UseSubmitBehavior="false" OnClick="btnFileUpload_Click" OnClientClick="if (!validateMultipleEntry()) { return false; }" />
                                <button type="button" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" onclick="clearFile()">Clear File</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="v-sidebar-fade"></div>  
            
           <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
            <ContentTemplate>
           <div class="card mt-3  mb-15" id="Div_grd" runat="server" visible="true">
                <div class="card-body">
                    <asp:GridView ID="grdVendorMaster" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                        CellSpacing="0" Width="100%" Visible="true" OnRowDataBound="grdVendorMaster_RowDataBound" DataKeyNames="VENDOR_TYPE_ID,STATE_ID,CITY_ID">
                        <Columns>       
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "VENDOR_CODE" HeaderText="Vendor Code" />
                            <asp:BoundField DataField = "VENDOR_NAME" HeaderText="Vendor Name" />
                            <asp:BoundField DataField = "VENDOR_TYPE" HeaderText="Vendor Type" />
                            <asp:BoundField DataField = "VENDOR_ADDRESS" HeaderText="Address" />
                            <asp:BoundField DataField = "VENDOR_PAN_NUMBER" HeaderText="Vendor PAN/TRC" />
                            <asp:BoundField DataField = "VENDOR_CONTACT_PERSON_NAME" HeaderText="Contact Person Name" />
                            <asp:BoundField DataField = "VENDOR_CONTACT_PERSON_MOBILE_NO" HeaderText="Contact Person No" />
                            <asp:BoundField DataField = "VENDOR_EMAIL_ID" HeaderText="Vendor Email ID" />
                            <asp:BoundField DataField = "VENDOR_GST_NUMBER" HeaderText="GST/BRN" />
                            <asp:BoundField DataField = "STATE_NAME" HeaderText="State" />
                            <asp:BoundField DataField = "CITY_NAME" HeaderText="City" />
                            <asp:BoundField DataField = "VENDOR_PIN_CODE" HeaderText="Pincode" />
                           <asp:BoundField DataField = "USER_NAME" HeaderText="User Name" />
                            <asp:BoundField DataField = "PASSWORD" HeaderText="Password" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                             <asp:BoundField DataField = "IS_TAG_FLAG" HeaderText="Tag Vendor" />
                            <asp:BoundField DataField = "STATUS" HeaderText="Status" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" class="text-theme-green" CommandArgument='<%# Eval("VENDOR_ID") %>' OnClick="lnkEdit_Click"><i class="fa fa-edit text-theme-green"></i> Edit</asp:LinkButton>&nbsp; &nbsp; | &nbsp; 
                                    <asp:LinkButton ID="lnlActive" runat="server" class="text-theme-green" CommandArgument='<%# Eval("VENDOR_ID") %>' OnClick="lnlActive_Click" OnClientClick="return confirm('Are you sure you want to active this category?');"><i class="fa fa-check text-theme-green"></i> Active</asp:LinkButton>
                                    <asp:LinkButton ID="lnkInactive" runat="server" class="text-theme-red" CommandArgument='<%# Eval("VENDOR_ID") %>' OnClick="lnkInactive_Click" OnClientClick="return confirm('Are you sure you want to inactive this category?');"><i class="fa fa-ban text-theme-red"></i> Inactive</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                         </Columns>
                    </asp:GridView>
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 95%;">
        <h2 class="modal_heading">EDIT DETAILS</h2>
        <asp:Button ID="btnCancel" runat="server" UseSubmitBehavior="false" Text="X" class="btn modal_closeBtn text-white" Style="background: #f00; margin-top: 3px;padding: 5px 12px;" />

        <div class="modal-body mt-2">
            <div class="row">
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Code<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModVendorCode" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor Code" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Name<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModVendorName" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor Name" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Type<span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlModVendorType" runat="server" class="form-control form-control-solid custom-select2" required="required">
                            <asp:ListItem Value="" Text="Select Vendor Type"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Manufacturer"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Supplier"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Service Provider"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Transporter"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Contractor"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12 col-lg-12">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Address<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModVendorAddress" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor Address" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor PAN/TRC<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModVendorPAN" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor PAN/TRC" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person Name<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModContPersonName" runat="server" class="form-control form-control-solid" placeholder="Enter Contact Person Name" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person Number<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModContPersonNo" runat="server" class="form-control form-control-solid" placeholder="Enter Contact Person Number" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Email ID<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModVendorEmail" runat="server" type="email" class="form-control form-control-solid" placeholder="Enter Vendor Email ID" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">GST / BRN<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModVendorGST" runat="server" type="email" class="form-control form-control-solid" placeholder="Enter GST / BRN" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">State<span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlModState" runat="server" class="form-control form-control-solid custom-select2" required="required">
                            <asp:ListItem Value="" Text="Select State"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Maharashtra"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">City<span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlModCity" runat="server" class="form-control form-control-solid custom-select2" required="required">
                            <asp:ListItem Value="" Text="Select City"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Mumbai"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Pune"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Nagpur"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Pincode<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModPincode" runat="server" type="email" class="form-control form-control-solid" placeholder="Enter Pincode" required="required"></asp:TextBox>
                    </div>
                </div>
                
                <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">
                    <asp:Button ID="Button1" runat="server" class="btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Submit" UseSubmitBehavior="false"/>
                    <asp:Button ID="Button2" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear" UseSubmitBehavior="false"/>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
    <cc1:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
        BehaviorID="popupBehavior"
        PopupControlID="pnlAddEdit"
        TargetControlID="lnkFake"
        BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>


<asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.8;">
            <span style="border-width: 0px; position: fixed; padding: 10px; background-color: #FFFFFF; font-size: 25px; left: 43%; top: 40%; border-radius: 50px;">Please Wait...</span>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>

<script>

    function isValidEmail(email) {
        let emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    }

    function allowOnlyNumberAndLimit10(input) {
        input.value = input.value.replace(/[^0-9]/g, '');
        if (input.value.length > 10) {
            input.value = input.value.slice(0, 10);
        }
    }

    function validateSingleEntry() {
        let isValid = true;

        let VendorCode = document.getElementById('<%= txtVendorCode.ClientID %>')
        let VendorName = document.getElementById('<%= txtVendorName.ClientID %>')
        let VendorType = document.getElementById('<%= ddlVendorType.ClientID %>')
        let VendorAddress = document.getElementById('<%= txtVendorAddress.ClientID %>')
        let VendorPan = document.getElementById('<%= txtVendorPan.ClientID %>')
        let ContactPersonName = document.getElementById('<%= txtContactPersonName.ClientID %>')
        let ContactPersonNo = document.getElementById('<%= txtContactPersonNo.ClientID %>')
        let VendorEmailID = document.getElementById('<%= txtVendorEmailID.ClientID %>')
        let GSTBRN = document.getElementById('<%= txtGSTBRN.ClientID %>')
        let State = document.getElementById('<%= ddlState.ClientID %>')
        let City = document.getElementById('<%= ddlCity.ClientID %>')
        let PinCode = document.getElementById('<%= txtPinCode.ClientID %>')
        let username = document.getElementById('<%= txtUsername.ClientID %>')
        let password = document.getElementById('<%= txtPassword.ClientID %>')
        let confirmpassword = document.getElementById('<%= txtConfirmPassword.ClientID %>')
        let status = document.getElementById('<%= ddlStatus.ClientID %>')

        if (VendorCode && VendorCode.value.trim() === "") {
            VendorCode.classList.add("is-invalid");
            isValid = false;
        }
        else if (VendorCode) {
            VendorCode.classList.remove("is-invalid");
        }

        if (VendorName && VendorName.value.trim() === "") {
            VendorName.classList.add("is-invalid");
            isValid = false;
        }
        else if (VendorName) {
            VendorName.classList.remove("is-invalid");
        }

        if (VendorType && VendorType.value.trim() === "") {
            VendorType.classList.add("is-invalid");
            isValid = false;
        }
        else if (VendorType) {
            VendorType.classList.remove("is-invalid");
        }

        if (VendorAddress && VendorAddress.value.trim() === "") {
            VendorAddress.classList.add("is-invalid");
            isValid = false;
        }
        else if (VendorAddress) {
            VendorAddress.classList.remove("is-invalid");
        }

        if (VendorPan && VendorPan.value.trim() === "") {
            VendorPan.classList.add("is-invalid");
            isValid = false;
        }
        else if (VendorPan) {
            VendorPan.classList.remove("is-invalid");
        }

        if (ContactPersonName && ContactPersonName.value.trim() === "") {
            ContactPersonName.classList.add("is-invalid");
            isValid = false;
        }
        else if (ContactPersonName) {
            ContactPersonName.classList.remove("is-invalid");
        }

        if (ContactPersonNo && ContactPersonNo.value.trim() === "") {
            ContactPersonNo.classList.add("is-invalid");
            isValid = false;
        }
        else if (ContactPersonNo && ContactPersonNo.value.length !== 10) {
            ContactPersonNo.classList.add("is-invalid");
            isValid = false;
        }
        else if (ContactPersonNo) {
            ContactPersonNo.classList.remove("is-invalid");
        }

        if (VendorEmailID && VendorEmailID.value.trim() === "") {
            VendorEmailID.classList.add("is-invalid");
            isValid = false;
        }
        else if (VendorEmailID && !isValidEmail(VendorEmailID.value.trim())) {
            VendorEmailID.classList.add("is-invalid");
            isValid = false;
        }
        else if (VendorEmailID) {
            VendorEmailID.classList.remove("is-invalid");
        }


        if (GSTBRN && GSTBRN.value.trim() === "") {
            GSTBRN.classList.add("is-invalid");
            isValid = false;
        }
        else if (GSTBRN) {
            GSTBRN.classList.remove("is-invalid");
        }

        if (State && State.value.trim() === "") {
            State.classList.add("is-invalid");
            isValid = false;
        }
        else if (State) {
            State.classList.remove("is-invalid");
        }

        if (City && City.value.trim() === "") {
            City.classList.add("is-invalid");
            isValid = false;
        }
        else if (City) {
            City.classList.remove("is-invalid");
        }

        if (PinCode && PinCode.value.trim() === "") {
            PinCode.classList.add("is-invalid");
            isValid = false;
        }
        else if (PinCode) {
            PinCode.classList.remove("is-invalid");
        }

        if (username && username.value.trim() === "") {
            username.classList.add("is-invalid");
            isValid = false;
        }
        else if (username) {
            username.classList.remove("is-invalid");
        }

        if (password && password.value.trim() === "") {
            password.classList.add("is-invalid");
            isValid = false;
        }
        else if (password) {
            password.classList.remove("is-invalid");
        }

        if (confirmpassword && confirmpassword.value.trim() === "") {
            confirmpassword.classList.add("is-invalid");
            isValid = false;
        }
        else if (confirmpassword) {
            confirmpassword.classList.remove("is-invalid");
        }

        if (password.value !== confirmpassword.value) {
            isValid = false;
            $('.error_msg').html("Password & Confirm Password should match");
        }
        else {
            $('.error_msg').html('');
        }

        if (status && status.value === "") {
            status.classList.add("is-invalid");
            isValid = false;
        }
        else if (status) {
            status.classList.remove("is-invalid");
        }

        return isValid;
    }

    function validateMultipleEntry() {

        let fileupload = document.getElementById('<%= flpUpload.ClientID %>');
        let isValid = true;

        if (!fileupload || fileupload.value === "") {
            fileupload.classList.add("is-invalid");
            isValid = false;
        }
        else {
            fileupload.classList.remove("is-invalid");
        }

        return isValid;
    }

    function bindValidationEvents() {
        let VendorCode = document.getElementById('<%= txtVendorCode.ClientID %>')
        let VendorName = document.getElementById('<%= txtVendorName.ClientID %>')
        let VendorType = document.getElementById('<%= ddlVendorType.ClientID %>')
        let VendorAddress = document.getElementById('<%= txtVendorAddress.ClientID %>')
        let VendorPan = document.getElementById('<%= txtVendorPan.ClientID %>')
        let ContactPersonName = document.getElementById('<%= txtContactPersonName.ClientID %>')
        let ContactPersonNo = document.getElementById('<%= txtContactPersonNo.ClientID %>')
        let VendorEmailID = document.getElementById('<%= txtVendorEmailID.ClientID %>')
        let GSTBRN = document.getElementById('<%= txtGSTBRN.ClientID %>')
        let State = document.getElementById('<%= ddlState.ClientID %>')
        let City = document.getElementById('<%= ddlCity.ClientID %>')
        let PinCode = document.getElementById('<%= txtPinCode.ClientID %>')
        let username = document.getElementById('<%= txtUsername.ClientID %>')
        let password = document.getElementById('<%= txtPassword.ClientID %>')
        let confirmpassword = document.getElementById('<%= txtConfirmPassword.ClientID %>')
        let status = document.getElementById('<%= ddlStatus.ClientID %>')
        let fileupload = document.getElementById('<%= flpUpload.ClientID %>');
        
        if (VendorCode) VendorCode.oninput = () => VendorCode.classList.remove("is-invalid");
        if (VendorName) VendorName.oninput = () => VendorName.classList.remove("is-invalid");
        if (VendorType) VendorType.oninput = () => VendorType.classList.remove("is-invalid");
        if (VendorAddress) VendorAddress.oninput = () => VendorAddress.classList.remove("is-invalid");
        if (VendorPan) VendorPan.oninput = () => VendorPan.classList.remove("is-invalid");
        if (ContactPersonName) ContactPersonName.oninput = () => ContactPersonName.classList.remove("is-invalid");
        if (ContactPersonNo) {ContactPersonNo.oninput = () => {allowOnlyNumberAndLimit10(ContactPersonNo);ContactPersonNo.classList.remove("is-invalid");};}
        if (VendorEmailID) VendorEmailID.oninput = () => VendorEmailID.classList.remove("is-invalid");
        if (GSTBRN) GSTBRN.oninput = () => GSTBRN.classList.remove("is-invalid");
        if (State) State.oninput = () => State.classList.remove("is-invalid");
        if (City) City.oninput = () => City.classList.remove("is-invalid");
        if (PinCode) PinCode.oninput = () => PinCode.classList.remove("is-invalid");
        if (username) username.oninput = () => username.classList.remove("is-invalid");
        if (password) password.oninput = () => password.classList.remove("is-invalid");
        if (confirmpassword) confirmpassword.oninput = () => confirmpassword.classList.remove("is-invalid");
        if (status) status.oninput = () => status.classList.remove("is-invalid");
        if (fileupload) fileupload.onchange = () => fileupload.classList.remove("is-invalid");
     }
     
     function clearFile()
     {

        var oldInput = document.getElementById('<%= flpUpload.ClientID %>');
        if (!oldInput) return;

        var newInput = document.createElement("input");
        newInput.type = "file";
        newInput.id = oldInput.id;
        newInput.name = oldInput.name;
        newInput.className = oldInput.className;

        oldInput.parentNode.replaceChild(newInput, oldInput);
     }

    document.addEventListener("DOMContentLoaded", bindValidationEvents);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindValidationEvents);

</script>


<script src="js/Vendor_Master.js"></script>
<script src="js/Sidebar_Panel.js"></script>
</asp:Content>
