$(document).ready(function () {
    //SetClearButton();
    HideInput();
    Bind_ddl_Delegator();
    Bind_Jenis_Pelanggaran();
    SetRole();

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
                    if (Result.Message != '') {
                        sessionStorage.setItem("IDPengaduan", Result.Message);

                        var mode = url.searchParams.get("v");
                        if (mode != null) {
                            if (mode == 'eks')
                                window.location.href = "/Pengaduan/PengaduanForm?v=eks";
                            else if (mode == 'int')
                                window.location.href = "/Pengaduan/PengaduanForm?v=int";
                            else if (mode == 'respon')
                                window.location.href = "/Pengaduan/PengaduanForm?v=int";
                            else
                                window.location.href = "/Pengaduan/PengaduanForm";
                        }
                        else
                            window.location.href = "/Pengaduan/PengaduanForm";
                    }
                    else
                        CustomNotif('error|Oops|Invalid External Request|window.location.href = "/Pengaduan/DaftarPengaduan"');
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
        if (mode == 'eks')
            document.getElementById('nav_tanggapan').click();
        else if (mode == 'int')
            document.getElementById('nav_tanggapan_internal').click();
        else if (mode == 'respon')
            document.getElementById('nav_proses').click();
    }

    var ID = sessionStorage.getItem("IDPengaduan");
    if (ID != null && ID != '') {
        FillForm(ID);
        document.getElementById('Action').value = 'proses';
    }
    else {
        sessionStorage.setItem("IDPengaduan", '');
        var url_var = window.location.href;
        var url = new URL(url_var);
        var act = url.searchParams.get("act");
        if (act == 'add') {

            var Role = sessionStorage.getItem("Role");
            if (Role.includes('Admin SPP')) {
                SetKuesioner();
                document.getElementById('Action').value = 'add';
                document.getElementById('ID_Header').value = '';

                document.getElementById('tabPengaduan').innerHTML = 'PENGADUAN BARU';
                document.getElementById("sp_Nomor").textContent = 'Di input oleh Admin SPP';

                document.getElementById('form_File_Bukti_Pendukung').style.display = 'block';
                document.getElementById('fu_FileEvidence').style.display = 'block';
                document.getElementById('btn_Kirim').style.display = 'block';
                document.getElementById('ket_mandatory').style.display = 'block';
                document.getElementById('div_btnAdd_Terlapor').style.display = 'block';

                document.getElementById('tab_tanggapan').style.display = 'none';
                document.getElementById('tab_tanggapan_internal').style.display = 'none';
                document.getElementById('tab_proses').style.display = 'none';
            }
        }
    }
});

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

function Bind_Jenis_Pelanggaran() {
    $("#Jenis_Pelanggaran").empty();
    $("#Jenis_Pelanggaran").append($("<option></option>").val("").html("- Select -"));

    $("#Jenis_Pelanggaran_Input").empty();
    $("#Jenis_Pelanggaran_Input").append($("<option></option>").val("").html("- Select -"));

    $.ajax({
        type: "POST",
        url: VP + 'Pengaduan/Get_Jenis_Pelanggaran',
        dataType: "json", contentType: "application/json",
        success: function (res) {
            $.each(res.Message, function (data, value) {
                var Value = value.GridTitle;
                $("#Jenis_Pelanggaran").append($("<option></option>").val(Value).html(Value));
                $("#Jenis_Pelanggaran_Input").append($("<option></option>").val(Value).html(Value));
            })
        }

    });
}

function SetStatus_Respon(Status_Respon) {
    document.getElementById('div_ddl_Delegator').style.display = 'none';
    document.getElementById('keterangan_penolakan').innerHTML = 'Keterangan';
    document.getElementById('lbl_Jenis_Pelanggaran').innerHTML = 'Jenis Pelanggaran';
    document.getElementById('div_Jenis_Pelanggaran').style.display = 'none';
    document.getElementById('Jenis_Pelanggaran').required = false;
    document.getElementById('ddl_Delegator').required = false;

    if (Status_Respon == 'Ditolak Admin SPP') {
        document.getElementById('keterangan_penolakan').innerHTML = 'Keterangan Penolakan untuk Pelapor *)';
        document.getElementById('lbl_Jenis_Pelanggaran').innerHTML = 'Jenis Pelanggaran';
        document.getElementById('Jenis_Pelanggaran').required = false;
        document.getElementById('ddl_Delegator').required = false;
        document.getElementById('div_ddl_Delegator').style.display = 'none';
        document.getElementById('div_Jenis_Pelanggaran').style.display = 'none';
    }
    else if (Status_Respon == 'Diproses') {
        document.getElementById('div_ddl_Delegator').style.display = 'block';
        document.getElementById('div_Jenis_Pelanggaran').style.display = 'block';
        document.getElementById('keterangan_penolakan').innerHTML = 'Keterangan untuk Tim Delegator *)';
        document.getElementById('lbl_Jenis_Pelanggaran').innerHTML = 'Jenis Pelanggaran *)';
        document.getElementById('Jenis_Pelanggaran').required = true;
        document.getElementById('ddl_Delegator').required = true;
    } else {
        document.getElementById('keterangan_penolakan').innerHTML = 'Keterangan';
        document.getElementById('lbl_Jenis_Pelanggaran').innerHTML = 'Jenis Pelanggaran';
        document.getElementById('Jenis_Pelanggaran').required = false;
        document.getElementById('ddl_Delegator').required = false;
        document.getElementById('div_Jenis_Pelanggaran').style.display = 'none';
        document.getElementById('div_ddl_Delegator').style.display = 'none';

    }
}

