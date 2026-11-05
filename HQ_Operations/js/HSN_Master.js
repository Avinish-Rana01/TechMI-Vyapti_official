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

    var $table = $("table[id*='grdHSNMaster']");
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
                targets: 7,
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

    let hsnCode = $('[id*=txtHSNCode]');
    let hsnDescription = $('[id*=txtHSNDesc]');
    let gstRate = $('[id*=txtGST]');
    let effectiveFromDate = $('[id*=txtEffectiveDate]');
    let status = $('[id*=ddlStatus]');

    if (hsnCode.length && hsnCode.val().trim() === "") {
        hsnCode.addClass("is-invalid");
        isValid = false;
    } else {
        hsnCode.removeClass("is-invalid");
    }

    if (hsnDescription.length && hsnDescription.val().trim() === "") {
        hsnDescription.addClass("is-invalid");
        isValid = false;
    } else {
        hsnDescription.removeClass("is-invalid");
    }

    if (gstRate.length && gstRate.val().trim() === "") {
        gstRate.addClass("is-invalid");
        isValid = false;
    } else {
        gstRate.removeClass("is-invalid");
    }

    if (effectiveFromDate.length && effectiveFromDate.val().trim() === "") {
        effectiveFromDate.addClass("is-invalid");
        isValid = false;
    } else {
        effectiveFromDate.removeClass("is-invalid");
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

    $('[id*=txtHSNCode]').on('input', function () {
        $(this).removeClass("is-invalid");
    });

    $('[id*=txtHSNDesc]').on('input', function () {
        $(this).removeClass("is-invalid");
    });

    $('[id*=txtGST]').on('input', function () {
        let value = $(this).val();

       value = value.replace(/[^0-9.]/g, '');

        let parts = value.split('.');
        if (parts.length > 2) {
            value = parts[0] + '.' + parts[1];
        }

         if (parts[1] && parts[1].length > 2) {
            value = parts[0] + '.' + parts[1].substring(0, 2);
        }

        $(this).val(value);

        if (value !== "" && !isNaN(value)) {
            $(this).removeClass("is-invalid");
        }
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

// Reinitialize after partial postback
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    initializeDatatable();
    $(document).ready(function () {
        //$('body').addClass('sidebar-open');
        bindValidationEvents();
    });
});

