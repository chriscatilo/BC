"use strict";
(function() {
    angular.module("eqcs.incident.directive")
        .controller("IncidentClassificationCtrl", [
            "$scope", "dataService", function($scope, data) {

                var vm = $scope.vm;

                var loadSchema = function(incidentClass) {

                    var url = vm.schemaUrl + "?class=" + incidentClass.code;
                    data.getSchema(url)
                        .then(function(payload) {
                            vm.resources.schemaAugments = payload.data;
                            vm.schema = {};
                            angular.copy(vm.resources.schemaAugments.default, vm.schema);

                            // remove value for each non-applicable property in the model
                            _.each(_.where(vm.schema, { notApplicable: true }), function(value) {
                                vm.writeModel[value.field] = void 0;
                            });

                        });
                };

                var isIntialModelLoad = false;

                // when incident is created or loaded then select the category and sub-category
                $scope.$watch("vm.writeModel", function(writeModel) {

                    if (writeModel.category || writeModel.subCategory) {

                        vm.category = writeModel.category
                            ? _.findWhere(vm.resources.categories, { code: writeModel.category })
                            : void 0;

                        vm.subCategory = !vm.category || !writeModel.subCategory
                            ? void 0
                            : _.findWhere(vm.category.children, { code: writeModel.subCategory });
                    }

                    isIntialModelLoad = !angular.isUndefined(writeModel.category);
                });

                // on category or sub category selection change...
                $scope.$watchGroup(["vm.category", "vm.subCategory"], function(values) {

                    var categorySelected = values[0], subCategorySelected = values[1];

                    var categoryCode = categorySelected ? categorySelected.code : void 0;

                    var categoryChanged = categoryCode !== vm.writeModel.category;
                    if (categoryChanged) {
                        vm.writeModel.category = categorySelected ? categorySelected.code : void 0;
                        vm.writeModel.subCategory = void 0;
                        vm.subCategory = void 0;
                    }

                    var subCategoryCode = subCategorySelected ? subCategorySelected.code : void 0;

                    var subCategoryChanged = subCategoryCode !== vm.writeModel.subCategory;
                    if (subCategoryChanged) {
                        vm.writeModel.subCategory = subCategorySelected ? subCategorySelected.code : void 0;
                    }

                    // For categories without sub category, then sub category = category
                    // So when category changes, make the sub category = category
                    // For categories with sub-categories, the user selects from a drop down
                    // and selection is reflected when instance is read only (closed or rejected)
                    vm.readModel = vm.readModel || {};
                    vm.readModel.subCategory = subCategorySelected
                        ? subCategorySelected.name
                        : categorySelected
                        ? categorySelected.name
                        : void 0;

                    var newIncidentClass = subCategorySelected || categorySelected;
                    if (!newIncidentClass) {
                        return;
                    }

                    loadSchema(newIncidentClass);

                    // if incident has just been loaded then do not default the it's risk rating
                    if (!isIntialModelLoad) {
                        if (!newIncidentClass.riskRatingDefault) {
                            vm.writeModel.riskRating = void 0;
                            vm.readModel.riskRating = void 0;
                            return;
                        }
                        var riskRating = _.findWhere(vm.resources.riskRatings, { code: newIncidentClass.riskRatingDefault });
                        vm.writeModel.riskRating = riskRating.code;
                        vm.readModel.riskRating = riskRating.name;
                    }
                    isIntialModelLoad = false;
                });
            }
        ]);
})();