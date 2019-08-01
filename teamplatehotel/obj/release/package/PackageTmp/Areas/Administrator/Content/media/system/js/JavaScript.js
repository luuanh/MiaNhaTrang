/*sự kiện khi validate và gửi form 
  biến: dialog: ID của form hoặc dialog. - dạng string.
  submitBt - là Object của button đóng form
*/
var targetClass = 'TargetAlias';
var aliasClass = 'Alias';
var loginUrl = '/Admin/Login/';
//Notify thông báo Message
function CreateMessage(message) {
    $("#container-notify").notify();
    $("#container-notify").notify("create", {
        title: 'Thông báo',
        text: message,
        speed: 600
    }, { expires: 5000});
}


//Reset form
 function resetControl(id) {
    if ($(id).length > 0) {
        $(id + ' input[type=text]').each(function () {
            $(this).val('');
        });
        $(id + ' input[type=password]').each(function () {
            $(this).val('');
        });
        $(id + ' textarea').each(function () {
            $(this).val('');
        });
    }
 }
//funtion show ImgLoading
 function ShowLoading() {
     $("#wraploadding").show();
     $("#wraploadding").css("width", $(window).width());
     $("#wraploadding").css("height", $(window).height());
     $("#imgloadding ").css("top", ($(window).height() / 2) - 80);
 }

//Convert chuỗi có dấu thành không giấu
function ConvertToUnSign(obj) {
    var str = obj;
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");// tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự -
    str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-
    str = str.replace(/^\-+|\-+$/g, "");//cắt bỏ ký tự - ở đầu và cuối chuỗi
    return str;
}
//Convert Title thành alias
function AutoAlias(targetClass, aliasClass) {
    if (targetClass == null) {
        targetClass = 'TargetAlias';
    }
    if (aliasClass == null) {
        aliasClass = 'Alias';
    }
    if ($('#' + targetClass).length > 0) {
        $('#' + targetClass).keyup(function () {
            var alias = $(this).val();
            $('#' + aliasClass).val(ConvertToUnSign(alias));
        });
        //
        $('#' + aliasClass).unbind();
        $('#' + aliasClass).change(function () {
            $('#' + aliasClass).val(ConvertToUnSign($(this).val()));
        });
    } else {
        $('.' + targetClass).keyup(function () {
            var alias = $(this).val();
            $('.' + aliasClass).val(ConvertToUnSign(alias));
        });
        //
        $('.' + aliasClass).unbind();
        $('.' + aliasClass).change(function () {
            $('.' + aliasClass).val(ConvertToUnSign($(this).val()));
        });
    }
}
function BrowseServer() {
    // You can use the "CKFinder" class to render CKFinder in a page:
    var finder = new CKFinder();
    finder.basePath = '../files';	// The path for the installation of CKFinder (default = "/ckfinder/").
    finder.selectActionFunction = SetFileField;
    finder.popup();

    // It can also be done in a single line, calling the "static"
    // popup( basePath, width, height, selectFunction ) function:
    // CKFinder.popup( '../', null, null, SetFileField ) ;
    //
    // The "popup" function can also accept an object as the only argument.
    // CKFinder.popup( { basePath : '../', selectActionFunction : SetFileField } ) ;
}

//Funtion Create CKFinder
//Funtion này được gọi trong sự kiên formCreated of jtable 
function BindUpload() {
    if (!$('.button-upload').length > 0) {
        var upload;
        var textBox = $('.upload');
        for (var i = 0; i < textBox.length; i++) {
            var uploadText = $(textBox[i]);
            uploadText.css('width', uploadText.width() - 70);
            uploadText.after('<button type="button" for="' + uploadText.attr('id') + '" class="button-upload ui-widget ui-state-default ui-corner-all">Upload</button>');
            $('.button-upload').click(function () {
                upload = $(this);
                // You can use the "CKFinder" class to render CKFinder in a page:
                var finder = new CKFinder();
                finder.selectActionFunction = setFileField;
                finder.popup();
            });
            function setFileField(fileUrl) {
                $('#' + $(upload).attr('for')).val(fileUrl);
            }
        }
    }
    if (!$('.add-percent').length > 0) {
        $('.input-percent').css('width', 40).after('<span class="add-percent">%</span>');
    }
}

