"use strict";
(function() {
    var
        oneOrMoreFieldsAreRequired = "One or more fields are required",
        thisFieldIsRequired = "This field is required";

    angular.module("eqcs.incident.constant")
        .constant("formConfig", {
            incident: {
                default: {
                    label: "Incident",

                    // details section
                    details: {
                        label: "Details",

                        errors: [{ key: "required", value: oneOrMoreFieldsAreRequired }],

                        noOfCandidates: {
                            errors: [
                                { key: "greaterThan99999999", value: "This should not be greater than 99999999" },
                                { key: "lessThanOne", value: "This should be no less than one" },
                                { key: "notAnInteger", value: "Invalid value." }
                            ]
                        }
                    },

                    // candidates section
                    candidates: {
                        label: "Candidates Involved",

                        addCandidateButton: "Add Candidate",

                        sectionCopy: "List of Candidates who where involved in the Incident",

                        notice: "Please Note: Address and UKVI Reference Number are mandatory if this incident is going to be reported to UKVI"
                    },

                    // recognising organisation section
                    referringOrg: {
                        label: "Recognising Organisation",

                        errors: [{ key: "required", value: oneOrMoreFieldsAreRequired }]
                    },

                    // UKVI section
                    ukvi: {
                        label: "UKVI",

                        errors: [{ key: "required", value: oneOrMoreFieldsAreRequired }],

                        reportToUKVI:
                        {
                            yesLabel: "Yes",
                            noLabel: "No",
                            yesValue: true,
                            noValue: false
                        }
                    }
                } // end of default
            }, // end of incident
            workflow: {
                rejection: {
                    default: {
                        label: "Incident Rejection",
                        reason: {
                            label: "Enter the reason for rejecting the incident",
                            errors: [{ key: "required", value: thisFieldIsRequired }]
                        }
                    },
                    validation: {
                        reason: {
                            required: true,
                            errors: [{ key: "required", value: thisFieldIsRequired }]
                        }
                    }
                },
                reopening: {
                    default: {
                        label: "Incident Reopening",
                        reason: {
                            label: "Enter the reason for reopening the incident"
                        }
                    },
                    validation: {
                        reason: {
                            required: true,
                            errors: [{ key: "required", value: thisFieldIsRequired }]
                        }
                    }
                },
                closure: {
                    default: {
                        label: "Incident Closure",
                        comments: {
                            label: "Closure Details"
                        },
                        residualRisk: {
                            label: "Residual Risk"
                        }
                    },
                    validation: {
                        errors: [{ key: "required", value: oneOrMoreFieldsAreRequired }],
                        comments: {
                            required: true
                        },
                        residualRisk: {
                            required: true
                        }
                    }
                }
            }
        });
})();