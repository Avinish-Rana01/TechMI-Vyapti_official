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

    var $table = $("table[id*='grdProductMaster']");
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
                title: 'Product_Master_' + datetime
            }
        ],
        columnDefs: [
            {
                targets: 14,
                visible: sapVal == 'Manual Entry'
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}

// Document ready
$(document).ready(function () {
    initializeDatatable();
});

// Reinitialize after partial postback
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    initializeDatatable();
    //$(document).ready(function () {
    //    $('body').addClass('sidebar-open');
    //});
});

