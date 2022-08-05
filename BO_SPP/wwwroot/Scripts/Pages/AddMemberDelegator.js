$(document).ready(function () {

    var DelegatorID = sessionStorage.getItem("DelegatorID");
    if (DelegatorID) {
        $.ajax({
            url: VP + 'Delegator/Get_DelegatorByID',
            type: 'POST',
            data: {
                ID: DelegatorID
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
        columns: [
            {
                data: '', defaultContent: '',
                render: function (data, type, full, meta) {
                    return '<td>' +
                        '<div class="flex align-items-center list-user-action text-nowrap">' +
                        '<button type="button" class="btn btn-outline-success btn-sm" onclick="Pilih(\'' + full.UserID + '\', \'' + full.Fullname.replace(/</g, "&lt;").replace(/>/g, "&gt;") + '\');"><i class="fa fa-check mr-1"></i>&nbsp;Pilih</button>' +
                        '</div>' +
                        '</td>';

                }
            },
            {
                data: "Img", 'sWidth' : '30px',
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
                data: 'Delegators', defaultContent: '',
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

        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            if (DelegatorID)
                RefreshTable(DelegatorID);
        }
    });


});

function RefreshTable(DelegatorID) {
    $.ajax({
        url: VP + 'Delegator/Get_CalonMemberDelegator',
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

$("#btnCancel").click(function () {
    window.location.href = "../Delegator/MemberDelegator";
});

$("#btnSync").click(function () {
    CustomNotif("error|Oops|Failed connect to Active Directory..");
});

function Pilih(UserID, Fullname) {
    var DataForm = new FormData();
    var DelegatorID = sessionStorage.getItem("DelegatorID");
    DataForm.append('UserID', UserID);
    DataForm.append('DelegatorID', DelegatorID);
    DataForm.append('Action', 'add');

    Swal.fire({
        title: 'Daftarkan ' + Fullname +' ke dalam Grup Delegator ini?',
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: 'Ya',
        denyButtonText: 'Batal',
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
                        RefreshTable(DelegatorID);

                        if (Result.Message === 1)
                            CustomNotif('success|Saved|Data berhasil ditambahkan dan email notifikasi terkirim|window.location.href = "../Delegator/MemberDelegator"');
                        else
                            CustomNotif('success|Saved|Data berhasil ditambahkan|window.location.href = "../Delegator/MemberDelegator"');

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