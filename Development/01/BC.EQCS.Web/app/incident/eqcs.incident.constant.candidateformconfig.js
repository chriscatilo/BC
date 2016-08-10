'use strict';
(function() {
    var
        oneOrMoreFieldsAreRequired = 'One or more fields are required';

    angular.module('eqcs.incident.constant')
        .constant("candidateFormConfig", {
            label: "Involved Candidate",

            errors: [{ key: 'required', value: oneOrMoreFieldsAreRequired }],

            gender: {
                label: 'Gender',
                yesLabel: "Female",
                noLabel: "Male",
                yesValue: "Female",
                noValue: "Male"
            }
        });
})();