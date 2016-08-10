'use strict';
(function () {

    angular
        .module('eqcs.core.directive')
        .directive('inputYesNo', function () {

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
                link: function(scope, elem, attr) {
                    scope.set = function (value) {
                        scope.ngModel = value;
                        scope.ngForm.$setDirty('true');
                    };
                },
                template:
                    '<div class="form-group" ng-class="{ \'has-error\' : ngForm.$invalid } ">' +
                        '<label class="control-label" for="ngForm.$name" ng-bind="config.label"></label>' +
                        '<client-validation-errors form="ngForm" errors="config.errors"></client-validation-errors>' +
                        '<div class="form-control btn-select transparent">' +
                            '<button name="ngForm.$name" class="btn" ng-class="{ \'btn-success\': ngModel === config.yesValue}" ng-model="ngModel" ng-click="set(config.yesValue)" ng-required="config.required" ><span>{{config.yesLabel}}</span></button>' +
                            '<button name="ngForm.$name" class="btn" ng-class="{ \'btn-success\': ngModel === config.noValue }" ng-model="ngModel" ng-click="set(config.noValue)" ng-required="config.required" ><span>{{config.noLabel}}</span></button>' +
                        '</div>'+
                    '</div>'
            };

        });
    
    angular
        .module('eqcs.core.directive')
        .directive('inputYesNoReadOnly', function () {

            return {
                replace: true,
                restrict: 'E',
                scope:
                {
                    ngModel: '=',
                    config: '=',
                },
                template:
                    '<div class="form-group"">' +
                        '<label class="control-label" ng-bind="config.label"></label>' +
                        '<p class="form-control-static" name="field" ng-bind="ngModel ? config.yesLabel : config.noLabel"></p>' +
                    '</div>'
            };

        });
})();
