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

//function TestLDAP() {
//    var username = $('#email').val();
//    if (username != "") {
//        $.ajax({
//            url: VP + 'Account/GetADUserByUsername',
//            type: 'POST',
//            data: {
//                username: username
//            },
//            success: function (Result) {
//                if (Result.Error == false) {
//                    var data = Result.Message;
//                    if (data == null)
//                        CustomNotif("error|failed to connect|" + data + "");
//                    else
//                        CustomNotif("success|connected|" + data + "");

//                } else {
//                    CustomNotif("error|Oops|" + Result.Message + "");
//                }
//            },
//            error: function (xhr, status, error) {
//                CustomNotif("error|Oops|" + error + "");
//            }
//        })
//    }
//    else {
//        CustomNotif("error|cannot use empty username|please input username");
//    }
    
//}

//function OpenLDAP() {
//    document.getElementById('div_username').style.display = 'block';
//}