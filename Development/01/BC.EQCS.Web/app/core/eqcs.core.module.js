'use strict';
(function () {

    angular.module('eqcs.core.filter', []);
    
    angular.module('eqcs.core.service', []);
    
    angular.module('eqcs.core.directive', []);

    var core = angular.module('eqcs.core', [
        'eqcs.core.service',
        'eqcs.core.directive',
        'eqcs.core.filter'
    ]);

    // override $location.path to allow change of url route without reloading
    // http://joelsaupe.com/programming/angularjs-change-path-without-reloading/
    core.run(['$route', '$rootScope', '$location', function($route, $rootScope, $location) {
        $location.pathSkipReload = function(path) {
            var lastRoute = $route.current;
            var un = $rootScope.$on('$locationChangeSuccess', function() {
                $route.current = lastRoute;
                un();
            });
            return $location.path(path);
        };
    }]);
})()