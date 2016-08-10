'use strict';
(function () {

    var otherOption = { code: "OTHER", name: "Other (please specify)" };

    angular.module('eqcs.incident.directive')
        .controller('IncidentReferringOrgCtrl', [
            '$scope', function($scope) {

                var vm = $scope.vm;

                vm.refOrgModel = {};

                var updateDirectiveConfig = function () {
                    var isOrgRequired = vm.schema.referringOrgType.required;
                    vm.schema.referringOrgName.required = isOrgRequired && (vm.refOrgModel.referringOrganisation === "OTHER");
                };

                $scope.$watch("vm.schema", function (schema) {
                    if (!schema) return;

                    // copy schema of referringOrganisation to referringOrgName and update labeling
                    vm.schema.referringOrgName = _.extend({}, schema.referringOrganisation);
                    vm.schema.referringOrgName.label = "Name";

                    updateDirectiveConfig();
                });

                // everytime orgnisation types are fetched, add OTHER to each type's organisations
                $scope.$watch("vm.resources.orgTypes", function(values) {
                    if (!values) return;
                    
                    _.each(values, function(orgType) {
                        if (!_.findWhere(orgType.organisations, { code: "OTHER" })) {
                            orgType.organisations.push(otherOption);
                        }
                    });
                });


                // watch selection of org type and type's organisations and update the refOrg model
                $scope.$watchGroup(["vm.referringOrgType","vm.referringOrganisation"], function (values, oldValues) {

                    var referringOrgType = values[0], referringOrganisation = values[1];

                    if (!referringOrgType && !referringOrganisation && !oldValues[0] && !oldValues[1]) {
                        return;
                    }

                    if (!referringOrgType && referringOrganisation) {
                        vm.referringOrganisation = void 0;
                        return;
                    }

                    vm.refOrgModel.referringOrgType = referringOrgType ? referringOrgType.code : null;
                    vm.refOrgModel.referringOrganisation =
                        !referringOrganisation
                        ? null
                        : !_.findWhere(referringOrgType.organisations, referringOrganisation)
                        ? null
                        : referringOrganisation.code;

                    // organisation name is always null as organisation selected already has a name
                    if (!referringOrganisation || referringOrganisation.code !== "OTHER") {
                        vm.refOrgModel.referringOrgName = null;
                    }

                    updateDirectiveConfig();
                });

                // the refOrg model is holding model for what the user enters on the screen and what is persisted thru writeModel
                $scope.$watchGroup(["vm.refOrgModel.referringOrgType", "vm.refOrgModel.referringOrganisation", "vm.refOrgModel.referringOrgName"],
                    function(newValues) {

                        var referringOrgType = newValues[0], referringOrganisation = newValues[1], referringOrgName = newValues[2];

                        if (!referringOrgType && (referringOrganisation || referringOrgName)) {
                            vm.refOrgModel.referringOrganisation = void 0;
                            vm.refOrgModel.referringOrgName = void 0;
                            return;
                        }

                        if (!referringOrgType) {
                            vm.referringOrgType = null;
                        } else {
                            var refOrgs = _.where(vm.resources.orgTypes, { code: referringOrgType });

                            vm.referringOrgType = refOrgs.length > 0 ? refOrgs[0] : null;

                            // when org type is Non-RO then organisation is always Other
                            if (referringOrgType === "NONRO") {
                                referringOrganisation = "OTHER";
                            }
                        }

                        if (!referringOrganisation) {
                            vm.referringOrganisation = null;
                        } else {

                            var orgs =
                                !vm.referringOrgType
                                    ? []
                                    : _.where(vm.referringOrgType.organisations, { code: referringOrganisation });

                            vm.referringOrganisation = orgs.length > 0 ? orgs[0] : null;
                        }

                        vm.writeModel.referringOrgType = referringOrgType;

                        vm.writeModel.referringOrgExists = 
                            !referringOrganisation && !referringOrgName 
                            ? null 
                            : referringOrganisation === "OTHER" && !referringOrgName
                            ? null
                            : referringOrganisation !== "OTHER";

                        vm.writeModel.referringOrganisation = vm.writeModel.referringOrgExists ? referringOrganisation : referringOrgName;
                    });

                // watch the values loaded as persisted 
                $scope.$watchGroup(["vm.writeModel.referringOrgType", "vm.writeModel.referringOrganisation", "vm.writeModel.referringOrgExists"],
                    function(newValues) {

                        var referringOrgType = newValues[0], referringOrganisation = newValues[1], referringOrgExists = newValues[2];

                        vm.refOrgModel.referringOrgType = referringOrgType;

                        vm.refOrgModel.referringOrganisation =
                            referringOrgExists
                            ? referringOrganisation
                            : referringOrgExists === false
                            ? "OTHER"
                            : referringOrgExists === null && vm.referringOrganisation && vm.referringOrganisation.code === "OTHER"
                            ? "OTHER"
                            : null;

                        vm.refOrgModel.referringOrgName = !referringOrgExists ? referringOrganisation : null;
                    });
            }
        ]);
})();