function HideInput() {
    document.getElementById('form_File_Bukti_Pendukung').style.display = 'none';
    document.getElementById('fu_FileEvidence').style.display = 'none';
    document.getElementById('btn_Kirim').style.display = 'none';
    document.getElementById('ket_mandatory').style.display = 'none';
    document.getElementById('div_btnAdd_Terlapor').style.display = 'none';

    document.getElementById('div_input_Tanggapan').style.display = 'none';
    document.getElementById('btn_ResponAdminSPP').style.display = 'none';
    document.getElementById('fu_Keterangan_Penyaluran_Filename').style.display = 'none';
    document.getElementById('btn_clear_fu_Keterangan_Penyaluran_Filename').style.display = 'none';
    document.getElementById('btn_ResponFinal').style.display = 'none';
    document.getElementById('fu_Keterangan_Respon_Filename').style.display = 'none';
    document.getElementById('btn_clear_fu_Keterangan_Respon_Filename').style.display = 'none';

    document.getElementById('btn_ResponDelegator').style.display = 'none';
    document.getElementById('fu_Keterangan_Pemeriksaan_Filename').style.display = 'none';
    document.getElementById('btn_clear_fu_Keterangan_Pemeriksaan_Filename').style.display = 'none';
    document.getElementById('fu_Keterangan_Konfirmasi_Filename').style.display = 'none';
    document.getElementById('btn_clear_fu_Keterangan_Konfirmasi_Filename').style.display = 'none';

    document.getElementById('div_respon_Admin_SPP').style.display = 'none';
    document.getElementById('div_respon_Delegator').style.display = 'none';
    document.getElementById('div_respon_Final').style.display = 'none';
}

