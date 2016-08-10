'use strict';
(function () {
    // TODO Chris: consider moving code away from angular and into a window object
    angular.module("eqcs.core.service")
        .factory('graphExtensions', function() {

            var findByCode = function(code) {
                if (this.code.toUpperCase() === code.toUpperCase()) {
                    return this;
                }

                var value;
                this.children.some(function(child) {
                    value = child.findByCode(code);
                    return !!value;
                });
                return value;
            }

            var getByType = function(type, list) {
                list = list || [];

                if (this.type.toUpperCase() === type.toUpperCase()) {
                    list.push(this);
                    return list;
                }

                this.children.forEach(function(child) {
                    child.getByType(type, list);
                });
                return list;
            }

            // traverse the graph tree and add methods to find and get nodes
            var extend = function(node, parent) {
                if (!node.findByCode) {
                    node.findByCode = findByCode;
                    node.getByType = getByType;
                    node.parent = parent;
                    node.children.forEach(function(child) {
                        extend(child, node);
                    });
                }
            };

            return {
                extend : extend
            };

        });
})()