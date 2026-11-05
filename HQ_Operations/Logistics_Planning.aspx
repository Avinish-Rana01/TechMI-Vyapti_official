<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="Logistics_Planning.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.Logistics_Planning" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HQ Operations | Logistics Planning</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span page-id="Warehouse_Operations" id="find_page"></span>
    <span page-id2="Logistics_Planning" id="find_page2"></span>

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
                            <asp:Label ID="lblPageName" runat="server" Text="Logistics Planning"></asp:Label>
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
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Logistics Plan ID<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtLogisticsPlanID" runat="server" class="form-control form-control-solid" placeholder="Enter Logistics Plan ID" required="required" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Plan Date<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPlanDate" runat="server" class="date-start2 form-control form-control-solid" placeholder="Select Plan Date" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Source Warehouse<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlSourceWarehouse" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Warehouse"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Warehouse 1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Warehouse 2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Warehouse 3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Destination Location<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlDesLoc" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Destination Location"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Location 1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Location 2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Location 3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Movement Type<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlMovementType" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Movement Type"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Stock Transfer"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Sales Dispatch"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Return"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Planned Dispatch Date<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPlannedDispatchDate" runat="server" class="date-start2 form-control form-control-solid" placeholder="Select Planned Dispatch Date" required="required"></asp:TextBox>
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
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Load Type<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlLoadType" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Load Type"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Full Truck"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Partial Load"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vehicle Type<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlVehicleType" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Vehicle Type"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="2 Wheeler"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="3 Wheeler"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Mini Truck"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="LCV"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="HCV"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vehicle Capacity<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtVehicleCap" runat="server" class="form-control form-control-solid" placeholder="Enter Vehicle Capacity" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Transporter Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtTransporterName" runat="server" class="form-control form-control-solid" placeholder="Enter Transporter Name" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vehicle No<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtVehicleNo" runat="server" class="form-control form-control-solid" placeholder="Enter Vehicle No" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtContactPersonName" runat="server" class="form-control form-control-solid" placeholder="Enter Contact Person Name" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person No</label>
                                            <asp:TextBox ID="txtContactPersonNo" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Contact Person No"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Total Quantity<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtTotalQty" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Total Quantity" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Total Weight<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtTotalWeight" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Total Weight" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Total Value<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtTotalVal" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Total Value" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Plan Status<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPlanStatus" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Plan Status"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Planned"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Assigned"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Dispatched"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Completed"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Cancelled"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Planned By<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPlannedBy" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="User 1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="User 2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-lg-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Approved By<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlApprovedBy" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="User 1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="User 2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-lg-12">
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
                            <asp:GridView ID="grdLogisticsPlanning" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LogisticsID" HeaderText="Logistics Plan ID" />
                                    <asp:BoundField DataField="PlanDate" HeaderText="Plan Date" />
                                    <asp:BoundField DataField="SourceWarehouse" HeaderText="Source Warehouse" />
                                    <asp:BoundField DataField="DestinationLoc" HeaderText="Destination Location" />
                                    <asp:BoundField DataField="MovementType" HeaderText="Movement Type" />
                                    <asp:BoundField DataField="PlannedDispatchDate" HeaderText="Planned Dispatch Date" />
                                    <asp:BoundField DataField="ExpectedDate" HeaderText="Expected Delivery Date" />
                                    <asp:BoundField DataField="LoadType" HeaderText="Load Type" />
                                    <asp:BoundField DataField="VehicleType" HeaderText="Vehicle Type" />
                                    <asp:BoundField DataField="VehicleCapacity" HeaderText="Vehicle Capacity" />
                                    <asp:BoundField DataField="TransporterName" HeaderText="Transporter Name" />
                                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" />
                                    <asp:BoundField DataField="ContactPersonName" HeaderText="Contact Person Name" />
                                    <asp:BoundField DataField="ContactPersonNo" HeaderText="Contact Person No" />
                                    <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" />
                                    <asp:BoundField DataField="TotalWeight" HeaderText="Total Weight" />
                                    <asp:BoundField DataField="TotalValue" HeaderText="Total Value" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                    <asp:BoundField DataField="Status" HeaderText="Plan Status" />
                                    <asp:BoundField DataField="PlannedBy" HeaderText="Planned By" />
                                    <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" />
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
    <script src="js/Logistics_Planning.js"></script>
    <script src="js/Sidebar_Panel.js"></script>

</asp:Content>