function FillForm(ID) {

    var Role = sessionStorage.getItem("Role");
    if (Role.includes('Admin SPP')) {
        document.getElementById('div_input_Tanggapan').style.display = 'block';
    }

    document.getElementById('ID_Header').value = ID;

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
                else {
                    document.getElementById('DisabledEmail').checked = true;
                    SetDisabledEmail();
                    document.getElementById('tab_tanggapan').style.display = 'none';
                }
                SetDisabledEmail();

                document.getElementById('Sumber').value = data.Sumber.replace(/</g, "&lt;").replace(/>/g, "&gt;");

                if (data.Handphone)
                    document.getElementById('txt_Handphone').value = data.Handphone.replace(/</g, "&lt;").replace(/>/g, "&gt;");

                FillDaftarTerlapor(data.ID);

                document.getElementById('txt_TempatKejadian').value = data.TempatKejadian.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                document.getElementById('txt_WaktuKejadian').value = data.s_WaktuKejadian.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                if (data.s_WaktuKejadian)
                    document.getElementById('txt_WaktuKejadian').disabled = true;

                document.getElementById('txt_Kronologi').value = data.Kronologi.replace(/</g, "&lt;").replace(/>/g, "&gt;");

                FillKuesionerValue(ID);

                var Status = data.Status;

                if (Status == 'Terkirim' && Role.includes('Admin SPP')) {
                    document.getElementById('div_input_Tanggapan').style.display = 'block';
                    document.getElementById('btn_ResponAdminSPP').style.display = 'block';
                    document.getElementById('fu_Keterangan_Penyaluran_Filename').style.display = 'block';
                    document.getElementById('btn_clear_fu_Keterangan_Penyaluran_Filename').style.display = 'block';
                }
                else if (Status == 'Ditolak Admin SPP' && Role.includes('Admin SPP')) {
                    document.getElementById('div_input_Tanggapan').style.display = 'block';
                }
                if (Status == 'Terkirim' && Role.includes('Delegator')) {
                    document.getElementById('div_input_Tanggapan_Internal').style.display = 'block';
                }
                else if (Status == 'Ditolak Admin SPP' && Role.includes('Delegator')) {
                    document.getElementById('div_input_Tanggapan_Internal').style.display = 'block';
                }
                else if (Status == 'Diproses' && Role.includes('Delegator')) {
                    document.getElementById('btn_ResponDelegator').style.display = 'block';
                    document.getElementById('fu_Keterangan_Pemeriksaan_Filename').style.display = 'block';
                    document.getElementById('btn_clear_fu_Keterangan_Pemeriksaan_Filename').style.display = 'block';
                    document.getElementById('fu_Keterangan_Konfirmasi_Filename').style.display = 'block';
                    document.getElementById('btn_clear_fu_Keterangan_Konfirmasi_Filename').style.display = 'block';
                }
                else if (Status == 'Ditolak Delegator' && Role.includes('Admin SPP')) {
                    document.getElementById('btn_ResponAdminSPP').style.display = 'block';
                    document.getElementById('fu_Keterangan_Penyaluran_Filename').style.display = 'block';
                    document.getElementById('btn_clear_fu_Keterangan_Penyaluran_Filename').style.display = 'block';
                }
                else if (Status == 'Dihentikan' && Role.includes('Admin SPP')) {
                    document.getElementById('btn_ResponFinal').style.display = 'block';
                    document.getElementById('fu_Keterangan_Respon_Filename').style.display = 'block';
                    document.getElementById('btn_clear_fu_Keterangan_Respon_Filename').style.display = 'block';
                }
                else if (Status == 'Ditindak lanjut' && Role.includes('Admin SPP')) {
                    document.getElementById('btn_ResponFinal').style.display = 'block';
                    document.getElementById('fu_Keterangan_Respon_Filename').style.display = 'block';
                    document.getElementById('btn_clear_fu_Keterangan_Respon_Filename').style.display = 'block';
                }

                if (Role.includes('Admin SPP')) {
                    document.getElementById('div_respon_Admin_SPP').style.display = 'block';
                }

                if ((Status == "Diproses" || Status == "Ditolak Delegator" || Status == "Dihentikan" || Status == "Ditindak lanjut" || Status == "Selesai")) {
                    document.getElementById('div_respon_Delegator').style.display = 'block';
                    document.getElementById('div_respon_Admin_SPP').style.display = 'block';
                }

                if ((Status == "Dihentikan" || Status == "Ditindak lanjut" || Status == "Selesai") && Role.includes('Admin SPP')) {
                    document.getElementById('div_respon_Final').style.display = 'block';
                    document.getElementById('div_respon_Delegator').style.display = 'block';
                }

                if (Status == 'Ditolak Admin SPP') {
                    document.getElementById('ddl_Status_Respon').value = 'Ditolak Admin SPP';
                }
                else if (Status == 'Diproses') {
                    document.getElementById('ddl_Status_Respon').value = 'Diproses';
                    if (data.DelegatorID) {
                        document.getElementById('ddl_Delegator').value = data.DelegatorID;
                        document.getElementById('ddl_Delegator').style.display = 'none';
                        document.getElementById('DelegatorName').style.display = 'block';
                        document.getElementById('DelegatorName').value = data.DelegatorName;
                    }
                }

                SetStatus_Respon(Status);

                if (data.DelegatorID) {
                    document.getElementById('tab_tanggapan_internal').style.display = 'block';
                    document.getElementById('ddl_Status_Respon').value = "Diproses";
                    if (Status == "Ditolak Delegator") {
                        document.getElementById('ddl_Status_Respon').value = "";
                    }
                }
                else {
                    if (Status == "Ditolak Admin SPP") {
                        document.getElementById('ddl_Status_Respon').value = "Ditolak Admin SPP";
                    }
                }

                if (data.Keterangan_Pemeriksaan) {
                    if (Status == "Selesai" || Status == "Ditindak lanjut")
                        document.getElementById('ddl_Status_Delegator').value = "Ditindak lanjut";
                    else if (Status == "Dihentikan" || Status == "Ditolak Admin SPP")
                        document.getElementById('ddl_Status_Delegator').value = "Dihentikan";
                    else if (Status == "Ditolak Delegator")
                        document.getElementById('ddl_Status_Delegator').value = "Ditolak Delegator";
                }

                if (data.Jenis_Pelanggaran) {
                    document.getElementById('Jenis_Pelanggaran_Input').value = data.Jenis_Pelanggaran.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                    document.getElementById('Jenis_Pelanggaran_Input').setAttribute("disabled", "disabled");
                }

                document.getElementById('Jenis_Pelanggaran').value = data.Jenis_Pelanggaran.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                document.getElementById('txt_Keterangan_Penyaluran').value = data.Keterangan_Penyaluran.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                document.getElementById('txt_Keterangan_Pemeriksaan').value = data.Keterangan_Pemeriksaan.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                document.getElementById('txt_Keterangan_Konfirmasi').value = data.Keterangan_Konfirmasi.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                document.getElementById('txt_Keterangan_Respon').value = data.Keterangan_Respon.replace(/</g, "&lt;").replace(/>/g, "&gt;");

                if (data.PenyaluranByDate) {
                    if (Status != "Ditolak Delegator") {
                        document.getElementById('PenyaluranByDate').textContent = data.PenyaluranByDate;
                        document.getElementById("ddl_Status_Respon").setAttribute("disabled", "disabled");
                        document.getElementById("txt_Keterangan_Penyaluran").setAttribute("disabled", "disabled");
                        document.getElementById("ddl_Delegator").setAttribute("disabled", "disabled");
                        document.getElementById("Jenis_Pelanggaran").setAttribute("disabled", "disabled");
                    }
                }


                if (data.TindakLanjutByDate) {
                    document.getElementById('TindakLanjutByDate').textContent = data.TindakLanjutByDate;
                    document.getElementById("ddl_Status_Delegator").setAttribute("disabled", "disabled");
                    document.getElementById("txt_Keterangan_Pemeriksaan").setAttribute("disabled", "disabled");
                    document.getElementById("txt_Keterangan_Konfirmasi").setAttribute("disabled", "disabled");
                }


                if (data.ResponByDate) {
                    document.getElementById('ResponByDate').textContent = data.ResponByDate;
                    document.getElementById("ddl_Status_Final").setAttribute("disabled", "disabled");
                    document.getElementById("txt_Keterangan_Respon").setAttribute("disabled", "disabled");
                }


                if (data.FilepathFilePenyaluran) {
                    var Ekstension = data.Keterangan_Penyaluran_Ekstension.toLowerCase();
                    var div = document.getElementById('div_fu_Keterangan_Penyaluran_Filename'); div.style.display = 'block';
                    var sb = '<a target="_blank" href="../Home/ShowFile?Filename=' + data.Keterangan_Penyaluran_Filename + '&Extension=' + Ekstension + '" class="btn btn-sm btn-outline-primary" style="cursor: pointer">File Lampiran</a>';
                    if (Ekstension == '.jpg' || Ekstension == '.png' || Ekstension == '.jpeg')
                        sb = '<a onclick="show_image(\'' + data.FilepathFilePenyaluran + '\', \'Foto Lampiran\')" class="btn btn-sm btn-outline-primary" style="cursor: pointer">Foto Lampiran</a>';

                    div.innerHTML = sb;
                } else if (data.PenyaluranByDate) { document.getElementById('form_File_Keterangan_Penyaluran_Filename').style.display = 'none'; }

                if (data.FilepathFilePemeriksaan) {
                    var Ekstension = data.Keterangan_Pemeriksaan_Ekstension.toLowerCase();
                    var div = document.getElementById('div_fu_Keterangan_Pemeriksaan_Filename'); div.style.display = 'block';
                    var sb = '<a target="_blank" href="../Home/ShowFile?Filename=' + data.Keterangan_Pemeriksaan_Filename + '&Extension=' + Ekstension + '" class="btn btn-sm btn-outline-primary" style="cursor: pointer">File Lampiran</a>';
                    if (Ekstension == '.jpg' || Ekstension == '.png' || Ekstension == '.jpeg')
                        sb = '<a onclick="show_image(\'' + data.FilepathFilePemeriksaan + '\', \'Foto Lampiran\')" class="btn btn-sm btn-outline-primary" style="cursor: pointer">Foto Lampiran</a>';

                    div.innerHTML = sb;
                } else if (data.TindakLanjutByDate) { document.getElementById('form_File_Keterangan_Pemeriksaan_Filename').style.display = 'none'; }

                if (data.FilepathFileKonfirmasi) {
                    var Ekstension = data.Keterangan_Konfirmasi_Ekstension.toLowerCase();
                    var div = document.getElementById('div_fu_Keterangan_Konfirmasi_Filename'); div.style.display = 'block';
                    var sb = '<a target="_blank" href="../Home/ShowFile?Filename=' + data.Keterangan_Konfirmasi_Filename + '&Extension=' + Ekstension + '" class="btn btn-sm btn-outline-primary" style="cursor: pointer">File Lampiran</a>';
                    if (Ekstension == '.jpg' || Ekstension == '.png' || Ekstension == '.jpeg')
                        sb = '<a onclick="show_image(\'' + data.FilepathFileKonfirmasi + '\', \'Foto Lampiran\')" class="btn btn-sm btn-outline-primary" style="cursor: pointer">Foto Lampiran</a>';

                    div.innerHTML = sb;
                } else if (data.TindakLanjutByDate) { document.getElementById('form_File_Keterangan_Konfirmasi_Filename').style.display = 'none'; }

                if (data.FilepathFileRespon) {
                    var Ekstension = data.Keterangan_Respon_Ekstension.toLowerCase();
                    var div = document.getElementById('div_fu_Keterangan_Respon_Filename'); div.style.display = 'block';
                    var sb = '<a target="_blank" href="../Home/ShowFile?Filename=' + data.Keterangan_Respon_Filename + '&Extension=' + Ekstension + '" class="btn btn-sm btn-outline-primary" style="cursor: pointer">File Lampiran</a>';
                    if (Ekstension == '.jpg' || Ekstension == '.png' || Ekstension == '.jpeg')
                        sb = '<a onclick="show_image(\'' + data.FilepathFileRespon + '\', \'Foto Lampiran\')" class="btn btn-sm btn-outline-primary" style="cursor: pointer">Foto Lampiran</a>';

                    div.innerHTML = sb;
                } else if (data.ResponByDate) { document.getElementById('form_File_Keterangan_Respon_Filename').style.display = 'none'; }

                var Keterangan_Status = Status;
                if (Status == "Terkirim")
                    Keterangan_Status = "Terkirim, menunggu Respon Admin SPP";
                else if (Status == "Diproses")
                    Keterangan_Status = "Menunggu Respon Delegator";
                else if (Status == "Ditolak Delegator")
                    Keterangan_Status = "Ditolak Delegator, Menunggu delegasi ulang oleh Admin SPP";
                else if (Status == "Dihentikan")
                    Keterangan_Status = "Dihentikan, Menunggu finalisasi Admin SPP";
                else if (Status == "Ditindak lanjut")
                    Keterangan_Status = "Sudah ditindak lanjut, menunggu finalisasi Admin SPP";

                var els = document.getElementsByClassName("sp_Nomor");
                for (var i = 0; i < els.length; i++) {
                    els[i].textContent = "Nomor: " + data.Nomor + " | Status Pengaduan: " + Keterangan_Status;
                }

                if (Status == "Selesai")
                    document.getElementById('div_input_Tanggapan').style.display = 'none';

                if (data.FileEvidence != null) {
                    load_file_evidence(ID);
                }

                if (Status == "Diproses" && !Role.includes("Delegator") && !data.TindakLanjutByDate) {
                    document.getElementById('div_respon_Delegator').style.display = 'none';
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

function FillDaftarTerlapor(ID_Header) {
    var action = $('#Action').val();
    $('#tbl_daftar_terlapor').DataTable({
        destroy: true,
        order: [1, 'asc'],
        columns: [
            {
                data: '', defaultContent: '', class: 'tdbutton',
                render: function (data, type, full, meta) {
                    if (action == 'proses') {
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
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null

                }
            },
            {
                data: 'NomorHandphone', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (data != null) return data.replace(/</g, "&lt;").replace(/>/g, "&gt;"); else return null

                }
            },
            {
                data: 'Departemen', defaultContent: '',
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
                data: '', defaultContent: '',
                render: function (data, type, full, meta) {
                    if (full.FilepathFileIdentitas != null) {
                        return '<a onclick="show_image(\'' + full.FilepathFileIdentitas + '\', \'Foto Identitas Pelapor\')" class="badge badge-danger ml-2" style="cursor: pointer">View</a>';
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
            Refresh_daftar_terlapor(ID_Header);
            $('#div_daftar_terlapor').fadeIn();
        }
    });
}

function Refresh_daftar_terlapor(ID_Header) {
    $.ajax({
        url: VP + 'Pengaduan/GetDetailPengaduanByIDHeader',
        data: {
            ID_Header: ID_Header
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
                        var div = document.getElementById('File_Bukti_Pendukung');
                        var num = i + 1;
                        var sb = '<a target="_blank" href="../Home/ShowFile?Filename=' + Result.Message[i].FileEvidence + '&Extension=' + Result.Message[i].FileEvidence_Ekstension + '" class="badge badge-primary ml-2 mb-2" style="cursor: pointer">File Evidence (' + num + ')</a>';
                        if (Result.Message[i].FileEvidence_Ekstension == '.jpg' || Result.Message[i].FileEvidence_Ekstension == '.png' || Result.Message[i].FileEvidence_Ekstension == '.jpeg')
                            sb = '<a onclick="show_image(\'' + Result.Message[i].FilepathFileEvidence + '\', \'Foto Evidence (' + num + ')\')" class="badge badge-primary ml-2 mb-2" style="cursor: pointer">Foto Evidence (' + num + ')</a>';

                        div.innerHTML += sb;
                    }
                }
            }
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
                document.getElementById('txt_Nama_Terlapor').value = data.Nama.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                if (data.NomorHandphone != null)
                    document.getElementById('txt_Handphone_Terlapor').value = data.NomorHandphone.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                if (data.Departemen != null)
                    document.getElementById('txt_Departemen_Terlapor').value = data.Departemen.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                if (data.Jabatan != null)
                    document.getElementById('txt_Jabatan_Terlapor').value = data.Jabatan.replace(/</g, "&lt;").replace(/>/g, "&gt;");
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

    DataForm.append('Email', $('#txt_Email').val());
    DataForm.append('Handphone', $('#txt_Handphone').val());

    DataForm.append('Nama', $('#txt_Nama_Terlapor').val());
    DataForm.append('NomorHandphone', $('#txt_Handphone_Terlapor').val());
    DataForm.append('Departemen', $('#txt_Departemen_Terlapor').val());
    DataForm.append('Jabatan', $('#txt_Jabatan_Terlapor').val());
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
        txt_Email: { AntiXSS: true, AntiHTML: true },
        Sumber: { required: true, AntiXSS: true, AntiHTML: true },
        txt_Handphone: { AntiXSS: true, AntiHTML: true },

        Jenis_Pelanggaran_Input: { required: true, AntiXSS: true, AntiHTML: true },
        txt_TempatKejadian: { required: true, AntiXSS: true, AntiHTML: true },
        txt_WaktuKejadian: { required: true, AntiXSS: true, AntiHTML: true },
        txt_Kronologi: { required: true, AntiXSS: true, AntiHTML: true },
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
        DataForm.append('Email', $('#txt_Email').val());
        DataForm.append('Handphone', $('#txt_Handphone').val());
        DataForm.append('Sumber', $('#Sumber').val());

        DataForm.append('Jenis_Pelanggaran', $('#Jenis_Pelanggaran_Input').val());
        DataForm.append('TempatKejadian', $('#txt_TempatKejadian').val());
        DataForm.append('WaktuKejadian', $('#txt_WaktuKejadian').val());
        DataForm.append('Kronologi', $('#txt_Kronologi').val());

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
                    sessionStorage.setItem("IDPengaduan", $('#ID_Header').val());

                    SubmitKuesionerValue($('#ID_Header').val());

                    CustomNotif('success|Terkirim|Terima kasih, pengaduan ini berhasil di submit dengan nomor tiket aduan : [' + Nomor + '], Silahkan melanjutkan ke tahapan proses Delegasi|window.location.href = "/Pengaduan/PengaduanForm"');
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
        DataForm.append('Tanggapan', $('#txt_Tanggapan').val());
        DataForm.append("UploadFileLampiran", $('#fu_FileLampiran')[0].files[0]);
        DataForm.append("TipePengirim", "Admin SPP");

        $.ajax({
            url: VP + 'Pengaduan/KirimTanggapanAdminSPP',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    FillTanggapan();
                    CustomNotif('success|Terkirim|Terima kasih Anda telah mengirimkan tanggapan');
                } else {
                    if (Result.Message == 'Request Entity Too Large')
                        Result.Message = 'Maximum file upload cannot exceed 20MB';
                    CustomNotif("error|Oops|" + Result.Message + "");
                }
            },
            error: function (xhr, status, error) {
                if (error == 'Request Entity Too Large')
                    error = 'Maximum file upload cannot exceed 20MB';
                CustomNotif("error|Oops|" + error + "");
            }
        })
    }
});

$('#FormTanggapan_Internal').validate({
    rules: {
        txt_Tanggapan_Internal: { required: true, AntiXSS: true, AntiHTML: true },
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

        var Role = sessionStorage.getItem('Role');
        if (Role.includes('Admin SPP'))
            Role = 'Internal - Admin SPP'
        else if (Role.includes('Delegator'))
            Role = 'Internal - Delegator'

        DataForm.append('IDPengaduan', $('#ID_Header').val());
        DataForm.append('Tanggapan', $('#txt_Tanggapan_Internal').val());
        DataForm.append("UploadFileLampiran", $('#fu_FileLampiran_Internal')[0].files[0]);
        DataForm.append("TipePengirim", Role);

        $.ajax({
            url: VP + 'Pengaduan/KirimTanggapanInternal',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    FillTanggapanInternal();
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
                var NamaPengirim = value.TipePengirim + " : @" + value.Nama;
                var isRead = '';
                if (value.IsRead == 0 && value.TipePengirim == 'Pelapor')
                    isRead = "<span class='badge badge-danger ml-2'>New</span>";

                sb += "<li>" +
                    "<div class='timeline-dots timeline-dot1 " + dot + "'></div>" +
                    "<h6 class='float-left mb-1' style='font-size: 14px;'>" + NamaPengirim + "</h6>" + isRead + "" +
                    "<small class='float-right mt-1'>" + value._Createdon + "</small>" +
                    "<div class='d-inline-block w-100'>" +
                    "<p class='text-sm text-justify'>" + value.Tanggapan.replace(/</g, "&lt;").replace(/>/g, "&gt;") + "</p>";

                if (value.FileLampiran != null) {
                    var FileLampiran = value.FileLampiran;
                    var FileLampiran_Ekstension = value.FileLampiran_Ekstension;
                    var Path = "";

                    if (FileLampiran_Ekstension == ".mp3" || FileLampiran_Ekstension == ".mpeg") {
                        Path = "../Home/GetFile?Filename=" + FileLampiran + "&Extension=" + FileLampiran_Ekstension + "";
                        sb += "<div class='iq-media-group'>" +
                            "<a href='#' class='iq-media'>" +
                            "<a href='" + Path + "' class='btn btn-outline-danger btn-sm' style='text-style: italic'><i class='fa fa-download'></i> Download Lampiran</a>" +
                            "</a>" +
                            "</div>";
                    }
                    else if (FileLampiran_Ekstension == ".jpg" || FileLampiran_Ekstension == ".jpeg" || FileLampiran_Ekstension == ".png") {
                        var title = 'File Lampiran'
                        Path = "../Home/GetFile?Filename=" + FileLampiran + "&Extension=" + FileLampiran_Ekstension + "";
                        sb += "<div class='iq-media-group'>" +
                            "<a href='#' class='iq-media'>" +
                            '<button onclick="show_image(\'' + value.FilepathFileLampiran + '\', \'' + title + '\');" class="btn btn-outline-danger btn-sm" style="text-style: italic"><i class="fa fa-download"></i> Lihat Lampiran</button>' +
                            "</a>" +
                            "</div>";
                    }
                    else {
                        Path = "../Home/ShowFile?Filename=" + FileLampiran + "&Extension=" + FileLampiran_Ekstension + "";
                        sb += "<div class='iq-media-group'>" +
                            "<a href='#' class='iq-media'>" +
                            "<a href='" + Path + "' target='_blank' class='btn btn-outline-danger btn-sm' style='text-style: italic'><i class='fa fa-download'></i> Buka Lampiran</a>" +
                            "</a>" +
                            "</div>";
                    }

                }

                sb += "</div></li>";
            })

            if (sb) {
                document.getElementById('ul_Tanggapan').innerHTML = sb.toString();
                document.getElementById('div_Riwayat_Tanggapan').style.display = 'block';
                ReadTanggapanPelapor();
            }
            else {
                document.getElementById('div_Riwayat_Tanggapan').style.display = 'none';
            }

        }
    })
}

