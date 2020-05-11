

function refreshGrid(id, offset) {
    $("#" + id).data("kendoGrid").dataSource.read();
    setTimeout(function () {
        $("#" + id + " .k-grid-content").css("height", "").css("max-height", $(window).height() - offset);
    }, 500);
}