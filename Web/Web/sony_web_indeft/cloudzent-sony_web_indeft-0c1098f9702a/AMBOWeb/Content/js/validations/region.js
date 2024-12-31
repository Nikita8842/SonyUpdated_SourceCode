$(document).ready(function () {


    $("#AddRegionForm").validate({
        submitHandler: function (form) {
            $.ajax({
                url: form.action,
                type: form.method,
                data: $(form).serialize(),
                success: function (response) {
                    if (response.MessageCode == MessageCodes.Acceptable.Created)
                    {
                        common.notify('success', response.Message);
                        $('#modalAdd').modal('toggle');
                        dtRegionGrid.ajax.reload();
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
            RegionName: {
                required: true,
                minlength: 4,
                maxlength: 150
            }
        },
        messages: {
            RegionName: {
                required: "    (Required)",
                minlength: "    (Minimum 4 characters required)",
                maxlength: "    (Maximum 150 characters allowed)"
            }
        },
        errorElement: "span",
        errorPlacement: function (error, element) {
            element.parent(".form-group").find('span').remove();
            error.addClass("text-red");
            error.insertAfter(element.parents(".form-group").children().first());
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").find('span').remove();
            $(element).parents(".form-group").addClass("has-success").removeClass("has-error");
        }
    });

    $("#UpdateRegionForm").validate({
        submitHandler: function (form) {
            $.ajax({
                url: form.action,
                type: form.method,
                data: $(form).serialize(),
                success: function (response) {
                    if (response.Data) {
                        common.notify('success', response.Message);
                        $('#modalUpdate').modal('toggle');
                        dtRegionGrid.ajax.reload();
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
            ID: {
                required: true,
                minlength: 1,
                maxlength: 100
            },
            RegionName: {
                required: true,
                minlength: 4,
                maxlength: 150
            }
        },
        messages: {
            ID: {
                required: "    (Required)",
                minlength: "    (Minimum 1 characters required)",
                maxlength: "    (Maximum 100 characters allowed)"
            },
            RegionName: {
                required: "    (Required)",
                minlength: "    (Minimum 4 characters required)",
                maxlength: "    (Maximum 150 characters allowed)"
            }
        },
        errorElement: "span",
        errorPlacement: function (error, element) {
            element.parent(".form-group").find('span').remove();
            error.addClass("text-red");
            error.insertAfter(element.parents(".form-group").children().first());
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").find('span').remove();
            $(element).parents(".form-group").addClass("has-success").removeClass("has-error");
        }
    });

    $("#DeleteRegionForm").validate({
        submitHandler: function (form) {
            $.ajax({
                url: form.action,
                type: form.method,
                data: $(form).serialize(),
                success: function (response) {
                    if (response.Data) {
                        common.notify('success', response.Message);
                        $('#modalDelete').modal('toggle');
                        dtRegionGrid.ajax.reload();
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
            ID: {
                required: true,
                minlength: 1,
                maxlength: 100
            }
        },
        messages: {
            ID: {
                required: "    (Required)",
                minlength: "    (Minimum 1 characters required)",
                maxlength: "    (Maximum 100 characters allowed)"
            }
        },
        errorElement: "span",
        errorPlacement: function (error, element) {
            element.parent(".form-group").find('span').remove();
            error.addClass("text-red");
            error.insertAfter(element.parents(".form-group").children().first());
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").find('span').remove();
            $(element).parents(".form-group").addClass("has-success").removeClass("has-error");
        }
    });


});