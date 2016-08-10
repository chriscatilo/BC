'use strict';
// TODO chris: refactore - simplify by converting directive into controller and integrate to incident.js/html
(function () {
    angular.module("eqcs.incident.directive")
        .directive("workflowform", function () {
            return {
                controllerAs: "vm",
                replace: true,
                restrict: "E",
                scope: {
                    resources: "=",
                    commandLinks: "=",
                    incidentModel: "=",
                    incidentReadModel: "=",
                    mode: "=",
                    onSuccess: "=",
                    onError: "=",
                    onCancel: "&"
                },
                controller: ["$scope", "appConfig", "formConfig", "incidentCommands", "viewUtils",
                    function ($scope, appConfig, formConfig, commands, viewUtils) {
                        
                        var vm = this;

                        vm.config = {
                            commandButtons: appConfig.commandButtons
                        };

                        // the incident model used by the form contained in the template
                        vm.writeModel = $scope.incidentModel;
                        vm.readModel = $scope.incidentReadModel;
                        // workflow mode is used to 
                        // 1. determine what command the user uses to submit the workflow
                        // 2. the form name and template the user submits
                        // 3. the css style of the form container
                        $scope.workflowMode = appConfig.workflowModes[$scope.mode];

                        vm.resources = $scope.resources;

                        var checkValidForm = function() {
                            if ($scope.viewForm.$invalid) {
                                viewUtils.illuminateValidationErrors();
                            } else {
                                $scope.$root.showOverlay();
                            }
                            return $scope.viewForm.$valid;
                        };

                        var setupFormConfig = function (commandName) {
                            var workflowFormConfig = formConfig.workflow[$scope.mode];
                            var defaultConfig = workflowFormConfig.default;
                            var augmentConfig = !commandName ? {} : workflowFormConfig.validation || {};
                            vm.config.form = viewUtils.deepCopy(augmentConfig, defaultConfig);
                            vm.serverValidationErrors = void 0;
                        }

                        var href = _.findWhere($scope.commandLinks, { name: $scope.workflowMode.commandName }).href;

                        vm.commands = [
                            {
                                name: $scope.workflowMode.commandName,
                                action: commands[$scope.workflowMode.commandName](
                                {
                                    vm: vm,
                                    url: href,
                                    beforeExecute: setupFormConfig,
                                    onExecute: checkValidForm,
                                    onSuccess: $scope.onSuccess,
                                    onError: $scope.onError
                                })
                            },
                            {
                                name: "cancel",
                                action: $scope.onCancel
                            }
                        ];

                        setupFormConfig();
                    }],
                template:
                    "<div class='workflow' ng-class='workflowMode.containerCssClass'>" +
                        "<section class='scroll-padding'></section>" +
                        "<div class='overlay' ng-click='onCancel()'></div>" +
                        "<section class='site-footer'>" +
                            "<form name='viewForm' novalidate>" +
                                "<div ng-include='workflowMode.formTemplate'></div>" +
                            "</form>" +
                        "</section>" +
                    "</div>"
            };
        });
})()