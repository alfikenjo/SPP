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
                document.getElementById('txtFullname').value = data.Fullname;
                document.getElementById('txtEmail').value = data.Email;
                document.getElementById('txtMobile').value = data.Mobile;
                document.getElementById('txtAddress').value = data.Address;
                if (data.Gender)
                    document.getElementById('ddlGender').value = data.Gender;
                document.getElementById('txt_NIP').value = data.NIP;
                document.getElementById('txt_Jabatan').value = data.Jabatan;
                document.getElementById('txt_Divisi').value = data.Divisi;

                if (data.Roles != '') {
                    var IDRoles = data.Roles;
                    if (IDRoles) {
                        var a = IDRoles.split(";");
                        var badge = '';
                        for (i = 0; i < a.length; i++) {
                            if (a[i].trim().length > 2) {
                                badge += "<span class='mt-2 badge border border-primary text-primary mt-2 mr-2'>" + a[i].trim() + "</span>";
                            }
                        }
                        document.getElementById('div_Role').innerHTML += badge;
                    }
                }

                if (data.Delegators != '' && data.Delegators != null) {
                    var IDRoles = data.Delegators;
                    var a = IDRoles.split(";");
                    var badge = '';
                    for (i = 0; i < a.length; i++) {
                        if (a[i].trim().length > 2) {
                            badge += "<span class='mt-2 badge border border-danger text-danger mt-2 mr-2'>" + a[i].trim() + "</span>";
                        }
                    }
                    document.getElementById('div_Role').innerHTML += badge;
                }

                if (data.Img != "" && data.Img != null) {
                    var path = data.Img;
                    document.getElementById('imgempPicture').src = path;
                }
                else
                    document.getElementById('imgempPicture').src = VP + "../image/default_avatar.png";

                if (data.EmailNotification == 1)
                    document.getElementById('chk_EmailNotification').checked = true;
                else
                    document.getElementById('chk_EmailNotification').checked = false;

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
                if (Result.Message == 1)
                    CustomNotif('success|Deleted|Foto profil Anda berhasil dihapus');
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
        txtFullname: { required: true, AntiXSS: true, AntiHTML: true },
        txtMobile: { AntiXSS: true, AntiHTML: true },
        ddlGender: { AntiXSS: true, AntiHTML: true },
        txtAddress: { AntiXSS: true, AntiHTML: true },
        txt_NIP: { AntiXSS: true, AntiHTML: true },
        txt_Jabatan: { AntiXSS: true, AntiHTML: true },
        txt_Divisi: { AntiXSS: true, AntiHTML: true },
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
        DataForm.append('enc_Address', $('#txtAddress').val());
        DataForm.append('Gender', $('#ddlGender').val());
        DataForm.append('enc_NIP', $('#txt_NIP').val());
        DataForm.append('enc_Jabatan', $('#txt_Jabatan').val());
        DataForm.append('enc_Divisi', $('#txt_Divisi').val());
        DataForm.append("Foto", $('#fuempPicture')[0].files[0]);

        $.ajax({
            url: VP + 'Account/UpdateMyProfile',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
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
        DataForm.append('Username', $('#txtEmail').val());
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

        var EmailNotification;
        if (document.getElementById('chk_EmailNotification').checked)
            EmailNotification = 1;
        else
            EmailNotification = 0;
        DataForm.append('EmailNotification', EmailNotification);

        $.ajax({
            url: VP + 'Account/UpdateEmail',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    if (Result.Message != '')
                        CustomNotif('success|Verifikasi Nomor Handphone|Input kode OTP yang terdapat pada Inbox SMS Anda|ShowInputOTP();');
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
                    var ID = Result.Message;
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
    var _Mobile = $('#txtMobile').val();
    if (_Mobile) {
        $.ajax({
            url: VP + 'Account/ResendOTPProfile',
            type: 'POST',
            data: {
                Mobile: _Mobile
            },
            success: function (Result) {
                if (Result.Error == false) {
                    CustomNotif('success|OTP Sent by SMS|Silahkan cek inbox SMS Anda');
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
        CustomNotif("error|Oops|Terjadi kesalahan, silahkan mencoba kembali");
    }
}

function ShowInputOTP() {
    document.getElementById('FormUpdateEmail').style.display = 'none';
    document.getElementById('FormSubmitOTP').style.display = 'block';
}

function ShowUpdateAkunForm() {
    document.getElementById('FormUpdateEmail').style.display = 'block';
    document.getElementById('FormSubmitOTP').style.display = 'none';
}

var Mobile = document.querySelector("#txtMobile");
window.intlTelInput(Mobile, {
    hiddenInput: "full_number",
    separateDialCode: true,
    utilsScript: "/js/telephone/utils.js",
});