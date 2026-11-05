// ============================================================
// DATE FORMAT
// ============================================================

function pad(n) {

    return n < 10 ? '0' + n : n;

}


// ============================================================
// GET FORMATTED DATE TIME
// ============================================================

function getFormattedDateTime() {

    var currentdate = new Date();

    return pad(currentdate.getDate()) + "-" +
        pad(currentdate.getMonth() + 1) + "-" +
        currentdate.getFullYear() + " At " +
        pad(currentdate.getHours()) + "h." +
        pad(currentdate.getMinutes()) + "m." +
        pad(currentdate.getSeconds()) + "s";

}


// ============================================================
// DATATABLE
// ============================================================

function initializeDatatable() {

    var $table =
        $("table[id*='grdEmployeeMaster']");


    if ($table.length === 0) {

        return;

    }


    var datetime =
        getFormattedDateTime();


    // ========================================================
    // DESTROY OLD DATATABLE
    // ========================================================

    if ($.fn.DataTable.isDataTable($table)) {

        $table.DataTable()
            .clear()
            .destroy();

    }


    // ========================================================
    // INITIALIZE
    // ========================================================

    $table.DataTable({

        language: {

            searchPlaceholder: "Search records"

        },


        paging: true,


        responsive: true,


        autoWidth: false,


        ordering: true,


        searching: true,


        pageLength: 10,


        lengthMenu: [

            [10, 25, 50, 100, -1],

            [10, 25, 50, 100, "All"]

        ],


        dom: 'Blfrtip',


        buttons: [

            {

                extend: 'excel',

                text: 'Export Data To Excel',

                title:
                    'Employee_Master_' +
                    datetime

            }

        ],


        // ====================================================
        // HIDE EMPLOYEE ID
        // ====================================================

        columnDefs: [

            {

                targets: 1,

                visible: false,

                searchable: false

            }

        ]

    });


    // ========================================================
    // HORIZONTAL SCROLL
    // ========================================================

    if ($table.parent(
        '.dataTables_scroll').length === 0) {

        $table.wrap(
            '<div class="dataTables_scroll" />'
        );

    }

}


// ============================================================
// SINGLE ENTRY VALIDATION
// ============================================================

function validateSingleEntry() {

    var isValid = true;


    var empCode =
        $('[id*=txtEmpCode]');


    var empName =
        $('[id*=txtEmpName]');


    var empEmail =
        $('[id*=txtEmpEmailID]');


    var empContact =
        $('[id*=txtEmpContactNo]');


    var role =
        $('[id*=ddlRole]');


    var department =
        $('[id*=ddlDepartment]');


    var designation =
        $('[id*=ddlDesignation]');


    var status =
        $('[id*=ddlStatus]');


    // ========================================================
    // EMPLOYEE CODE
    // ========================================================

    if (empCode.length &&
        empCode.val().trim() === "") {

        empCode.addClass(
            "is-invalid");

        isValid = false;

    }
    else {

        empCode.removeClass(
            "is-invalid");

    }


    // ========================================================
    // EMPLOYEE NAME
    // ========================================================

    if (empName.length &&
        empName.val().trim() === "") {

        empName.addClass(
            "is-invalid");

        isValid = false;

    }
    else {

        empName.removeClass(
            "is-invalid");

    }


    // ========================================================
    // EMAIL
    // ========================================================

    if (empEmail.length &&
        empEmail.val().trim() === "") {

        empEmail.addClass(
            "is-invalid");

        isValid = false;

    }
    else {

        empEmail.removeClass(
            "is-invalid");

    }


    // ========================================================
    // CONTACT
    // ========================================================

    if (empContact.length &&
        empContact.val().trim() === "") {

        empContact.addClass(
            "is-invalid");

        isValid = false;

    }
    else {

        empContact.removeClass(
            "is-invalid");

    }


    // ========================================================
    // ROLE
    // ========================================================

    if (role.length &&
        role.val() === "") {

        role.addClass(
            "is-invalid");

        isValid = false;

    }
    else {

        role.removeClass(
            "is-invalid");

    }


    // ========================================================
    // DEPARTMENT
    // ========================================================

    if (department.length &&
        department.val() === "") {

        department.addClass(
            "is-invalid");

        isValid = false;

    }
    else {

        department.removeClass(
            "is-invalid");

    }


    // ========================================================
    // DESIGNATION
    // ========================================================

    if (designation.length &&
        designation.val() === "") {

        designation.addClass(
            "is-invalid");

        isValid = false;

    }
    else {

        designation.removeClass(
            "is-invalid");

    }


    // ========================================================
    // STATUS
    // ========================================================

    if (status.length &&
        status.val() === "") {

        status.addClass(
            "is-invalid");

        isValid = false;

    }
    else {

        status.removeClass(
            "is-invalid");

    }


    return isValid;

}


// ============================================================
// MULTIPLE ENTRY VALIDATION
// ============================================================

function validateMultipleEntry() {

    var fileUpload =
        $('[id*=flpUpload]');


    var isValid = true;


    if (!fileUpload.length ||
        fileUpload.val() === "") {

        fileUpload.addClass(
            "is-invalid");

        isValid = false;

    }
    else {

        fileUpload.removeClass(
            "is-invalid");

    }


    return isValid;

}


// ============================================================
// VALIDATION EVENTS
// ============================================================

function bindValidationEvents() {

    $('[id*=txtEmpCode], ' +
        '[id*=txtEmpName], ' +
        '[id*=txtEmpEmailID], ' +
        '[id*=txtEmpContactNo]')
        .off('input.employee')
        .on(
            'input.employee',
            function () {

                $(this).removeClass(
                    "is-invalid");

            }
        );


    $('[id*=ddlRole], ' +
        '[id*=ddlDepartment], ' +
        '[id*=ddlDesignation], ' +
        '[id*=ddlStatus]')
        .off('change.employee')
        .on(
            'change.employee',
            function () {

                $(this).removeClass(
                    "is-invalid");

            }
        );


    // ========================================================
    // FILE
    // ========================================================

    $('[id*=flpUpload]')
        .off('change.employee')
        .on(
            'change.employee',
            function () {

                $(this).removeClass(
                    "is-invalid");

            }
        );

}


// ============================================================
// CLEAR FILE
// ============================================================

function clearFile() {

    var oldInput =
        $('[id*=flpUpload]');


    if (!oldInput.length) {

        return;

    }


    var newInput =
        $('<input>', {

            type: 'file',

            id: oldInput.attr('id'),

            name: oldInput.attr('name'),

            class: oldInput.attr('class')

        });


    oldInput.replaceWith(
        newInput
    );


    bindValidationEvents();

}


// ============================================================
// DOCUMENT READY
// ============================================================

$(document).ready(function () {

    initializeDatatable();

    bindValidationEvents();
    setDefaultEmployeeStatus();

});


// ============================================================
// UPDATE PANEL
// ============================================================

if (typeof Sys !== "undefined" &&
    Sys.WebForms &&
    Sys.WebForms.PageRequestManager) {

    Sys.WebForms.PageRequestManager
        .getInstance()
        .add_endRequest(function () {

            initializeDatatable();

            bindValidationEvents();

        });

}


function resetValidation() {

    if (typeof Page_Validators !== "undefined") {

        for (var i = 0; i < Page_Validators.length; i++) {

            Page_Validators[i].isvalid = true;

            var validator =
                document.getElementById(Page_Validators[i].id);

            if (validator) {
                validator.style.display = "none";
            }
        }
    }

    // Remove Bootstrap invalid classes
    $('.is-invalid').removeClass('is-invalid');

    // Remove validation summary if any
    $('.validation-error').hide();
}