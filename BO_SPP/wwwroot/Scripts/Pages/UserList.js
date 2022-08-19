$(document).ready(function () {
    $('#TableData').DataTable({
        destroy: true,
        "scrollX": true,
        columns: [
            {
                data: '', defaultContent: '',
                render: function (data, type, full, meta) {
                    return '<td>' +
                        '<div class="flex align-items-center list-user-action text-nowrap">' +
                        '<button type="button" class="btn btn-outline-primary btn-sm mr-1" onclick="Edit(\'' + full.UserID + '\');"><i class="fa fa-edit"></i></button>' +
                        '</div>' +
                        '</td>';

                }
            },
            {
                data: "Img",
                "mRender": function (data, type, row) {
                    if (data != null && data != '')
                        return '<td class="text-center"><img class="rounded img-fluid avatar-40" src="' + data + '" ></td>';
                    else
                        return '<td class="text-center"><img class="rounded img-fluid avatar-40" src="/image/default_avatar.png" ></td>';
                }
            },
            {
                data: 'Fullname', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null

                }
            },
            {
                data: 'Email', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null

                }
            },
            {
                data: 'Mobile', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null

                }
            },
            {
                data: 'Divisi', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null
                }
            },
            {
                data: 'Jabatan', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null
                }
            },
            {
                data: 'Roles', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null
                }
            },
            {
                data: 'Delegators', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) {
                        var Delegators = data.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                        if (data.length > 30)
                            Delegators = '<span title="' + Delegators + '">' + data.replace(/</g, "&lt;").replace(/>/g, "&gt;").substring(0, 30) + '...</span>';
                        return Delegators;
                    }

                    else
                        return null

                }
            },
            { data: "s_UpdatedOn" },
            { data: "UpdatedBy" },
            { data: "Status" }

        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            RefreshTable();
        },
        createdRow: function (row, data, dataIndex) {
            if (data.Status == 'Non-Aktif') {
                $(row).addClass('text-danger');
            }
        }

    });

    Bind_ddl_Role();
    Bind_ddl_Delegator();
    Bind_ddlRole();

});

function Bind_ddl_Role() {
    $("#ddl_Role").empty();
    $("#ddl_Role").append($("<option></option>").val("").html("- Select -"));
    $.ajax({
        type: "POST",
        url: VP + 'Account/Get_ddl_Role',
        dataType: "json", contentType: "application/json",
        success: function (res) {
            $.each(res.Message, function (data, value) {
                var ID = value.Name;
                var Value = value.Name;
                $("#ddl_Role").append($("<option></option>").val(ID).html(Value));
            })
        }

    });
}

function Bind_ddlRole() {
    $("#ddlRole").empty();
    $("#ddlRole").append($("<option></option>").val("").html("- Select -"));
    $.ajax({
        type: "POST",
        url: VP + 'Account/Get_ddl_Role',
        dataType: "json", contentType: "application/json",
        success: function (res) {
            $.each(res.Message, function (data, value) {
                var ID = value.ID;
                var Value = value.Name;
                $("#ddlRole").append($("<option></option>").val(ID).html(Value));
            })
        }

    });
}

function Bind_ddl_Delegator() {
    $("#ddl_Delegator").empty();
    $("#ddl_Delegator").append($("<option></option>").val("").html("- Select -"));
    $.ajax({
        type: "POST",
        url: VP + 'Delegator/Get_ddl_Delegator',
        dataType: "json", contentType: "application/json",
        success: function (res) {
            $.each(res.Message, function (data, value) {
                var ID = value.ID;
                var Value = value.Name;
                $("#ddl_Delegator").append($("<option></option>").val(ID).html(Value));
            })
        }

    });
}

