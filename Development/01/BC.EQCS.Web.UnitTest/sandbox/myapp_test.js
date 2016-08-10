/// <reference path="/Scripts/jasmine/jasmine.js" />
/// <reference path="myapp.js" />

describe("myapp tests =>", function() {

    it("isDebug", function() {
        expect(app.isDebug).toEqual(true);
    });

    it("log", function() {
        expect(app.log).toBeDefined();
    });
})