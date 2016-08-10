'use strict';
(function() {

    var validateNoOfCandidates = function ($scope) {

        var args = {
            field: $scope.viewForm.details.noOfCandidates,
            value: $scope.vm.writeModel.noOfCandidates
        };

        args.field.$setValidity('greaterThan99999999', true);
        args.field.$setValidity('lessThanOne', true);
        args.field.$setValidity('notAnInteger', true);

        var isEmpty = angular.isUndefined(args.value) || _.isNull(args.value) || (String(args.value) || '').trim() === '';

        // is number of candidates an integer value
        var intValue = parseInt(args.value, 10);
        var isNotAnInteger = !isEmpty && (Number(args.value) !== intValue);
        if (isNotAnInteger) {
            args.field.$setValidity('notAnInteger', false);
            return false;
        }

        // is number of candidates not less than 1
        var lessThanOne = !isEmpty && args.value < 1;
        if (lessThanOne) {
            args.field.$setValidity('lessThanOne', false);
            return false;
        }

        // is number of candidates greater than 99999999
        var greaterThan99999999 = !isEmpty && args.value > 99999999;
        if (greaterThan99999999) {
            args.field.$setValidity('greaterThan99999999', false);
            return false;
        }

        return true; // is valid
    };

    var watchDelegates = [];

    var startValidator = function (args) {
        var watchDelegate = args.$scope.$watch(args.watchExp, function () {
            args.validator(args.$scope);
        });
        watchDelegates.push(watchDelegate);
    }

    var destroyWatches = function() {
        _.each(watchDelegates, function(destroyDelegate) {
            destroyDelegate();
        });
        watchDelegates = [];
    }

    var startValidators = function ($scope) {

        startValidator({
            $scope: $scope,
            watchExp: 'vm.writeModel.noOfCandidates',
            validator: validateNoOfCandidates
        });

    };

    angular.module('eqcs.incident')
        .factory('incidentCustomValidation', ['$q', '$timeout', function($q, $timeout) {
            return {
                start: function ($scope) {

                    var deferred = $q.defer();
                    
                    // custom validation should only be called once, unless stopped
                    if (watchDelegates.length > 0) {
                        deferred.resolve();
                        return deferred.promise;
                    }

                    startValidators($scope);

                    // wait for validation flags of the $scope.viewForm to be resolved
                    $timeout(function () {
                        deferred.resolve();
                    });

                    return deferred.promise;
                },

                stop: function() {
                    destroyWatches();
                }
            }
        }]);
})();