<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="HU_Creation.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.HU_Creation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title> HQ Operations | HU Creation</title>
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
    <span page-id2="HU_Creation" id="find_page2"></span>

<asp:ScriptManager ID="ScriptManager1" runat="server" >
</asp:ScriptManager>

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>--%>
    <div class="toolbar py-5 pb-lg-5" id="kt_toolbar">
        <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
            <div class="page-title d-flex flex-column me-3 flex-start">
               <h1 class="d-flex text-white m-0 fs-3">
                   <asp:Label ID="lblMenuName" runat="server" Text="HQ Operations"></asp:Label>
                   <span class="h-20px border-gray-500 border-start mx-3"></span>
                   <asp:Label ID="lblPageName" runat="server" Text="HU Creation"></asp:Label>      
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
                        <div class="col-12 col-md-6 col-lg-3 col-xl-2">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Warehouse <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="false" class="form-control form-control-solid custom-select2" required="required">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-12 col-md-6 col-lg-3 col-xl-2">
    <div class="mb-3">
        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
            Store <span class="text-danger">*</span>
        </label>

        <asp:DropDownList
            ID="ddlStore"
            runat="server"
            AutoPostBack="false"
            class="form-control form-control-solid custom-select2"
            required="required">
        </asp:DropDownList>

        <asp:RequiredFieldValidator
            ID="rfvStore"
            runat="server"
            ControlToValidate="ddlStore"
            InitialValue=""
            ErrorMessage="Please select Store"
            Display="Dynamic"
            CssClass="text-danger"
            ValidationGroup="HUValidation">
        </asp:RequiredFieldValidator>

    </div>
</div>
                        <div class="col-12 col-md-6 col-lg-3 col-xl-3">
    <div class="mb-3">
        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
            Line Item <span class="text-danger">*</span>
        </label>

        <asp:DropDownList
            ID="ddlHULineItem"
            runat="server"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlHULineItem_SelectedIndexChanged"
            class="form-control form-control-solid custom-select2"
            required="required">
        </asp:DropDownList>

        <asp:RequiredFieldValidator
            ID="rfvLineItem"
            runat="server"
            ControlToValidate="ddlHULineItem"
            InitialValue=""
            ErrorMessage="Please select Line Item"
            Display="Dynamic"
            CssClass="text-danger"
            ValidationGroup="HUValidation">
        </asp:RequiredFieldValidator>

    </div>
</div>
                        <div class="col-12 col-md-6 col-lg-4 col-xl-3">
    <div class="mb-3">
        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
            EAN<span class="text-danger">*</span>
        </label>

        <asp:DropDownList
            ID="ddlEAN"
            runat="server"
            AutoPostBack="false"
            class="form-control form-control-solid custom-select2"
            required="required">

            <asp:ListItem Text="Select EAN" Value="" />

        </asp:DropDownList>

        <asp:RequiredFieldValidator
            ID="rfvEAN"
            runat="server"
            ControlToValidate="ddlEAN"
            InitialValue=""
            ErrorMessage="Please select EAN"
            Display="Dynamic"
            CssClass="text-danger"
            ValidationGroup="HUValidation">
        </asp:RequiredFieldValidator>

    </div>
</div>

                        <div class="col-12 col-md-6 col-lg-4 col-xl-2">
    <div class="mb-3">
        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
            HU Qty<span class="text-danger">*</span>
        </label>

        <asp:TextBox
            ID="txtHUQty"
            runat="server"
            class="req_qty quantity form-control form-control-solid"
            placeholder="Enter HU Qty"
            required="required">
        </asp:TextBox>

        <asp:RequiredFieldValidator
            ID="rfvHUQty"
            runat="server"
            ControlToValidate="txtHUQty"
            ErrorMessage="Please enter HU Qty"
            Display="Dynamic"
            CssClass="text-danger"
            ValidationGroup="HUValidation">
        </asp:RequiredFieldValidator>

        <asp:RegularExpressionValidator
            ID="revHUQty"
            runat="server"
            ControlToValidate="txtHUQty"
            ValidationExpression="^[1-9][0-9]*$"
            ErrorMessage="Please enter a valid HU Qty"
            Display="Dynamic"
            CssClass="text-danger"
            ValidationGroup="HUValidation">
        </asp:RegularExpressionValidator>

    </div>
