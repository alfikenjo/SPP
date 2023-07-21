$(document).ready(function () {
    Bind_Jenis_Pelanggaran();
    document.getElementById('Action').value = 'add';
    var mb = document.getElementById("hidden_mobile").value; //$('hidden_mobile').val();    
    document.getElementById('txt_Handphone').value = mb;

    var url_string = window.location.href;
    var url = new URL(url_string);
    var paramID = url.searchParams.get("ID");
    if (paramID != null) {

        sessionStorage.setItem("IDPengaduan", '');
        $.ajax({
            url: VP + 'Pengaduan/SetSessionIDPengaduan',
            type: 'POST',
            data: {
                ID: paramID
            },
            success: function (Result) {
                if (Result.Error == false) {
                    var culture = document.getElementById('culture').value;
                    if (Result.Message != '') {
                        sessionStorage.setItem("IDPengaduan", Result.Message);
                        var mode = url.searchParams.get("v");
                        if (mode != null)
                            window.location.href = "/Pengaduan/PengaduanForm?v=msg&culture=" + culture;
                        else
                            window.location.href = "/Pengaduan/PengaduanForm?culture=" + culture;
                    }
                    else
                        CustomNotif('error|Oops|Invalid External Request|window.location.href = "/Pengaduan/DaftarPengaduan?culture=' + culture + '"');
                } else {
                    CustomNotif("error|Oops|" + Result.Message + "");
                }
            },
            error: function (xhr, status, error) {
                CustomNotif("error|Oops|" + error + "");
            }
        })
    }

    var mode = url.searchParams.get("v");
    if (mode != null) {
        document.getElementById('nav_proses').click();
    }

    var ID = sessionStorage.getItem("IDPengaduan");
    if (ID != null && ID != '') {
        FillForm(ID);
        document.getElementById('Action').value = 'view';
    }
    else
        SetKuesioner();

});

