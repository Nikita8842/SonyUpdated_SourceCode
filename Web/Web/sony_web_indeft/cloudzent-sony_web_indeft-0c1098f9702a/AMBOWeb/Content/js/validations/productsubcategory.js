$(document).ready(function () {
    $('#ddAddProdCategory').change(function () { $(this).valid(); });
    $('#ddUpdateProdCategory').change(function () { $(this).valid(); });
    $("#AddProdSubCategoryForm").validate({
        submitHandler: function (form) {
            $.ajax({
                url: form.action,
                type: form.method,
                data: $(form).serialize(),
                success: function (response) {
                    if (response.Data) {
                        common.notify('success', response.Message);
                        $('#modalAdd').modal('toggle');
                        dtProductSubCategoryGrid.ajax.reload();
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
            SubCategoryName: {
                required: true,
                minlength: 3,
                maxlength: 150
            },
            Description: {
                required: true,
                minlength: 5,
                maxlength: 500
            },
            ProductCategoryId: {
                required: true,
                digits: true,
                minlength: 1,
                maxlength: 9999999
            }
        },
        messages: {
            SubCategoryName: {
                required: "    (Required)",
                minlength: "    (Minimum 3 characters required)",
                maxlength: "    (Maximum 150 characters allowed)"
            },
            Description: {
                required: "    (Required)",
                minlength: "    (Minimum 5 characters required)",
                maxlength: "    (Maximum 500 characters allowed)"
            },
            ProductCategoryId: {
                required: "    (Required)",
                digits: "    (Invalid Selection)",
                minlength: "    (Required)",
                maxlength: "    (Invalid Selection)"
            }
        },
        errorElement: "span",
        errorPlacement: function (error, element) {
            $(element).parent(".form-group").find('span.error.text-red').remove();
            error.addClass("text-red");
            if ($(element).attr('type') == undefined)
                error.insertAfter($(element).parents(".form-group").children('span:eq(0)'));
            else
                error.insertAfter(element.parents(".form-group").children().first());
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parent(".form-group").find('span.error.text-red').remove();
            $(element).parents(".form-group").addClass("has-success").removeClass("has-error");
        }
    });

    $("#UpdateProdSubCategoryForm").validate({
        submitHandler: function (form) {
            $.ajax({
                url: form.action,
                type: form.method,
                data: $(form).serialize(),
                success: function (response) {
                    if (response.Data) {
                        common.notify('success', response.Message);
                        $('#modalUpdate').modal('toggle');
                        dtProductSubCategoryGrid.ajax.reload();
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
                digits: true,
                minlength: 1,
                maxlength: 100
            },
            SubCategoryName: {
                required: true,
                minlength: 3,
                maxlength: 150
            },
            Description: {
                required: true,
                minlength: 5,
                maxlength: 500
            },
            ProductCategoryId: {
                required: true,
                digits: true,
                minlength: 1,
                maxlength: 9999999
            }
        },
        messages: {
            ID: {
                required: "    (Required)",
                digits: "    (Invalid ID)",
                minlength: "    (Minimum 1 characters required)",
                maxlength: "    (Maximum 100 characters allowed)"
            },
            SubCategoryName: {
                required: "    (Required)",
                minlength: "    (Minimum 3 characters required)",
                maxlength: "    (Maximum 150 characters allowed)"
            },
            Description: {
                required: "    (Required)",
                minlength: "    (Minimum 5 characters required)",
                maxlength: "    (Maximum 500 characters allowed)"
            },
            ProductCategoryId: {
                required: "    (Required)",
                digits: "    (Invalid Selection)",
                minlength: "    (Required)",
                maxlength: "    (Invalid Selection)"
            }
        },
        errorElement: "span",
        errorPlacement: function (error, element) {
            $(element).parent(".form-group").find('span.error.text-red').remove();
            error.addClass("text-red");
            if ($(element).attr('type') == undefined)
                error.insertAfter($(element).parents(".form-group").children('span:eq(0)'));
            else
                error.insertAfter(element.parents(".form-group").children().first());
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parent(".form-group").find('span.error.text-red').remove();
            $(element).parents(".form-group").addClass("has-success").removeClass("has-error");
        }
    });

    $("#DeleteProdSubCategoryForm").validate({
        submitHandler: function (form) {
            $.ajax({
                url: form.action,
                type: form.method,
                data: $(form).serialize(),
                success: function (response) {
                    if (response.Data) {
                        common.notify('success', response.Message);
                        $('#modalDelete').modal('toggle');
                        dtProductSubCategoryGrid.ajax.reload();
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
                digits: true,
                minlength: 1,
                maxlength: 100
            }
        },
        messages: {
            ID: {
                required: "    (Required)",
                digits: "    (Invalid ID)",
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
            $(element).parent(".form-group").find('span.error.text-red').remove();
            $(element).parents(".form-group").addClass("has-success").removeClass("has-error");
        }
    });
});