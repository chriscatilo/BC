
'use strict';
(function () {

    var myApp = angular.module('eqcs.login', []);

    myApp.controller('LoginAutoController', ['$scope', '$http', function ($scope, $http) {
        $scope.init = function() {

            //$http.post('/windows/windowslogin', { msg: 'hello word!' }).
            //    success(function(data, status, headers, config) {
            //        // this callback will be called asynchronously
            //        // when the response is available
            //    }).
            //    error(function(data, status, headers, config) {
            //        // called asynchronously if an error occurs
            //        // or server returns response with an error status.
            //    });


            //window.location.href = "/windows/WindowsLogin";


            $("#WindowsLogin").submit();
        }


    }]);
})();