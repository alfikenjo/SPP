$(document).ready(function () {
    $('#TableData').DataTable({
        destroy: true,
        order: [[0, 'desc']],
        columns: [            
            { data: "Datetime" },
            { data: "Username" },            
            { data: "Menu" },
            { data: "Halaman" },           
            { data: "Action" },           
        ],
        fnInitComplete: function () {
            RefreshTable();
        }
    });     
});

function RefreshTable() {    

    $.ajax({
        url: VP + 'AuditTrail/GetAuditTrail',         
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