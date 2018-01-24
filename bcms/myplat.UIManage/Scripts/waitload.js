//演示加载
function _loading(msg,divId) {
    if (msg == undefined || msg == "")
        msg = "数据保存中，请稍候...";
    if (divId == undefined || divId == "")
        divId = "category-container";
    var h = $(window).height();
    var w = $(window).width();
    var bodyH = h;
    $("<div class=\"datagrid-mask\"></div>").css({ display: "block", width: "100%", float: "left", height: bodyH }).appendTo($("#" + divId));
    $("<div class=\"datagrid-mask-msg\" ></div>").html(msg).appendTo($("#" + divId)).css({
        display: "block",
        height: "40px",
        left: (w) / 2,
        top: (h) / 2,
        position: "fixed",
        'z-index': "9999"
    });
}
function _stop() {
    $('.datagrid-mask-msg').fadeOut(500);
    $('.datagrid-mask').fadeOut(500);
}