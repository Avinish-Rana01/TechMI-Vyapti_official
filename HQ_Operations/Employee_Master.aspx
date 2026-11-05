<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/HQ_Operations/HQ_Operations.Master"
    AutoEventWireup="true"
    CodeBehind="Employee_Master.aspx.cs"
    Inherits="Markss_Retail_Suite.HQ_Operations.Employee_Master" %>


<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">

    <title>HQ Operations | Employee Master</title>


    <style type="text/css">

        body {
            background: #d0dbe1;
        }

        div.dt-buttons {
            position: relative;
        }

        .hiddencol {
            display: none !important;
        }

        .validation-error {
            color: red;
            font-size: 13px;
           /* display: block;*/
            margin-top: 5px;
        }

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

    <span page-id="Master_Management"
        id="find_page"></span>

    <span page-id2="Employee_Master"
        id="find_page2"></span>


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
            class="container-xxl d-flex stack flex-wrap">


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
                        Text="Employee Master">
                    </asp:Label>

                </h1>

            </div>


            <!-- SINGLE / MULTIPLE -->

   <div class="page-title d-flex ms-auto align-items-center"
    runat="server"
    id="rdbcheck">

     <asp:RadioButton
    ID="rdbSingleEntry"
    runat="server"
    GroupName="status"
    Text="Single Entry"
    CssClass="sidebar-toggler me-4"
    OnClick="resetValidation(); showSingleEntry();" />




    <asp:RadioButton
    ID="rdbMultipleEntry"
    runat="server"
    GroupName="status"
    Text="Multiple Entry"
    CssClass="sidebar-toggler"
    OnClick="resetValidation(); showMultipleEntry();" />

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
            <!-- SIDEBAR -->
            <!-- ================================================= -->

            <div class="v-sidebar-wrapper">

                <div class="v-sidebar">


                    <!-- SIDEBAR HEADER -->

                    <div class="sidebar-header">

                        <h4 class="mb-0">

                            <asp:Label
                                ID="lblTitle"
                                runat="server"
                                Text="EMPLOYEE DETAILS">
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


                  <div id="divSingleEntry">


                        <asp:HiddenField
                            ID="hdnID"
                            runat="server" />


                        <asp:UpdatePanel
                            ID="UpdatePanel1"
                            runat="server">

                            <ContentTemplate>


                                <div class="row">


                                    <!-- EMP CODE -->

                                    <div class="col-md-6 col-lg-6">

                                        <div class="mb-3">

                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">

                                                Employee Code

                                                <span class="text-danger">*</span>

                                            </label>


                                            <asp:TextBox
                                                ID="txtEmpCode"
                                                runat="server"
                                                CssClass="form-control form-control-solid"
                                                placeholder="Enter Employee Code">
                                            </asp:TextBox>


                                            <asp:RequiredFieldValidator
                                                ID="rfvEmpCode"
                                                runat="server"
                                                ControlToValidate="txtEmpCode"
                                                ValidationGroup="EmployeeValidation"
                                                ErrorMessage="Employee Code is required."
                                                Display="Dynamic"
                                                CssClass="validation-error">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                    </div>


                                    <!-- EMP NAME -->

                                    <div class="col-md-6 col-lg-6">

                                        <div class="mb-3">

                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">

                                                Employee Name

                                                <span class="text-danger">*</span>

                                            </label>


                                            <asp:TextBox
                                                ID="txtEmpName"
                                                runat="server"
                                                CssClass="form-control form-control-solid"
                                                placeholder="Enter Employee Name">
                                            </asp:TextBox>


                                            <asp:RequiredFieldValidator
                                                ID="rfvEmpName"
                                                runat="server"
                                                ControlToValidate="txtEmpName"
                                                ValidationGroup="EmployeeValidation"
                                                ErrorMessage="Employee Name is required."
                                                Display="Dynamic"
                                                CssClass="validation-error">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                    </div>


                                    <!-- EMAIL -->

                                    <div class="col-md-6 col-lg-6">

                                        <div class="mb-3">

                                            <label class="mb-1 form-label fs-6 fw-bold text-gray-700">

                                                Email ID

                                                <span class="text-danger">*</span>

                                            </label>


                                            <asp:TextBox
                                                ID="txtEmpEmailID"
                                                runat="server"
                                                CssClass="form-control form-control-solid"
                                                placeholder="Enter Email ID">
                                            </asp:TextBox>


                                            <asp:RequiredFieldValidator
                                                ID="rfvEmpEmailID"
                                                runat="server"
                                                ControlToValidate="txtEmpEmailID"
                                                ValidationGroup="EmployeeValidation"
                                                ErrorMessage="Email ID is required."
                                                Display="Dynamic"
                                                CssClass="validation-error">
                                            </asp:RequiredFieldValidator>


                                            <asp:RegularExpressionValidator
                                                ID="revEmpEmailID"
                                                runat="server"
                                                ControlToValidate="txtEmpEmailID"
                                                ValidationGroup="EmployeeValidation"
                                                ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                                                ErrorMessage="Please enter valid Email ID."
                                                Display="Dynamic"
                                                CssClass="validation-error">
                                            </asp:RegularExpressionValidator>

                                        </div>

                                    </div>



                                   <!-- CONTACT -->

