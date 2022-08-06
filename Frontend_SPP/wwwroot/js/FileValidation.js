function validateimage(fuData) {
    var msg = '';
    var isValid = false;
    if (fuData.value == '') {
        isValid = false;
    }
    else {
        var size = fuData.files[0].size;
        var SizeMB = (size / 1024) / 1000;
        if (SizeMB > 20) {
            msg = "Maximum file upload cannot exceed 20MB";
        }
        else {
            var Extension = fuData.value.substring(fuData.value.lastIndexOf('.') + 1).toLowerCase();
            if (Extension == "png" || Extension == "jpeg" || Extension == "jpg") {
                isValid = true;
            } else {
                msg = "Invalid file format";
            }
        }
    }
    if (isValid == false) {
        if (msg != '')
            alert(msg);
        fuData.value = '';
    }
}

function validatefile(fuData) {
    var isValidEkstension = 0;
    var msg = "";
    var isValid = false;
    if (fuData.value == '') {
        fuData.value = '';
        alert('File is empty');
    }
    else {

        var Extension = fuData.value.substring(fuData.value.lastIndexOf('.') + 1).toLowerCase();
        $.ajax({
            type: "POST",
            url: VP + 'Pengaduan/CheckFileEkstension?eks=' + Extension,
            dataType: "json", contentType: "application/json",
            success: function (res) {
                isValidEkstension = res.Message;
                var MaxUploadSize = res.MaxUploadSize;

                var size = 0;
                for (var i = 0; i < fuData.files.length; i++) {
                    var currentMB = fuData.files[i].size / 1024 / 1000;
                    size = size + currentMB
                }
                if (size > MaxUploadSize) {
                    msg = "Maximum file upload cannot exceed " + MaxUploadSize + " MB";
                }
                else {
                    if (isValidEkstension == 0)
                        msg = "File format is not valid";
                    else
                        isValid = true;
                }

                if (isValid == false) {
                    fuData.value = '';
                    if (msg != "")
                        alert(msg);
                }
            }
        });
    }


}
