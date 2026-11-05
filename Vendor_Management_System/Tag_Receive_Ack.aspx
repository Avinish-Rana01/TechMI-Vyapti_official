<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor_Management_System/Vendor_Management.Master" AutoEventWireup="true" CodeBehind="Tag_Receive_Ack.aspx.cs" Inherits="Markss_Retail_Suite.Vendor_Management_System.Tag_Receive_Ack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title> Vendor Management System | Tag Receive Acknowledgement</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span page-id="Tag_Receive_Acknowledge" id="find_page"></span>

<asp:ScriptManager ID="ScriptManager1" runat="server" >
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>
    <div class="toolbar py-5 pb-lg-5" id="kt_toolbar">
        <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
            <div class="page-title d-flex flex-column me-3 flex-start">
               <h1 class="d-flex text-white m-0 fs-3">
                   <asp:Label ID="lblMenuName" runat="server" Text="Vendor Management System"></asp:Label>
                   <span class="h-20px border-gray-500 border-start mx-3"></span>
                   <asp:Label ID="lblPageName" runat="server" Text="Tag Receive Acknowledgement"></asp:Label>      
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
                        <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Work Order No <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlWorkOrdNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkOrdNo_SelectedIndexChanged" class="form-control form-control-solid custom-select2" required="required">
                                   <%-- <asp:ListItem Value="" Text="Select Work Order No"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="WO2500002"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-12 col-md-6 col-lg-4 col-xl-4">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Challan No <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlChallanNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChallanNo_SelectedIndexChanged" class="form-control form-control-solid custom-select2" required="required">
                                   <%-- <asp:ListItem Value="" Text="Select Challan No"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="CH100001"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

           <div class="card mt-3  mb-5" id="Div_grd" runat="server" visible="false">
                <div class="card-body">
                    <asp:GridView ID="grdChallanDtl" runat="server" AutoGenerateColumns = "false" 
                            class="display nowrap table table-hover table-striped table-bordered" 
                            cellspacing="0" width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "CHALLAN_NO" HeaderText = "Challan No" />
                            <asp:BoundField DataField = "VENDOR_NAME" HeaderText="Vendor Name" />
                            <asp:BoundField DataField = "WO_No" HeaderText="WO No" />
                            <asp:BoundField DataField = "WO_Dt" HeaderText="WO Date" />
                            <asp:BoundField DataField = "TAG_PO_NO" HeaderText="PO NO" />
                            <asp:BoundField DataField = "PO_Dt" HeaderText="PO Dt" />
                            <asp:BoundField DataField = "TAG_PO_QTY" HeaderText="PO Tag Qty" />
                            <asp:BoundField DataField = "TAG_PO_VERIFY_QTY" HeaderText="Tag Received Qty" />    
                        </Columns>
                    </asp:GridView>

                    <center>
                         <asp:Button ID="btnTagRecivedConfrmation" runat="server" OnClick="btnTagRecivedConfrmation_Click" class="btn btn-primary mt-1 mb-3" Text="Tag Received Confirmation" UseSubmitBehavior="false" visible="false"/>
                    </center>
                </div>
            </div>

           <div class="card mt-3  mb-15" id="Div_grd2" runat="server" visible="false">
                <div class="card-body">
                    <asp:GridView ID="grdFinalInvoiceGrid" runat="server" AutoGenerateColumns = "false" 
                            class="display nowrap table table-hover table-striped table-bordered" 
                            cellspacing="0" width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "Grn_No" HeaderText = "GRN No" />
                            <asp:BoundField DataField = "PO_No" HeaderText = "PO No" />
                            <asp:BoundField DataField = "Invoice_No" HeaderText="Invoice Number" />
                            <asp:BoundField DataField = "Invoice_Amt" HeaderText="Invoice Amount" />
                            <asp:BoundField DataField = "Invoice_Dt" HeaderText="Invoice Date" />
                            <asp:BoundField DataField = "PO_Line_Item" HeaderText="PO Line Item" />
                            <asp:BoundField DataField = "PO_Line_Item_Qty" HeaderText="PO Line Item Qty" />
                            <asp:BoundField DataField = "PO_Line_Item_Amt" HeaderText="PO Line Item Amount" />
                            <asp:BoundField DataField = "Invoice_Line_Item_Qty" HeaderText="Invoice Qty" />
                            <asp:BoundField DataField = "Transporter_Name" HeaderText="Transporter Name" />
                            <asp:BoundField DataField = "Vehicle_No" HeaderText="Vehicle Number" />
                            <asp:BoundField DataField = "User_Name" HeaderText="Created By" />
                            <asp:BoundField DataField = "Entry_Dt" HeaderText="Created Date/Time" />
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
<script src="js/Tag_Receive_Ack.js"></script>
</asp:Content>
