<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master" AutoEventWireup="true" CodeBehind="Reader_Master.aspx.cs" Inherits="Markss_Retail_Suite.HQ_Operations.Reader_Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HQ Operations | Reader Master</title>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
        }

         .hiddencol
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span page-id="Master_Management" id="find_page"></span>
<span page-id2="Reader_Master" id="find_page2"></span>

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
                   <asp:Label ID="lblPageName" runat="server" Text="Reader Master"></asp:Label>      
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
                            <asp:Label ID="lblTitle" runat="server" Text="READER DETAILS" Visible="true"></asp:Label>
                        </h4>
                        <button type="button" class="sidebar-close-btn" id="sidebar-close-btn">
                            <i class="fa fa-times fa-2x"></i>
                        </button>
                    </div>

                    <hr class="sidebar-hr">

                    <div id="divSingleEntry">
                        <asp:HiddenField ID="hdnID" runat="server" />
                        <div class="row">
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <%--<label class="mb-1 form-label fs-6 fw-bold text-gray-700">MTYP<span class="text-danger">*</span></label>--%>
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Reader Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtReaderName" runat="server" class="form-control form-control-solid" placeholder="Enter Reader Name" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Reader MAC<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtReaderMAC" runat="server" class="form-control form-control-solid" placeholder="Enter Reader MAC" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="mb-3">
                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Reader Antenna<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtReaderAntenna" runat="server" class="form-control form-control-solid" placeholder="Enter Reader Antenna" required="required"></asp:TextBox>
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
                            <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">
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
                    <asp:GridView ID="grdReaderMaster" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                        CellSpacing="0" Width="100%" Visible="true" OnRowDataBound="grdReaderMaster_RowDataBound">
                        <Columns>       
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField = "READER_ID" HeaderText = "Reader ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"  ReadOnly="true"/>
                            <asp:BoundField DataField = "READER_NAME" HeaderText="Reader Name" />
                            <asp:BoundField DataField = "READER_MAC" HeaderText="Reader MAC" />
                            <asp:BoundField DataField = "READER_ANTENNA" HeaderText="Reader Antenna" />
                            <asp:BoundField DataField = "STATUS" HeaderText="Status" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" class="text-theme-green" CommandArgument='<%# Eval("READER_ID") %>' OnClick="lnkEdit_Click"><i class="fa fa-edit text-theme-green"></i> Edit</asp:LinkButton>&nbsp; &nbsp; | &nbsp; 
                                    <asp:LinkButton ID="lnlActive" runat="server" class="text-theme-green" CommandArgument='<%# Eval("READER_ID") %>' OnClick="lnlActive_Click" OnClientClick="return confirm('Are you sure you want to active this category?');"><i class="fa fa-check text-theme-green"></i> Active</asp:LinkButton>
                                    <asp:LinkButton ID="lnkInactive" runat="server" class="text-theme-red" CommandArgument='<%# Eval("READER_ID") %>' OnClick="lnkInactive_Click" OnClientClick="return confirm('Are you sure you want to inactive this category?');"><i class="fa fa-ban text-theme-red"></i> Inactive</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
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
<script src="js/Reader_Master.js"></script>
<script src="js/Sidebar_Panel.js"></script>
</asp:Content>
