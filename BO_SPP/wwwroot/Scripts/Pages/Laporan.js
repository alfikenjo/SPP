$(document).ready(function () {
    LoadData();
})

function LoadData() {
    var Start = $('#Start').val();
    var End = $('#End').val();

    Get_Laporan_1();
    Get_Laporan_2();
}

function Get_Laporan_1() {
    var Start = $('#Start').val();
    var End = $('#End').val();

    $.ajax({
        url: VP + 'Laporan/Get_Laporan_1',
        type: 'POST',
        data: {
            Start: Start,
            End: End
        },
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];

                document.getElementById('Semua').innerText = data.Semua;
                document.getElementById('Terkirim').innerText = data.Terkirim;
                document.getElementById('Ditolak_Admin_SPP').innerText = data.Ditolak_Admin_SPP;
                document.getElementById('Diproses').innerText = data.Diproses;
                document.getElementById('Ditolak_Delegator').innerText = data.Ditolak_Delegator;
                document.getElementById('Dihentikan').innerText = data.Dihentikan;
                document.getElementById('Ditindak_lanjut').innerText = data.Ditindak_lanjut;
                document.getElementById('Selesai').innerText = data.Selesai;

            } else {
                CustomNotif("error|Oops|" + Result.Message + "");
            }
        },
        error: function (xhr, status, error) {
            CustomNotif("error|Oops|" + error + "");
        }
    })
}

function Get_Laporan_2() {
    $('#TableData').DataTable({
        destroy: true,
        order: [2, 'desc'],
        columns: [
            {
                data: 'Delegator', defaultContent: '', sWidth: '30%',
                render: function (data, type, full, meta) {
                    if (data != null) {
                        if (data.length > 40)
                            return '<span style="cursor: pointer" title="' + data.replace(/</g, "&lt;").replace(/>/g, "&gt;") + '">' + data.substring(0, 40).replace(/</g, "&lt;").replace(/>/g, "&gt;") + '...</span>';
                        else
                            return data.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                    }
                    else return null
                }
            },
            {
                data: 'Progress', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data == 100)
                        return '<div class="progress"><div class="progress-bar bg-success" role="progressbar" style="width: ' + data + '%;" aria-valuenow="' + data + '" aria-valuemin="0" aria-valuemax="100">' + data + '%</div></div>';
                    if (data >= 75)
                        return '<div class="progress"><div class="progress-bar bg-info" role="progressbar" style="width: ' + data + '%;" aria-valuenow="' + data + '" aria-valuemin="0" aria-valuemax="100">' + data + '%</div></div>';
                    if (data >= 50)
                        return '<div class="progress"><div class="progress-bar" role="progressbar" style="width: ' + data + '%;" aria-valuenow="' + data + '" aria-valuemin="0" aria-valuemax="100">' + data + '%</div></div>';
                    if (data == 0)
                        return '<div class="progress"><div class="progress-bar bg-danger" role="progressbar" style="width: 100%;" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100">0%</div></div>';
                    else
                        return '<div class="progress"><div class="progress-bar bg-warning" role="progressbar" style="width: ' + data + '%;" aria-valuenow="' + data + '" aria-valuemin="0" aria-valuemax="100">' + data + '%</div></div>';
                }
            },
            { data: 'Masuk', defaultContent: "", sWidth: '5%' },
            { data: 'Proses', defaultContent: "", sWidth: '5%' },
            { data: 'Selesai', defaultContent: "", sWidth: '5%' },
        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            Refresh_Laporan_2();
        }
    });
}

function Refresh_Laporan_2() {
    var Start = $('#Start').val();
    var End = $('#End').val();

    $.ajax({
        url: VP + 'Laporan/Get_Laporan_2',
        data: {
            Start: Start,
            End: End
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

let StartDate = window.document.getElementById("Start");
let OldStartDate = StartDate.value;
let isStartChanged = function () {
    if (StartDate.value !== OldStartDate) {
        OldStartDate = StartDate.value;
        return true;
    };
    return false;
};
StartDate.addEventListener("blur", function () {
    if (isStartChanged())
        LoadData();
});


let EndDate = window.document.getElementById("End");
let OldEndDate = EndDate.value;
let isEndChanged = function () {
    if (EndDate.value !== OldEndDate) {
        OldEndDate = EndDate.value;
        return true;
    };
    return false;
};
EndDate.addEventListener("blur", function () {
    if (isEndChanged())
        LoadData();
});
