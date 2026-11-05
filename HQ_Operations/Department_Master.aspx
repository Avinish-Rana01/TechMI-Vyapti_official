

<%@ Page Title="" Language="C#" MasterPageFile="~/HQ_Operations/HQ_Operations.Master"
    AutoEventWireup="true"
    CodeBehind="Department_Master.aspx.cs"
    Inherits="Markss_Retail_Suite.HQ_Operations.Department_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>HQ Operations | Department Master</title>

    <style type="text/css">

        body {
            background: #d0dbe1;
        }

        div.dt-buttons {
            position: relative;
        }

        /* Hidden Department ID column */
        .hiddencol {
            display: none !important;
        }

        /* Validation */
        .validation-error {
            color: red;
            font-size: 13px;
            display: block;
            margin-top: 5px;
        }

        /* Action links */
        .action-link {
            text-decoration: none;
            margin-right: 8px;
        }

    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">


    <!-- ========================================================= -->
    <!-- PAGE IDENTIFICATION -->
    <!-- ========================================================= -->

    <span page-id="Master_Management" id="find_page"></span>

    <span page-id2="Department_Master" id="find_page2"></span>


    <!-- ========================================================= -->
    <!-- SCRIPT MANAGER -->
    <!-- ========================================================= -->

    <asp:ScriptManager
        ID="ScriptManager1"
        runat="server">
    </asp:ScriptManager>


    <asp:HiddenField
        ID="hfSidebarOpen"
        runat="server" />


    <!-- ========================================================= -->
    <!-- TOOLBAR -->
    <!-- ========================================================= -->

    <div class="toolbar py-5 pb-lg-2"
        id="kt_toolbar">

        <div id="kt_toolbar_container"
            class="container-xxl d-flex flex-stack flex-wrap">


            <!-- PAGE TITLE -->

            <div class="page-title d-flex flex-column me-3 flex-start">

                <h1 class="d-flex text-white m-0 fs-3">

                    <asp:Label
                        ID="lblMenuName"
                        runat="server"
                        Text="HQ Operations">
                    </asp:Label>

                    <span class="h-20px border-gray-500 border-start mx-3"></span>

                    <asp:Label
                        ID="lblPageName"
                        runat="server"
                        Text="Department Master">
                    </asp:Label>

                </h1>

            </div>


            <!-- ================================================= -->
            <!-- SINGLE / MULTIPLE ENTRY -->
            <!-- ================================================= -->

            <div class="page-title d-flex me-3"
                runat="server"
                id="rdbcheck">


                <asp:RadioButton
                    ID="rdbSingleEntry"
                    runat="server"
                    GroupName="status"
                    Text="Single Entry"
                    CssClass="sidebar-toggler"
                    onclick="showSingleEntry();" />


                <asp:RadioButton
                    ID="rdbMultipleEntry"
                    runat="server"
                    GroupName="status"
                    Text="Multiple Entry"
                    CssClass="sidebar-toggler"
                    onclick="showMultipleEntry();" />


            </div>

        </div>

    </div>


    <!-- ========================================================= -->
    <!-- MAIN CONTENT -->
    <!-- ========================================================= -->

    <div id="kt_content_container"
        class="d-flex flex-column-fluid align-items-start container-xxl">

        <div class="content flex-row-fluid"
            id="kt_content">


            <!-- ================================================= -->
            <!-- RIGHT SIDE SLIDER -->
            <!-- ================================================= -->

            <div class="v-sidebar-wrapper">

                <div class="v-sidebar">


                    <!-- ================================================= -->
                    <!-- SIDEBAR HEADER -->
                    <!-- ================================================= -->

                    <div class="sidebar-header">

                        <h4 class="mb-0">

                            <asp:Label
                                ID="lblTitle"
                                runat="server"
                                Text="DEPARTMENT DETAILS"
                                Visible="true">
                            </asp:Label>

                        </h4>


                        <button
                            type="button"
                            class="sidebar-close-btn"
                            id="sidebar-close-btn">

                            <i class="fa fa-times fa-2x"></i>

                        </button>

                    </div>


                    <hr class="sidebar-hr" />


                    <!-- ================================================= -->
                    <!-- SINGLE ENTRY -->
                    <!-- ================================================= -->

                    <div id="divSingleEntry">


                        <asp:HiddenField
                            ID="hdnID"
                            runat="server" />


                        <asp:UpdatePanel
                            ID="UpdatePanel1"
                            runat="server">

                            <ContentTemplate>


                                <div class="row">


                                    <!-- ================================================= -->
                                    <!-- DEPARTMENT NAME -->
                                    <!-- ================================================= -->

                                    <div class="col-md-6 col-lg-6">

                                        <div class="mb-3">

                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">

                                                Department Name

                                                <span class="text-danger">*</span>

                                            </label>


                                            <asp:TextBox
                                                ID="txtDepartmentName"
                                                runat="server"
                                                CssClass="form-control form-control-solid"
                                                placeholder="Enter Department Name">
                                            </asp:TextBox>


                                            <asp:RequiredFieldValidator
                                                ID="rfvDepartmentName"
                                                runat="server"
                                                ControlToValidate="txtDepartmentName"
                                                ValidationGroup="DepartmentValidation"
                                                ErrorMessage="Department Name is required."
                                                Display="Dynamic"
                                                CssClass="validation-error">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                    </div>

 <!-- ================================================= -->
