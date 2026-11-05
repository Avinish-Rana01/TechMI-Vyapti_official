<%@ Page Title="" Language="C#" MasterPageFile="~/Retail_Suite.Master" AutoEventWireup="true" CodeBehind="Invoice_Generation.aspx.cs" Inherits="Markss_Retail_Suite.Invoice_Generation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Retail Suite | Vendor Management System | Invoice Generation</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" >
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>

    <div class="toolbar py-5 pb-lg-5" id="kt_toolbar">
        <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
            <div class="page-title d-flex flex-column me-3 flex-start">
               <h1 class="d-flex text-white m-0 fs-3">
                   <asp:Label ID="lblMenuName" runat="server"></asp:Label>      
                   <span class="h-20px border-gray-500 border-start mx-3"></span>
                   <asp:Label ID="lblPageName" runat="server" Text="Invoice Generation"></asp:Label>      
               </h1>
            </div>

            <div class="page-title d-flex flex-column me-3 flex-end">
                <h1 class="d-flex text-white m-0 fs-3 text-uppercase">Markss Retail Suite</h1>
            </div>
        </div>
    </div>
    <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
        <div class="content flex-row-fluid" id="kt_content">
           <div class="card card-flush">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">PO Number <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPONo" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                    <asp:ListItem Value="" Text="Select PO No"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="PO12345"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="PO21345"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Invoice Date <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtInvoiceDate" runat="server" class="date-start2 form-control form-control-solid" placeholder="Select Invoice Date" require="required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-6">
                            <div class="mb-3">
                                <label for="txtTransporterName" class="mb-1 form-label fs-6 fw-bold text-gray-700">Transporter Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtTransporterName" type="text" runat="server" class="form-control form-control-solid" placeholder="Enter Transporter Name" ClientIDMode="Static" required="required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vehicle Number <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtVehicleNo" runat="server" class="form-control form-control-solid" placeholder="Enter Vehicle Number" require="required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">PO Line Item <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPOLineItem" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                    <asp:ListItem Value="" Text="Select PO Line Item"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="ABC"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">PO Line Item Quantity <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtPOLineItemQty" type="text" runat="server" class="quantity form-control form-control-solid" placeholder="Enter PO Line Item Quantity" ClientIDMode="Static" required="required"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">PO Line Item Amount <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtPOLineItemAmt" type="text" runat="server" class="quantity form-control form-control-solid" placeholder="Enter PO Line Item Amount" ClientIDMode="Static" required="required"  ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Invoice Quantity <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtInvoiceQty" type="text" runat="server" class="inv_qty quantity form-control form-control-solid" placeholder="Enter Invoice Quantity" ClientIDMode="Static" required="required"></asp:TextBox>
                                </div>
                        </div>
                        <div class="mt-3 col-md-6 col-lg-3 flex flex-center justify-content-center">
                            <asp:Button ID="btnAdd" runat="server" class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1 w-50" Text="Add" UseSubmitBehavior="false"/>
                            <asp:Button ID="btnClear" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1 w-50" Text="Clear" UseSubmitBehavior="false"/>
                        </div>
                    </div>
                </div>
            </div>

           <div class="card mt-3  mb-15" id="Div_grd" runat="server" visible="true">
            <div class="card-body">
                <asp:GridView ID="grdAddInvoice" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                    CellSpacing="0" Width="100%" Visible="true">
                    <Columns>       
                        <asp:TemplateField HeaderText="Sr.No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField = "PONo" HeaderText = "PO No" />
                        <asp:BoundField DataField = "InvoiceDate" HeaderText="Invoice Date" />
                        <asp:BoundField DataField = "LineItem" HeaderText="PO Line Item" />
                        <asp:BoundField DataField = "LineItemQty" HeaderText="PO Line Item Qty" />
                        <asp:BoundField DataField = "POLineItemAmt" HeaderText="PO Line Item Amount" />
                        <asp:BoundField DataField = "InvoiceQty" HeaderText="Invoice Qty" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" class="text-theme-red" OnClientClick="return confirm('Are you sure you want to delete?');"><i class="fa fa-trash text-theme-red"></i> Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                     </Columns>
                </asp:GridView>

                <center>
                        <asp:Button ID="btnGenerateInvoice" runat="server" class="btn btn-primary select-basic-multiple-seven mt-1 mb-3" Text="Generate Invoice" UseSubmitBehavior="false"/>
                    </center>

                    <asp:GridView ID="grdFinalInvoiceGrid" runat="server" AutoGenerateColumns = "false" 
                            class="display nowrap table table-hover table-striped table-bordered" 
                            cellspacing="0" width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO No">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPONo" runat="server" class="text-theme-green" OnClick="View"> PO12345</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "TransporterName" HeaderText="Transporter Name" />
                            <asp:BoundField DataField = "VehicleNo" HeaderText="Vehicle Number" />
                            <asp:BoundField DataField = "CreatedBy" HeaderText="Created By" />
                            <asp:BoundField DataField = "CreatedDateTime" HeaderText="Created Date/Time" />
                        </Columns>
                    </asp:GridView>
            </div>
        </div>
        </div>
    </div>

    <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 95%;">
        <h2 class="modal_heading">VIEW DETAILS</h2>
        <asp:Button ID="btnCancel" runat="server" UseSubmitBehavior="false" Text="X" class="btn modal_closeBtn text-white" OnClick="btnCancel_Click" Style="background: #f00; margin-top: 3px;padding: 5px 12px;" />

        <div class="modal-body mt-2">
            <div class="row">
                <div class="col-md-6 col-lg-4 mb-3">
                    <h6 class="text-start">PO Number : <asp:Label ID="lblPONumber" runat="server" Font-Bold="true" Text="PO12345" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-4 mb-3">
                    <h6 class="text-start">Transporter Name : <asp:Label ID="lblTransporterName" runat="server" Font-Bold="true" Text="XYZ Transport" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-4 mb-3">
                    <h6 class="text-start">Vehicle No : <asp:Label ID="lblVehicleNo" runat="server" Font-Bold="true" Text="MH-01-AD-0001" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-12 col-lg-12 col-xxl-12">
                    <asp:GridView ID="grdViewDetails" runat="server" AutoGenerateColumns="false"
                        class="display nowrap table table-hover table-striped table-bordered"
                        CellSpacing="0" Width="100%">
                        <Columns>
                            <asp:BoundField DataField = "InvoiceNo" HeaderText="Invoice Number" />
                            <asp:BoundField DataField = "InvoiceAmt" HeaderText="Invoice Amount" />
                            <asp:BoundField DataField = "InvoiceDate" HeaderText="Invoice Date" />
                            <asp:BoundField DataField = "LineItem" HeaderText="PO Line Item" />
                            <asp:BoundField DataField = "LineItemQty" HeaderText="PO Line Item Qty" />
                            <asp:BoundField DataField = "POLineItemAmt" HeaderText="PO Line Item Amount" />
                            <asp:BoundField DataField = "InvoiceQty" HeaderText="Invoice Qty" />
                        </Columns>
                    </asp:GridView>
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

</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.8;">
            <span style="border-width: 0px; position: fixed; padding: 10px; background-color: #FFFFFF; font-size: 25px; left: 43%; top: 40%; border-radius: 50px;">Please Wait...</span>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>

<script src="assets/js/pages/Invoice_Generation.js"></script>
</asp:Content>
