'use strict';
// container to all 'run' executions to eqcs.incident module
(function () {
    angular.module('eqcs.incident').run([
        '$rootScope', 'dataService', 'graphExtensions',
        function ($rootScope, dataService) {
            dataService.getUser()
                .then(function(payload) {

                    var user = payload.data;
                                   
                    $rootScope.user = user;

                });
        }
    ]);
})()