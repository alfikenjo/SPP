$(document).ready(function () {
    FillForm();
});

$('#FormInput').validate({
    rules: {
        Request_OTP: { required: true, AntiXSS: true, AntiHTML: true },
        Submit_OTP: { required: true, AntiXSS: true, AntiHTML: true }
    },
    errorElement: 'span',
    errorPlacement: function (error, element) {
        error.addClass('invalid-feedback');
        element.closest('.form-group').append(error);
    },
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    },
    submitHandler: function () {

        var DataForm = new FormData();
        DataForm.append('Request_OTP', $('#Request_OTP').val());
        DataForm.append('Submit_OTP', $('#Submit_OTP').val());
        

        $.ajax({
            url: VP + 'Setting/SaveConfiguration',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    CustomNotif('success|Saved|Configuration Setting is updated|FillForm();');
                } else {
                    CustomNotif("error|Oops|" + Result.Message + "");
                }
            },
            error: function (xhr, status, error) {
                CustomNotif("error|Oops|" + error + "");
            }
        })
    }
});


function FillForm() {

    $.ajax({
        url: VP + 'Setting/GetConfiguration',
        type: 'POST',
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];

                if (data) {
                    document.getElementById('Request_OTP').value = data.Request_OTP;
                    document.getElementById('Submit_OTP').value = data.Submit_OTP;
                    
                }



            } else {
                CustomNotif("error|Oops|" + Result.Message + "");
            }
        },
        error: function (xhr, status, error) {
            CustomNotif("error|Oops|" + error + "");
        }
    })
}
