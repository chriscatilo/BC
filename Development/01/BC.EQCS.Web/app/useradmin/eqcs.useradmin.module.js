'use strict';
(function () {

    angular.module('eqcs.useradmin.service', []);

    angular.module('eqcs.useradmin.constant', []);

    angular.module('eqcs.useradmin', [
        'ngRoute',
        'eqcs.core',
        'eqcs.useradmin.service',
        'eqcs.useradmin.constant',

        'kendo.directives',
        'toaster',
        'wc.Directives'
    ]);

    angular.module('eqcs.useradmin')
        .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

            $locationProvider.html5Mode(true);

            $routeProvider
                .when('/home', {
                    controller: 'UserAdminHomeCtrl',
                    controllerAs: 'vm',
                    templateUrl: '/app/useradmin/templates/home.html',
                    caseInsensitiveMatch: true
                });

        }]);
})()