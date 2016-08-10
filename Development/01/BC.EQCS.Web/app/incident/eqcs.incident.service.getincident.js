'use strict';
(function () {
    angular.module('eqcs.incident')
        .factory('getIncident', [
            '$q', 'dataService', 'graphExtensions', 'getIncidentResources',
            function ($q, data, graphExtensions, getResources) {

                return function(identifier) {

                    var deferred = $q.defer();

                    var onError = function(err) {
                        deferred.reject(err);
                    };

                    var vm = {}

                    var onGetViewModelSuccess = function(payload) {

                        vm.readModel = payload.data.model;

                        vm.incidentUri = payload.data.uri;

                        vm.candidatesUri = payload.data.candidates;

                        vm.tabsAvailable = payload.data.tabsAvailable;

                        var promises = [
                           getResources(payload.data.resource),
                          payload.data.persisted ? data.get(payload.data.persisted) : void 0
                        ];

                        $q.all(promises)
                            .then(function(values) {
                                
                                // resources
                                angular.extend(vm, values[0].data);

                                // persist model
                                vm.writeModel = values[1].data;

                                deferred.resolve({ data: vm });
                            }, onError);
                    };

                    var id = parseInt(identifier);

                    var uri = isNaN(id)
                        ? identifier // identifier is unique resource identifier
                        : '/api/incident/' + id; // identifier is incident id

                    data.get(uri).then(onGetViewModelSuccess, onError);

                    return deferred.promise;
                };
            }
    ]);
})()