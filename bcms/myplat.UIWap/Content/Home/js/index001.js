var isRequesting = true;
var page = 2;
var indexNum = 0;
$(document).ready(function () {
    //懒加载
    lazyDownload("#mainulist");
});
$(window).scroll(function () {
    //懒加载
    lazyDownload("#mainulist");
    //滚动条加载更多
    if (isRequesting) {
        var bot = 160;
        var scrolltop = $(window).scrollTop();
        var scrollHeight = $(document).height();
        var windowHeight = $(window).height();
        if (scrolltop + bot >= scrollHeight - windowHeight) {
            getMoreArticlesForList();
            //懒加载
            lazyDownload("#mainulist");
        }
    }
});
function getMoreArticlesForList() {
    var umparam = getParameterByName("umparam");
    var url = this.location.href;
    var viewName = url.substring(url.substring(0, url.indexOf('?')).lastIndexOf('/') + 1, url.substring(0, url.indexOf('?')).length);
    $.ajax({
        url: "/Home/GetMoreArticlesForList?p=" + page,
        async: false,
        cache: true,
        success: function (data) {
            var html = '';
            if (data == undefined || data == "") {
                html = '<h1 class="moremes">没有更多文章</h1>';
                isRequesting = false;
            }
            else {
                page++;
                var obj = JSON.parse(data);
                indexNum = 10 * (page-1);
                for (var i = 0; i < obj.length; i++) {
                    indexNum++;
                    var imgUrl = "#";
                    if (obj[i].CoverImgList != undefined && obj[i].CoverImgList.length > 0) {
                        imgUrl = obj[i].CoverImgList[0];
                    }
                    if (viewName == '')
                    {
                        viewName = "DetailClear";
                    }
                    var href = "/Home/" + viewName + "?id=" + obj[i].Id + "&RowNumber=" + obj[i].RowNumber + "&umparam=" + umparam;
                    html += '<li><a  href="' + href + '">'
                    if (imgUrl != '#') {
                        html += '   <img src="'
                            + imgUrl + '">'
                    }
                    html+='<h2>'   + RepPlus(obj[i].Title) + '</h2></a></li>';
                };
            }
            $('#mainulist').append(html);
               
        },
    });
}
function RepPlus(str) {
    if (str == null || str == undefined) {
        return '';
    }
    else {
        return str.replace(/\+/g, '%20');
    }
}

function lazyDownload(selector) {
    var sh = $(window).height();
    var imgs = $(selector).find("img[data-src]");

    imgs.each(function () {
        var self = $(this);
        var a = self.offset().top;
        var h = sh + $(document).scrollTop() - a;

        if (h >= -100) {
            self.attr("src", self.attr("data-src"));
            self.removeAttr("data-src");
            self.removeAttr("height");
        }
    });
}