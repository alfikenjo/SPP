$(document).ready(function () {
    $('#TableData').DataTable({
        destroy: true,
        order: [[3, 'asc']],
        columns: [
            {
                data: ID, defaultContent: '',
                render: function (data, type, full, meta) {
                    var status = '';
                    if (full.Status == 'Aktif')
                        status = '<span class="btn btn-outline-success btn-sm"><i class="fa fa-check"></i>&nbsp;Published</button>';
                    return '<td>' +
                        '<div class="flex align-items-center list-user-action text-nowrap">' +
                        '<button type="button" class="btn btn-outline-primary btn-sm mr-1" onclick="Edit(\'' + full.ID + '\');"><i class="fa fa-edit"></i></button>' +
                        '<button type="button" class="btn btn-outline-danger btn-sm mr-1" onclick="Delete(\'' + full.ID + '\');"><i class="fa fa-trash"></i></button>' + status +
                        '</div>' +
                        '</td>';

                }
            },
            {
                data: "Filename",
                "mRender": function (data, type, row) {
                    if (data != null && data != '')
                        return '<td class="text-center"><img class="rounded img-fluid" style="height: 50px" src="' + data + '" ></td>';
                    else
                        return '<td class="text-center"><img class="rounded img-fluid" style="height: 50px" src="/image/banner_none.jpg" ></td>';
                }
            },
            {
                data: 'Title', defaultContent: '',
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
            { data: 's_UpdatedOn', defaultContent: "" },
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
        url: VP + 'CMS/GetBanner',
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
    $("#FormTitle").text("Add Banner");
    resetFormEdit();
    $("#divInput").show();
    document.getElementById('nav_ID').click();
    document.getElementById("fu_Filename_ID").focus();
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
        title: 'Apakah anda yakin akan menghapus banner ini?',
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
                url: VP + 'CMS/SaveBanner',
                data: DataForm,
                type: 'POST',
                contentType: false,
                processData: false,
                success: function (Result) {
                    if (Result.Error == false) {
                        RefreshTable();
                        CustomNotif('success|Deleted|Banner berhasil dihapus');
                    } else {
                        CustomNotif("error|Oops|" + Result.Message + "");
                    }
                },
                error: function (xhr, status, error) {
                    CustomNotif("error|Oops|" + error + "");
                }
            });
        } else if (result.isDenied) {
            Swal.fire('Banner batal dihapus', '', 'info')
        }
    })

}

