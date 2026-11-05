//function openSidebar(entryType) {

//    $('body').addClass('sidebar-open');

//    $('.v-sidebar-wrapper').addClass('sidebar-control');
//    $('.v-sidebar-fade').addClass('active');
//    $('[id$=hfSidebarOpen]').val('true');

//    if (entryType === 'single') {
//        $('[id$=rdbSingleEntry]').prop('checked', true);
//        $('#divSingleEntry').show();
//        $('#divMultipleEntry').hide();
//    }

//    if (entryType === 'multiple') {
//        $('[id$=rdbMultipleEntry]').prop('checked', true);
//        $('#divMultipleEntry').show();
//        $('#divSingleEntry').hide();
//    }
//}

function openSidebar(entryType) {

    $('body').addClass('sidebar-open');

    $('.v-sidebar-wrapper').addClass('sidebar-control');
    $('.v-sidebar-fade').addClass('active');
    $('[id$=hfSidebarOpen]').val('true');

    if (entryType === 'single') {

        $('[id$=rdbSingleEntry]').prop('checked', true);

        $('#divSingleEntry').show();
        $('#divMultipleEntry').hide();
    }

    if (entryType === 'multiple') {

        $('[id$=rdbMultipleEntry]').prop('checked', true);

        $('#divMultipleEntry').show();
        $('#divSingleEntry').hide();
    }

    // Sidebar open hone ke baad DataTable width recalculate
    setTimeout(function () {

        var table = $("table[id*='grdUserMaster']");

        if (
            table.length &&
            $.fn.DataTable.isDataTable(table[0])
        ) {

            table
                .DataTable()
                .columns
                .adjust();

        }

    }, 300);
}

function bindSidebar() {

    $(document).off('click.sidebar').on('click.sidebar', '.sidebar-toggler', function () {
        $('body').addClass('sidebar-open');
        $('.v-sidebar-wrapper').addClass('sidebar-control');
        $('.v-sidebar-fade').addClass('active');

        $('[id$=hfSidebarOpen]').val('true');
        $('[id$=lblEditTitle]').hide();
        $('[id$=lblTitle]').show();


        if ($('[id$=rdbSingleEntry]').is(':checked')) {
            $('.sidebar-title').html("Single Entry");
            $('#divSingleEntry').show();
            $('#divMultipleEntry').hide();
            $('[id$=lblEditTitle]').hide();
            $('[id$=lblTitle]').show();
        }

        if ($('[id$=rdbMultipleEntry]').is(':checked')) {
            $('.sidebar-title').html("Multiple Entry");
            $('#divMultipleEntry').show();
            $('#divSingleEntry').hide();
        }

        const $sidebar = $('.v-sidebar');


        $sidebar.find('input[type="text"], input[type="password"], input[type="email"], input[type="number"], .password')
            .val('');

        $sidebar.find('textarea').val('');

        $sidebar.find('select').each(function () {
            this.selectedIndex = 0;
            $(this).trigger('change');
        });
    });

    

    $(document).off('click.sidebarClose').on('click.sidebarClose', '#sidebar-close-btn', function (e) {

        e.preventDefault();
        e.stopPropagation();
        closeSidebar();
        $('[id$=rdbSingleEntry]').prop('checked', false);

        return false;
    });


    $(document).off('keydown.sidebar').on('keydown.sidebar', function (e) {
        if (e.key === 'Escape') {
            closeSidebar();
        }
    });
}

//function closeSidebar() {
//    $('body').removeClass('sidebar-open');
//    const $sidebar = $('.v-sidebar');
    

//    $sidebar.find('input[type="text"], input[type="password"], input[type="email"], input[type="number"]')
//        .val('');

//    $sidebar.find('input[type="password"], input[type="text"].password')
//        .val('');

//    setTimeout(function () {
//        $sidebar.find('input[type="password"], input[type="text"].password').val('');
//    }, 50);

//    $sidebar.find('textarea').val('');

//    $sidebar.find('select:not(.aspNetDisabled):not([disabled])').each(function () {
//        this.selectedIndex = 0;
//        $(this).triggerHandler('change');
//    });

//    $sidebar.find('select.aspNetDisabled, select[disabled]').each(function () {
//        $(this).empty();
//        $(this).triggerHandler('change');
//    });

//    $sidebar.find('input[type="checkbox"], input[type="radio"]')
//        .prop('checked', false)
//        .triggerHandler('change');

//    $sidebar.find('.aspNetDisabled').each(function () {
//        var $el = $(this);
//        var tag = this.tagName.toLowerCase();
//        if (tag === 'select') {
//            return;
//        } else if (tag === 'input' || tag === 'textarea') {
//            $el.val('');
//        } else {
//            $el.empty().text('');
//        }
//    });

//    $sidebar.find('.module-panel').hide();

//    $('.btnSubmit').each(function () {
//        $(this).is('input') ? $(this).val('Submit') : $(this).text('Submit');
//    });

//    $('.v-sidebar-wrapper').removeClass('sidebar-control');
//    $('.v-sidebar-fade').removeClass('active');
//    $('[id$=hfSidebarOpen]').val('false');
//    $('.sidebar-title').html('');
//    $('#divSingleEntry, #divMultipleEntry').hide();
//}

function closeSidebar() {
    window.location.reload();
}


$(document).ready(function () {
    bindSidebar();
});

