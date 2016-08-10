'use strict';
(function () {
    angular
        .module('eqcs.incident.directive')
        .directive('activitypayload', function () {

            return {
                replace: true,
                restrict: 'E',
                scope:
                {
                    ngForm: '=',
                    ngModel: '='
                },
                controller: ["$scope", "appConfig",
                    function ($scope, appConfig) {
                        var vm = this;
                        $scope.genPayloadView = function (payLoad, logType) {
                          
                            if (logType !== "Change"
                                && logType !== "NewCandidate"
                                && logType !== "Submission"
                                && logType !== "ActionUpdated"
                                && logType !== "NewAction"
                                && logType !== "FYI"
                                && logType !== "IncidentSnapshot") {
                                return payLoad;
                            }
                            else {
                                return JSON.parse(payLoad);
                            }
                        };
                    }],

                link: function(scope, elem, attr) {
                    scope.payload = attr.payload;
                    scope.logtype = attr.logtype;
                    scope.rowid = attr.rowid;
                    scope.isFYILog = attr.logtype === "FYI";
                    scope.isDiffLog = attr.logtype === "Change" || attr.logtype === "ActionUpdated";
                    scope.isAllPropertyLog = attr.logtype === "Submission" || attr.logtype === "NewCandidate" || attr.logtype === "NewAction" || attr.logtype === "IncidentSnapshot";
                    scope.payloadModel = scope.genPayloadView(scope.payload, scope.logtype);
                },
                template: '<div>' +
                    '<button ng-show="isFYILog" type="button" class="btn" data-toggle="collapse" data-target="#fyi{{rowid}}">+</button>' +
                    '<div ng-show="isFYILog" id="fyi{{rowid}}" class="collapse out">' +
                        '<table class="table">' +
                            '<tr><td>Sent: </td><td>{{payloadModel.Sent}}</td>' +
                            '<tr><td>Subject: </td><td>{{payloadModel.Subject}}</td>' +
                            '<tr><td>From: </td><td>{{payloadModel.From}}</td>' +
                            '<tr><td>To: </td><td>{{payloadModel.To}}</td>' +
                            '<tr><td>Message: </td><td>{{payloadModel.Message}}</td>' +
                         '</table>' +
                 
                    '</div>' +

                    '<button ng-show="isDiffLog" type="button" class="btn" data-toggle="collapse" data-target="#incChange{{rowid}}">+</button>' +
                    '<div ng-show="isDiffLog" id="incChange{{rowid}}" class="collapse out">' +
                        '<table class="table table-striped">' +
                            '<thead>' +
                            '<tr>' +
                                '<th></th>' +
                                '<th>Before</th>' +
                                '<th>After</th>' +
                            '</tr>' +
                            '</thead>' +
                            '<tbody>' +
                                '<tr ng-repeat="row in payloadModel track by $index">' +
                                    '<td>{{row.Label}}</td>' +
                                    '<td>{{row.OriginalValue}}</td>' +
                                    '<td>{{row.NewValue}}</td>' +
                                '</tr>' +
                            '</tbody>' +
                        '</table>' +
                    '</div>' +
                    '<button ng-show="isAllPropertyLog" type="button" class="btn" data-toggle="collapse" data-target="#sub{{rowid}}">+</button>' +
                    '<div ng-show="isAllPropertyLog" id="sub{{rowid}}" class="collapse out">' +
                        '<table class="table table-striped">' +
                            '<thead>' +
                            '<tr>' +
                                '<th>Field</th>' +
                                '<th>Value</th>' +
                            '</tr>' +
                            '</thead>' +
                            '<tbody>' +
                                '<tr ng-repeat="row in payloadModel track by $index">' +
                                    '<td>{{row.Label}}</td>' +
                                    '<td>{{row.Value}}</td>' +
                                '</tr>' +
                            '</tbody>' +
                        '</table>' +
                    '</div>' +
                    '<div ng-hide="isDiffLog || isAllPropertyLog || isFYILog">{{payloadModel}}</div><div>'
            };
        });
})();
