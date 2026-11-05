<%@ Page Title="" Language="C#" MasterPageFile="~/Retail_Suite.Master" AutoEventWireup="true" CodeBehind="Supplier_Management.aspx.cs" Inherits="Markss_Retail_Suite.Supplier_Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Retail Suite | HO Operations | Supplier Management</title>
    
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
<span page-id2="Work_Order_Creation" id="find_page2"></span>

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
                   <asp:Label ID="lblPageName" runat="server" Text="Supplier Management"></asp:Label>      
               </h1>
            </div>

            <div class="page-title d-flex flex-column me-3 flex-end">
                <h1 class="d-flex text-white m-0 fs-3 text-uppercase">Markss Retail Suite</h1>
            </div>
        </div>
    </div>
    <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
        <div class="content flex-row-fluid" id="kt_content">
           <div class="card card-flush">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6 col-lg-6">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtVendorName" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor Name" required="required" autofocus></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Vendor Code <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtVendorCode" runat="server" class="form-control form-control-solid" placeholder="Enter Vendor Code" required="required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Contact Person Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtContactPerson" runat="server" class="form-control form-control-solid" placeholder="Enter Contact Person Name" required="required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-9">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Address <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtAddress" runat="server" class="form-control form-control-solid" placeholder="Enter Address" required="required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Email ID <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtEmailID" runat="server" type="email" class="form-control form-control-solid" placeholder="Enter Email ID" required="required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">Phone Number <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtPhone" runat="server" class="quantity form-control form-control-solid" placeholder="Enter Phone Number" required="required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-3">
                            <div class="mb-3">
                                <label class="mb-1 form-label fs-6 fw-bold text-gray-700">GST Number <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtGST" runat="server" class="form-control form-control-solid" placeholder="Enter GST Number" required="required"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="mt-3 col-md-6 col-lg-3 flex flex-center justify-content-center">
                            <asp:Button ID="btnSubmit" runat="server" class="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1 w-50" Text="Submit" UseSubmitBehavior="false" OnClick="btnSubmit_Click"/>
                            <asp:Button ID="btnClear" runat="server" class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1 w-50" Text="Clear" UseSubmitBehavior="false" OnClick="btnClear_Click"/>
                        </div>
                    </div>
                </div>
            </div>

           <div class="card mt-3  mb-15" id="Div_grd" runat="server" visible="false">
            <div class="card-body">
                <asp:GridView ID="grdSupplierDetails" runat="server" AutoGenerateColumns="false" class="table_modal display nowrap table table-hover table-striped table-bordered"
                    CellSpacing="0" Width="100%">
                    <Columns>       
                        <asp:TemplateField HeaderText="Sr.No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField = "VendorName" HeaderText="Vendor Name" />
                        <asp:BoundField DataField = "VendorCode" HeaderText="Vendor Code" />
                        <asp:BoundField DataField = "ContactPersonName" HeaderText="Contact Person Name" />
                        <asp:BoundField DataField = "Address" HeaderText="Address" />
                        <asp:BoundField DataField = "EmailID" HeaderText="Email ID" />
                        <asp:BoundField DataField = "Phone" HeaderText="Phone Number" />
                        <asp:BoundField DataField = "GST" HeaderText="GST Number" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" class="text-theme-green"><i class="fa fa-pencil text-theme-green"></i> Edit</asp:LinkButton> &nbsp;|&nbsp;
                                <asp:LinkButton ID="lnkDelete" runat="server" class="text-theme-red" OnClick="lnkDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?');"><i class="fa fa-trash text-theme-red"></i> Delete</asp:LinkButton>
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

<script src="../assets/js/pages/Supplier_Management.js"></script>

</asp:Content>
