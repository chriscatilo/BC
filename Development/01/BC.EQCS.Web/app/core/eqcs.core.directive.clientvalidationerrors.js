'use strict';
(function () {

    angular
        .module('eqcs.core.directive')
        .directive('clientValidationErrors', function () {

            return {
                replace: false,
                restrict: 'E',
                scope:
                {
                    form: '=',
                    errors: '=',
                },
                template:
                    '<ul class="validation-error">' +
                        '<li ng-repeat="err in errors" ng-if="form.$error[err.key]" ng-bind="err.value"></li>' +
                    '</ul>'
            };

        });
})();

