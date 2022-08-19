$(document).ready(function () {
    FillForm();
});

$('#FormInput').validate({
    rules: {
        SMTPAddress: { AntiXSS: true, AntiHTML: true },
        SMTPPort: { AntiXSS: true, AntiHTML: true },
        EmailAddress: { AntiXSS: true, AntiHTML: true },
        Password: { AntiXSS: true, AntiHTML: true },
        SenderName: { AntiXSS: true, AntiHTML: true }
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
        DataForm.append('enc_SMTPAddress', $('#SMTPAddress').val());
        DataForm.append('enc_SMTPPort', $('#SMTPPort').val());
        DataForm.append('enc_EmailAddress', $('#EmailAddress').val());
        DataForm.append('enc_Password', $('#Password').val());
        DataForm.append('enc_SenderName', $('#SenderName').val());
        DataForm.append('EnableSSL', document.getElementById('EnableSSL').checked);
        DataForm.append('UseDefaultCredentials', document.getElementById('UseDefaultCredentials').checked);
        DataForm.append('UseAsync', document.getElementById('UseAsync').checked);
        DataForm.append('EnableServices', document.getElementById('EnableServices').checked);

        DataForm.append('NewUser', document.getElementById('NewUser').checked);
        DataForm.append('NewRoleAssignment', document.getElementById('NewRoleAssignment').checked);
        DataForm.append('UserPasswordReset', document.getElementById('UserPasswordReset').checked);
        DataForm.append('Messaging', document.getElementById('Messaging').checked);
        DataForm.append('ReminderServices', document.getElementById('ReminderServices').checked);

        $.ajax({
            url: VP + 'Setting/SaveNotificationSetting',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    CustomNotif('success|Saved|Notification Setting is updated|FillForm();');
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
        url: VP + 'Setting/GetNotificationSetting',
        type: 'POST',        
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];

                if (data) {
                    document.getElementById('SMTPAddress').value = data.SMTPAddress;
                    document.getElementById('SMTPPort').value = data.SMTPPort;
                    document.getElementById('EmailAddress').value = data.EmailAddress;
                    document.getElementById('Password').value = data.Password;
                    document.getElementById('SenderName').value = data.SenderName;
                    document.getElementById('EnableSSL').checked = data.EnableSSL;
                    document.getElementById('UseDefaultCredentials').checked = data.UseDefaultCredentials;
                    document.getElementById('UseAsync').checked = data.UseAsync;
                    document.getElementById('EnableServices').checked = data.EnableServices;

                    document.getElementById('NewUser').checked = data.NewUser;
                    document.getElementById('NewRoleAssignment').checked = data.NewRoleAssignment;
                    document.getElementById('UserPasswordReset').checked = data.UserPasswordReset;
                    document.getElementById('Messaging').checked = data.Messaging;
                    document.getElementById('ReminderServices').checked = data.ReminderServices;

                    document.getElementById('UpdatedOn').innerText = data.UpdatedBy + ' @' + data.UpdatedOn;
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
