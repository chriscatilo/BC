'use strict';
(function () {
    angular.module('eqcs.core.directive')
        .directive('eqcstable', function () {
        return {
            replace: true,
            scope: {
                headers: "=",
                items: "=",
                deleteButton: "="
                },
            template:
                 '<div class="tableContainer">' +
                    '<table class="table">' +
                        '<th>' +
                            '<td></td>' +
                            '<td ng-repeat="header in headers">' +
                            '{{header}}'+
                            '<td>' +
                        '</th>' +
                        '<tr ng-repeat="row in items">' +
                            '<td class="delete" ng-hide="{{deleteButton == false}}" id="delete"></td>' +
                            '<td ng-repeat="fields in row"' +
                                '<p>{{fields}}</p>' +
                            '</td>' +
                        '</tr>' +
                    '</table>'  +
                '</div>'
            };
        });
})()