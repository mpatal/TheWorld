// site.js

(function () {
    //var ele = $("#username");
    //ele.text("Marvin Patal");

    //var main = $("#main");
    //main.on("mouseenter", function() {
    //    main.css("background-color", "#888");
    //});

    //main.on("mouseleave", function() {
    //    main.css("background-color", "");
    //});

    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {
    //    var me = $(this);
    //    alert(me.text());
    //});

    var $sidebarAndWrapper = $("#sidebar, #wrapper");
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function() {
        $sidebarAndWrapper.toggleClass("hide-sidebar");

        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.toggleClass("fa-angle-right");
        } else {
            $icon.removeClass("fa-angle-right");
            $icon.toggleClass("fa-angle-left");
        }
        

    });
    

})();
