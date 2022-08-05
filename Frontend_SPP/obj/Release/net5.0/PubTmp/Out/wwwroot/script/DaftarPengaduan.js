$(document).ready(function () {
    Bind_Jenis_Pelanggaran();
    sessionStorage.setItem("IDPengaduan", '');
    $('#TableData').DataTable({
        destroy: true,        
        order: [[4, 'desc']],
        columns: [
            {
                data: '', defaultContent: '',
                render: function (data, type, full, meta) {
                    var badge_unread_tanggapan = '';
                    if (full.Unread_Tanggapan_Admin_SPP > 0)
                        badge_unread_tanggapan = '<button type="button" class="btn btn-danger btn-sm" onclick="View_Message(\'' + full.ID + '\');">' + full.Unread_Tanggapan_Admin_SPP +'&nbsp;<i class="fa fa-envelope"></i></button>';
                    return '<td>' +
                                '<div class="flex align-items-center list-user-action text-nowrap">' +                          
                                    '<button type="button" class="btn btn-outline-primary btn-sm mr-1" onclick="View(\'' + full.ID + '\');"><i class="fa fa-eye"></i></button>' + badge_unread_tanggapan + 
                                '</div>' +
                            '</td>';
                }
            },
            { data: "Nomor" },
            {
                data: "NamaTerlapor",
                "mRender": function (data, type, row) {
                    if (data.length > 25) {
                        return '<td>' + data.substring(0, 25) + ' ...</td>';
                    }
                    else
                        return '<td>' + data + '</td>';
                }
            },
           
            { data: "_WaktuKejadian" },
            { data: "_CreatedOn" },
            {
                data: "Status",
                "mRender": function (data, type, row) {
                    var culture = document.getElementById('culture').value;
                    if (culture == 'en') {
                        if (data == 'Terkirim')
                            return 'Sent';
                        else if (data == 'Diproses')
                            return 'Processed';
                        else if (data == 'Selesai')
                            return 'Completed';
                        else if (data == 'Ditolak')
                            return 'Declined';
                        else
                            return data;
                    }
                    else 
                        return data;
                }
            },
        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            RefreshTable();
        }
    });

});

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

function View(ID) {
    var culture = document.getElementById('culture').value;
    sessionStorage.setItem("IDPengaduan", ID);
    window.location = '../Pengaduan/PengaduanForm?culture=' + culture;      
}

function View_Message(ID) {
    var culture = document.getElementById('culture').value;
    sessionStorage.setItem("IDPengaduan", ID);
    window.location = '../Pengaduan/PengaduanForm?v=msg&culture=' + culture;
}

function RefreshTable() {
    var _Status = $('#ddl_Status').val();

    $.ajax({
        url: VP + 'Pengaduan/GetPengaduanByEmail',
        data: {
            Status : _Status
        },
        type: 'POST',
        //contentType: false,
        //processData: false,
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
    RefreshTable();
}