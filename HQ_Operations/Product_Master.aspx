<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="Product_Master.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.Product_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HQ Operations | Product Master</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }
        body {
            background: #d0dbe1;
        }
        .hiddencol
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span page-id="Master_Management" id="find_page"></span>
<span page-id2="Product_Master" id="find_page2"></span>

<asp:ScriptManager ID="ScriptManager1" runat="server" >
</asp:ScriptManager>

    <asp:HiddenField ID="hfSidebarOpen" runat="server"/>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>

    <div class="toolbar py-5 pb-lg-2" id="kt_toolbar">
        <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
            <div class="page-title d-flex flex-column me-3 flex-start">
               <h1 class="d-flex text-white m-0 fs-3">
                   <asp:Label ID="lblMenuName" runat="server" Text="HQ Operations"></asp:Label>      
                   <span class="h-20px border-gray-500 border-start mx-3"></span>
                   <asp:Label ID="lblPageName" runat="server" Text="Product Master"></asp:Label>      
               </h1>
            </div>
             <asp:HiddenField ID="hdf_SAP_Value" runat="server" />
            <div class="page-title d-flex me-3" runat="server" id="rdbcheck">
                <asp:RadioButton ID="rdbSingleEntry" runat="server" GroupName="status" Text="Single Entry" CssClass="sidebar-toggler"/>
                <asp:RadioButton ID="rdbMultipleEntry" runat="server" GroupName="status" Text="Multiple Entry" CssClass="sidebar-toggler"/>
            </div>
        </div>
    </div>
    <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
        <div class="content flex-row-fluid" id="kt_content">

            <div class="v-sidebar-wrapper">
                <div class="v-sidebar">
                    <div class="sidebar-header">
                        <h4 class="mb-0">
                            <asp:Label ID="lblTitle" runat="server" Text="PRODUCT DETAILS" Visible="true"></asp:Label>
                        </h4>
                        <button type="button" class="sidebar-close-btn" id="sidebar-close-btn">
                            <i class="fa fa-times fa-2x"></i>
                        </button>
                    </div>

                    <hr class="sidebar-hr">

                    <div id="divSingleEntry">
                         <asp:HiddenField ID="hdnID" runat="server" />
                        <div class="row">
                            <div class="col-md-6 col-lg-3">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Product Code<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtProductCode" runat="server" class="form-control form-control-solid" placeholder="Enter Product Code" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-9">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Product Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtProductName" runat="server" class="form-control form-control-solid" placeholder="Enter Product Name" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Article Number<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtArticleNo" runat="server" class="form-control form-control-solid" placeholder="Enter Article Number" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">EAN<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtEAN" runat="server" class="form-control form-control-solid" placeholder="Enter EAN" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Category<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" class="form-control form-control-solid custom-select2" required="required">
                                        <%--<asp:ListItem Value="" Text="Select Category"></asp:ListItem>--%>
                                       <%-- <asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="XYZ"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Brand<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlBrand" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                        <asp:ListItem Value="" Text="Select Brand"></asp:ListItem>
                                        <%--<asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="XYZ"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">UOM<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlUOM" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                        <asp:ListItem Value="" Text="Select UOM"></asp:ListItem>
                                        <%--<asp:ListItem Value="1" Text="PCS"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="BOX"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="KG"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="LTR"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">HSN Code<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlHSNCode" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                        <asp:ListItem Value="" Text="Select HSN Code"></asp:ListItem>
                                      <%--  <asp:ListItem Value="1" Text="120300040"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="120300041"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Cost Price<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtCostPrice" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Cost Price" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">MRP<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtMRP" runat="server" class="quantity form-control form-control-solid" placeholder="Enter MRP" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Selling Price<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtSellingPrice" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Selling Price" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Status<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlStatus" runat="server" class="form-control form-control-solid custom-select2" required="required">
                                        <asp:ListItem Value="" Text="Select Status"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="mt-1 col-md-12 col-lg-12 flex flex-center justify-content-center">
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Submit" UseSubmitBehavior="false"/>
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear" UseSubmitBehavior="false"/>
                            </div>
                        </div>
                    </div>

                    <div id="divMultipleEntry" style="display:none;">
                        <div class="row">
                            <div class="col-md-6 col-lg-12">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Upload File<span class="text-danger">*</span></label>
                                    <asp:FileUpload ID="flpUpload" runat="server" class="form-control form-control-solid" required="required"/>
                                </div>
                            </div>
                        
                            <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">
                                <asp:Button ID="btnFileUpload" runat="server" OnClick="btnFileUpload_Click" class="btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Upload File" UseSubmitBehavior="false"/>
                                <asp:Button ID="btnFileClear" runat="server" OnClick="btnFileClear_Click" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear File" UseSubmitBehavior="false"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="v-sidebar-fade"></div>

           <div class="card mt-3  mb-15" id="Div_grd" runat="server" visible="true">
                <div class="card-body">
                    <asp:GridView ID="grdProductMaster" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                        CellSpacing="0" Width="100%" Visible="true" OnRowDataBound="grdProductMaster_RowDataBound">
                        <Columns>       
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "PRODUCT_ID" HeaderText = "PRODUCT ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"  ReadOnly="true"/>
                            <asp:BoundField DataField = "PRODUCT_CODE" HeaderText="Product Code" />
                            <asp:BoundField DataField = "PRODUCT_NAME" HeaderText="Product Name" />
                            <asp:BoundField DataField = "ARTICLE_NO" HeaderText="Article Number" />
                            <asp:BoundField DataField = "EAN" HeaderText="EAN" />
                            <asp:BoundField DataField = "CAT_NAME" HeaderText="Category" />
                            <asp:BoundField DataField = "BRAND_NAME" HeaderText="Brand" />
                            <asp:BoundField DataField = "UOM_NAME" HeaderText="UOM" />
                            <asp:BoundField DataField = "HSN_CODE" HeaderText="HSN Code" />
                            <asp:BoundField DataField = "COST_PRICE" HeaderText="Cost Price" />
                            <asp:BoundField DataField = "MRP" HeaderText="MRP" />
                            <asp:BoundField DataField = "SELLING_PRICE" HeaderText="Selling Price" />
                            <asp:BoundField DataField = "STATUS" HeaderText="Status" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" class="text-theme-green" CommandArgument='<%# Eval("PRODUCT_ID") %>' OnClick="lnkEdit_Click"><i class="fa fa-edit text-theme-green"></i> Edit</asp:LinkButton>&nbsp; &nbsp; | &nbsp; 
                                    <asp:LinkButton ID="lnlActive" runat="server" class="text-theme-green" CommandArgument='<%# Eval("PRODUCT_ID") %>' OnClick="lnlActive_Click" OnClientClick="return confirm('Are you sure you want to active this category?');"><i class="fa fa-check text-theme-green"></i> Active</asp:LinkButton>
                                    <asp:LinkButton ID="lnkInactive" runat="server" class="text-theme-red" CommandArgument='<%# Eval("PRODUCT_ID") %>' OnClick="lnkInactive_Click" OnClientClick="return confirm('Are you sure you want to inactive this category?');"><i class="fa fa-ban text-theme-red"></i> Inactive</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                         </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 95%;">
        <h2 class="modal_heading">EDIT DETAILS</h2>
        <asp:Button ID="btnCancel" runat="server" UseSubmitBehavior="false" Text="X" class="btn modal_closeBtn text-white" Style="background: #f00; margin-top: 3px;padding: 5px 12px;" />

        <div class="modal-body mt-2">
            <div class="row">
                <div class="col-md-6 col-lg-3">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Product Code<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModProductCode" runat="server" class="form-control form-control-solid" placeholder="Enter Product Code" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Product Name<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModProductName" runat="server" class="form-control form-control-solid" placeholder="Enter Product Name" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Article Number<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModArticleNo" runat="server" class="form-control form-control-solid" placeholder="Enter Article Number" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">EAN<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModEAN" runat="server" class="form-control form-control-solid" placeholder="Enter EAN" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Category<span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlModCat" runat="server" class="form-control form-control-solid custom-select2" required="required">
                            <asp:ListItem Value="" Text="Select Category"></asp:ListItem>
                            <asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                            <asp:ListItem Value="0" Text="XYZ"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Brand<span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlModBrand" runat="server" class="form-control form-control-solid custom-select2" required="required">
                            <asp:ListItem Value="" Text="Select Brand"></asp:ListItem>
                            <asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                            <asp:ListItem Value="0" Text="XYZ"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">UOM<span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlModUOM" runat="server" class="form-control form-control-solid custom-select2" required="required">
                            <asp:ListItem Value="" Text="Select UOM"></asp:ListItem>
                            <asp:ListItem Value="1" Text="PCS"></asp:ListItem>
                            <asp:ListItem Value="2" Text="BOX"></asp:ListItem>
                            <asp:ListItem Value="3" Text="KG"></asp:ListItem>
                            <asp:ListItem Value="4" Text="LTR"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">HSN Code<span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlModHSN" runat="server" class="form-control form-control-solid custom-select2" required="required">
                            <asp:ListItem Value="" Text="Select HSN Code"></asp:ListItem>
                            <asp:ListItem Value="1" Text="120300040"></asp:ListItem>
                            <asp:ListItem Value="0" Text="120300041"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                        
                <div class="col-md-6 col-lg-3">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Cost Price<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModCostPrice" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Cost Price" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">MRP<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModMRP" runat="server" class="quantity form-control form-control-solid" placeholder="Enter MRP" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3">
                    <div class="mb-3">
                        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Selling Price<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtModSellingPrice" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Selling Price" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">
                    <asp:Button ID="btnModUpdate" runat="server" class="btn btn-primary er fs-6 px-8 py-3 mt-3 m-1" Text="Update" UseSubmitBehavior="false"/>
                    <asp:Button ID="btnModClear" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1" Text="Clear" UseSubmitBehavior="false"/>
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
<script src="js/Product_Master.js"></script>
<script src="js/Sidebar_Panel.js"></script>
</asp:Content>
