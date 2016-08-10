'use strict';
(function() {

    angular.module("eqcs.core.service")
        .factory('userMsg', [
            'toaster',
            function (toaster) {

                var genericErrMsg = 'Oops, an application error has occurred';

                var popup = function (msg, type, time, title) {
                    type = type || "success";
                    time = time || 7000;
                    title = title || '';
                    toaster.pop(type, title, msg, time, 'trustedHtml');
                };

                var popupByHttpErrorStatus = function(status, error) {

                    var msg = !!error
                        ? error
                        : status == 403
                        ? 'User is not authorised'
                        : status == 404
                        ? "Resource was not found"
                        : genericErrMsg;
                    
                    toaster.pop('error', msg, null, 15000, 'trustedHtml');
                };
                
                var popupGeneralError = function (error) {

                    var msg = !!error
                        ? error
                        : genericErrMsg;

                    toaster.pop('error', msg, null, 15000, 'trustedHtml');
                };

                return {
                    
                    popup: popup,
                    
                    popupByHttpErrorStatus: popupByHttpErrorStatus,
                    
                    popupGeneralError: popupGeneralError,
                };
            }
        ]);

})()