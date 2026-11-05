$(document).ready(function () {
    function initializeDatatable() {
        var $table = $("table[id*='grdAddInvoice']");
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
            //dom: 'Bflrtip',
        });

        if ($table.parent('.dataTables_scroll').length === 0) {
            $table.wrap('<div class="dataTables_scroll" />');
        }
       
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
                    title: 'Invoice_Generation_' + datetime,
                    exportOptions: {
                        // columns: [0, 1]
                    }
                },
                {
                    extend: 'print',
                    title: 'Invoice_Generation_' + datetime,
                    exportOptions: {
                        // columns: [0, 1]
                    }
                },
                {
                    extend: 'pdf',
                    title: 'Invoice_Generation_' + datetime,
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
                    title: 'Invoice_Generation_' + datetime,
                    exportOptions: {
                        // columns: [0, 1]
                    }
                },
                {
                    extend: 'print',
                    title: 'Invoice_Generation_' + datetime,
                    exportOptions: {
                        // columns: [0, 1]
                    }
                },
                {
                    extend: 'pdf',
                    title: 'Invoice_Generation_' + datetime,
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


    $(document).ready(function () {
        initializeDatatable();
        initializeDatatable2();
        initializeDatatable3();
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        initializeDatatable();
        initializeDatatable2();
        setTimeout(function () {
            if ($("table[id*='grdViewDetails']").is(":visible")) {
                initializeDatatable3();
            }
        }, 300);
    });
});
