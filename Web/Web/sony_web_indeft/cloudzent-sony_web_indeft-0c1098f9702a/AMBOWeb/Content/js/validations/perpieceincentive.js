$(document).ready(function () {

    $('#ddAddProdCat').on('select2:open, select2:close', function () { $(this).valid(); });
    $('#ddAddTargetCat').change(function () { $(this).valid(); });
    $('#ddAddPPTargetCat').change(function () { $(this).valid(); });
    $('#ddAddSFADivision').change(function () { $(this).valid(); });
    $('#ddAddBranch').change(function () { $(this).valid(); });
    $('#ddAddChannel').change(function () { $(this).valid(); });

    $("#AddPerPieceIncentiveForm").validate({
        rules: {
            SchemeName: {
                required: true,
                minlength: 3,
                maxlength: 200
            },
            DateFrom: {
                required: true,
                minlength: 7,
                maxlength: 7
            },
            DateTo: {
                required: true,
                minlength: 7,
                maxlength: 7
            },
            ProductCategoryIds: {
                required: true,
                minlength: 1,
                maxlength: 9999999999
            },
            TargetCategoryIds: {
                required: true,
                minlength: 1,
                maxlength: 9999999999
            },
            PerPieceTargetCategoryIds: {
                required: true,
                minlength: 1,
                maxlength: 9999999999
            },
            DivisionIds: {
                required: true,
                minlength: 1,
                maxlength: 9999999999
            },
            BranchIds: {
                required: true,
                minlength: 1,
                maxlength: 9999999999
            },
            ChannelIds: {
                required: true,
                minlength: 1,
                maxlength: 9999999999
            }
            
        },
        messages: {
            SchemeName: {
                required: "    (Required)",
                minlength: "    (Minimum 3 characters required)",
                maxlength: "    (Maximum 200 characters allowed)"
            },
            DateFrom: {
                required: "    (Required)",
                minlength: "    (Invalid Date Selected)",
                maxlength: "    (Invalid Date Selected)"
            },
            DateTo: {
                required: "    (Required)",
                minlength: "    (Invalid Date Selected)",
                maxlength: "    (Invalid Date Selected)"
            },
            ProductCategoryIds: {
                required: "    (Required)",
                minlength: "    (Required)",
                maxlength: "    (Invalid Selection Found)",
            },
            TargetCategoryIds: {
                required: "    (Required)",
                minlength: "    (Required)",
                maxlength: "    (Invalid Selection Found)"
            },
            PerPieceTargetCategoryIds: {
                required: "    (Required)",
                minlength: "    (Required)",
                maxlength: "    (Invalid Selection Found)"
            },
            DivisionIds: {
                required: "    (Required)",
                minlength: "    (Required)",
                maxlength: "    (Invalid Selection Found)"
            },
            BranchIds: {
                required: "    (Required)",
                minlength: "    (Required)",
                maxlength: "    (Invalid Selection Found)"
            },
            ChannelIds: {
                required: "    (Required)",
                minlength: "    (Required)",
                maxlength: "    (Invalid Selection Found)"
            }
        },
        errorElement: "span",
        errorPlacement: function (error, element) {
            $(element).parent(".form-group").find('span.error.text-red').remove();
            error.addClass("text-red");
            $(error).insertBefore(element);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").find('span.error.text-red').remove();
            $(element).parents(".form-group").addClass("has-success").removeClass("has-error");
        }
    });

});