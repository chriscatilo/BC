'use strict';

(function() {
    angular.module("eqcs.incident")
        .factory("ukviService", function () {
            
            return {
                create: function ($scope) {

                    var vm = $scope.vm;

                    /* show UKVI immediate report when 
                        * category is Other or when category\subcategory has no default report type
                        * report to UKVI is Yes 
                        * product is UKVI
                    */

                    $scope.$watch("vm.readModel",
                        function (value) {

                            if (!value) return;

                            vm.showUkviImmediateReport =
                                value.reportUkvi && 
                                value.isUkvi;
                        });

                    $scope.$watchGroup([
                        "vm.writeModel.category", 
                        "vm.writeModel.reportUkvi", 
                        "vm.product"],
                        function(values) {

                            var category = values[0],
                                reportUkvi = values[1],
                                product = values[2];

                            if (!category && !reportUkvi && !product) return;

                            vm.showUkviImmediateReport = vm.category &&
                                reportUkvi &&
                                product &&
                                product.isUkvi;

                        });


                    // this is to ensure that ukviImmediateReportType is validated at the appropriate time
                    $scope.$watchGroup(["vm.schema.category.required", "vm.writeModel.reportUkvi", "vm.schema.ukviImmediateReportType.required"],
                        function (values) {
                            if (values[0] === undefined) return; 
                            vm.schema.ukviImmediateReportType.required = values[0] && vm.showUkviImmediateReport;
                        });

                    // when category \ sub category is selected, set the ukvi immediate report to default for view only
                    $scope.$watchGroup(["vm.category", "vm.subCategory", "vm.writeModel.reportUkvi"], function (values) {

                        var category = values[0],
                            subCategory = values[1];

                        if (!category && !subCategory) return;

                        var ukviImmediateReportType = (subCategory && subCategory.ukviImmediateReportType) || (!subCategory && category.ukviImmediateReportType);
                        var reportType = _.findWhere(vm.resources.ukviImmediateReportTypes, { code: ukviImmediateReportType });

                        vm.readModel = vm.readModel || {};
                        vm.readModel.ukviImmediateReportType = !reportType ? void 0 : reportType.name;

                        vm.ukviImmediateReportTypeReadonly = reportType !== void 0;
                    });
                }
            }
        });
})()