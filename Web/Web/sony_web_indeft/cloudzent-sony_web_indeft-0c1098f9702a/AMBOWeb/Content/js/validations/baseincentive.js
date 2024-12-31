$(document).ready(function () {

    $("#DeleteBaseIncentiveForm").validate({
        submitHandler: function (form) {
            $.ajax({
                url: form.action,
                type: form.method,
                data: $(form).serialize(),
                success: function (response) {
                    if (response.Data) {
                        common.notify('success', response.Message);
                        $('#modalDelete').modal('toggle');
                        dtBaseIncentiveGrid.ajax.reload();
                    }
                    else
                        common.notify('error', response.Message);
                },
                error: function (response) {
                    common.notify('error', 'Server error occured. Please contact your administrator.');
                }
            });
        },
        rules: {
            TargetCategoryId: {
                required: true,
                minlength: 1,
                maxlength: 100
            }
        },
        messages: {
            TargetCategoryId: {
                required: "    (TargetCategoryId is Required)",
                minlength: "    (Minimum 1 characters required)",
                maxlength: "    (Maximum 100 characters allowed)"
            }
        },
        errorElement: "span",
        errorPlacement: function (error, element) {
        },
        highlight: function (element, errorClass, validClass) {
            common.notify('error', 'Unable to delete this record. ID is not provided for deletion.');
        },
        unhighlight: function (element, errorClass, validClass) {
        }
    });

});