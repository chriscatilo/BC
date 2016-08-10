'use strict';
(function () {

    angular
        .module('eqcs.core.directive')
        .directive('textArea', function () {

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
                        '<textarea class="form-control" id="ngForm.$name" name="field" ng-model="ngModel" ' +
                            'ng-required="config.required" ' +
                            'maxlength="{{ config.maxLength }}" ' +
                            'ng-disabled="ngDisabled"></textarea>' +
                    '</div>'
            };

        });
    
    angular
        .module('eqcs.core.directive')
        .directive('textAreaReadOnly', function () {

            return {
                replace: true,
                restrict: 'E',
                scope:
                {
                    ngModel: '=',
                    config: '=',
                },
                template:
                    '<div class="form-group">' +
                        '<label class="control-label" ng-bind="config.label"></label>' +
                        '<textarea class="form-control" name="field" ng-model="ngModel" ' +
                            'readonly="readonly"></textarea>' +
                    '</div>'
            };

        });
})();
