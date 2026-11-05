<%@ Page Title="" Language="C#" MasterPageFile="~/Retail_Suite.Master" AutoEventWireup="true" CodeBehind="Warehouse_Dashboard.aspx.cs" Inherits="Markss_Retail_Suite.Warehouse_Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Retail Suite | Dashboards | Warehouse Dashboard</title>
    <style type="text/css">
        body {
            background: #d0dbe1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span page-id="Warehouse_Dashboard" id="find_page"></span>

<div class="toolbar py-5 pb-lg-15" id="kt_toolbar">
    <!--begin::Container-->
    <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
        <!--begin::Page title-->
        <div class="page-title d-flex flex-column me-3 flex-start">
            <!--begin::Title-->
                <h1 class="d-flex text-white m-0 fs-3">
                    <asp:Label ID="lblMenuName" runat="server"></asp:Label>      
                                 
            <span class="h-20px border-gray-500 border-start mx-3"></span>
                    <asp:Label ID="lblPageName" runat="server" Text="Warehouse Dashboard"></asp:Label>      
            </h1>
        </div>

            <div class="page-title d-flex flex-column me-3 flex-end">
            <!--begin::Title-->
            <h1 class="d-flex text-white m-0 fs-3">
                    Markss Retail Suite
            </h1>
        </div>
    </div>
    <!--end::Container-->
</div>
</asp:Content>
