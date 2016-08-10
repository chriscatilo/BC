'use strict';
(function () {
    angular.module('eqcs.incident')
        .factory('getIncidentResources', [
            '$q', 'dataService', 'graphExtensions',
            function ($q, data, graphExtensions) {

                return function(url) {

                    var deferred = $q.defer();

                    var onError = function(err) {
                        deferred.reject(err);
                    };

                    var vm = {}

                    var onGetResourceSuccess = function (payload) {

                        vm.commandLinks = payload.data.commands;
                        vm.odata = payload.data.odata;

                        var keyedPromises = [];

                        _.reduce(payload.data.references, function (agg, link) {
                            if (link.href != undefined) {
                                var promise = data.getCached(link.href);
                                agg.push(
                                {
                                    key: link.name,
                                    promise: promise,
                                    type: 'reference'
                                });
                            }
                            return agg;
                        }, keyedPromises);

                        _.reduce(payload.data.trees, function (agg, link) {
                            var promise = data.getCached(link.href);
                            agg.push(
                            {
                                key: link.name,
                                promise: promise,
                                type: 'tree'
                            });
                            return agg;
                        }, keyedPromises);

                        vm.schemaUrl = payload.data.schema;

                        keyedPromises.push({
                            key: 'schema',
                            promise: data.getSchema(vm.schemaUrl)
                        });

                        keyedPromises.push({
                            key: 'authorisation',
                            promise: data.get(payload.data.authorisation)
                        });

                        var promises = _.reduce(keyedPromises, function (agg, current) {
                            agg.push(current.promise);
                            return agg;
                        }, []);

                        var onPromisesSuccess = function (values) {

                            for (var i = 0; i < values.length; i++) {
                                keyedPromises[i].data = values[i].data;
                            }

                            vm.resources = {};

                            _.reduce(_.where(keyedPromises, { type: 'reference' }), function (agg, curr) {
                                agg[curr.key] = curr.data;
                                return agg;
                            }, vm.resources);

                            _.reduce(_.where(keyedPromises, { type: 'tree' }), function (agg, curr) {
                                graphExtensions.extend(curr.data);
                                agg[curr.key] = curr.data;
                                return agg;
                            }, vm.resources);

                            if (vm.resources.class) {
                                vm.resources.categories = vm.resources.class.getByType("Category");
                            }
                            if (vm.resources.adminUnit) {
                                vm.resources.testCentres = vm.resources.adminUnit.getByType("TEST_CENTRE");
                            }
                            
                            var schema = _.first(_.where(keyedPromises, { key: 'schema' }));
                            vm.resources.schemaAugments = schema.data;
                            vm.schema = {};
                            angular.copy(vm.resources.schemaAugments.default, vm.schema);

                            var authorisation = _.first(_.where(keyedPromises, { key: "authorisation" }));
                            vm.authorisation = vm.authorisation || { };
                            vm.authorisation.incident = authorisation.data;

                            deferred.resolve({ data: vm });
                        };

                        $q.all(promises).then(onPromisesSuccess, onError);
                    };

                    data.get(url)
                        .then(onGetResourceSuccess, onError);

                    return deferred.promise;
                };
            }
    ]);
})()