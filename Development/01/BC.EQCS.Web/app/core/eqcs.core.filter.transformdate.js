'use strict';
(function() {
    angular.module('eqcs.core.filter')
        .filter("transformDate", function() {
            return function(input) {
                var date = kendo.parseDate(input);
                return date;
            };
        });

    angular.module('eqcs.core.filter')
        .filter("transformToShortDate", ['$filter', function ($filter) {
            return function (input) {

                var date = $filter('transformDate')(input);

                return $filter('date')(date, 'short');
            };
        }]);
})()