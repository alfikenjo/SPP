$(document).ready(function () {
    $('#TableData').DataTable({
        destroy: true,
        columns: [
            {
                data: ID, defaultContent: '',
                render: function (data, type, full, meta) {
                    return '<td>' +
                        '<div class="flex align-items-center list-user-action text-nowrap">' +
                        '<button type="button" class="btn btn-outline-primary btn-sm mr-1" onclick="Edit(\'' + full.ID + '\');"><i class="fa fa-edit"></i></button>' +
                        '<button type="button" class="btn btn-outline-danger btn-sm" onclick="Delete(\'' + full.ID + '\');"><i class="fa fa-trash"></i></button>' +
                        '</div>' +
                        '</td>';

                }
            },
            {
                data: 'Name', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null

                }
            },
            { data: 'UpdatedOn', defaultContent: "" },
            { data: 'UpdatedBy', defaultContent: "" },

        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            RefreshTable();
        }
    });


});

function RefreshTable() {
    $.ajax({
        url: VP + 'Setting/GetFileEkstensionFilter',
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

$("#btnAdd").click(function () {
    $("#Action").val("add");
    $("#FormTitle").text("Add Ekstension");
    resetFormEdit();
    $("#divInput").show();
    document.getElementById("Name").focus();
});

function CloseForm() {
    resetFormEdit();
    $("#divInput").hide();
};

function Edit(ID) {
    $("#Action").val("edit");
    resetFormEdit();
    FillForm(ID);
}

function Delete(ID) {
    resetFormEdit();
    $("#divInput").hide();

    var DataForm = new FormData();
    DataForm.append('ID', ID);
    DataForm.append('Action', 'hapus');

    Swal.fire({
        title: 'Apakah anda yakin akan menghapus data ini?',
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: 'Ya',
        denyButtonText: 'Tidak',
        customClass: {
            actions: 'my-actions',
            cancelButton: 'order-1 right-gap',
            confirmButton: 'order-2',
            denyButton: 'order-3',
        }
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: VP + 'Setting/SaveFileEkstensionFilter',
                data: DataForm,
                type: 'POST',
                contentType: false,
                processData: false,
                success: function (Result) {
                    if (Result.Error == false) {
                        RefreshTable();
                        CustomNotif('success|Saved|Data berhasil dihapus');
                    } else {
                        CustomNotif("error|Oops|" + Result.Message + "");
                    }
                },
                error: function (xhr, status, error) {
                    CustomNotif("error|Oops|" + error + "");
                }
            });
        } else if (result.isDenied) {
            Swal.fire('Data batal dihapus', '', 'info')
        }
    })

}

function FillForm(ID) {

    $.ajax({
        url: VP + 'Setting/GetFileEkstensionFilterByID',
        type: 'POST',
        data: {
            ID: ID
        },
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                document.getElementById('ID').value = ID;
                document.getElementById('Name').value = data.Name;
                $("#FormTitle").text("Modify Ekstension");
                $("#divInput").show();
                document.getElementById("Name").focus();
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
    $("#ID").val("");
    document.getElementById('Name').value = "";
}


$('#FormInput').validate({
    rules: {
        Name: { required: true, AntiXSS: true, AntiHTML: true },
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
        DataForm.append('Name', $('#Name').val());

        $.ajax({
            url: VP + 'Setting/SaveFileEkstensionFilter',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    CloseForm();
                    RefreshTable();
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