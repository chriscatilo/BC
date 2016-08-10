'use strict';
(function () {

    angular
        .module('eqcs.core.directive')
        .directive('command', ['$timeout', function ($timeout) {

            return {
                replace: true,
                restrict: 'E',
                scope: {
                    'class': '@',
                    label: '@',
                    preEvent: '&',
                    event: '&',
                    commandScope: '=',
                    commandName: '@',
                    name: '@',
                },
                link: function (scope, elem, attr) {
                    
                    if (!scope.commandName) {
                        scope.commandName = attr.name;
                    }

                    var commandScope = !scope.commandScope ? scope.$parent : scope.commandScope;

                    scope.execute = function () {

                        commandScope.lastCommand = scope.commandName;

                        $timeout(function() {

                            var doEvent = !scope.preEvent ? true : scope.preEvent();
                            doEvent = angular.isUndefined(doEvent) ? true : doEvent;

                            if (doEvent) {
                                scope.event();
                            }

                        });

                    };
                },
                template: 
                    '<button class="{{ class }}" ng-click="execute()" ng-bind="label"></button>'
            };

        }]);
})();
