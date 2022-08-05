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
    var msg = "";
    var isValid = false;
    if (fuData.value == '') {
        isValid = false;
    }
    else {
        var size = 0;
        for (var i = 0; i < fuData.files.length; i++) {
            var currentByte = fuData.files[i].size;
            var currentMB = fuData.files[i].size / 1024 / 1000;
            size = size + currentMB
        }
        if (size > 20) {
            msg = "Maximum file upload cannot exceed 20MB";
        }
        else {
            isValid = true;
        }
    }
    if (isValid == false) {
        if (msg != '')
            alert(msg);
        fuData.value = '';
    }
}