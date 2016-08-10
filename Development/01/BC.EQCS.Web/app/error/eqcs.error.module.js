'use strict';
(function () {
    
    angular.module('eqcs.error.service', []);
    
    angular.module('eqcs.error.constant', []);
    
    angular.module('eqcs.error', [
        'ngRoute',
        'eqcs.core',
        'eqcs.error.service',
        'eqcs.error.constant'
    ]);

    angular.module('eqcs.error')
        .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

            $locationProvider.html5Mode(true);
            
            $routeProvider
                .when('/home', {
                    controller: 'errorHomeCtrl',
                    controllerAs: 'vm',
                    templateUrl: '/app/error/templates/home.html',
                    caseInsensitiveMatch: true
                });

            $routeProvider
                .when('/authoriserDecline', {
                    controller: 'errorAuthoriserDeclineCtrl',
                    controllerAs: 'vm',
                    templateUrl: '/app/error/templates/authoriserDecline.html',
                    caseInsensitiveMatch: true
                });

        }]);
})()