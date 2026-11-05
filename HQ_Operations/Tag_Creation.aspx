<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="Tag_Creation.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.Tag_Creation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HO Operations | Tag Creation</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span page-id="Procurement_Management" id="find_page"></span>
    <span page-id2="Tag_Creation" id="find_page2"></span>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:HiddenField ID="hfSidebarOpen" runat="server"/>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="toolbar py-5 pb-lg-5" id="kt_toolbar">
                <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
                    <div class="page-title d-flex flex-column me-3 flex-start">
                        <h1 class="d-flex text-white m-0 fs-3">
                            <asp:Label ID="lblMenuName" runat="server" Text="HQ Operations"></asp:Label>
                            <span class="h-20px border-gray-500 border-start mx-3"></span>
                            <asp:Label ID="lblPageName" runat="server" Text="Tag Creation"></asp:Label>
                        </h1>
                    </div>

                    <div class="page-title d-flex flex-column me-3 flex-end">
                        <%--<h1 class="d-flex text-white m-0 fs-3 text-uppercase">Markss Retail Suite</h1>--%>
                    </div>
                </div>
            </div>
            <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
                <div class="content flex-row-fluid" id="kt_content">
                    <div class="card card-flush">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6 col-lg-4">
                                    <div class="mb-3">
                                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Tag PO No <span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlTagPONo" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                            <asp:ListItem Value="" Text="Select Work Order No"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="PO2500002"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card mt-3  mb-5" id="Div_grd" runat="server">
                        <div class="card-body">
                            <asp:GridView ID="grdChallanDetail" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PONo" HeaderText="PO No" />
                                    <asp:BoundField DataField="PODate" HeaderText="PO Date" />
                                    <asp:BoundField DataField="TotPOTagQty" HeaderText="Total PO Tag Qty" />
                                    <asp:BoundField DataField="POTagVerifyQty" HeaderText="PO Tag Verify Qty" />
                                    <asp:BoundField DataField="POTagRemQty" HeaderText="PO Tag Remaining Qty" />
                                </Columns>
                            </asp:GridView>

                            <div class="row mb-3" id="detail_row" runat="server">
                                <div class="col-md-6 col-lg-8">
                                    <div class="mb-3">
                                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Upload File<span class="text-danger">*</span></label>
                                        <asp:FileUpload ID="flpUpload" runat="server" class="form-control form-control-solid" required="required" />
                                    </div>
                                </div>

                                <div class="mt-5 col-md-6 col-lg-4">
                                    <asp:Button ID="btnFileUpload" runat="server" class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Upload File" UseSubmitBehavior="false" />
                                    <asp:Button ID="btnFileClear" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear File" UseSubmitBehavior="false" />
                                </div>
                            </div>

                            <asp:GridView ID="grdFileData" runat="server" AutoGenerateColumns="false"
                                class="display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PONo" HeaderText="PO No" />
                                    <asp:BoundField DataField="EPC_Count" HeaderText="EPC Count" />
                                </Columns>
                            </asp:GridView>

                            <center>
                                <asp:Button ID="btnCreateChallan" runat="server" class="btn btn-primary select-basic-multiple-seven mt-1 mb-3" Text="Create Challan" UseSubmitBehavior="false" visible="false" />
                            </center>
                        </div>
                    </div>

                    <div class="card mt-3  mb-15" id="Div_grd2" runat="server">
                        <div class="card-body">
                            <asp:GridView ID="grdFinalChallan" runat="server" AutoGenerateColumns="false"
                                class="display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Challan Number">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkChallanNo" runat="server" class="text-theme-green" OnClick="View"> CH2500001</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date/Time" />
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
                        <div class="col-md-6 col-lg-6 mb-3">
                            <h6 class="text-start">Challan No :
                                <asp:Label ID="lblChallanONumber" runat="server" Font-Bold="true" Text="CH2500001" CssClass="f-s-16"></asp:Label></h6>
                        </div>
                        
                        <div class="col-md-6 col-lg-6 mb-3">
                            <h6 class="text-end">Challan Date :
                                <asp:Label ID="lblPODate" runat="server" Font-Bold="true" Text="2025-12-9" CssClass="f-s-16"></asp:Label></h6>
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
                                    <asp:BoundField DataField="PONo" HeaderText="PO No" />
                                    <asp:BoundField DataField="PODate" HeaderText="PO Date" />
                                    <asp:BoundField DataField="TotPOTagQty" HeaderText="Total PO Tag Qty" />
                                    <asp:BoundField DataField="POTagVerifyQty" HeaderText="PO Tag Verify Qty" />
                                    <asp:BoundField DataField="POTagRemQty" HeaderText="PO Tag Remaining Qty" />
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

    <script src="js/Tag_Creation.js"></script>
</asp:Content>
