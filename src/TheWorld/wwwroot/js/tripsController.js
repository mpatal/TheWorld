(function () {
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

        vm.loadTrips = function () {
            $http.get("/api/trips")
            .then(function (response) {
                //Success
                angular.copy(response.data, vm.trips);
            }, function (error) {
                //Failure
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });
        }

        vm.loadTrips();

        vm.addTrip = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    //success
                    vm.trips.push(response.data);
                    vm.newTrip = {};
                }, function (error) {
                    //Failure
                    vm.errorMessage = "Failed to save new trip: " + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
    }

})();