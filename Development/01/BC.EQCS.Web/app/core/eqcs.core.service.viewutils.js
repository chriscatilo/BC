'use strict';
(function() {

    angular.module('eqcs.core.service')
        .service("viewUtils", ['$timeout', function($timeout) {

            this.illuminateValidationErrors = function () {

                $timeout(function () {

                    var first = $("ul.validation-error li:first-child");

                    var flash = function() {
                        $("ul.validation-error").fadeIn(200).fadeOut(200).fadeIn(200);
                    };

                    // uses a jquery plugin called "visible" 
                    // https://github.com/customd/jquery-visible
                    if (first.length !== 0 && !first.visible()) {
                        var top = first.offset().top - 150;
                        $('html, body').animate({ scrollTop: top }, 1000, null, flash);
                    } else {
                        flash();
                    }
                });


            };

            this.deepCopy = function extendDeep(dst) {
                angular.forEach(arguments, function (obj) {
                    if (obj !== dst) {
                        angular.forEach(obj, function (value, key) {
                            if (dst[key] && dst[key].constructor && dst[key].constructor === Object) {
                                extendDeep(dst[key], value);
                            } else {
                                dst[key] = value;
                            }
                        });
                    }
                });
                return dst;
            };


        }]);

})()