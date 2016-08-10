'use strict';
(function () {

    angular.module('eqcs.incident')
        .controller('IncidentCtrl', [
            '$scope', 'getIncident', 'dataService', 'userMsg', 'formConfig', 'appConfig', '$routeParams', '$location', 'viewUtils', 'incidentCommands', '$filter', 'candidateService', 'incidentCustomValidation', "$timeout", "ukviService", 'getFetchFailure',
            function ($scope, getIncident, dataService, userMsg, formConfig, appConfig, $routeParams, $location, viewUtils, incidentCommands, $filter, candidateService, incidentCustomValidation, $timeout, ukviService, onFetchFailure) {

                var vm = $scope.vm;
                $scope.candidateModel = {};

                vm.isIncidentIdInRoute = !angular.isUndefined($routeParams.id);

                

                vm.refreshIncident = function (onError) {

                    var onSuccess = function (payload) {
                        angular.extend(vm, payload.data);
                        
                        if (!vm.isIncidentIdInRoute) {
                            $location.pathSkipReload(vm.readModel.id).replace();
                            vm.isIncidentIdInRoute = true;
                        }

                     
                        vm.showView = true;
                        vm.mode = 'incident';
                        vm.cancel = vm.refreshIncident;
                        vm.config.activeMenu = 'edit';
                        vm.config.menusVisible = ['home', 'new'];

                        if (vm.commandLinks.length === 0) {
                            vm.config.menusVisible.push('view');

                        } else {
                            vm.config.menusVisible.push('edit');
                        }

                    };
                    
                    getIncident(vm.incidentUri || $routeParams.id)
                        .then(onSuccess, onError);
                };
                vm.cancel = vm.cancel || vm.refreshIncident;

                $scope.$watch("vm.writeModel", function (model) {
                    $scope.viewForm.$setPristine();
                    $timeout(function() {
                        incidentCustomValidation.stop($scope);
                    });
                });
              

              
                $scope.onCommandSuccess = function () {
                    incidentCustomValidation.stop($scope);
                    vm.refreshIncident(onFetchFailure);
                };

                var setupFormConfig = function (commandName) {
                    if (!commandName) return;
                    vm.schema = {};
                    angular.copy(vm.resources.schemaAugments.default, vm.schema);
                    _.each(vm.resources.schemaAugments[commandName], function (item) {
                        angular.extend(vm.schema[item.field], item);
                    });
                    vm.serverValidationErrors = void 0;

                };

                var startValidation = function(commandName) {
                    setupFormConfig(commandName);
                    return incidentCustomValidation.start($scope);
                }

                var onCommandPreExecute = function (commandName) {
                    $scope.showOverlay();
                    startValidation(commandName);
                }

                // augment schema taken from the server with field specific configs
                // (including validation error messages)
                $scope.$watch("vm.schema", function (value) {
                    if (!value) return;
                    angular.extend(vm.schema.reportUkvi, vm.config.form.ukvi.reportToUKVI);
                    angular.extend(vm.schema.noOfCandidates, vm.config.form.details.noOfCandidates);
                });

                var checkValidForm = function () {
                    if ($scope.viewForm.$invalid) {
                        $scope.hideOverlay();
                        viewUtils.illuminateValidationErrors();
                    }
                    return $scope.viewForm.$valid;
                };

                $scope.onCommandError = function (payload) {
                    incidentCustomValidation.stop($scope);
                    $scope.hideOverlay();
                    if (payload.status === 400 && payload.error && payload.error.failureType === "Validation") {
                        vm.serverValidationErrors = payload.error.validationResult.errors;
                        viewUtils.illuminateValidationErrors();
                    } else if (payload.status === 409) {
                        userMsg.popup(EQCS_Error_Parser(payload.error, 'incident'), 'warning', 15000, 'Conflict');
                    } else {
                        userMsg.popupGeneralError(payload);
                    }
                    vm.mode = 'incident';
                };

                var isDirty = function () {
                    return $scope.viewForm.$dirty;
                };

                $scope.$watch("vm.commandLinks", function (links) {

                    if (!links) {
                        vm.commands = void 0;
                        return;
                    }

                    // TODO Chris: refactor available commands
                    vm.availableCommands = _.reduce(links, function (agg, link) {
                        agg.push(link.name);
                        return agg;
                    }, []);
                    
                    vm.commands =
                    {
                        cancel: function () {
                            vm.cancel(onFetchFailure);
                        }
                    };

                    var getCommandUri = function (command) {
                        var cmd = _.findWhere(links, { name: command });
                        return cmd ? cmd.href : null;
                    };
                    var saveUrl = getCommandUri('save');
                    if (saveUrl) {
                        vm.commands.save = incidentCommands.save(
                        {
                            url: saveUrl,
                            vm: vm,
                            isDirty: isDirty,
                            beforeExecute: onCommandPreExecute,
                            onExecute: checkValidForm,
                            onSuccess: $scope.onCommandSuccess,
                            onError: $scope.onCommandError
                        });
                    }

                    var raiseUrl = getCommandUri('raise');
                    if (raiseUrl) {
                        vm.commands.raise = incidentCommands.raise(
                        {
                            url: raiseUrl,
                            vm: vm,
                            isDirty: isDirty,
                            beforeExecute: onCommandPreExecute,
                            onExecute: checkValidForm,
                            onSuccess: $scope.onCommandSuccess,
                            onError: $scope.onCommandError
                        });
                    }

                    var acceptUrl = getCommandUri('accept');
                    if (acceptUrl) {
                        vm.commands.accept = incidentCommands.accept(
                        {
                            url: acceptUrl,
                            vm: vm,
                            isDirty: isDirty,
                            beforeExecute: onCommandPreExecute,
                            onExecute: checkValidForm,
                            onSuccess: $scope.onCommandSuccess,
                            onError: $scope.onCommandError
                        });
                    }

                    var rejectUrl = getCommandUri('reject');
                    if (rejectUrl) {
                        vm.commands.reject = function () {
                            vm.mode = appConfig.workflowModes.rejection.name;
                        };
                    }
                    var closeUrl = getCommandUri('close');
                    if (closeUrl) {
                        vm.commands.close = function () {
                            startValidation("close")
                                .then(function () {
                                    if (checkValidForm()) {
                                        vm.mode = appConfig.workflowModes.closure.name;
                                    }
                                });
                        };
                    }
                    var reopenUrl = getCommandUri('reopen');
                    if (reopenUrl) {
                        vm.commands.reopen = function () {
                            vm.mode = appConfig.workflowModes.reopening.name;
                        };
                    }
                    var deleteUrl = getCommandUri('delete');
                    if (deleteUrl) {
                        vm.commands.delete = incidentCommands.delete({
                            url: deleteUrl,
                            vm: vm,
                            onSuccess: function () { $location.path("/home").replace(); },
                            onError: $scope.onCommandError
                        });
                    }

                    vm.addCandidateUrl = getCommandUri('addCandidate');
                });

                var workflowModes = _.reduce(_.toArray(appConfig.workflowModes), function (aggregate, current) {
                    aggregate.push(current.name);
                    return aggregate;
                }, []);

                vm.config = {
                    form: formConfig.incident.default, // TODO Chris: refactor out
                    app: appConfig,
                    activeTab: 'incident',
                    activeMenu: 'new',
                    menusVisible: ['home', 'new']
                };
                
                setupFormConfig();

                $scope.$watch("vm.mode", function (value) {
                    vm.modeIsWorkflow = _.contains(workflowModes, value);
                });

                $scope.$watch("vm.commandCounts", function (commandCounts) {

                    vm.readonly = commandCounts ? commandCounts.updating === 0 : true;
                });

                $scope.$watch("vm.readModel.raisedDate", function (value) {
                    if (!value) {
                        return;
                    }
                    vm.raisedDate = $filter('transformToShortDate')(value);
                });

                $scope.$watch("vm.readModel.createDate", function (value) {
                    if (!value) {
                        return;
                    }
                    vm.createDate = $filter('transformToShortDate')(value);
                });

                $scope.$watch("vm.product", function (value, prev) {
                    if (value === prev) return;

                    if (!value) {
                        vm.writeModel.reportUkvi = null;
                    }
                    else if (value.code !== vm.writeModel.product) {
                        vm.writeModel.reportUkvi = value.isUkvi;
                    }
                    vm.writeModel.product = value ? value.code : void 0;
                });

                $scope.$watch("vm.writeModel.product", function(value) {
                    if (!value) {
                        vm.product = void 0;
                    } else {
                        var values = _.where(vm.resources.products, {
                            code: value
                        });

                        vm.product = values.length > 0 ? values[0]: void 0;
                    }
                });

                //TODO: Refactor this code, and all of the main angular controllers as a whole, into angular services, and helper libraries - Bryan
                $scope.$watch("vm.odata", function (value) {
                    if (!value) {
                        return;
                    }

                    //TODO:Bryan, get these search names from the tabs which are desired to be displayed
                    var searchNames = vm.tabsAvailable;
                    var indexes = {};
                    if (!(typeof searchNames === "undefined")) {
                    
                    for (var i = 0; i < searchNames.length; i++) {
                        indexes[searchNames[i]] = -1;

                            _.each(appConfig.visibleIncidentTabs, function (tabName, index) {
                            var searchName = searchNames[i];
                            if (tabName === searchName)
                                indexes[searchName] = index;
                        });

                        //If action is not one of the odata enpoints returned then we need to make sure it doesn't appear in the visible tab list, otherwise make sure it does
                            if (_.find(value, function (item) { return item.name === searchNames[i]; }) == undefined) {
                            if (indexes[searchNames[i]] > -1)
                                appConfig.visibleIncidentTabs.splice(indexes[searchNames[i]], 1);

                        } else {
                            if (indexes[searchNames[i]] === -1)
                                appConfig.visibleIncidentTabs.push(searchNames[i]);
                        }
                    }
                }
                });

                candidateService.create($scope);

                ukviService.create($scope);
            }
        ]);
})();

function findByName(source, name) {
    for (var i = 0; i < source.length; i++) {
        if (source[i].name === name) {
            return source[i];
        }
    }

    return -1;
}