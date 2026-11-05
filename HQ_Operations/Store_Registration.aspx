<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="Store_Registration.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.Store_Registration" enableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HQ Operations | Store Registration</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span page-id="Master_Management" id="find_page"></span>
<span page-id2="Store_Registration" id="find_page2"></span>

<asp:ScriptManager ID="ScriptManager1" runat="server" >
</asp:ScriptManager>

    <asp:HiddenField ID="hfSidebarOpen" runat="server"/>

    <div class="toolbar py-5 pb-lg-2" id="kt_toolbar">
        <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
            <div class="page-title d-flex flex-column me-3 flex-start">
               <h1 class="d-flex text-white m-0 fs-3">
                   <asp:Label ID="lblMenuName" runat="server" Text="HQ Operations"></asp:Label>      
                   <span class="h-20px border-gray-500 border-start mx-3"></span>
                   <asp:Label ID="lblPageName" runat="server" Text="Store Registration"></asp:Label>      
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
                            <asp:Label ID="lblTitle" runat="server" Text="STORE DETAILS" Visible="true"></asp:Label>
                        </h4>
                        <button type="button" class="sidebar-close-btn" id="sidebar-close-btn">
                            <i class="fa fa-times fa-2x"></i>
                        </button>
                    </div>

                    <hr class="sidebar-hr">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                    <ContentTemplate>
                    <div id="divSingleEntry">
                        <div class="row">
                            <div class="col-md-6 col-lg-4">
                               <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Store Code<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtStoreCode" runat="server" class="form-control form-control-solid" placeholder="Enter Store Code"></asp:TextBox>
                                    <div class="invalid-feedback">Store Code is required.</div>
                                </div>
                            
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Store Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtStoreName" runat="server" class="form-control form-control-solid" placeholder="Enter Store Name"></asp:TextBox>
                                    <div class="invalid-feedback">Store Name is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">GST / BRN<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtGSTBRN" runat="server" class="form-control form-control-solid" placeholder="Enter GST / BRN" MaxLength="15"></asp:TextBox>
                                    <div class="invalid-feedback">GST / BRN is required.</div>
                                </div>
                            </div>
                            <%--<div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Store PAN/TRC<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtStorePan" runat="server" class="form-control form-control-solid" placeholder="Enter Store PAN/TRC" required="required"></asp:TextBox>
                                </div>
                            </div>--%>
                            <div class="col-md-6 col-lg-8">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Store Address<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtStoreAddress" runat="server" class="form-control form-control-solid" placeholder="Enter Store Address" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    <div class="invalid-feedback">Address is required.</div>
                                </div>
                            </div>
                            
                            <div class="col-md-6 col-lg-4">
                                 <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtContactPersonName" runat="server" class="form-control form-control-solid" placeholder="Enter Contact Person Name"></asp:TextBox>
                                    <div class="invalid-feedback">Contact Person Name is required.</div>
                                 </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                               <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person Number<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtContactPersonNo" runat="server" class="number form-control form-control-solid" placeholder="Enter Contact Person Number" MaxLength="10"></asp:TextBox>
                                    <div class="invalid-feedback">Person Number is required.</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Store Email ID<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtStoreEmailID" runat="server" class="form-control form-control-solid" placeholder="Enter Store Email ID"></asp:TextBox>
                                    <div class="invalid-feedback">Email ID is required.</div>
                                </div>
                            </div>
                            
                            <div class="col-md-6 col-lg-4">
    <div class="mb-3">
        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
            Store Manager<span class="text-danger">*</span>
        </label>

        <asp:DropDownList
            ID="ddlStoreManager"
            runat="server"
            Class="form-control form-control-solid custom-select2">

            <asp:ListItem Value="" Text="Select Store Manager"></asp:ListItem>

        </asp:DropDownList>

        <div class="invalid-feedback">
            Store Manager is required.
        </div>
    </div>
</div>

<div class="col-md-6 col-lg-4">
    <div class="mb-3">
        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
            Area Manager<span class="text-danger">*</span>
        </label>

        <asp:DropDownList
            ID="ddlAreaManager"
            runat="server"
            Class="form-control form-control-solid custom-select2">

            <asp:ListItem Value="" Text="Select Area Manager"></asp:ListItem>

        </asp:DropDownList>

        <div class="invalid-feedback">
            Area Manager is required.
        </div>
    </div>
