<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor_Management_System/Vendor_Management.Master" AutoEventWireup="true" CodeBehind="ASN_Details.aspx.cs" Inherits="Markss_Retail_Suite.Vendor_Management_System.ASN_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title> Vendor Management System | ASN Details</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span page-id="ASN_Details" id="find_page"></span>

<asp:ScriptManager ID="ScriptManager1" runat="server" >
</asp:ScriptManager>

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>--%>
    <div class="toolbar py-5 pb-lg-5" id="kt_toolbar">
        <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
            <div class="page-title d-flex flex-column me-3 flex-start">
               <h1 class="d-flex text-white m-0 fs-3">
                   <asp:Label ID="lblMenuName" runat="server" Text="Vendor Management System"></asp:Label>
                   <span class="h-20px border-gray-500 border-start mx-3"></span>
                   <asp:Label ID="lblPageName" runat="server" Text="ASN Details"></asp:Label>      
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
                        <div class="col-12 col-md-6 col-lg-3 col-xl-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">PO No<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPONo" runat="server" class="form-control form-control-solid custom-select2" AutoPostBack="true" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged" required="required">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-12 col-md-6 col-lg-3 col-xl-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Line Item<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlWOLineItem" runat="server" class="form-control form-control-solid custom-select2" AutoPostBack="true" OnSelectedIndexChanged="ddlWOLineItem_SelectedIndexChanged" required="required">
                                    <asp:ListItem Text="Select Line Item" Value="" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-12 col-md-6 col-lg-3 col-xl-3">
                            <div class="mb-3">
        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">EAN<span class="text-danger">*</span></label>
        <asp:DropDownList ID="ddlEAN" runat="server" OnSelectedIndexChanged="ddlEAN_SelectedIndexChanged" AutoPostBack="true" class="form-control form-control-solid custom-select2" required="required"> 
            <asp:ListItem Text="Select EAN" Value="" />
        </asp:DropDownList>
    </div>
