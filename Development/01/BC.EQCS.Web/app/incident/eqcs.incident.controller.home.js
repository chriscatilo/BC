'use strict';
(function () {
    angular.module('eqcs.incident')
        .controller('IncidentHomeCtrl', [
            '$scope', 'appConfig', '$rootScope', 'dataService', 'currentuser',
            function ($scope, appConfig, $rootScope, dataService, currentuser) {
                var vm = this;

                vm.user = currentuser.data;
                $scope.username = currentuser.data.firstName + " " + currentuser.data.surname;
                $scope.userRoles = currentuser.data.applicationRoles;

                $scope.userMayWantReports = false;
                for (var j = 0; j < $scope.userRoles.length; j++) {
                    var role = $scope.userRoles[j];
                    $scope.userMayWantReports = $scope.userMayWantReports || role.shortCode === "GLOBAL_OPERATIONS";
                    $scope.userMayWantReports = $scope.userMayWantReports || role.shortCode === "RMT";
                }


                vm.config = {
                    app: appConfig,
                    activeMenu: 'home',
                    menusVisible: ['home', 'new']
                };

                $scope.tabStripOptions = "";

                $scope.SelectLists = {};

                var getStringListFromDataArray = function (dataArrayIn, fieldName, selectListName) {
                    var distinctValues = {};

                    //Preload Distinct values with any values which may have already been added to the list on previous calls
                    if ($scope.SelectLists != undefined && $scope.SelectLists[selectListName] != undefined) {
                        for (var k = 0; k < $scope.SelectLists[selectListName].length; k++) {
                            distinctValues[$scope.SelectLists[selectListName][k]] = "";
                        }
                    }


                    for (var i = 0; i < dataArrayIn.length; i++) {
                        if (dataArrayIn[i][fieldName]) {
                            var itemName = dataArrayIn[i][fieldName];

                            distinctValues[itemName] = "";
                        }
                    }


                    //Now convet to a string array and return
                    var stringArrayToReturn = [];
                    for (var fieldItem in distinctValues) {
                        if (distinctValues.hasOwnProperty(fieldItem)) {
                            stringArrayToReturn.push(fieldItem);
                        }
                    }

                    $scope.SelectLists[selectListName] = stringArrayToReturn;
                };

                var genLiveDisplayedCatOrSubCat = function (element) {
                    element.kendoDropDownList({
                        dataSource: $scope.SelectLists.LiveCatOrSubCat,
                        optionLabel: "--Select Value--"
                    });
                };
                var genClosedDisplayedCatOrSubCat = function (element) {
                    element.kendoDropDownList({
                        dataSource: $scope.SelectLists.ClosedCatOrSubCat,
                        optionLabel: "--Select Value--"
                    });
                };
                var genActiveActionsDisplayedCatOrSubCat = function (element) {
                    element.kendoDropDownList({
                        dataSource: $scope.SelectLists.ActiveActionsCatOrSubCat,
                        optionLabel: "--Select Value--"
                    });
                };
                var genStatus_live = function (element) {
                    element.kendoDropDownList({
                        dataSource: $scope.SelectLists.Status,
                        optionLabel: "--Select Value--"
                    });
                };

                var genStatus_actionsActive = function (element) {
                    element.kendoDropDownList({
                        dataSource: $scope.SelectLists.Status,
                        optionLabel: "--Select Value--"
                    });
                };

                var genStatus_closed = function (element) {
                    element.kendoDropDownList({
                        dataSource: $scope.SelectLists.Status,
                        optionLabel: "--Select Value--"
                    });
                };
                var genProduct = function (element) {
                    element.kendoDropDownList({
                        dataSource: $scope.SelectLists.Product,
                        optionLabel: "--Select Value--"
                    });
                };
                var genLoggedBy = function (element) {
                    element.kendoDropDownList({
                        dataSource: $scope.SelectLists.LoggedBy,
                        optionLabel: "--Select Value--"
                    });
                };
                var genTestCentreNumber_liveIncidentsList = function (element) {
                    element.removeAttr("data-bind");
                    element.kendoMultiSelect({
                        autoClose: false,
                        dataSource: $scope.SelectLists.TestCentreNumber,
                        change: function (e) {
                            var filter = { logic: "or", filters: [] };
                            var values = this.value();
                            $.each(values, function (i, v) {
                                filter.filters.push({ field: "TestCentreNumber", operator: "eq", value: v });
                            });

                            //var currentFilter = $scope.liveIncidentsInitFilter;
                            //var newFilter = { logic: "and", filters: [currentFilter, filter] };

                            $scope.liveIncidentsListDataSource.filter(filter);
                        }

                    });
                };
                var genTestCentreNumber_incidentActionsActiveList = function (element) {
                    element.removeAttr("data-bind");
                    element.kendoMultiSelect({
                        autoClose: false,
                        dataSource: $scope.SelectLists.TestCentreNumber,
                        change: function (e) {
                            var filter = { logic: "or", filters: [] };
                            var values = this.value();
                            $.each(values, function (i, v) {
                                filter.filters.push({ field: "TestCentreNumber", operator: "eq", value: v });
                            });

                            //var currentFilter = $scope.incidentActionsActiveInitFilter;
                            //var newFilter = { logic: "and", filters: [currentFilter, filter] };

                            $scope.incidentActionsActiveListDataSource.filter(filter);
                        }

                    });
                };
                var genTestCentreNumber_closedIncidentsList = function (element) {
                    element.removeAttr("data-bind");
                    element.kendoMultiSelect({
                        autoClose: false,
                        dataSource: $scope.SelectLists.TestCentreNumber,
                        change: function (e) {
                            var filter = { logic: "or", filters: [] };
                            var values = this.value();
                            $.each(values, function (i, v) {
                                filter.filters.push({ field: "TestCentreNumber", operator: "eq", value: v });
                            });

                            //var currentFilter = $scope.closedIncidentsInitFilter;
                            //var newFilter = { logic: "and", filters: [currentFilter, filter] };

                            $scope.closedIncidentsListDataSource.filter(filter);
                        }

                    });
                };
                var genIncidentNumber = function (element) {
                    element.kendoAutoComplete({
                        dataSource: $scope.SelectLists.IncidentNumber
                    });
                };
                var genReportToUkVi = function (element) {
                    element.kendoDropDownList({
                        dataSource: ["Yes", "No", "N/A"],//$scope.SelectLists.ReportUkvi,
                        optionLabel: "--Select Value--"
                    });
                };

                //The model to apply to the schema
                var model = {
                    id: "Id",
                    fields: {
                        Id: { type: "number" },
                        Status: { type: "string" },
                        IncidentNumber: { type: "string" },
                        TestCentreNumber: { type: "string" },
                        LoggedBy: { type: "string" },
                        Product: { type: "string" },
                        Category: { type: "string" },
                        SubCategory: { type: "string" },
                        DisplayedCatOrSubCat: { type: "string" },
                        TestDate: { type: "date" },
                        IncidentDate: { type: "date" },
                        HasActiveAction: { type: "string" },
                        ReportUkvi: { type: "string" },
                        UkviFollowUpDate: { type: "date" }
                    }
                }

                //The schema used for the individual datasources
                var commonSchema = {
                    data: function (data) {
                        var returnData = data.value;
                        return returnData;
                    },
                    total: function (data) {
                        return data["odata.count"];
                    },
                    model: model
                };

                //Parameter map function
                var parameterMapper = function (data) {
                    var d = kendo.data.transports.odata.parameterMap(data);
                    return d;
                }
                
                //Define datasource for live incidents
                $scope.liveIncidentsListDataSource = new kendo.data.DataSource({
                    type: "odata-v4",

                    transport: {
                        read: { url: "/odata/LiveIncidentsListing/", cache: false },
                        dataType: "json",
                        parameterMap: parameterMapper
                    },
                    serverSorting: true,
                    serverPaging: false,
                    serverFiltering: true,
                    sortable: {
                        mode: "single",
                        allowUnsort: false
                    },
                    sort: { field: "IncidentNumber", dir: "desc" },
                    pageSize: 50,
                    schema: commonSchema
                });



                $scope.incidentActionsActiveInitFilter = { field: "HasActiveAction", operator: "eq", value: "true" };
                ////Define datasource for incidents with active Actions
                $scope.incidentActionsActiveListDataSource = new kendo.data.DataSource({
                    type: "odata-v4",
                    transport: {
                        read: { url: "/odata/ActiveActionIncidentsListing/", cache: false },
                        dataType: "json",
                        parameterMap: parameterMapper
                    },
                    serverSorting: true,
                    serverPaging: false,
                    serverFiltering: true,
                    sortable: {
                        mode: "single",
                        allowUnsort: false
                    },
                    batch: true,
                    sort: { field: "IncidentNumber", dir: "desc" },
                    pageSize: 50,
                    schema: commonSchema
                });


                $scope.closedIncidentsInitFilter = { field: "Status", operator: "eq", value: "Closed" };
                ////Defined datasource for closed incidents
                $scope.closedIncidentsListDataSource = new kendo.data.DataSource({
                    type: "odata-v4",
                    transport: {
                        read: { url: "/odata/ClosedIncidentsListing/", cache: false },
                        dataType: "json",
                        parameterMap: parameterMapper
                    },
                    serverSorting: true,
                    serverPaging: false,
                    serverFiltering: true,
                    sortable: {
                        mode: "single",
                        allowUnsort: false
                    },
                    sort: { field: "IncidentNumber", dir: "desc" },
                    pageSize: 50,
                    schema: commonSchema
                });

                $scope.liveIncidentsListGridOptions = {
                    groupable: false,
                    resizable: false,
                    dataBound: function (arg) {

                        getStringListFromDataArray(arg.sender.dataSource._data, "IncidentNumber", "IncidentNumber");
                        getStringListFromDataArray(arg.sender.dataSource._data, "Status", "Status");
                        getStringListFromDataArray(arg.sender.dataSource._data, "TestCentreNumber", "TestCentreNumber");
                        getStringListFromDataArray(arg.sender.dataSource._data, "DisplayedCatOrSubCat", "LiveCatOrSubCat");
                        getStringListFromDataArray(arg.sender.dataSource._data, "Product", "Product");
                        getStringListFromDataArray(arg.sender.dataSource._data, "LoggedBy", "LoggedBy");
                        getStringListFromDataArray(arg.sender.dataSource._data, "ReportUkvi", "ReportUkvi");

                    },
                    filterable: {
                        mode: "menu"
                    },
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
                            field: "IncidentNumber",
                            title: "Incident Number",
                            width: 3,
                            template: "<div class=\"{{username == '${LoggedBy}'? 'userLogged' : ''}}\"> <a  href='/incident/${Id}'>${IncidentNumber}</a> </div>",
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        contains: "Contains..."
                                    }
                                },
                                ui: genIncidentNumber
                            }
                        },
                        {
                            field: "Status",
                            title: "Status",
                            width: 3,
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "Equals..."
                                    }
                                },
                                ui: genStatus_live
                            }
                        },
                        {
                            field: "TestCentreNumber",
                            title: "Test Centre Number",
                            width: 4,
                            filterable: {
                                multi: true,
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "Equals..."
                                    }
                                },
                                ui: genTestCentreNumber_liveIncidentsList
                            }
                        },
                        {
                            field: "LoggedBy",
                            title: "Logged By",
                            width: 3,
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "Equals..."
                                    }
                                },
                                ui: genLoggedBy
                            }
                        },
                        {
                            field: "Product",
                            title: "Product",
                            width: 3,
                            sortable: { allowUnsort: false },
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "equals..."
                                    }
                                },
                                ui: genProduct
                            }

                        },
                        {
                            field: "DisplayedCatOrSubCat",
                            title: "Category",
                            width: 5,
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "equals..."
                                    }
                                },
                                ui: genLiveDisplayedCatOrSubCat
                            }
                        },
                        {
                            field: "TestDate",
                            title: "Test Date",
                            format: "{0:dd/MM/yyyy}",
                            width: 3,
                            filterable: {
                                extra: true,
                                operators: {
                                    ui: "datetimepicker"
                                }
                            }

                        },
                        {
                            field: "IncidentDate",
                            title: "Incident Date",
                            format: "{0:dd/MM/yyyy}",
                            width: 3,
                            filterable: {
                                extra: true,
                                operators: {
                                    ui: "datetimepicker"
                                }
                            }

                        },
                        {
                            field: "ReportUkvi",
                            title: "Report to UKVI",
                            width: 3,
                            sortable: { allowUnsort: false },
                            encoded: false,
                            template: "${ReportUkvi} <a ng-show=\"'${ReportUkvi}'==='Yes' && ('${Status}'==='Closed' || '${Status}'==='In Progress') && userMayWantReports\" href='/incident/${Id}/report/ukviimmediate' target='_self'><div class=\"download\"></div></a>",
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "equals..."
                                    }
                                },
                                ui: genReportToUkVi
                            }
                        },
                        {
                            field: "UkviFollowUpDate",
                            title: "Followup Date",
                            format: "{0:dd/MM/yyyy}",
                            width: 3,
                            filterable: {
                                extra: true,
                                operators: {
                                    ui: "datetimepicker"
                                }
                            }
                        }
                    ]
                };

                $scope.incidentActionsActiveListGridOptions = {
                    groupable: false,
                    resizable: false,
                    dataBound: function (arg) {

                        getStringListFromDataArray(arg.sender.dataSource._data, "IncidentNumber", "IncidentNumber");
                        getStringListFromDataArray(arg.sender.dataSource._data, "Status", "Status");
                        getStringListFromDataArray(arg.sender.dataSource._data, "TestCentreNumber", "TestCentreNumber");
                        getStringListFromDataArray(arg.sender.dataSource._data, "DisplayedCatOrSubCat", "ActiveActionsCatOrSubCat");
                        getStringListFromDataArray(arg.sender.dataSource._data, "Product", "Product");
                        getStringListFromDataArray(arg.sender.dataSource._data, "LoggedBy", "LoggedBy");
                        getStringListFromDataArray(arg.sender.dataSource._data, "ReportUkvi", "ReportUkvi");

                    },
                    filterable: {
                        mode: "menu"
                    },
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
                            field: "IncidentNumber",
                            title: "Incident Number",
                            width: 3,
                            template: "<div class=\"{{username == '${LoggedBy}'? 'userLogged' : ''}}\"> <a  href='/incident/${Id}'>${IncidentNumber}</a> </div>",
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        contains: "Contains..."
                                    }
                                },
                                ui: genIncidentNumber
                            }
                        },
                        {
                            field: "Status",
                            title: "Status",
                            width: 3,
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "Equals..."
                                    }
                                },
                                ui: genStatus_actionsActive
                            }
                        },
                        {
                            field: "TestCentreNumber",
                            title: "Test Centre Number",
                            width: 4,
                            filterable: {
                                multi: true,
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "Equals..."
                                    }
                                },
                                ui: genTestCentreNumber_incidentActionsActiveList
                            }
                        },
                        {
                            field: "LoggedBy",
                            title: "Logged By",
                            width: 3,
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "Equals..."
                                    }
                                },
                                ui: genLoggedBy
                            }
                        },
                        {
                            field: "Product",
                            title: "Product",
                            width: 3,
                            sortable: { allowUnsort: false },
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "equals..."
                                    }
                                },
                                ui: genProduct
                            }

                        },
                        {
                            field: "DisplayedCatOrSubCat",
                            title: "Category",
                            width: 5,
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "equals..."
                                    }
                                },
                                ui: genActiveActionsDisplayedCatOrSubCat
                            }
                        },
                        {
                            field: "TestDate",
                            title: "Test Date",
                            format: "{0:dd/MM/yyyy}",
                            width: 3,
                            filterable: {
                                extra: true,
                                operators: {
                                    ui: "datetimepicker"
                                }
                            }

                        },
                        {
                            field: "IncidentDate",
                            title: "Incident Date",
                            format: "{0:dd/MM/yyyy}",
                            width: 3,
                            filterable: {
                                extra: true,
                                operators: {
                                    ui: "datetimepicker"
                                }
                            }

                        },
                        {
                            field: "ReportUkvi",
                            title: "Report to UKVI",
                            width: 3,
                            sortable: { allowUnsort: false },
                            encoded: false,
                            template: "${ReportUkvi} <a ng-show=\"'${ReportUkvi}'==='Yes' && ('${Status}'==='Closed' || '${Status}'==='In Progress')\" href='/incident/${Id}/report/ukviimmediate' target='_self'><div class=\"download\"></div></a>",
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "equals..."
                                    }
                                },
                                ui: genReportToUkVi
                            }
                        },
                        {
                            field: "UkviFollowUpDate",
                            title: "Followup Date",
                            format: "{0:dd/MM/yyyy}",
                            width: 3,
                            filterable: {
                                extra: true,
                                operators: {
                                    ui: "datetimepicker"
                                }
                            }
                        }
                    ]
                };

                $scope.closedIncidentsListGridOptions = {
                    groupable: false,
                    resizable: false,
                    dataBound: function (arg) {

                        getStringListFromDataArray(arg.sender.dataSource._data, "IncidentNumber", "IncidentNumber");
                        getStringListFromDataArray(arg.sender.dataSource._data, "Status", "Status");
                        getStringListFromDataArray(arg.sender.dataSource._data, "TestCentreNumber", "TestCentreNumber");
                        getStringListFromDataArray(arg.sender.dataSource._data, "DisplayedCatOrSubCat", "ClosedCatOrSubCat");
                        getStringListFromDataArray(arg.sender.dataSource._data, "Product", "Product");
                        getStringListFromDataArray(arg.sender.dataSource._data, "LoggedBy", "LoggedBy");
                        getStringListFromDataArray(arg.sender.dataSource._data, "ReportUkvi", "ReportUkvi");

                    },
                    filterable: {
                        mode: "menu"
                    },
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
                            field: "IncidentNumber",
                            title: "Incident Number",
                            width: 3,
                            template: "<div class=\"{{username == '${LoggedBy}'? 'userLogged' : ''}}\"> <a  href='/incident/${Id}'>${IncidentNumber}</a> </div>",
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        contains: "Contains..."
                                    }
                                },
                                ui: genIncidentNumber
                            }
                        },
                        {
                            field: "Status",
                            title: "Status",
                            width: 3,
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "Equals..."
                                    }
                                },
                                ui: genStatus_closed
                            }
                        },
                        {
                            field: "TestCentreNumber",
                            title: "Test Centre Number",
                            width: 4,
                            filterable: {
                                multi: true,
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "Equals..."
                                    }
                                },
                                ui: genTestCentreNumber_closedIncidentsList
                            }
                        },
                        {
                            field: "LoggedBy",
                            title: "Logged By",
                            width: 3,
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "Equals..."
                                    }
                                },
                                ui: genLoggedBy
                            }
                        },
                        {
                            field: "Product",
                            title: "Product",
                            width: 3,
                            sortable: { allowUnsort: false },
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "equals..."
                                    }
                                },
                                ui: genProduct
                            }

                        },
                        {
                            field: "DisplayedCatOrSubCat",
                            title: "Category",
                            width: 5,
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "equals..."
                                    }
                                },
                                ui: genClosedDisplayedCatOrSubCat
                            }
                        },
                        {
                            field: "TestDate",
                            title: "Test Date",
                            format: "{0:dd/MM/yyyy}",
                            width: 3,
                            filterable: {
                                extra: true,
                                operators: {
                                    ui: "datetimepicker"
                                }
                            }

                        },
                        {
                            field: "IncidentDate",
                            title: "Incident Date",
                            format: "{0:dd/MM/yyyy}",
                            width: 3,
                            filterable: {
                                extra: true,
                                operators: {
                                    ui: "datetimepicker"
                                }
                            }

                        },
                        {
                            field: "ReportUkvi",
                            title: "Report to UKVI",
                            width: 3,
                            sortable: { allowUnsort: false },
                            encoded: false,
                            template: "${ReportUkvi} <a ng-show=\"'${ReportUkvi}'==='Yes' && ('${Status}'==='Closed' || '${Status}'==='In Progress') && userMayWantReports\" href='/incident/${Id}/report/ukviimmediate' target='_self'><div class=\"download\"></div></a>",
                            filterable: {
                                extra: false,
                                operators: {
                                    string: {
                                        eq: "equals..."
                                    }
                                },
                                ui: genReportToUkVi
                            }
                        },
                        {
                            field: "UkviFollowUpDate",
                            title: "Followup Date",
                            format: "{0:dd/MM/yyyy}",
                            width: 3,
                            filterable: {
                                extra: true,
                                operators: {
                                    ui: "datetimepicker"
                                }
                            }
                        }
                    ]
                };


            }
        ]);
})();