var btnSubmit = $(".btnSubmit");
var input = document
    .querySelectorAll("input")
    .forEach((elem) => elem.addEventListener("input", validateButton));

$("select").change(function () {
    validateButton();
});

$("input").change(function () {
    validateButton();
});

$("textarea").change(function () {
    validateButton();
});

$("textarea").on('input', function () {
    validateButton();
});

btnSubmit.attr("disabled", true);

function validateButton() {

    let valid = true;
    $('[required]').each(function () {
        if ($(this).is(':invalid') || !$(this).val())
            valid = false;
    })
    if (!valid) {
        btnSubmit.attr("disabled", true);
    }
    else {
        btnSubmit.attr("disabled", false);
    }
}
validateButton();

var btnFile = $('.btnFile');
btnFile.attr("disabled", true);

$(".file").change(function () {
    var file = $(this).val();
    if (file !== "") {
        btnFile.attr("disabled", false);
    }
    else {
        btnFile.attr("disabled", true);
    }
});

var btnGenerate = $(".btnGenerate");

$(document).ready(function () {
    $("[id*=ddlReceivingType]").change(function () {
        validatebtnGenerate();
    });

    validatebtnGenerate();
});

function validatebtnGenerate() {
    var value = $("[id*=ddlReceivingType]").val();

    if (value === "") {
        btnGenerate.prop("disabled", true);
    } else {
        btnGenerate.prop("disabled", false);
    }
}

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
    var btnSubmit = $(".btnSubmit");
    var input = document
        .querySelectorAll("input")
        .forEach((elem) => elem.addEventListener("input", validateButton));

    $("select").change(function () {
        validateButton();
    });

    $("input").change(function () {
        validateButton();
    });
    $("textarea").change(function () {
        validateButton();
    });

    $("textarea").on('input', function () {
        validateButton();
    });
    btnSubmit.attr("disabled", true);

    function validateButton() {

        let valid = true;
        $('[required]').each(function () {
            if ($(this).is(':invalid') || !$(this).val())
                valid = false;
        })
        if (!valid) {
            btnSubmit.attr("disabled", true);
        }
        else {
            btnSubmit.attr("disabled", false);
        }
    }
    validateButton();

    var btnFile = $('.btnFile');
    btnFile.attr("disabled", true);

    $(".file").change(function () {
        var file = $(this).val();
        if (file !== "") {
            btnFile.attr("disabled", false);
        }
        else {
            btnFile.attr("disabled", true);
        }
    });

    var btnGenerate = $(".btnGenerate");

    $(document).ready(function () {
        $("[id*=ddlReceivingType]").change(function () {
            validatebtnGenerate();
        });

        validatebtnGenerate();
    });

    function validatebtnGenerate() {
        var value = $("[id*=ddlReceivingType]").val();

        if (value === "") {
            btnGenerate.prop("disabled", true);
        } else {
            btnGenerate.prop("disabled", false);
        }
    }
})