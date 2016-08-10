/// <reference path="../../BC.EQCS.Web/Scripts/angular.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/angular-route.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/angular-mocks.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/angular-animate.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/jquery-2.1.3.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/toaster.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/kendo/kendo.all.min.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/underscore.min.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/wcAngularOverlay.js" />
/// <reference path="/Scripts/jasmine/jasmine.js" />

/// <reference path="../../BC.EQCS.Web/app/core/eqcs.core.module.js" />
/// <reference path="../../BC.EQCS.Web/app/incident/eqcs.incident.module.js" />
/// <reference path="../../BC.EQCS.Web/app/incident/eqcs.incident.constant.appconfig.js" />

// directive under test
/// <reference path="../../BC.EQCS.Web/app/core/eqcs.core.directive.availablecommands.js" />

describe('eqcs.core.directive.availablecommands =>', function () {

    describe("given incident model with all known commands", function() {

        var availableCommands =
        [
            'save',
            'raise',
            'delete',
            'accept',
            'reject',
            'close',
            'reopen',
            'addnote',
            'addaction'
        ];

        describe("when client presents available commands", function () {

            var $compile, $rootScope, updatingCommands, nonUpdatingCommands;

            beforeEach(module('eqcs.incident'));

            beforeEach(inject(function (_$compile_, _$rootScope_, _appConfig_) {
                $compile = _$compile_;
                $rootScope = _$rootScope_;

                $rootScope.commandButtons = _appConfig_.commandButtons;

                $rootScope.availableCommands = availableCommands;

                var commandButtons = _.map(_appConfig_.commandButtons, function(obj, key) {
                    return angular.extend({ name: key }, obj);
                });

                updatingCommands = _.pluck(_.where(commandButtons, { updating: true }), 'name');

                nonUpdatingCommands = _.pluck(_.where(commandButtons, { nonUpdating: true }), 'name');

            }));

            beforeEach(function() {
                $rootScope.commandActions =
                {
                    save: function() {},
                    raise: function() {},
                    accept: function() {},
                    reject: function() {},
                    close: function() {},
                    reopen: function() {},
                    delete: function() {},
                    addnote: function() {},
                    addaction: function() {},
                    cancel: function() {}
                };
            });

            var elementUnderTest;

            beforeEach(function() {
                var html =
                    "<div available-commands='availableCommands' " +
                        "view-form='form' " +
                        "command-actions='commandActions' " +
                        "command-buttons='commandButtons' " +
                        "enabled-counts='counts'>" +
                    "</div>";
                elementUnderTest = $compile(html)($rootScope);
            });

            var getCommandShown = function () {
                var scope = elementUnderTest.isolateScope();
                var commandsShown = scope.vm.commandsShown;
                var values = _.reduce(commandsShown, function (agg, current) {
                    if (!current.config.disabled) {
                        agg.push(current.name);
                    }
                    return agg;
                }, []);
                return values;
            };

            describe("and input form is pristine", function () {

                beforeEach(function () {

                    $rootScope.$digest();

                    $rootScope.form2 = {
                        $pristine: true,
                        $dirty: false
                    };

                    $rootScope.$digest();
                });

                it('then non-updating commands are available and counted', function () {

                    var actual = getCommandShown().sort();

                    expect(actual).toEqual(nonUpdatingCommands.sort());
                });

                it('and updating-only commands are not available', function() {
                    var updateOnlyCommands = _.difference(updatingCommands, nonUpdatingCommands);
                    
                    var actual = getCommandShown();

                    var numberOfUpdateOnlyCommands = _.intersection(actual, updateOnlyCommands).length;

                    expect(numberOfUpdateOnlyCommands).toEqual(0);
                });
                
                it('and cancel command is not available', function() {
                    
                    var actual = getCommandShown();

                    expect(actual).not.toContain('cancel');
                });

                it('and non-updating command counts is correct', function () {
                    
                    var expectedCommands = _.intersection($rootScope.availableCommands, nonUpdatingCommands);

                    expect($rootScope.counts.nonUpdating).toEqual(expectedCommands.length);
                });

                it('and updating command counts is correct', function () {

                    var availableNonUpdatingCommands = _.intersection($rootScope.availableCommands, nonUpdatingCommands);

                    var exepected = _.intersection(availableNonUpdatingCommands, updatingCommands);

                    expect($rootScope.counts.updating).toEqual(exepected.length);

                });
            });

            describe("and input form is dirty", function () {

                beforeEach(function () {

                    $rootScope.$digest();

                    $rootScope.form = {
                        $pristine: false,
                        $dirty: true
                    };

                    $rootScope.$digest();

                });

                it('then updating commands and cancel commands are available', function () {
                    var actual = getCommandShown().sort();
                    var expected = _.union(updatingCommands, ['cancel']).sort();
                    expect(actual).toEqual(expected);
                });

                it('and non-updating only commands not are available', function () {

                    var nonUpdateOnlyCommands = _.difference(nonUpdatingCommands,updatingCommands);

                    var actual = getCommandShown();

                    var numberOfNonUpdatingCommands = _.intersection(actual, nonUpdateOnlyCommands).length;

                    expect(numberOfNonUpdatingCommands).toEqual(0);
                });

                it('and cancel command is available', function () {

                    var actual = getCommandShown();

                    expect(actual).toContain('cancel');
                });

                it('and updating command counts is correct', function () {

                    var expectedCommands = _.intersection($rootScope.availableCommands, updatingCommands);

                    expect($rootScope.counts.updating).toEqual(expectedCommands.length);

                });
            });

        });
    });

    
});