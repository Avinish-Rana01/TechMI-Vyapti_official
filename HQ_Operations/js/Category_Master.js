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
    var sapVal = $('[id*= hdf_SAP_Value]').val();

    var $table = $("table[id*='grdCategoryMaster']");
    if ($table.length === 0) return;

    var datetime = getFormattedDateTime();

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
        dom: 'Blfrtip',
        buttons: [
            {
                extend: 'excel',
                text: 'Export Data To Excel',
                title: 'Category_Master_' + datetime
            }
        ],
        columnDefs: [
            {
                targets: 5, 
                visible: sapVal == 'Manual Entry'
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}

function validateSingleEntry() {
    let isValid = true;

    let catcode = $('[id*=txtCatCode]');
    let catname = $('[id*=txtCatName]');
    let status = $('[id*=ddlStatus]');

    if (catcode.length && catcode.val().trim() === "") {
        catcode.addClass("is-invalid");
        isValid = false;
    } else {
        catcode.removeClass("is-invalid");
    }

    if (catname.length && catname.val().trim() === "") {
        catname.addClass("is-invalid");
        isValid = false;
    } else {
        catname.removeClass("is-invalid");
    }

    if (status.length && status.val() === "") {
        status.addClass("is-invalid");
        isValid = false;
    } else {
        status.removeClass("is-invalid");
    }

    return isValid;
}

function validateMultipleEntry() {
    let fileupload = $('[id*=flpUpload]');
    let isValid = true;

    if (!fileupload.length || fileupload.val() === "") {
        fileupload.addClass("is-invalid");
        isValid = false;
    } else {
        fileupload.removeClass("is-invalid");
    }

    return isValid;
}

function bindValidationEvents() {

    $('[id*=txtCatCode]').on('input', function () {
        $(this).removeClass("is-invalid");
    });

    $('[id*=txtCatName]').on('input', function () {
        $(this).removeClass("is-invalid");
    });

    $('[id*=ddlStatus]').on('change', function () {
        $(this).removeClass("is-invalid");
    });

    $('[id*=flpUpload]').on('change', function () {
        $(this).removeClass("is-invalid");
    });
}

function clearFile() {

    let oldInput = $('[id*=flpUpload]');
    if (!oldInput.length) return;

    let newInput = $('<input>', {
        type: 'file',
        id: oldInput.attr('id'),
        name: oldInput.attr('name'),
        class: oldInput.attr('class')
    });

    oldInput.replaceWith(newInput);
}



$(document).ready(function () {
    initializeDatatable();
    bindValidationEvents();
});

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    initializeDatatable();
    $(document).ready(function () {
       // $('body').addClass('sidebar-open');
        bindValidationEvents();
    });
});

