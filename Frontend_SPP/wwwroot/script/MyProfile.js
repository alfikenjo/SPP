$(document).ready(function () {
    FillForm()
});

function FillForm() {

    $.ajax({
        url: VP + 'Account/GetMyProfile',
        type: 'POST',
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                document.getElementById('UserID').value = data.UserID;
                if (data.Fullname)
                    document.getElementById('txtFullname').value = data.Fullname.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                document.getElementById('txtEmail').value = data.Email.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                if (data.Mobile)
                    document.getElementById('txtMobile').value = data.Mobile.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                document.getElementById('txtAddress').value = data.Address;
                var gender = "";
                if (data.Gender != null && data.Gender != '')
                    gender = data.Gender;
                document.getElementById('ddlGender').value = gender;

                if (data.Roles != null) {
                    if (IDRoles) {
                        var IDRoles = data.Roles;
                        var a = IDRoles.split(";"), i;
                        var badge = '';
                        for (i = 0; i < a.length; i++) {
                            if (a[i].trim().length > 2) {
                                badge += "<span class='mt-2 badge border border-primary text-primary mt-2 mr-2'>" + a[i].trim() + "</span>";
                            }
                        }
                        document.getElementById('div_Role').innerHTML += badge;
                    }
                }

                if (data.Mobile != '' && data.Mobile_Verification == 0)
                    document.getElementById('btn_verifikasi_mobile').style.display = 'block';
                else
                    document.getElementById('btn_verifikasi_mobile').style.display = 'none';

                if (data.Img != "" && data.Img != null) {
                    var path = data.Img;
                    document.getElementById('imgempPicture').src = path;
                }
                else
                    document.getElementById('imgempPicture').src = VP + "../image/default_avatar.png";

            } else {
                CustomNotif("error|Oops|" + Result.Message + "");
            }
        },
        error: function (xhr, status, error) {
            CustomNotif("error|Oops|" + error + "");
        }
    })
}

$(".upload-button").on('click', function () {
    $(".file-upload").click();
});

fuempPicture.onchange = evt => {
    const [file] = fuempPicture.files
    if (file) {
        imgempPicture.src = URL.createObjectURL(file)
    }
}

