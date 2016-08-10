'use strict';
(function () {
    angular.module('eqcs.auditing.constant')
        .constant("appConfig", {
            menu: [
                { route: '/auditing/home', label: 'Home', name: 'home' },
            ]
        });
})()