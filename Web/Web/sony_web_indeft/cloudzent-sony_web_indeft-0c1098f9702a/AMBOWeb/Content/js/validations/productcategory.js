$(document).ready(function () {

    $.validator.addMethod("selectvalueminlength", function (value, element, arg) {
        return value.length >= arg;
    }, "");
    $.validator.addMethod("selectvaluemaxlength", function (value, element, arg) {
        return value.length <= arg;
    }, "");


    //$('#ddAddDivision').change(function () { $(this).valid(); });
  //  $('#ddAddCategoryName').change(function () { $(this).valid(); });
    //$('#ddUpdateDivision').change(function () { $(this).valid(); });
    //$('#ddUpdateCategoryName').change(function () { $(this).valid(); });
    $("#AddProductCategoryForm").validate({
        submitHandler: function (form) {
            $.ajax({
                url: form.action,
                type: form.method,
                data: $(form).serialize(),
                success: function (response) {
                    if (response.MessageCode == MessageCodes.Acceptable.Created) {
                        common.notify('success', response.Message);
                        $('#modalAdd').modal('toggle');
                        dtProductCategoryGrid.ajax.reload();
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
            Division: {
                required: true,
                digits: true,
                minlength: 1,
                maxlength: 1,
                selectvalueminlength: 2,
                selectvaluemaxlength: 5
            },
            CategoryName: {
                required: true,
                minlength: 3,
                maxlength: 200
            },
            Description: {
                required: true,
                minlength: 3,
                maxlength: 200
            }
        },
        messages: {
            Division: {
                required: "    (Required)",
                digits: "    (Invalid Selection)",
                minlength: "    (Required)",
                maxlength: "    (Invalid Selection)",
                selectvalueminlength: "    (Minimum 2 characters required)",
                selectvaluemaxlength: "    (Maximum 5 characters required)"
            },
            CategoryName: {
                required: "    (Required)",
                minlength: "    (Minimum 3 characters required)",
                maxlength: "    (Maximum 200 characters allowed)"
            },
            Description: {
                required: "    (Required)",
                minlength: "    (Minimum 3 characters required)",
                maxlength: "    (Maximum 200 characters allowed)"
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

    $("#UpdateProductCategoryForm").validate({
        submitHandler: function (form) {
            $.ajax({
                url: form.action,
                type: form.method,
                data: $(form).serialize(),
                success: function (response) {
                    if (response.MessageCode == MessageCodes.Acceptable.Accepted) {
                        common.notify('success', response.Message);
                        $('#modalUpdate').modal('toggle');
                        dtProductCategoryGrid.ajax.reload();
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
            Division: {
                required: true,
                digits: true,
                minlength: 1,
                maxlength: 1,
                selectvalueminlength: 2,
                selectvaluemaxlength: 5
            },
            CategoryName: {
                required: true,
                minlength: 3,
                maxlength: 200
            },
            Description: {
                required: true,
                minlength: 3,
                maxlength: 200
            }
        },
        messages: {
            ID: {
                required: "    (Required)",
                digits: "    (Invalid ID)",
                minlength: "    (Minimum 1 characters required)",
                maxlength: "    (Maximum 100 characters allowed)"
            },
            Division: {
                required: "    (Required)",
                digits: "    (Invalid Selection)",
                minlength: "    (Required)",
                maxlength: "    (Invalid Selection)",
                selectvalueminlength: "    (Minimum 2 characters required)",
                selectvaluemaxlength: "    (Maximum 5 characters required)"
            },
            CategoryName: {
                required: "    (Required)",
                minlength: "    (Minimum 3 characters required)",
                maxlength: "    (Maximum 200 characters allowed)"
            },
            Description: {
                required: "    (Required)",
                minlength: "    (Minimum 3 characters required)",
                maxlength: "    (Maximum 200 characters allowed)"
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

    $("#DeleteProductCategoryForm").validate({
        submitHandler: function (form) {
            $.ajax({
                url: form.action,
                type: form.method,
                data: $(form).serialize(),
                success: function (response) {
                    if (response.MessageCode == MessageCodes.Acceptable.Accepted) {
                        common.notify('success', response.Message);
                        $('#modalDelete').modal('toggle');
                        dtProductCategoryGrid.ajax.reload();
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