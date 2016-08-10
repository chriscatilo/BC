'use strict';
(function () {

    angular.module('eqcs.error')
        .controller('errorHomeCtrl', [
        '$scope', 'appConfig',
        function ($scope, appConfig) {
            var vm = this;

            vm.config = {
                app: appConfig
            };
        }
    ]);
})()