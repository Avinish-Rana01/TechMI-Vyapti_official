function pad(n) {
    return n < 10 ? '0' + n : n;
}


function getFormattedDateTime() {

    var currentdate = new Date();

    return pad(currentdate.getDate()) + "-" +
        pad(currentdate.getMonth() + 1) + "-" +
        currentdate.getFullYear() + " At " +
        pad(currentdate.getHours()) + "h." +
        pad(currentdate.getMinutes()) + "m." +
        pad(currentdate.getSeconds()) + "s";
}

function initializeDatatable() {

    var $table =
        $("table[id*='grdDepartmentMaster']");


    if ($table.length === 0) {
        return;
    }


    var datetime =
        getFormattedDateTime();

    if ($.fn.DataTable.isDataTable($table)) {

        $table.DataTable().clear().destroy();

    }
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
                    'Department_Master_' +
                    datetime
            }

        ],
        columnDefs: [

            {
                targets: 1,

                visible: false,

                searchable: false
            }

        ]

    });
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


    var departmentName =
        $('[id*=txtDepartmentName]');


    var status =
        $('[id*=ddlStatus]');


    // ========================================================
    // DEPARTMENT NAME
    // ========================================================

    if (departmentName.length &&
        departmentName.val().trim() === "") {

        departmentName.addClass(
            "is-invalid");

        isValid = false;

    }
    else {

        departmentName.removeClass(
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


    $('[id*=txtDepartmentName]')
        .off('input.department')
        .on(
            'input.department',
            function () {

                $(this).removeClass(
                    "is-invalid");

            }
        );


    $('[id*=ddlStatus]')
        .off('change.department')
        .on(
            'change.department',
            function () {

                $(this).removeClass(
                    "is-invalid");

            }
        );


    $('[id*=flpUpload]')
        .off('change.department')
        .on(
            'change.department',
            function () {

                $(this).removeClass(
                    "is-invalid");

            }
        );

}

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

});


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