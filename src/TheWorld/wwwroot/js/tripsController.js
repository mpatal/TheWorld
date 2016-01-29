(function() {
    "use strict";

    //Getting the eisting module and adding a controller
    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController() {
        var vm = this;

        vm.name = "Marvin";
    }

})();