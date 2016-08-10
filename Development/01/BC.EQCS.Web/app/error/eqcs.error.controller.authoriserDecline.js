'use strict';
(function () {

    angular.module('eqcs.error')
        .controller('errorAuthoriserDeclineCtrl', [
        '$scope', 'appConfig',
        function ($scope, appConfig) {
            var vm = this;

            vm.config = {
                app: appConfig
            };
        }
    ]);
})()