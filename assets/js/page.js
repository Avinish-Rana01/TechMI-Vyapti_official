$(document).ready(function () {
    $(".custom-select2").select2({
        search: true
    });
    var title = $('title').text();
    $('.page_head').text(title);

});
function pageLoad() {
    jQuery('.dataTable').wrap('<div class="dataTables_scrollBody" />');
    jQuery('.table_modal').wrap('<div class="dataTables_scroll" />');
}
(function () {
    var pageId = $("#find_page").attr("page-id");
    $("span").each(function () {
        if ($(this).hasClass(pageId)) {
            $(this).addClass("active_class");
        }
    });

    var pageId2 = $("#find_page2").attr("page-id2");
    $("a").each(function () {
        if ($(this).hasClass(pageId2)) {
            $(this).addClass("active");
        }
    });

})();

$(".quantity").keypress(function (e) {
    if (e.which < 48 || e.which > 57) {
        e.preventDefault();
    }
});

$(".quantity").on("input", function () {
    this.value = this.value.replace(/^0+/, "");
});

$(".quantity").on("change", function () {
    this.value = this.value.replace(/,/g, "");
    if (isNaN(this.value)) {
        this.value = '';
    }
});
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {

    $(document).ready(function () {
        $(".custom-select2").select2({
            search: true
        });
        var title = $('title').text();
        $('.page_head').text(title);

    });
    function pageLoad() {
        jQuery('.dataTable').wrap('<div class="dataTables_scrollBody" />');
        jQuery('.table_modal').wrap('<div class="dataTables_scroll" />');
    }
    (function () {
        var pageId = $("#find_page").attr("page-id");
        $("span").each(function () {
            if ($(this).hasClass(pageId)) {
                $(this).addClass("active_class");
            }
        });

        var pageId2 = $("#find_page2").attr("page-id2");
        $("a").each(function () {
            if ($(this).hasClass(pageId2)) {
                $(this).addClass("active");
            }
        });
    })();
    
    $(".quantity").keypress(function (e) {
        if (e.which < 48 || e.which > 57) {
            e.preventDefault();
        }
    });

    $(".quantity").on("input", function () {
        this.value = this.value.replace(/^0+/, "");
    });

    $(".quantity").on("change", function () {
        this.value = this.value.replace(/,/g, "");
        if (isNaN(this.value)) {
            this.value = '';
        }
    });
});
