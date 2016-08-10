'use strict';
(function () {
    angular.module("eqcs.core.service")
        .factory("customValidationBuilder", function () {
            return {
                
                createContainer: function() {
                    return new Container();
                },

                createValidator: function($scope, name, getForm, doEnable) {
                    return new CustomValidator($scope, name, getForm, doEnable);
                },
                
                validators: {
                    ///
                    /// Ensure that one value exists 
                    ///
                    /// name: name used in client validation
                    /// form: the form to attach the validation error
                    /// modelPaths: paths within a model used to watch values
                    oneValueExists: function($scope, name, form, modelPaths) {
                        var watch = $scope.$watchGroup(modelPaths, function(values) {
                            var invalid = _.reduce(values, function(aggregate, current) {
                                return aggregate && !current;
                            }, true);

                            form.$setValidity(name, !invalid);
                        });

                        return watch;
                    }
                }
            };
        });
    
    
    /// 
    /// Container for customer validators built
    ///
    var Container = function () { };
    Container.prototype =
    {
        enableAll: function () {
            doAll(this, 'enable');
            return this;
        },

        disableAll: function () {
            doAll(this, 'disable');
            return this;
        },

        resetAll: function () {
            doAll(this, 'reset');
            return this;
        },
    };

    var doAll = function (container, action) {
        _.map(container, function (value) {
            if (value[action]) {
                value[action]();
            }
        });
    };
    
    ///
    /// Wrapper for custom validator
    ///
    var CustomValidator = function ($scope, name, getForm, doEnable) {
        this.watch = void 0;
        this.$scope = $scope;
        this.getForm = getForm;
        this.name = name;
        this.doEnable = doEnable;
    };
    CustomValidator.prototype = {
        
        enable: function () {
            var form = this.getForm();
            if (!this.watch && form) {
                this.watch = this.doEnable(this.$scope, this.name, form);
            }
            return this;
        },

        disable: function () {
            this.watch = void 0;
            return this;
        },

        reset: function () {
            var form = this.getForm();
            if (form) {
                form.$setValidity(this.name, true);
            }
            return this;
        }
    };
})()