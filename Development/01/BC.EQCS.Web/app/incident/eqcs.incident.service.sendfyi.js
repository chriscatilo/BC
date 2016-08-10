'use strict';
(function () {
    angular.module("eqcs.incident.service")
        .factory("sendFYIService", [
             'dataService', 'userMsg', 'incidentCommands', 'viewUtils',
            function (dataService, userMsg, incidentCommands, viewUtils) {

                return {
                    create: function ($scope) {
                        $scope.lockScrollPosition = function() {
                            var scrollPosition = [
                            self.pageXOffset || document.documentElement.scrollLeft || document.body.scrollLeft,
                            self.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop
                            ];
                            var html = jQuery('html'); // it would make more sense to apply this to body, but IE7 won't have that
                            html.data('scroll-position', scrollPosition);
                            html.data('previous-overflow', html.css('overflow'));
                            html.css('overflow', 'hidden');
                            window.scrollTo(scrollPosition[0], scrollPosition[1]);
                        };

                        $scope.unlockScrollPosition = function() {
                            var html = jQuery('html');
                            var scrollPosition = html.data('scroll-position');
                            html.css('overflow', html.data('previous-overflow'));
                            window.scrollTo(scrollPosition[0], scrollPosition[1])
                        };


                        $scope.isFYIPermitted = $scope.vm.readModel.status == "In Progress";

                        $scope.showSendFYIForm = function () {

                            $scope.sendFYIFormVisible = true;
                            $scope.sendFYIForm.$setPristine();
                            $scope.sendFYIFormSelectedRecipients = null;
                            $scope.sendFYIFormMessage = null;
                            $scope.disableSendFYICommand = false;
                            $scope.sendFYIServerValidationErrors = [];

                            //Block page scrolling
                            $scope.lockScrollPosition();

                        };

                        $scope.closeSendFYIForm = function () {
                            $scope.sendFYIFormVisible = false;

                            //unblock page scrolling
                            $scope.unlockScrollPosition();
                        };

                        $scope.sendFYI = function() {
                          
                            $scope.sendFYIServerValidationErrors = [];

                            if (recipientsSelected()) {

                                var fyiModel = {
                                    IncidentId: $scope.vm.readModel.id,
                                    Recipients: $scope.sendFYIFormSelectedRecipients,
                                    OptionalMessage: $scope.sendFYIFormMessage
                                };

                                $scope.showOverlay();

                                dataService.sendFYIMessage(fyiModel)
                                    .then(function() {
                                        userMsg.popup("FYI message sent successfully");
                                        $scope.incidentActivityListDataSource.read();
                                        $scope.hideOverlay();
                                        $scope.closeSendFYIForm();

                                    }, function() {
                                        userMsg.popupGeneralError("Failed to send FYI message");
                                        $scope.hideOverlay();
                                    });

                            } else {

                                alert($scope.sendFYIServerValidationErrors.join());
                            }
                        };
                        
                      // Validation
                        var recipientsSelected = function () {
                            
                          var validRecipients = $scope.sendFYIFormSelectedRecipients && $scope.sendFYIFormSelectedRecipients.length > 0;
                          if (validRecipients == false) {
                              $scope.sendFYIServerValidationErrors.push("Please select one or more recipients");
                          }
                          return validRecipients;
                        }

                     
                    }
                };
            }
        ]);
})();