'use strict';
(function () {

    var statuses = [
        { number: 0, name: 'none', displayName: 'Unsaved' },
        { number: 1, name: 'draft', displayName: 'Draft' },
        { number: 2, name: 'raised', displayName: 'Raised' },
        { number: 3, name: 'inprogress', displayName: 'In Progress' },
        { number: 4, name: 'rejected', displayName: 'Rejected' },
        { number: 5, name: 'closed', displayName: 'Closed' }
    ];

    angular.module('eqcs.incident.constant')
        .constant("appConfig",
        {
            menu: [
                { route: '/incident/home', label: 'Home', name: 'home' },
                { route: '/incident/', label: 'New', name: 'new' },
                { route: '/incident/<tabParam>', label: 'Edit', name: 'edit' },
                { route: '/incident/<tabParam>', label: 'View', name: 'view' }
              
            ],
            incidentTabs: [
                { route: '/incident', label: 'Incident', name: 'incident' },
                { route: '/incident/actions', label: 'Actions', name: 'actions' },
                { route: '/incident/activity', label: 'Activity', name: 'activity' }
            ],
            visibleIncidentTabs: ['incident'],
            menusVisible : ['home', 'new'],
            status: {
                byNumber: _.reduce(statuses, function(agg, current) {
                    agg[current.number] = current;
                    return agg;
                }, {}),
                byName: _.reduce(statuses, function (agg, current) {
                    agg[current.name] = current;
                    return agg;
                }, {})
            },
                
                commandButtons: {
                    save: {
                        label: 'Save',
                        cssClass: "btn btn-primary btn-lg",
                        updating: true
                    },
                    delete: {
                        label: 'Delete',
                        cssClass: "btn btn-warning btn-lg",
                        nonUpdating: true
                    },
                    raise: {
                        label: 'Raise',
                        cssClass: "btn btn-primary btn-lg",
                        updating: true, nonUpdating: true
                    },
                    accept: {
                        label: 'Accept',
                        cssClass: "btn btn-primary btn-lg",
                        updating: true, nonUpdating: true
                    },
                    reject: {
                        label: 'Reject',
                        cssClass: "btn btn-warning btn-lg",
                        nonUpdating: true
                    },
                    close: {
                        label: 'Close Incident',
                        cssClass: "btn btn-primary btn-lg",
                        updating: true, nonUpdating: true
                    },
                    reopen: {
                        label: 'Reopen',
                        cssClass: "btn btn-primary btn-lg",
                        nonUpdating: true
                    },
                    addnote: {
                        label: 'Add Work Note',
                        cssClass: "btn btn-primary btn-lg",
                        nonUpdating: true
                    },
                    addaction: {
                        label: 'Add Action',
                        cssClass: "btn btn-primary btn-lg",
                        nonUpdating: true
                    },
                    cancel: {
                        label: 'Cancel',
                        cssClass: "btn btn-warning btn-cancel btn-lg"
                    }
                },

                workflowModes: {
                    rejection: {
                        name: 'rejection',
                        commandName: 'reject',
                        containerCssClass: 'rejection',
                        formTemplate: '/app/incident/templates/rejection.html'
                    },
                    reopening: {
                        name: 'reopening',
                        commandName: 'reopen',
                        containerCssClass: 'reopening',
                        formTemplate: '/app/incident/templates/reopening.html'
                    },
                    closure: {
                        name: 'closure',
                        commandName: 'close',
                        containerCssClass: 'closure',
                        formTemplate: '/app/incident/templates/closure.html'
                    }
                }
            });
})()