<!-- STATUS -->
<!-- ================================================= -->

<div class="col-md-6 col-lg-6">

    <div class="mb-3">

        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
            Status
            <span class="text-danger">*</span>
        </label>

        <asp:DropDownList
            ID="ddlStatus"
            runat="server"
            CssClass="form-control form-control-solid custom-select2">

            <%--<asp:ListItem
                Value=""
                Text="Select Status">
            </asp:ListItem>--%>

            <asp:ListItem
                Value="1"
                Text="Active">
            </asp:ListItem>

            <asp:ListItem
                Value="0"
                Text="Inactive">
            </asp:ListItem>

        </asp:DropDownList>

    </div>

</div>


                                    <!-- ================================================= -->
                                    <!-- SINGLE ENTRY BUTTONS -->
                                    <!-- ================================================= -->

                                    <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">


                                        <asp:Button
                                            ID="btnSubmit"
                                            runat="server"
                                            CssClass="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1"
                                            Text="Submit"
                                            UseSubmitBehavior="false"
                                            OnClick="btnSubmit_Click"
                                            ValidationGroup="DepartmentValidation" />


                                        <asp:Button
                                            ID="btnClear"
                                            runat="server"
                                            CssClass="btn btn-info er fs-6 px-8 py-3 mt-3 m-1"
                                            Text="Clear"
                                            UseSubmitBehavior="false"
                                            OnClick="btnClear_Click"
                                            CausesValidation="false" />


                                    </div>


                                </div>


                            </ContentTemplate>

                        </asp:UpdatePanel>


                    </div>


                    <!-- ================================================= -->
                    <!-- MULTIPLE ENTRY -->
                    <!-- ================================================= -->

                    <div id="divMultipleEntry"
                        style="display: none;">


                        <div class="row">


                            <div class="col-md-6 col-lg-12">

                                <div class="mb-3">


                                    <label class="mb-1 form-label fs-6 fw-bold text-gray-700">

                                        Upload File

                                        <span class="text-danger">*</span>

                                    </label>


                                    <asp:FileUpload
                                        ID="flpUpload"
                                        runat="server"
                                        CssClass="form-control form-control-solid" />


                                    <div class="invalid-feedback">

                                        Upload File is required

                                    </div>


                                    <small class="text-muted">

                                        Excel columns should be:

                                        <b>DEPT_NAME</b>

                                        and

                                        <b>STATUS</b>

                                    </small>


                                </div>

                            </div>


                            <!-- ================================================= -->
                            <!-- MULTIPLE ENTRY BUTTONS -->
                            <!-- ================================================= -->

                            <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">


                                <asp:Button
                                    ID="btnFileUpload"
                                    runat="server"
                                    CssClass="btnFile btn btn-primary er fs-6 px-8 py-3 mt-3 m-1"
                                    Text="Upload File"
                                    UseSubmitBehavior="false"
                                    OnClick="btnFileUpload_Click"
                                    CausesValidation="false"
                                    OnClientClick="return validateMultipleEntry();" />


                                <button
                                    type="button"
                                    class="btn btn-info er fs-6 px-8 py-3 mt-3 m-1"
                                    onclick="clearFile();">

                                    Clear File

                                </button>


                            </div>


                        </div>


                    </div>


                </div>

            </div>


            <!-- SIDEBAR BACKGROUND -->

            <div class="v-sidebar-fade"></div>


            <!-- ========================================================= -->
            <!-- DEPARTMENT GRID -->
            <!-- ========================================================= -->

            <asp:UpdatePanel
                ID="UpdatePanel2"
                runat="server">

                <ContentTemplate>


                    <div
                        class="card mt-3 mb-15"
                        id="Div_grd"
                        runat="server"
                        visible="true">


                        <div class="card-body">


                          <asp:GridView
    ID="grdDepartmentMaster"
    runat="server"
    AutoGenerateColumns="false"
    CssClass="table_modal display nowrap table table-hover table-striped table-bordered"
    CellSpacing="0"
    Width="100%"
    Visible="true"
    DataKeyNames="DEPT_ID"
    OnRowDataBound="grdDepartmentMaster_RowDataBound">

    <Columns>

       

        <asp:TemplateField HeaderText="Sr.No">

            <ItemTemplate>

                <%# Container.DataItemIndex + 1 %>

            </ItemTemplate>

        </asp:TemplateField>


        

        <asp:BoundField
            DataField="DEPT_ID"
            HeaderText="Department ID"
            ItemStyle-CssClass="hiddencol"
            HeaderStyle-CssClass="hiddencol"
            ReadOnly="true" />


      

        <asp:BoundField
            DataField="DEPT_NAME"
            HeaderText="Department Name" />


        

        <asp:BoundField
            DataField="STATUS_TEXT"
            HeaderText="Status" />


       

        <asp:TemplateField HeaderText="Action">

            <ItemTemplate>

             

                <asp:LinkButton
                    ID="lnkEdit"
                    runat="server"
                    CssClass="text-theme-green action-link"
                    CommandArgument='<%# Eval("DEPT_ID") %>'
                    OnClick="lnkEdit_Click"
                    CausesValidation="false">

                    <i class="fa fa-edit text-theme-green"></i>
                    Edit

                </asp:LinkButton>


                &nbsp;&nbsp; | &nbsp;&nbsp;


          

                <asp:LinkButton
                    ID="lnkActive"
                    runat="server"
                    CssClass="text-theme-green action-link"
                    CommandArgument='<%# Eval("DEPT_ID") %>'
                    OnClick="lnkActive_Click"
                    OnClientClick="return confirm('Are you sure you want to Active this department?');"
                    CausesValidation="false">

                    <i class="fa fa-check text-theme-green"></i>
                    Active

                </asp:LinkButton>


          

                <asp:LinkButton
                    ID="lnkInactive"
                    runat="server"
                    CssClass="text-theme-red action-link"
                    CommandArgument='<%# Eval("DEPT_ID") %>'
                    OnClick="lnkInactive_Click"
                    OnClientClick="return confirm('Are you sure you want to In-Active this department?');"
                    CausesValidation="false">

                    <i class="fa fa-ban text-theme-red"></i>
                    Inactive

                </asp:LinkButton>

            </ItemTemplate>

        </asp:TemplateField>

    </Columns>

