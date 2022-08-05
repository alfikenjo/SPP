$(document).ready(function () {
    SetRole();
    Bind_Jenis_Pelanggaran();
    sessionStorage.setItem("IDPengaduan", '');
    var Role = sessionStorage.getItem("Role");

    if (Role.includes('Admin SPP'))
        document.getElementById('div_add_pengaduan').style.display = 'block';

    $('#TableData').DataTable({
        destroy: true,
        order: [[5, 'desc']],
        columns: [
            {
                data: '', defaultContent: '',
                render: function (data, type, full, meta) {
                    var badge_unread_tanggapan = '';
                    var badge_unread_tanggapan_internal = '';

                    if (Role.includes('Admin SPP')) {
                        if (full.Unread_Tanggapan_Pelapor > 0)
                            badge_unread_tanggapan = '<button type="button" title="Pesan dari Pelapor" class="btn btn-danger btn-sm mr-1" onclick="View_Message_Pelapor(\'' + full.ID + '\');">' + full.Unread_Tanggapan_Pelapor + '&nbsp;<i class="fa fa-envelope"></i></button>';

                        if (full.Unread_Tanggapan_Internal_Delegator > 0)
                            badge_unread_tanggapan_internal = '<button type="button" title="Pesan dari Delegator" class="btn btn-warning btn-sm mr-1" onclick="View_Message_Internal(\'' + full.ID + '\');">' + full.Unread_Tanggapan_Internal_Delegator + '&nbsp;<i class="fa fa-envelope"></i></button>';

                        if (full.Status == 'Terkirim' || full.Status == 'Ditolak Delegator' || full.Status == 'Dihentikan' || full.Status == 'Ditindak lanjut') {
                            return '<td>' +
                                '<div class="flex align-items-center list-user-action text-nowrap">' +
                                '<button type="button" class="btn btn-outline-danger btn-sm mr-1" onclick="View(\'' + full.ID + '\');">Proses</button>' + badge_unread_tanggapan + badge_unread_tanggapan_internal +
                                '</div>' +
                                '</td>';
                        } if (full.Status == 'Selesai') {
                            return '<td>' +
                                '<div class="flex align-items-center list-user-action text-nowrap">' +
                                '<button type="button" class="btn btn-outline-success btn-sm mr-1" onclick="View(\'' + full.ID + '\');">View</button>' + badge_unread_tanggapan + badge_unread_tanggapan_internal +
                                '</div>' +
                                '</td>';
                        } else {
                            return '<td>' +
                                '<div class="flex align-items-center list-user-action text-nowrap">' +
                                '<button type="button" class="btn btn-outline-primary btn-sm mr-1" onclick="View(\'' + full.ID + '\');">View</button>' + badge_unread_tanggapan + badge_unread_tanggapan_internal +
                                '</div>' +
                                '</td>';
                        }
                    }
                    else if (Role.includes('Delegator')) {
                        if (full.Unread_Tanggapan_Internal_Admin_SPP > 0)
                            badge_unread_tanggapan_internal = '<button type="button" title="Pesan dari Admin SPP" class="btn btn-warning btn-sm mr-1" onclick="View_Message_Internal(\'' + full.ID + '\');">' + full.Unread_Tanggapan_Internal_Admin_SPP + '&nbsp;<i class="fa fa-envelope"></i></button>';

                        if (full.Status == 'Diproses') {
                            return '<td>' +
                                '<div class="flex align-items-center list-user-action text-nowrap">' +
                                '<button type="button" class="btn btn-outline-danger btn-sm mr-1" onclick="View(\'' + full.ID + '\');">Tindak lanjuti</button>' + badge_unread_tanggapan_internal +
                                '</div>' +
                                '</td>';
                        } else if (full.Status == 'Selesai' || full.Status == 'Ditolak Admin SPP') {
                            return '<td>' +
                                '<div class="flex align-items-center list-user-action text-nowrap">' +
                                '<button type="button" class="btn btn-outline-success btn-sm mr-1" onclick="View(\'' + full.ID + '\');">View</button>' + badge_unread_tanggapan_internal +
                                '</div>' +
                                '</td>';
                        } else {
                            return '<td>' +
                                '<div class="flex align-items-center list-user-action text-nowrap">' +
                                '<button type="button" class="btn btn-outline-primary btn-sm mr-1" onclick="View(\'' + full.ID + '\');">View</button>' + badge_unread_tanggapan_internal +
                                '</div>' +
                                '</td>';
                        }
                    }
                    else {
                        return '';
                    }

                }
            },
            {
                data: "Status",
                "mRender": function (data, type, row) {
                    if (data == 'Tindak Lanjut')
                        return 'Menunggu Tindak Lanjut';
                    if (data == 'Respon Terkirim')
                        return 'Selesai';
                    else
                        return data;
                }
            },
            {
                data: 'Nomor', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null

                }
            },
            {
                data: "DelegatorName",
                "mRender": function (data, type, row) {
                    if (data.length > 25) {
                        return '<td>' + data.replace(/</g, "&lt;").replace(/>/g, "&gt;").substring(0, 25) + ' ...</td>';
                    }
                    else
                        return '<td>' + data.replace(/</g, "&lt;").replace(/>/g, "&gt;") + '</td>';
                }
            },
            {
                data: 'Email', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return 'Not Available'

                }
            },
            { data: "_CreatedOn" },
            {
                data: 'Jenis_Pelanggaran', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null

                }
            },

        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            var url_string = window.location.href;
            var url = new URL(url_string);
            var filter = url.searchParams.get("f");
            if (filter != null) {                
                document.getElementById('ddl_Status').value = filter;
            }

            var filter_Jenis_Pelanggaran = url.searchParams.get("j");
            if (filter_Jenis_Pelanggaran != null) {                
                document.getElementById('Jenis_Pelanggaran').value = filter_Jenis_Pelanggaran;
            }

            RefreshTable();
        },
        createdRow: function (row, data, dataIndex) {
            if (Role.includes('Admin SPP') && (data.Status == 'Terkirim' || data.Status == 'Dihentikan' || data.Status == 'Ditindak lanjut' || data.Status == 'Ditolak Delegator')) {
                $(row).addClass('text-danger');
            }
        }
    });

});

