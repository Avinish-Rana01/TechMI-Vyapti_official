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

    var $table = $("table[id*='grdBrandMaster']");
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
                title: 'Brand_Master_' + datetime
            }
        ],
        columnDefs: [
            {
                targets: 6,
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

    let category = $('[id*=ddlCategory]');
    let brndcode = $('[id*=txtBrandCode]');
    let brndname = $('[id*=txtBrandName]');
    let status = $('[id*=ddlStatus]');

    if (category.length && category.val().trim() === "") {
        category.addClass("is-invalid");
        isValid = false;
    } else {
        category.removeClass("is-invalid");
    }

    if (brndcode.length && brndcode.val().trim() === "") {
        brndcode.addClass("is-invalid");
        isValid = false;
    } else {
        brndcode.removeClass("is-invalid");
    }

    if (brndname.length && brndname.val().trim() === "") {
        brndname.addClass("is-invalid");
        isValid = false;
    } else {
        brndname.removeClass("is-invalid");
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

    $('[id*=ddlCategory]').on('input change', function () {
        $(this).removeClass("is-invalid");
    });

    $('[id*=txtBrandCode]').on('input', function () {
        $(this).removeClass("is-invalid");
    });

    $('[id*=txtBrandName]').on('input', function () {
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