/*Xóa hết thông báo lỗi theo Key*/
function ClearErrorTag(tagId) {
    //tagId is name
    var flagCKeditor = false;
    if (!$('#' + tagId).length) {
        //input
        if ($('input[name="' + tagId + '"]').length) {
            tagId = $('input[name="' + tagId + '"]').attr('id');
        }
            //area
        else if ($('textarea[name="' + tagId + '"]').length) {
            var editor = $('textarea[name="' + tagId + '"]');
            if (editor.hasClass('full-ckeditor') || editor.hasClass('basic-ckeditor') || editor.hasClass('ckeditor')) {
                //ckeditor
                tagId = 'cke_' + $('textarea[name="' + tagId + '"]').attr('id');
            } else {
                tagId = $('textarea[name="' + tagId + '"]').attr('id');
            }
        }
        //selelect
        if ($('select[name="' + tagId + '"]').length) {
            tagId = $('select[name="' + tagId + '"]').attr('id');
        }
    }
    $('.' + tagId + 'formError').remove();
    $('span[data-valmsg-for="' + tagId + '"]').text('');
    $('#' + tagId).parent().removeClass('error').find('span').removeClass('check-error');
    $('#' + tagId).removeClass('error');
}
/*Thêm thông báo lỗi nhập theo Key*/
function AddErrorTag(tagId, errorMessage) {
    if (!$('#' + tagId).length) {
        //input
        if ($('input[name="' + tagId + '"]').length) {
            tagId = $('input[name="' + tagId + '"]').attr('id');
        }
            //area
        else if ($('textarea[name="' + tagId + '"]').length) {
            tagId = $('textarea[name="' + tagId + '"]').attr('id');
        }
        //select 
        if ($('select[name="' + tagId + '"]').length) {
            tagId = $('select[name="' + tagId + '"]').attr('id');
        }
    } else {
        if ($('input[name="' + tagId + '"]').hasClass('switch') || $('input[name="' + tagId + '"]').hasClass('mini-switch')) {
            $('input[name="' + tagId + '"]').next().attr('id', 'switch_' + tagId);
        }
    }
    if ($('.' + tagId + 'formError').length) {
        $('#' + tagId + 'formErrorContent').html('* ' + errorMessage + '<br/>');
        return;
    }
    $('#' + tagId).before(HtmlError(tagId, 'formId', errorMessage));
    ClearTabErrors();
}

function HtmlError(tagId, formId, errorMessage) {
    var top = 0;
    var left = 0;
    if ($('#' + tagId).length) {
        top = $('#' + tagId).position().top;
        left = $('#' + tagId).position().left;
    }
    if ($('#' + tagId).hasClass('hasDatepicker')) {
        top -= 16;
        left += 16;
    }
    return '<div class="' + tagId + 'formError formError" style="max-width:147px;opacity: 0.87; position: absolute; top: ' + top + 'px; left: ' + (left + $('#' + tagId).width() - 20) + 'px; margin-top: -45px;"><div style="min-height:30px" class="formErrorContent" id="' + tagId + 'formErrorContent">* ' + errorMessage + '<br></div><div class="formErrorArrow"><div class="line10"><!-- --></div><div class="line9"><!-- --></div><div class="line8"><!-- --></div><div class="line7"><!-- --></div><div class="line6"><!-- --></div><div class="line5"><!-- --></div><div class="line4"><!-- --></div><div class="line3"><!-- --></div><div class="line2"><!-- --></div><div class="line1"><!-- --></div></div></div>';
}
function ClearTabErrors() {
    var top = 0;
    var left = 0;
    var input = $('input');
    for (var i = 0; i < input.length; i++) {
        var temp = $(input)[i];
        if ($(temp).hasClass('error')) {
            //input
            console.log();
            var className = $(temp).attr('id');
            top = $(temp).position().top;
            left = $(temp).position().left + $(temp).width() - 20;
            if ($(temp).hasClass('hasDatepicker')) {
                top -= 16;
                left += 16;
            }
            //reset location
            $('div[class="' + className + 'formError formError"]').css('top', top).css('left', left);
        }
    }
   
}
function ClearErrorOnChange() {
    $('input.error').keyup(function () {
        ClearErrorTag($(this).attr('id'));
        $(this).removeClass('error');
    });
    $('input.error').change(function () {
        ClearErrorTag($(this).attr('id'));
        $(this).removeClass('error');
    });
}
/*Kiểm tra xem Key này có còn lỗi không
key : id của trường 
errors: mảng của lỗi khi server trả về
*/
function CheckErrorsExists(key, errors) {
    for (var i = 0; i < errors.length; i++) {
        if (errors[i].Key == key && errors[i].Value.length > 0) {
            //console.log(errors[i].Value);
            return true;
        }
    }
    return false;
}
//Hàm duyệt danh sách lỗi khi server trả về