function Bind_Jenis_Pelanggaran() {
    $("#Jenis_Pelanggaran").empty();
    $("#Jenis_Pelanggaran").append($("<option></option>").val("").html("- Select -"));
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

function FillForm(ID) {

    document.getElementById('tab_proses').style.display = 'block';
    document.getElementById('form_File_Bukti_Pendukung').style.display = 'none';
    document.getElementById('fu_FileEvidence').style.display = 'none';
    document.getElementById('btn_Kirim').style.display = 'none';
    document.getElementById('div_btnAdd_Terlapor').style.display = 'none';

    document.getElementById('ID_Header').value = ID;
    //document.getElementById('Action').value = "view";

    $.ajax({
        url: VP + 'Pengaduan/GetPengaduanByID',
        type: 'POST',
        data: {
            ID: ID
        },
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];

                if (data.Email)
                    document.getElementById('txt_Email').value = data.Email.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                if (data.Handphone)
                    document.getElementById('txt_Handphone').value = data.Handphone.replace(/</g, "&lt;").replace(/>/g, "&gt;");

                document.getElementById('txt_Handphone').disabled = true;
                FillDaftarTerlapor();

                document.getElementById('txt_TempatKejadian').value = data.TempatKejadian.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                document.getElementById('txt_TempatKejadian').disabled = true;

                document.getElementById('txt_WaktuKejadian').value = data.s_WaktuKejadian;
                if (data.s_WaktuKejadian)
                    document.getElementById('txt_WaktuKejadian').disabled = true;

                document.getElementById('txt_Kronologi').value = data.Kronologi.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                document.getElementById('txt_Kronologi').disabled = true;

                if (data.Jenis_Pelanggaran)
                    document.getElementById('Jenis_Pelanggaran').value = data.Jenis_Pelanggaran.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                document.getElementById('Jenis_Pelanggaran').disabled = true;

                document.getElementById('ddl_Status_Final').value = data.Status;

                var culture = document.getElementById('culture').value;
                if (culture == 'en') {
                    if (data.Status == 'Selesai')
                        document.getElementById('ddl_Status_Final').value = 'Completed';
                    else if (data.Status == 'Ditolak Admin SPP')
                        document.getElementById('ddl_Status_Final').value = 'Declined';
                    else
                        document.getElementById('ddl_Status_Final').value = data.Status;
                }
                document.getElementById('ddl_Status_Final').disabled = true;


                FillKuesionerValue(ID);

                var Status = data.Status;
                var Keterangan_Status = data.Status;

                if (Status == "Terkirim")
                    Keterangan_Status = "Menunggu Respon Petugas";
                else if (Status == "Diproses" || Status == "Ditolak Delegator" || Status == "Dihentikan" || Status == "Ditindak lanjut")
                    Keterangan_Status = "Diproses";
                else if (Status == "Ditolak Admin SPP")
                    Keterangan_Status = "Pengaduan Ditolak";
                else if (Status == "Selesai")
                    Keterangan_Status = "Selesai";


                if (Status == "Selesai" || Status == "Ditolak Admin SPP") {
                    document.getElementById('tab_Respon').style.display = 'block';
                    if (data.ResponByDate != "") {
                        document.getElementById('txt_Keterangan_Respon').value = data.Keterangan_Respon.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                        if (data.ResponByDate)
                            document.getElementById('ResponByDate').textContent = data.ResponByDate;

                        if (data.FilepathFileRespon) {
                            var Ekstension = data.Keterangan_Respon_Ekstension.toLowerCase();
                            var div = document.getElementById('div_fu_Keterangan_Respon_Filename'); div.style.display = 'block';
                            var sb = '<a target="_blank" href="../Home/sef?Filename=' + data.Keterangan_Respon_Filename + '&Extension=' + Ekstension + '" class="btn btn-sm btn-outline-primary" style="cursor: pointer">Attachment</a>';
                            if (Ekstension == '.jpg' || Ekstension == '.png' || Ekstension == '.jpeg')
                                sb = '<a onclick="show_image(\'' + data.FilepathFileRespon + '\', \'Attachment\')" class="btn btn-sm btn-outline-primary" style="cursor: pointer">Attachment</a>';

                            div.innerHTML = sb;
                        } else if (data.ResponByDate) { document.getElementById('form_File_Keterangan_Respon_Filename').style.display = 'none'; }
                    }
                }

                if (Status == "Ditolak Admin SPP") {
                    document.getElementById('tab_Respon').style.display = 'block';
                    if (data.ResponByDate == null && data.PenyaluranByDate != '') {
                        document.getElementById('txt_Keterangan_Respon').value = data.Keterangan_Penyaluran.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                        if (data.PenyaluranByDate)
                            document.getElementById('ResponByDate').textContent = data.PenyaluranByDate;

                        if (data.FilepathFilePenyaluran) {
                            var Ekstension = data.Keterangan_Penyaluran_Ekstension.toLowerCase();
                            var div = document.getElementById('div_fu_Keterangan_Respon_Filename'); div.style.display = 'block';
                            var sb = '<a target="_blank" href="../Home/sef?Filename=' + data.Keterangan_Penyaluran_Filename + '&Extension=' + Ekstension + '" class="btn btn-sm btn-outline-primary" style="cursor: pointer">Attachment</a>';
                            if (Ekstension == '.jpg' || Ekstension == '.png' || Ekstension == '.jpeg')
                                sb = '<a onclick="show_image(\'' + data.FilepathFilePenyaluran + '\', \'Attachment\')" class="btn btn-sm btn-outline-primary" style="cursor: pointer">Attachment</a>';

                            div.innerHTML = sb;
                        } else if (data.ResponByDate) { document.getElementById('form_File_Keterangan_Respon_Filename').style.display = 'none'; }
                    }
                }

                if (culture == 'en') {
                    if (Keterangan_Status == 'Terkirim') Keterangan_Status = 'Sent';
                    else if (Keterangan_Status == 'Diproses') Keterangan_Status = 'Processed';
                    else if (Keterangan_Status == 'Ditolak Admin SPP') Keterangan_Status = 'Declined';
                    else if (Keterangan_Status == 'Selesai') Keterangan_Status = 'Completed';
                }
                document.getElementById('sp_Nomor').textContent = "No. " + data.Nomor + " | Status : " + Keterangan_Status;

                if (culture == 'en') {
                    var ResponByDate = document.getElementById('ResponByDate').textContent;
                    document.getElementById('ResponByDate').textContent = ResponByDate.replace('Direspon oleh ', '').replace('Diselesaikan oleh ', '');
                }

                if (Status == "Selesai")
                    document.getElementById('div_input_Tanggapan').style.display = 'none';

                if (data.FileEvidence != null) {
                    load_file_evidence(ID);
                }

                if (data.Nomor == '' || data.Nomor == null) {
                    window.location.href = "/Pengaduan/DaftarPengaduan?culture=" + culture;
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

function FillDaftarTerlapor() {
    var action = $('#Action').val();
    $('#tbl_daftar_terlapor').DataTable({
        destroy: true,
        order: [1, 'asc'],
        columns: [
            {
                data: '', defaultContent: '', class: 'tdbutton',
                render: function (data, type, full, meta) {
                    if (action == 'view') {
                        return '';
                    }
                    else {
                        return '<td class="tdbutton">' +
                            '<button onclick="Delete_Detail_Terlapor(\'' + full.ID + '\')" class="btn btn-outline-danger btn-sm mr-1" type="button"><i class="fa fa-trash"></i></button>' +
                            '<button onclick="Edit_Detail_Terlapor(\'' + full.ID + '\')" class="btn btn-outline-primary btn-sm" type="button"><i class="fa fa-edit"></i></button>' +
                            '</td>';
                    }

                }
            },
            {
                data: 'Nama', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return '';
                }
            },
            {
                data: 'NomorHandphone', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return '';
                }
            },
            {
                data: 'Departemen', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return '';
                }
            },
            {
                data: 'Jabatan', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return '';
                }
            },
            {
                data: '', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (full.FilepathFileIdentitas != null) {
                        return '<a onclick="show_image(\'' + full.FilepathFileIdentitas + '\', \'Photo\')" class="badge badge-danger ml-2" style="cursor: pointer">View</a>';
                    }
                    else {
                        return '';
                    }
                }
            }
        ],
        columnDefs: [
            { targets: 0, searchable: false, orderable: false }
        ],
        fnInitComplete: function () {
            Refresh_daftar_terlapor();
            $('#div_daftar_terlapor').fadeIn();
        }
    });
}

