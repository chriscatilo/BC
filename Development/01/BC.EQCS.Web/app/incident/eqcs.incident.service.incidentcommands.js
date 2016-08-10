'use strict';
(function () {
    angular.module("eqcs.incident.service")
        .service("incidentCommands", [
            '$timeout', 'userMsg', '$routeParams', 'dataService', 'confirmationService',
            function ($timeout, userMsg, $routeParams, dataService, confirmationService) {

                var createCommand = function (args, commandName, doCommand) {
                    return function () {
                        if (args.beforeExecute) {
                            args.beforeExecute(commandName);
                        }
                        $timeout(function () {
                            // if onExecute exists and it returns undefined, null or true then execute command
                            if (args.onExecute && args.onExecute() === false) {
                                return;
                            }
                            if (commandName === 'delete') {
                                confirmationService.delete("Deleting incident", doCommand);
                            } else {
                                doCommand();
                            }
                        });
                    };
                };

                this.save = function (args) {
                    return createCommand(args, "save", function () {
                        var success = function (payload) {
                            userMsg.popup("Incident saved");
                            args.vm.incidentUri = args.vm.incidentUri || payload.location;
                            args.onSuccess();
                        };
                        dataService
                            .saveIncident(args.url, args.vm.writeModel, !args.vm.incidentUri)
                            .then(success, args.onError);
                    });
                };

                this.raise = function (args) {
                    return createCommand(args, "raise", function () {
                        var success = function (payload) {
                            args.vm.incidentUri = args.vm.incidentUri || payload.location;
                            userMsg.popup("Incident raised");
                            args.onSuccess();
                            //call the notifications
                            dataService.raiseIncidentNotifications(args.vm.readModel.id);
                            
                        };
                        dataService
                            .raiseIncident(args.url, args.vm.writeModel, !args.vm.incidentUri)
                            .then(success, args.onError);
                    });
                };

                this.accept = function (args) {
                    return createCommand(args, "accept", function () {
                        var success = function () {
                            userMsg.popup("Incident accepted");
                            //call the notifications
                            dataService.acceptIncidentNotifications(args.vm.readModel.id);
                            args.onSuccess();
                        };
                        dataService
                            .acceptIncident(args.url, args.isDirty() ? args.vm.writeModel : null)
                            .then(success, args.onError);
                    });
                };

                this.reject = function (args) {
                    return createCommand(args, "reject", function () {
                        var success = function () {
                            userMsg.popup("Incident rejected");
                            //call the notifications
                            dataService.rejectIncidentNotifications(args.vm.readModel.id);
                            args.onSuccess();
                        };
                        dataService
                            .rejectIncident(args.url, args.vm.rejectionModel)
                            .then(success, args.onError);
                    });
                };

                this.close = function (args) {
                    return createCommand(args, "close", function () {
                        var success = function () {
                            userMsg.popup("Incident closed");
                            //call the notifications
                            dataService.closeIncidentNotifications(args.vm.readModel.id);
                            args.onSuccess();
                        };

                        args.vm.writeModel.residualRiskRating = args.vm.userSelectedResidualRiskRating;

                        dataService
                            .closeIncident(args.url, args.vm.writeModel, args.vm.closureModel)
                            .then(success, args.onError);
                    });
                };

                this.delete = function (args) {
                    return createCommand(args.vm, "delete", function () {
                        var success = function () {
                            userMsg.popup("Incident deleted");
                            args.onSuccess();
                        };
                        dataService
                            .deleteIncident(args.vm.incidentUri)
                            .then(success, args.onError);
                    });
                };

                this.reopen = function (args) {
                    return createCommand(args, "reopen", function () {
                        var success = function () {
                            userMsg.popup("Incident re-opened");
                            args.onSuccess();
                        };
                        dataService
                            .reopenIncident(args.url, args.vm.reopeningModel)
                            .then(success, args.onError);
                    });
                };

                this.addCandidate = function (args) {
                    return createCommand(args, "addCandidate", function () {
                        var success = function (payload) {
                            userMsg.popup("Candidate saved successfully");
                            var newCandidateUri = payload.location;
                            args.onSuccess(newCandidateUri);
                        };
                        dataService
                            .post(args.url, args.vm.candidateModel)
                            .then(success, args.onError);

                    });
                };

                this.updateCandidate = function (args) {
                    return createCommand(args, "addCandidate", function () {
                        var success = function () {
                            userMsg.popup("Candidate saved successfully");
                            args.onSuccess(args.url);
                        };
                        dataService
                            .put(args.url, args.vm.candidateModel)
                            .then(success, args.onError);

                    });
                }
            }]);
})();



