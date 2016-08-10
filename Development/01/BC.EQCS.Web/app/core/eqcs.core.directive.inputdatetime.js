'use strict';
(function () {

    angular
        .module('eqcs.core.directive')
        .directive('inputDateTime', function () {

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
                        '<input class="form-control" type="text" id="{{ngForm.$name}}" name="field" kendo-date-time-picker ' +
                            'ng-model="ngModel" k-ng-model="dateValue" k-format="\'dd/MM/yyyy HH:mm \'" k-parse-formats="[\'yyyy-MM-ddTHH:mm\']" ' +
                            'ng-required="config.required" ' +
                            'ng-disabled="ngDisabled"></input>' +
                    '</div>'
            };

        });
    

    angular
        .module('eqcs.core.directive')
        .directive('inputDateTimeReadOnly', function () {

            return {
                replace: true,
                restrict: 'E',
                scope:
                {
                    ngModel: '=',
                    config: '=',
                    filter: '@',
                },
                template:
                    '<div class="form-group"">' +
                        '<label class="control-label" ng-bind="config.label"></label>' +
                        '<p class="form-control-static" name="field" ng-bind="ngModel | date: \'dd/MM/yyyy 00:00:00\'"></p>' +
                    '</div>'
            };

        });
})();
