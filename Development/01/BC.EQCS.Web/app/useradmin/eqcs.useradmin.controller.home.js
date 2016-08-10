'use strict';
(function () {

    angular.module('eqcs.useradmin')
        .controller('UserAdminHomeCtrl', [
        '$scope', 'appConfig',
        function ($scope, appConfig) {
            var vm = this;

            vm.config = {
                app: appConfig
            };
        }
    ]);
})()