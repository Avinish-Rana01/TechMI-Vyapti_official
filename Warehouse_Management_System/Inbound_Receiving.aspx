<%@ Page Title="" Language="C#" MasterPageFile="~/Warehouse_Management_System/Warehouse_Management.Master" AutoEventWireup="true" CodeBehind="Inbound_Receiving.aspx.cs" Inherits="Markss_Retail_Suite.Warehouse_Management_System.Inbound_Receiving" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Warehouse Management System | Inbound / Receiving (GRN)</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span page-id="Inbound_Operations" id="find_page"></span>
    <span page-id2="Inbound_Receiving" id="find_page2"></span>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="toolbar py-5 pb-lg-2" id="kt_toolbar">
                <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
                    <div class="page-title d-flex flex-column me-3 flex-start">
                        <h1 class="d-flex text-white m-0 fs-3">
                            <asp:Label ID="lblMenuName" runat="server"></asp:Label>
                            <span class="h-20px border-gray-500 border-start mx-3"></span>
                            <asp:Label ID="lblPageName" runat="server" Text="Inbound / Receiving (GRN)"></asp:Label>
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
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">GRN No<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtGRNNo" runat="server" class="form-control form-control-solid" placeholder="Enter GRN No" required="required" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">GRN Date<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtGRNDate" runat="server" class="date-start2 form-control form-control-solid" placeholder="Select GRN Date" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">PO No<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPONo" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select PO No"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="PO1001"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="PO1002"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Code<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlVendorCode" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Vendor Code"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Vendor 1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Vendor 2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtVendorName" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor Name" required="required" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Code<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlWarehouseCode" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Warehouse Code"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="W001"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="W002"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtWarehouseName" runat="server" class="form-control form-control-solid" placeholder="Enter Warehouse Name" required="required" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Item Code<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlItemCode" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Item Code"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="PR001"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="PR002"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-6 col-xl-6">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Item Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtItemName" runat="server" class="form-control form-control-solid" placeholder="Enter Item Name" required="required" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Expected Qty<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtExpectedQty" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Expected Qty" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Received Qty<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtReceivedQty" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Received Qty" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Short / Excess Qty<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtShortQty" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Short / Excess Qty" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Damaged Qty<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtDamagedQty" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Damaged Qty" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Batch / Serial No (if applicable)</label>
                                            <asp:TextBox ID="txtBatchNo" runat="server" class="form-control form-control-solid" placeholder="Enter Batch / Serial No"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Expiry Date (if applicable)</label>
                                            <asp:TextBox ID="txtExpiryDate" runat="server" class="date-end2 form-control form-control-solid" placeholder="Select Expiry Date" ></asp:TextBox>
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
                            <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="GRNNo" HeaderText="GRN No" />
                                    <asp:BoundField DataField="GRNDate" HeaderText="GRN Date" />
                                    <asp:BoundField DataField="PONo" HeaderText="PO No" />
                                    <asp:BoundField DataField="VendorCode" HeaderText="Vendor Code" />
                                    <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" />
                                    <asp:BoundField DataField="WarehouseCode" HeaderText="Warehouse Code" />
                                    <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse Name" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                    <asp:BoundField DataField="ExpectedQty" HeaderText="Expected Qty" />
                                    <asp:BoundField DataField="ReceivedQty" HeaderText="Received Qty" />
                                    <asp:BoundField DataField="ShortQty" HeaderText="Short Qty" />
                                    <asp:BoundField DataField="DamagedQty" HeaderText="Damaged Qty" />
                                    <asp:BoundField DataField="BatchNo" HeaderText="Batch / Serial No" />
                                    <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date" />
                                    <asp:BoundField DataField="QCStatus" HeaderText="QC Status" />
                                    <asp:BoundField DataField="GRNStatus" HeaderText="GRN Status" />
                                    <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                    <asp:BoundField DataField="CreatedDateTime" HeaderText="Created Date/Time" />
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
    <script src="js/Grid_Details.js"></script>
    <script src="../HQ_Operations/js/Sidebar_Panel.js"></script>
</asp:Content>
