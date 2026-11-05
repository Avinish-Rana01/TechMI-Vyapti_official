<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="Warehouse_Master.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.Warehouse_Master" enableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HQ Operations | Warehouse Master</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
        body {
            background: #d0dbe1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span page-id="Master_Management" id="find_page"></span>
<span page-id2="Warehouse_Master" id="find_page2"></span>

<asp:ScriptManager ID="ScriptManager1" runat="server" >
</asp:ScriptManager>

    <asp:HiddenField ID="hfSidebarOpen" runat="server"/>


    <div class="toolbar py-5 pb-lg-2" id="kt_toolbar">
        <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
            <div class="page-title d-flex flex-column me-3 flex-start">
               <h1 class="d-flex text-white m-0 fs-3">
                   <asp:Label ID="lblMenuName" runat="server" Text="HQ Operations"></asp:Label>
                   <span class="h-20px border-gray-500 border-start mx-3"></span>
                   <asp:Label ID="lblPageName" runat="server" Text="Warehouse Master"></asp:Label>      
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
                        <h5 class="sidebar-title"></h5>
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
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Code<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtWarehouseCode" runat="server" class="form-control form-control-solid" placeholder="Enter Warehouse Code" required="required"></asp:TextBox>
                                <div class="invalid-feedback">Warehouse Code is required</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtWarehouseName" runat="server" class="form-control form-control-solid" placeholder="Enter Warehouse Name" required="required"></asp:TextBox>
                                <div class="invalid-feedback">Warehouse Name is required </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Type<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlWarehouseType" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                        <%--<asp:ListItem Value="" Text="Select Warehouse Type"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Central Warehouse"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Distribution Center (DC)"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Store Warehouse"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Transit / Temporary"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <div class="invalid-feedback">Warehouse TYpe is required</div>
                                </div>
                            </div>
                            <div class="col-md-12 col-lg-12">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Address<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtWarehouseAddress" runat="server" class="form-control form-control-solid" placeholder="Enter Warehouse Address" required="required"></asp:TextBox>
                                 <div class="invalid-feedback">Warehouse Address is required</div>
                                </div>
                            </div>
                           <%-- <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse PAN/TRC<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtWarehousePan" runat="server" class="form-control form-control-solid" placeholder="Enter Warehouse PAN/TRC" required="required"></asp:TextBox>
                                </div>
                            </div>--%>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtContactPersonName" runat="server" class="form-control form-control-solid" placeholder="Enter Contact Person Name" required="required"></asp:TextBox>
                                <div class="invalid-feedback">Contact Person Name is required!!!</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person Number<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtContactPersonNo" runat="server" class="number form-control form-control-solid" placeholder="Enter Contact Person Number" required="required"></asp:TextBox>
                                <div class="invalid-feedback">Contact Person Number is required!!!</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Email ID<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtWarehouseEmailID" runat="server"  class="form-control form-control-solid" placeholder="Enter Warehouse Email ID" required="required"></asp:TextBox>
                                <div class="invalid-feedback">Warehouse Email ID is required</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">GST / BRN<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtGSTBRN" runat="server"  class="form-control form-control-solid" placeholder="Enter GST / BRN" required="required"></asp:TextBox>
                                    <div class="invalid-feedback">GST / BRN is required</div>
                               </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">State<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" class="form-control form-control-solid" required="required">
                                        <%--<asp:ListItem Value="" Text="Select State"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Maharashtra"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnState" runat="server" />
                                     <div class="invalid-feedback">State is required!!!</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">City<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlCity" runat="server" class="form-control form-control-solid" required="required">
                                       <asp:ListItem Value="" Text="Select City"></asp:ListItem>
                                        <%-- <asp:ListItem Value="1" Text="Mumbai"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Pune"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Nagpur"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnCity" runat="server" />
                                     <div class="invalid-feedback">City is required</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Pincode<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtPinCode" runat="server" class="form-control form-control-solid" placeholder="Enter Pincode" required="required"></asp:TextBox>
                                 <div class="invalid-feedback">Pincode is required</div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-4">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Status<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlStatus" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                       <%-- <asp:ListItem Value="" Text="Select Status"></asp:ListItem>--%>
                                        <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="invalid-feedback">Status is required</div>
                                </div>
                            </div>
                            <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">
                               <%-- <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" class="btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Submit" UseSubmitBehavior="false"/>--%>

                                 <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"  OnClientClick="if (!validateSingleEntry()) { return false; }" class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Submit" UseSubmitBehavior="false"/>
                                 <asp:Button ID="Button3" runat="server" OnClick="btnClear_Click" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear" UseSubmitBehavior="false" OnClientClick="resetValue();"/>
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
                                    <asp:FileUpload ID="flpUpload" runat="server" class="form-control form-control-solid" required="required"/>
                                     <div class="invalid-feedback">Upload File is required</div>
                                </div>
                            </div>
                        
                            <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">
                               <%-- <asp:Button ID="btnFileUpload" runat="server" class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Upload File" UseSubmitBehavior="false"/>
                                <asp:Button ID="btnFileClear" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear File" UseSubmitBehavior="false"/>--%>

                                <asp:Button ID="btnFileUpload" runat="server" OnClientClick="if (!validateMultipleEntry()) { return false; }" class="btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Upload File" UseSubmitBehavior="false"/>
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
                    <asp:GridView ID="grdWarehouseMaster" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                        CellSpacing="0" Width="100%" Visible="true" DataKeyNames="WAREHOUSE_TYPE_ID,STATE_ID,CITY_ID" OnRowDataBound="grdWarehouseMaster_RowDataBound">
                        <Columns>       
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "WAREHOUSE_CODE" HeaderText="Warehouse Code" />
                            <asp:BoundField DataField = "WAREHOUSE_NAME" HeaderText="Warehouse Name" />
                            <asp:BoundField DataField = "WAREHOUSE_TYPE" HeaderText="Warehouse Type" />
                            <asp:BoundField DataField = "WAREHOUSE_ADDRESS" HeaderText="Address" />
                           <%-- <asp:BoundField DataField = "WAREHOUSE_PAN_NUMBER" HeaderText="Warehouse PAN/TRC" />--%>
                            <asp:BoundField DataField = "WAREHOUSE_CONTACT_PERSON_NAME" HeaderText="Contact Person Name" />
                            <asp:BoundField DataField = "WAREHOUSE_CONTACT_PERSON_NUMBER" HeaderText="Contact Person No" />
                            <asp:BoundField DataField = "WAREHOUSE_EMAIL_ID" HeaderText="Warehouse Email ID" />
                            <asp:BoundField DataField = "WAREHOUSE_GST_NUMBER" HeaderText="GST/BRN" />
                            <asp:BoundField DataField = "STATE_NAME" HeaderText="State" />
                            <asp:BoundField DataField = "CITY_NAME" HeaderText="City" />
                            <asp:BoundField DataField = "WAREHOUSE_PIN_CODE" HeaderText="Pincode" />
                            <asp:BoundField DataField = "STATUS" HeaderText="Status" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" class="text-theme-green" CommandArgument='<%# Eval("WAREHOUSE_ID") %>' OnClick="lnkEdit_Click"><i class="fa fa-edit text-theme-green"></i> Edit</asp:LinkButton>&nbsp; &nbsp; | &nbsp; 
                                    <asp:LinkButton ID="lnlActive" runat="server" class="text-theme-green" CommandArgument='<%# Eval("WAREHOUSE_ID") %>' OnClick="lnlActive_Click" OnClientClick="return confirm('Are you sure you want to active this category?');"><i class="fa fa-check text-theme-green"></i> Active</asp:LinkButton>
                                    <asp:LinkButton ID="lnkInactive" runat="server" class="text-theme-red" CommandArgument='<%# Eval("WAREHOUSE_ID") %>' OnClick="lnkInactive_Click" OnClientClick="return confirm('Are you sure you want to inactive this category?');"><i class="fa fa-ban text-theme-red"></i> Inactive</asp:LinkButton>
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
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Code<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModVendorCode" runat="server" class="form-control form-control-solid" placeholder="Enter Warehouse Code" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Name<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModVendorName" runat="server" class="form-control form-control-solid" placeholder="Enter Warehouse Name" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Type<span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlModVendorType" runat="server" class="form-control form-control-solid custom-select2" required="required">
                            <asp:ListItem Value="" Text="Select Warehouse Type"></asp:ListItem>
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
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Address<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModVendorAddress" runat="server" class="form-control form-control-solid" placeholder="Enter Warehouse Address" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-4">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse PAN/TRC<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModVendorPAN" runat="server" class="form-control form-control-solid" placeholder="Enter Warehouse PAN/TRC" required="required"></asp:TextBox>
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
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Email ID<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModVendorEmail" runat="server" type="email" class="form-control form-control-solid" placeholder="Enter Warehouse Email ID" required="required"></asp:TextBox>
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
                    <asp:Button ID="Button1" runat="server" class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Submit" UseSubmitBehavior="false"/>
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

        let whcode = document.getElementById('<%= txtWarehouseCode.ClientID %>');
        let whname = document.getElementById('<%= txtWarehouseName.ClientID %>');
        let whtype = document.getElementById('<%= ddlWarehouseType.ClientID %>');
        let whaddress = document.getElementById('<%= txtWarehouseAddress.ClientID %>');
        let name = document.getElementById('<%= txtContactPersonName.ClientID %>');
        let contactPersonNo = document.getElementById('<%= txtContactPersonNo.ClientID %>');
        let whemail = document.getElementById('<%= txtWarehouseEmailID.ClientID %>');
        let gstbrn = document.getElementById('<%= txtGSTBRN.ClientID %>');
        let state = document.getElementById('<%= ddlState.ClientID %>');
        let city = document.getElementById('<%= ddlCity.ClientID %>');
        let pincode = document.getElementById('<%= txtPinCode.ClientID %>');
        let status = document.getElementById('<%= ddlStatus.ClientID %>');

        if (whcode && whcode.value.trim() === "") {
            whcode.classList.add("is-invalid");
            isValid = false;
        } else if (whcode) {
            whcode.classList.remove("is-invalid");
        }

        if (whname && whname.value.trim() === "") {
            whname.classList.add("is-invalid");
            isValid = false;
        } else if (whname) {
            whname.classList.remove("is-invalid");
        }

        if (whtype && whtype.value.trim() === "") {
            whtype.classList.add("is-invalid");
            isValid = false;
        } else if (whtype) {
            whtype.classList.remove("is-invalid");
        }

        if (whaddress && whaddress.value.trim() === "") {
            whaddress.classList.add("is-invalid");
            isValid = false;
        } else if (whaddress) {
            whaddress.classList.remove("is-invalid");
        }

        if (name && name.value.trim() === "") {
            name.classList.add("is-invalid");
            isValid = false;
        } else if (name) {
            name.classList.remove("is-invalid");
        }

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

        if (whemail && whemail.value.trim() === "") {
            whemail.classList.add("is-invalid");
            isValid = false;
        }
        else if (whemail && !isValidEmail(whemail.value.trim())) {
            whemail.classList.add("is-invalid");
            isValid = false;
        }
        else if (whemail) {
            whemail.classList.remove("is-invalid");
        }

        if (gstbrn && gstbrn.value.trim() === "") {
            gstbrn.classList.add("is-invalid");
            isValid = false;
        } else if (gstbrn) {
            gstbrn.classList.remove("is-invalid");
        }

        if (state && state.value.trim() === "") {
            state.classList.add("is-invalid");
            isValid = false;
        } else if (state) {
            state.classList.remove("is-invalid");
        }

        if (city && city.value.trim() === "") {
            city.classList.add("is-invalid");
            isValid = false;
        } else if (city) {
            city.classList.remove("is-invalid");
        }

        if (pincode && pincode.value.trim() === "") {
            pincode.classList.add("is-invalid");
            isValid = false;
        } else if (pincode) {
            pincode.classList.remove("is-invalid");
        }

        if (status && status.value === "") {
            status.classList.add("is-invalid");
            isValid = false;
        } else if (status) {
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
        } else {
            fileupload.classList.remove("is-invalid");
        }

        return isValid;
    }

    function bindValidationEvents() {
        let whcode = document.getElementById('<%= txtWarehouseCode.ClientID %>');
        let whname = document.getElementById('<%= txtWarehouseName.ClientID %>');
        let whtype = document.getElementById('<%= ddlWarehouseType.ClientID %>');
        let whaddress = document.getElementById('<%= txtWarehouseAddress.ClientID %>');
        let name = document.getElementById('<%= txtContactPersonName.ClientID %>');
        let contactPersonNo = document.getElementById('<%= txtContactPersonNo.ClientID %>');
        let whemail = document.getElementById('<%= txtWarehouseEmailID.ClientID %>');
        let gstbrn = document.getElementById('<%= txtGSTBRN.ClientID %>');
        let state = document.getElementById('<%= ddlState.ClientID %>');
        let city = document.getElementById('<%= ddlCity.ClientID %>');
        let pincode = document.getElementById('<%= txtPinCode.ClientID %>');
        let status = document.getElementById('<%= ddlStatus.ClientID %>');
        let fileupload = document.getElementById('<%= flpUpload.ClientID %>');

        if (whcode) whcode.oninput = () => whcode.classList.remove("is-invalid");
        if (whname) whname.oninput = () => whname.classList.remove("is-invalid");
        if (whtype) whtype.oninput = () => whtype.classList.remove("is-invalid");
        if (whaddress) whaddress.oninput = () => whaddress.classList.remove("is-invalid");
        if (name) name.oninput = () => name.classList.remove("is-invalid");

        if (contactPersonNo) {
            contactPersonNo.oninput = () => {
                allowOnlyNumberAndLimit10(contactPersonNo);
                contactPersonNo.classList.remove("is-invalid");
            };
        }        if (whemail) whemail.oninput = () => whemail.classList.remove("is-invalid");
        if (gstbrn) gstbrn.oninput = () => gstbrn.classList.remove("is-invalid");
        if (state) state.oninput = () => state.classList.remove("is-invalid");
        if (city) city.oninput = () => city.classList.remove("is-invalid");
        if (pincode) pincode.oninput = () => pincode.classList.remove("is-invalid");
        if (status) status.oninput = () => status.classList.remove("is-invalid");
        if (fileupload) fileupload.onchange = () => fileupload.classList.remove("is-invalid");
    }

  
    function clearFile() {

        var oldInput = document.getElementById('<%= flpUpload.ClientID %>');
        if (!oldInput) return;

        // Create new file input
        var newInput = document.createElement("input");
        newInput.type = "file";
        newInput.id = oldInput.id;
        newInput.name = oldInput.name;
        newInput.className = oldInput.className;

        // Replace
        oldInput.parentNode.replaceChild(newInput, oldInput);
    }

    document.addEventListener("DOMContentLoaded", bindValidationEvents);

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindValidationEvents);


</script>

<script src="js/Warehouse_Master.js"></script>
<script src="js/Sidebar_Panel.js"></script>
</asp:Content>
