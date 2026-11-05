<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="EAN_Master.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.EAN_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HQ Operations | EAN Master</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }

        .hiddencol {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span page-id="Master_Management" id="find_page"></span>
    <span page-id2="EAN_Master" id="find_page2"></span>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:HiddenField ID="hfSidebarOpen" runat="server" />

    <div class="toolbar py-5 pb-lg-2" id="kt_toolbar">
        <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
            <div class="page-title d-flex flex-column me-3 flex-start">
                <h1 class="d-flex text-white m-0 fs-3">
                    <asp:Label ID="lblMenuName" runat="server" Text="HQ Operations"></asp:Label>
                    <span class="h-20px border-gray-500 border-start mx-3"></span>
                    <asp:Label ID="lblPageName" runat="server" Text="EAN Master"></asp:Label>
                </h1>
            </div>
            <asp:HiddenField ID="hdf_SAP_Value" runat="server" />
            <div class="page-title d-flex me-3" runat="server" id="rdbcheck">
                <asp:RadioButton ID="rdbSingleEntry" runat="server" GroupName="status" Text="Single Entry" CssClass="sidebar-toggler" />
                <asp:RadioButton ID="rdbMultipleEntry" runat="server" GroupName="status" Text="Multiple Entry" CssClass="sidebar-toggler" />
            </div>
        </div>
    </div>
    <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
        <div class="content flex-row-fluid" id="kt_content">

            <div class="v-sidebar-wrapper">
                <div class="v-sidebar">
                    <div class="sidebar-header">
                        <h4 class="mb-0">
                            <asp:Label ID="lblTitle" runat="server" Text="EAN DETAILS" Visible="true"></asp:Label>
                        </h4>
                        <button type="button" class="sidebar-close-btn" id="sidebar-close-btn">
                            <i class="fa fa-times fa-2x"></i>
                        </button>
                    </div>

                    <hr class="sidebar-hr">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="divSingleEntry">
                                <asp:HiddenField ID="hdnID" runat="server" />
                                <div class="row">
                                    <%--<div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">MTYP<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtMTYP" runat="server" class="form-control form-control-solid" placeholder="Enter MTYP" required="required"></asp:TextBox>
                                </div>
                            </div>--%>
                                    <div class="col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Article No<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlMaterial" runat="server" class="form-control form-control-solid custom-select2" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged" AutoPostBack="true" required="required">
                                            </asp:DropDownList>
                                            <div class="invalid-feedback">Material is required.</div>
                                            <%-- <asp:TextBox ID="txtMaterial" runat="server" class="form-control form-control-solid" placeholder="Enter Material" required="required"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">UOM<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtUOM" runat="server" class="form-control form-control-solid" placeholder="Enter UOM" required="required"></asp:TextBox>
                                </div>
                            </div>--%>
                                    <div class="col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">UOM<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlUOM" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select UOM"></asp:ListItem>
                                                <%--<asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="XYZ"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <div class="invalid-feedback">UOM is required.</div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Category<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlCategory" runat="server" class="form-control form-control-solid custom-select2" Enabled="false" required="required">
                                                <asp:ListItem Value="" Text="Select Category"></asp:ListItem>
                                                <%--<asp:ListItem Value="1" Text="ABC"></asp:ListItem> 
                                        <asp:ListItem Value="2" Text="XYZ"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <div class="invalid-feedback">Category is required.</div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Size<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlSize" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Size"></asp:ListItem>
                                                <asp:ListItem Value="XS" Text="Extra Small"></asp:ListItem>
                                                <asp:ListItem Value="S" Text="Small"></asp:ListItem>
                                                <asp:ListItem Value="M" Text="Medium"></asp:ListItem>
                                                <asp:ListItem Value="l" Text="Large"></asp:ListItem>
                                                <asp:ListItem Value="XL" Text="Extra Large"></asp:ListItem>
                                                <asp:ListItem Value="XXL" Text="Extra Extra Large"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="invalid-feedback">Size is required.</div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">EAN<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtEAN" runat="server" class="form-control form-control-solid" placeholder="Enter EAN" required="required"></asp:TextBox>
                                            <div class="invalid-feedback">EAN is required.</div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Status<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                             <%--   <asp:ListItem Value="" Text="Select Status"></asp:ListItem>--%>
                                                <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="invalid-feedback">Status is required.</div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">MRP<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtMRP" runat="server" class="form-control form-control-solid" placeholder="Enter MRP" required="required"></asp:TextBox>
                                            <div class="invalid-feedback">MRP is required.</div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Effective From<span class="text-danger">*</span></label>
                                            <asp:TextBox
                                                ID="txtEffective"
                                                runat="server"
                                                class="date-start2 form-control form-control-solid"
                                                placeholder="Effective From Date"
                                                required="required">
                                            </asp:TextBox>

                                            <div class="invalid-feedback">Effective From is required.</div>
                                        </div>
                                    </div>
                                    <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">
                                        <%--                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" class="btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Submit" UseSubmitBehavior="false"/>--%>
                                        <%-- <asp:Button ID="Button2" runat="server" OnClick="btnClear_Click" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear" UseSubmitBehavior="false"/>--%>
                                        <asp:Button ID="btnSubmit" runat="server" class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Submit" UseSubmitBehavior="false" OnClick="btnSubmit_Click" OnClientClick="if (!validateSingleEntry()) { return false; }" />
                                        <asp:Button ID="btnClear" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear" UseSubmitBehavior="false" OnClick="btnClear_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div id="divMultipleEntry" style="display: none;">
                        <div class="row">
                            <div class="col-md-6 col-lg-12">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Upload File<span class="text-danger">*</span></label>
                                    <asp:FileUpload ID="flpUpload" runat="server" class="form-control form-control-solid" required="required" />
                                    <div class="invalid-feedback">Please Select file to upload.</div>
                                </div>
                            </div>

                            <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">
                                <%-- <asp:Button ID="btnFileUpload" runat="server" OnClick="btnFileUpload_Click" class="btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Upload File" UseSubmitBehavior="false"/>
                                <asp:Button ID="btnFileClear" runat="server" OnClick="btnFileClear_Click" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear File" UseSubmitBehavior="false"/>--%>

                                <asp:Button ID="btnFileUpload" runat="server" class="btnFile btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Upload File" UseSubmitBehavior="false" OnClick="btnFileUpload_Click" OnClientClick="if (!validateMultipleEntry()) { return false; }" />
                                <button type="button" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" onclick="clearFile()">Clear File</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="v-sidebar-fade"></div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="card mt-3  mb-15" id="Div_grd" runat="server" visible="true">
                        <div class="card-body">
                            <asp:GridView ID="grdEANMaster" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%" Visible="true" OnRowDataBound="grdEANMaster_RowDataBound" DataKeyNames="MATERIAL_ID, CAT_ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField = "MYTP" HeaderText="MYTP" />--%>
                                    <asp:BoundField DataField="EAN_ID" HeaderText="EAN ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true" />
                                    <%--<asp:BoundField DataField = "MATERIAL_CODE" HeaderText="Material" />--%>
                                    <asp:BoundField DataField="ARTICLE_NO" HeaderText="Article No" />
                                    <asp:BoundField DataField="ARTICLE_DESCRIPTION" HeaderText="Article Description" />
                                    <asp:BoundField DataField="SIZE" HeaderText="Size" />
                                    <asp:BoundField DataField="EAN" HeaderText="EAN" />
                                    <asp:BoundField DataField="CAT_CODE" HeaderText="Category Code" />
                                    <asp:BoundField DataField="CAT_NAME" HeaderText="Category" />
                                    <asp:BoundField DataField="UOM_NAME" HeaderText="UOM" />
                                    <asp:BoundField DataField="MRP" HeaderText="MRP" />
                                    <asp:BoundField DataField="EFFECTIVE_FROM_DATE" HeaderText="Effective From" />

                                    <asp:BoundField DataField="STATUS" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" class="text-theme-green" CommandArgument='<%# Eval("EAN_ID") %>' OnClick="lnkEdit_Click"><i class="fa fa-edit text-theme-green"></i> Edit</asp:LinkButton>&nbsp; &nbsp; | &nbsp;  
                                    <asp:LinkButton ID="lnlActive" runat="server" class="text-theme-green" CommandArgument='<%# Eval("EAN_ID") %>' OnClick="lnlActive_Click" OnClientClick="return confirm('Are you sure you want to active this category?');"><i class="fa fa-check text-theme-green"></i> Active</asp:LinkButton>
                                            <asp:LinkButton ID="lnkInactive" runat="server" class="text-theme-red" CommandArgument='<%# Eval("EAN_ID") %>' OnClick="lnkInactive_Click" OnClientClick="return confirm('Are you sure you want to inactive this category?');"><i class="fa fa-ban text-theme-red"></i> Inactive</asp:LinkButton>
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
    <script src="js/EAN_Master.js"></script>
    <script src="js/Sidebar_Panel.js"></script>

    <script>

        function validateSingleEntry() {
            let isValid = true;

            let material = document.getElementById('<%= ddlMaterial.ClientID %>');
            let uom = document.getElementById('<%= ddlUOM.ClientID %>');
            let category = document.getElementById('<%= ddlCategory.ClientID %>');
            let size = document.getElementById('<%= ddlSize.ClientID %>');
            let ean = document.getElementById('<%= txtEAN.ClientID %>');
            let status = document.getElementById('<%= ddlStatus.ClientID %>');

            if (material && material.value.trim() === "") {
                material.classList.add("is-invalid");
                isValid = false;
            }
            else if (material) {
                material.classList.remove("is-invalid");
            }

            if (uom && uom.value.trim() === "") {
                uom.classList.add("is-invalid");
                isValid = false;
            }
            else if (uom) {
                uom.classList.remove("is-invalid");
            }

            if (category && category.value.trim() === "") {
                category.classList.add("is-invalid");
                isValid = false;
            }
            else if (category) {
                category.classList.remove("is-invalid");
            }

            if (size && size.value.trim() === "") {
                size.classList.add("is-invalid");
                isValid = false;
            }
            else if (size) {
                size.classList.remove("is-invalid");
            }

            if (ean && ean.value.trim() === "") {
                ean.classList.add("is-invalid");
                isValid = false;
            }
            else if (ean) {
                ean.classList.remove("is-invalid");
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
            let material = document.getElementById('<%= ddlMaterial.ClientID %>');
            let uom = document.getElementById('<%= ddlUOM.ClientID %>');
            let category = document.getElementById('<%= ddlCategory.ClientID %>')
            let size = document.getElementById('<%= ddlSize.ClientID %>')
            let ean = document.getElementById('<%= txtEAN.ClientID %>');
            let status = document.getElementById('<%= ddlStatus.ClientID %>');
            let fileupload = document.getElementById('<%= flpUpload.ClientID %>');

            if (material) material.oninput = () => material.classList.remove("is-invalid");
            if (uom) uom.oninput = () => uom.classList.remove("is-invalid");
            if (category) category.oninput = () => category.classList.remove("is-invalid");
            if (size) size.oninput = () => size.classList.remove("is-invalid");
            if (ean) ean.oninput = () => ean.classList.remove("is-invalid");
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

</asp:Content>
