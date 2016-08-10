'use strict';
(function () {
    
    angular.module('eqcs.auditing.service', []);
    
    angular.module('eqcs.auditing.constant', []);
    
    angular.module('eqcs.auditing', [
        'ngRoute',
        'eqcs.core',
        'eqcs.auditing.service',
        'eqcs.auditing.constant',

        'kendo.directives',
        'toaster',
        'wc.Directives'
    ]);

    angular.module('eqcs.auditing')
        .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

            $locationProvider.html5Mode(true);
            
            $routeProvider
                .when('/home', {
                    controller: 'AuditingHomeCtrl',
                    controllerAs: 'vm',
                    templateUrl: '/app/auditing/templates/home.html',
                    caseInsensitiveMatch: true
                });

        }]);
})()