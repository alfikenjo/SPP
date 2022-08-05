$(document).ready(function () {
    $('#TableData').DataTable({
        destroy: true,
        order: [[4, 'desc']],
        columns: [
            {
                data: ID, defaultContent: '',
                render: function (data, type, full, meta) {
                    return '<td>' +
                        '<div class="flex align-items-center list-user-action text-nowrap">' +
                        '<button type="button" class="btn btn-outline-primary btn-sm mr-1" onclick="Edit(\'' + full.ID + '\');"><i class="fa fa-edit"></i></button>' +
                        '</div>' +
                        '</td>';

                }
            },
            {
                data: "Tipe",
                "mRender": function (data, type, row) {
                    return data.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                }
            },
            {
                data: 'Subject', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) {
                        if (data.length > 100)
                            return '<span style="cursor: pointer" title="' + data.replace(/</g, "&lt;").replace(/>/g, "&gt;") + '">' + data.substring(0, 50).replace(/</g, "&lt;").replace(/>/g, "&gt;") + '...</span>';
                        else
                            return data.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                    }

                    else return null

                }
            },
            {
                data: 'Status', defaultContent: '',
                render: function (data, type, full, meta) {
                    return data.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                }
            },
            { data: 'UpdatedOn', defaultContent: "" },
            { data: 'UpdatedBy', defaultContent: "" }

        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            RefreshTable();
        },
        createdRow: function (row, data, dataIndex) {
            if (data.Status == 'Non Aktif') {
                $(row).addClass('text-danger');
            }
        }
    });
});

function RefreshTable() {
    CloseForm();
    $.ajax({
        url: VP + 'Setting/GetEmailSetting',
        type: 'POST',
        success: function (Result) {
            if (Result.Error == false) {
                var table = $('#TableData').DataTable();
                table.clear().draw();
                if (Result.Message != null && Result.Message.length > 0) {
                    table.rows.add(Result.Message);
                    table.columns.adjust().draw();
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

function CloseForm() {
    resetFormEdit();
    $("#divInput").hide();
};

function Edit(ID) {
    $("#Action").val("edit");
    resetFormEdit();
    FillForm(ID);
}

function FillForm(ID) {

    $.ajax({
        url: VP + 'Setting/GetEmailSettingByID',
        type: 'POST',
        data: {
            ID: ID
        },
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                document.getElementById('ID').value = ID;
                document.getElementById('Tipe').value = data.Tipe;
                document.getElementById('Subject').value = data.Subject;

                if (data.Parameter != '') {
                    var IDRoles = data.Parameter;
                    var a = IDRoles.split(",");
                    var badge = '';
                    for (i = 0; i < a.length; i++) {
                        if (a[i].trim().length > 1) {
                            badge += "<span class='mt-2 badge border border-primary text-primary mt-2 mr-2'>" + a[i].trim() + "</span>";
                        }
                    }
                    document.getElementById('div_parameter').innerHTML = badge;
                }

                if (data.Konten)
                    $("#Konten").summernote("code", data.Konten);
                else
                    $("#Konten").summernote("code", "");

                document.getElementById('Status').value = data.Status;
                $("#FormTitle").text("Modify Mail Template");
                $("#divInput").show();
                document.getElementById("Subject").focus();

            } else {
                CustomNotif("error|Oops|" + Result.Message + "");
            }
        },
        error: function (xhr, status, error) {
            CustomNotif("error|Oops|" + error + "");
        }
    })
}

function resetFormEdit() {
    document.getElementById('Tipe').value = '';
    document.getElementById('Subject').value = '';
    $("#Konten").summernote("code", "");
    document.getElementById('Status').value = 'Aktif';
}

$('#FormInput').validate({
    rules: {
        Tipe: { AntiXSS: true, AntiHTML: true },
        Subject: { AntiXSS: true, AntiHTML: true },
        Status: { required: true, AntiXSS: true, AntiHTML: true }

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
        DataForm.append('ID', $('#ID').val());
        DataForm.append('Action', $('#Action').val());

        DataForm.append("Tipe", $('#Tipe').val());
        DataForm.append("Subject", $('#Subject').val());
        DataForm.append("Konten", $('#Konten').summernote('code'));
        DataForm.append('Status', $('#Status').val());

        $.ajax({
            url: VP + 'Setting/SaveEmailSetting',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    CloseForm();
                    RefreshTable();
                    document.getElementById('ID').value = '';
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
