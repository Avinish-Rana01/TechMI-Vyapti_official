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
    var $table = $("table[id*='grdDetails']");
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
                title: 'PO_Details_' + datetime
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}

function initializeDatatable2() {
    var $table = $("table[id*='grdPODetails']");
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
                title: 'PO_Line_Item_Details_' + datetime
            },
            {
                extend: 'print',
                title: 'PO_Line_Item_Details_' + datetime
            },
            {
                extend: 'pdf',
                title: 'PO_Line_Item_Details_' + datetime,
                orientation: 'landscape'
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}

$(document).ready(function () {
    initializeDatatable();
    initializeDatatable2();
});

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    initializeDatatable();

    setTimeout(function () {
        if ($("table[id*='grdPODetails']").is(":visible")) {
            initializeDatatable2();
        }
    }, 300);
});