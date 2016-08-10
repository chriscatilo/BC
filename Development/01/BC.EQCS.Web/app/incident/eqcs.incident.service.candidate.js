'use strict';
(function() {
    angular.module("eqcs.incident.service")
        .factory("candidateService", [
            'dataService', 'userMsg', 'incidentCommands', 'viewUtils', 'candidateFormConfig',
            function (dataService, userMsg, incidentCommands, viewUtils, candidateFormConfig) {

                return {
                    create: function($scope) {

                        var vm = $scope.vm;

                        vm.config.form.candidate = candidateFormConfig;

                        $scope.candidateGridOptions =
                        {
                            groupable: false,
                            resizable: false,

                            columns: [
                                {
                                    width: 2,
                                    template: "<button type='button' class='delete' ng-if='vm.addCandidateUrl' ng-click='deleteCandidate(\"${uri}\")'></button>"
                                },
                                {
                                    title: "Candidate Number",
                                    width: 4,
                                    headerAttributes: { style: "white-space: normal" },

                                    template: "<span ng-if='!vm.addCandidateUrl'>${number}</span>" +
                                        "<a href='' ng-if='vm.addCandidateUrl' ng-click='showUpdateCandidateForm(\"${uri}\")'>${number}</a>"
                                },
                                {
                                    field: "surname",
                                    title: "Surname",
                                    width: 4
                                },
                                {
                                    field: "firstnames",
                                    title: "Firstname(s)",
                                    width: 5
                                },
                                {
                                    field: "nationality",
                                    title: "Country",
                                    width: 5,
                                    headerAttributes: {
                                        style: "white-space: normal"
                                    }
                                },
                                {
                                    field: "address",
                                    title: "Address",
                                    width: 4
                                },
                                {
                                    field: "dateOfBirth",
                                    title: "Date of Birth",
                                    format: "{0:dd/MM/yyyy}",
                                    width: 5
                                },
                                {
                                    field: "gender",
                                    title: "Gender",
                                    width: 3
                                },
                                {
                                    field: "idDocumentNumber",
                                    title: "ID Document Number",
                                    width: 5,
                                    headerAttributes: { style: "white-space: normal" }
                                },
                                {
                                    field: "trfNumber",
                                    title: "TRF Number",
                                    width: 4,
                                    headerAttributes: { style: "white-space: normal" }
                                },
                                {
                                    field: "dateTrfCancelled",
                                    title: "Date Trf Cancelled",
                                    format: "{0:dd/MM/yyyy}",
                                    width: 5,
                                    headerAttributes: { style: "white-space: normal" }
                                },
                                {
                                    field: "ukviRefNumber",
                                    title: "UKVI Reference Number",
                                    width: 5,
                                    headerAttributes: { style: "white-space: normal" }
                                }
                            ]
                        };

                        //Setting up the kendo UI controls
                        //The model to apply to the schema
                        var candidateSchemaModel = {
                            uri: "uri",
                            fields: {
                                uri: { type: "string" },
                                number: { type: "string" },
                                surname: { type: "string" },
                                firstnames: { type: "string" },
                                address: { type: "string" },
                                dateOfBirth: { type: "date" },
                                gender: { type: "string" },
                                idDocumentNumber: { type: "string" },
                                trfNumber: { type: "string" },
                                dateTrfCancelled: { type: "date" },
                                countryOfOrigin: { type: "string" },
                                ukviRefNumber: { type: "string" }
                            }
                        };

                        //The schema used for the individual datasources
                        var candidateSchema = {
                            data: function(data) {

                                // when candidates are fetched, return candidate models with uri attached
                                var models = _.reduce(data, function(agg, current) {
                                    current.model.uri = current.uri;
                                    current.model.persistedUri = current.persisted;
                                    agg.push(current.model);
                                    return agg;
                                }, []);
                                vm.candidateCount = models.length;
                                return models;
                            },
                            model: candidateSchemaModel
                        };

                        $scope.$watch("vm.candidatesUri", function(value) {
                            if (!value) return;
                            $scope.candidateDataSource = new kendo.data.DataSource({
                                transport: {
                                    read: { url: value, cache: false },
                                    dataType: "json"
                                },
                                serverSorting: false,
                                serverPaging: false,
                                schema: candidateSchema
                            });
                        });

                        var onSaveSuccess = function(candidateUri) {
                            dataService.get(candidateUri).then(
                                function() {
                                    $scope.candidateDataSource.read();
                                });
                            vm.candidateModel = {};
                            $scope.saveCandidate = void 0;
                            $scope.closeCandidateForm();
                        };

                        var onSaveFailure = function (payload) {
                            if (payload.status === 400 && payload.error && payload.error.failureType === "Validation") {
                                vm.candidateServerValidationErrors = payload.error.validationResult.errors;
                                viewUtils.illuminateValidationErrors();
                            } else if (payload.status === 409) {
                                userMsg.popup(EQCS_Error_Parser(payload.error, 'candidate'), 'warning', 15000, 'Conflict');
                            } else {
                                userMsg.popupGeneralError("Failed to save the candidate.");
                            }
                        };

                        var checkValidForm = function() {
                            if ($scope.candidateForm.$invalid) {
                                viewUtils.illuminateValidationErrors();
                            } else {
                                $scope.showOverlay();
                            }
                            return $scope.candidateForm.$valid;
                        };

                        var isDirty = function () {
                            return $scope.candidateForm.$dirty;
                        };

                        // TODO Chris: move this to a reusable service 
                        var aggregateSchema = function (commandName) {
                            vm.schema.candidate = {};
                            angular.copy(vm.resources.schemaAugments.default, vm.schema.candidate);
                            angular.extend(vm.schema.candidate.candidateGender, vm.config.form.candidate.gender);

                            if (!commandName) return;
                            _.each(vm.resources.schemaAugments[commandName], function(item) {
                                angular.extend(vm.schema.candidate[item.field], item);
                            });
                            vm.serverValidationErrors = void 0;
                        }

                        var addCandidate = function(candidatesUri) {
                            return {
                                label: "Add Candidate",
                                action: incidentCommands.addCandidate(
                                {
                                    url: candidatesUri,
                                    vm: vm,
                                    beforeExecute: aggregateSchema,
                                    isDirty: isDirty,
                                    onExecute: checkValidForm,
                                    onSuccess: onSaveSuccess,
                                    onError: onSaveFailure
                                })
                            };
                        };

                        var updateCandidate = function(candidateUri) {
                            return {
                                label: "Update Candidate",
                                action: incidentCommands.updateCandidate(
                                {
                                    url: candidateUri,
                                    vm: vm,
                                    beforeExecute: aggregateSchema,
                                    isDirty: isDirty,
                                    onExecute: checkValidForm,
                                    onSuccess: onSaveSuccess,
                                    onError: onSaveFailure
                                })
                            };
                        };

                        var prepareFields = function () {

                            aggregateSchema();
                            $scope.candidateForm.$setPristine();
                        }

                        $scope.showAddCandidateForm = function(addCandidateUrl) {

                            // pre-populate the date of birth with today's date minus 25 years
                            var date = new Date();
                            var dateOfBirth = (date.getFullYear() - 25) +
                                "-" + date.getMonth() +
                                "-" + date.getDate();

                            vm.candidateModel = {
                                dateOfBirth: dateOfBirth,
                                dateTrfCancelled: null

                        };
                            $scope.saveCandidate = addCandidate(addCandidateUrl);
                            $scope.candidateFormVisible = true;

                            prepareFields();
                        };
                        $scope.showUpdateCandidateForm = function(candidateUri) {

                            var candidate = _.findWhere($scope.candidateDataSource.data(), { uri: candidateUri });

                            // even though candidate object exists, fetch a fresh one in case it was updated
                            dataService.get(candidate.persistedUri).then(
                                function(payload) {
                                    vm.candidateModel = payload.data;
                                    $scope.saveCandidate = updateCandidate(candidateUri);
                                    $scope.candidateFormVisible = true;
                                },

                                // if the fetch fails, then maybe it was deleted so show error and refresh candidates
                                function() {
                                    userMsg.popupGeneralError("Candidate was not found.");
                                    $scope.candidateDataSource.read();
                                    $scope.closeCandidateForm();
                                });

                            prepareFields();
                        };

                        $scope.closeCandidateForm = function() {
                            $scope.candidateFormVisible = false;
                        };
                        $scope.deleteCandidate = function(uri) {
                            if (confirm("Are you sure you wish to delete this candidate?")) {
                                dataService.delete(uri).then(
                                    function() {
                                        userMsg.popup("Candidate deleted successfully");

                                        //http://jsfiddle.net/anotherlab/C63Zv/
                                        var deleted = _.findWhere($scope.candidateDataSource.data(), { uri: uri });
                                        $scope.candidateDataSource.remove(deleted);
                                        vm.candidateCount--;

                                    }, function() {
                                        userMsg.popupGeneralError("Failed to delete the candidate.");
                                    }
                                );
                            }
                        };
                    }
                };
            }
        ]);
})();