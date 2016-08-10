'use strict';
(function() {
    angular.module('eqcs.incident')
        .config([
            '$routeProvider', '$locationProvider', function($routeProvider, $locationProvider) {

                $locationProvider.html5Mode(true);

                var resolver = {
                    currentuser: function (dataService) {
                        return dataService.getUser();
                    }
                }

                resolver.currentuser.$inject = ['dataService'];


            $routeProvider
                .when('/home', {
                    controller: 'IncidentHomeCtrl',
                    controllerAs: 'vm',
                    templateUrl: '/app/incident/templates/home.html',
                    caseInsensitiveMatch: true,
                    resolve: resolver
                })
            .when('/activity/:id', { // TODO Chris: change to /:id/activity
                controller: 'IncidentActivityCtrl',
                controllerAs: 'vm',
                templateUrl: '/app/incident/templates/incidentActivity.cshtml',
                caseInsensitiveMatch: true,
                resolve: resolver
            })
            .when('/actions/:id', { // TODO Chris: change to /:id/action
                controller: 'IncidentActionsCtrl',
                controllerAs: 'vm',
                templateUrl: '/app/incident/templates/incidentActions.cshtml',
                caseInsensitiveMatch: true,
                resolve: resolver
            })
            .when('/', {
                controller: 'IncidentCreateCtrl',
                controllerAs: 'vm',
                templateUrl: '/app/incident/templates/incident.html',
                caseInsensitiveMatch: true,
                resolve: resolver
            })
            .when('/:id', {
                controller: 'IncidentEditCtrl',
                controllerAs: 'vm',
                templateUrl: '/app/incident/templates/incident.html',
                caseInsensitiveMatch: true,
                resolve: resolver
            });

        }
        ]);
})()