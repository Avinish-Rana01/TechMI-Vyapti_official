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

var _isInitializingDatatable1 = false;
var _isInitializingDatatable2 = false;

function ensureThead($table) {
    if ($table.find('> thead').length > 0) {
        return;
    }

    var $rows = $table.find('> tbody > tr, > tr');
    if ($rows.length === 0) return;

    var $headerRow = $rows.first();
    var $bodyRows = $rows.slice(1);

    var $thead = $('<thead></thead>');
    $headerRow.find('td').each(function () {
        var $th = $('<th></th>').html($(this).html());
        $.each(this.attributes, function () {
            if (this.name !== 'class') $th.attr(this.name, this.value);
        });
        $th.addClass($(this).attr('class') || '');
        $thead.append($th);
    });
    if ($headerRow.find('th').length > 0) {
        $thead.empty().append($headerRow);
    } else {
        $headerRow.remove();
    }
    $table.prepend($thead);

    var $existingTbody = $table.find('> tbody');
    if ($existingTbody.length === 0) {
        var $tbody = $('<tbody></tbody>');
        $bodyRows.each(function () { $tbody.append(this); });
        $table.append($tbody);
    } else {
        $existingTbody.find('> tr').each(function () {
            if ($(this).is($headerRow)) $(this).remove();
        });
    }
}

function initializeDatatable() {
    if (_isInitializingDatatable1) return;
    var $table = $("table[id*='grdUserMaster']");
    if ($table.length === 0) return;

    _isInitializingDatatable1 = true;
    try {
        var datetime = getFormattedDateTime();
        if ($.fn.DataTable.isDataTable($table)) {
            $table.DataTable().clear().destroy();
        }

        ensureThead($table);

        var headerColCount = $table.find('thead tr:first th, thead tr:first td').length;
        var mismatchFound = false;
        $table.find('tbody tr').each(function () {
            var $cells = $(this).find('td, th');
            var rowColCount = 0;
            $cells.each(function () {
                rowColCount += (parseInt($(this).attr('colspan'), 10) || 1);
            });
            if (rowColCount !== headerColCount) {
                mismatchFound = true;
                console.warn('DataTable column mismatch on row:', this,
                    'expected', headerColCount, 'got', rowColCount);
            }
        });
        if (mismatchFound) {
            _isInitializingDatatable1 = false;
            return;
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
                    title: 'User_Master_' + datetime
                }
            ]
        });
        if ($table.parent('.dataTables_scroll').length === 0) {
            $table.wrap('<div class="dataTables_scroll" />');
        }
    } finally {
        _isInitializingDatatable1 = false;
    }
}

function initializeDatatable2() {
    if (_isInitializingDatatable2) return;
    var $table = $("table[id*='grdViewModules']");
    if ($table.length === 0) return;

    _isInitializingDatatable2 = true;
    try {
        var datetime = getFormattedDateTime();
        if ($.fn.DataTable.isDataTable($table)) {
            $table.DataTable().clear().destroy();
        }

        ensureThead($table);

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
                    title: 'Modules_Access_' + datetime
                }
            ]
        });
        if ($table.parent('.dataTables_scroll').length === 0) {
            $table.wrap('<div class="dataTables_scroll" />');
        }
    } finally {
        _isInitializingDatatable2 = false;
    }
}

$(document).ready(function () {
    initializeDatatable();
    initializeDatatable2();
});

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    initializeDatatable();
    setTimeout(function () {
        if ($("table[id*='grdViewModules']").is(":visible")) {
            initializeDatatable2();
        }
    }, 300);
});

$(document).on('click', '.password-eye', function (e) {
    e.preventDefault();
    e.stopPropagation();
    var button = $(this);
    var passwordId = button.attr('data-password-id');
    var passwordInput = document.getElementById(passwordId);
    if (!passwordInput) {
        return;
    }
    var icon = button.find('i');
    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        icon.removeClass('fa-eye');
        icon.addClass('fa-eye-slash');
    }
    else {
        passwordInput.type = 'password';
        icon.removeClass('fa-eye-slash');
        icon.addClass('fa-eye');
    }
});