var isRequesting = true;
var page = 1;
var indexNum = 0;
$(document).ready(function () {
    //懒加载
    lazyDownload("#mainListContainer");
    resetImage();
    addArticleFooter();
});
$(window).scroll(function () {
    //懒加载
    lazyDownload("#mainListContainer");
    //滚动条加载更多
    if (isRequesting) {
        if (page >= 99) {
            isRequesting = false;
            return;
        }
        var bot = 260;
        var scrolltop = $(window).scrollTop();
        var scrollHeight = $(document).height();
        var windowHeight = $(window).height();
        if (scrolltop + windowHeight >= scrollHeight - windowHeight) {
            getMoreArticle();
            //懒加载
            lazyDownload("#mainListContainer");
            resetImage();
        }
    }
});
function getMoreArticle() {
    var umparam = getParameterByName("umparam");
    var t = getParameterByName("t");
    var articleId = $("#articleId").val();
    var RowNumber = $("#RowNumber").val();
    var dataType = $("#dataType").val();
    var url = this.location.href;
    var viewName = url.substring(url.substring(0, url.indexOf('?')).lastIndexOf('/') + 1, url.substring(0, url.indexOf('?')).length);
    $.ajax({
        url: "/Home/GetMoreArticleList?p=" + page + "&id=" + articleId + "&RowNumber=" + RowNumber + "&dataType=" + dataType,
        async: false,
        cache: false,
        success: function (data) {
            var html = '';
            if (data == undefined || data == "") {
                isRequesting = false;
            }
            else {
                page++;
                var obj = JSON.parse(data);
                indexNum = 10 * (page - 1);
                for (var i = 0; i < obj.length; i++) {
                    indexNum++;
                    var imgUrl = "";
                    if (i==2)
                    {
                        //html += '<script type="text/javascript">var cpro_id = "u2840406";</script>' +
                        //'<script type="text/javascript" src="http://cpro.baidustatic.com/cpro/ui/cm.js"></script>';
                    }
                    //图片
                    if (obj[i].CoverImgList == undefined || obj[i].CoverImgList == null || obj[i].CoverImgList.length <= 0) {
                        imgUrl ='<h2>' + RepPlus(obj[i].Title) + '</h2>';
                    }
                    else {
                        //大图
                        if (obj[i].ImgShowType == 1) {
                            imgUrl = '<h2>' + RepPlus(obj[i].Title) + '</h2>' + '<div class="list-img-holder-large"><img src="http://res.junshi.cn/img/common/imageholder.jpg" data-src="' +
                                        obj[i].CoverImgList[0] + '"/></div>';
                        }//三张小图
                        else if (obj[i].CoverImgList.length >= 3) {
                            imgUrl ='<h2>' + RepPlus(obj[i].Title) + '</h2>'+ '<div class="images-list-box"><ul>';
                            for (var j = 0; j < 3; j++) {
                                imgUrl += '<li> <div class="image_container"><img src="http://res.junshi.cn/img/common/imageholder.jpg" data-src="'
                                + obj[i].CoverImgList[j]
                                + '" /></div></li>';
                            }
                            imgUrl += '</ul></div></a>';
                        }//一张小图
                        else {
                            imgUrl = '<div class="single-img-box"><img src="http://res.junshi.cn/img/common/imageholder.jpg" data-src="' +
                             obj[i].CoverImgList[0] + '"/></div>'
                            + '<h2>' + RepPlus(obj[i].Title) + '</h2>';
                        }
                    }
                    var href = "/Home/" + viewName + "?id=" + obj[i].Id + "&RowNumber=" + obj[i].RowNumber + "&umparam=" + umparam;
                    if (i == 1 || i == 3 || i == 5)
                    {
                        href += obj[i].OriginalUrl;
                    }
                    html += '<section class="f_section" onclick="moreClick(\''+ i + '\',\'' + obj[i].Title + '\')"><a href="' + href + '">' + imgUrl + '  </a><div class="clear"></div></section>';
                 
                };
                $('#mainListContainer').append(html);
            } 
        },
    });
}

function addTxAds() {
    var adPosition = 0;
    var Sesstions = $(".f_section");
    var displaySess = [];
    Sesstions.each(function () {
        if ($(this).css('display') == 'none') {
        } else {
            displaySess.push($(this));
        }
    });


    if (displaySess.length >= 1) {
        var write = document.write;
        var i = 0;
        document.write = function (str) {
            $(".tx-ad-relation").each(function () {
                $(this).append(str);
            });
        }
        var htmlStr = "<div id='my_a_box'  class='tx-ad-relation'><script type='text/javascript' src='http://zzjs2.firefang.cn/kg3a1ecf94f7cef73edb1463c5a7f725ea53e3d30375e13f.js'></script></div>";
        adPositio = displaySess.length % 10;
        if (adPositio%4==1) {
            displaySess[displaySess.length - 1].after("<div id='my_a_box_" + displaySess.length+ "'  class='tx-ad-relation'><script type='text/javascript'>var cpro_id = 'u2840406';</script><script type='text/javascript' src='http://cpro.baidustatic.com/cpro/ui/cm.js'></script></div>");
        }
    }


}
function RepPlus(str) {
    if (str == null || str == undefined) {
        return '';
    }
    else {
        return str.replace(/\+/g, '%20');
    }
}
function resetImage() {
    var h = $(".images-list-box").width() * 0.3333 * 0.65;
    var imgs = $(".images-list-box").find("img:not([style])");
    imgs.height(h);

    var imgsingles = $(".single-img-box").find("img:not([style])");
    if (h > 0) {
        imgsingles.height(h);
    }
    else {
        imgsingles.height(90);
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

function addArticleFooter()
{
    //$("#articlefooter").html(" <img src='http://img.junqingtv.com/d/d/787d/origin/787327_660x665.jpg'/>");
    var isfirstshow = getCookie("isfirstshow");
    if (isfirstshow == undefined || isfirstshow == "") {
        setCookie("isfirstshow", "1", "3650");
        //$("#articlefooter").html(" <img src='http://img.junqingtv.com/d/d/787d/origin/787327_660x665.jpg'/>")
        //_czc.push(['_trackEvent', 'jiangShow', 'ShowQRCode']);
    }
    
}