
function getIdSelections(table) {
    return $.map(table.bootstrapTable('getSelections'), function (row) {
        return row.Id;
    });
};


function LoadPageMain(obj, url) {
    try {
        var ht = location.origin;
        if (url.indexOf('http://') > 0 || url.indexOf('https://') > 0 || url.indexOf('//') > 0) {
            obj.load(url, function () {
                $('.loading-box').hide();
            });
        } else {
            obj.load(ht + '/' + url, function () {
                $('.loading-box').hide();
            });
        }
    } catch (e) {
        console.log(e);
    }
}
function addSuccess(d, s, o) {
    if (s !== "success") {
        $('.result-error-message').show().text("很抱歉，请求遇到了错误。");
    }
    if (typeof d == "object") {
        if (d.Status === 0) {
            $(".modal .close").trigger('click');
            RefreshTable();
            $('.result-error-message').hide();
        } else {
            $('.result-error-message').show().text(d.Message);
        }
    } else {
        $('.result-error-message').hide();
        $(".modal .close").trigger('click');
        RefreshTable();
    }
};
function RefreshTable() {
    $('.bootstrap-table button[title="Refresh"]').trigger('click');
}

function CreateJavaScript(url, func) {
    var s1, head = document.getElementsByTagName('head')[0];
    s1 = document.createElement("script");
    s1.type = "text/javascript";
    s1.src = url;
    s1.onreadystatechange = function () {
        if (this.readyState === "complete" || this.readyState === "loaded") {
            if (typeof func != "function")
                func();
        }
    };
    s1.onload = func;
    head.appendChild(s1);
}

function createIframe(obj, url) {
    var o = obj || document.getElementById('iframetarget');
    var iframe = document.createElement('iframe');
    iframe.src = url;
    //iframe.scrolling = "no";
    iframe.frameBorder = 0;
    iframe.width = '100%';
    iframe.height = '100%';
    iframe.onload = function (e) {
        alert("d");
        var $o = $(e.target);
        $o.height(window.outerHeight - 250);
        $('.loading-box').hide();
    }
    e.removeAll();
    o.appendChild(iframe);
    
}