'use strict';
(function () {

    angular.module('eqcs.auditing')
        .controller('AuditingHomeCtrl', [
        '$scope', 'appConfig',
        function ($scope, appConfig) {
            var vm = this;

            vm.config = {
                app: appConfig
            };
        }
    ]);
})()