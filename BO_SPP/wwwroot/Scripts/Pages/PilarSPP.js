$(document).ready(function () {

    $('#TableData').DataTable({
        destroy: true,
        order: [[3, 'asc']],
        columns: [
            {
                data: ID, defaultContent: '',
                render: function (data, type, full, meta) {
                    return '<td>' +
                        '<div class="flex align-items-center list-user-action text-nowrap">' +
                        '<button type="button" class="btn btn-outline-primary btn-sm mr-1" onclick="Edit(\'' + full.ID + '\');"><i class="fa fa-edit"></i></button>' +
                        '<button type="button" class="btn btn-outline-danger btn-sm mr-1" onclick="Delete(\'' + full.ID + '\');"><i class="fa fa-trash"></i></button>' +
                        '</div>' +
                        '</td>';
                }
            },
            {
                data: 'GridTitle', defaultContent: '',
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
                data: 'Description', defaultContent: '',
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
            { data: 's_UpdatedOn', defaultContent: "" },
            { data: 'UpdatedBy', defaultContent: "" }

        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            RefreshTable();
        }
    });

    FillForm();
});

function FillForm() {
    FillForm_ID();
    FillForm_EN();
}

function FillForm_ID() {
    var Tipe = $('#Tipe').val();
    $.ajax({
        url: VP + 'CMS/Get_Single_CMS',
        type: 'POST',
        data: {
            Tipe: Tipe,
            Lang: 'ID'
        },
        success: function (Result) {
            if (Result.Error == false) {

                if (Result.RowCount > 0) {
                    document.getElementById('Action').value = 'edit';

                    var data = Result.Message[0];
                    document.getElementById('ID').value = data.ID;

                    document.getElementById('Title_ID').value = data.Title.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                    document.getElementById('SubTitle_ID').value = data.SubTitle.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                }
                else {
                    document.getElementById('Action').value = 'add';
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

function FillForm_EN() {
    var Tipe = $('#Tipe').val();
    $.ajax({
        url: VP + 'CMS/Get_Single_CMS',
        type: 'POST',
        data: {
            Tipe: Tipe,
            Lang: 'EN'
        },
        success: function (Result) {
            if (Result.Error == false) {

                if (Result.RowCount > 0) {
                    var data = Result.Message[0];

                    document.getElementById('Title_EN').value = data.Title.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                    document.getElementById('SubTitle_EN').value = data.SubTitle.replace(/</g, '&lt;').replace(/>/g, '&gt;');

                }
                else {
                    document.getElementById('Action').value = 'add';
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
        Title_ID: { required: true, AntiXSS: true, AntiHTML: true },
        SubTitle_ID: { required: true, AntiXSS: true, AntiHTML: true },

        Title_EN: { required: true, AntiXSS: true, AntiHTML: true },
        SubTitle_EN: { required: true, AntiXSS: true, AntiHTML: true },

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
        DataForm.append('Tipe', $('#Tipe').val());

        DataForm.append('Title_ID', $('#Title_ID').val());
        DataForm.append('SubTitle_ID', $('#SubTitle_ID').val());

        DataForm.append('Title_EN', $('#Title_EN').val());
        DataForm.append('SubTitle_EN', $('#SubTitle_EN').val());

        $.ajax({
            url: VP + 'CMS/Save_CMS',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    FillForm();
                    CustomNotif('success|Saved|Header berhasil disimpan');
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


function RefreshTable() {
    var Tipe = $('#TipeDetail').val();
    CloseForm();
    $.ajax({
        url: VP + 'CMS/Get_Multiple_CMS',
        data: {
            Tipe: Tipe,
            Lang: 'ID'
        },
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
    $("#ActionDetail").val("add");
    $("#FormTitle").text("Add Pilar SPP");
    $("#divInput").show();
    document.getElementById("GridTitle_ID").focus();
    document.getElementById('nav_ID_Detail').click();
    resetFormEdit();
});

function CloseForm() {
    resetFormEdit();
    $("#divInput").hide();
};

function Edit(ID) {
    $("#ActionDetail").val("edit");
    resetFormEdit();
    FillFormDetail(ID);
}

function Delete(ID) {
    resetFormEdit();
    $("#divInput").hide();

    var DataForm = new FormData();
    DataForm.append('ID', ID);
    DataForm.append('Action', 'hapus');

    Swal.fire({
        title: 'Apakah anda yakin akan menghapus baris ini?',
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
                url: VP + 'CMS/Save_CMS',
                data: DataForm,
                type: 'POST',
                contentType: false,
                processData: false,
                success: function (Result) {
                    if (Result.Error == false) {
                        RefreshTable();
                        CustomNotif('success|Deleted|Data berhasil dihapus');
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

function FillFormDetail_ID(ID) {

    $.ajax({
        url: VP + 'CMS/Get_CMS_By_ID',
        type: 'POST',
        data: {
            ID: ID,
            Lang: 'ID'
        },
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                document.getElementById('IDDetail').value = ID;

                document.getElementById('GridTitle_ID').value = data.GridTitle.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                document.getElementById('Description_ID').value = data.Description.replace(/</g, '&lt;').replace(/>/g, '&gt;');

                $("#divInput").show();
                document.getElementById('GridTitle_ID').focus();
                document.getElementById('nav_ID_Detail').click();

            } else {
                CustomNotif("error|Oops|" + Result.Message + "");
            }
        },
        error: function (xhr, status, error) {
            CustomNotif("error|Oops|" + error + "");
        }
    })
}

function FillFormDetail_EN(ID) {

    $.ajax({
        url: VP + 'CMS/Get_CMS_By_ID',
        type: 'POST',
        data: {
            ID: ID,
            Lang: 'EN'
        },
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];

                document.getElementById('GridTitle_EN').value = data.GridTitle.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                document.getElementById('Description_EN').value = data.Description.replace(/</g, '&lt;').replace(/>/g, '&gt;');

            } else {
                CustomNotif("error|Oops|" + Result.Message + "");
            }
        },
        error: function (xhr, status, error) {
            CustomNotif("error|Oops|" + error + "");
        }
    })
}

function FillFormDetail(ID) {
    FillFormDetail_ID(ID);
    FillFormDetail_EN(ID);
}

function resetFormEdit() {
    document.getElementById('GridTitle_ID').value = '';
    document.getElementById('Description_ID').value = '';
    document.getElementById('GridTitle_EN').value = '';
    document.getElementById('Description_EN').value = '';
}

$('#FormInputDetail').validate({
    rules: {
        GridTitle_ID: { required: true, AntiXSS: true, AntiHTML: true },
        Description_ID: { required: true, AntiXSS: true, AntiHTML: true },

        GridTitle_EN: { required: true, AntiXSS: true, AntiHTML: true },
        Description_EN: { required: true, AntiXSS: true, AntiHTML: true },
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
        DataForm.append('ID', $('#IDDetail').val());
        DataForm.append('Action', $('#ActionDetail').val());
        DataForm.append('Tipe', $('#TipeDetail').val());

        DataForm.append('GridTitle_ID', $('#GridTitle_ID').val());
        DataForm.append('Description_ID', $('#Description_ID').val());

        DataForm.append('GridTitle_EN', $('#GridTitle_EN').val());
        DataForm.append('Description_EN', $('#Description_EN').val());

        $.ajax({
            url: VP + 'CMS/Save_CMS',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    CloseForm();
                    RefreshTable();
                    CustomNotif('success|Saved|Pilar SPP berhasil disimpan');
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