var wx_ready = false;
var wx_num = 0;
var setI;
var share_title;
var share_link;
var share_imgurl;
var share_desc;

getconfig();

function getconfig() {
    var url = window.location.href.split("#")[0];
    //url = encodeURIComponent(url);
    $.ajax({
        url: "/Home/InitWx?url=" + url,
        dataType: "json",
        type: 'get',
        success: function (data) {
            wx_config(data);
        }
    });
}

function wx_config(data) {
    wx.config({
        debug: false,
        appId: data.appId,
        timestamp: data.timestamp,
        nonceStr: data.nonceStr,
        signature: data.signature,
        jsApiList: ["ready", "checkJsApi", 'onMenuShareTimeline', 'onMenuShareAppMessage']
    });
    wx.ready(function () {
        wx_ready = true;
        // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。
    });
    wx.error(function (res) {
        console.log(res);
    });
    wx.checkJsApi({
        jsApiList: ["ready", "onMenuShareTimeline", "onMenuShareAppMessage"], // 需要检测的JS接口列表，所有JS接口列表见附录2,
        success: function (res) {
            console.log(res);
        }
    });
}

function set_share() {
    if (wx_ready) {
        clearInterval(setI);
        //朋友圈
        wx.onMenuShareTimeline({
            title: share_title, // 分享标题
            link: share_link, // 分享链接
            imgUrl: share_imgurl, // 分享图标
            success: function () {
                share_succ();
            },
            cancel: function () {
                share_err();
            }
        });
        //朋友
        wx.onMenuShareAppMessage({
            title: share_title, // 分享标题
            desc: share_desc, // 分享描述
            link: share_link, // 分享链接
            imgUrl: share_imgurl, // 分享图标
            type: '', // 分享类型,music、video或link，不填默认为link
            dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
            trigger: function (res) {
                
            },
            success: function (res) {
                
                share_succ();
            },
            cancel: function (res) {
                share_err();
            },
            fail: function (res) {
            }
        });

    } else {
        console.log("微信配置信息获取失败");
    }
}

function share(title, link, imgurl, desc) {
    share_title = title;
    share_link = link;
    share_imgurl = imgurl;
    share_desc = desc;
    setI = setInterval(set_share, 2000);

}