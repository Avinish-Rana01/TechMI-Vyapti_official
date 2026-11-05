<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="Stock_Movement.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.Stock_Movement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HQ Operations | Stock Movement</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span page-id="Warehouse_Operations" id="find_page"></span>
    <span page-id2="Stock_Movement" id="find_page2"></span>

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
                            <asp:Label ID="lblPageName" runat="server" Text="Stock Movement"></asp:Label>
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
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Stock Movement ID<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtStockMovID" runat="server" class="form-control form-control-solid" placeholder="Enter Stock Movement ID" required="required" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Movement Type<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlMovementType" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Movement Type"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Inward"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Outward"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Adjustment"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Damage"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Return"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Internal Transfer"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Movement Date<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtMovementDate" runat="server" class="date-start2 form-control form-control-solid" placeholder="Select Movement Date" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Code<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlWarehouseCode" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Warehouse Code"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="W001"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="W002"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-6">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtWarehouseName" runat="server" class="form-control form-control-solid" placeholder="Enter Warehouse Name" required="required" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Reference No<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtRefNo" runat="server" class="form-control form-control-solid" placeholder="Enter Reference No" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Reference Date<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtRefDate" runat="server" class="date-start2 form-control form-control-solid" placeholder="Select Reference Date" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Source Type<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlSourceType" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Source Type"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Supplier"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Store"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Warehouse"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Customer"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Source Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtSourceName" runat="server" class="form-control form-control-solid" placeholder="Enter Source Name" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Destination Type<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlDestinationType" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Destination Type"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Supplier"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Store"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Warehouse"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Customer"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Destination Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtDestinationName" runat="server" class="form-control form-control-solid" placeholder="Enter Destination Name" required="required"></asp:TextBox>
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
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Quantity<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtQty" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Quantity" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Reason Code<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlReasonCode" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Reason Code"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Sale"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Damage"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Expiry"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Audit Adjustment"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Transfer"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-9">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Remarks</label>
                                            <asp:TextBox ID="txtRemarks" runat="server" class="form-control form-control-solid" placeholder="Enter Remarks"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Status<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Status"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Draft"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Posted"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Cancelled"></asp:ListItem>
                                            </asp:DropDownList>
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
                            <asp:GridView ID="grdStockMovement" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StockMovID" HeaderText="Stock Movement ID" />
                                    <asp:BoundField DataField="MovType" HeaderText="Movement Type" />
                                    <asp:BoundField DataField="MovDate" HeaderText="Movement Date" />
                                    <asp:BoundField DataField="WarehouseCode" HeaderText="Warehouse Code" />
                                    <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse Name" />
                                    <asp:BoundField DataField="RefNo" HeaderText="Reference No" />
                                    <asp:BoundField DataField="refDate" HeaderText="Reference Date" />
                                    <asp:BoundField DataField="SourceType" HeaderText="Source Type" />
                                    <asp:BoundField DataField="SourceName" HeaderText="Source Name" />
                                    <asp:BoundField DataField="DestinationType" HeaderText="Destination Type" />
                                    <asp:BoundField DataField="DestinationName" HeaderText="Destination Name" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                    <asp:BoundField DataField="Category" HeaderText="Category" />
                                    <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                    <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                    <asp:BoundField DataField="ReasonCode" HeaderText="Reason Code" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                    <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                    <asp:BoundField DataField="CreatedDateTime" HeaderText="Created Date/Time" />
                                    <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" />
                                    <asp:BoundField DataField="ApprovedDateTime" HeaderText="Approved Date/Time" />
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
    <script src="js/Stock_Movement.js"></script>
    <script src="js/Sidebar_Panel.js"></script>
</asp:Content>
