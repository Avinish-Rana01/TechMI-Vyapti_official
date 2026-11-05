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

function initializeWODatatable() {
    var $table = $("table[id*='grdWODetail']");
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
        //dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                title: 'POS_Master_' + datetime
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}

function initializeDatatable() {
    var $table = $("table[id*='grdTagPO']");
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
                title: 'POS_Master_' + datetime
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}

function initializeDatatable2() {
    var $table = $("table[id*='grdFinalTagPO']");
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
                title: 'POS_Master_' + datetime
            },
            {
                extend: 'print',
                title: 'POS_Master_' + datetime
            },
            {
                extend: 'pdf',
                title: 'POS_Master_' + datetime,
                orientation: 'landscape'
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
                title: 'POS_Master_' + datetime
            },
            {
                extend: 'print',
                title: 'POS_Master_' + datetime
            },
            {
                extend: 'pdf',
                title: 'POS_Master_' + datetime,
                orientation: 'landscape'
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}

$(document).ready(function () {
    initializeWODatatable();
    initializeDatatable();
    initializeDatatable2();
    initializeDatatable3();
});


$('.req_qty').on('input change', function () {
    const $this = $(this);
    const req_qty = parseFloat($this.val());
    const work_ord_qty = parseFloat($('.work_ord_qty').val());

    if (req_qty > work_ord_qty) {
        setTimeout(function () {
            alert('Required Quantity Cannot Be Greater Than Work Order Quantity');
            $this.val("").focus();
        }, 0);
    }
});

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    initializeWODatatable();
    initializeDatatable();
    initializeDatatable2();
    setTimeout(function () {
        if ($("table[id*='grdViewDetails']").is(":visible")) {
            initializeDatatable3();
        }
    }, 300);


    $('.req_qty').on('input change', function () {
        const $this = $(this);
        const req_qty = parseFloat($this.val());
        const work_ord_qty = parseFloat($('.work_ord_qty').val());

        if (req_qty > work_ord_qty) {
            setTimeout(function () {
                alert('Required Quantity Cannot Be Greater Than Work Order Quantity');
                $this.val("").focus();
            }, 0);
        }
    });
});
