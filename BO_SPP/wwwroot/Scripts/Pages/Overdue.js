$(document).ready(function () {
    LoadData();
})

function LoadData() {
    Get_Overdue_14();
    Get_Overdue_90();
}

function Get_Overdue_90() {
    $('#TableData90').DataTable({
        destroy: true,
        order: [4, 'desc'],
        columns: [
            {
                data: '', defaultContent: '', sWidth: '10%',
                render: function (data, type, full, meta) {
                    return '<td>' +
                        '<div class="flex align-items-center list-user-action text-nowrap">' +
                        '<button type="button" class="btn btn-outline-danger btn-sm mr-1" onclick="View(\'' + full.ID + '\');">Open</button>' +
                        '</div>' +
                        '</td>';
                }
            },
            { data: 'Nomor', defaultContent: "" },
            { data: 'Tanggal_Kirim', defaultContent: "" },
            { data: 'DelegatorName', defaultContent: "" },
            { data: 'Status', defaultContent: "" },
            { data: 'Overdue', defaultContent: "" },
        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            Refresh_Overdue_90();
        }
    });
}

function Refresh_Overdue_90() {
    $.ajax({
        url: VP + 'Laporan/Get_Overdue_90',
        type: 'POST',
        success: function (Result) {
            if (Result.Error == false) {
                var table = $('#TableData90').DataTable();
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

function Get_Overdue_14() {
    $('#TableData14').DataTable({
        destroy: true,
        order: [4, 'desc'],
        columns: [
            {
                data: '', defaultContent: '', sWidth: '10%',
                render: function (data, type, full, meta) {
                    return '<td>' +
                        '<div class="flex align-items-center list-user-action text-nowrap">' +
                        '<button type="button" class="btn btn-outline-danger btn-sm mr-1" onclick="View(\'' + full.ID + '\');">Open</button>' +
                        '</div>' +
                        '</td>';
                }
            },
            { data: 'Nomor', defaultContent: "" },
            { data: 'Tanggal_Kirim', defaultContent: "" },
            { data: 'Status', defaultContent: "" },
            { data: 'Overdue', defaultContent: "" },
        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            Refresh_Overdue_14();
        }
    });
}

function Refresh_Overdue_14() {
    $.ajax({
        url: VP + 'Laporan/Get_Overdue_14',
        type: 'POST',
        success: function (Result) {
            if (Result.Error == false) {
                var table = $('#TableData14').DataTable();
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


function View(ID) {
    sessionStorage.setItem("IDPengaduan", ID);
    window.location = '../Pengaduan/PengaduanForm';
}