$(document).ready(function () {
    function initializeDatePickers() {
        const today = new Date();

        $('.date-start').bootstrapMaterialDatePicker({
            time: false,
            nowButton: false,
            clearButton: true,
            format: 'YYYY-MM-DD',
            maxDate: today
        }).off('change').on('change', function (e, date) {
            if (date) {
                $('.date-end').prop('disabled', false);
                $('.date-end').val(''); 
                $('.date-end').bootstrapMaterialDatePicker('setMinDate', date);
                $('.date-end').bootstrapMaterialDatePicker('setMaxDate', today);
            } else {
                $('.date-end').val('');
                $('.date-end').prop('disabled', true);
                $('.date-end').bootstrapMaterialDatePicker('setMinDate', null);
            }
        });

        $('.date-end').bootstrapMaterialDatePicker({
            time: false,
            nowButton: false,
            clearButton: true,
            format: 'YYYY-MM-DD',
            maxDate: today
        }).off('change').on('change', function (e, date) {
            const fromDate = $('.date-start').val();

            const toDateStr = moment(date).format('YYYY-MM-DD');
            const fromDateStr = moment(fromDate).format('YYYY-MM-DD');

            if (fromDate && toDateStr < fromDateStr) {
                alert('To Date must be greater than or equal to From Date.');
                $('.date-end').val('');
            }
        }).prop('disabled', $('.date-start').val() === '');

        const currentFromDate = $('.date-start').val();
        if (currentFromDate) {
            const minDate = new Date(currentFromDate.split('/').reverse().join('-'));
            $('.date-end').bootstrapMaterialDatePicker('setMinDate', minDate);
        }
    }

    initializeDatePickers();

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        initializeDatePickers();
    });
});
