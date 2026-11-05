<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="Inter_Branch_Transfer.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.Inter_Branch_Transfer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HQ Operations | Inter Branch Transfer</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span page-id="Warehouse_Operations" id="find_page"></span>
    <span page-id2="Inter_Branch_Transfer" id="find_page2"></span>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:HiddenField ID="hfSidebarOpen" runat="server"/>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="toolbar py-5 pb-lg-2" id="kt_toolbar">
                <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
                    <div class="page-title d-flex flex-column me-3 flex-start">
                        <h1 class="d-flex text-white m-0 fs-3">
                            <asp:Label ID="lblMenuName" runat="server" Text="HQ Operations"></asp:Label>
                            <span class="h-20px border-gray-500 border-start mx-3"></span>
                            <asp:Label ID="lblPageName" runat="server" Text="Inter Branch Transfer"></asp:Label>
                        </h1>
                    </div>

                    <div class="page-title d-flex me-3">
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
                                <h5 class="sidebar-title"></h5>
                                <button type="button" class="sidebar-close-btn" id="sidebar-close-btn">
                                    <i class="fa fa-times fa-2x"></i>
                                </button>
                            </div>

                            <hr class="sidebar-hr">

                            <div id="divSingleEntry">
                                <div class="row">
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Transfer ID<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtTransferID" runat="server" class="form-control form-control-solid" placeholder="Enter Transfer ID" required="required" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">From Location<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlFromLoc" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select From Location"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Location 1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Location 2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Location 3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">To Location<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlToLoc" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select To Location"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Location 1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Location 2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Location 3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Request Date<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtReqDate" runat="server" class="date-start2 form-control form-control-solid" placeholder="Select Request Date" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Dispatch Date<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtDispatchDate" runat="server" class="date-start2 form-control form-control-solid" placeholder="Select Dispatch Date" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Expected Delivery Date<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtExpDelDate" runat="server" class="date-end2 form-control form-control-solid" placeholder="Select Expected Delivery Date" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vehicle Required<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlVehicleReq" runat="server" class="vehicle form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-6 col-lg-3" id="transporter_name" style="display:none;">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Transporter Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtTransporterName" runat="server" class="form-control form-control-solid" placeholder="Enter Transporter Name" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3" id="vehicle_no" style="display:none;">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vehicle No<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtVehicleNo" runat="server" class="form-control form-control-solid" placeholder="Enter Vehicle No" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3" id="contact_person_name" style="display:none;">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtContactPersonName" runat="server" class="form-control form-control-solid" placeholder="Enter Contact Person Name" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3" id="contact_person_no" style="display:none;">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person No</label>
                                            <asp:TextBox ID="txtContactPersonNo" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Contact Person No"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Priority<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPriority" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Normal"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Urgent"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Item Code<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlItemCode" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Item Code"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="PR001"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="PR002"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-9">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Item Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtItemName" runat="server" class="form-control form-control-solid" placeholder="Enter Item Name" required="required" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Category<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlCategory" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Category"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="C001"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="C002"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Brand<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlBrand" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Brand"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="B001"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="B002"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Requested Quantity<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtReqQty" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Requested Quantity" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Approved Quantity<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtApprovedQty" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Approved Quantity" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Disptached Quantity<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtDisptachedQty" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Disptached Quantity" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Transfer Status<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Status"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Requested"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Approved"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Dispatched"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="In-Transit"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Received"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="Closed"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-lg-6">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Remarks</label>
                                            <asp:TextBox ID="txtRemarks" runat="server" class="form-control form-control-solid" placeholder="Enter Remarks"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="mt-1 col-md-12 col-lg-12 flex flex-center justify-content-center">
                                        <asp:Button ID="btnSubmit" runat="server" class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Submit" UseSubmitBehavior="false" />
                                        <asp:Button ID="btnClear" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear" UseSubmitBehavior="false" />
                                    </div>
                                </div>
                            </div>

                            <div id="divMultipleEntry" style="display: none;">
                                <div class="row">
                                    <div class="col-md-6 col-lg-12">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Upload File<span class="text-danger">*</span></label>
                                            <asp:FileUpload ID="flpUpload" runat="server" class="form-control form-control-solid" required="required" />
                                        </div>
                                    </div>

                                    <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">
                                        <asp:Button ID="btnFileUpload" runat="server" class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Upload File" UseSubmitBehavior="false" />
                                        <asp:Button ID="btnFileClear" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear File" UseSubmitBehavior="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="v-sidebar-fade"></div>

                    <div class="card mt-3  mb-15" id="Div_grd" runat="server" visible="true">
                        <div class="card-body">
                            <asp:GridView ID="grdInterBranchTransfer" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TransferID" HeaderText="Transfer ID" />
                                    <asp:BoundField DataField="FromLoc" HeaderText="From Location" />
                                    <asp:BoundField DataField="ToLoc" HeaderText="To Location" />
                                    <asp:BoundField DataField="RequestDate" HeaderText="Request Date" />
                                    <asp:BoundField DataField="DispatchDate" HeaderText="Dispatch Date" />
                                    <asp:BoundField DataField="ExpectedDate" HeaderText="Expected Delivery Date" />
                                    <asp:BoundField DataField="VehicleReq" HeaderText="Vehicle Required" />
                                    <asp:BoundField DataField="TransporterName" HeaderText="Transporter Name" />
                                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" />
                                    <asp:BoundField DataField="ContactPersonName" HeaderText="Contact Person Name" />
                                    <asp:BoundField DataField="ContactPersonNo" HeaderText="Contact Person No" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                    <asp:BoundField DataField="Category" HeaderText="Category" />
                                    <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                    <asp:BoundField DataField="ReqQty" HeaderText="Requested Quantity" />
                                    <asp:BoundField DataField="ApprovedQty" HeaderText="Approved Quantity" />
                                    <asp:BoundField DataField="DispatchedQty" HeaderText="Dispatched Quantity" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                    <asp:BoundField DataField="Status" HeaderText="Transfer Status" />
                                    <asp:BoundField DataField="RequestedBy" HeaderText="Requested By" />
                                    <asp:BoundField DataField="RequestedDateTime" HeaderText="Requested Date/Time" />
                                    <asp:BoundField DataField="DispatchBy" HeaderText="Dispatch By" />
                                    <asp:BoundField DataField="DispatchDateTime" HeaderText="Dispatch Date/Time" />
                                    <asp:BoundField DataField="ReceiveBy" HeaderText="Receive By" />
                                    <asp:BoundField DataField="ReceiveDateTime" HeaderText="Receive Date/Time" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.8;">
                <span style="border-width: 0px; position: fixed; padding: 10px; background-color: #FFFFFF; font-size: 25px; left: 43%; top: 40%; border-radius: 50px;">Please Wait...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script src="js/Inter_Branch_Transfer.js"></script>
    <script src="js/Sidebar_Panel.js"></script>

    
</asp:Content>
