'use strict';
(function () {
    angular.module('eqcs.useradmin.constant')
        .constant("appConfig", {
            menu: [
                { route: '/useradmin/home', label: 'Home', name: 'home' }
            ]
        });
})()