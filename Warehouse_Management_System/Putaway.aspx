<%@ Page Title="" Language="C#" MasterPageFile="~/Warehouse_Management_System/Warehouse_Management.Master" AutoEventWireup="true" CodeBehind="Putaway.aspx.cs" Inherits="Markss_Retail_Suite.Warehouse_Management_System.Putaway" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Warehouse Management System | Put-Away</title>
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
                            <asp:Label ID="lblPageName" runat="server" Text="Put-Away"></asp:Label>
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
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Put-Away No<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPutAwayNo" runat="server" class="form-control form-control-solid" placeholder="Enter Put-Away No" required="required" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Put-Away Date<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPutAwayDate" runat="server" class="date-start2 form-control form-control-solid" placeholder="Enter Put-Away Date" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">GRN No<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlGRN" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select GRN No"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="GRN1001"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="GRN1002"></asp:ListItem>
                                            </asp:DropDownList>
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
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">From Location<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlFromLoc" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select From Location"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Location 1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Location 2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">To Location<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlToLoc" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select To Location"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Location 1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Location 2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Assigned User<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlAssignedUser" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select User"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="User 1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="User 2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                                        <div class="mb-3">
                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Put-Away Status<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                                <asp:ListItem Value="" Text="Select Put-Away Status"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Status 1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Status 2"></asp:ListItem>
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
                            <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PutAwayNo" HeaderText="Put-Away No" />
                                    <asp:BoundField DataField="PutAwayDate" HeaderText="Put-Away Date" />
                                    <asp:BoundField DataField="GRNNo" HeaderText="GRN No" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                    <asp:BoundField DataField="FromLoc" HeaderText="From Location" />
                                    <asp:BoundField DataField="ToLoc" HeaderText="To Location" />
                                    <asp:BoundField DataField="AssignedUser" HeaderText="Assigned User" />
                                    <asp:BoundField DataField="PutAwayStatus" HeaderText="Put-Away Status" />
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
