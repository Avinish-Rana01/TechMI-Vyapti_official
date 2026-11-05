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

    var $table = $("table[id*='grdVendorMaster']");
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
                title: 'Vendor_Master_' + datetime
            }
        ],
        columnDefs: [
            {
                targets: 17,
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
    initSelect2();
    bindState();
});

function initSelect2() {
    var ddlState = $('[id*=ddlState]');
    var ddlCity = $('[id*=ddlCity]');

    if (ddlState.data('select2')) { ddlState.select2('destroy'); }
    if (ddlCity.data('select2')) { ddlCity.select2('destroy'); }

    ddlState.select2({ placeholder: "Select State", allowClear: true, width: '100%' });
    ddlCity.select2({ placeholder: "Select City", allowClear: true, width: '100%' });

    ddlCity.off('select2:select select2:unselect').on('select2:select select2:unselect', function (e) {
        var val = $(this).val() || '';
        var hdnCity = $('[id*=hdnCity]');
        hdnCity.val(val === '0' ? '' : val);
    });
}

function bindState() {
    var ddl = $('[id*=ddlState]');
    var hdnState = $('[id*=hdnState]');
    var hdnCity = $('[id*=hdnCity]');

    ddl.off('change.state').on('change.state', function (e) {
        var selectedVal = $(this).val() || '';
        hdnState.val(selectedVal === '0' ? '' : selectedVal);

        if (e.originalEvent) {
            hdnCity.val('');
            $('[id*=ddlCity]').val(null).trigger('change.select2');
        }

        bindCity();
    });

    ddl.empty().append('<option></option>');

    $.ajax({
        type: "POST",
        url: "Vendor_Master.aspx/BindState",
        data: JSON.stringify({}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {
            if (res.d && res.d.length > 0) {
                ddl.prop('disabled', false);
                $.each(res.d, function (i, item) {
                    ddl.append(new Option(item.text, item.id));
                });

                var savedState = hdnState.val();
                if (savedState) {
                    ddl.val(savedState).trigger('change');
                } else if (res.d.length === 1) {
                    ddl.val(res.d[0].id).trigger('change');
                } else {
                    ddl.val(null).trigger('change.select2');
                }
            } else {
                ddl.append(new Option('No records available', '0', true, true)).trigger('change.select2');
            }
        }
    });
}

function bindCity() {
    var state = $('[id*=ddlState]').val() || '';
    var ddl = $('[id*=ddlCity]');
    var hdnCity = $('[id*=hdnCity]');

    if (!state || state === '0') {
        ddl.empty().append('<option></option>').val(null).trigger('change.select2');
        hdnCity.val('');
        return;
    }

    $.ajax({
        type: "POST",
        url: "Vendor_Master.aspx/BindCity",
        data: JSON.stringify({ state: state }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {
            ddl.empty().append('<option></option>');

            if (res.d && res.d.length > 0) {
                ddl.prop('disabled', false);
                $.each(res.d, function (i, item) {
                    ddl.append(new Option(item.text, item.id));
                });

                var savedCityId = hdnCity.val();

                if (savedCityId && ddl.find("option[value='" + savedCityId + "']").length > 0) {
                    ddl.val(savedCityId).trigger('change.select2');
                } else if (res.d.length === 1) {
                    var actualId = res.d[0].id;
                    hdnCity.val(actualId);
                    ddl.val(actualId).trigger('change.select2');
                    $('[id*=ddlCity]').removeClass('is-invalid');
                } else {
                    ddl.val(null).trigger('change.select2');
                    hdnCity.val(''); 
                  }
            } else {
                ddl.append(new Option('No records available', '0', true, true)).trigger('change.select2');
                ddl.prop('disabled', true);
                hdnCity.val('');
            }
        }
    });
}

function resetValue() {
    $("[id*=hdnState]").val("");
    $("[id*=hdnCity]").val("");
    $('[id*=ddlState], [id*=ddlCity]').val(null).trigger('change.select2');
}


Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    initializeDatatable();
    initSelect2();
    bindState();
});
