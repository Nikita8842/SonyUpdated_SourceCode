var common = {

    ajax_get: function (url, data, loadingDivID, success, error) {
        return $.ajax({
            url: url,
            data: JSON.stringify(data),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                var loader = '<div class="overlay" id="whizTloader"><i class="fa fa-refresh fa-spin"></i></div>';
                $(loadingDivID).append(loader);
            },
            success: success,
            error: error,
            complete: function () {
                $(loadingDivID).find('.overlay').remove();
            }
        });
    },

    ajax_post: function (url, data, loadingDivID, success, error) {
        return $.ajax({
            url: url,
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                var loader = '<div class="overlay" id="whizTloader"><i class="fa fa-refresh fa-spin"></i></div>';
                $(loadingDivID).append(loader);
            },
            success: success,
            error: error,
            complete: function () {
                $(loadingDivID).find('.overlay').remove();
            }
        });
    },
    notify: function (type,text) {
        new Noty({
            type: type,
            text: text,
            layout: 'topRight',
            theme: 'metroui',//available themes are relax, mint and metroui, we can also create our own personal themes
            timeout: 5000,//in milliseconds, or false for sticky notification
            progressBar: true,
            closeWith: ['click']
        }).show();
    },
    createDatePicker: function (id) {
        $(id).prop('data-inputmask', "'alias': 'dd/mm/yyyy'");
        $(id).prop('data-mask', '');
        $(id).inputmask('dd/mm/yyyy', { 'placeholder': 'dd/mm/yyyy' })
        $(id).datepicker({
            autoclose: true,
            format: "dd/mm/yyyy"
        });
    },
    linkDatePickers: function (startDateID, endDateID) {
        $(startDateID).on('change', function () {
            var startdate = $(startDateID).datepicker('getDate');
            $(endDateID).datepicker('setStartDate', startdate);
        });
    }
}

$(document).ready(function ()
{
    //readonly radio/text/select will not focus and will be non editable
    $('input[readonly]').click(function () {
        return false;
    });
    $('select.form-control.input-sm.readonly').click(function () {
        alert('working');
        return false;
    });
    //iCheck for checkbox and radio inputs
    $('input[type="checkbox"], input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });

    $('.text-blue.small.clearDD').on('click', function () {
        $(this).parent().find('select').first().val(null).change();
    });

    $('span.removeAllDD').on('click', function () {
        $(this).parent().parent().find('select').first().val(null).change();
    });

    $('span.selectAllDD').on('click', function () {
        var values = $(this).parent().parent().find('select').first().find('option').map(function () { return $(this).val(); });
        $(this).parent().parent().find('select').first().val(values).change();
    });

    $('.text-blue.small.clearDD').on('click', function () {
        $(this).parent().find('select').first().val(null).change();
    });


    //Bug ID: 0001750
    //User: Nikhil Thakral
    //we should not restrict user keypress, instead only server side regular expression will be used to detect invalid characters
    //hence commenting these events
    //$('input[AlphaNumericWithSpace]').keyup(function () {
    //    $(this).val($(this).val().replace(/[^a-zA-Z0-9 ]/g, function (str) { common.notify('error','Only alphabets, numbers and spaces are allowed in this field.'); return ''; }));
    //});

    //$('input[AlphaNumeric]').keyup(function () {
    //    $(this).val($(this).val().replace(/[^a-zA-Z0-9]/g, function (str) { common.notify('error', 'Only alphabets and numbers are allowed in this field.'); return ''; }));
    //});

    //$(document).on("keydown", "input[Numeric]", function (e) {
    //    if (!((e.which >= 48 && e.which <= 57) || (e.which >= 96 && e.which <= 105) || e.which == 8 || e.which == 9 || e.which == 13)) {
    //        common.notify('error', 'Only numbers are allowed in this field.');
    //        e.preventDefault();
    //    }
    //});

    //$('input[NumericWithSpace]').keyup(function () {
    //    $(this).val($(this).val().replace(/[^0-9 ]/g, function (str) { common.notify('error', 'Only numbers and spaces are allowed in this field.'); return ''; }));
    //});

    //$('input[AlphaNumericSymbol1WithSpace]').keyup(function () {
    //    $(this).val($(this).val().replace(/[^a-zA-Z0-9 ._-]/g, function (str) { common.notify('error', 'Only alphabets, numbers, spaces, periods, hyphens and underscores are allowed in this field.'); return ''; }));
    //});

    //$('input[AlphaNumericSymbol1]').keyup(function () {
    //    $(this).val($(this).val().replace(/[^a-zA-Z0-9._-]/g, function (str) { common.notify('error', 'Only alphabets, numbers, periods, hyphens and underscores are allowed in this field.'); return ''; }));
    //});
});


function ValidateFromToDate()
{
    var _FromDate = $('#txtFromDate').val();
    var _ToDate = $('#txtToDate').val();

    var datefromparse = _FromDate.split('/');
    var parafromdate = datefromparse[1] + '/' + datefromparse[0] + '/' + datefromparse[2]

    var datetoparse = _ToDate.split('/');
    var paratodate = datetoparse[1] + '/' + datetoparse[0] + '/' + datetoparse[2]

    var fd = Date.parse(parafromdate);
    var td = Date.parse(paratodate);

    if (_FromDate == null || _FromDate == "") {
        common.notify('error', "Please select From Date");
        return false;
    }
    if (_ToDate == null || _ToDate == "") {
        common.notify('error', "Please select To Date");
        return false;
    }
    if (fd > td) {
       // common.notify('error', "To Date Can't be less than From Date ");
        return false;
    }
    else
        return true;
}


function ValidateCurrentDate() {

    var _FromDate = $('#txtFromDate').val();
    var _ToDate = $('#txtToDate').val();

    var datefromparse = _FromDate.split('/');
    var parafromdate = datefromparse[1] + '/' + datefromparse[0] + '/' + datefromparse[2]

    var datetoparse = _ToDate.split('/');
    var paratodate = datetoparse[1] + '/' + datetoparse[0] + '/' + datetoparse[2]

    var fd = Date.parse(parafromdate);
    var td = Date.parse(paratodate);

    var d = new Date().getDate();
    var m = new Date().getMonth() + 1; // JavaScript months are 0-11
    var y = new Date().getFullYear();

    if (d < 10) {
        d = '0' + d;
    }

    if (m < 10) {
        m = '0' + m;
    }

    var Curdate = d + '/' + m + '/' + y;
    var Curdateparse = Curdate.split('/');
    var Curdate = Curdateparse[1] + '/' + Curdateparse[0] + '/' + Curdateparse[2]
    var cd = Date.parse(Curdate);

    if (fd > cd || td > cd) {
        return false
    }
    else
        return true;
}
