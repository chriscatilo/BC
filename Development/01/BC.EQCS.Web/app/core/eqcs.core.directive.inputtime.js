'use strict';
(function () {

    angular
        .module('eqcs.core.directive')
        .directive('inputTime', function () {

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
                link: function (scope) {
                    var watch = scope.$watch('ngForm.$name', function (value) {
                        if (!value) return;
                        $('#' + scope.ngForm.$name).keypress(function (event) {
                            event.preventDefault();
                        });
                        watch(); // destroy
                    });
                },
                template:
                    '<div class="form-group" ng-class="{ \'has-error\' : ngForm.$invalid } ">' +
                        '<label class="control-label" for="ngForm.$name" ng-bind="config.label"></label>' +
                        '<client-validation-errors form="ngForm" errors="config.errors"></client-validation-errors>' +
                        '<input class="form-control" type="text" id="{{ngForm.$name}}" name="field" kendo-time-picker ' +
                            'ng-model="ngModel" k-ng-model="dateValue" k-format="\'HH:mm \'" k-parse-formats="[\'HH:mm\']" ' +
                            'ng-required="config.required" ' +
                            'ng-disabled="ngDisabled"></input>' +
                    '</div>'
            };

        });
    

    angular
        .module('eqcs.core.directive')
        .directive('inputTimeReadOnly', function () {

            return {
                replace: true,
                restrict: 'E',
                scope:
                {
                    ngModel: '=',
                    label: '=',
                    filter: '@',
                },
                template:
                    '<div class="form-group"">' +
                        '<label class="control-label" ng-bind="label"></label>' +
                        '<p class="form-control-static" name="field" ng-bind="ngModel | date: \'00:00:00\'"></p>' +
                    '</div>'
            };

        });
})();