function RefreshTable() {
    CloseForm();
    var DataForm = new FormData();
    DataForm.append('isActive', $('#ddl_isActive').val());
    DataForm.append('Roles', $('#ddl_Role').val());

    var _isActive = $('#ddl_isActive').val();
    var _Roles = $('#ddl_Role').val();
    var _Delegator = $('#ddl_Delegator').val();
    if (_Delegator == null)
        _Delegator = '';

    $.ajax({
        url: VP + 'Account/GetUser',
        data: {
            isActive: _isActive,
            Roles: _Roles,
            Delegator: _Delegator
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
    $("#Action").val("add");
    $("#FormTitle").text("Add User");
    resetFormEdit();
    $("#divInput").show();
    document.getElementById("txtEmail").readOnly = false;
    document.getElementById("txtEmail").focus();
});

function CloseForm() {
    resetFormEdit();
    $("#divInput").hide();
};

function Edit(ID) {
    $("#Action").val("edit");
    resetFormEdit();
    FillForm(ID);
    document.getElementById("txtEmail").readOnly = true;
}

function Delete(ID) {
    resetFormEdit();
    $("#divInput").hide();

    var DataForm = new FormData();
    DataForm.append('UserID', ID);
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
                url: VP + 'Account/SaveUser',
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

function FillForm(ID) {

    $.ajax({
        url: VP + 'Account/GetUserByID',
        type: 'POST',
        data: {
            UserID: ID
        },
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                document.getElementById('UserID').value = ID;
                document.getElementById('txtFullname').value = data.Fullname;
                document.getElementById('txtEmail').value = data.Email;
                document.getElementById('txtMobile').value = data.Mobile;
                document.getElementById('txtAddress').value = data.Address;
                document.getElementById('ddlGender').value = data.Gender;
                document.getElementById('txt_NIP').value = data.NIP;
                document.getElementById('txt_Jabatan').value = data.Jabatan;
                document.getElementById('txt_Divisi').value = data.Divisi;
                //console.log(data.ID_Roles);
                if (data.ID_Roles != null) {
                    var IDRoles = data.ID_Roles.toLowerCase();
                    //console.log('IDRoles :' + IDRoles);
                    var a = IDRoles.split(";"), i;

                    var selectedValuesTest = [];
                    for (i = 0; i < a.length; i++) {
                        if (a[i].trim().length == 36) {
                            selectedValuesTest.push(a[i].trim().toUpperCase());
                        }
                    }
                    //console.log(selectedValuesTest);
                    //let role = selectedValuesTest[0];
                    //console.log(role);
                    $('#ddlRole').val(selectedValuesTest).trigger('change');
                }

                document.getElementById('ddlIsActive').value = data.isActive;

                if (data.Img) {
                    var path = data.Img;
                    document.getElementById('imgempPicture').src = path;
                }
                else
                    document.getElementById('imgempPicture').src = "/image/default_avatar.png";


                $("#FormTitle").text("Modify User");
                $("#divInput").show();
                document.getElementById("txtFullname").focus();

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
    document.getElementById('hfRow').value = "";
    document.getElementById("txtFullname").value = "";
    document.getElementById("txtEmail").value = "";
    document.getElementById("txt_NIP").value = "";
    document.getElementById("txt_Jabatan").value = "";
    document.getElementById("txt_Divisi").value = "";
    document.getElementById("txtMobile").value = "";
    document.getElementById("ddlGender").value = "";
    document.getElementById("txtAddress").value = "";
    document.getElementById("ddlIsActive").value = "1";
    document.getElementById("imgempPicture").removeAttribute("src");
    document.getElementById("imgempPicture").setAttribute("src", "/image/default_avatar.png");
    $("#ddlRole").val([]).change();
}

$('#FormInput').validate({
    rules: {
        txtFullname: { AntiXSS: true, AntiHTML: true },
        txtEmail: { required: true, AntiXSS: true, AntiHTML: true },
        txtMobile: { AntiXSS: true, AntiHTML: true },
        ddlGender: { AntiXSS: true, AntiHTML: true },
        txtAddress: { AntiXSS: true, AntiHTML: true },
        txt_NIP: { AntiXSS: true, AntiHTML: true },
        txt_Jabatan: { AntiXSS: true, AntiHTML: true },
        txt_Divisi: { AntiXSS: true, AntiHTML: true },
        ddlRole: { required: true, AntiXSS: true, AntiHTML: true },
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
        DataForm.append('UserID', $('#UserID').val());
        DataForm.append('Action', $('#Action').val());
        DataForm.append('enc_Fullname', $('#txtFullname').val());
        DataForm.append('enc_Email', $('#txtEmail').val());
        DataForm.append('enc_Mobile', $('#txtMobile').val());
        DataForm.append('enc_Address', $('#txtAddress').val());
        DataForm.append('enc_Gender', $('#ddlGender').val());
        DataForm.append('enc_NIP', $('#txt_NIP').val());
        DataForm.append('enc_Jabatan', $('#txt_Jabatan').val());
        DataForm.append('enc_Divisi', $('#txt_Divisi').val());
        DataForm.append('ID_Roles', $('#ddlRole').val());
        DataForm.append("Foto", $('#fuempPicture')[0].files[0]);
        DataForm.append('isActive', $('#ddlIsActive').val());

        $.ajax({
            url: VP + 'Account/SaveUser',
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

fuempPicture.onchange = evt => {

    const [file] = fuempPicture.files
    if (file) {
        if (ValidateFileImage(file))
            imgempPicture.src = URL.createObjectURL(file)
        else {
            alert("Image not valid");
            document.getElementById('fuempPicture').value = '';
            imgempPicture.src = '';
        }
    }
}

function ValidateFileImage(oInput) {
    var isValid = false;
    if (oInput.type == "image/png" || oInput.type == "image/jpg" || oInput.type == "image/jpeg") {
        isValid = true;
    }
    return isValid;
}

function GetLDAPAccByEmail() {
    var email = $('#txtEmail').val();
    if (email != '') {

        $.ajax({
            url: VP + 'Account/GetLDAPAccByEmail',
            type: 'POST',
            data: {
                email: email
            },
            success: function (Result) {

                if (Result.Error == false) {
                    if (Result.Message.length > 0) {
                        var data = Result.Message[0];
                        document.getElementById('txtFullname').value = data.Name;
                        document.getElementById('txtEmail').value = data.Email;
                        document.getElementById('txtMobile').value = data.Mobile;
                        document.getElementById('txtAddress').value = data.Location;
                        document.getElementById('txt_Jabatan').value = data.Title;
                        document.getElementById('txt_Divisi').value = data.Department;
                        CustomNotif("success|User found|Mohon melengkapi Role dan informasi lainnya|document.getElementById('txtFullname').focus();");
                    }
                    else {
                        CustomNotif("error|AD Result|Email not found");
                    }
                }
                else if (Result.Message == 'Already Registered') {
                    CustomNotif("error|Oops|Email ini sudah terdaftar pada Back Office SPP");
                }
                else if (Result.Message == 'Already Registered Pelapor') {
                    CustomNotif("error|Oops|Email ini sudah terdaftar sebagai pelapor pada Portal SPP, Anda perlu melakukan Edit & menghapus Role [Pelapor] dari Akun tersebut jika dibutuhkan");
                }
                else {
                    CustomNotif("error|Oops|" + Result.Message + "");
                }

            },
            error: function (xhr, status, error) {
                CustomNotif("error|Oops|" + error + "");
            }
        })

    }
    else {
        CustomNotif("error|Oops|Email not found");
    }
}

var txtMobile = document.querySelector("#txtMobile");
window.intlTelInput(txtMobile, {
    hiddenInput: "full_number",
    separateDialCode: true,
    utilsScript: "/js/telephone/utils.js",
});