</div>

                        <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">PO Line Item Qty<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtWOLineQty" runat="server" class="quantity form-control form-control-solid" placeholder="Enter PO Line Item Qty" required="required" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Remaining Qty<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtRemainingQty" runat="server" class="work_ord_qty form-control form-control-solid" placeholder="Enter Remaining Qty" required="required" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-12 col-md-6 col-lg-4 col-xl-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">ASN Qty<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtASNQty" runat="server" class="req_qty quantity form-control form-control-solid" placeholder="Enter ASN Qty" required="required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="mt-1 col-md-6 col-lg-3 flex flex-center justify-content-center">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" class="btn btn-primary er fs-6 px-8 py-3 mt-3 m-1 w-50" Text="Submit" UseSubmitBehavior="false"/>
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1 w-50" Text="Clear" UseSubmitBehavior="false"/>
                        </div>
                    </div>
                </div>
            </div>

           <div class="card mt-3  mb-5" id="Div_grd" runat="server" visible="false">
                <div class="card-body">
                    <asp:GridView ID="grdAddASN" runat="server" AutoGenerateColumns = "false" 
                            class="display nowrap table table-hover table-striped table-bordered" 
                            cellspacing="0" width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField = "PO_No" HeaderText = "PO Number" />--%>                            
                            <asp:BoundField DataField = "ASN_Line_Item" HeaderText="ASN Line Item" />
                             <asp:BoundField DataField = "EAN" HeaderText="EAN" />
                            <asp:BoundField DataField = "ASN_Line_Item_Qty" HeaderText="PO Qty" />
                            <asp:BoundField DataField = "ASN_Qty" HeaderText="ASN Qty" />
                            <asp:TemplateField HeaderText="Delete">  
                                 <ItemTemplate>  
                                     <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" CommandArgument='<%# Eval("ASN_DETAIL_ID") %>' class="text-theme-red"  OnClientClick="return confirm('Are you sure you want to delete this record?');"><i  class="fa fa-trash font_width"></i> Delete </asp:LinkButton> 
                                 </ItemTemplate>  
                           </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <div class="row mb-3" id="div_upload_id" runat="server" visible="false">
                         <div class="col-12 col-md-6 col-lg-2 col-xl-2">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Receiving Type <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlReceivingType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReceivingType_SelectedIndexChanged" class="form-control form-control-solid custom-select2">
                                    <asp:ListItem Value="" Text="Select Receiving Type"></asp:ListItem>  
                                    <asp:ListItem Value="0" Text="Garments Inward"></asp:ListItem>  <%--WITH ENCODE--%>
                                    <asp:ListItem Value="1" Text="Tag Inward"></asp:ListItem>       <%--WITHOUT ENCODE--%>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-5" runat="server" visible="false" id="div_upload">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Upload File<span class="text-danger">*</span></label>
                                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control form-control-solid" required="required"/>
                                <div class="invalid-feedback">Please Select file to upload.</div>
                            </div>
                        </div>
                        <div class="mt-5 col-md-12 col-lg-5">
                            <asp:Button ID="btnGenerateASN" OnClick="btnGenerateASN_Click" runat="server" class="btnGenerate btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Generate ASN" UseSubmitBehavior="false"  />
                            <asp:Button ID="btnFileClear" Visible="false" OnClick="btnFileClear_Click" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear File" UseSubmitBehavior="false" />
                            <a href="/Vendor_Management_System/Download_Template/ASN_Tag_File.csv" visible="false" download="ASN_Tag_Template.csv" class="btn btn-success er fs-6 px-8 py-3 mt-3 m-1" runat="server" id="download_template_id">Download Template</a>                           
                        </div>
                    </div>                    
                </div>
            </div>

           <div class="card mt-3  mb-15" id="Div_grd2" runat="server" visible="false">
                <div class="card-body">
                    <asp:GridView ID="grdFinalASNGrid" runat="server" AutoGenerateColumns = "false" 
                            class="display nowrap table table-hover table-striped table-bordered" 
                            cellspacing="0" width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ASN Number">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkASN_No" runat="server"  class="text-theme-green" OnClick="lnkASN_No_Click" Text='<%# Eval("ASN_Number") %>' CommandArgument='<%# Eval("ASN_Number") %>'> </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "PO_NO" HeaderText = "PO Number" />    
                            <asp:BoundField DataField = "User_Name" HeaderText="Created By" />
                            <asp:BoundField DataField = "ASN_Dt" HeaderText="ASN Date Time" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 95%;">
        <h2 class="modal_heading">VIEW DETAILS</h2>
        <asp:Button ID="btnCancel" runat="server" UseSubmitBehavior="false" Text="X" class="btn modal_closeBtn text-white" OnClick="btnCancel_Click"  Style="background: #f00; margin-top: 3px;padding: 5px 12px;" />

        <div class="modal-body mt-2">
            <div class="row">
                <div class="col-md-6 col-lg-4 mb-3">
                    <h6 class="text-start">ASN Number : <asp:Label ID="lblASNNumber" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-4 mb-3">
                    <h6 class="text-start">PO Number : <asp:Label ID="lblWONo" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-4 mb-3">
                    <h6 class="text-start">ASN Date : <asp:Label ID="lblASNDate" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
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
                            <asp:BoundField DataField = "ASN_Line_Item" HeaderText="ASN Line Item" />
                            <asp:BoundField DataField = "EAN" HeaderText="EAN" />     
                            <asp:BoundField DataField = "ASN_Line_Item_Qty" HeaderText="ASN Line Item Qty" />
                            <asp:BoundField DataField = "ASN_Qty" HeaderText="ASN Qty" />                           
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
  <%--<script type="text/javascript">
        function callValidateQty() {
            $('.asn_qty').on('change input', function () {
                validateASNQty();
            });

            function validateASNQty() {
                var remaining_qty = parseInt($('.remaining_qty').val());
                var asn_qty = parseInt($('.asn_qty').val());

                if (asn_qty > remaining_qty) {
                    alert("The value you entered is " + asn_qty + ". ASN Qty cannot be greater than WO Line Item Qty & Remaining Qty combined");
                    $('.asn_qty').val("");
                }
            }
        }
    </script>--%>
<script src="js/ASN_Details.js"></script>

</asp:Content>
