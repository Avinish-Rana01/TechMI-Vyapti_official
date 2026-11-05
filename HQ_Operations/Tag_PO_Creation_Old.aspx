<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="Tag_PO_Creation_Old.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.Tag_PO_Creation_Old" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HO Operations | Tag PO Creation</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span page-id="Procurement_Management" id="find_page"></span>
    <span page-id2="Tag_PO_Creation" id="find_page2"></span>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:HiddenField ID="hfSidebarOpen" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="toolbar py-5 pb-lg-5" id="kt_toolbar">
                <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
                    <div class="page-title d-flex flex-column me-3 flex-start">
                        <h1 class="d-flex text-white m-0 fs-3">
                            <asp:Label ID="lblMenuName" runat="server" Text="HQ Operations"></asp:Label>
                            <span class="h-20px border-gray-500 border-start mx-3"></span>
                            <asp:Label ID="lblPageName" runat="server" Text="Tag PO Creation"></asp:Label>
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
                                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Work Order No <span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlWorkOrdNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkOrdNo_SelectedIndexChanged" class="form-control form-control-solid custom-select2" required="required">
                                            <%-- <asp:ListItem Value="" Text="Select Work Order No"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="WO2500002"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card mt-3  mb-5" id="Div_grd" runat="server" visible="false">
                        <div class="card-body">
                            <asp:GridView ID="grdWODetail" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="WO_No" HeaderText="Work Order No" />
                                    <asp:BoundField DataField="VENDOR_NAME" HeaderText="Vendor Name" />
                                    <asp:BoundField DataField="WO_DATE" HeaderText="Work Order Date" />
                                    <asp:BoundField DataField="WO_Line_Item" HeaderText="WO Line Item" />
                                    <asp:BoundField DataField="EAN" HeaderText="EAN" />
                                    <asp:BoundField DataField="WO_Line_Item_Qty" HeaderText="WO Line Item Qty" />
                                    <%--<asp:BoundField DataField="ReqQty" HeaderText="WO Line Item Pending Qty" />--%>
                                </Columns>
                            </asp:GridView>

                            <div class="row mb-3" id="detail_row" runat="server">
                                <div class="col-md-6 col-lg-4">
                                    <div class="mb-3">
                                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Tag Vendor <span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlVendorName" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                            <asp:ListItem Value="" Text="Select Vendor"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Amazon"></asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="invalid-feedback">Please Select Vendor!!!</div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-2">
                                    <div class="mb-3">
                                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Tag PO Date <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtPODate" runat="server" class="date-start form-control form-control-solid" placeholder="Select Tag PO Date"></asp:TextBox>
                                        <div class="invalid-feedback">Please Select the PO Date!!!</div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-2">
                                    <div class="mb-3">
                                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Work Order Quantity <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtWOQty" type="text" runat="server" class="work_ord_qty form-control form-control-solid" placeholder="Enter PO Quantity" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-2">
                                    <div class="mb-3">
                                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Remaining Quantity <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtRemQty" type="text" runat="server" class="work_ord_qty form-control form-control-solid" placeholder="Remaining Quantity" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-2">
                                    <div class="mb-3">
                                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Required Quantity <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtRequiredQty" type="text" runat="server" class="req_qty  quantity form-control form-control-solid" placeholder="Required Quantity" ClientIDMode="Static" required="required"></asp:TextBox>
                                        <div class="invalid-feedback">Please Enter Required Quantity!!!</div>
                                    </div>
                                </div>
                                <div class="mt-3 col-md-6 col-lg-3 flex flex-center justify-content-center">
                                    <asp:Button ID="btnAdd" runat="server" OnClientClick="if (!validateSingleEntry()) { return false; }" OnClick="btnCreateTagPO_Click" class="btn btn-primary select-basic-multiple-seven mt-1 mb-3" Text="Create Tag PO" UseSubmitBehavior="false" />
                                    <asp:Button ID="btnClear" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1 w-50" Text="Clear" UseSubmitBehavior="false" Visible="false" OnClick="btnClear_Click" />
                                </div>
                            </div>

                            <asp:GridView ID="grdTagPO" runat="server" AutoGenerateColumns="false"
                                class="display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField = "PO_Line_Item" HeaderText = "Line Item" />
                        <asp:BoundField DataField = "EAN" HeaderText = "EAN" />--%>
                                    <asp:BoundField DataField="TAG_PO_LINE_ITEM_QTY" HeaderText="Tag PO Qty" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" CommandArgument='<%# Eval("TAG_PO_DTL_ID") %>' runat="server" class="text-theme-red" OnClick="lnkDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?');"><i class="fa fa-trash text-theme-red"></i> Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <center>
                                <asp:Button ID="btnCreateTagPO" OnClick="btnCreateTagPO_Click" runat="server" class="btn btn-primary select-basic-multiple-seven mt-1 mb-3" Text="Create Tag PO" UseSubmitBehavior="false" visible="true" />
                            </center>
                        </div>
                    </div>

                    <div class="card mt-3  mb-15" id="Div_grd2" runat="server" visible="false">
                        <div class="card-body">
                            <asp:GridView ID="grdFinalTagPO" runat="server" AutoGenerateColumns="false"
                                class="display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="WO_No" HeaderText="Work Order Number" />
                                    <asp:TemplateField HeaderText="Tag PO Number">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPONo" runat="server"
                                                Text='<%# Eval("TAG_PO_NO") %>'
                                                CommandArgument='<%# Eval("TAG_PO_NO") %>'
                                                CssClass="text-theme-green"
                                                OnClick="lnkPONo_Click">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PO_Dt" HeaderText="PO Date" />
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
                            <h6 class="text-start">Work Order No :
                                <asp:Label ID="lblWONumber" runat="server" Font-Bold="true" Text="WO2500002" CssClass="f-s-16"></asp:Label></h6>
                        </div>
                        <div class="col-md-6 col-lg-6 mb-3">
                            <h6 class="text-start">Vendor Name :
                                <asp:Label ID="lblVendorName" runat="server" Font-Bold="true" Text="Amazon" CssClass="f-s-16"></asp:Label></h6>
                        </div>
                        <div class="col-md-6 col-lg-2 mb-3">
                            <h6 class="text-start">PO Date :
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
                                   <%-- <asp:BoundField DataField="TAG_PO_Line_Item" HeaderText="Line Item" />
                                    <asp:BoundField DataField="EAN" HeaderText="EAN" />--%>
                                    <asp:BoundField DataField="TAG_PO_LINE_ITEM_QTY" HeaderText="Tag PO Order Qty" />
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

    <script src="js/Tag_PO_Creation.js"></script>


    <script>

        function validateSingleEntry() {
            let isValid = true;

        <%--let ordno = document.getElementById('<%= ddlWorkOrdNo.ClientID %>');--%>
        let vendor = document.getElementById('<%= ddlVendorName.ClientID %>');
        let podate = document.getElementById('<%= txtPODate.ClientID %>');
        let qty = document.getElementById('<%= txtRequiredQty.ClientID %>');


            //if (ordno && ordno.value.trim() === "") {
            //    ordno.classList.add("is-invalid");
            //    isValid = false;
            //} else if (ordno) {
            //    ordno.classList.remove("is-invalid");
            //}

            if (vendor && vendor.value.trim() === "") {
                vendor.classList.add("is-invalid");
                isValid = false;
            } else if (vendor) {
                vendor.classList.remove("is-invalid");
            }

            if (podate && podate.value.trim() === "") {
                podate.classList.add("is-invalid");
                isValid = false;
            } else if (podate) {
                podate.classList.remove("is-invalid");
            }

            //if (lineitem && lineitem.value.trim() === "") {
            //    lineitem.classList.add("is-invalid");
            //    isValid = false;
            //} else if (lineitem) {
            //    lineitem.classList.remove("is-invalid");
            //}

            if (qty && qty.value.trim() === "") {
                qty.classList.add("is-invalid");
                isValid = false;
            } else if (qty) {
                qty.classList.remove("is-invalid");
            }

            return isValid;
        }

        function bindValidationEvents() {
        //let ordno = document.getElementById('<%= ddlWorkOrdNo.ClientID %>');
        let vendor = document.getElementById('<%= ddlVendorName.ClientID %>');
        let podate = document.getElementById('<%= txtPODate.ClientID %>');
        let qty = document.getElementById('<%= txtRequiredQty.ClientID %>');

            //if (ordno) vendor.oninput = () => ordno.classList.remove("is-invalid");
            if (vendor) vendor.oninput = () => vendor.classList.remove("is-invalid");
            if (podate) podate.oninput = () => podate.classList.remove("is-invalid");
            /* if (lineitem) status.oninput = () => lineitem.classList.remove("is-invalid");*/
            if (qty) status.oninput = () => qty.classList.remove("is-invalid");
        }


        document.addEventListener("DOMContentLoaded", bindValidationEvents);

        Sys.WebForms.PageRequestManager.getInstance()
            .add_endRequest(bindValidationEvents);


    </script>

</asp:Content>
