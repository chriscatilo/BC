'use strict';
(function () {

    angular.module('eqcs.incident')
        .controller('IncidentActionsCtrl', [
            '$scope', '$q', 'dataService', 'userMsg', 'formConfig', 'appConfig', '$routeParams', 'currentuser', 'getIncident', '$location', 'viewUtils', 'incidentCommands', '$filter', '$http',
            function ($scope, $q, dataService, userMsg, formConfig, appConfig, $routeParams, currentuser, getIncident, $location, viewUtils, incidentCommands, $filter, $http) {
                var vm = this;
                vm.model = {
                    incidentId: 0,
                    url: "",
                    documentList: [],
                    onDocumentUpload: function (response) {
                        $scope.currentAction.documentList.push(response);
                        this.documentList = $scope.currentAction.documentList;

                        $scope.incidentActionsListDataSource.read();
                    },
                    removeUploadedFile: function (item) {
                        var result;
                        dataService.removeUploadedFile(item.id).then(result = removeFileOnSuccess(item), saveFailure);
                        $scope.incidentActionsListDataSource.read();
                        return result;
                    }
                };

                var removeFileOnSuccess = function (item) {
                    item.id = parseInt(item.id);
                    var document = _.findWhere($scope.currentAction.documentList, { id: item.id });
                    if (document) {
                        $scope.currentAction.documentList = _.without($scope.currentAction.documentList, document);
                        vm.model.documentList = $scope.currentAction.documentList;
                        userMsg.popup("File removed successfully");
                        return true;
                    }

                    return false;
                }

                vm.config = {
                    app: appConfig,
                    activeMenu: 'edit',
                    menusVisible: ['home', 'new', 'edit']
                };

                vm.isIncidentIdInRoute = !angular.isUndefined($routeParams.id);

                if (vm.isIncidentIdInRoute) {
                    vm.model.incidentId = +$routeParams.id;
                    vm.config.activeTab = 'actions';
                }

                var onGetIncidentSuccess = function (payload) {
                    if (payload.data.readModel.status === "Rejected") {
                        $location.path(payload.data.readModel.id);
                    }
                    if ((payload.data.writeModel.incidentActions.length <= 0 && !jQuery.inArray("actions", payload.data.tabsAvailable)) && (payload.data.readModel.status === "Closed")) {
                        $location.path(payload.data.readModel.id);
                    }
                    if (payload.data.writeModel.incidentActions.length <= 0 && (payload.data.readModel.status === "Draft" || payload.data.readModel.status === "Submitted")) {
                        $location.path(payload.data.readModel.id);
                    } else {
                        angular.extend($scope.vm, payload.data);
                        $scope.addActionAvailable = payload.data.readModel.status === "In Progress";


                        if (!vm.isIncidentIdInRoute) {
                            $location.pathSkipReload($scope.vm.readModel.id).replace();
                            vm.isIncidentIdInRoute = true;
                        }
                        $scope.vm.showView = true;
                        $scope.vm.mode = 'incidentaction';
                    }
                };

                var onGetIncidentError = function (e) {
                    $scope.addActionAvailable = false;
                };

                getIncident($routeParams.id).then(onGetIncidentSuccess, onGetIncidentError);

                var currentActionDefaultState = { id: 0, incidentId: +$routeParams.id, actionDescription: "", assignedToTestCentre: true, assignedToIndividuals: [], documentList: [] };
                $scope.currentAction = $.extend({}, currentActionDefaultState);

                if ($routeParams.id !== undefined) {
                    vm.model.incidentId = +$routeParams.id;
                    vm.config.activeTab = 'actions';
                }

                $scope.clearCurrentAction = function () {
                    if ($scope.currentAction.id === 0) {
                        dataService.removeOrphenFiles($scope.currentAction.documentList);
                    }

                    $scope.currentAction = $.extend({}, currentActionDefaultState);

                    $scope.currentAction.documentList = [];
                    vm.model.documentList = [];
                    vm.model.url = "";
                };

                $scope.clearSelectedUsers = function () {
                    $scope.currentAction.assignedToIndividuals = [];
                };

                var saveSuccess = function (result) {
                    userMsg.popup("Action saved successfully");

                    if ($scope.currentAction.id > 0) {
                        $scope.vm.writeModel.incidentActions = _.filter($scope.vm.writeModel.incidentActions, function (item) {
                            return item.id !== $scope.currentAction.id;
                        });
                    }

                    if (result.data !== "") {
                        $scope.currentAction.id = result.data.id;
                        $scope.currentAction.rowVersion = result.data.rowVersion;
                    }

                    $scope.vm.writeModel.incidentActions.push($scope.currentAction);
                    $scope.incidentActionsListDataSource.read();

                    $scope.clearCurrentAction();
                };

                var createActionSuccess = function (result) {
                    saveSuccess(result);
                    //Add the call to the notification service here
                    dataService.createActionNotifications($scope.vm.readModel.id, result.data.id);
                };

                var closeActionSuccess = function (result) {
                    //Add the call to the notification service here
                    dataService.closeActionNotifications($scope.vm.readModel.id, $scope.currentAction.id);

                    saveSuccess(result);
                };

                var editActionSuccess = function (result) {
                    //Add the call to the notification service here
                    dataService.editActionNotifications($scope.vm.readModel.id, $scope.currentAction.id);

                    saveSuccess(result);
                };

                var saveFailure = function (e, f) {
                    if (e.status === 400 && e.error.validationResult.errors.length > 0) {
                        for (var i = 0; i < e.error.validationResult.errors.length; i++) {
                            var property = e.error.validationResult.errors[i].propertyName;
                            var message = e.error.validationResult.errors[i].errorMessage != undefined ? e.error.validationResult.errors[i].errorMessage : "";
                            userMsg.popupGeneralError(property + ": " + message);
                        }
                    } else if (e.status === 409) {
                        userMsg.popup(EQCS_Error_Parser(e.error, 'incident action'), 'warning', 15000, 'Conflict');
                    } else {
                        userMsg.popupGeneralError("Failed to save the incident action");
                    }

                    $scope.isDisabled = false;

                    $scope.hideOverlay();
                };


                $scope.saveNClose = function (windowName) {
                    $scope.isDisabled = true;
                    var _this = this;

                    //Run custom validation where it exists
                    var validateUsersSelection = function () { return $scope.currentAction.assignedToTestCentre || $scope.currentAction.assignedToIndividuals.length > 0; };
                    $scope.actionForm.$setValidity("userSelection", validateUsersSelection(), this);
                   

                    if (!$scope.actionForm.$valid) {
                        for (var validationType in $scope.actionForm.$error) {
                            if ($scope.actionForm.$error.hasOwnProperty(validationType))
                                for (var i = 0; i < $scope.actionForm.$error[validationType].length; i++) {
                                    userMsg.popupGeneralError($scope.actionForm.$error[validationType][i].$name + " : " + validationType);
                                }
                        }

                        $scope.isDisabled = false;
                        userMsg.popupGeneralError("Selection is invalid, please correct");

                        return;
                    }

                    $scope.showOverlay();

                    //Process users collection into a collection of ids / guids
                    var wNModel = $scope.currentAction;

                    wNModel.assignedTo = $scope.currentAction.assignedToIndividuals;

                    // Add list of files attached to CurrentAction
                    vm.model.documentList = [];

                    if ($scope.currentAction.id === 0) {
                        dataService.createAction(wNModel).then(createActionSuccess, saveFailure);
                    } else {
                        if (wNModel.responseText === '') {
                            dataService.saveAction(wNModel).then(editActionSuccess, saveFailure);
                        } else {
                            dataService.saveAction(wNModel).then(closeActionSuccess, saveFailure);
                        }

                    }

                    //Clearing the filter at this point prevents the control being pre filtered when we want to next display it
                    $scope.localDataSource.filter([]);

                    $scope.actionFormVisible = false;
                    //$scope[windowName].close();
                    $scope.workNoteContent = "";
                };

                $scope.populateFormForAction = function (actionId, windowName, mode) {
                    var _this = this;
                    $scope.isDisabled = false;
                    //populate the 'current' model from the passed in model
                    $scope.actionFormMode = mode != undefined ? mode : "create";
                    
                    var actions = $scope.vm.writeModel.incidentActions;
                    for (var i = 0; i < actions.length; i++) {
                        if (actions[i].id === actionId) {
                            $scope.currentAction = $.extend({}, actions[i]);
                            $scope.currentAction.assignedToIndividuals = [];

                            // Get the current action uploaded files from service
                            dataService.documentsForAction($scope.currentAction.id).then(function (data) {
                                $scope.currentAction.documentList = [];
                                vm.model.documentList = $scope.currentAction.documentList;
                                _.each(data, function (item, idx) {
                                    if (item.length !== 0) {
                                        _.each(item, function(data, idx) {
                                            $scope.currentAction.documentList.push({
                                                contentName: data.contentName,
                                                contentType: data.contentType,
                                                id: parseInt(data.id)
                                            });
                                        });
                                    } 
                                });
                            });

                            // set ActionModel for Existing selected action
                            vm.model.documentList = $scope.currentAction.documentList;
                            vm.model.url = window.location.protocol + '//' + window.location.host + '/api/incident/' + vm.config.activeTab + '/' + actionId + '/Document';

                            $scope.currentAction.assignedToIndividuals = actions[i].assignedTo;

                            $scope.actionFormVisible = true;
                            return;
                        }
                    }
                    userMsg.popupGeneralError("Could not retrieve Action");
                };

                $scope.openActionForm = function (windowName) {
                    $scope.isDisabled = true;
                    $scope.clearCurrentAction();

                    // set ActionModel for New Action
                    $scope.currentAction.documentList = [];
                    vm.model.documentList = [];
                    vm.model.url = window.location.protocol + '//' + window.location.host + '/api/incident/actions/0/Document';

                    $scope.actionFormVisible = true;
                };

                // Start Kendo UI Users MultiSelect
                $scope.localDataSource = new kendo.data.DataSource({
                    transport: {
                        read: {
                            url: window.location.protocol + '//' + window.location.host + '/api/useradmin/api/incident/' + vm.model.incidentId + '/assignableusers',
                            dataType: "json"
                        }
                    }
                });

                $scope.userSelection = {
                    placeholder: "Select individuals...",
                    dataTextField: "displayName",
                    dataValueField: "objectGuid",
                    valuePrimitive: true,
                    autoBind: false,
                    dataSource: $scope.localDataSource
                };
                // End Kendo UI Users MultiSelect

                $scope.closeActionForm = function () {
                    $scope.actionForm.$setPristine();
                    $scope.actionFormVisible = false;
                    $scope.incidentActionsListDataSource.read();
                };



                //Setting up the kendo UI controls
                //The model to apply to the schema
                var incidentActionsListingmodel = {
                    id: "Id",
                    fields: {
                        Id: { type: "number" },
                        IncidentId: { type: "number" },
                        AssignedTo: { type: "string" },
                        AssignedBy: { type: "string" },
                        RequestedBy: { type: "string" },
                        AssignedOn: { type: "date" },
                        ActionDescription: { type: "string" },
                        Comments: { type: "string" },
                        DateOfCreation: { type: "date" },
                        Status: { type: "string" },
                        StatusId: { type: "number" },
                        AssignedToId: { type: "number" },
                        AssignedById: { type: "number" },
                        RequestedById: { type: "number" },
                        FileIds: { type: "string" },
                        FileNames: { type: "string" },
                        Files: { type: "string" } //N.B. This column isn't returned from the server, it's added by the data function after the data is returned
                    }
                }

                $scope.documentIfExist = function (event, id) {
                    var url = window.location.protocol + '//' +window.location.host + "/api/downloaddocument/" +id;
                    $http.get(url)
                    .then(function (response) {
                        window.open(url, "_parent");
                         return true;
                        }, function (response) {
                        event.preventDefault();
                        userMsg.popupGeneralError('File not found.');
                    });
                }

                //The schema used for the individual datasources
                var incidentActionsSchema = {
                    data: function (data) {
                        var returnData = data.value;

                        // Clear documents for every action.
                        if ($scope.vm.writeModel !== undefined) {
                            _.each($scope.vm.writeModel.incidentActions, function (data, idx) {
                                data.documentList = [];
                            });
                        }

                        for (var i = 0; i < data.value.length; i++) {
                            var tempItem = data.value[i];
                            tempItem.Files = "";

                            if (tempItem.FileIds != null) {
                                var ids = tempItem.FileIds.trim().split(",");
                                var names = tempItem.FileNames.trim().split(",");

                                for (var j = 0; j < ids.length - 1; j++) {
                                    if (ids[j] !== "") {
                                        // Reasign the documents for each action
                                        if ($scope.vm.writeModel !== undefined) {
                                            _.each($scope.vm.writeModel.incidentActions, function(data, idx) {
                                                if(tempItem.Id === data.id) {
                                                    data.documentList.push({
                                                        contentName: names[j],
                                                        contentType: names[j].split('.')[1],
                                                        id: ids[j]
                                                    });
                                                }
                                            });
                                        }
                                    }
                                    tempItem.Files += "<a ng-click=\"documentIfExist($event," + ids[j] + ")\" href='' >" + names[j] + " </a><br>";
                                }
                            }
                        }

                        return returnData;
                    },
                    total: function(data) {
                        return data['odata.count'];
                    },
                model: incidentActionsListingmodel
            };

              //Parameter map function
                var parameterMapper = function(data) {
                    var d = kendo.data.transports.odata.parameterMap(data);
                    d.IncidentId = vm.model.incidentId;
                    return d;
                }

                //Define datasource for incidents with active Actions
                $scope.incidentActionsListDataSource = new kendo.data.DataSource({
                    type: "odata-v4",
                    transport: {
                        read: { url: "/odata/IncidentActionListing/", cache: false },
                        dataType: "json",
                        parameterMap: parameterMapper
                    },
                    serverSorting: false,
                    serverPaging: false,
                    serverFiltering: true,
                    filter: { field: "IncidentId", operator: "eq", value: vm.model.incidentId },

                    schema: incidentActionsSchema,
                    error: function(error) {
                        if(error.status === 403 || error.errorThrown == "Forbidden") {
                            //TODO: I used window.locaction as $location.path(); produced an error. see if it can be fixed
                            window.location = "../error/AuthoriserDecline";
                        }
                    }
                });

                $scope.incidentActionsListGridOptions = {
                    groupable: false,
                    resizable: false,
                    dataBound: $scope.handleGridDatabound,
                    columns: [
                        {
                            title: "",
                            template: "<div ng-hide=\"'${Status}'=='Closed'\"> " +
                                "<span title=\"Update\" class=\"glyphicon glyphicon-edit\" ng-click=\"populateFormForAction(${Id},'win1', 'update')\" ng-show=\"${IsAuthorised}\" ng-disabled=\"${!IsAuthorised}\" ></span>" +
                                "<span title=\"Respond\" class=\"glyphicon-respond\" ng-click=\"populateFormForAction(${Id},'win1', 'response');\"></span>" +
                                "</div>",
                            width: 1
                        },
                        {
                            field: "AssignedTo",
                            title: "Assignee",
                            width: 3
                        },
                        {
                            field: "AssignedBy",
                            title: "Assigned By",
                            width: 3
                        },
                        {
                            field: "AssignedOn",
                            title: "Assigned On",
                            width: 3,
                            format: "{0:dd/MM/yyyy}"
                        },
                        {
                            field: "ActionDescription",
                            title: "Description",
                            width: 7
                        },
                        {
                            field: "Comments", 
                            title: "Comments",
                            width: 7
                        },
                        {
                            field: "Files",
                            title: "Attachments",
                            encode: false,
                            template: "#=Files#",
                            width: 3
                        },
                        {
                            field: "Status",
                            title: "Action Status",
                            width: 3
                        }
                    ]
                };


                //TODO: Refactor this code, and all of the main angular controllers as a whole, into angular services, and helper libraries - Bryan
                $scope.$watch("vm.odata", function(value) {
                    if(!value) {
                        return;
                    }

                    //TODO:Bryan, get these search names from the tabs which are desired to be displayed
                    var searchNames = vm.tabsAvailable;
                    var indexes = { };
                    if (!(typeof searchNames === "undefined")) {

                        for (var i = 0; i < searchNames.length; i++) {
                            indexes[searchNames[i]]= - 1;

                            _.each(appConfig.visibleIncidentTabs, function(tabName, index) {
                                var searchName = searchNames[i];
                                if (tabName === searchName)
                                    indexes[searchName]= index;
                            });

                            //If action is not one of the odata enpoints returned then we need to make sure it doesn't appear in the visible tab list, otherwise make sure it does
                            if (_.find(value, function(item) { return item.name === searchNames[i]; }) == undefined) {
                                if (indexes[searchNames[i]]> - 1)
                                    appConfig.visibleIncidentTabs.splice(indexes[searchNames[i]], 1);

                            } else {
                                if (indexes[searchNames[i]] === - 1)
                                    appConfig.visibleIncidentTabs.push(searchNames[i]);
                            }
                        }
                    }
                });
            }
        ]);
}) ();

//var check = function() {
//    alert('test');
//    return false;
//}