function View(ID) {
    sessionStorage.setItem("IDPengaduan", ID);
    window.location = '../Pengaduan/PengaduanForm';
}

function View_Message_Pelapor(ID) {
    sessionStorage.setItem("IDPengaduan", ID);
    window.location = '../Pengaduan/PengaduanForm?v=eks';
}

function View_Message_Internal(ID) {
    sessionStorage.setItem("IDPengaduan", ID);
    window.location = '../Pengaduan/PengaduanForm?v=int';
}

function RefreshTable() {
    var _Status = $('#ddl_Status').val();
    var Jenis_Pelanggaran = $('#Jenis_Pelanggaran').val();

    

    $.ajax({
        url: VP + 'Pengaduan/GetPengaduanByEmail',
        data: {
            Status: _Status,
            Jenis_Pelanggaran: Jenis_Pelanggaran
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

function btn_View_Click() {
    RefreshTable();
}

function btn_Reset_Click() {
    $('#ddl_Status').val('');
    $('#Jenis_Pelanggaran').val('');
    RefreshTable();
}

function Bind_Jenis_Pelanggaran() {
    $("#Jenis_Pelanggaran").empty();
    $("#Jenis_Pelanggaran").append($("<option></option>").val("").html("Semua"));
    $.ajax({
        type: "POST",
        url: VP + 'Pengaduan/Get_Jenis_Pelanggaran',
        dataType: "json", contentType: "application/json",
        success: function (res) {
            $.each(res.Message, function (data, value) {
                var Value = value.GridTitle;
                $("#Jenis_Pelanggaran").append($("<option></option>").val(Value).html(Value));
            })
        }

    });
}