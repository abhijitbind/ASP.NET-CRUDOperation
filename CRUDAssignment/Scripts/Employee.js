$(document).ready(function(){
       
});

$(function () {
    $("#loaderbody").addClass("hide");

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass("hide");
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass("hide");
    })
})

function addEmpPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid())
    {
        debugger;
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {                
                $("#firstTab").html(response.html);
                refreshAddNewTab($(form).attr('data-resetUrl'), true);
                $.notify(response.message, "success");
                if (typeof activateDataTable !== undefined && $.isFunction(activateDataTable))
                    activateDataTable();
                } else {
                    $.notify(response.message, "error");
                }
            }
        }
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);
    }
    return false;
}

function refreshAddNewTab(resetUrl, showViewTab) {
    debugger
    $.ajax({
        type: 'get',
        url: resetUrl,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New');
            if (showViewTab)
                $('ul.nav.nav-tabs a:eq(0)').tab('show');
        }
    });
}

function Edit(url) {
    debugger;
    $.ajax({
        type: 'get',
        url: url,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Edit');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');
        }
    });
}

function Delete(url) {
    if (confirm("Are you sure to delete this record ?") == true) {
        $.ajax({
            type: 'post',
            url: url,
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    $.notify(response.message, "warn");
                    if (typeof activateDataTable !== undefined && $.isFunction(activateDataTable))
                        activateDataTable();
                   
                } else {
                    $.notify(response.message, "error");
                }
               
            }
        });
    }
}