</div>
                        <div class="mt-1 col-md-12 col-lg-12 flex flex-center justify-content-center">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" class="btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Submit" UseSubmitBehavior="false" ValidationGroup="HUValidation"/>
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear" UseSubmitBehavior="false"/>
                        </div>
                    </div>
                </div>
            </div>

           <div class="card mt-3  mb-5" id="grid_div" runat="server" visible="false">
                <div class="card-body">
                    <asp:GridView ID="grdAddHU" runat="server" AutoGenerateColumns = "false" 
                            class="display nowrap table table-hover table-striped table-bordered" 
                            cellspacing="0" width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>                               
                            <asp:BoundField DataField = "STORE_NAME" HeaderText = "Store" />      
                            <asp:BoundField DataField = "HU_LINE_ITEM" HeaderText="HU Line Item" />
                            <asp:BoundField DataField = "EAN" HeaderText="EAN" />
                            <asp:BoundField DataField = "HU_LINE_ITEM_QTY" HeaderText="HU Qty" />
                            <asp:TemplateField HeaderText="Delete">  
                                 <ItemTemplate>  
                                     <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" CommandArgument='<%# Eval("HU_DETAILS_ID") %>' runat="server" class="text-theme-red"  OnClientClick="return confirm('Are you sure you want to delete this record?');"><i  class="fa fa-trash font_width"></i> Delete </asp:LinkButton> 
                                 </ItemTemplate>  
                           </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
             
                </div>
            </div>

            <center>
                 <asp:Button ID="btnGenerateHU" OnClick="btnGenerateHU_Click" runat="server" class="btnGenerate btn btn-primary er fs-6 px-8 py-3 mt-3 m-1 mb-10" Text="Generate HU" UseSubmitBehavior="false"  Visible="false"/>
            </center>
           
           <div class="card mt-3  mb-15" id="grid_final_div" runat="server" visible="false">
                <div class="card-body">
                    <asp:GridView ID="grdFinalHUGrid" runat="server" AutoGenerateColumns = "false" 
                            class="display nowrap table table-hover table-striped table-bordered" 
                            cellspacing="0" width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HU Number">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkHU_No" runat="server"  class="text-theme-green" OnClick="lnkHU_No_Click" Text='<%# Eval("HU_NUMBER") %>' CommandArgument='<%# Eval("HU_NUMBER") %>'> </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField = "WAREHOUSE_NAME" HeaderText="Warehouse Name" />
                            <asp:BoundField DataField = "STORE_NAME" HeaderText = "Store Name" />    
                           <%-- <asp:BoundField DataField = "User_Name" HeaderText="Created By" />--%>
                            <asp:BoundField DataField = "HU_Date" HeaderText="HU Date Time" />
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
                <div class="col-md-6 col-lg-3 mb-3">
                    <h6 class="text-start">HU Number : <asp:Label ID="lblHUNumber" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-3 mb-3">
                    <h6 class="text-start">Warehouse : <asp:Label ID="lblWarehouse" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-3 mb-3">
                    <h6 class="text-start">Store : <asp:Label ID="lblStore" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
                </div>
                <div class="col-md-6 col-lg-3 mb-3">
                    <h6 class="text-start">HU Date : <asp:Label ID="lblHUDate" runat="server" Font-Bold="true" Text="" CssClass="f-s-16"></asp:Label></h6>
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
                            <asp:BoundField DataField = "HU_Line_Item" HeaderText="HU Line Item" />
                            <asp:BoundField DataField = "EAN" HeaderText="EAN" />
                            <asp:BoundField DataField = "HU_LINE_ITEM_QTY" HeaderText="HU Qty" />  
                             <asp:BoundField DataField="MRP" HeaderText="MRP" />
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
  
<script src="js/HU_Details.js"></script>

</asp:Content>