</asp:GridView>


                        </div>

                    </div>


                </ContentTemplate>

            </asp:UpdatePanel>


        </div>

    </div>


    <!-- ========================================================= -->
    <!-- UPDATE PROGRESS -->
    <!-- ========================================================= -->

    <asp:UpdateProgress
        ID="UpdateProgress1"
        runat="server">


        <ProgressTemplate>


            <div style="
                position: fixed;
                text-align: center;
                height: 100%;
                width: 100%;
                top: 0;
                right: 0;
                left: 0;
                z-index: 9999999;
                background-color: #000000;
                opacity: 0.8;">


                <span style="
                    border-width: 0px;
                    position: fixed;
                    padding: 10px;
                    background-color: #FFFFFF;
                    font-size: 25px;
                    left: 43%;
                    top: 40%;
                    border-radius: 50px;">

                    Please Wait...

                </span>


            </div>


        </ProgressTemplate>


    </asp:UpdateProgress>


    <!-- ========================================================= -->
    <!-- JAVASCRIPT -->
    <!-- ========================================================= -->

    <script type="text/javascript">

        function showSingleEntry() {

            var singleDiv =
                document.getElementById("divSingleEntry");

            var multipleDiv =
                document.getElementById("divMultipleEntry");


            if (singleDiv) {
                singleDiv.style.display = "block";
            }


            if (multipleDiv) {
                multipleDiv.style.display = "none";
            }

        }


        function showMultipleEntry() {

            var singleDiv =
                document.getElementById("divSingleEntry");

            var multipleDiv =
                document.getElementById("divMultipleEntry");


            if (singleDiv) {
                singleDiv.style.display = "none";
            }


            if (multipleDiv) {
                multipleDiv.style.display = "block";
            }

        }

    </script>



    <script src="js/Sidebar_Panel.js"></script>
    <script src="js/Department_Master.js"></script>


</asp:Content>