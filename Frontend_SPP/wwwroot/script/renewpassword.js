$(document).ready(function () {

    var isValid = $('#isValid').val();
    if (isValid == false)
        CustomNotif("error|Oops|Link is invalid or expired|window.location='/'");

    var url_string = window.location.href;
    var url = new URL(url_string);
    var ID = url.searchParams.get("ID");    
    if (ID) {
        document.getElementById('Renew_Password').focus();
    }
});

function Verifikasi(ID) {
    $.ajax({
        url: VP + 'Account/GetUserID',
        type: 'POST',
        data: {
            ID: ID
        },
        success: function (Result) {
            if (Result.Error == false) {
                if (Result.Message == 'Berhasil')
                    CustomNotif('success|Aktivasi Akun|Akun Anda telah aktif, silahkan login sebagai Pelapor pada aplikasi SPP PT SMI|window.location="/#t-login"');
            } else {
                CustomNotif("error|Oops|" + Result.Message + "|window.location='/'");
            }
        },
        error: function (xhr, status, error) {
            CustomNotif("error|Oops|" + error + "|window.location='/'");
        }
    })
}

$('#FormRenewPassword').validate({
    rules: {
        Renew_Password: { required: true, AntiXSS: true, AntiXSS: true, AntiHTML: true },
        Renew_Password_Reentered: { required: true, AntiXSS: true, AntiHTML: true },
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
        DataForm.append("Renew_Password", $('#Renew_Password').val());
        DataForm.append('Renew_Password_Reentered', $('#Renew_Password_Reentered').val());

        var url_string = window.location.href;
        var url = new URL(url_string);
        var ID = url.searchParams.get("ID");    
        DataForm.append('ID', ID);

        $.ajax({
            url: VP + 'Account/SubmitRenewPassword',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {                    
                    CustomNotif('success|Reset Password|Anda berhasil melakukan perubahan password, silahkan login menggunakan password baru Anda|window.location="/#t-login"');
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


var myInput = document.getElementById("Renew_Password");
var letter = document.getElementById("letter");
var capital = document.getElementById("capital");
var number = document.getElementById("number");
var length = document.getElementById("length");

// When the user clicks on the password field, show the message box
myInput.onfocus = function () {
    document.getElementById("message").style.display = "block";
}

// When the user clicks outside of the password field, hide the message box
myInput.onblur = function () {
    document.getElementById("message").style.display = "none";
}

// When the user starts to type something inside the password field
myInput.onkeyup = function () {
    // Validate lowercase letters
    var lowerCaseLetters = /[a-z]/g;
    if (myInput.value.match(lowerCaseLetters)) {
        letter.classList.remove("invalid");
        letter.classList.add("valid");
    } else {
        letter.classList.remove("valid");
        letter.classList.add("invalid");
    }

    // Validate capital letters
    var upperCaseLetters = /[A-Z]/g;
    if (myInput.value.match(upperCaseLetters)) {
        capital.classList.remove("invalid");
        capital.classList.add("valid");
    } else {
        capital.classList.remove("valid");
        capital.classList.add("invalid");
    }

    // Validate numbers
    var numbers = /[0-9]/g;
    if (myInput.value.match(numbers)) {
        number.classList.remove("invalid");
        number.classList.add("valid");
    } else {
        number.classList.remove("valid");
        number.classList.add("invalid");
    }

    // Validate length
    if (myInput.value.length >= 8) {
        length.classList.remove("invalid");
        length.classList.add("valid");
    } else {
        length.classList.remove("valid");
        length.classList.add("invalid");
    }
}
