'use strict';
(function () {
    angular.module('eqcs.core.directive')
        .directive('menuTabs', function () {
            return {
                replace: true,
                scope: {
                    menuItems: "=",
                    menuActive: "=",
                    menusVisible: "=",
                    visibleTabs: "=",
                    tabItems: "=",
                    tabActive: "=",
                    tabParam: "=",
                    showTabs: "="
                },
                link: function (scope, elem, attr) {
                    //Remove the tabItems if they don't appear int he visibleTabs list
                    var processVisibleTabs = function (tabs) {

                        scope.tabsToDisplay = scope.tabItems;

                        if (scope.visibleTabs !== undefined) {
                            scope.tabsToDisplay = [];

                            for (var i = 0; i < scope.tabItems.length; i++) {
                                if (scope.visibleTabs.indexOf(scope.tabItems[i].name) > -1) {
                                    scope.tabsToDisplay.push(scope.tabItems[i]);
                                }
                            }
                        }

                        scope.menusToDisplay = scope.menuItems;

                        if (typeof scope.tabParam !== 'undefined')
                        for (var j = 0; j < scope.menuItems.length; j++) {
                            scope.menuItems[j].route = scope.menuItems[j].route.replace("<tabParam>", scope.tabParam);
                        }

                        if (scope.menusVisible !== undefined) {
                            scope.menusToDisplay = [];

                            for (var i = 0; i < scope.menuItems.length; i++) {
                                if (scope.menusVisible.indexOf(scope.menuItems[i].name) > -1) {
                                    scope.menusToDisplay.push(scope.menuItems[i]);
                                }
                            }
                        }

                    }

                    scope.$watchCollection("visibleTabs", processVisibleTabs);
                    scope.$watchCollection("menusVisible", processVisibleTabs);
                },
                template:
                    '<section class="app-header navbar navbar-fixed-top" style="top: 69px;">' +
                        '<div class="container">' +
                            '<div class=navbar-collapse collapse" id="navbar-main">' +
                                '<ul class="nav navbar-nav">' +
                                    '<li ng-repeat="item in menusToDisplay" ng-class="{ \'active\' : item.name == menuActive }">' +
                                        '<a href="{{ item.route }}" ng-bind="item.label" target="_self"></a></li>' +
                                '</ul>' +
                                '<ul class="nav nav-tabs" ng-if="showTabs">' +
                                    '<li ng-repeat="item in tabsToDisplay" ng-class="{ \'active\' : item.name == tabActive }">' +
                                        '<a href="{{ item.route }}/{{ tabParam }}" ng-bind="item.label"></a></li>' +
                                '</ul>' +
                        '</div>' +
                    '</section>'
            };
        });
})()