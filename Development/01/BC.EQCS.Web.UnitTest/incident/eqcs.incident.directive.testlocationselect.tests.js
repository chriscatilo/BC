/// <reference path="../../BC.EQCS.Web/Scripts/angular.js" />
/// <reference path="/Scripts/jasmine/jasmine.js" />

// directive under test
/// <reference path="../../BC.EQCS.Web/app/ incident/eqcs.incident.directive.testlocationselect.js" />

'use strict';

/*
 * given my admin structure has more than one test centre
 *      
 *      when I select a test centre with more than one test location
 *          then  the model has the correct test centre
 *          
 *          when I select a test location
 *              then the model has the correct test location
 * 
 *      when I select a test centre with just one test location
 *          then the model has the correct test centre
 *          then the model has the correct test location 
 * 
 */

// TODO Chris. to be completed
xdescribe('eqcs.incident.directive.testlocationselect =>', function() {

    describe("given my admin structure has more than one test centre", function() {
        
        describe("when I select a test centre with more than one test location", function () {

            it("then  the model has the correct test centre", function () { });

            describe("when I select a test location", function () {

                it("then the model has the correct test location", function() {
                });

            });

        });

        describe("when I select a test centre with just one test location", function () {

            it("then the model has the correct test centre", function () {

            });

            it("then the model has the correct test location", function () {
            });
        });
    });
});
