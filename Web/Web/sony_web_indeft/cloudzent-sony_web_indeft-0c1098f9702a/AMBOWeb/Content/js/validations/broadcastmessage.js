$(document).ready(function () {


    $("#AddBroadcastMessageForm").validate({
        submitHandler: function (form) {

            var _Subject = $('#txtSubject').val();
            var _Message = $('#txtMessage').val();
            var _Division = $('select#ddAddSFADivision option:selected').length;
            var _Branch = $('select#ddAddBranch option:selected').length

            var _Role = [];
            _Role = $('#ddlRole').val();

            if ($("#fileChooser").get(0).files.length > 0)
            {
                var size = $("#fileChooser")[0].files[0].size;
                if (size > 4000000) {
                    common.notify('error', 'Please upload files of maximum size 4MB.');
                    $('#loading').hide();
                    return false;
                }
            }

            var _Filename=$('#fileChooserValue').val();


            if (_Subject == "") {
                common.notify('error', 'Please Enter Subject');
                $('#loading').hide();
                return false;
            }

            if ($('#txtSubject').val().length > 120) {
                common.notify('error', 'Length of Subject should be max 120 character');
                $('#txtSubject').val('');
                $('#loading').hide();
                return false;
            }

            if (_Message == "") {
                common.notify('error', 'Please Enter Message');
                $('#loading').hide();
                return false;
            }

            if (_Message.length > 500) {
                common.notify('error', 'Length of Subject should be max 500 character');
                $('#txtMessage').val('');
                $('#loading').hide();
                return false;
            }

            if (_Role.length == 0) {
                common.notify('error', 'Please Select Role');
                $('#loading').hide();
                return false;
            }


            if (_Branch == 0) {
                common.notify('error', 'Please Select Branch');
                $('#loading').hide();
                return false;
            }

         

            if (_Division == 0)
            {
                for(var i=0;i<_Role.length;i++)
                {
                    if (_Role[i] == "47" || _Role[i] == "57" || _Role[i] == "59" || _Role[i] == "61") {
                        common.notify('error', 'Please Select Division');
                        $('#loading').hide();
                        return false;
                    }
                }
            }


           


            if (_Filename != "")
            {
                $('#FileName').val(_Filename);

            }
            var formData = new FormData(form);
           
            $.ajax({
                url: form.action,
                type: form.method,
                data: formData,
                success: function (response) {
                    if (response.Data) {
                        $('#loading').hide();
                        $(location).attr('href', '/BroadcastMessage');
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
            
        },
        messages: {
            
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