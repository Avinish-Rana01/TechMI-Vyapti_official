<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor_Management_System/Vendor_Management.Master" AutoEventWireup="true" CodeBehind="Work_Order_Details.aspx.cs" Inherits="Markss_Retail_Suite.Vendor_Management_System.Work_Order_Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Vendor Management System | PO Details</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <span page-id="Work_Order_Details" id="find_page"></span>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="toolbar py-5 pb-lg-5" id="kt_toolbar">
                <div id="kt_toolbar_container" class="container-xxl d-flex flex-stack flex-wrap">
                    <div class="page-title d-flex flex-column me-3 flex-start">
                        <h1 class="d-flex text-white m-0 fs-3">
                            <asp:Label ID="lblMenuName" runat="server" Text="Vendor Management System"></asp:Label>
                            <span class="h-20px border-gray-500 border-start mx-3"></span>
                            <asp:Label ID="lblPageName" runat="server" Text="PO Details"></asp:Label>
                        </h1>
                    </div>
                </div>
            </div>

            <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start container-xxl">
                <div class="content flex-row-fluid" id="kt_content">

                    <div class="card mt-3 mb-15" id="Div_grd" runat="server">
                        <div class="card-body">

                            <asp:GridView ID="grdDetails" runat="server"
                                AutoGenerateColumns="false"
                                class="display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0"
                                Width="100%">

                                <Columns>

                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="PO No">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPONo"
                                                runat="server"
                                                CssClass="text-theme-green"
                                                Text='<%# Eval("PO_NO") %>'
                                                CommandArgument='<%# Eval("PO_NO") %>'
                                                OnClick="lnkPONo_Click">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="WODt" HeaderText="PO Date" />

                                    <asp:BoundField DataField="WO_Line_Item_Qty" HeaderText="PO Line Item Qty" />

                                </Columns>

                            </asp:GridView>

                        </div>
                    </div>

                    <asp:Panel ID="pnlPODetails"
                        runat="server"
                        CssClass="modalPopup"
                        Style="display:none;width:95%;">

                        <h2 class="modal_heading">VIEW PO DETAILS</h2>

                        <asp:Button ID="btnClose"
                            runat="server"
                            UseSubmitBehavior="false"
                            Text="X"
                            CssClass="btn modal_closeBtn text-white"
                            OnClick="btnClose_Click"
                            Style="background:#f00;margin-top:3px;padding:5px 12px;" />

                        <div class="modal-body mt-2">

                            <div class="row">

                                <div class="col-md-6 col-lg-4 mb-3">
                                    <h6 class="text-start">
                                        PO No :
                                        <asp:Label ID="lblPONumber"
                                            runat="server"
                                            Font-Bold="true"
                                            CssClass="f-s-16">
                                        </asp:Label>
                                    </h6>
                                </div>

                                <div class="col-md-6 col-lg-4 mb-3">
                                    <h6 class="text-start">
                                        Vendor Name :
                                        <asp:Label ID="lblVendorName"
                                            runat="server"
                                            Font-Bold="true"
                                            CssClass="f-s-16">
                                        </asp:Label>
                                    </h6>
                                </div>

                                <div class="col-md-6 col-lg-4 mb-3">
                                    <h6 class="text-start">
                                        PO Date :
                                        <asp:Label ID="lblPODate"
                                            runat="server"
                                            Font-Bold="true"
                                            CssClass="f-s-16">
                                        </asp:Label>
                                    </h6>
                                </div>

                                <div class="col-md-12 col-lg-12 col-xxl-12">

                                    <asp:GridView ID="grdPODetails"
                                        runat="server"
                                        AutoGenerateColumns="false"
                                        class="display nowrap table table-hover table-striped table-bordered"
                                        CellSpacing="0"
                                        Width="100%">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WO_Line_Item" HeaderText="Line Item" />
                                            <asp:BoundField DataField="EAN" HeaderText="EAN" />
                                            <asp:BoundField DataField="WO_Line_Item_Qty" HeaderText="PO Qty" />
                                        </Columns>

                                    </asp:GridView>

                                </div>

                            </div>

                        </div>

                    </asp:Panel>

                    <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>

                    <cc1:ModalPopupExtender ID="popup"
                        runat="server"
                        DropShadow="false"
                        BehaviorID="popupBehavior"
                        PopupControlID="pnlPODetails"
                        TargetControlID="lnkFake"
                        BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position:fixed;text-align:center;height:100%;width:100%;top:0;right:0;left:0;z-index:9999999;background-color:#000000;opacity:0.8;">
                <span style="border-width:0px;position:fixed;padding:10px;background-color:#FFFFFF;font-size:25px;left:43%;top:40%;border-radius:50px;">
                    Please Wait...
                </span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <script src="js/Grid_Details.js"></script>

</asp:Content>