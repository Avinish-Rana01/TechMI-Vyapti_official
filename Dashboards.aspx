<%@ Page Title="" Language="C#" MasterPageFile="~/Retail_Suite.Master" AutoEventWireup="true" CodeBehind="Dashboards.aspx.cs" Inherits="Markss_Retail_Suite.Dashboards" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Retail Suite | Dashboards | Dashboards</title>
    <style type="text/css">
        body {
            background:#d0dbe1
        }
     
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<span page-id="All_Dashboard" id="find_page"></span>
<div class="toolbar py-5 pb-lg-5" id="kt_toolbar">
    <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
        <div class="page-title d-flex flex-column me-3 flex-start">
            <h1 class="d-flex text-white m-0 fs-3">
                <asp:Label ID="lblMenuName" runat="server"></asp:Label>      
                <span class="h-20px border-gray-500 border-start mx-3"></span>
                <asp:Label ID="lblPageName" runat="server" Text="Dashboards"></asp:Label>      
            </h1>
        </div>

        <div class="page-title d-flex flex-column me-3 flex-end">
            <h1 class="d-flex text-white m-0 fs-3 text-uppercase">Markss Retail Suite</h1>
        </div>
    </div>
</div>

<div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
    <div class="content flex-row-fluid" id="kt_content">
        <div class="row">
            <asp:LinkButton ID="HO_Dashboard" runat="server" class="col-md-6 col-lg-3 card-anchor" OnClick="HO_Dashboard_Click">
                <div class="dash-card">
                    <div class="title">HO Operations Dashboard</div>
                    <div class="icon">
                        <img src="assets/media/icons/ho_operations.png" title="icon" width="50"/>
                    </div>
                </div>
           </asp:LinkButton> 

            <asp:LinkButton class="col-md-6 col-lg-3 card-anchor" id="Vendor_Dashboard" runat="server" OnClick="Vendor_Dashboard_Click">
                <div class="dash-card">
                        <div class="title">Vendor Management Dashboard</div>
                        <div class="icon">
                            <img src="assets/media/icons/team.png" title="icon" width="50"/>
                        </div>
                     </div>
            </asp:LinkButton>

            <asp:LinkButton class="col-md-6 col-lg-3 card-anchor" id="Warehouse_Dashboard" runat="server" OnClick="Warehouse_Dashboard_Click">
                <div class="dash-card">
                        <div class="title">Warehouse Management Dashboard</div>
                        <div class="icon">
                            <img src="assets/media/icons/inventory-management.png" title="icon" width="50"/>
                        </div>
                     </div>
            </asp:LinkButton>
            <asp:LinkButton class="col-md-6 col-lg-3 card-anchor" id="Store_Dashboard" runat="server" OnClick="Store_Dashboard_Click">
                <div class="dash-card">
                        <div class="title">Store Operations Dashboard</div>
                        <div class="icon">
                            <img src="assets/media/icons/store_operations.png" title="icon" width="50"/>
                        </div>
                     </div>
            </asp:LinkButton>
            <asp:LinkButton class="col-md-6 col-lg-3 card-anchor" id="Reports_Dashboard" runat="server" OnClick="Reports_Dashboard_Click">
               <div class="dash-card">
                        <div class="title">Reports & Analytics Dashboard</div>
                        <div class="icon">
                            <img src="assets/media/icons/seo-report.png" title="icon" width="50"/>
                        </div>
                     </div>
            </asp:LinkButton>
        </div>
       
    </div>
</div>
</asp:Content>
