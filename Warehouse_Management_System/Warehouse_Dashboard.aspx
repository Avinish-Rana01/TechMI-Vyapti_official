<%@ Page Title="" Language="C#" MasterPageFile="~/Warehouse_Management_System/Warehouse_Management.Master" AutoEventWireup="true" CodeBehind="Warehouse_Dashboard.aspx.cs" Inherits="Markss_Retail_Suite.Warehouse_Management_System.Warehouse_Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Warehouse Management System | Vendor Dashboard</title>
    <style type="text/css">
        body {
            background: #d0dbe1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span page-id="Warehouse_Dashboard" id="find_page"></span>

<div class="toolbar py-5 pb-lg-15" id="kt_toolbar">
    <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
        <div class="page-title d-flex flex-column me-3 flex-start">
            <h1 class="d-flex text-white m-0 fs-3">
                <asp:Label ID="lblMenuName" runat="server"></asp:Label>      
                <span class="h-20px border-gray-500 border-start mx-3"></span>
                <asp:Label ID="lblPageName" runat="server" Text="Warehouse Dashboard"></asp:Label>      
            </h1>
        </div>
        <div class="page-title d-flex flex-column me-3 flex-end">
            <%--<h1 class="d-flex text-white m-0 fs-3">
               Markss Retail Suite
            </h1>--%>
        </div>
    </div>
</div>
<div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
    <div class="content flex-row-fluid" id="kt_content">
        <div class="card card-flush">
            <div class="card-body">
                <h3 class="text-center">WAREHOUSE MANAGEMENT DASHBOARD</h3>
            </div>
        </div>
    </div>
</div>
</asp:Content>