function ReadTanggapanPelapor() {
    var ID = sessionStorage.getItem("IDPengaduan");
    $.ajax({
        url: VP + 'Pengaduan/ReadTanggapanPelapor',
        type: 'POST',
        data: {
            ID_Pengaduan: ID
        },
        success: function (res) {
            SetNotifikasi();
        }
    })
}

function FillTanggapanInternal() {
    var ID = sessionStorage.getItem("IDPengaduan");
    document.getElementById('txt_Tanggapan_Internal').value = '';
    document.getElementById('fu_FileLampiran_Internal').value = null;

    var Role = sessionStorage.getItem("Role");
    if (Role.includes('Admin SPP'))
        Role = 'Internal - Admin SPP';
    else if (Role.includes('Delegator'))
        Role = 'Internal - Delegator';

    $.ajax({
        url: VP + 'Pengaduan/GetTanggapanInternalByIDPengaduan',
        type: 'POST',
        data: {
            ID_Pengaduan: ID,
            TipePengirim: Role
        },
        success: function (res) {
            var sb = "";
            $.each(res.Message, function (data, value) {

                var dot = value.TipePengirim == "Internal - Admin SPP" ? "border-primary text-primary" : "border-danger text-danger";
                var TipePengirim = value.TipePengirim == "Internal - Admin SPP" ? "Admin SPP" : "Delegator";

                var DelegatorName = "";
                if (value.DelegatorName.length > 80 && TipePengirim == "Delegator")
                    DelegatorName = '<span title="' + value.DelegatorName + '">' + value.DelegatorName.substring(0, 80) + '...</span>';
                else if (TipePengirim == "Delegator")
                    DelegatorName = '<span>' + value.DelegatorName + '</span>';


                var NamaPengirim = TipePengirim + " : " + DelegatorName + " @" + value.Nama;
                var isRead = '';
                if (value.IsRead == 0)
                    isRead = "<span class='badge badge-danger ml-2'>New</span>";

                sb += "<li>" +
                    "<div class='timeline-dots timeline-dot1 " + dot + "'></div>" +
                    "<h6 class='float-left mb-1' style='font-size: 14px;'>" + NamaPengirim + "</h6>" + isRead + "" +
                    "<small class='float-right mt-1'>" + value._Createdon + "</small>" +
                    "<div class='d-inline-block w-100'>" +
                    "<p class='text-sm text-justify'>" + value.Tanggapan.replace(/</g, "&lt;").replace(/>/g, "&gt;") + "</p>";

                if (value.FileLampiran != null) {
                    var FileLampiran = value.FileLampiran;
                    var FileLampiran_Ekstension = value.FileLampiran_Ekstension;
                    var Path = "";

                    if (FileLampiran_Ekstension == ".mp3" || FileLampiran_Ekstension == ".mpeg") {
                        Path = "../Home/GetFile?Filename=" + FileLampiran + "&Extension=" + FileLampiran_Ekstension + "";
                        sb += "<div class='iq-media-group'>" +
                            "<a href='#' class='iq-media'>" +
                            "<a href='" + Path + "' class='btn btn-outline-danger btn-sm' style='text-style: italic'><i class='fa fa-download'></i> Download Lampiran</a>" +
                            "</a>" +
                            "</div>";
                    }
                    else if (FileLampiran_Ekstension == ".jpg" || FileLampiran_Ekstension == ".jpeg" || FileLampiran_Ekstension == ".png") {
                        var title = 'File Lampiran'
                        Path = "../Home/GetFile?Filename=" + FileLampiran + "&Extension=" + FileLampiran_Ekstension + "";
                        sb += "<div class='iq-media-group'>" +
                            "<a href='#' class='iq-media'>" +
                            '<button onclick="show_image(\'' + value.FilepathFileLampiran + '\', \'' + title + '\');" class="btn btn-outline-danger btn-sm" style="text-style: italic"><i class="fa fa-download"></i> Lihat Lampiran</button>' +
                            "</a>" +
                            "</div>";
                    }
                    else {
                        Path = "../Home/ShowFile?Filename=" + FileLampiran + "&Extension=" + FileLampiran_Ekstension + "";
                        sb += "<div class='iq-media-group'>" +
                            "<a href='#' class='iq-media'>" +
                            "<a href='" + Path + "' target='_blank' class='btn btn-outline-danger btn-sm' style='text-style: italic'><i class='fa fa-download'></i> Buka Lampiran</a>" +
                            "</a>" +
                            "</div>";
                    }

                }

                sb += "</div></li>";
            })

            if (sb) {
                document.getElementById('ul_Tanggapan_Internal').innerHTML = sb.toString();
                document.getElementById('div_Riwayat_Tanggapan_Internal').style.display = 'block';
                ReadTanggapanInternal();
            }
            else {
                document.getElementById('div_Riwayat_Tanggapan_Internal').style.display = 'none';
            }

        }
    })
}

