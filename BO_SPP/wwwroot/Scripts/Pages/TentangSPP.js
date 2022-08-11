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
                    document.getElementById('SubTitle_ID').value = data.SubTitle.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                    document.getElementById('LabelTombol_ID').value = data.LabelTombol.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                    document.getElementById('Description_ID').value = data.Description.replace(/</g, '&lt;').replace(/>/g, '&gt;');

                    if (data.Filename) {
                        var path = data.Filename;
                        document.getElementById('div_Preview_ID').style.display = 'block';
                        document.getElementById('Preview_ID').src = path;
                    }
                    else {
                        document.getElementById('Preview_ID').src = "";
                        document.getElementById('div_Preview_ID').style.display = 'none';
                    }

                    if (data.Filename1) {
                        var Ekstension = data.Ekstension1.toLowerCase();
                        var div = document.getElementById('div_Filename1_ID');
                        var sb = '<a target="_blank" href="../Home/sef?Filename=' + data.Filename1 + '&Extension=' + Ekstension + '" class="btn btn-sm btn-outline-primary" style="cursor: pointer">Buka Lampiran</a>';
                        div.innerHTML = sb;
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
                    document.getElementById('SubTitle_EN').value = data.SubTitle.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                    document.getElementById('LabelTombol_EN').value = data.LabelTombol.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                    document.getElementById('Description_EN').value = data.Description.replace(/</g, '&lt;').replace(/>/g, '&gt;');

                    if (data.Filename) {
                        var path = data.Filename;
                        document.getElementById('div_Preview_EN').style.display = 'block';
                        document.getElementById('Preview_EN').src = path;
                    }
                    else {
                        document.getElementById('Preview_EN').src = "";
                        document.getElementById('div_Preview_ID').style.display = 'none';
                    }

                    if (data.Filename1) {
                        var Ekstension = data.Ekstension1.toLowerCase();
                        var div = document.getElementById('div_Filename1_EN');
                        var sb = '<a target="_blank" href="../Home/sef?Filename=' + data.Filename1 + '&Extension=' + Ekstension + '" class="btn btn-sm btn-outline-primary" style="cursor: pointer">Open Attachment</a>';
                        div.innerHTML = sb;
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
        SubTitle_ID: { required: true, AntiXSS: true, AntiHTML: true },
        LabelTombol_ID: { required: true, AntiXSS: true, AntiHTML: true },
        Description_ID: { required: true, AntiXSS: true, AntiHTML: true },
        Filename_ID: { AntiXSS: true, AntiHTML: true },
        Filename1_ID: { AntiXSS: true, AntiHTML: true },

        Title_EN: { required: true, AntiXSS: true, AntiHTML: true },
        SubTitle_EN: { required: true, AntiXSS: true, AntiHTML: true },
        LabelTombol_EN: { required: true, AntiXSS: true, AntiHTML: true },
        Description_EN: { required: true, AntiXSS: true, AntiHTML: true },
        Filename_EN: { AntiXSS: true, AntiHTML: true },
        Filename1_EN: { AntiXSS: true, AntiHTML: true },

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
            CustomNotif("error|Oops|Gambar sidebar tidak valid|return();");
        }
        else {
            var DataForm = new FormData();
            DataForm.append('ID', $('#ID').val());
            DataForm.append('Action', $('#Action').val());
            DataForm.append('Tipe', $('#Tipe').val());

            DataForm.append('Title_ID', $('#Title_ID').val());
            DataForm.append('SubTitle_ID', $('#SubTitle_ID').val());
            DataForm.append('LabelTombol_ID', $('#LabelTombol_ID').val());
            DataForm.append('Description_ID', $('#Description_ID').val());

            DataForm.append('Title_EN', $('#Title_EN').val());
            DataForm.append('SubTitle_EN', $('#SubTitle_EN').val());
            DataForm.append('LabelTombol_EN', $('#LabelTombol_EN').val());
            DataForm.append('Description_EN', $('#Description_EN').val());

            DataForm.append("Upload_ID", $('#Filename_ID')[0].files[0]);
            DataForm.append("Upload_EN", $('#Filename_EN')[0].files[0]);

            DataForm.append("Upload1_ID", $('#Filename1_ID')[0].files[0]);
            DataForm.append("Upload1_EN", $('#Filename1_EN')[0].files[0]);

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

function ValidateFileImage(oInput) {
    var isValid = false;
    if (oInput.type == "image/png" || oInput.type == "image/jpg" || oInput.type == "image/jpeg") {
        isValid = true;
    }
    return isValid;
}

Filename_ID.onchange = evt => {
    const [file] = Filename_ID.files
    if (file) {
        if (ValidateFileImage(file) == false) {
            alert("Image not valid");
            document.getElementById('Filename_ID').value = '';
        }
        else {
            document.getElementById('div_Preview_ID').style.display = 'block';
            Preview_ID.src = URL.createObjectURL(file);
        }
    }
}

Filename_EN.onchange = evt => {
    const [file] = Filename_EN.files
    if (file) {
        if (ValidateFileImage(file) == false) {
            alert("Image not valid");
            document.getElementById('Filename_EN').value = '';
        }
        else {
            document.getElementById('div_Preview_ID').style.display = 'block';
            Preview_EN.src = URL.createObjectURL(file);
        }

    }
}

