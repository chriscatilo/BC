'use strict';
(function() {
    angular.module('eqcs.incident.directive')
        .controller('TestCentreLocationCtrl', [
            '$scope', function ($scope) {

                var vm = $scope.vm;
                
                $scope.$watch("vm.testCentre", function (value) {
                    vm.writeModel.testCentre = value ? value.code : void 0;
                    vm.writeModel.testLocation = void 0;
                });

                $scope.$watch("vm.writeModel.testCentre", function (value) {
                    if (!value) {
                        vm.testCentre = void 0;
                    }
                    else {
                        var values = _.where(vm.resources.testCentres, { code: value });

                        vm.testCentre = values.length > 0 ? values[0] : void 0;
                    }
                });

                $scope.$watch('vm.testLocation', function (value) {
                    vm.writeModel.testLocation = value ? value.code : void 0;
                });

                $scope.$watch('vm.writeModel.testLocation', function (value) {
                    if (!value) {
                        vm.testLocation = void 0;
                    }
                    else {
                        var values =
                            !vm.testCentre
                            ? []
                            : _.where(vm.testCentre.children, { code: value });

                        vm.testLocation = values.length > 0 ? values[0] : void 0;
                    }
                });
            }
        ]);
})();