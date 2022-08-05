function htmlDecode(value) {
    return $("<textarea/>").html(value).text();
}
function htmlEncode(value) {
    return $('<textarea/>').text(value).html();
}

$(document).ready(function () { 
    FillForm();
});

function FillForm() {
    $.ajax({
        url: VP + 'CMS/GetAboutUs',
        type: 'POST',        
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                if (data != null)
                    $("#KontentHTML").summernote("code", data.ContentHTML);
                else
                    $("#KontentHTML").summernote("code", "");

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
        //KontentHTML: { AntiXSS: true, AntiHTML: true },
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
        DataForm.append("ContentHTML", $('#KontentHTML').summernote('code'));
        $.ajax({
            url: VP + 'CMS/SaveAboutUs',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {                    
                    CustomNotif('success|Saved|Konten berhasil disimpan');
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
