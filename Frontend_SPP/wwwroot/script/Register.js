var culture = document.getElementById('culture').value;

$(document).ready(function () {

    FillCapcthaRegister();
    FillPeriode();


    var url_string = window.location.href;
    var url = new URL(url_string);
    var ID = url.searchParams.get("ID");
    if (ID != null) {
        Verifikasi(ID);
    }
});

function Verifikasi(ID) {
    $.ajax({
        url: VP + 'Account/VerifikasiByEmail',
        type: 'POST',
        data: {
            ID: ID
        },
        success: function (Result) {
            if (Result.Error == false) {
                if (Result.Message == 'Berhasil')
                    if(culture == 'en')
                        CustomNotif('success|Account Activation|Your account is now active, you can login as whistleblower on PT SMI WBS Application|window.location="/#t-login"');
                    else
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

$('#FormRegister').validate({
    rules: {
        Fullname: { AntiXSS: true, AntiHTML: true },
        Email: { required: true, AntiXSS: true, AntiHTML: true },
        Mobile: { AntiXSS: true, AntiHTML: true },
        Register_Password: { required: true, AntiXSS: true, AntiXSS: true, AntiHTML: true },
        Register_Password_Reentered: { required: true, AntiXSS: true, AntiHTML: true },
        captcha: { required: true, AntiXSS: true, AntiHTML: true }
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
        DataForm.append('enc_Fullname', $('#Fullname').val());
        DataForm.append('enc_Email', $('#Email').val());
        DataForm.append('enc_Mobile', $('#Mobile').val());
        DataForm.append("Register_Password", $('#Register_Password').val());
        DataForm.append('Register_Password_Reentered', $('#Register_Password_Reentered').val());
        DataForm.append('captcha', $('#captcha').val());

        $.ajax({
            url: VP + 'Account/Register',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    if(culture == 'en')
                        CustomNotif('success|Register|Please check your email inbox to activate your account|window.location="/"');
                    else
                        CustomNotif('success|Register|Silahkan cek inbox email Anda untuk melakukan aktivasi akun|window.location="/"');
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

$('#FormLogin').validate({
    rules: {

        Email: { required: true, AntiXSS: true, AntiHTML: true },
        Login_Password: { required: true, AntiXSS: true, AntiXSS: true, AntiHTML: true },
        captchaLogin: { required: false, AntiXSS: true, AntiXSS: true, AntiHTML: true }
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
        DataForm.append('enc_Email', $('#Login_Email').val());
        DataForm.append("PasswordHash", $('#Login_Password').val());
        DataForm.append("captcha", $('#captchaLogin').val());

        $.ajax({
            url: VP + 'Account/Login',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    if (Result.culture)
                        window.location = "/Dashboard?culture=" + Result.culture;
                    else
                        window.location = "/Dashboard?culture=ID";
                }
                else {
                    var errorMsg = Result.Message;
                    var failedAttemp = Result.failedAttemp;
                    if (failedAttemp == 3) {
                        ResetFormLogin();
                        FillCapcthaLogin();
                    }
                    else {
                        ResetFormLogin();
                        CustomNotif("error|Oops|" + Result.Message + "");
                    }

                }
            },
            error: function (xhr, status, error) {
                CustomNotif("error|Oops|" + error + "");
            }
        })
    }
});

function ResetFormLogin() {
    document.getElementById('Login_Password').value = '';
    if (captchaLogin)
        document.getElementById('captchaLogin').value = '';
}

$('#FormForgotPassword').validate({
    rules: {
        FP_Email: { required: true, AntiXSS: true, AntiHTML: true }
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
        DataForm.append('enc_Email', $('#FP_Email').val());

        $.ajax({
            url: VP + 'Account/SubmitForgotPasswordByEmail',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    if(culture == 'en')
                        CustomNotif('success|Reset Password|Please check your email inbox to reset your password|window.location="/"');
                    else
                        CustomNotif('success|Reset Password|Silahkan cek inbox email Anda untuk melakukan reset password|window.location="/"');
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

$('#FormForgotPasswordHP').validate({
    rules: {
        FP_phone: { required: true, AntiXSS: true, AntiHTML: true }
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
        DataForm.append('enc_Mobile', $('#FP_phone').val());

        $.ajax({
            url: VP + 'Account/SubmitForgotPasswordByHP',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                document.getElementById('UserID').value = '';
                if (Result.Error == false) {
                    var New_User_Password_Forgotten_ID = Result.Message;
                    document.getElementById('New_User_Password_Forgotten_ID').value = New_User_Password_Forgotten_ID;

                    if(culture == 'en')
                        CustomNotif('success|OTP Sent by SMS|Please check your SMS inbox to reset your password|OpenInputOTP();');
                    else
                        CustomNotif('success|OTP Sent by SMS|Silahkan cek inbox SMS Anda untuk melakukan reset password|OpenInputOTP();');
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

$('#FormForgotPasswordOTP').validate({
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
        DataForm.append('New_User_Password_Forgotten_ID', $('#New_User_Password_Forgotten_ID').val());

        $.ajax({
            url: VP + 'Account/SubmitOTP',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    var ID = Result.Message;
                    if(culture == 'en')
                        CustomNotif('success|OTP Accepted|OTP Accepted, please input your new password to recover your account|window.location="/account/renewpassword?ID=' + ID + '"');
                    else
                        CustomNotif('success|OTP Berhasil|Kode OTP diterima, silahkan input password untuk memulihkan akun Anda|window.location="/account/renewpassword?ID=' + ID + '"');
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

function FillCapcthaRegister() {
    $.ajax({
        type: "GET",
        url: VP + 'Home/FillCaptcha',
        dataType: "json", contentType: "application/json",
        success: function (res) {
            var ImageUrl = res.Message;
            imgCaptcha.src = ImageUrl;
        }

    });
}

function FillCapcthaLogin() {
    $.ajax({
        type: "GET",
        url: VP + 'Home/FillCaptcha',
        dataType: "json", contentType: "application/json",
        success: function (res) {
            document.getElementById('div_Captcha').style.display = 'block';
            var ImageUrl = res.Message;
            imgCaptchaLogin.src = ImageUrl;
        }

    });
}

function ResendOTP() {
    var _New_User_Password_Forgotten_ID = $('#New_User_Password_Forgotten_ID').val();
    if (_New_User_Password_Forgotten_ID) {
        $.ajax({
            url: VP + 'Account/ResendOTP',
            type: 'POST',
            data: {
                New_User_Password_Forgotten_ID: _New_User_Password_Forgotten_ID
            },
            success: function (Result) {
                if (Result.Error == false) {
                    var New_User_Password_Forgotten_ID = Result.Message;
                    document.getElementById('New_User_Password_Forgotten_ID').value = New_User_Password_Forgotten_ID;
                    if(culture == 'en')
                        CustomNotif('success|OTP Sent by SMS|Please check your SMS inbox to reset your password');
                    else
                        CustomNotif('success|OTP Sent by SMS|Silahkan cek inbox SMS Anda untuk melakukan reset password');
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
        if(culture == 'en')
            CustomNotif("error|Oops|An error occurred, please try again|UseHP();");
        else
            CustomNotif("error|Oops|Terjadi kesalahan, silahkan mencoba kembali|UseHP();");
    }
}

function OpenFP() {
    document.getElementById('div_login').style.display = 'none';
    document.getElementById('div_forgot_password').style.display = 'block';
    document.getElementById('div_forgot_password_HP').style.display = 'none';
    document.getElementById('div_forgot_password_OTP').style.display = 'none';
}

function UseHP() {
    document.getElementById('div_login').style.display = 'none';
    document.getElementById('div_forgot_password').style.display = 'none';
    document.getElementById('div_forgot_password_HP').style.display = 'block';
    document.getElementById('div_forgot_password_OTP').style.display = 'none';
}

function UseEmail() {
    document.getElementById('div_login').style.display = 'none';
    document.getElementById('div_forgot_password').style.display = 'block';
    document.getElementById('div_forgot_password_HP').style.display = 'none';
    document.getElementById('div_forgot_password_OTP').style.display = 'none';
}

function UseLogin() {
    document.getElementById('div_login').style.display = 'block';
    document.getElementById('div_forgot_password').style.display = 'none';
    document.getElementById('div_forgot_password_HP').style.display = 'none';
    document.getElementById('div_forgot_password_OTP').style.display = 'none';
}

function OpenInputOTP() {
    document.getElementById('div_login').style.display = 'none';
    document.getElementById('div_forgot_password').style.display = 'none';
    document.getElementById('div_forgot_password_HP').style.display = 'none';
    document.getElementById('div_forgot_password_OTP').style.display = 'block';
}

var myInput = document.getElementById("Register_Password");
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

var FP_phone = document.querySelector("#FP_phone");
window.intlTelInput(FP_phone, {
    hiddenInput: "full_number",
    separateDialCode: true,
    utilsScript: "/js/telephone/utils.js",
});

var Mobile = document.querySelector("#Mobile");
window.intlTelInput(Mobile, {
    hiddenInput: "full_number",
    separateDialCode: true,
    utilsScript: "/js/telephone/utils.js",
});

function FillPeriode() {
    $.ajax({
        url: "Home/Get_Year_Dumas",
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (data) {
            for (var i = 0; i < data.Message.length; ++i) {
                var start_year = document.getElementById("start_year");
                start_year.options[start_year.options.length] = new Option(data.Message[i].Year, data.Message[i].Year);

                var end_year = document.getElementById("end_year");
                end_year.options[end_year.options.length] = new Option(data.Message[i].Year, data.Message[i].Year);

                var start_year_chart_2 = document.getElementById("start_year_chart_2");
                start_year_chart_2.options[start_year_chart_2.options.length] = new Option(data.Message[i].Year, data.Message[i].Year);

                var end_year_chart_2 = document.getElementById("end_year_chart_2");
                end_year_chart_2.options[end_year_chart_2.options.length] = new Option(data.Message[i].Year, data.Message[i].Year);

                var start_year_chart_3 = document.getElementById("start_year_chart_3");
                start_year_chart_3.options[start_year_chart_3.options.length] = new Option(data.Message[i].Year, data.Message[i].Year);

                var end_year_chart_3 = document.getElementById("end_year_chart_3");
                end_year_chart_3.options[end_year_chart_3.options.length] = new Option(data.Message[i].Year, data.Message[i].Year);
            }

            document.getElementById("start_year").value = data.Message[0].Year;
            document.getElementById("end_year").value = data.Message[data.Message.length - 1].Year;

            document.getElementById("start_year_chart_2").value = data.Message[0].Year;
            document.getElementById("end_year_chart_2").value = data.Message[data.Message.length - 1].Year;

            document.getElementById("start_year_chart_3").value = data.Message[0].Year;
            document.getElementById("end_year_chart_3").value = data.Message[data.Message.length - 1].Year;

            document.getElementById("start_month").value = data.Message[0].StartMonth;
            document.getElementById("end_month").value = data.Message[0].EndMonth;

            document.getElementById("start_month_chart_2").value = data.Message[0].StartMonth;
            document.getElementById("end_month_chart_2").value = data.Message[0].EndMonth;

            document.getElementById("start_month_chart_3").value = data.Message[0].StartMonth;
            document.getElementById("end_month_chart_3").value = data.Message[0].EndMonth;
        },
        complete: function (data) {
            LoadChart1();
            LoadChart2();
            LoadChart3();
        }
    });
}

function LoadChart1() {


    var DataForm = new FormData();
    DataForm.append('start_year', $('#start_year').val());
    DataForm.append('start_month', $('#start_month').val());
    DataForm.append('end_year', $('#end_year').val());
    DataForm.append('end_month', $('#end_month').val());

    $.ajax({
        url: "Home/Get_Pengaduan_Status_by_Period",
        type: 'POST',
        data: DataForm,
        contentType: false,
        processData: false,
        success: function (data) {
            var result = data.Message[0];
            
            var div_BelumDiproses = document.getElementById('div_BelumDiproses');
            var BelumDiproses = document.createElement('span');
            $(BelumDiproses).attr('data-from', 0);
            $(BelumDiproses).attr('data-to', result.BelumDiproses);
            $(BelumDiproses).attr('data-speed', 2000);
            $(BelumDiproses).attr('data-refresh-interval', 50);
            div_BelumDiproses.innerHTML = '';
            div_BelumDiproses.appendChild(BelumDiproses);

            var div_Total = document.getElementById('div_Total');
            var Total = document.createElement('span');
            $(Total).attr('data-from', 0);
            $(Total).attr('data-to', result.Total);
            $(Total).attr('data-speed', 2000);
            $(Total).attr('data-refresh-interval', 50);
            div_Total.innerHTML = '';
            div_Total.appendChild(Total);

            var div_Diproses = document.getElementById('div_Diproses');
            var Diproses = document.createElement('span');
            $(Diproses).attr('data-from', 0);
            $(Diproses).attr('data-to', result.Diproses);
            $(Diproses).attr('data-speed', 2000);
            $(Diproses).attr('data-refresh-interval', 50);
            div_Diproses.innerHTML = '';
            div_Diproses.appendChild(Diproses);

            var div_Selesai = document.getElementById('div_Selesai');
            var Selesai = document.createElement('span');
            $(Selesai).attr('data-from', 0);
            $(Selesai).attr('data-to', result.Selesai);
            $(Selesai).attr('data-speed', 2000);
            $(Selesai).attr('data-refresh-interval', 50);
            div_Selesai.innerHTML = '';
            div_Selesai.appendChild(Selesai);

            var div_Ditolak = document.getElementById('div_Ditolak');
            var Ditolak = document.createElement('span');
            $(Ditolak).attr('data-from', 0);
            $(Ditolak).attr('data-to', result.Ditolak);
            $(Ditolak).attr('data-speed', 2000);
            $(Ditolak).attr('data-refresh-interval', 50);
            div_Ditolak.innerHTML = '';
            div_Ditolak.appendChild(Ditolak);

            SEMICOLON.widget.counter();

        }
    });


}

function LoadChart2() {
    var _StartYear = $("#start_year_chart_2").val();
    var _StartMonth = $("#start_month_chart_2").val();
    var _EndYear = $("#end_year_chart_2").val();
    var _EndMonth = $("#end_month_chart_2").val();
    $.ajax({
        url: VP + 'Home/get_Chart_2?start_year=' + _StartYear + '&end_year=' + _EndYear + '&start_month=' + _StartMonth + '&end_month=' + _EndMonth,
        type: 'POST',
        success: function (data) {
            var result = data.Message[0];
            var div_Portal = document.getElementById('div_Portal');
            var Portal = document.createElement('span');
            $(Portal).attr('data-from', 0);
            $(Portal).attr('data-to', result.Portal);
            $(Portal).attr('data-speed', 2000);
            $(Portal).attr('data-refresh-interval', 50);
            div_Portal.innerHTML = '';
            div_Portal.appendChild(Portal);

            var div_Surat = document.getElementById('div_Surat');
            var Surat = document.createElement('span');
            $(Surat).attr('data-from', 0);
            $(Surat).attr('data-to', result.Surat);
            $(Surat).attr('data-speed', 2000);
            $(Surat).attr('data-refresh-interval', 50);
            div_Surat.innerHTML = '';
            div_Surat.appendChild(Surat);

            var div_Telepon = document.getElementById('div_Telepon');
            var Telepon = document.createElement('span');
            $(Telepon).attr('data-from', 0);
            $(Telepon).attr('data-to', result.Telepon);
            $(Telepon).attr('data-speed', 2000);
            $(Telepon).attr('data-refresh-interval', 50);
            div_Telepon.innerHTML = '';
            div_Telepon.appendChild(Telepon);

            var div_Email = document.getElementById('div_Email');
            var Email = document.createElement('span');
            $(Email).attr('data-from', 0);
            $(Email).attr('data-to', result.Email);
            $(Email).attr('data-speed', 2000);
            $(Email).attr('data-refresh-interval', 50);
            div_Email.innerHTML = '';
            div_Email.appendChild(Email);

            var div_Fax = document.getElementById('div_Fax');
            var Fax = document.createElement('span');
            $(Fax).attr('data-from', 0);
            $(Fax).attr('data-to', result.Fax);
            $(Fax).attr('data-speed', 2000);
            $(Fax).attr('data-refresh-interval', 50);
            div_Fax.innerHTML = '';
            div_Fax.appendChild(Fax);

            SEMICOLON.widget.counter();

        }

        //success: function (Result) {
        //    if (Result.Error == false) {
        //        Create_Chart_2(Result.Message.Series, Result.Message.Categories, Result.Title);
        //    }
        //}
    })
}

function LoadChart3() {
    var _StartYear = $("#start_year_chart_3").val();
    var _StartMonth = $("#start_month_chart_3").val();
    var _EndYear = $("#end_year_chart_3").val();
    var _EndMonth = $("#end_month_chart_3").val();
    $.ajax({
        url: VP + 'Home/get_Chart_3?start_year=' + _StartYear + '&end_year=' + _EndYear + '&start_month=' + _StartMonth + '&end_month=' + _EndMonth,
        type: 'POST',
        success: function (data) {
            var result = data.Message;          

            for (var i = 0; i < data.Message.length; i++) {
                var label = result[i].Kategori_EN;
                var label = document.getElementById(label);
                var span = document.createElement('span');
                $(span).attr('data-from', 0);
                $(span).attr('data-to', result[i].Jumlah);
                $(span).attr('data-speed', 2000);
                $(span).attr('data-refresh-interval', 50);
                label.innerHTML = '';
                label.appendChild(span);
            }


            //var div_PUU = document.getElementById('div_PUU');
            //var PUU = document.createElement('span');
            //$(PUU).attr('data-from', 0);
            //$(PUU).attr('data-to', result.PUU);
            //$(PUU).attr('data-speed', 2000);
            //$(PUU).attr('data-refresh-interval', 50);
            //div_PUU.innerHTML = '';
            //div_PUU.appendChild(PUU);

            //var div_PraktekGratifikasi = document.getElementById('div_PraktekGratifikasi');
            //var PraktekGratifikasi = document.createElement('span');
            //$(PraktekGratifikasi).attr('data-from', 0);
            //$(PraktekGratifikasi).attr('data-to', result.PG);
            //$(PraktekGratifikasi).attr('data-speed', 2000);
            //$(PraktekGratifikasi).attr('data-refresh-interval', 50);
            //div_PraktekGratifikasi.innerHTML = '';
            //div_PraktekGratifikasi.appendChild(PraktekGratifikasi);

            //var div_WewenangJabatan = document.getElementById('div_WewenangJabatan');
            //var WewenangJabatan = document.createElement('span');
            //$(WewenangJabatan).attr('data-from', 0);
            //$(WewenangJabatan).attr('data-to', result.WJ);
            //$(WewenangJabatan).attr('data-speed', 2000);
            //$(WewenangJabatan).attr('data-refresh-interval', 50);
            //div_WewenangJabatan.innerHTML = '';
            //div_WewenangJabatan.appendChild(WewenangJabatan);

            //var div_AkuntansiKeuangan = document.getElementById('div_AkuntansiKeuangan');
            //var AkuntansiKeuangan = document.createElement('span');
            //$(AkuntansiKeuangan).attr('data-from', 0);
            //$(AkuntansiKeuangan).attr('data-to', result.AKs);
            //$(AkuntansiKeuangan).attr('data-speed', 2000);
            //$(AkuntansiKeuangan).attr('data-refresh-interval', 50);
            //div_AkuntansiKeuangan.innerHTML = '';
            //div_AkuntansiKeuangan.appendChild(AkuntansiKeuangan);

            //var div_PedomanKodeEtik = document.getElementById('div_PedomanKodeEtik');
            //var PedomanKodeEtik = document.createElement('span');
            //$(PedomanKodeEtik).attr('data-from', 0);
            //$(PedomanKodeEtik).attr('data-to', result.PKE);
            //$(PedomanKodeEtik).attr('data-speed', 2000);
            //$(PedomanKodeEtik).attr('data-refresh-interval', 50);
            //div_PedomanKodeEtik.innerHTML = '';
            //div_PedomanKodeEtik.appendChild(PedomanKodeEtik);

            SEMICOLON.widget.counter();

        }

        //success: function (Result) {
        //    if (Result.Error == false) {
        //        Create_Chart_2(Result.Message.Series, Result.Message.Categories, Result.Title);
        //    }
        //}
    })
}

//function LoadChart3() {
//    var _StartYear = $("#start_year_chart_3").val();
//    //var _EndYear = $("#end_year_chart_3").val();
//    $.ajax({
//        url: VP + 'Home/get_Chart_3?start_year=' + _StartYear,
//        type: 'POST',
//        success: function (Result) {
//            if (Result.Error == false) {
//                Create_Chart_3(Result.Message.Series, Result.Message.Categories, Result.Title);
//            }
//        }
//    })
//}

function Create_Chart_2(Series, Categories, Title) {
    var ctx = document.getElementById("chart_2").getContext("2d");
    window.myBar = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: Categories,
            datasets: Series
        },
        options: {
            responsive: true,
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: Title
            }
        }
    });

}

function Create_Chart_3(Series, Categories, Title) {
    var ctx = document.getElementById("chart_3").getContext("2d");
    window.myBar = new Chart(ctx, {
        type: 'line',
        data: {
            labels: Categories,
            datasets: Series
        },
        options: {
            plugins: {
                filler: false,
                title: {
                    display: true,
                    text: (ctx) => 'Fill: ' + ctx.chart.data.datasets[0].fill
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: Title
            }
        },
    });

}