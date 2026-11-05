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

// function initializeDatatable() {
//     var sapVal = $('[id*= hdf_SAP_Value]').val();

//     var $table = $("table[id*='grdMaterialMaster']");
//     if ($table.length === 0) return;

//     var datetime = getFormattedDateTime();

//     if ($.fn.DataTable.isDataTable($table)) {
//         $table.DataTable().clear().destroy();
//     }

//     $table.DataTable({
//         language: {
//             searchPlaceholder: "Search records"
//         },
//         paging: true,
//         responsive: true,
//         autoWidth: false,
//         ordering: true,
//         searching: true,
//         dom: 'Blfrtip',
//         buttons: [
//             {
//                 extend: 'excel',
//                 text: 'Export Data To Excel',
//                 title: 'Material_Master_' + datetime
//             }
//         ],
//         columnDefs: [
//             {
//                 targets: 11,
//                 visible: sapVal == 'Manual Entry'
//             }
//         ]
//     });

//     if ($table.parent('.dataTables_scroll').length === 0) {
//         $table.wrap('<div class="dataTables_scroll" />');
//     }
// }


function initializeDatatable() {

    var sapVal = $('[id*= hdf_SAP_Value]').val();

    var $table = $("table[id*='grdMaterialMaster']");

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
                title: 'Material_Master_' + datetime
            }
        ]
    });

    if ($table.parent('.dataTables_scroll').length === 0) {
        $table.wrap('<div class="dataTables_scroll" />');
    }
}

function validateSingleEntry() {
    let isValid = true;

    let Category = $('[id*=ddlCategory]');
    // MaterialCode, Gender, Fabric, MRP, efdate are commented out in the form
    let articleNo = $('[id*=txtArticle]');
    let articleDesc = $('[id*=txtarticleDesc]');
    let status = $('[id*=ddlStatus]');

    const fields = [Category, articleNo, articleDesc, status];

    fields.forEach(f => {
        if (f.length && f.val().trim() === "") {
            f.addClass("is-invalid");
            isValid = false;
        } else if (f.length) {
            f.removeClass("is-invalid");
        }
    });

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
    let Category = $('[id*=ddlCategory]');
    let MaterialCode = $('[id*=txtMaterialCode]');
    let articleNo = $('[id*=txtArticle]');
    let articleDesc = $('[id*=txtarticleDesc]');
    let Gender = $('[id*=ddlGender]');
    // let Size = $('[id*=ddlSize]');
    let Fabric = $('[id*=txtFabric]');
    let MRP = $('[id*=txtMRP]');
    let efdate = $('[id*=txtEffectiveFromDate]');
    let status = $('[id*=ddlStatus]');
    let fileupload = $('[id*=flpUpload]');


    if (Category.length) Category.on('input', () => Category.removeClass("is-invalid"));
    if (MaterialCode.length) MaterialCode.on('input', () => MaterialCode.removeClass("is-invalid"));
    if (articleNo.length) articleNo.on('input', () => articleNo.removeClass("is-invalid"));
    if (articleDesc.length) articleDesc.on('input', () => articleDesc.removeClass("is-invalid"));
    if (Gender.length) Gender.on('input', () => Gender.removeClass("is-invalid"));
    // if (Size.length) Size.on('input', () => Size.removeClass("is-invalid"));
    if (Fabric.length) Fabric.on('input', () => Fabric.removeClass("is-invalid"));
    //if (MRP.length) MRP.on('input', () => MRP.removeClass("is-invalid"));
    if (efdate.length) efdate.on('input', () => efdate.removeClass("is-invalid"));
    if (status.length) status.on('input', () => status.removeClass("is-invalid"));
    if (fileupload.length) fileupload.on('change', () => fileupload.removeClass("is-invalid"));

    
    $('[id*=txtMRP]').on('input', function () {
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

}

function clearFile() {
    let oldInput = $('[id*=flpUpload]');
    if (!oldInput.length) return;

    // Create new file input
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
    bindValidationEvents();
});

