$(document).ready(function () {
    FillForm();
});

function FillForm() {
    FillForm_ID();
    FillForm_EN();
}

function FillForm_ID() {
    var Tipe = $('#Tipe').val();
    $.ajax({
        url: VP + 'CMS/Get_Single_CMS',
        type: 'POST',
        data: {
            Tipe: Tipe,
            Lang: 'ID'
        },
        success: function (Result) {
            if (Result.Error == false) {

                if (Result.RowCount > 0) {
                    document.getElementById('Action').value = 'edit';

                    var data = Result.Message[0];
                    document.getElementById('ID').value = data.ID;

                    document.getElementById('Title_ID').value = data.Title.replace(/</g, '&lt;').replace(/>/g, '&gt;');

                    if (data.Filename) {
                        var path = data.Filename;
                        document.getElementById('div_Preview_ID').style.display = 'block';
                        Path = "../Home/sef?Filename=" + data.Filename + "&Extension=" + data.Ekstension + "";
                        var sb = "<div class='iq-media-group'>" +
                            "<a href='#' class='iq-media'>" +
                            "<a href='" + Path + "' target='_blank' class='btn btn-outline-danger btn-sm' style='text-style: italic'><i class='fa fa-download'></i> Buka Lampiran</a>" +
                            "</a>" +
                            "</div>";
                        document.getElementById('div_Preview_ID').innerHTML = sb;
                    }
                    else {
                        document.getElementById('Preview_ID').src = "";
                        document.getElementById('div_Preview_ID').style.display = 'none';
                    }
                         
                }
                else {
                    document.getElementById('Action').value = 'add';
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

function FillForm_EN() {
    var Tipe = $('#Tipe').val();
    $.ajax({
        url: VP + 'CMS/Get_Single_CMS',
        type: 'POST',
        data: {
            Tipe: Tipe,
            Lang: 'EN'
        },
        success: function (Result) {
            if (Result.Error == false) {

                if (Result.RowCount > 0) {
                    var data = Result.Message[0];

                    document.getElementById('Title_EN').value = data.Title.replace(/</g, '&lt;').replace(/>/g, '&gt;');

                    if (data.Filename) {
                        document.getElementById('div_Preview_EN').style.display = 'block';                        
                        Path = "../Home/sef?Filename=" + data.Filename + "&Extension=" + data.Ekstension + "";
                        var sb = "<div class='iq-media-group'>" +
                            "<a href='#' class='iq-media'>" +
                            "<a href='" + Path + "' target='_blank' class='btn btn-outline-danger btn-sm' style='text-style: italic'><i class='fa fa-download'></i> Buka Lampiran</a>" +
                            "</a>" +
                            "</div>";
                        document.getElementById('div_Preview_EN').innerHTML = sb;
                    }
                    else {
                        document.getElementById('Preview_EN').src = "";
                        document.getElementById('div_Preview_EN').style.display = 'none';
                    }
                }
                else {
                    document.getElementById('Action').value = 'add';
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

$('#FormInput').validate({
    rules: {
        Title_ID: { required: true, AntiXSS: true, AntiHTML: true },
        Filename_ID: { AntiXSS: true, AntiHTML: true },

        Title_EN: { required: true, AntiXSS: true, AntiHTML: true },
        Filename_EN: { AntiXSS: true, AntiHTML: true },
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
        var upload = $('#Filename_ID')[0].files[0];
        var Action = $('#Action').val();
        if (!upload && Action == 'add') {
            CustomNotif("error|Oops|File PDF tidak valid|return();");
        }
        else {
            var DataForm = new FormData();
            DataForm.append('ID', $('#ID').val());
            DataForm.append('Action', $('#Action').val());
            DataForm.append('Tipe', $('#Tipe').val());

            DataForm.append('Title_ID', $('#Title_ID').val());

            DataForm.append('Title_EN', $('#Title_EN').val());

            DataForm.append("Upload_ID", $('#Filename_ID')[0].files[0]);
            DataForm.append("Upload_EN", $('#Filename_EN')[0].files[0]);

            $.ajax({
                url: VP + 'CMS/Save_CMS',
                data: DataForm,
                type: 'POST',
                contentType: false,
                processData: false,
                success: function (Result) {
                    if (Result.Error == false) {
                        FillForm();
                        CustomNotif('success|Saved|Perubahan berhasil disimpan');
                    } else {
                        CustomNotif("error|Oops|" + Result.Message + "");
                    }
                },
                error: function (xhr, status, error) {
                    CustomNotif("error|Oops|" + error + "");
                }
            })
        }
        
    }
});

function ValidateFilePDF(oInput) {
    var isValid = false;
    if (oInput.type == "application/pdf") {
        isValid = true;
    }
    return isValid;
}

Filename_ID.onchange = evt => {
    const [file] = Filename_ID.files
    if (file) {
        if (ValidateFilePDF(file) == false) {
            alert("File is not valid");
            document.getElementById('Filename_ID').value = '';
        }
        else {
            document.getElementById('div_Preview_ID').style.display = 'block';
            var object = '<iframe src="' + URL.createObjectURL(file) + '" frameborder="0" style="overflow:hidden;height:100vh;width:100%" height="100vh" width="100%"></iframe>';
            document.getElementById('div_Preview_ID').innerHTML = object;

        }
    }
}

Filename_EN.onchange = evt => {
    const [file] = Filename_EN.files
    if (file) {
        if (ValidateFilePDF(file) == false) {
            alert("File is not valid");
            document.getElementById('Filename_EN').value = '';
        }
        else {
            document.getElementById('div_Preview_EN').style.display = 'block';
            var object = '<iframe src="' + URL.createObjectURL(file) + '" frameborder="0" style="overflow:hidden;height:100vh;width:100%" height="100vh" width="100%"></iframe>';
            document.getElementById('div_Preview_EN').innerHTML = object;
        }

    }
}
