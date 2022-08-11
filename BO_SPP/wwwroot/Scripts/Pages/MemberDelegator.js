$(document).ready(function () {

    var ID = sessionStorage.getItem("DelegatorID");
    if (ID) {
        $.ajax({
            url: VP + 'Delegator/Get_DelegatorByID',
            type: 'POST',
            data: {
                ID: ID
            },
            success: function (Result) {
                if (Result.Error == false) {
                    var data = Result.Message[0];
                    document.getElementById('DelegatorName').innerText = data.Name.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                } else {
                    CustomNotif("error|Oops|" + Result.Message + "");
                }
            },
            error: function (xhr, status, error) {
                CustomNotif("error|Oops|" + error + "");
            }
        })
    }

    $('#TableData').DataTable({
        destroy: true,
        //"scrollX": true,
        columns: [

            {
                data: "Img", 'sWidth': '30px',
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
                data: 'Status', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null

                }
            },
            {
                data: '', defaultContent: '',
                render: function (data, type, full, meta) {
                    return '<td>' +
                        '<div class="flex align-items-center list-user-action text-nowrap">' +
                        '<button type="button" class="btn btn-outline-danger btn-sm" onclick="Delete(\'' + full.ID + '\');"><i class="fa fa-trash"></i></button>' +
                        '</div>' +
                        '</td>';

                }
            },

        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            if (ID)
                RefreshTable(ID);
        }
    });


});

function RefreshTable(DelegatorID) {
    $.ajax({
        url: VP + 'Delegator/Get_MemberDelegator',
        type: 'POST',
        data: {
            DelegatorID: DelegatorID
        },
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
    resetFormEdit();
    $("#divInput").show();
    document.getElementById("txtEmail").focus();
});

function CloseForm() {
    resetFormEdit();
    $("#divInput").hide();
};

function resetFormEdit() {
    document.getElementById("txtFullname").value = "";
    document.getElementById("txtEmail").value = "";
    document.getElementById("txt_Jabatan").value = "";
    document.getElementById("txt_Divisi").value = "";
    document.getElementById("txtMobile").value = "";
    document.getElementById("txtAddress").value = "";
}

function GetLDAPAccByEmailByDelegatorID() {
    var DelegatorID = sessionStorage.getItem("DelegatorID");
    var email = $('#txtEmail').val();
    if (email != '') {

        $.ajax({
            url: VP + 'Account/GetLDAPAccByEmailByDelegatorID',
            type: 'POST',
            data: {
                email: email,
                DelegatorID: DelegatorID,
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
                        CustomNotif("success|User found|Silahkan klik tombol Invite untuk melanjutkan|document.getElementById('txtFullname').focus();");
                    }
                    else {
                        CustomNotif("error|AD Result|Email not found");
                    }
                }
                else if (Result.Message == 'Already Registered') {
                    CustomNotif("error|Oops|Email ini sudah terdaftar sebagai member pada Grup Delegator ini");
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

function Delete(ID) {
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
                url: VP + 'Delegator/SaveMemberDelegator',
                data: DataForm,
                type: 'POST',
                contentType: false,
                processData: false,
                success: function (Result) {
                    if (Result.Error == false) {
                        var DelegatorID = sessionStorage.getItem("DelegatorID");
                        RefreshTable(DelegatorID);
                        CustomNotif('success|Saved|Data berhasil dihapus');
                    } else {
                        CustomNotif("error|Oops|" + Result.Message + "");
                    }
                },
                error: function (xhr, status, error) {
                    CustomNotif("error|Oops|" + error + "");
                }
            });
        }
    })

}

$('#FormInput').validate({
    rules: {
        txtFullname: { required: true, AntiXSS: true, AntiHTML: true },
        txtEmail: { required: true, AntiXSS: true, AntiHTML: true },
        txtMobile: { AntiXSS: true, AntiHTML: true },
        txtAddress: { AntiXSS: true, AntiHTML: true },
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
        DataForm.append('Fullname', $('#txtFullname').val());
        DataForm.append('Email', $('#txtEmail').val());
        DataForm.append('Mobile', $('#txtMobile').val());
        DataForm.append('Address', $('#txtAddress').val());
        DataForm.append('Jabatan', $('#txt_Jabatan').val());
        DataForm.append('Divisi', $('#txt_Divisi').val());

        var ID = sessionStorage.getItem("DelegatorID");
        DataForm.append('DelegatorID', ID);

        $.ajax({
            url: VP + 'Account/InviteMemberDelegator',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    CloseForm();
                    if (Result.Message === 1)
                        CustomNotif('success|Invited|Member Grup Delegator berhasil ditambahkan dan email notifikasi terkirim|window.location.href = "../Delegator/MemberDelegator"');
                    else
                        CustomNotif('success|Saved|Member Grup Delegator berhasil ditambahkan|window.location.href = "../Delegator/MemberDelegator"');
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