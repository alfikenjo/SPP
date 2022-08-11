$(document).ready(function () {
    FillCapctha();
    sessionStorage.setItem("Role", '');

});

function FillCapctha() {

    $.ajax({
        type: "POST",
        url: VP + 'Account/FillCaptcha',
        dataType: "json", contentType: "application/json",
        success: function (res) {
            var ImageUrl = res.Message;
            imgCaptcha.src = ImageUrl;
        }

    });
}

$('#FormInput').validate({
    rules: {
        email: { required: true, AntiXSS: true, AntiHTML: true },
        password: { AntiXSS: true, AntiHTML: true },
        captcha: { required: true, AntiXSS: true, AntiHTML: true },
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
    }
});

