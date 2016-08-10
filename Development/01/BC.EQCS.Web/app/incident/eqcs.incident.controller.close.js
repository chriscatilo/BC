"use strict";
(function() {
    angular.module("eqcs.incident")
        .controller("IncidentCloseCtrl", [
            "$q", "$scope",
            function ($q, $scope) {

                var vm = $scope.vm;

                var incidentClassCode = vm.writeModel.subCategory || vm.writeModel.category;

                var incidentClass = $scope.resources.class.findByCode(incidentClassCode);

                vm.userSelectedResidualRiskRating = incidentClass.residualRiskRatingDefault;
            }
        ]);
})();