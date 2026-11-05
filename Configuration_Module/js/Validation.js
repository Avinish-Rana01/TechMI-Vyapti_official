(function () {

    var btnSubmit = $(".btnSubmit");

    function bindEvents() {
        $("input, select, textarea").off("input change").on("input change", validateButton);

        $(".password, .confirm_password").off("keyup change").on("keyup change", validateButton);

        $("[id*='chkAllMain']").off("change").on("change", function () {
            validateButton();
        });

        $(".module-table input[type='checkbox']").off("change").on("change", function () {
            validateButton();
        });
    }

    function validateButton() {
        let isValid = true;

        $('[required]').each(function () {
            if (!$(this).val() || $(this).is(':invalid')) {
                isValid = false;
            }
        });

        if ($("input[name*='rblHQConfig']:checked").length === 0) {
            isValid = false;
        }

        const checkedModules = $(".module-table input[type='checkbox']:checked").length;

        if (checkedModules === 0) {
            isValid = false;
        }

        const pass = $('.password').val() || "";
        const confirm = $('.confirm_password').val() || "";
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>]).{8,}$/;

        if (pass.length > 0 && !passwordRegex.test(pass)) {
            $('#alert_msg').show().html("Password must be at least 8 characters with Upper, Lower, Number, and Special char.");
            isValid = false;
        } else {
            $('#alert_msg').hide();
        }

        if (confirm.length > 0 && pass !== confirm) {
            $('#alert_msg2').show().html("Passwords do not match.");
            isValid = false;
        } else {
            $('#alert_msg2').hide();
        }

        if (pass === "" || confirm === "") {
            isValid = false;
        }

        $(".btnSubmit").prop("disabled", !isValid);
    }
    $(document).ready(function () {
        bindEvents();
        validateButton();
    });

    if (Sys && Sys.WebForms) {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            bindEvents();
            validateButton();
        });
    }

})();
