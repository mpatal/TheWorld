//tripEditorController.js
(function() {
    "use strict";

    angular.module("app-trips")
        .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};

        var url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url)
            .then(function(response) {
                    //success
                    angular.copy(response.data, vm.stops);
                    var travelStops = _.map(vm.stops, function(item) {
                        return {
                            lat: item.latitude,
                            long: item.longitude,
                            info: item.name
                        };
                    });

                    _showTravelMap(travelStops);
                },
                function(error) {
                    //failure
                    vm.errorMessage = "Failed to load stops";
                })
            .finally(function() {
                vm.isBusy = false;
            });

        vm.addStop = function() {
            vm.isBusy = true;

            $http.post(url, vm.newStop)
                .then(function(response) {
                    //success go to route    
                    vm.stops.push(response.data);
                    _showTravelMap(vm.stops);

                    //clear the form
                    vm.newStop = {};
                }, function(error) {
                    //failure
                    vm.errorMessage = "Failed to add new stop.";
                })
                .finally(function() {
                    vm.isBusy = false;
                });
        };
    };

    function _showTravelMap(stops) {
        
        if (stops && stops.length > 0) {
            //show map
            travelMap.createMap({
                stops: stops,
                selector: "#map",
                currentStop: 1,
                initialZoom: 3

            });
        }
    }

})();