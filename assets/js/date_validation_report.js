
$(document).ready(function () {
    function initializeDatePickers() {
        const today = new Date();

        $('.date-start2').bootstrapMaterialDatePicker({
            time: false,
            nowButton: false,
            clearButton: true,
            format: 'YYYY-MM-DD',
            maxDate: today
        })
            .off('change') // remove previous handler
            .on('change', function (e, date) {
                const inputVal = $(this).val().trim();

                if (inputVal === '') {
                    // ✅ Clear button clicked
                    $('.date-end2').val('');
                    $('.date-end2').prop('disabled', true);
                    $('.date-end2').bootstrapMaterialDatePicker('setMinDate', null);
                    //location.reload();
                    return;
                }

                if (date) {
                    $('.date-end2').prop('disabled', false).val('');
                    $('.date-end2').bootstrapMaterialDatePicker('setMinDate', date);
                    $('.date-end2').bootstrapMaterialDatePicker('setMaxDate', today);
                    $(this).removeClass("is-invalid");
                }
            });

        $('.date-end2').bootstrapMaterialDatePicker({
            time: false,
            nowButton: false,
            clearButton: true,
            format: 'YYYY-MM-DD',
            maxDate: today
        })
            .off('change')
            .on('change', function (e, date) {
                const inputVal = $(this).val().trim();

                if (inputVal === '') {
                   // location.reload();
                    return;
                }

                const fromDate = $('.date-start2').val();
                if (fromDate && date) {
                    const from = new Date(fromDate);
                    if (date < from) {
                        alert('To Date must be greater than or equal to From Date.');
                        $(this).val('');
                    }
                }
            });

        $('.date-end2').prop('disabled', $('.date-start2').val() === '');

        const currentFromDate = $('.date-start2').val();
        if (currentFromDate) {
            $('.date-end2').bootstrapMaterialDatePicker('setMinDate', new Date(currentFromDate));
        }
    }

    initializeDatePickers();

if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        initializeDatePickers();
    });
    }
});

