$(document).ready(function () {
    function initializeDatatable() {
        var $table = $("table[id*='grdInvoice']");
        if ($table.length === 0) return;

        var currentdate = new Date();
        var datetime = currentdate.getDate() + "-" +
            (currentdate.getMonth() + 1) + "-" +
            currentdate.getFullYear() + " At " +
            currentdate.getHours() + "h." +
            currentdate.getMinutes() + "m." +
            currentdate.getSeconds() + "s";

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
            bDestroy: true,
            dom: 'Bflrtip',

            buttons: [
                {
                    extend: 'excel',
                    title: 'Invoice_Receiving_' + datetime,
                    exportOptions: {
                        // columns: [0, 1]
                    }
                },
                {
                    extend: 'print',
                    title: 'Invoice_Receiving_' + datetime,
                    exportOptions: {
                        // columns: [0, 1]
                    }
                },
                {
                    extend: 'pdf',
                    title: 'Invoice_Receiving_' + datetime,
                    orientation: 'landscape',
                    exportOptions: {
                        // columns: [0, 1]
                    }
                }
            ]
        });

        if ($table.parent('.dataTables_scroll').length === 0) {
            $table.wrap('<div class="dataTables_scroll" />');
        }
        $(".custom-select2").select2({
            search: true
        });
    }

    function initializeDatatable2() {
        var $table = $("table[id*='grdFinalInvoiceGrid']");
        if ($table.length === 0) return;

        var currentdate = new Date();
        var datetime = currentdate.getDate() + "-" +
            (currentdate.getMonth() + 1) + "-" +
            currentdate.getFullYear() + " At " +
            currentdate.getHours() + "h." +
            currentdate.getMinutes() + "m." +
            currentdate.getSeconds() + "s";

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
            bDestroy: true,
            dom: 'Bflrtip',

            buttons: [
                {
                    extend: 'excel',
                    title: 'Invoice_Receiving_' + datetime,
                    exportOptions: {
                        // columns: [0, 1]
                    }
                },
                {
                    extend: 'print',
                    title: 'Invoice_Receiving_' + datetime,
                    exportOptions: {
                        // columns: [0, 1]
                    }
                },
                {
                    extend: 'pdf',
                    title: 'Invoice_Receiving_' + datetime,
                    orientation: 'landscape',
                    exportOptions: {
                        // columns: [0, 1]
                    }
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

        var currentdate = new Date();
        var datetime = currentdate.getDate() + "-" +
            (currentdate.getMonth() + 1) + "-" +
            currentdate.getFullYear() + " At " +
            currentdate.getHours() + "h." +
            currentdate.getMinutes() + "m." +
            currentdate.getSeconds() + "s";

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
            bDestroy: true,
            dom: 'Bflrtip',

            buttons: [
                {
                    extend: 'excel',
                    title: 'Invoice_Master_' + datetime,
                    exportOptions: {
                        // columns: [0, 1]
                    }
                },
                {
                    extend: 'print',
                    title: 'Invoice_Master_' + datetime,
                    exportOptions: {
                        // columns: [0, 1]
                    }
                },
                {
                    extend: 'pdf',
                    title: 'Invoice_Master_' + datetime,
                    orientation: 'landscape',
                    exportOptions: {
                        // columns: [0, 1]
                    }
                }
            ]
        });

        if ($table.parent('.dataTables_scroll').length === 0) {
            $table.wrap('<div class="dataTables_scroll" />');
        }
    }


    initializeDatatable();
    initializeDatatable2();

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        initializeDatatable();
        initializeDatatable2();
    });
});
