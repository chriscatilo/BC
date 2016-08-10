'use strict';
(function () {
    angular.module('eqcs.incident')
        .controller('IncidentCreateCtrl', [
            '$scope', '$q', 'dataService', 'getIncidentResources','currentuser',
            function ($scope, $q, data, getResources) {

                var vm = this;

                vm.showView = false;

                var start = function () {

                    vm.writeModel = {};

                    getResources('/api/incident/resource')
                        .then(function(payload) {

                            angular.extend(vm, payload.data);

                            vm.showView = true;

                            vm.mode = 'incident';
                        });
                }

                vm.cancel = start;

                start();
            }
        ]);
})()