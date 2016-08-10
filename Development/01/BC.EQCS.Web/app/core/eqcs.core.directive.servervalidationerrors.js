'use strict';
(function () {

    angular
        .module('eqcs.core.directive')
        .directive('serverValidationErrors', function () {

            return {
                replace: false,
                restrict: 'E',
                scope:
                {
                    errors: '=',
                },
                template:
                    '<ul class="validation-error">' +
                        '<li ng-repeat="err in errors" ng-bind="err.errorMessage"></li>' +
                    '</ul>'
            };

        });
})();

