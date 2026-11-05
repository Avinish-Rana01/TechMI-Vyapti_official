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
    var $table = $("table[id*='grdInterBranchTransfer']");
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
                title: 'Inter_Branch_Transfer_Details_' + datetime
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}

function showTransportDetailDiv() {
    $('[id*=ddlVehicleReq]').on('change', function () {
        var value = $(this).val();
        if (value === "1") {
            $('#transporter_name').show();
            $('#vehicle_no').show();
            $('#contact_person_name').show();
            $('#contact_person_no').show();
        }
        else {
            $('#transporter_name').hide();
            $('#vehicle_no').hide();
            $('#contact_person_name').hide();
            $('#contact_person_no').hide();
        }
    });
}

// Document ready
$(document).ready(function () {
    initializeDatatable();
    showTransportDetailDiv();
});

// Reinitialize after partial postback
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    initializeDatatable();
    showTransportDetailDiv();
    //$(document).ready(function () {
    //    $('body').addClass('sidebar-open');
    //});
});

