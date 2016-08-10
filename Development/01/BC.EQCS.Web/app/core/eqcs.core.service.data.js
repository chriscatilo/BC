'use strict';
(function () {
    angular.module("eqcs.core.service")
        .factory("coreDataService", [
            '$http', '$cacheFactory', '$q', '$location',
            function ($http, $cacheFactory, $q, $location) {

                var cache = $cacheFactory('data');
                
                var get = function(uri, isCached) {

                    var deferred = $q.defer();

                    if (isCached) {
                        var data = cache.get(uri);
                        if (data) {
                        
                            deferred.resolve({ data: data });
                        }
                    }

                    var onSuccess = function (response) {
                       
                        if (isCached) {
                            cache.put(uri, response.data);
                        }
                        deferred.resolve({ data: response.data });
                    };

                    var onError = function (error, status) {                       
                        deferred.reject({ error: error, status: status });

                        if (error.status === 403) {
                            //TODO: I used window.locaction as $location.path(); produced an error. see if it can be fixed
                            window.location = "../error/AuthoriserDecline";
                        }
                    };

                    $http.get(uri)
                        .then(onSuccess, onError);

                    return deferred.promise;
                };

                var post = function (uri, model) {

                    var defer = $q.defer();

                    $http.post(uri, model)
                        .success(
                            function (data, status, headers, config) {
                                
                                var location = headers("location");
                                
                                var payload = {
                                    location: location,
                                    data: data,
                                    status: status,
                                    headers: headers,
                                    config: config
                                };
                                defer.resolve(payload);
                            })
                        .error(
                            function(data, status) {
                                defer.reject({ error: data, status: status });
                            });

                    return defer.promise;
                };
                
                var put = function (uri, model) {

                    var defer = $q.defer();

                    $http.put(uri, model)
                        .success(
                            function (data, status, headers, config) {

                                var payload = {
                                    data: data,
                                    status: status,
                                    headers: headers,
                                    config: config
                                };
                                defer.resolve(payload);
                            })
                        .error(
                            function (data, status) {
                                defer.reject({ error: data, status: status });
                            });

                    return defer.promise;
                };
                
                var del = function (uri) {

                    var defer = $q.defer();

                    $http.delete(uri)
                        .success(
                            function (data, status, headers, config) {
                                var payload = {
                                    data: data,
                                    status: status,
                                    headers: headers,
                                    config: config
                                };
                                defer.resolve(payload);
                            })
                        .error(
                            function (data, status) {
                                defer.reject({ error: data, status: status });
                            });

                    return defer.promise;
                }

                return {
                    
                    get: function(uri) {
                        return get(uri, false);
                    },

                    getCached: function (uri) {
                        return get(uri, true);
                    },
                    
                    post: post,
                    
                    put: put,

                    delete: del
                };

            }]);
})()