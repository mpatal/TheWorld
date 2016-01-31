(function() {
    "use strict";

    //Getting the eisting module and adding a controller
    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;

        vm.trips = [];

        vm.newTrip = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips")
            .then(function(response) {
                //Success
                angular.copy(response.data, vm.trips);
            }, function(error) {
                //Failure
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function() {
                vm.isBusy = false;
            });

        vm.addTrip = function() {
            vm.trips.push({
                name: vm.newTrip.name,
                created: new Date()
            });
            vm.newTrip = {};
        }
    }

})();