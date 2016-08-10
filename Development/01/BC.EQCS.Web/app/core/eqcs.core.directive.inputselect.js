'use strict';
function addDefaultIfMissing(element) {
	if (element.find("select option:first").attr("value") != "?") {
		element.find("select").prepend("<option value=\"?\"></option>");
	};
}

						
(function () {

    angular
        .module('eqcs.core.directive')
        .directive('inputSelect', function () {

            return {
                replace: true,
                restrict: 'E',
                scope:
                {
                    ngForm: '=',
                    ngModel: '=',
                    ngDisabled: '=',
                    config: '=',
                    options: '=',
                    optionExp: '@',
                    excludeDefaultOption: '='
                },
                link: function ($scope, element, attrs) {

                    if (attrs.excludeDefaultOption != "true" ) {
                        
                        addDefaultIfMissing(element);

                        element.bind('change', function() {
                            addDefaultIfMissing(element);
                        });
                    }

                },
                template:
                    '<div class="form-group" ng-class="{ \'has-error\' : ngForm.$invalid } ">' +
                        '<label class="control-label" for="ngForm.$name" ng-bind="config.label"></label>' +
                        '<client-validation-errors form="ngForm" errors="config.errors"></client-validation-errors>' +
                        '<select class="form-control" id="ngForm.$name" name="field" ng-model="ngModel" ng-options="{{optionExp}}" ng-required="config.required" ng-disabled="ngDisabled">' +
                        '</select>' +
                    '</div>'
            };

        });

    
    
    angular
        .module('eqcs.core.directive')
        .directive('inputSelectReadOnly', function () {

            return {
                replace: true,
                restrict: 'E',
                scope:
                {
                    ngModel: '=',
                    config: '=',
                    options: '=',
                    optionValue: '@',
                    optionText: '@'
                },
                link: function (scope, elem, attr) {

                    scope.$watch("ngModel", function (model) {
                        
                        scope.text = void 0;
                        
                        if (model && scope.options) {
                            
                            for (var i = 0; i < scope.options.length; i++) {
                                
                                var item = scope.options[i];
                                
                                var itemValue = item[scope.optionValue];

                                if (itemValue === model) {
                                    scope.text = item[scope.optionText];
                                    break;
                                }
                            }
                        }
                    });
                    
                },
                template:
                    '<div class="form-group"">' +
                        '<label class="control-label" ng-bind="config.label"></label>' +
                        '<p class="form-control-static" name="field" ng-bind="text"></p>' +
                    '</div>'
            };

        });
})();
