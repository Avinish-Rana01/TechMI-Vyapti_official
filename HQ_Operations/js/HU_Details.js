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

function initializeHUDatatable() {
    var $table = $("table[id*='grdAddHU']");
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
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                text: 'Export Data To Excel',
                title: 'HU_Details_' + datetime
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}

function initializeDatatable() {
    var $table = $("table[id*='grdFinalHUGrid']");
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
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                text: 'Export Data To Excel',
                title: 'HU_Details_' + datetime
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}


function initializeDatatable3() {
    var $table = $("table[id*='grdViewDetails']");
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
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                text: 'Export Data To Excel',
                title: 'HU_Details_' + datetime
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}

$(document).ready(function () {
    initializeHUDatatable();
    initializeDatatable();
    initializeDatatable3();
});

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    initializeHUDatatable();
    initializeDatatable();
    setTimeout(function () {
        if ($("table[id*='grdViewDetails']").is(":visible")) {
            initializeDatatable3();
        }
    }, 300);

});