<div class="col-md-6 col-lg-6">

    <div class="mb-3">

        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
            Contact No
            <span class="text-danger">*</span>
        </label>

        <asp:TextBox
            ID="txtEmpContactNo"
            runat="server"
            CssClass="form-control form-control-solid"
            MaxLength="10"
            placeholder="Enter Contact No"
            onkeypress="return event.charCode >= 48 && event.charCode <= 57;">
        </asp:TextBox>

        <!-- Required Validation -->
        <asp:RequiredFieldValidator
            ID="rfvEmpContactNo"
            runat="server"
            ControlToValidate="txtEmpContactNo"
            ValidationGroup="EmployeeValidation"
            ErrorMessage="Contact No is required."
            Display="Dynamic"
            CssClass="validation-error">
        </asp:RequiredFieldValidator>

        <!-- 10 Digit Mobile Number Validation -->
        <asp:RegularExpressionValidator
            ID="revEmpContactNo"
            runat="server"
            ControlToValidate="txtEmpContactNo"
            ValidationGroup="EmployeeValidation"
            ValidationExpression="^[6-9][0-9]{9}$"
            ErrorMessage="Enter a valid 10-digit mobile number."
            Display="Dynamic"
            CssClass="validation-error">
        </asp:RegularExpressionValidator>

    </div>

</div>

<div class="col-md-6 col-lg-6">

    <div class="mb-3">

        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
            Role
            <span class="text-danger">*</span>
        </label>

        <asp:DropDownList
            ID="ddlRole"
            runat="server"
            CssClass="form-control form-control-solid custom-select2">

            <asp:ListItem
                Value=""
                Text="Select Role">
            </asp:ListItem>

        </asp:DropDownList>

    </div>

</div>

                                    <!-- DEPARTMENT -->

<div class="col-md-6 col-lg-6">

    <div class="mb-3">

        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
            Department
            <span class="text-danger">*</span>
        </label>

        <asp:DropDownList
            ID="ddlDepartment"
            runat="server"
            CssClass="form-control form-control-solid custom-select2">

            <asp:ListItem
                Value=""
                Text="Select Department">
            </asp:ListItem>

        </asp:DropDownList>

    </div>

</div>

                                    <!-- DESIGNATION -->

<div class="col-md-6 col-lg-6">

    <div class="mb-3">

        <label class="mb-1 form-label fs-6 fw-bold text-gray-700">
            Designation
            <span class="text-danger">*</span>
        </label>

        <asp:DropDownList
            ID="ddlDesignation"
            runat="server"
            CssClass="form-control form-control-solid custom-select2">

            <asp:ListItem
                Value=""
                Text="Select Designation">
            </asp:ListItem>

        </asp:DropDownList>

    </div>