function ClearFile(FileID) {

    $.ajax({
        url: VP + 'Account/DeleteFotoProfil',
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (Result) {
            if (Result.Error == false) {
                document.getElementById(FileID).value = '';
                var imgempPicture = document.getElementById('imgempPicture');
                imgempPicture.src = '../image/default_avatar.png';
                if (Result.Message == 1) {
                    var culture = document.getElementById('culture').value;
                    if (culture == 'en')
                        CustomNotif('success|Removed|Your profile photo has been removed');
                    else
                        CustomNotif('success|Deleted|Foto profil Anda berhasil dihapus');
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

$('#FormInput').validate({
    rules: {
        txtFullname: { required: false, AntiXSS: true, AntiHTML: true },
        txtMobile: { AntiXSS: true, AntiHTML: true },
        ddlGender: { AntiXSS: true, AntiHTML: true },
        txtAddress: { AntiXSS: true, AntiHTML: true },
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
        DataForm.append('enc_Fullname', $('#txtFullname').val());
        DataForm.append('enc_Email', $('#txtEmail').val());
        DataForm.append('enc_Mobile', $('#txtMobile').val());
        DataForm.append('enc_Address', $('#txtAddress').val());
        DataForm.append('Gender', $('#ddlGender').val());
        DataForm.append("Foto", $('#fuempPicture')[0].files[0]);

        $.ajax({
            url: VP + 'Account/UpdateMyProfile',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    var culture = document.getElementById('culture').value;
                    if (culture == 'en')
                        CustomNotif('success|Saved|Your profile has been successfully updated');
                    else
                        CustomNotif('success|Saved|Perubahan berhasil disimpan');
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

$('#FormChangePassword').validate({
    rules: {
        CurrentPassword: { required: true, AntiXSS: true, AntiHTML: true },
        NewPassword: { required: true, AntiXSS: true, AntiHTML: true },
        NewPasswordVerifiy: { required: true, AntiXSS: true, AntiHTML: true },
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
        DataForm.append('CurrentPassword', $('#CurrentPassword').val());
        DataForm.append('NewPassword', $('#NewPassword').val());
        DataForm.append('NewPasswordVerifiy', $('#NewPasswordVerifiy').val());

        $.ajax({
            url: VP + 'Account/ChangePassword',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    var culture = document.getElementById('culture').value;
                    if (culture == 'en')
                        CustomNotif('success|Saved|Your password has been changed successfully, you can login with the new password|window.location="/Home?culture=' + culture + '#t-login"');
                    else
                        CustomNotif('success|Saved|Perubahan berhasil disimpan, Anda dapat login kembali menggunakan password terbaru|window.location="/Home?culture=' + culture + '#t-login"');
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

$('#FormUpdateEmail').validate({
    rules: {
        txtEmail: { required: true, AntiXSS: true, AntiHTML: true },
        txtMobile: { required: false, AntiXSS: true, AntiHTML: true },
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
        DataForm.append('enc_Mobile', $('#txtMobile').val());

        $.ajax({
            url: VP + 'Account/UpdateEmail',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    var culture = document.getElementById('culture').value;
                    if (Result.Message != '') {
                        if (culture == 'en')
                            CustomNotif('success|Phone Number Verification|Enter the OTP code from your SMS Inbox|ShowInputOTP();');
                        else
                            CustomNotif('success|Verifikasi Nomor Handphone|Input kode OTP yang terdapat pada Inbox SMS Anda|ShowInputOTP();');
                    }
                    else
                        if (culture == 'en')
                            CustomNotif('success|Saved|Your account has been successfully updated');
                        else
                            CustomNotif('success|Saved|Perubahan berhasil disimpan');
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

$('#FormSubmitOTP').validate({
    rules: {
        OTP: { required: true, AntiXSS: true, AntiHTML: true }
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
        DataForm.append('OTP', $('#OTP').val());

        $.ajax({
            url: VP + 'Account/SubmitOTPProfile',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    var culture = document.getElementById('culture').value;
                    if (culture == 'en')
                        CustomNotif('success|OTP|Your mobile number has been successfully verified|ShowUpdateAkunForm();');
                    else
                        CustomNotif('success|OTP|Nomor Handphone Anda sudah berhasil diverifikasi|ShowUpdateAkunForm();');
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

function ResendOTP() {
    var culture = document.getElementById('culture').value;
    var _Mobile = $('#txtMobile').val();
    if (_Mobile) {
        $.ajax({
            url: VP + 'Account/ResendOTPProfile',
            type: 'POST',
            data: {
                enc_Mobile: _Mobile
            },
            success: function (Result) {
                if (Result.Error == false) {

                    if (culture == 'en')
                        CustomNotif('success|OTP Sent by SMS|Please check your SMS inbox|ShowInputOTP()');
                    else
                        CustomNotif('success|OTP Sent by SMS|Silahkan cek inbox SMS Anda|ShowInputOTP()');
                } else {
                    CustomNotif("error|Oops|" + Result.Message);
                }
            },
            error: function (xhr, status, error) {
                CustomNotif("error|Oops|" + Result.Message);
            }
        })
    }
    else {
        if (culture == 'en')
            CustomNotif("error|Oops|An error occurred, please try again|UseHP();");
        else
            CustomNotif("error|Oops|Terjadi kesalahan, silahkan mencoba kembali|UseHP();");
    }
}

function ShowInputOTP() {
    document.getElementById('FormUpdateEmail').style.display = 'none';
    document.getElementById('FormSubmitOTP').style.display = 'block';
}

function ShowUpdateAkunForm() {

    document.getElementById('FormUpdateEmail').style.display = 'block';
    document.getElementById('FormSubmitOTP').style.display = 'none';
    FillForm();
}

var myInput = document.getElementById("NewPassword");
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


var Mobile = document.querySelector("#txtMobile");
window.intlTelInput(Mobile, {
    hiddenInput: "full_number",
    separateDialCode: true,
    utilsScript: "/js/telephone/utils.js",
});