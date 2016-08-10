"use strict";
(function () {
    angular.module("eqcs.core.directive")
        .directive("availableCommands", function() {
            return {
                controllerAs: "vm",
                restrict: "A",
                scope: {
                    availableCommands: "=",
                    viewForm: "=",
                    commandActions: "=",
                    commandButtons: "=",
                    enabledCounts: "="
                },
                controller: ["$scope",
                    function ($scope) {

                        // create lookup lists of updating and non-updating buttons
                        var buttons = _.reduce(
                            _.map($scope.commandButtons, function(obj, key) {
                                var cmd = angular.extend({ name: key }, obj);
                                return cmd;
                            }),
                            function(agg, current) {
                                if (current.updating) {
                                    agg.updating.push(current.name);
                                }
                                if (current.nonUpdating) {
                                    agg.nonUpdating.push(current.name);
                                }
                                return agg;
                            }, { updating: [], nonUpdating: [] });
                        
                        var vm = this;

                        // if form is dirty then enable updating commands
                        // else if form is pristine then enable non-updating commands
                        var update = function () {
  
                            var availableCommands = angular.copy($scope.availableCommands);

                            // add cancel to available commands if any updating commands exists
                            if (_.intersection(availableCommands, buttons.updating).length > 0) {
                                availableCommands.push('cancel');
                            }
                            var isDirty = $scope.viewForm ? $scope.viewForm.$dirty : false;

                            var enabledCommands = _.intersection(availableCommands, isDirty
                                ? _.union(buttons.updating, ['cancel'])
                                : buttons.nonUpdating);

                            vm.commandsShown = _.reduce(availableCommands,
                                function(agg, cmdName) {
                                    if (cmdName) {
                                        cmdName = cmdName.toLowerCase();
                                        var action = $scope.commandActions[cmdName];
                                        if (action) {
                                            var config = angular.copy($scope.commandButtons[cmdName]);
                                            config.disabled = !_.contains(enabledCommands, cmdName);
                                            agg.push({ name: cmdName, action: action, config: config });
                                        }
                                    }
                                    return agg;
                                }, []);

                            // count the number of enabled commands
                            $scope.enabledCounts = {
                                updating: _.intersection(enabledCommands, buttons.updating).length,
                                nonUpdating: _.intersection(enabledCommands, buttons.nonUpdating).length,
                                total: enabledCommands.length
                            }
                            


                            //Override the raise buttons disabled setting for new incidents TODO: Move this override into the main server side logic when refactoring is allowed again
                            for (var i = 0; i < vm.commandsShown.length; i++) {
                                if (vm.commandsShown[i].config.label === "Raise") {
                                    if ($scope.$parent != undefined && $scope.$parent.$parent != undefined && $scope.$parent.$parent.vm != undefined) {
                                        var itIsANewIncident = $scope.$parent.$parent.vm.readModel == undefined || $scope.$parent.$parent.vm.readModel.id == undefined;
                                        var theFormIsPristine = $scope.viewForm.$pristine;
                                        var disableRaise = itIsANewIncident && theFormIsPristine || vm.commandsShown[i].config.disabled;
                                        vm.commandsShown[i].config.disabled = disableRaise;
                                    }
                                }
                            }
                        };

                        $scope.$watch("availableCommands", update);
                        $scope.$watch("viewForm.$dirty", update);

                        // this watcher is required to disable commands when the form is invalid
                        $scope.$watch("viewForm.$valid", update);

                    }

                ],
                template:
                    "<section class='site-footer animate-if' ng-if='enabledCounts.total > 0'>" +
                        '<div class="commands">' +
                            '<div class="container">' +
                                '<div class="col-lg-12">' +
                                    '<button ng-repeat="cmd in vm.commandsShown" class="{{ cmd.config.cssClass }}" ng-click="cmd.action()" ng-bind="cmd.config.label" ng-disabled="cmd.config.disabled"></button>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                    "</section>"
                    
            }
        });
})();
