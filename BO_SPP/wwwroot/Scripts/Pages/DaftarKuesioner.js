$(document).ready(function () {
    sessionStorage.setItem("IDKuesioner", '');

    $('#TableData').DataTable({
        destroy: true,
        order: [[5, 'desc']],
        columns: [
            {
                data: '', defaultContent: '',
                render: function (data, type, full, meta) {
                    return '<td>' +
                        '<div class="flex align-items-center list-user-action text-nowrap">' +
                        '<button type="button" class="btn btn-outline-info btn-sm mr-1" onclick="View(\'' + full.ID + '\');"><i class="fa fa-eye"></i></button>' +
                        '<button type="button" class="btn btn-outline-primary btn-sm mr-1" onclick="Edit(\'' + full.ID + '\');"><i class="fa fa-edit"></i></button>' +
                        '<button type="button" class="btn btn-outline-danger btn-sm" onclick="Delete(\'' + full.ID + '\');"><i class="fa fa-trash"></i></button>' +
                        '</div>' +
                        '</td>';
                }
            },
            {
                data: 'Title', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) {
                        if (data.length > 50)
                            return '<span style="cursor: pointer" title="' + data.replace(/</g, "&lt;").replace(/>/g, "&gt;") + '">' + data.substring(0, 50).replace(/</g, "&lt;").replace(/>/g, "&gt;") + '...</span>';
                        else
                            return data.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                    }

                    else return null

                }
            },
            { data: "Status" },
            { data: "StartDate" },
            { data: "EndDate" },
            { data: "UpdatedOn" },
            { data: "UpdatedBy" }

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

function View(ID) {
    sessionStorage.setItem("IDKuesioner", ID);
    window.location = '../Kuesioner/FormBuilder?act=v';
}

function Edit(ID) {
    sessionStorage.setItem("IDKuesioner", ID);
    window.location = '../Kuesioner/FormBuilder?act=e';
}

function Delete(ID, Title) {
    var DataForm = new FormData();
    DataForm.append('ID', ID);
    DataForm.append('Title', Title);
    DataForm.append('Action', 'hapus');

    Swal.fire({
        title: 'Apakah anda yakin akan menghapus kuesioner ini?',
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
                url: VP + 'Kuesioner/SaveKuesioner',
                data: DataForm,
                type: 'POST',
                contentType: false,
                processData: false,
                success: function (Result) {
                    if (Result.Error == false) {
                        RefreshTable();
                        CustomNotif('success|Deleted|Kuesioner berhasil dihapus');
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

function RefreshTable() {
    
    $.ajax({
        url: VP + 'Kuesioner/Get_Kuesioner',       
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
