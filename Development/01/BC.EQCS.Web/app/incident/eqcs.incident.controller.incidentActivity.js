'use strict';
(function () {

    angular.module('eqcs.incident')
        .controller('IncidentActivityCtrl', [
            '$scope', '$q', 'dataService', 'userMsg', 'formConfig', 'appConfig', '$routeParams', 'getIncident', 'sendFYIService', '$location',
            function ($scope, $q, dataService, userMsg, formConfig, appConfig, $routeParams, getIncident, sendFYIService, $location) {

                var vm = this;
                vm.model = { incidentId: 0 };
                vm.config = {
                    app: appConfig,
                    activeMenu: 'edit',
                    menusVisible: ['home', 'new', 'edit']
                };

                vm.isIncidentIdInRoute = !angular.isUndefined($routeParams.id);

                if (vm.isIncidentIdInRoute) {
                    vm.model.incidentId = +$routeParams.id;
                    vm.config.activeTab = 'activity';
                }

                $scope.closeActivityForm = function () {
                    $scope.activityForm.$setPristine();
                    $scope.activityFormVisible = false;
                };

                var onGetIncidentSuccess = function (payload) {
                    if (payload.data.readModel.status === "Rejected") {
                        $location.path(payload.data.readModel.id);
                    }
                    if (payload.data.readModel.status === "Draft" || payload.data.readModel.status === "Submitted") {
                        $location.path(payload.data.readModel.id);
                    } else {
                        angular.extend($scope.vm, payload.data);

                        sendFYIService.create($scope);

                        $scope.addActionAvailable = payload.data.readModel.status === "In Progress";
                        $scope.canAddWorkNote = payload.data.readModel.status !== "Closed"; //vm.readModel.status

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


                var requestTemplate = { IncidentId: 0, Content: "" };
                $scope.saveNClose = function (windowName) {


                    var _this = this;

                    var modelId = vm.model.incidentId;
                    var content = $scope.workNoteContent;
                    var wNModel = { IncidentId: modelId, Content: content };


                    if (!$scope.activityForm.$valid) {
                        for (var validationType in $scope.activityForm.$error) {
                            if ($scope.activityForm.$error.hasOwnProperty(validationType))
                                for (var i = 0; i < $scope.activityForm.$error[validationType].length; i++) {
                                    userMsg.popupGeneralError($scope.activityForm.$error[validationType][i].$name + " : " + validationType);
                                }
                        }
                        userMsg.popupGeneralError("Selection is invalid, please correct");
                        return;
                    }

                    $scope.showOverlay();
                    dataService.saveWorkNote(wNModel)
                        .then(function () {
                                //popup = function (msg, type, time, title)
                                userMsg.popup("Work note saved successfully");
                                $scope.incidentActivityListDataSource.read();
                                dataService.sendWorkNoteNotification(wNModel.IncidentId);
                        }, function() {
                                userMsg.popupGeneralError("Failed to save the work note");
                                $scope.hideOverlay();
                            }
                        );

                    $scope.activityFormVisible = false;
                    $scope.workNoteContent = "";
                };


                $scope.openNote = function (windowName) {
                    $scope.activityFormVisible = true;
                };
                
                //Setting up the kendo UI controls
                //The model to apply to the schema
                var incidentActivityListingmodel = {
                    id: "Id",
                    fields: {
                        Id: { type: "number" },
                        IncidentId: { type: "number" },
                        DateTimeOfActivity: { type: "date" },
                        Username: { type: "string" },
                        LogType: { type: "string" },
                        Payload: { type: "string" }
                    }
                }

                //The schema used for the individual datasources
                var incidentActivitySchema = {
                    data: function (data) {
                       var returnData = data.value;
                        return returnData;
                    },
                    total: function (data) {
                        return data['odata.count'];
                    },
                    model: incidentActivityListingmodel
                };

                //Parameter map function
                var parameterMapper = function (data) {
                    var d = kendo.data.transports.odata.parameterMap(data);
                    d.IncidentId = vm.model.incidentId;
                    return d;
                }


                //Define datasource for incidents with active Actions
                $scope.incidentActivityListDataSource = new kendo.data.DataSource({
                    type: "odata-v4",
                    transport: {
                        read: { url: "/odata/IncidentActivityListing/", cache: false },
                        dataType: "json",
                        parameterMap: parameterMapper
                    },
                    serverSorting: false,
                    serverPaging: false,
                    serverFiltering: true,
                    sortable: {
                        mode: "single",
                        allowUnsort: false
                    },
                    
                    sort: { field: "Id", dir: "desc" },
                    filter: { field: "IncidentId", operator: "eq", value: vm.model.incidentId },
                    pageSize: 10,
                    schema: incidentActivitySchema,
                    error: function (error) {
                        if (error.status === 403 || error.errorThrown ==  "Forbidden") {
                            //TODO: I used window.locaction as $location.path(); produced an error. see if it can be fixed
                            window.location = "../error/AuthoriserDecline";
                        }
                    }
                });

                $scope.incidentActivityListGridOptions = {
                    groupable: false,
                    resizable: false,
                    dataBound: $scope.handleGridDatabound,
                    filterable: false,//{
                        //mode: "menu"
                    //},
                    sortable: {
                        mode: "single",
                        allowUnsort: false
                    },
                    pageable: {
                        refresh: true,
                        pageSizes: true,
                        buttonCount: 5
                    },
                    columns: [
                        {
                            field: "Username",
                            title: "User name",
                            width: 3//,
                            //filterable: {
                            //    extra: false,
                            //    operators: {
                            //        string: {
                            //            eq: "Equals..."
                            //        }
                            //    }
                            //}
                        },
                        {
                            field: "LogType",
                            title: "Log Type",
                            width: 3,
                            sortable: { allowUnsort: false }//,
                            //filterable: {
                            //    extra: false,
                            //    operators: {
                            //        string: {
                            //            eq: "Equals..."
                            //        }
                            //    }
                            //}

                        },
                        {
                            field: "Payload",
                            title: "Information",
                            width: 7,
                            encoded: false,
                            template: '<activitypayload logtype="${LogType}" payload="${Payload}" rowid="${Id}"/>'
                        },
                        {
                            field: "DateTimeOfActivity",
                            title: "Date Of Activity",
                            format: "{0:dd/MM/yyyy HH:mm:ss}",
                            width: 3//,
                            //filterable: {
                            //    extra: true,
                            //    operators: {
                            //        ui: "datetimepicker"
                            //    }
                            //}

                        }
                    ]
                };

                //TODO: Refactor this code, and all of the main angular controllers as a whole, into angular services, and helper libraries - Bryan
                $scope.$watch("vm.odata", function(value) {
                    if (!value) {
                        return;
                    }

                    //TODO:Bryan, get these search names from the tabs which are desired to be displayed
                    var searchNames = vm.tabsAvailable;
                    var indexes = {};
                    if (!(typeof searchNames === "undefined")) {

                        for (var i = 0; i < searchNames.length; i++) {
                            indexes[searchNames[i]] = -1;

                            _.each(appConfig.visibleIncidentTabs, function(tabName, index) {
                                var searchName = searchNames[i];
                                if (tabName === searchName)
                                    indexes[searchName] = index;
                            });

                            //If action is not one of the odata enpoints returned then we need to make sure it doesn't appear in the visible tab list, otherwise make sure it does
                            if (_.find(value, function(item) { return item.name === searchNames[i]; }) == undefined) {
                                if (indexes[searchNames[i]] > -1)
                                    appConfig.visibleIncidentTabs.splice(indexes[searchNames[i]], 1);

                            } else {
                                if (indexes[searchNames[i]] === -1)
                                    appConfig.visibleIncidentTabs.push(searchNames[i]);
                            }
                        }
                    }
                });
              
                // Configure Kendo UI MultiSelect Control for Recipients Input
                // Unfortunately this cannot be done inside sendfyi service module because the service is created in a callback and KendoUI control does not bind in that event. 
                $scope.localDataSource = new kendo.data.DataSource({
                    transport: {
                        read: {
                            url: window.location.protocol + '//' + window.location.host + '/api/useradmin/api/incident/' + $routeParams.id + '/assignableusers',
                            //url: "http://localhost:52429/api/useradmin/api/incident/2728/assignableusers",
                            dataType: "json"
                        }
                    }
                });

                $scope.sendFYIFormRecipientOptions = {
                    placeholder: "Select recipients...",
                    dataTextField: "displayName",
                    dataValueField: "objectGuid",
                    valuePrimitive: true,
                    autoBind: false,
                    dataSource: $scope.localDataSource
                };
            }
        ]);
})()