<%@ Page Title="" Language="C#" MasterPageFile="~/Retail_Suite.Master" AutoEventWireup="true" CodeBehind="View_Work_Order_Details.aspx.cs" Inherits="Markss_Retail_Suite.View_Work_Order_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Retail Suite | HO Operations | View Work Order Details</title>
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
<span page-id="Procurement_Management" id="find_page"></span>
<span page-id2="View_Work_Order_Details" id="find_page2"></span>

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
                   <asp:Label ID="lblPageName" runat="server" Text="View Work Order Details"></asp:Label>      
               </h1>
            </div>

            <div class="page-title d-flex flex-column me-3 flex-end">
                <h1 class="d-flex text-white m-0 fs-3 text-uppercase">Markss Retail Suite</h1>
            </div>
        </div>
    </div>

     <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
        <div class="content flex-row-fluid" id="kt_content">
           <div class="card mt-3  mb-15" id="Div_grd" runat="server">
                <div class="card-body">
                    <asp:GridView ID="grdWODetails" runat="server" AutoGenerateColumns = "false" 
                            class="display nowrap table table-hover table-striped table-bordered" 
                            cellspacing="0" width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Order No">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkWONo" runat="server" class="text-theme-green" OnClick="View" Text='<%#Eval("WONo").ToString()%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "VendorName" HeaderText = "Vendor Name" />
                            <asp:BoundField DataField = "WODate" HeaderText = "Work Order Date" />
                            <asp:BoundField DataField = "WOStatus" HeaderText = "Work Order Status" />
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
                <div class="col-md-6 col-lg-3 mb-3">
                    <h6 class="text-start">Work Order No : <asp:Label ID="lblWONumber" runat="server" Font-Bold="true" Text="WO2500002" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-6 mb-3">
                    <h6 class="text-start">Vendor Name : <asp:Label ID="lblVendorName" runat="server" Font-Bold="true" Text="Amazon" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-3 mb-3">
                    <h6 class="text-start">Work Order Date : <asp:Label ID="lblPODate" runat="server" Font-Bold="true" Text="2025-12-9" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-12 col-lg-12 col-xxl-12">
                    <asp:GridView ID="grdViewDetails" runat="server" AutoGenerateColumns="false"
                        class="display nowrap table table-hover table-striped table-bordered"
                        CellSpacing="0" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "LineItem" HeaderText="Line Item" />
                            <asp:BoundField DataField = "WOQty" HeaderText="Work Order Qty" />
                            <asp:BoundField DataField = "TagSupply" HeaderText="Tag Supply" />
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
<script src="assets/js/pages/View_Work_Order_Details.js"></script>
</asp:Content>
