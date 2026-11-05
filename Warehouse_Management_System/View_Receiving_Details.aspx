<%@ Page Title="" Language="C#" MasterPageFile="~/Warehouse_Management_System/Warehouse_Management.Master" AutoEventWireup="true" CodeBehind="View_Receiving_Details.aspx.cs" Inherits="Markss_Retail_Suite.Warehouse_Management_System.View_Receiving_Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Warehouse Management System | View Receiving Details</title>
    <style type="text/css">
        div.dt-buttons{position:relative;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span page-id="View_Receiving_Details" id="find_page"></span>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hfSidebarOpen" runat="server"/>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="toolbar py-5 pb-lg-5" id="kt_toolbar">
                <div id="kt_toolbar_container" class="container-xxl d-flex flex-stack flex-wrap">
                    <div class="page-title d-flex flex-column me-3 flex-start">
                        <h1 class="d-flex text-white m-0 fs-3">
                            <asp:Label ID="lblMenuName" runat="server" Text="Warehouse Management System"></asp:Label>
                            <span class="h-20px border-gray-500 border-start mx-3"></span>
                            <asp:Label ID="lblPageName" runat="server" Text="View Receiving Details"></asp:Label>
                        </h1>
                    </div>
                </div>
            </div>
            <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start container-xxl">
                <div class="content flex-row-fluid" id="kt_content">
                    <div class="card mt-3 mb-15" id="Div_grd" runat="server" visible="false">
                        <div class="card-body">
                            <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="false" CssClass="display nowrap table table-hover table-striped table-bordered" CellSpacing="0" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ASN No">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkASN_No" runat="server" CssClass="text-theme-green" Text='<%# Eval("ASN_NUMBER") %>' CommandArgument='<%# Eval("ASN_NUMBER") %>' OnClick="lnkASN_No_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ASN_DATE" HeaderText="ASN Date"/>
                                    <asp:BoundField DataField="PO_NO" HeaderText="PO No"/>
                                    <asp:BoundField DataField="PO_DATE" HeaderText="PO Date"/>
                                    <asp:BoundField DataField="WO_NO" HeaderText="Work Order No"/>
                                    <asp:BoundField DataField="WO_DATE" HeaderText="Work Order Date"/>
                                    <asp:BoundField DataField="VENDOR_NAME" HeaderText="Vendor Name"/>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display:none;width:95%;">
                        <h2 class="modal_heading">VIEW RECEIVING DETAILS</h2>
                        <asp:Button ID="btnCancel" runat="server" UseSubmitBehavior="false" Text="X" CssClass="btn modal_closeBtn text-white" OnClick="btnCancel_Click" Style="background:#f00;margin-top:3px;padding:5px 12px;"/>
                        <div class="modal-body mt-2">
                            <div class="row">
                                <div class="col-md-6 col-lg-3 mb-3">
                                    <h6 class="text-start">ASN No : <asp:Label ID="lblASNNumber" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
                                </div>
                                <div class="col-md-6 col-lg-6 mb-3">
                                    <h6 class="text-start">Vendor Name : <asp:Label ID="lblVendorName" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
                                </div>
                                <div class="col-md-6 col-lg-3 mb-3">
                                    <h6 class="text-start">ASN Date : <asp:Label ID="lblASNDate" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
                                </div>
                                <div class="col-md-12 col-lg-12 col-xxl-12">
                                    <asp:GridView ID="grdPODetails" runat="server" AutoGenerateColumns="false" CssClass="display nowrap table table-hover table-striped table-bordered" CellSpacing="0" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ASN_LINE_ITEM" HeaderText="Line Item"/>
                                            <asp:BoundField DataField="EAN" HeaderText="EAN"/>
                                            <asp:BoundField DataField="WO_LINE_ITEM_QTY" HeaderText="Work Order Qty"/>
                                            <asp:BoundField DataField="WO_LINE_ITEM_ASN_QTY" HeaderText="ASN Qty"/>
                                            <asp:BoundField DataField="Total_Receive_Qty" HeaderText="Received Qty"/>
                                        </Columns>
                                    </asp:GridView>
                                    <h6 class="text-start mt-3">Total Received Qty : <asp:Label ID="lbl_total_received_qty" runat="server" Font-Bold="true" Text="0" CssClass="f-s-16"></asp:Label></h6>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                    <cc1:ModalPopupExtender ID="popup" runat="server" DropShadow="false" BehaviorID="popupBehavior" PopupControlID="pnlAddEdit" TargetControlID="lnkFake" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position:fixed;text-align:center;height:100%;width:100%;top:0;right:0;left:0;z-index:9999999;background-color:#000;opacity:.8;">
                <span style="border-width:0;position:fixed;padding:10px;background-color:#fff;font-size:25px;left:43%;top:40%;border-radius:50px;">Please Wait...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script src="js/Grid_Details.js"></script>
</asp:Content>