</div>





                            <div class="col-md-6 col-lg-4">
                               <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">State<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlState" runat="server" class="form-control form-control-solid" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="false">
                                        <asp:ListItem Value="" Text="Select State"></asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="invalid-feedback">State is required.</div>
                                    <asp:HiddenField ID="hdnState" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                               <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">City<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlCity" runat="server" class="form-control form-control-solid">
                                        <asp:ListItem Value="" Text="Select City"></asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="invalid-feedback">City is required.</div>
                                    <asp:HiddenField ID="hdnCity" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Pincode<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtPinCode" runat="server" class="form-control form-control-solid" placeholder="Enter Pincode" oninput="this.value = this.value.replace(/[^0-9]/g,'');"></asp:TextBox>
                                    <div class="invalid-feedback">Pincode is required.</div>
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
                                <asp:Button ID="btnFileUpload" runat="server" class="btnFile btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Upload File" UseSubmitBehavior="false" OnClientClick="if (!validateMultipleEntry()) { return false; }" />
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
                    <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                        CellSpacing="0" Width="100%" Visible="true" DataKeyNames="STATE_ID,CITY_ID,STORE_MANAGER_ID,AREA_MANAGER_ID" OnRowDataBound="grdDetails_RowDataBound">
                        <Columns>       
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "STORE_CODE" HeaderText="Store Code" />
                            <asp:BoundField DataField = "STORE_NAME" HeaderText="Store Name" />
                            <asp:BoundField DataField = "STORE_ADDRESS" HeaderText="Store Address" />
                            <%--<asp:BoundField DataField = "STORE_PAN_NUMBER" HeaderText="Store PAN/TRC" />--%>
                            <asp:BoundField DataField = "STORE_CONTACT_PERSON_NAME" HeaderText="Contact Person Name" />
                            <asp:BoundField DataField = "STORE_CONTACT_PERSON_NUMBER" HeaderText="Contact Person No" />
                            <asp:BoundField DataField = "STORE_EMAIL_ID" HeaderText="Store Email ID" />

                            <asp:BoundField DataField="STORE_MANAGER_NAME" HeaderText="Store Manager" />
                            <asp:BoundField DataField="AREA_MANAGER_NAME" HeaderText="Area Manager" />

                            <asp:BoundField DataField = "STORE_GST_NUMBER" HeaderText="GST/BRN" />
                            <asp:BoundField DataField = "STATE_NAME" HeaderText="State" />
                            <asp:BoundField DataField = "CITY_NAME" HeaderText="City" />
                            <asp:BoundField DataField = "STORE_PIN_CODE" HeaderText="Pincode" />
                            <asp:BoundField DataField = "STATUS" HeaderText="Status" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" class="text-theme-green" CommandArgument='<%# Eval("STORE_ID") %>' OnClick="lnkEdit_Click"><i class="fa fa-edit text-theme-green"></i> Edit</asp:LinkButton>&nbsp; &nbsp; | &nbsp; 
                                    <asp:LinkButton ID="lnlActive" runat="server" class="text-theme-green" CommandArgument='<%# Eval("STORE_ID") %>' OnClick="lnlActive_Click" OnClientClick="return confirm('Are you sure you want to active this category?');"><i class="fa fa-check text-theme-green"></i> Active</asp:LinkButton>
                                    <asp:LinkButton ID="lnkInactive" runat="server" class="text-theme-red" CommandArgument='<%# Eval("STORE_ID") %>' OnClick="lnkInactive_Click" OnClientClick="return confirm('Are you sure you want to inactive this category?');"><i class="fa fa-ban text-theme-red"></i> Inactive</asp:LinkButton>
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

        let storeCode = document.getElementById('<%= txtStoreCode.ClientID %>');
        let storeName = document.getElementById('<%= txtStoreName.ClientID %>');
       <%-- let storePan = document.getElementById('<%= txtStorePan.ClientID %>');--%>
        let storeAddress = document.getElementById('<%= txtStoreAddress.ClientID %>');
        let contactPersonName = document.getElementById('<%= txtContactPersonName.ClientID %>');
        let contactPersonNo = document.getElementById('<%= txtContactPersonNo.ClientID %>');
        let storeEmailID = document.getElementById('<%= txtStoreEmailID.ClientID %>');
        let gstBRN = document.getElementById('<%= txtGSTBRN.ClientID %>');
        let state = document.getElementById('<%= ddlState.ClientID %>');
        let city = document.getElementById('<%= ddlCity.ClientID %>');
        let pinCode = document.getElementById('<%= txtPinCode.ClientID %>');
        let status = document.getElementById('<%= ddlStatus.ClientID %>');
        let storeManager = document.getElementById('<%= ddlStoreManager.ClientID %>');
        let areaManager = document.getElementById('<%= ddlAreaManager.ClientID %>');

        if (storeManager && storeManager.value.trim() === "") { storeManager.classList.add("is-invalid"); isValid = false; }
        else if (storeManager) storeManager.classList.remove("is-invalid");

        if (areaManager && areaManager.value.trim() === "") { areaManager.classList.add("is-invalid"); isValid = false; }
        else if (areaManager) areaManager.classList.remove("is-invalid"); 

        if (storeCode && storeCode.value.trim() === "") { storeCode.classList.add("is-invalid"); isValid = false; }
        else if (storeCode) storeCode.classList.remove("is-invalid");

        if (storeName && storeName.value.trim() === "") { storeName.classList.add("is-invalid"); isValid = false; }
        else if (storeName) storeName.classList.remove("is-invalid");

        //if (storePan && storePan.value.trim() === "") { storePan.classList.add("is-invalid"); isValid = false; }
        //else if (storePan) storePan.classList.remove("is-invalid");

        if (storeAddress && storeAddress.value.trim() === "") { storeAddress.classList.add("is-invalid"); isValid = false; }
        else if (storeAddress) storeAddress.classList.remove("is-invalid");

        if (contactPersonName && contactPersonName.value.trim() === "") { contactPersonName.classList.add("is-invalid"); isValid = false; }
        else if (contactPersonName) contactPersonName.classList.remove("is-invalid");

        if (contactPersonNo && contactPersonNo.value.trim() === "") {
            contactPersonNo.classList.add("is-invalid");
            isValid = false;
        }
        else if (contactPersonNo && contactPersonNo.value.length !== 10) {
            contactPersonNo.classList.add("is-invalid");
            isValid = false;
        }
        else if (contactPersonNo) {
            contactPersonNo.classList.remove("is-invalid");
        }

        if (storeEmailID && storeEmailID.value.trim() === "") {
            storeEmailID.classList.add("is-invalid");
            isValid = false;
        }
        else if (storeEmailID && !isValidEmail(storeEmailID.value.trim())) {
            storeEmailID.classList.add("is-invalid");
            isValid = false;
        }
        else if (storeEmailID) {
            storeEmailID.classList.remove("is-invalid");
        }

        if (gstBRN && gstBRN.value.trim() === "") { gstBRN.classList.add("is-invalid"); isValid = false; }
        else if (gstBRN) gstBRN.classList.remove("is-invalid");

        if (state && state.value.trim() === "") { state.classList.add("is-invalid"); isValid = false; }
        else if (state) state.classList.remove("is-invalid");

        if (city && city.value.trim() === "") { city.classList.add("is-invalid"); isValid = false; }
        else if (city) city.classList.remove("is-invalid");

        if (pinCode && pinCode.value.trim() === "") { pinCode.classList.add("is-invalid"); isValid = false; }
        else if (pinCode) pinCode.classList.remove("is-invalid");

        if (status && status.value === "") { status.classList.add("is-invalid"); isValid = false; }
        else if (status) status.classList.remove("is-invalid");

        return isValid;
    }

    function validateMultipleEntry() {
        let fileupload = document.getElementById('<%= flpUpload.ClientID %>');
        let isValid = true;

        if (!fileupload || fileupload.value === "") {
            fileupload.classList.add("is-invalid");
            isValid = false;
        } else {
            fileupload.classList.remove("is-invalid");
        }
        return isValid;
    }

    function bindValidationEvents() {
        let storeCode = document.getElementById('<%= txtStoreCode.ClientID %>');
        let storeName = document.getElementById('<%= txtStoreName.ClientID %>');
<%--        let storePan = document.getElementById('<%= txtStorePan.ClientID %>');--%>
        let storeAddress = document.getElementById('<%= txtStoreAddress.ClientID %>');
        let contactPersonName = document.getElementById('<%= txtContactPersonName.ClientID %>');
        let contactPersonNo = document.getElementById('<%= txtContactPersonNo.ClientID %>');
        let storeEmailID = document.getElementById('<%= txtStoreEmailID.ClientID %>');
        let gstBRN = document.getElementById('<%= txtGSTBRN.ClientID %>');
        let state = document.getElementById('<%= ddlState.ClientID %>');
        let city = document.getElementById('<%= ddlCity.ClientID %>');
        let pinCode = document.getElementById('<%= txtPinCode.ClientID %>');
        let status = document.getElementById('<%= ddlStatus.ClientID %>');
        let fileupload = document.getElementById('<%= flpUpload.ClientID %>');

        if (storeCode) storeCode.oninput = () => storeCode.classList.remove("is-invalid");
        if (storeName) storeName.oninput = () => storeName.classList.remove("is-invalid");
        //if (storePan) storePan.oninput = () => storePan.classList.remove("is-invalid");
        if (storeAddress) storeAddress.oninput = () => storeAddress.classList.remove("is-invalid");
        if (contactPersonName) contactPersonName.oninput = () => contactPersonName.classList.remove("is-invalid");

        if (contactPersonNo) {
            contactPersonNo.oninput = () => {
                allowOnlyNumberAndLimit10(contactPersonNo);
                contactPersonNo.classList.remove("is-invalid");
            };
        }

        if (storeEmailID) storeEmailID.oninput = () => storeEmailID.classList.remove("is-invalid");
        if (gstBRN) gstBRN.oninput = () => gstBRN.classList.remove("is-invalid");
        if (state) state.oninput = () => state.classList.remove("is-invalid");
        if (city) city.oninput = () => city.classList.remove("is-invalid");
        if (pinCode) pinCode.oninput = () => pinCode.classList.remove("is-invalid");
        if (status) status.oninput = () => status.classList.remove("is-invalid");
        if (fileupload) fileupload.onchange = () => fileupload.classList.remove("is-invalid");
    }

    function clearFile() {
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

<script src="js/Store_Master.js"></script>
<script src="js/Sidebar_Panel.js"></script>
</asp:Content>
