﻿(function() {
    "use strict";

    //Getting the eisting module and adding a controller
    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController() {
        var vm = this;

        vm.trips = [
            {
                name: "US Trip",
                created: new Date()
            },
            {
                name: "World Trip",
                created: new Date()
            }
        ];
    }

})();