</div>

                                    <!-- STATUS -->

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

           <%-- <asp:ListItem
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


                                    <!-- BUTTONS -->

                                    <div class="mt-3 col-md-12 col-lg-12 flex flex-center justify-content-center">


                                        <asp:Button
                                            ID="btnSubmit"
                                            runat="server"
                                            CssClass="btnSubmit btn btn-primary er fs-6 px-8 py-3 mt-3 m-1"
                                            Text="Submit"
                                            UseSubmitBehavior="false"
                                            OnClick="btnSubmit_Click"
                                            ValidationGroup="EmployeeValidation" />


                                       <asp:Button
    ID="btnClear"
    runat="server"
    CssClass="btn btn-info er fs-6 px-8 py-3 mt-3 m-1"
    Text="Clear"
    UseSubmitBehavior="false"
    OnClick="btnClear_Click"
    OnClientClick="resetValidation();"
    CausesValidation="false" />


                                    </div>

                                </div>

                            </ContentTemplate>

                        </asp:UpdatePanel>

                    </div>

                  <div id="divMultipleEntry"
                        style="display:none;">


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

                                        <b>EMP_CODE</b>,
                                        <b>EMP_NAME</b>,
                                        <b>EMP_EMAILID</b>,
                                        <b>EMP_CONTACTNO</b>,
                                        <b>ROLE_NAME</b>,
                                        <b>DEPARTMENT_NAME</b>,
                                        <b>DESIGNATION_NAME</b>,
                                        <b>STATUS</b>

                                    </small>

                                </div>

                            </div>


                            <!-- MULTIPLE BUTTONS -->

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


            <!-- SIDEBAR FADE -->

            <div class="v-sidebar-fade"></div>


            <!-- ================================================= -->
            <!-- GRID -->
            <!-- ================================================= -->

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
                                ID="grdEmployeeMaster"
                                runat="server"
                                AutoGenerateColumns="false"
                                CssClass="table_modal display nowrap table table-hover table-striped table-bordered"
                                CellSpacing="0"
                                Width="100%"
                                Visible="true"
                                DataKeyNames="EMP_ID,EMP_ROLEID,EMP_DEPARTMENTID,EMP_DESIGNATIONID,STATUS"
                                OnRowDataBound="grdEmployeeMaster_RowDataBound">


                                <Columns>


                                   

                                    <asp:TemplateField
                                        HeaderText="Sr.No">

                                        <ItemTemplate>

                                            <%# Container.DataItemIndex + 1 %>

                                        </ItemTemplate>

                                    </asp:TemplateField>


                                  

                                    <asp:BoundField
                                        DataField="EMP_ID"
                                        HeaderText="Employee ID"
                                        ItemStyle-CssClass="hiddencol"
                                        HeaderStyle-CssClass="hiddencol"
                                        ReadOnly="true" />


                                    

                                    <asp:BoundField
                                        DataField="EMP_CODE"
                                        HeaderText="Employee Code" />


                                 

                                    <asp:BoundField
                                        DataField="EMP_NAME"
                                        HeaderText="Employee Name" />


                                   

                                    <asp:BoundField
                                        DataField="EMP_EMAILID"
                                        HeaderText="Email ID" />


                                

                                    <asp:BoundField
                                        DataField="EMP_CONTACTNO"
                                        HeaderText="Contact No" />


                                 

                                    <asp:BoundField
                                        DataField="ROLE_NAME"
                                        HeaderText="Role" />


                                 

                                    <asp:BoundField
                                        DataField="DEPARTMENT_NAME"
                                        HeaderText="Department" />


                                   

                                    <asp:BoundField
                                        DataField="DESIGNATION_NAME"
                                        HeaderText="Designation" />


                                    

                                    <asp:BoundField
                                        DataField="STATUS_TEXT"
                                        HeaderText="Status" />


                                  

                                    <asp:TemplateField
                                        HeaderText="Action">

                                        <ItemTemplate>


                                          

                                            <asp:LinkButton
                                                ID="lnkEdit"
                                                runat="server"
                                                CssClass="text-theme-green action-link"
                                                CommandArgument='<%# Eval("EMP_ID") %>'
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
                                                CommandArgument='<%# Eval("EMP_ID") %>'
                                                OnClick="lnkActive_Click"
                                                OnClientClick="return confirm('Are you sure you want to Active this employee?');"
                                                CausesValidation="false">

                                                <i class="fa fa-check text-theme-green"></i>
                                                Active

                                            </asp:LinkButton>


                                            <!-- INACTIVE -->

                                            <asp:LinkButton
                                                ID="lnkInactive"
                                                runat="server"
                                                CssClass="text-theme-red action-link"
                                                CommandArgument='<%# Eval("EMP_ID") %>'
                                                OnClick="lnkInactive_Click"
                                                OnClientClick="return confirm('Are you sure you want to In-Active this employee?');"
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
                position:fixed;
                text-align:center;
                height:100%;
                width:100%;
                top:0;
                right:0;
                left:0;
                z-index:9999999;
                background-color:#000000;
                opacity:0.8;">

                <span style="
                    border-width:0px;
                    position:fixed;
                    padding:10px;
                    background-color:#FFFFFF;
                    font-size:25px;
                    left:43%;
                    top:40%;
                    border-radius:50px;">

                    Please Wait...

                </span>

            </div>

        </ProgressTemplate>

    </asp:UpdateProgress>


    <!-- ========================================================= -->
    <!-- SIDEBAR JS -->
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
    <script src="js/Employee_Master.js"></script>


</asp:Content>