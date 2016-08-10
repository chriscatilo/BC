'use strict';
(function () {

    angular
        .module('eqcs.core.directive')
        .directive('inputText', function () {

            return {
                replace: true,
                restrict: 'E',
                scope:
                {
                    ngForm: '=',
                    ngModel: '=',
                    ngDisabled: '=',
                    config: '=',
                },
                template:
                    '<div class="form-group" ng-class="{ \'has-error\' : ngForm.$invalid } ">' +
                        '<label class="control-label" for="ngForm.$name" ng-bind="config.label"></label>' +
                        '<client-validation-errors form="ngForm" errors="config.errors"></client-validation-errors>' +
                        '<input class="form-control" type="text" id="ngForm.$name" name="field" ng-model="ngModel" ' +
                            'ng-required="config.required" ' +
                            'maxlength="{{ config.maxLength }}" ' +
                            'ng-disabled="ngDisabled" ' +
                            'ng-pattern="config.pattern"></input>' +
                    '</div>'
            };

        });
    
    angular
        .module('eqcs.core.directive')
        .directive('inputTextReadOnly', function () {

            return {
                replace: true,
                restrict: 'E',
                scope:
                {
                    ngModel: '=',
                    label: '=',
                },
                template:
                    '<div class="form-group"">' +
                        '<label class="control-label" ng-bind="label"></label>' +
                        '<p class="form-control-static" name="field" ng-bind="ngModel"></p>' +
                    '</div>'
            };

        });
})();
