<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="View_HU_Details.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.View_HU_Details" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HQ Operations | View HU Details</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <span page-id="Inbound_Operations" id="find_page"></span>
    <span page-id2="HU_Creation" id="find_page2"></span>--%>

    <span page-id="Warehouse_Operations" id="find_page"></span>
    <span page-id2="View_HU_Dtl" id="find_page2"></span>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>--%>
    <div class="toolbar py-5 pb-lg-5" id="kt_toolbar">
        <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
            <div class="page-title d-flex flex-column me-3 flex-start">
                <h1 class="d-flex text-white m-0 fs-3">
                    <asp:Label ID="lblMenuName" runat="server" Text="HQ Operations"></asp:Label>
                    <span class="h-20px border-gray-500 border-start mx-3"></span>
                    <asp:Label ID="lblPageName" runat="server" Text="View HU Details"></asp:Label>
                </h1>
            </div>

            <div class="page-title d-flex flex-column me-3 flex-end">
                <%--<h1 class="d-flex text-white m-0 fs-3 text-uppercase">Markss Retail Suite</h1>--%>
            </div>
        </div>
    </div>

    <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
        <div class="content flex-row-fluid" id="kt_content">
            

            <div class="card mt-3  mb-15" id="grid_final_div" runat="server" >
                <div class="card-body">
                    <asp:GridView ID="grdFinalHUGrid" runat="server" AutoGenerateColumns="false"
                        class="display nowrap table table-hover table-striped table-bordered" CellSpacing="0" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HU Number">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkHU_No" runat="server" class="text-theme-green" OnClick="lnkHU_No_Click" Text='<%# Eval("HU_NUMBER") %>' CommandArgument='<%# Eval("HU_NUMBER") %>'> </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="WAREHOUSE_NAME" HeaderText="Warehouse Name" />
                            <asp:BoundField DataField="STORE_NAME" HeaderText="Store Name" />
                            <%-- <asp:BoundField DataField = "User_Name" HeaderText="Created By" />--%>
                            <asp:BoundField DataField="HU_Date" HeaderText="HU Date" />
                            <asp:BoundField DataField="HU_STATUS" HeaderText="HU Status" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>

    <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 95%;">
        <h2 class="modal_heading">VIEW DETAILS</h2>
        <asp:Button ID="btnCancel" runat="server" UseSubmitBehavior="false" Text="X" class="btn modal_closeBtn text-white" OnClick="btnCancel_Click" Style="background: #f00; margin-top: 3px; padding: 5px 12px;" />

        <div class="modal-body mt-2">
            <div class="row">
                <div class="col-md-6 col-lg-3 mb-3">
                    <h6 class="text-start">HU Number :
                        <asp:Label ID="lblHUNumber" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-3 mb-3">
                    <h6 class="text-start">Warehouse :
                        <asp:Label ID="lblWarehouse" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-3 mb-3">
                    <h6 class="text-start">Store :
                        <asp:Label ID="lblStore" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-3 mb-3">
                    <h6 class="text-start">HU Date :
                        <asp:Label ID="lblHUDate" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
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
                            <asp:BoundField DataField="HU_Line_Item" HeaderText="HU Line Item" />
                            <asp:BoundField DataField="EAN" HeaderText="EAN" />
                            <asp:BoundField DataField="MRP" HeaderText="MRP" />
                            <asp:BoundField DataField="HU_LINE_ITEM_QTY" HeaderText="HU Qty" />
                            <asp:BoundField DataField="HU_SCAN_QTY" HeaderText="HU Scanned Qty" />
                            
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>



    <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
    <cc1:ModalPopupExtender ID="popup" runat="server" DropShadow="false" BehaviorID="mpe"
        PopupControlID="pnlAddEdit" TargetControlID="lnkFake"
        BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.8;">
                <span style="border-width: 0px; position: fixed; padding: 10px; background-color: #FFFFFF; font-size: 25px; left: 43%; top: 40%; border-radius: 50px;">Please Wait...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <script src="js/View_HU_Details.js"></script>

</asp:Content>
