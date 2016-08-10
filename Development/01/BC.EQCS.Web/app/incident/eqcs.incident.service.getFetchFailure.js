'use strict';
(function () {
    angular.module('eqcs.incident.service')
        .factory('getFetchFailure', [
             'userMsg', '$location',
                function (userMsg, $location) {                
                    return function (payload) {
                     
                                var msg = payload.error.status === 404
                                    ? "Incident was not found"
                                    : payload.error.status === 403
                                    ? "User not authorised"
                                    : void 0;
                        
                                if (msg) {
                                    userMsg.popupByHttpErrorStatus(payload.error.status, msg);
                                    
                                } else {
                                    userMsg.popupGeneralError();
                                }

                                $location.path("/home").replace();
                            }

                            
                    }
         
    ]);
})()