function ReportErrors(response) {
    if (response.Errors != null) {
        for (var i = 0; i < response.Errors.length; i++) {
            if (CheckErrorsExists(response.Errors[i].Key, response.Errors)) {
                AddErrorTag(response.Errors[i].Key, response.Errors[i].Value[0]);
            } else {
                ClearErrorTag(response.Errors[i].Key);
            }
        }
        ClearErrorOnChange();
        var top = 0;
        if ($('.formErrorContent').first().length) {
            top = $('.formErrorContent').first().offset().top;
        }
        $('html,body').animate({
            scrollTop: top
        }, 'slow');
    }
}

//Tích hợp ckfunder cho input.upload;

function bindUl() {

    //if (!$('.button-upload').length > 0) {
    var upload;
    var textBox = $('.upload');
    for (var i = 0; i < textBox.length; i++) {
        console.log("da vao1");
        var uploadText = $(textBox[i]);
        uploadText.css('width', uploadText.width() - 70);
        uploadText.after('<button type="button" for="' + uploadText.attr('id') + '" class="button-upload ui-widget ui-state-default ui-corner-all">Upload</button>');
        $('.button-upload').click(function () {
            upload = $(this);
            // You can use the "CKFinder" class to render CKFinder in a page:
            var finder = new CKFinder();
            finder.basePath = '../files';
            finder.selectActionFunction = setFileField;
            finder.popup();
        });
        function setFileField(fileUrl) {
            $('#' + $(upload).attr('for')).val(fileUrl);
        }
    }
}
// Create ckeditor
function CreateCkEditor() {
    var ckeditor_right = $('textarea.basic-ckeditor-right');
    var tagId = '';
    for (var i = 0; i < ckeditor_right.length; i++) {
        tagId = $($(ckeditor_right)[i]).attr('id');
        if ($('#' + tagId).css('display') != 'none') {
            CKEDITOR.replace(tagId, {
                width: '400px',
                height: '350px',
                toolbar: [
                        { name: 'document', items: ['Source', '-'] }, // Defines toolbar group with name (used to create voice label) and items in 3 subgroups.
                        { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'SpecialChar'] },
                        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Strike', 'Underline', 'Iframe'] },
                        { name: 'links', items: ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'Link', 'Unlink', 'Anchor'] },
                        { name: 'colors', items: ['TextColor', 'BGColor', 'NumberedList', 'BulletedList'] },
                ]
            });
        }
    }

    var ckeditor = $('textarea.basic-ckeditor');
    var tagId = '';
    for (var i = 0; i < ckeditor.length; i++) {
        tagId = $($(ckeditor)[i]).attr('id');
        if ($('#' + tagId).css('display') != 'none') {
            CKEDITOR.replace(tagId, {
                width: '948px',
                height: '160px',
                toolbar: [
                        { name: 'document', items: ['Source', '-'] }, // Defines toolbar group with name (used to create voice label) and items in 3 subgroups.
                        { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'SpecialChar'] },
                        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Strike', 'Underline', 'Iframe', '-', 'RemoveFormat', 'Subscript', 'Superscript'] },
                        { name: 'links', items: ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'Link', 'Unlink', 'Anchor'] },
                        { name: 'colors', items: ['TextColor', 'BGColor', 'NumberedList', 'BulletedList'] },
                ]
            });
        }
    }
    var area = $('textarea.full-ckeditor');
    for (var j = 0; j < area.length; j++) {
        tagId = $($(area)[j]).attr('id');
        if ($('#' + tagId).css('display') != 'none') {
            CKEDITOR.replace(tagId, {
                height: '400px',
                toolbar: [
                            { name: 'document', items: ['Source'] },
                            { name: 'editing', groups: ['find', 'selection', 'spellchecker'], items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Scayt'] },
                            { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
                            { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
                            { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
                            { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
                            { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
                            { name: 'colors', items: ['TextColor', 'BGColor'] },
                            { name: 'tools', items: ['Maximize', 'ShowBlocks'] },
                            { name: 'others', items: ['-'] },
                            { name: 'about', items: ['About'] }
                ]
            });
        }
    }
}


