'use strict';
(function () {
    
    angular.module('eqcs.incident.service', []);

    angular.module('eqcs.incident.constant', []);
    
    angular.module('eqcs.incident.directive', []);

    angular.module('eqcs.incident', [
        'ngRoute',
        'eqcs.core',
        'eqcs.incident.service',
        'eqcs.incident.constant',
        'eqcs.incident.directive',
        'kendo.directives',
        'toaster',
        'wc.Directives'
    ]);

})()