function ReadTanggapanInternal() {
    var ID = sessionStorage.getItem("IDPengaduan");
    var Role = sessionStorage.getItem("Role");
    if (Role.includes('Admin SPP'))
        Role = 'Internal - Delegator';
    else if (Role.includes('Delegator'))
        Role = 'Internal - Admin SPP';

    $.ajax({
        url: VP + 'Pengaduan/ReadTanggapanInternal',
        type: 'POST',
        data: {
            ID_Pengaduan: ID,
            TipePengirim: Role
        },
        success: function (res) {
            SetNotifikasiInternal();
        }
    })
}

$('#formResponAdminSPP').validate({
    rules: {
        ddl_Status_Respon: { required: true, AntiXSS: true, AntiHTML: true },
        txt_Keterangan_Penyaluran: { required: true, AntiXSS: true, AntiHTML: true },
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
        DataForm.append('Status', $('#ddl_Status_Respon').val());
        DataForm.append('DelegatorID', $('#ddl_Delegator').val());
        DataForm.append('Jenis_Pelanggaran', $('#Jenis_Pelanggaran').val());
        DataForm.append('Keterangan_Penyaluran', $('#txt_Keterangan_Penyaluran').val());
        DataForm.append("Upload", $('#fu_Keterangan_Penyaluran_Filename')[0].files[0]);

        $.ajax({
            url: VP + 'Pengaduan/SavePenyaluran',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    var status = $('#ddl_Status_Respon').val();
                    if (status == 'Diproses')
                        CustomNotif('success|Responded|Terima kasih, pengaduan ini berhasil direspon dan sudah di delegasikan|window.location.href = "/Pengaduan/PengaduanForm?v=respon"');
                    else if (status == 'Ditolak Admin SPP')
                        CustomNotif('success|Responded|Terima kasih, pengaduan ini berhasil direspon dengan penolakan|window.location.href = "/Pengaduan/PengaduanForm?v=respon"');
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

$('#formResponDelegator').validate({
    rules: {
        txt_Keterangan_Pemeriksaan: { required: true, AntiXSS: true, AntiHTML: true },
        txt_Keterangan_Konfirmasi: { required: true, AntiXSS: true, AntiHTML: true },
        ddl_Status_Delegator: { required: true, AntiXSS: true, AntiHTML: true },
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
        DataForm.append('Status', $('#ddl_Status_Delegator').val());
        DataForm.append('Keterangan_Pemeriksaan', $('#txt_Keterangan_Pemeriksaan').val());
        DataForm.append('Keterangan_Konfirmasi', $('#txt_Keterangan_Konfirmasi').val());
        DataForm.append("UploadPemeriksaan", $('#fu_Keterangan_Pemeriksaan_Filename')[0].files[0]);
        DataForm.append("UploadKonfirmasi", $('#fu_Keterangan_Konfirmasi_Filename')[0].files[0]);

        $.ajax({
            url: VP + 'Pengaduan/Save_TindakLanjut',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    var status = $('#ddl_Status_Delegator').val();
                    if (status == 'Ditindak lanjut')
                        CustomNotif('success|Berhasil|Terima kasih, pengaduan ini sudah di tindak lanjuti|window.location.href = "/Pengaduan/PengaduanForm?v=respon"');
                    else if (status == 'Ditolak Delegator')
                        CustomNotif('success|Berhasil|Terima kasih, pengaduan ini sudah dikembalikan kepada Admin SPP untuk peninjauan ulang|window.location.href = "/Pengaduan/PengaduanForm?v=respon"');
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

function HentikanDelegator() {
    var DataForm = new FormData();

    DataForm.append('ID', $('#ID_Header').val());
    DataForm.append('Status', 'Dihentikan');
    DataForm.append('Keterangan_Pemeriksaan', $('#txt_Keterangan_Pemeriksaan').val());
    DataForm.append('Keterangan_Konfirmasi', $('#txt_Keterangan_Konfirmasi').val());
    DataForm.append("UploadPemeriksaan", $('#fu_Keterangan_Pemeriksaan_Filename')[0].files[0]);
    DataForm.append("UploadKonfirmasi", $('#fu_Keterangan_Konfirmasi_Filename')[0].files[0]);

    $.ajax({
        url: VP + 'Pengaduan/Save_TindakLanjut',
        data: DataForm,
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (Result) {
            if (Result.Error == false) {
                CustomNotif('success|Dihentikan|Terima kasih, penanganan pengaduan ini sudah dihentikan, selanjutnya Admin SPP akan melakukan finalisasi|window.location.href = "/Pengaduan/PengaduanForm"');
            } else {
                CustomNotif("error|Oops|" + Result.Message + "");
            }
        },
        error: function (xhr, status, error) {
            CustomNotif("error|Oops|" + error + "");
        }
    })
}

$('#formResponFinal').validate({
    rules: {
        ddl_Status_Final: { required: true, AntiXSS: true, AntiHTML: true },
        txt_Keterangan_Respon: { required: true, AntiXSS: true, AntiHTML: true },
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
        DataForm.append('Status', $('#ddl_Status_Final').val());
        DataForm.append('Keterangan_Respon', $('#txt_Keterangan_Respon').val());
        DataForm.append("Upload", $('#fu_Keterangan_Respon_Filename')[0].files[0]);

        $.ajax({
            url: VP + 'Pengaduan/SaveRespon',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    var status = $('#ddl_Status_Final').val();
                    if (status == 'Selesai')
                        CustomNotif('success|Finalisasi|Terima kasih, pengaduan ini berhasil diselesaikan|window.location.href = "/Pengaduan/PengaduanForm?v=respon"');
                    else if (status == 'Ditolak Admin SPP')
                        CustomNotif('success|Penolakan|Terima kasih, pengaduan ini berhasil direspon dengan penolakan|window.location.href = "/Pengaduan/PengaduanForm?v=respon"');
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

function SetDisabledEmail() {
    var DisabledEmail = document.getElementById('DisabledEmail').checked;
    var Email = $('#txt_Email').val();
    if (Email == '') {
        document.getElementById('div_DisabledEmail').style.display = 'inline';
        $('#txt_Email').val('');
        if (DisabledEmail == 1) {
            //var txt_Email = $('txt_Email');
            $('#txt_Email').prop('required', false);
            $('#txt_Email').prop('readonly', true);
            //txt_Email.required = false;
            //txt_Email.val('');
            //txt_Email.attr('readonly', '');
            document.getElementById('lbl_txt_Email').innerText = "Email";
        }
        else {
            $('#txt_Email').prop('required', true);
            $('#txt_Email').prop('readonly', false);
            //var txt_Email = $('txt_Email');
            //txt_Email.attr("required", '');
            //txt_Email.val('');
            //txt_Email.removeAttribute('readonly');
            document.getElementById('lbl_txt_Email').innerText = "Email *)";
        }
    }
    else {
        document.getElementById('div_DisabledEmail').style.display = 'none';
    }
}

function SetResponDelegator() {
    var ddl_Status_Delegator = document.getElementById('ddl_Status_Delegator').value;
    if (ddl_Status_Delegator == 'Dihentikan' || ddl_Status_Delegator == 'Ditolak Delegator') {
        document.getElementById('small_Keterangan_Pemeriksaan').innerText = "Keterangan untuk Admin SPP *)";
    }
    else
        document.getElementById('small_Keterangan_Pemeriksaan').innerText = "Keterangan Hasil Investigasi *)";
}

function SetClearButton() {
    var file_input_index = 0;
    $('input[type=file]').each(function (e) {
        console.log($('input[type=file]')[e].id);
        file_input_index++;
        $(this).wrap('<div id="file_input_container_' + file_input_index + '"></div>');
        $(this).after('<input type="button" id="btn_clear_' + $('input[type=file]')[e].id + '" class="ClearFile" style="font-size: 12px;" value="Clear" onclick="reset_html(\'file_input_container_' + file_input_index + '\')" />');
    });
}