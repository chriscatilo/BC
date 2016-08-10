'use strict';
(function () {

    angular.module('eqcs.incident')
        .controller('IncidentEditCtrl', [
            '$q', 'getIncident', '$scope', 'getFetchFailure',
            function ($q, getIncident, $scope, onFetchFailure) {
               
                var vm = this;
               
                vm.writeModel = {};

                vm.showView = false;

                var start = $scope.$watch('vm.refreshIncident', function () {
                    vm.refreshIncident(onFetchFailure);
                    start(); // destroy watch
                });
               
            }
        ]);
})()