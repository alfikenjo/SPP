$(document).ready(function () {
    var elems = [];
    $('#field_count').val(JSON.stringify(elems));
    //var field_count = $('#field_count').val();
    //field_count = JSON.parse(field_count);

    document.getElementById('Action').value = 'add';
    var url_string = window.location.href;
    var url = new URL(url_string);
    var act = url.searchParams.get("act");
    if (act == 'e')
        document.getElementById('Action').value = 'edit';
    else if (act == 'v') {
        document.getElementById('Action').value = 'view';
        document.getElementById('div_AddNewField').style.display = 'none';
        document.getElementById('div_btn_save').style.display = 'none';
    }

    else
        act = 'add';

    var ID = sessionStorage.getItem("IDKuesioner");
    if (ID != null && ID != '') {
        FillForm(ID, act);
    }
    else {
        sessionStorage.setItem("IDKuesioner", '');
        document.getElementById('Action').value = 'add';
    }

});

function FillForm(ID, act) {

    document.getElementById('IDHeader').value = ID;

    $.ajax({
        url: VP + 'Kuesioner/Get_Kuesioner_Detail_By_IDHeader',
        type: 'POST',
        data: {
            IDHeader: ID
        },
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                if (Result.Message.length > 0) {

                    $('#Title').val(data.Title);
                    document.getElementById('Preview_Title').innerText = data.Title;
                    $('#StartDate').val(data.StartDate);
                    $('#EndDate').val(data.EndDate);
                    $('#Status').val(data.Status);

                    var arr = [];
                    for (var i = 0; i < Result.Message.length; i++) {
                        var Num = Result.Message[i].Num;
                        arr.push(Num);
                    }

                    $('#field_count').val(JSON.stringify(arr));
                    if (act == 'e')
                        $('#ActionDetail').val('edit');

                    for (var k = 0; k < arr.length; k++) {
                        if (arr[k]) {
                            BindForm(arr[k], Result.Message[k].InputType, Result.Message[k].Label, Result.Message[k].Required, Result.Message[k].Options);
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

function GetOptions(ID) {
    $.ajax({
        url: VP + 'Kuesioner/Get_Kuesioner_Detail_Options_By_IDHeader',
        type: 'POST',
        data: {
            IDHeader: ID
        },
        success: function (Result) {
            if (Result.Error == false) {
                var data = Result.Message[0];
                //document.getElementById('Param_Options').value = data.Options;
                return data.Options;
            } else {
                return "";
            }
        },
        error: function (xhr, status, error) {
            return "";
        }
    })
}

function SetTitle() {
    var Preview_Title = 'Form Title';
    var Title = $('#Title').val();
    if (Title != '') {
        Preview_Title = Title;
        document.getElementById('Card_Title').style.display = 'block';
        document.getElementById('Preview_Title').innerHTML = Preview_Title;
    }
    else {
        document.getElementById('Preview_Title').innerHTML = Preview_Title;
    }

}

Element.prototype.remove = function () {
    this.parentElement.removeChild(this);
}
NodeList.prototype.remove = HTMLCollection.prototype.remove = function () {
    for (var i = this.length - 1; i >= 0; i--) {
        if (this[i] && this[i].parentElement) {
            this[i].parentElement.removeChild(this[i]);
        }
    }
}

function setButtonSave() {
    var arr = $('#field_count').val();
    arr = JSON.parse(arr);
    if (arr.length > 0) {
        document.getElementById('div_button_save').style.display = 'block';
        document.getElementById('Card_Title').style.display = 'block';
    }
    else
        document.getElementById('div_button_save').style.display = 'none';
}

function remove_element(i) {
    var old_div_preview_group = document.getElementById('div_preview_group_' + i);
    if (old_div_preview_group)
        old_div_preview_group.remove();

    var div_type = document.getElementById('div_type_' + i);
    if (div_type)
        div_type.remove();

    var hr = document.getElementById('hr_' + i);
    if (hr)
        hr.remove();


    var old_value = $('#field_count').val();
    old_value = JSON.parse(old_value);
    removeArray(old_value, i);

    $('#field_count').val(JSON.stringify(old_value));
    setButtonSave();
}

function SetType(tipe, i) {
    var div_options = document.getElementById('div_options_' + i);
    if (div_options)
        div_options.remove();

    var div_inline_checkbox = document.getElementById('div_inline_checkbox_' + i);
    if (div_inline_checkbox)
        div_inline_checkbox.remove();

    if (tipe == 'select' || tipe == 'radios' || tipe == 'checkbox') {
        var div_type = document.getElementById('div_type_' + i);

        var div = document.createElement('div');
        div.id = 'div_options_' + i;
        div.classList.add('form-group');
        div_type.appendChild(div);

        var label = document.createElement('label');
        label.innerText = 'Options';
        div.appendChild(label);

        var input = document.createElement('input');
        input.id = 'options_' + i;
        input.type = 'text';
        input.value = 'option 1, option 2, option 3';
        input.addEventListener('change', function () {
            ChangePreview(i);
        });
        input.classList.add('form-control');
        input.placeholder = 'comma separated (Option A, Option B, Option C)';
        div.appendChild(input);

    }

    ChangePreview(i);
}

function OpenOptions(i, Options) {
    var div_type = document.getElementById('div_type_' + i);

    var div = document.createElement('div');
    div.id = 'div_options_' + i;
    div.classList.add('form-group');
    div_type.appendChild(div);

    var label = document.createElement('label');
    label.innerText = 'Options';
    div.appendChild(label);

    var input = document.createElement('input');
    input.id = 'options_' + i;
    input.type = 'text';
    input.value = Options;
    input.addEventListener('change', function () {
        ChangePreview(i);
    });
    input.classList.add('form-control');
    input.placeholder = 'comma separated (Option A, Option B, Option C)';
    div.appendChild(input);
}

function getCount(parent, getChildrensChildren) {
    var relevantChildren = 0;
    var children = parent.childNodes.length;
    for (var i = 0; i < children; i++) {
        if (parent.childNodes[i].nodeType != 3) {
            if (getChildrensChildren)
                relevantChildren += getCount(parent.childNodes[i], true);
            relevantChildren++;
        }
    }
    return relevantChildren;
}

function Preview(i) {
    var old_div_preview_group = document.getElementById('div_preview_group_' + i);
    if (old_div_preview_group)
        old_div_preview_group.remove();

    var value_label = 'Label ' + i;
    var set_label = document.getElementById('label_' + i);
    if (set_label) {
        if (set_label.value)
            value_label = set_label.value;
    }

    var required = 0;
    var set_required = document.getElementById('required_' + i);
    if (set_required) {
        required = set_required.checked;
    }
    var div_preview = document.getElementById('div_preview');

    var div_preview_group = document.createElement('div');
    div_preview_group.id = 'div_preview_group_' + i;
    if (required == 1)
        div_preview_group.classList.add('form-group', 'required');
    else
        div_preview_group.classList.add('form-group');

    div_preview.appendChild(div_preview_group);

    var label_preview = document.createElement('label');
    label_preview.classList.add('control-label');
    label_preview.innerText = value_label;
    div_preview_group.appendChild(label_preview);

    var set_select = document.getElementById('select_' + i).value;
    if (set_select == 'input') {
        var input = document.createElement('input');
        input.type = 'text';
        if (required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'textarea') {
        var input = document.createElement('textarea');
        if (required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'email') {
        var input = document.createElement('input');
        input.type = 'email';
        if (required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'select') {
        var input = document.createElement('select');
        input.id = 'select_preview_' + i;
        if (required == 1)
            input.classList.add('form-control', 'required');
        else
            input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }

    var set_options = document.getElementById('options_' + i);
    if (set_options) {
        var default_option = document.createElement("option");
        default_option.value = null;
        default_option.text = '- select -';
        var default_select_preview = document.getElementById('select_preview_' + i);
        if (default_select_preview)
            default_select_preview.appendChild(default_option);

        var arr = add_element_to_array('options_' + i);
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

    setButtonSave();

}

function AddPreview(i) {
    var value_label = 'Label ' + i;
    var set_label = document.getElementById('label_' + i);
    if (set_label) {
        if (set_label.value)
            value_label = set_label.value;
    }

    var required = 0;
    var set_required = document.getElementById('required_' + i);
    if (set_required) {
        required = set_required.checked;
    }
    var div_preview = document.getElementById('div_preview');

    var div_preview_group = document.createElement('div');
    div_preview_group.id = 'div_preview_group_' + i;
    if (required == 1)
        div_preview_group.classList.add('form-group', 'required');
    else
        div_preview_group.classList.add('form-group');

    div_preview.appendChild(div_preview_group);

    var label_preview = document.createElement('label');
    label_preview.id = 'label_preview_' + i;
    label_preview.classList.add('control-label', 'text-justify');
    label_preview.innerText = value_label;
    div_preview_group.appendChild(label_preview);

    var set_select = document.getElementById('select_' + i).value;
    if (set_select == 'input') {
        var input = document.createElement('input');
        input.id = 'input_preview_' + i;
        input.type = 'text';
        input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'textarea') {
        var input = document.createElement('textarea');
        input.id = 'input_preview_' + i;
        input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'email') {
        var input = document.createElement('input');
        input.id = 'input_preview_' + i;
        input.type = 'email';
        input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'select') {
        var input = document.createElement('select');
        input.id = 'select_preview_' + i;
        input.classList.add('form-control');
        div_preview_group.appendChild(input);

        var set_options = document.getElementById('options_' + i);
        if (set_options) {
            var default_option = document.createElement("option");
            default_option.value = null;
            default_option.text = '- select -';
            var default_select_preview = document.getElementById('select_preview_' + i);
            if (default_select_preview)
                default_select_preview.appendChild(default_option);

            var arr = add_element_to_array('options_' + i);
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
        input.id = 'checkbox_preview_' + i;
        input.type = 'checkbox';
        input.classList.add('custom-control-input');
        div.appendChild(input);

        var label = document.createElement('label');
        label.classList.add('custom-control-label');
        label.htmlFor = 'checkbox_preview_' + i;
        var set_label = document.getElementById('label_' + i);
        if (set_label) {
            if (set_label.value)
                label.innerText = set_label.value;
            else
                label.innerText = 'Label ' + i;
        }


        div.appendChild(label);
    }
    else if (set_select == 'checkbox') {

        var set_options = document.getElementById('options_' + i);
        if (set_options) {

            var div_form_checkbox = document.createElement('div');
            div_form_checkbox.classList.add('form-group');
            div_form_checkbox.id = 'div_form_checkbox_' + i;
            div_preview_group.appendChild(div_form_checkbox);

            var arr = add_element_to_array('options_' + i);
            for (var k = 0; k < arr.length; k++) {
                if (arr[k] != '') {
                    var div = document.createElement('div');
                    div.id = 'input_preview_' + i + k;
                    div.classList.add('custom-control', 'custom-checkbox', 'custom-control-inline');

                    var input = document.createElement('input');
                    input.id = 'checkbox_preview_' + i + k;
                    input.type = 'checkbox';
                    input.classList.add('custom-control-input');
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

        var set_options = document.getElementById('options_' + i);
        if (set_options) {

            var div_form_radio = document.createElement('div');
            div_form_radio.classList.add('form-group');
            div_form_radio.id = 'div_form_radio_' + i;
            div_preview_group.appendChild(div_form_radio);

            var arr = add_element_to_array('options_' + i);
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



    setButtonSave();

}

function DeletePreview(i) {
    var old_div_preview_group = document.getElementById('div_preview_group_' + i);
    if (old_div_preview_group)
        old_div_preview_group.remove();

    setButtonSave();

}

function ChangePreview(i) {
    var value_label = 'Label ' + i;
    var set_label = document.getElementById('label_' + i);
    if (set_label) {
        if (set_label.value)
            value_label = set_label.value;
    }

    var required = 0;
    var set_required = document.getElementById('required_' + i);
    if (set_required) {
        required = set_required.checked;
    }

    div_preview_group = document.getElementById('div_preview_group_' + i);
    if (div_preview_group) {
        div_preview_group.classList.remove('required');
        if (required == 1)
            div_preview_group.classList.add('required');
    }

    var label_preview = document.getElementById('label_preview_' + i);
    if (label_preview)
        label_preview.innerText = value_label;
    else {
        var label_preview = document.createElement('label');
        label_preview.id = 'label_preview_' + i;
        label_preview.classList.add('control-label', 'text-justify');
        label_preview.innerText = value_label;
        div_preview_group.appendChild(label_preview);
    }

    var old_input = document.getElementById('input_preview_' + i);
    if (old_input)
        old_input.remove();

    var old_select = document.getElementById('select_preview_' + i);
    if (old_select)
        old_select.remove();

    var div_form_checkbox = document.getElementById('div_form_checkbox_' + i);
    if (div_form_checkbox)
        div_form_checkbox.remove();

    var div_form_radio = document.getElementById('div_form_radio_' + i);
    if (div_form_radio)
        div_form_radio.remove();

    var div_preview_group = document.getElementById('div_preview_group_' + i);

    var set_select = document.getElementById('select_' + i).value;
    if (set_select == 'input') {
        var input = document.createElement('input');
        input.id = 'input_preview_' + i;
        input.type = 'text';
        input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'textarea') {
        var input = document.createElement('textarea');
        input.id = 'input_preview_' + i;
        input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'email') {
        var input = document.createElement('input');
        input.id = 'input_preview_' + i;
        input.type = 'email';
        input.classList.add('form-control');
        div_preview_group.appendChild(input);
    }
    else if (set_select == 'select') {
        var input = document.createElement('select');
        input.id = 'select_preview_' + i;
        input.classList.add('form-control');        
        div_preview_group.appendChild(input);

        var set_options = document.getElementById('options_' + i);
        if (set_options) {
            var default_option = document.createElement("option");
            default_option.value = null;
            default_option.text = '- select -';
            var default_select_preview = document.getElementById('select_preview_' + i);
            if (default_select_preview)
                default_select_preview.appendChild(default_option);

            var arr = add_element_to_array('options_' + i);
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
        input.id = 'checkbox_preview_' + i;
        input.type = 'checkbox';
        input.classList.add('custom-control-input');
        div.appendChild(input);

        var label = document.createElement('label');
        label.classList.add('custom-control-label');
        label.htmlFor = 'checkbox_preview_' + i;
        var set_label = document.getElementById('label_' + i);
        if (set_label) {
            if (set_label.value)
                label.innerText = set_label.value;
            else
                label.innerText = 'Label ' + i;
        }


        div.appendChild(label);
    }
    else if (set_select == 'checkbox') {

        var set_options = document.getElementById('options_' + i);
        if (set_options) {

            var div_form_checkbox = document.createElement('div');
            div_form_checkbox.classList.add('form-group');
            div_form_checkbox.id = 'div_form_checkbox_' + i;
            div_preview_group.appendChild(div_form_checkbox);

            var arr = add_element_to_array('options_' + i);
            for (var k = 0; k < arr.length; k++) {
                if (arr[k] != '') {
                    var div = document.createElement('div');
                    div.id = 'input_preview_' + i + k;
                    div.classList.add('custom-control', 'custom-checkbox', 'custom-control-inline');

                    var input = document.createElement('input');
                    input.id = 'checkbox_preview_' + i + k;
                    input.type = 'checkbox';
                    input.classList.add('custom-control-input');
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

        var set_options = document.getElementById('options_' + i);
        if (set_options) {

            var div_form_radio = document.createElement('div');
            div_form_radio.classList.add('form-group');
            div_form_radio.id = 'div_form_radio_' + i;
            div_preview_group.appendChild(div_form_radio);

            var arr = add_element_to_array('options_' + i);
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

    setButtonSave();

}

function add_element_to_array(elemenId) {
    let splits = document.getElementById(elemenId).value.split(',');
    var notesArray = new Array();
    for (var i = 0; i < splits.length; i++) {
        notesArray.push(splits[i]);
    }
    return notesArray;
}

function GetElementInsideContainer(containerID, childID) {
    var elm = document.getElementById(childID);
    var parent = elm ? elm.parentNode : {};
    return (parent.id && parent.id === containerID) ? elm.value : {};
}

function AddNewField() {
    var element = document.getElementById("div_element");
    const divs = document.getElementsByClassName('divelement');
    var i = divs.length + 1;

    var arr = $('#field_count').val();
    arr = JSON.parse(arr);
    var largest = 0;

    for (r = 0; r <= largest; r++) {
        if (arr[r] > largest) {
            var largest = arr[r];
        }
    }

    if (largest > 0)
        i = largest + 1;

    var div_type = document.createElement('div');
    div_type.id = 'div_type_' + i;
    div_type.classList.add('divelement');
    element.appendChild(div_type);

    var div_form_row = document.createElement('div');
    div_form_row.classList.add('form-row');
    div_type.appendChild(div_form_row);

    var div_col_1 = document.createElement('div');
    div_col_1.classList.add('col-md-3', 'col-sm-12');
    div_form_row.appendChild(div_col_1);

    var div_form_group_1 = document.createElement('div');
    div_form_group_1.classList.add('form-group');
    div_col_1.appendChild(div_form_group_1);

    var label_1 = document.createElement('label');
    label_1.innerText = 'Type';
    div_form_group_1.appendChild(label_1);

    var select = document.createElement('select');
    select.id = 'select_' + i;
    select.classList.add('form-control');
    select.addEventListener('change', function () {
        SelectOnChange(i);
    });
    div_form_group_1.appendChild(select);

    var div_col_2 = document.createElement('div');
    div_col_2.classList.add('col-md-6', 'col-sm-12');
    div_form_row.appendChild(div_col_2);

    var div_form_group_2 = document.createElement('div');
    div_form_group_2.classList.add('form-group');
    div_col_2.appendChild(div_form_group_2);

    var label_2 = document.createElement('label');
    label_2.innerText = 'Label';
    div_form_group_2.appendChild(label_2);

    var input = document.createElement('input');
    input.type = 'text';
    input.id = 'label_' + i;
    input.classList.add('form-control');
    input.addEventListener('change', function () {
        ChangePreview(i);
    });
    div_form_group_2.appendChild(input);

    var div_col_3 = document.createElement('div');
    div_col_3.classList.add('col-md-2', 'col-sm-6');
    div_form_row.appendChild(div_col_3);

    var div_required = document.createElement('div');
    div_required.classList.add('custom-control', 'custom-switch', 'custom-switch-color', 'custom-control-inline', 'margin-top-35');
    div_col_3.appendChild(div_required);

    var input_required = document.createElement('input');
    input_required.type = 'checkbox';
    input_required.id = 'required_' + i;
    input_required.classList.add('custom-control-input', 'bg-primary');
    input_required.addEventListener('change', function () {
        ChangePreview(i);
    });
    div_required.appendChild(input_required);

    var label_3 = document.createElement('label');
    label_3.innerText = 'Required';
    label_3.htmlFor = 'required_' + i;
    label_3.classList.add('custom-control-label');
    div_required.appendChild(label_3);

    var div_toolbar = document.createElement('div');
    div_toolbar.classList.add('col-md-1', 'col-sm-6', 'card-header-toolbar', 'text-right');
    div_form_row.appendChild(div_toolbar);

    var btnDelete = document.createElement('a');
    btnDelete.addEventListener('click', function () {
        remove_element(i);
    });
    btnDelete.href = '#';
    div_toolbar.appendChild(btnDelete);

    var i_more = document.createElement('i');
    i_more.classList.add('fa', 'fa-trash', 'text-danger', 'margin-top-35');
    btnDelete.appendChild(i_more);

    var hr = document.createElement('hr');
    hr.id = 'hr_' + i;
    element.appendChild(hr);

    var select = document.getElementById('select_' + i);
    if (select) {
        var array_type = ["input", "email", "textarea", "select", "checkbox", "radios", "switch"];
        for (var j = 0; j < array_type.length; j++) {
            var option = document.createElement("option");
            option.value = array_type[j];
            option.text = array_type[j];
            select.appendChild(option);
        }
    }

    var field_count = $('#field_count').val();
    field_count = JSON.parse(field_count);
    field_count.push(i);
    $('#field_count').val(JSON.stringify(field_count));
    AddPreview(i);

}

function SelectOnChange(i) {
    var select = document.getElementById('select_' + i);
    if (select) {
        var value = select.value;
        SetType(value, i);
    }
}

function removeArray(arr) {
    var what, a = arguments, L = a.length, ax;
    while (L > 1 && arr.length) {
        what = a[--L];
        while ((ax = arr.indexOf(what)) !== -1) {
            arr.splice(ax, 1);
        }
    }
    return arr;
}

function BindForm(param_i, param_select, param_label, param_required, Options) {
    var i = param_i;
    var element = document.getElementById('div_element');

    var div_type = document.createElement('div');
    div_type.id = 'div_type_' + i;
    div_type.classList.add('divelement');
    element.appendChild(div_type);

    var div_form_row = document.createElement('div');
    div_form_row.classList.add('form-row');
    div_type.appendChild(div_form_row);

    var div_col_1 = document.createElement('div');
    div_col_1.classList.add('col-md-3', 'col-sm-12');
    div_form_row.appendChild(div_col_1);

    var div_form_group_1 = document.createElement('div');
    div_form_group_1.classList.add('form-group');
    div_col_1.appendChild(div_form_group_1);

    var label_1 = document.createElement('label');
    label_1.innerText = 'Type';
    div_form_group_1.appendChild(label_1);

    var select = document.createElement('select');
    select.id = 'select_' + i;
    select.classList.add('form-control');
    select.addEventListener('change', function () {
        SelectOnChange(i);
    });

    if (param_select == 'select' || param_select == 'checkbox' || param_select == 'radios')
        OpenOptions(i, Options);

    var array_type = ["input", "email", "textarea", "select", "checkbox", "radios", "switch"];
    for (var j = 0; j < array_type.length; j++) {
        var option = document.createElement("option");
        option.value = array_type[j];
        option.text = array_type[j];
        select.appendChild(option);
    }

    select.value = param_select;
    div_form_group_1.appendChild(select);

    var div_col_2 = document.createElement('div');
    div_col_2.classList.add('col-md-6', 'col-sm-12');
    div_form_row.appendChild(div_col_2);

    var div_form_group_2 = document.createElement('div');
    div_form_group_2.classList.add('form-group');
    div_col_2.appendChild(div_form_group_2);

    var label_2 = document.createElement('label');
    label_2.innerText = 'Label';
    div_form_group_2.appendChild(label_2);

    var input = document.createElement('input');
    input.type = 'text';
    input.id = 'label_' + i;
    input.classList.add('form-control');
    input.addEventListener('change', function () {
        ChangePreview(i);
    });
    input.value = param_label;
    div_form_group_2.appendChild(input);

    var div_col_3 = document.createElement('div');
    div_col_3.classList.add('col-md-2', 'col-sm-6');
    div_form_row.appendChild(div_col_3);

    var div_required = document.createElement('div');
    div_required.classList.add('custom-control', 'custom-switch', 'custom-switch-color', 'custom-control-inline', 'margin-top-35');
    div_col_3.appendChild(div_required);

    var input_required = document.createElement('input');
    input_required.type = 'checkbox';
    input_required.id = 'required_' + i;
    input_required.classList.add('custom-control-input', 'bg-primary');
    input_required.addEventListener('change', function () {
        ChangePreview(i);
    });
    input_required.checked = param_required;
    div_required.appendChild(input_required);

    var label_3 = document.createElement('label');
    label_3.innerText = 'Required';
    label_3.htmlFor = 'required_' + i;
    label_3.classList.add('custom-control-label');
    div_required.appendChild(label_3);

    var div_toolbar = document.createElement('div');
    div_toolbar.classList.add('col-md-1', 'col-sm-6', 'card-header-toolbar', 'text-right');
    div_form_row.appendChild(div_toolbar);

    var btnDelete = document.createElement('a');
    btnDelete.addEventListener('click', function () {
        remove_element(i);
    });
    btnDelete.href = '#';
    div_toolbar.appendChild(btnDelete);

    var i_more = document.createElement('i');
    i_more.classList.add('fa', 'fa-trash', 'text-danger', 'margin-top-35');
    btnDelete.appendChild(i_more);

    var hr = document.createElement('hr');
    hr.id = 'hr_' + i;
    element.appendChild(hr);

    AddPreview(i);

}

$('#FormBuilder').validate({
    rules: {
        Title: { required: true, AntiXSS: true, AntiHTML: true },
        StartDate: { AntiXSS: true, AntiHTML: true },
        EndDate: { AntiXSS: true, AntiHTML: true },
        Status: { required: true, AntiXSS: true, AntiHTML: true },
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

        DataForm.append('Action', $('#Action').val());
        DataForm.append('ID', $('#IDHeader').val());
        DataForm.append('Title', $('#Title').val());
        DataForm.append('StartDate', $('#StartDate').val());
        DataForm.append('EndDate', $('#EndDate').val());
        DataForm.append('Status', $('#Status').val());

        $.ajax({
            url: VP + 'Kuesioner/SaveKuesioner',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    var IDHeader = Result.Message;
                    $('#IDHeader').val(IDHeader);
                    sessionStorage.setItem("IDKuesioner", IDHeader);
                    console.log('SaveKuesioner OK: ' + IDHeader);
                    SaveKuesionerDetail(IDHeader);
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

function SaveKuesionerDetail(IDHeader) {
    var xs = $('#field_count').val();
    xs = JSON.parse(xs);

    for (const x of xs) {
        var select = document.getElementById('select_' + x).value;
        var label = document.getElementById('label_' + x).value;
        var checkbox = document.getElementById('required_' + x).checked;

        var DataForm = new FormData();
        DataForm.append('IDHeader', IDHeader);
        DataForm.append('Num', x);
        DataForm.append('InputType', select);
        DataForm.append('Label', label);
        DataForm.append('Required', checkbox);

        $.ajax({
            url: VP + 'Kuesioner/SaveKuesionerDetail',
            data: DataForm,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (Result) {
                if (Result.Error == false) {
                    var IDHeaderOptions = Result.Message;
                    console.log('SaveKuesionerDetail OK');
                    SaveOptions(IDHeaderOptions, x);
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

function SaveOptions(IDHeader, x) {
    var set_options = document.getElementById('options_' + x);
    if (set_options) {
        var arr = add_element_to_array('options_' + x);
        for (var k = 0; k < arr.length; k++) {
            if (arr[k] != '') {

                var i = k + 1;
                var DataForm = new FormData();
                DataForm.append('IDHeader', IDHeader);
                DataForm.append('Num', i);
                DataForm.append('Options', arr[k]);

                $.ajax({
                    url: VP + 'Kuesioner/Save_KuesionerDetailOptions',
                    data: DataForm,
                    type: 'POST',
                    contentType: false,
                    processData: false,
                    success: function (Result) {
                        if (Result.Error == false) {
                            var ID = $('#IDHeader').val();
                            CustomNotif('success|Form Builder|Kuesioner berhasil di simpan|window.location.href = "/Kuesioner/DaftarKuesioner"');
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
    }
    else {
        var ID = $('#IDHeader').val();
        CustomNotif('success|Form Builder|Kuesioner berhasil di simpan|window.location.href = "/Kuesioner/DaftarKuesioner"');
    }
}