'use strict';
(function () {
    angular
        .module('eqcs.core.directive')
        .directive('fileUploader', function () {
            return {
                replace: true,
                restrict: 'E',
                scope:
                {
                    ngcontainermodel: "=",
                },
                templateUrl: '../app/incident/templates/FileUploader.html',
                controller: ['$scope', 'fileUploaderService', 'userMsg', '$http', function ($scope, fileUploaderService, userMsg, $http) {
                    $scope.uploader = new fileUploaderService({
                        // add path if needed. 
                        url: window.location.protocol + '//' + window.location.host + "/api" + window.location.pathname + '/Document'
                    });

                    $scope.uploader.filters.push({
                        name: 'extensionFilter',
                        fn: function (item, options) {
                            var filename = item.name;
                            var extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();
                            if (extension === "pdf" || extension === "doc" || extension === "docx" || extension === "jpg")
                                return true;
                            else {
                                userMsg.popupGeneralError('Invalid file format. Please select a file with pdf/doc/docs or jpg format  and try again.');
                                return false;
                            }
                        }
                    });

                    $scope.uploader.filters.push({
                        name: 'sizeFilter',
                        fn: function (item, options) {

                            var fileSize = item.size;
                            if (fileSize === null)
                            { return true }
                            fileSize = parseInt(fileSize) / (1024 * 1024);
                            if (fileSize <= 10)
                                return true;
                            else {
                                userMsg.popupGeneralError('Selected file exceeds the 10MB file size limit. Please choose a new file and try again.');
                                return false;
                            }
                        }
                    });

                    function resetQueue() {
                        //if ($scope.ngcontainermodel.documentList.length > 0) {
                        $scope.uploader.queue = [];
                        angular.forEach($scope.ngcontainermodel.documentList, function (e) {
                            var item = {
                                file: { name: e.contentName },
                                isUploading: true,
                                download: function () {
                                    var url = window.location.protocol + '//' + window.location.host + "/api/downloaddocument/" + e.id;
                                    $http.get(url)
                                    .then(function (response) {
                                        window.open(url, "_parent");
                                    }, function (response) {
                                        userMsg.popupGeneralError('File not found.');
                                    });
                                },
                                remove: function () {
                                    if ($scope.ngcontainermodel.removeUploadedFile(e)) {
                                        $scope.uploader.removeUploadedFile(this);
                                    }
                                }
                            };
                            $scope.uploader.queue.push(item);
                        });
                        //}
                    };

                    $scope.$watch("ngcontainermodel.documentList", function () {
                        resetQueue();
                    });

                    $scope.$watch("ngcontainermodel.url", function () {
                        $scope.reset();
                        $scope.uploader.url = $scope.ngcontainermodel.url;
                        resetQueue();
                    });

                    $scope.uploader.onSuccessItem = function (item, response, status, headers) {
                        userMsg.popup("File uploaded successfully");
                        $scope.ngcontainermodel.onDocumentUpload(response);
                        resetQueue();
                        $('#fileInput').val('');
                    };

                    $scope.uploader.onErrorItem = function (item, response, status, headers) {

                        for (var i = 0; i < response.validationResult.errors.length; i++) {
                            var property = response.validationResult.errors[i].propertyName;
                            var message = response.validationResult.errors[i].errorMessage != undefined ? response.validationResult.errors[i].errorMessage : "";
                            userMsg.popupGeneralError(property + ": " + message);
                        }

                        userMsg.popupGeneralError("Failed to upload the file");

                        $scope.reset();
                        $scope.uploader.removeFromQueue(item);
                    }

                    $scope.reset = function () {
                        $scope.uploader.progress = 0;
                        $('.progress-bar').css('width', 0);
                    }
                }]
            };
        });
})();

