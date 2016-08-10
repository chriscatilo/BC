'use strict';
(function() {
    angular.module('eqcs.core.directive')
        .directive('commands', function() {
            return {
                replace: true,
                restrict: 'E',
                scope: {
                    actions: '=',
                    configs: '='
                },
                link: function (scope) {
                    scope.$watch("actions", function(actions) {
                        scope.commands = _.reduce(actions || [], function (aggregate, current) {
                            if (current) {
                                var commandName = current.name.toLowerCase();
                                var config = scope.configs[commandName];
                                if (config) {
                                    aggregate.push({ config: config, action: current.action });
                                }
                            }
                            return aggregate;
                        }, []);
                    });
                },
                template:
                    '<div class="commands">' +
                        '<div class="container">' +
                            '<div class="col-lg-12">' +
                                '<button ng-repeat="cmd in commands" class="{{ cmd.config.cssClass }}" ng-click="cmd.action()" ng-bind="cmd.config.label" ng-disabled="cmd.config.disabled"></button>' +
                            '</div>' +
                        '</div>' +
                    '</div>'
            };
        });
})()