function Refresh_daftar_terlapor() {
    var ID = sessionStorage.getItem("IDPengaduan");
    $.ajax({
        url: VP + 'Pengaduan/GetDetailPengaduanByIDHeader',
        data: {
            ID_Header: ID
        },
        type: 'POST',
        success: function (Result) {
            if (Result.Error == false) {
                var table = $('#tbl_daftar_terlapor').DataTable();
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

function load_file_evidence(ID_Header) {
    $.ajax({
        url: VP + 'Pengaduan/GetFileEvidenceByIDHeader',
        data: {
            ID_Header: ID_Header
        },
        type: 'POST',
        success: function (Result) {
            if (Result.Error == false) {
                document.getElementById('div_File_Bukti_Pendukung').style.display = 'block';
                if (Result.Message != null && Result.Message.length > 0) {
                    for (var i = 0; i < Result.Message.length; ++i) {
                        var FileEvidence = Result.Message[i].FileEvidence;
                        var FileEvidence_Ekstension = Result.Message[i].FileEvidence_Ekstension;
                        //console.log('FileEvidence ' + FileEvidence);
                        //console.log('FileEvidence_Ekstension ' + FileEvidence_Ekstension);
                        var div = document.getElementById('File_Bukti_Pendukung');
                        var num = i + 1;
                        var sb = '<a target="_blank" href="../Home/sef?Filename=' + Result.Message[i].FileEvidence + '&Extension=' + Result.Message[i].FileEvidence_Ekstension + '" class="badge badge-primary ml-2 mb-2" style="cursor: pointer">File (' + num + ')</a>';
                        if (Result.Message[i].FileEvidence_Ekstension == '.jpg' || Result.Message[i].FileEvidence_Ekstension == '.png' || Result.Message[i].FileEvidence_Ekstension == '.jpeg')
                            sb = '<a onclick="show_image(\'' + Result.Message[i].FilepathFileEvidence + '\', \'Photo (' + num + ')\')" class="badge badge-primary ml-2 mb-2" style="cursor: pointer">Photo (' + num + ')</a>';

                        div.innerHTML += sb;
                    }
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

function AddTerlapor() {
    document.getElementById('Action').value = 'add';
    ResetFormTerlapor();
    $('#div_form_terlapor').fadeIn();
    document.getElementById('txt_Nama_Terlapor').focus();
}

function Edit_Detail_Terlapor(ID) {
    $.ajax({
        url: VP + 'Pengaduan/GetDetailPengaduanByID',
        type: 'POST',
        data: {
            ID: ID
        },
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                document.getElementById('ID').value = ID;
                document.getElementById('txt_Nama_Terlapor').value = data.Nama;
                document.getElementById('txt_Handphone_Terlapor').value = data.NomorHandphone;
                document.getElementById('txt_Departemen_Terlapor').value = data.Departemen;
                document.getElementById('txt_Jabatan_Terlapor').value = data.Jabatan;
                document.getElementById('span_btn_SaveTerlapor').innerHTML = 'Update';
                document.getElementById('Action').value = 'edit';

                $('#div_form_terlapor').fadeIn();
                document.getElementById('txt_Nama_Terlapor').focus();

            } else {
                CustomNotif("error|Oops|" + Result.Message + "");
            }
        },
        error: function (xhr, status, error) {
            CustomNotif("error|Oops|" + error + "");
        }
    })
}

function Delete_Detail_Terlapor(ID) {
    if (confirm("Anda yakin akan menghapus baris ini?")) {
        document.getElementById('ID').value = ID;
        document.getElementById('Action').value = 'hapus';
        SaveTerlapor();
    }
};

function ResetFormTerlapor() {
    document.getElementById('txt_Nama_Terlapor').value = '';
    document.getElementById('txt_Handphone_Terlapor').value = '';
    document.getElementById('txt_Departemen_Terlapor').value = '';
    document.getElementById('txt_Jabatan_Terlapor').value = '';
    document.getElementById('fu_FileIdentitas').value = '';
    document.getElementById('span_btn_SaveTerlapor').innerHTML = 'Submit';
}

function CancelTerlapor() {
    ResetFormTerlapor();
    $('#div_form_terlapor').hide();
}

function SaveTerlapor() {

    var DataForm = new FormData();
    DataForm.append('Action', $('#Action').val());
    DataForm.append('ID', $('#ID').val());
    DataForm.append('ID_Header', $('#ID_Header').val());

    DataForm.append('enc_Email', $('#txt_Email').val());
    DataForm.append('enc_Handphone', $('#txt_Handphone').val());

    DataForm.append('enc_Nama', $('#txt_Nama_Terlapor').val());
    DataForm.append('enc_NomorHandphone', $('#txt_Handphone_Terlapor').val());
    DataForm.append('enc_Departemen', $('#txt_Departemen_Terlapor').val());
    DataForm.append('enc_Jabatan', $('#txt_Jabatan_Terlapor').val());
    DataForm.append("UploadFileIdentitas", $('#fu_FileIdentitas')[0].files[0]);

    $.ajax({
        url: VP + 'Pengaduan/SaveDetailPengaduan',
        data: DataForm,
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (Result) {
            if (Result.Error == false) {
                var ID_Header = Result.Message;
                document.getElementById('ID_Header').value = ID_Header;
                sessionStorage.setItem('IDPengaduan', ID_Header);
                FillDaftarTerlapor(ID_Header);
                CancelTerlapor();
            } else {
                CustomNotif("error|Oops|" + Result.Message + "");
            }
        },
        error: function (xhr, status, error) {
            CustomNotif("error|Oops|" + error + "");
        }
    })
}

$('#FormInput').validate({
    rules: {
        txt_Email: { required: true, AntiXSS: true, AntiHTML: true },
        txt_Handphone: { required: false, AntiXSS: true, AntiHTML: true },

        txt_TempatKejadian: { required: true, AntiXSS: true, AntiHTML: true },
        txt_WaktuKejadian: { required: true, AntiXSS: true, AntiHTML: true },
        txt_Kronologi: { required: true, AntiXSS: true, AntiHTML: true },
        Jenis_Pelanggaran: { required: true, AntiXSS: true, AntiHTML: true }

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

        DataForm.append('ID', $('#ID_Header').val());
        DataForm.append('enc_Email', $('#txt_Email').val());
        DataForm.append('enc_Handphone', $('#txt_Handphone').val());

        DataForm.append('enc_TempatKejadian', $('#txt_TempatKejadian').val());
        DataForm.append('WaktuKejadian', $('#txt_WaktuKejadian').val());
        DataForm.append('enc_Kronologi', $('#txt_Kronologi').val());
        DataForm.append('Jenis_Pelanggaran', $('#Jenis_Pelanggaran').val());

        var files = $("#fu_FileEvidence").get(0).files;

        for (var i = 0; i < files.length; i++) {
            DataForm.append('UploadFileEvidence', files[i]);
        }

        $.ajax({
            url: VP + 'Pengaduan/SavePengaduan',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    var Nomor = Result.Message;
                    SubmitKuesionerValue($('#ID_Header').val());
                    var culture = document.getElementById('culture').value;
                    if (culture == 'en')
                        CustomNotif('success|Report Sent|Thank you, your report was successfully sent with ticket no. [' + Nomor + '], Your report will be processed by WBS Admin|window.location.href = "/Pengaduan/DaftarPengaduan?culture=' + culture + '"');
                    else
                        CustomNotif('success|Terkirim|Terima kasih, pengaduan Anda sudah terkirim dengan nomor tiket aduan : [' + Nomor + '], Pengaduan akan diproses oleh Pihak terkait|window.location.href = "/Pengaduan/DaftarPengaduan?culture=' + culture + '"');
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

$('#FormTanggapan').validate({
    rules: {
        txt_Tanggapan: { required: true, AntiXSS: true, AntiHTML: true },
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

        DataForm.append('IDPengaduan', $('#ID_Header').val());
        DataForm.append('enc_Tanggapan', $('#txt_Tanggapan').val());
        DataForm.append("UploadFileLampiran", $('#fu_FileLampiran')[0].files[0]);
        DataForm.append("TipePengirim", "Pelapor");

        $.ajax({
            url: VP + 'Pengaduan/KirimTanggapanPelapor',
            data: DataForm,
            type: 'POST',
            contentType: false,
            enctype: 'multipart/form-data',
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    FillTanggapan();
                    var culture = document.getElementById('culture').value;
                    if (culture == 'en')
                        CustomNotif('success|Message Sent|Your message was sent successfully');
                    else
                        CustomNotif('success|Terkirim|Terima kasih Anda telah mengirimkan tanggapan');
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

$(function () {
    if (typeof $.fn.barrating !== typeof undefined) {
        $('#example').barrating({
            theme: 'fontawesome-stars',
            showValues: false,
            showSelectedRating: false,
            onSelect: function (value, text) {
                document.getElementById("hf_Feedback").value = value;
            }
        });
        $('#bars-number').barrating({
            theme: 'bars-1to10'
        });
        $('#barsnumber').barrating({
            theme: 'bars-1to10'
        });
        $('#movie-rating').barrating('show', {
            theme: 'bars-movie'
        });
        $('#movie-rating').barrating('set', 'Mediocre');
        $('#pill-rating-a').barrating({
            theme: 'bars-pill',
            showValues: true,
            showSelectedRating: false,
            onSelect: function (value, text) {
                alert('Selected rating: ' + value);
            }
        });
    }
    if (typeof $.fn.mdbRate !== typeof undefined) {
        $('#rateMe1').mdbRate();
        $('#face-rating').mdbRate();
    }
});

function FillTanggapan() {
    var ID = sessionStorage.getItem("IDPengaduan");
    document.getElementById('txt_Tanggapan').value = '';
    document.getElementById('fu_FileLampiran').value = null;

    $.ajax({
        url: VP + 'Pengaduan/GetTanggapanByIDPengaduan',
        type: 'POST',
        data: {
            ID_Pengaduan: ID
        },
        success: function (res) {
            var sb = "";
            $.each(res.Message, function (data, value) {

                var dot = value.TipePengirim == "Admin SPP" ? "border-primary text-primary" : "border-danger text-danger";
                var NamaPengirim = value.TipePengirim == "Admin SPP" ? "Admin SPP" : value.Nama;
                var isRead = '';
                if (value.IsRead == 0 && value.TipePengirim == 'Admin SPP')
                    isRead = "<span class='badge badge-danger ml-2'>New</span>";

                sb += "<li>" +
                    "<div class='timeline-dots timeline-dot1 " + dot + "'></div>" +
                    "<h6 class='float-left mb-1'>" + NamaPengirim + "</h6>" + isRead + "" +
                    "<small class='float-right mt-1'>" + value._Createdon + "</small>" +
                    "<div class='d-inline-block w-100'>" +
                    "<p class='text-sm text-justify'>" + value.Tanggapan.replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/(?:\r\n|\r|\n)/g, '<br>') + "</p>";

                if (value.FileLampiran != null) {
                    var FileLampiran = value.FileLampiran;
                    var FileLampiran_Ekstension = value.FileLampiran_Ekstension;
                    var Path = "";

                    if (FileLampiran_Ekstension == ".mp3" || FileLampiran_Ekstension == ".mpeg") {
                        Path = "../Home/gef?Filename=" + FileLampiran + "&Extension=" + FileLampiran_Ekstension + "";
                        sb += "<div class='iq-media-group'>" +
                            "<a href='#' class='iq-media'>" +
                            "<a href='" + Path + "' class='btn btn-outline-danger btn-sm' style='text-style: italic'><i class='fa fa-download'></i> Download Lampiran</a>" +
                            "</a>" +
                            "</div>";
                    }
                    else if (FileLampiran_Ekstension == ".jpg" || FileLampiran_Ekstension == ".jpeg" || FileLampiran_Ekstension == ".png") {
                        var title = 'Attachment'
                        Path = "../Home/gef?Filename=" + FileLampiran + "&Extension=" + FileLampiran_Ekstension + "";
                        sb += "<div class='iq-media-group'>" +
                            "<a href='#' class='iq-media'>" +
                            '<button onclick="show_image(\'' + value.FilepathFileLampiran + '\', \'' + title + '\');" class="btn btn-outline-danger btn-sm" style="text-style: italic"><i class="fa fa-image"></i> Attachment</button>' +
                            "</a>" +
                            "</div>";
                    }
                    else {
                        Path = "../Home/sef?Filename=" + FileLampiran + "&Extension=" + FileLampiran_Ekstension + "";
                        sb += "<div class='iq-media-group'>" +
                            "<a href='#' class='iq-media'>" +
                            "<a href='" + Path + "' target='_blank' class='btn btn-outline-danger btn-sm' style='text-style: italic'><i class='fa fa-download'></i> Open Attachment</a>" +
                            "</a>" +
                            "</div>";
                    }

                }

                sb += "</div></li>";
            })

            if (sb != '') {
                document.getElementById('ul_Tanggapan').innerHTML = sb.toString();
                document.getElementById('div_Riwayat_Tanggapan').style.display = 'block';
                ReadTanggapanAdminSPP();
            }
            else {
                document.getElementById('div_Riwayat_Tanggapan').style.display = 'none';
            }

        }
    })
}

function ReadTanggapanAdminSPP() {
    var ID = sessionStorage.getItem("IDPengaduan");
    $.ajax({
        url: VP + 'Pengaduan/ReadTanggapanAdminSPP',
        type: 'POST',
        data: {
            ID_Pengaduan: ID
        },
        success: function (res) {
            SetNotifikasi();
        }
    })
}

var txt_Handphone = document.querySelector("#txt_Handphone");
window.intlTelInput(txt_Handphone, {
    hiddenInput: "full_number",
    separateDialCode: true,
    utilsScript: "/js/telephone/utils.js",
});

var txt_Handphone_Terlapor = document.querySelector("#txt_Handphone_Terlapor");
window.intlTelInput(txt_Handphone_Terlapor, {
    hiddenInput: "full_number",
    separateDialCode: true,
    utilsScript: "/js/telephone/utils.js",
});

function getLabel(id) {
    return $("#" + id).next("label").html();
}

function getCheckedIds(div_id, tipe) {
    var checkedCbs = document.querySelectorAll('#' + div_id + ' input[type="' + tipe + '"]:checked');
    var ids = [];
    for (var r = 0; r < checkedCbs.length; r++)
        ids.push(getLabel(checkedCbs[r].id));
    return ids;
}

function GetKuesionerValue() {
    var IDKuesioner = $('#IDKuesioner').val();
    var arr = [];
    var cart = [];
    var field_count = $('#field_count').val();
    arr = JSON.parse(field_count);
    if (arr.length > 0) {
        $.ajax({
            async: false,
            url: VP + 'Kuesioner/Get_Kuesioner_Detail_By_IDHeader',
            data: {
                IDHeader: IDKuesioner
            },
            type: 'POST',
            success: function (Result) {
                if (Result.Error == false) {
                    var data = Result.Message[0];
                    if (Result.Message.length > 0) {

                        var arr = [];
                        for (var j = 0; j < Result.Message.length; j++) {
                            var Num = Result.Message[j].Num;
                            arr.push(Num);
                        }

                        if (arr.length > 0) {

                            for (var k = 0; k < arr.length; k++) {
                                if (arr[k]) {
                                    var person = {};
                                    var i = arr[k];
                                    var InputType = Result.Message[k].InputType;
                                    var Label = Result.Message[k].Label;
                                    var Required = Result.Message[k].Required;
                                    var Options = Result.Message[k].Options;

                                    person.InputType = InputType;
                                    person.Label = Label;
                                    person.Required = Required;
                                    person.Options = Options;

                                    if (InputType == 'input' || InputType == 'email' || InputType == 'textarea')
                                        person.InputTypeValue = document.getElementById('input_preview_' + i).value;
                                    else if (InputType == 'select')
                                        person.InputTypeValue = document.getElementById('select_preview_' + i).value;
                                    else if (InputType == 'checkbox') {
                                        var ids = getCheckedIds('div_form_checkbox_' + i, 'checkbox');
                                        person.InputTypeValue = ids.join(',');
                                    }
                                    else if (InputType == 'radios') {
                                        var ids = getCheckedIds('div_form_radio_' + i, 'radio');
                                        person.InputTypeValue = ids.join(',');
                                    }
                                    else if (InputType == 'switch') {
                                        var checked = document.getElementById('switch_preview_' + i).checked;
                                        person.InputTypeValue = checked;
                                    }

                                    cart.push(person);

                                }
                            }

                            //console.log(JSON.stringify(cart));

                        }
                    }
                }
            }
        })
    }

    return JSON.stringify(cart);
}

function SubmitKuesionerValue(IDPengaduan) {
    var IDKuesioner = $('#IDKuesioner').val();
    var field_count = $('#field_count').val();
    if (field_count != '') {
        arr = JSON.parse(field_count);
        if (arr.length > 0) {
            $.ajax({
                url: VP + 'Kuesioner/Get_Kuesioner_Detail_By_IDHeader',
                data: {
                    IDHeader: IDKuesioner
                },
                type: 'POST',
                success: function (Result) {
                    if (Result.Error == false) {
                        var data = Result.Message[0];
                        if (Result.Message.length > 0) {

                            var arr = [];
                            for (var j = 0; j < Result.Message.length; j++) {
                                var Num = Result.Message[j].Num;
                                arr.push(Num);
                            }

                            if (arr.length > 0) {

                                for (var k = 0; k < arr.length; k++) {
                                    if (arr[k]) {
                                        var i = arr[k];
                                        var InputType = Result.Message[k].InputType;
                                        var Label = Result.Message[k].Label;
                                        var Required = Result.Message[k].Required;
                                        var Options = Result.Message[k].Options;
                                        var InputValue = '';

                                        if (InputType == 'input' || InputType == 'email' || InputType == 'textarea')
                                            InputValue = document.getElementById('input_preview_' + i).value;
                                        else if (InputType == 'select')
                                            InputValue = document.getElementById('select_preview_' + i).value;
                                        else if (InputType == 'checkbox') {
                                            var ids = getCheckedIds('div_form_checkbox_' + i, 'checkbox');
                                            InputValue = ids.join(',');
                                        }
                                        else if (InputType == 'radios') {
                                            var ids = getCheckedIds('div_form_radio_' + i, 'radio');
                                            InputValue = ids.join(',');
                                        }
                                        else if (InputType == 'switch') {
                                            var checked = document.getElementById('switch_preview_' + i).checked;
                                            InputValue = checked == true ? "true" : "false";
                                        }

                                        var DataForm = new FormData();
                                        DataForm.append('IDPengaduan', IDPengaduan);
                                        DataForm.append('Title', data.Title);
                                        DataForm.append('Num', i);
                                        DataForm.append('InputType', InputType);
                                        DataForm.append('Label', Label);
                                        DataForm.append('Required', Required);
                                        DataForm.append('Options', Options);
                                        DataForm.append('InputValue', InputValue);

                                        $.ajax({
                                            url: VP + 'Kuesioner/Save_KuesionerValue',
                                            data: DataForm,
                                            type: 'POST',
                                            contentType: false,
                                            processData: false,
                                            success: function (Result) {
                                                if (Result.Error == false) {
                                                    console.log("save value : " + i);
                                                }
                                            }
                                        })

                                    }
                                }

                            }
                        }
                    }
                }
            })
        }
    }

}

function Test_Kuesioner() {
    var arr = [];
    arr = GetKuesionerValue();
    console.log(arr);

    for (var i = 0; i < arr.length; i++) {
        var InputType = arr[i].InputType;
        console.log(InputType);
    }
}

function SetKuesioner() {
    $.ajax({
        url: VP + 'Kuesioner/Get_Aktif_Kuesioner',
        type: 'POST',
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                if (Result.Message.length > 0) {

                    var arr = [];
                    for (var i = 0; i < Result.Message.length; i++) {
                        var Num = Result.Message[i].Num;
                        arr.push(Num);
                    }

                    $('#field_count').val(JSON.stringify(arr));
                    if (arr.length > 0) {
                        document.getElementById('div_kuesioner').style.display = 'block';
                        document.getElementById('Title').innerText = data.Title;
                        document.getElementById('IDKuesioner').value = data.IDKuesioner;
                    }
                    for (var k = 0; k < arr.length; k++) {
                        if (arr[k]) {
                            AddPreview(arr[k], Result.Message[k].InputType, Result.Message[k].Label, Result.Message[k].Required, Result.Message[k].Options);
                        }
                    }

                }
                else {
                    $('#ActionDetail').val('add');
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

function AddPreview(param_i, set_select, param_label, param_required, set_options) {
    var i = param_i;
    if (param_label == '')
        param_label = 'Label ' + i;

    var div_preview = document.getElementById('div_preview');

    var div_preview_group = document.createElement('div');
    div_preview_group.id = 'div_preview_group_' + i;
    if (param_required == 1)
        div_preview_group.classList.add('form-group', 'required');
    else
        div_preview_group.classList.add('form-group');

    div_preview.appendChild(div_preview_group);

    var label_preview = document.createElement('label');
    label_preview.id = 'label_preview_' + i;
    label_preview.classList.add('control-label', 'text-justify');
    label_preview.innerText = param_label;
    div_preview_group.appendChild(label_preview);

    if (set_select == 'input') {
        var input = document.createElement('input');
        input.id = 'input_preview_' + i;
        input.type = 'text';
        if (param_required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'textarea') {
        var input = document.createElement('textarea');
        input.id = 'input_preview_' + i;
        if (param_required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'email') {
        var input = document.createElement('input');
        input.id = 'input_preview_' + i;
        input.type = 'email';
        if (param_required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'select') {
        var input = document.createElement('select');
        input.id = 'select_preview_' + i;
        if (param_required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);

        if (set_options) {
            var default_option = document.createElement("option");
            default_option.value = '';
            default_option.text = '- select -';
            var default_select_preview = document.getElementById('select_preview_' + i);
            if (default_select_preview)
                default_select_preview.appendChild(default_option);

            var arr = add_value_to_array(set_options);
            for (var k = 0; k < arr.length; k++) {
                if (arr[k] != '') {
                    var option = document.createElement("option");
                    option.value = arr[k];
                    option.text = arr[k];
                    var select_preview = document.getElementById('select_preview_' + i);
                    if (select_preview)
                        select_preview.appendChild(option);
                }

            }

        }
    }
    else if (set_select == 'switch') {

        var control_label = document.getElementById('label_preview_' + i);
        if (control_label)
            control_label.remove();

        var div = document.createElement('div');
        div.id = 'input_preview_' + i;
        div.classList.add('custom-control', 'custom-switch', 'custom-control-inline');
        div_preview_group.appendChild(div);

        var input = document.createElement('input');
        input.id = 'switch_preview_' + i;
        input.type = 'checkbox';
        input.classList.add('custom-control-input');
        div.appendChild(input);

        var label = document.createElement('label');
        label.classList.add('custom-control-label');
        label.htmlFor = 'switch_preview_' + i;
        label.innerText = param_label;


        div.appendChild(label);
    }
    else if (set_select == 'checkbox') {

        if (set_options) {

            var div_form_checkbox = document.createElement('div');
            div_form_checkbox.classList.add('form-group');
            div_form_checkbox.id = 'div_form_checkbox_' + i;
            div_preview_group.appendChild(div_form_checkbox);

            var arr = add_value_to_array(set_options);
            for (var k = 0; k < arr.length; k++) {
                if (arr[k] != '') {
                    var div = document.createElement('div');
                    div.id = 'input_preview_' + i + k;
                    div.classList.add('custom-control', 'custom-checkbox', 'custom-control-inline');


                    var input = document.createElement('input');
                    input.id = 'checkbox_preview_' + i + k;
                    input.type = 'checkbox';
                    input.classList.add('custom-control-input');
                    if (k == 0)
                        input.checked = true;
                    div.appendChild(input);

                    var label = document.createElement('label');
                    label.classList.add('custom-control-label');
                    label.htmlFor = 'checkbox_preview_' + i + k;
                    label.innerText = arr[k];
                    div.appendChild(label);

                    div_form_checkbox.appendChild(div);

                }

            }

        }
    }
    else if (set_select == 'radios') {

        if (set_options) {

            var div_form_radio = document.createElement('div');
            div_form_radio.classList.add('form-group');
            div_form_radio.id = 'div_form_radio_' + i;
            div_preview_group.appendChild(div_form_radio);

            var arr = add_value_to_array(set_options);
            for (var k = 0; k < arr.length; k++) {
                if (arr[k] != '') {
                    var div = document.createElement('div');
                    div.id = 'input_preview_' + i + k;
                    div.classList.add('custom-control', 'custom-radio', 'custom-control-inline');

                    var input = document.createElement('input');
                    input.id = 'radio_preview_' + i + k;
                    input.name = 'radio_preview_' + i;
                    input.type = 'radio';
                    input.classList.add('custom-control-input');
                    if (k == 0)
                        input.checked = true;
                    div.appendChild(input);

                    var label = document.createElement('label');
                    label.classList.add('custom-control-label');
                    label.htmlFor = 'radio_preview_' + i + k;
                    label.innerText = arr[k];
                    div.appendChild(label);

                    div_form_radio.appendChild(div);

                }

            }

        }
    }

}

function add_value_to_array(value) {
    let splits = value.split(',');
    var notesArray = new Array();
    for (var i = 0; i < splits.length; i++) {
        notesArray.push(splits[i]);
    }
    return notesArray;
}

function FillKuesionerValue(IDPengaduan) {
    $.ajax({
        url: VP + 'Kuesioner/Get_KuesionerValue_By_IDPengaduan',
        data: {
            IDPengaduan: IDPengaduan
        },
        type: 'POST',
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                if (Result.Message.length > 0) {

                    var arr = [];
                    for (var i = 0; i < Result.Message.length; i++) {
                        var Num = Result.Message[i].Num;
                        arr.push(Num);
                    }

                    $('#field_count').val(JSON.stringify(arr));
                    if (arr.length > 0) {
                        document.getElementById('div_kuesioner').style.display = 'block';
                        document.getElementById('Title').innerText = data.Title;
                        document.getElementById('IDKuesioner').value = data.IDKuesioner;
                    }
                    for (var k = 0; k < arr.length; k++) {
                        if (arr[k]) {
                            FillPreview(arr[k], Result.Message[k].InputType, Result.Message[k].Label, Result.Message[k].Required, Result.Message[k].Options, Result.Message[k].InputValue);
                        }
                    }

                }
                else {
                    $('#ActionDetail').val('add');
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

function FillPreview(param_i, set_select, param_label, param_required, set_options, param_value) {
    var i = param_i;
    if (param_label == '')
        param_label = 'Label ' + i;

    var div_preview = document.getElementById('div_preview');

    var div_preview_group = document.createElement('div');
    div_preview_group.id = 'div_preview_group_' + i;
    if (param_required == 1)
        div_preview_group.classList.add('form-group', 'required');
    else
        div_preview_group.classList.add('form-group');

    div_preview.appendChild(div_preview_group);

    var label_preview = document.createElement('label');
    label_preview.id = 'label_preview_' + i;
    label_preview.classList.add('control-label', 'text-justify');
    label_preview.innerText = param_label;
    div_preview_group.appendChild(label_preview);

    if (set_select == 'input') {
        var input = document.createElement('input');
        input.id = 'input_preview_' + i;
        input.type = 'text';
        input.value = param_value;
        if (param_required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'textarea') {
        var input = document.createElement('textarea');
        input.id = 'input_preview_' + i;
        input.value = param_value;
        if (param_required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'email') {
        var input = document.createElement('input');
        input.id = 'input_preview_' + i;
        input.type = 'email';
        input.value = param_value;
        if (param_required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'select') {
        var input = document.createElement('select');
        input.id = 'select_preview_' + i;
        if (param_required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);

        if (set_options) {
            var default_option = document.createElement("option");
            default_option.value = '';
            default_option.text = '- select -';
            var default_select_preview = document.getElementById('select_preview_' + i);
            if (default_select_preview)
                default_select_preview.appendChild(default_option);

            var arr = add_value_to_array(set_options);
            for (var k = 0; k < arr.length; k++) {
                if (arr[k] != '') {
                    var option = document.createElement("option");
                    option.value = arr[k];
                    option.text = arr[k];
                    var select_preview = document.getElementById('select_preview_' + i);
                    if (select_preview)
                        select_preview.appendChild(option);
                }

            }

        }

        input.value = param_value;
    }
    else if (set_select == 'switch') {

        var control_label = document.getElementById('label_preview_' + i);
        if (control_label)
            control_label.remove();

        var div = document.createElement('div');
        div.id = 'input_preview_' + i;
        div.classList.add('custom-control', 'custom-switch', 'custom-control-inline');
        div_preview_group.appendChild(div);

        var input = document.createElement('input');
        input.id = 'switch_preview_' + i;
        input.type = 'checkbox';
        input.checked = param_value == "true";
        input.classList.add('custom-control-input');
        div.appendChild(input);

        var label = document.createElement('label');
        label.classList.add('custom-control-label');
        label.htmlFor = 'switch_preview_' + i;
        label.innerText = param_label;


        div.appendChild(label);
    }
    else if (set_select == 'checkbox') {

        if (set_options) {

            var div_form_checkbox = document.createElement('div');
            div_form_checkbox.classList.add('form-group');
            div_form_checkbox.id = 'div_form_checkbox_' + i;
            div_preview_group.appendChild(div_form_checkbox);

            var arr = add_value_to_array(set_options);
            for (var k = 0; k < arr.length; k++) {
                if (arr[k] != '') {
                    var div = document.createElement('div');
                    div.id = 'input_preview_' + i + k;
                    div.classList.add('custom-control', 'custom-checkbox', 'custom-control-inline');


                    var input = document.createElement('input');
                    input.id = 'checkbox_preview_' + i + k;
                    input.type = 'checkbox';
                    input.classList.add('custom-control-input');
                    input.disabled = true;
                    div.appendChild(input);

                    var label = document.createElement('label');
                    label.classList.add('custom-control-label');
                    label.htmlFor = 'checkbox_preview_' + i + k;
                    label.innerText = arr[k];
                    div.appendChild(label);

                    div_form_checkbox.appendChild(div);

                    var arre = add_value_to_array(param_value);
                    for (var r = 0; r < arre.length; r++) {
                        var LabelText = arre[r];
                        var elem = $("label:contains('" + LabelText + "')").prev('input');
                        elem.attr('disabled', false);
                        elem.attr('checked', true);
                    }

                }

            }

        }
    }
    else if (set_select == 'radios') {

        if (set_options) {

            var div_form_radio = document.createElement('div');
            div_form_radio.classList.add('form-group');
            div_form_radio.id = 'div_form_radio_' + i;
            div_preview_group.appendChild(div_form_radio);

            var arr = add_value_to_array(set_options);
            for (var k = 0; k < arr.length; k++) {
                if (arr[k] != '') {
                    var div = document.createElement('div');
                    div.id = 'input_preview_' + i + k;
                    div.classList.add('custom-control', 'custom-radio', 'custom-control-inline');

                    var input = document.createElement('input');
                    input.id = 'radio_preview_' + i + k;
                    input.name = 'radio_preview_' + i;
                    input.type = 'radio';
                    input.classList.add('custom-control-input');
                    if (k == 0)
                        input.checked = true;
                    div.appendChild(input);

                    var label = document.createElement('label');
                    label.classList.add('custom-control-label');
                    label.htmlFor = 'radio_preview_' + i + k;
                    label.innerText = arr[k];
                    div.appendChild(label);

                    div_form_radio.appendChild(div);

                    var arre = add_value_to_array(param_value);
                    for (var r = 0; r < arre.length; r++) {
                        var LabelText = arre[r];
                        var elem = $("label:contains('" + LabelText + "')").prev('input');
                        elem.attr('disabled', false);
                        elem.attr('checked', true);
                    }

                }

            }

        }
    }

}