function FillForm(ID) {

    $.ajax({
        url: VP + 'CMS/GetBannerByID',
        type: 'POST',
        data: {
            ID: ID
        },
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                document.getElementById('ID').value = ID;

                document.getElementById('Title_ID').value = data.Title_ID;         
                document.getElementById('Title_ID_Color').value = data.Title_ID_Color;         
                document.getElementById('SubTitle_ID').value = data.SubTitle_ID;
                document.getElementById('SubTitle_ID_Color').value = data.SubTitle_ID_Color;
                SetPreviewImageText_ID();
                //document.getElementById('LabelTombol_ID').value = data.LabelTombol_ID;
                //document.getElementById('Link_ID').value = data.Link_ID;

                if (data.Filename_ID) {
                    var path = data.Filename_ID;
                    document.getElementById('div_Preview_ID').style.display = 'block';
                    document.getElementById('Preview_ID').src = path;
                }
                else {
                    document.getElementById('Preview_ID').src = "/image/banner_none.jpg";
                    document.getElementById('div_Preview_ID').style.display = 'none';
                }

                document.getElementById('Title_EN').value = data.Title_EN;
                document.getElementById('Title_EN_Color').value = data.Title_EN_Color;
                document.getElementById('SubTitle_EN').value = data.SubTitle_EN;
                document.getElementById('SubTitle_EN_Color').value = data.SubTitle_EN_Color;
                SetPreviewImageText_EN();
                //document.getElementById('LabelTombol_EN').value = data.LabelTombol_EN;
                //document.getElementById('Link_EN').value = data.Link_EN;

                if (data.Filename_EN) {
                    var path = data.Filename_EN;
                    document.getElementById('div_Preview_EN').style.display = 'block';
                    document.getElementById('Preview_EN').src = path;
                }
                else {
                    document.getElementById('Preview_EN').src = "/image/banner_none.jpg";
                    document.getElementById('div_Preview_ID').style.display = 'none';
                }


                document.getElementById('Status').value = data.Status;

                //const $select = document.querySelector('#Status');
                //$select.value = data.Status;

                $("#FormTitle").text("Modify Banner");
                $("#divInput").show();
                document.getElementById('nav_ID').click();
                document.getElementById("fu_Filename_ID").focus();

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
    document.getElementById('fu_Filename_ID').value = '';
    document.getElementById('Title_ID').value = '';
    document.getElementById('SubTitle_ID').value = '';
    //document.getElementById('LabelTombol_ID').value = '';
    //document.getElementById('Link_ID').value = '';
    document.getElementById("Preview_ID").removeAttribute("src");
    document.getElementById("Preview_ID").setAttribute("src", "/image/banner_none.jpg");
    document.getElementById('div_Preview_ID').style.display = 'none';

    document.getElementById('fu_Filename_EN').value = '';
    document.getElementById('Title_EN').value = '';
    document.getElementById('SubTitle_EN').value = '';
    //document.getElementById('LabelTombol_EN').value = '';
    //document.getElementById('Link_EN').value = '';
    document.getElementById("Preview_EN").removeAttribute("src");
    document.getElementById("Preview_EN").setAttribute("src", "/image/banner_none.jpg");
    document.getElementById('div_Preview_EN').style.display = 'none';

    document.getElementById('Status').value = 'Aktif';
}

$('#FormInput').validate({
    rules: {
        fu_Filename_ID: { AntiXSS: true, AntiHTML: true },
        Title_ID: { AntiXSS: true, AntiHTML: true },
        Title_ID_Color: { AntiXSS: true, AntiHTML: true },
        SubTitle_ID: { AntiXSS: true, AntiHTML: true },
        SubTitle_ID_Color: { AntiXSS: true, AntiHTML: true },
        //LabelTombol_ID: { AntiXSS: true, AntiHTML: true },
        //Link_ID: { AntiXSS: true, AntiHTML: true },

        fu_Filename_EN: { AntiXSS: true, AntiHTML: true },
        Title_EN: { AntiXSS: true, AntiHTML: true },
        Title_EN_Color: { AntiXSS: true, AntiHTML: true },
        SubTitle_EN: { AntiXSS: true, AntiHTML: true },
        SubTitle_EN_Color: { AntiXSS: true, AntiHTML: true },
        //LabelTombol_EN: { AntiXSS: true, AntiHTML: true },
        //Link_EN: { AntiXSS: true, AntiHTML: true },

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

        DataForm.append("Upload_ID", $('#fu_Filename_ID')[0].files[0]);
        DataForm.append('Title_ID', $('#Title_ID').val());
        DataForm.append('Title_ID_Color', $('#Title_ID_Color').val());
        DataForm.append('SubTitle_ID', $('#SubTitle_ID').val());
        DataForm.append('SubTitle_ID_Color', $('#SubTitle_ID_Color').val());
        DataForm.append('LabelTombol_ID', '');
        DataForm.append('Link_ID', '');

        DataForm.append("Upload_EN", $('#fu_Filename_EN')[0].files[0]);
        DataForm.append('Title_EN', $('#Title_EN').val());
        DataForm.append('Title_EN_Color', $('#Title_EN_Color').val());
        DataForm.append('SubTitle_EN', $('#SubTitle_EN').val());
        DataForm.append('SubTitle_EN_Color', $('#SubTitle_EN_Color').val());
        DataForm.append('LabelTombol_EN', '');
        DataForm.append('Link_EN', '');

        DataForm.append('Status', $('#Status').val());

        $.ajax({
            url: VP + 'CMS/SaveBanner',
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

function ValidateFileImage(oInput) {
    var isValid = false;
    if (oInput.type == "image/png" || oInput.type == "image/jpg" || oInput.type == "image/jpeg") {
        isValid = true;
    }
    return isValid;
}

fu_Filename_ID.onchange = evt => {
    const [file] = fu_Filename_ID.files
    if (file) {
        if (ValidateFileImage(file) == false) {
            alert("Image not valid");
            document.getElementById('fu_Filename_ID').value = '';
        }
        else {
            document.getElementById('div_Preview_ID').style.display = 'block';
            Preview_ID.src = URL.createObjectURL(file);
        }
    }
}

fu_Filename_EN.onchange = evt => {
    const [file] = fu_Filename_EN.files
    if (file) {
        if (ValidateFileImage(file) == false) {
            alert("Image not valid");
            document.getElementById('fu_Filename_EN').value = '';
        }
        else {
            document.getElementById('div_Preview_ID').style.display = 'block';
            Preview_EN.src = URL.createObjectURL(file);
        }

    }
}

function SetPreviewImageText_ID() {
    var TitleText = document.getElementById('Title_ID').value;
    var Title_Color = document.getElementById('Title_ID_Color').value;
    if (Title_Color == "Dark")
        Title_Color = "#444"
    else if (Title_Color == "Light")
        Title_Color = "#fff"

    document.getElementById('Preview_Title_ID').innerHTML = '<h2 class="text-nowrap" style="color: ' + Title_Color + '; font-family: "Poppins", sans-serif; line-height: 1.5; font-weight: 600;" >' + TitleText + '</h2>';

    var SubTitleText = document.getElementById('SubTitle_ID').value;
    var SubTitle_Color = document.getElementById('SubTitle_ID_Color').value;
    if (SubTitle_Color == "Dark")
        SubTitle_Color = "#555"
    else if (SubTitle_Color == "Light")
        SubTitle_Color = "#fff"
    document.getElementById('Preview_SubTitle_ID').innerHTML = '<span style="color: ' + SubTitle_Color + '; font-family: "Lato", sans-serif; line-height: 1.5;" >' + SubTitleText + '</span>';
}


function SetPreviewImageText_EN() {
    var TitleText = document.getElementById('Title_EN').value;
    var Title_Color = document.getElementById('Title_EN_Color').value;
    if (Title_Color == "Dark")
        Title_Color = "#444"
    else if (Title_Color == "Light")
        Title_Color = "#fff"

    document.getElementById('Preview_Title_EN').innerHTML = '<h2 class="text-nowrap" style="color: ' + Title_Color + '; font-family: "Poppins", sans-serif; line-height: 1.5; font-weight: 600;" >' + TitleText + '</h2>';

    var SubTitleText = document.getElementById('SubTitle_EN').value;
    var SubTitle_Color = document.getElementById('SubTitle_EN_Color').value;
    if (SubTitle_Color == "Dark")
        SubTitle_Color = "#555"
    else if (SubTitle_Color == "Light")
        SubTitle_Color = "#fff"
    document.getElementById('Preview_SubTitle_EN').innerHTML = '<span style="color: ' + SubTitle_Color + '; font-family: "Lato", sans-serif; line-height: 1.5;" >' + SubTitleText + '</span>';
}