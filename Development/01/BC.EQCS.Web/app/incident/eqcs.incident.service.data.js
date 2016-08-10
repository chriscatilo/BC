'use strict';
(function () {
    angular.module("eqcs.incident.service")
        .factory("dataService", [
            'coreDataService', '$q',
            function (core, $q) {

                var container = {
                    getRiskRatings: function() {
                        return core.getCached('/api/incident/risk');
                    },

                    saveIncident: function(uri, model, isNew) {

                        if (isNew) {
                            return core.post(uri, model);
                        }
                        return core.put(uri, model);
                    },

                    raiseIncident: function(uri, model, isNew) {

                        if (isNew) {
                            return core.post(uri, model);
                        }
                        return core.put(uri, model);
                    },

                    acceptIncident: function(uri, model) {
                        return core.put(uri, model);
                    },

                    rejectIncident: function(uri, rejectionModel) {
                        return core.put(uri, rejectionModel);
                    },

                    closeIncident: function(uri, incidentModel, closureModel) {
                        return core.put(uri, { incidentModel: incidentModel, workflowModel: closureModel });
                    },

                    deleteIncident: function(uri) {
                        return core.delete(uri);
                    },

                    reopenIncident: function(uri, reopeningModel) {
                        return core.put(uri, reopeningModel);
                    },

                    getUser: function() {
                        return core.get('/api/user');
                    },

                    saveWorkNote: function(model) {
                        return core.post('/api/WorkNote', model);
                    },

                    sendFYIMessage: function (model) {
                        
                        return core.post('/api/notification/SendFyiNotification', model);
                    },

                    raiseIncidentNotifications: function (incidentId) {
                        return core.post('/api/notification/' + incidentId + '/SendRaisedIncidentNotification');
                    },
                    
                    acceptIncidentNotifications: function (incidentId) {
                        return core.post('/api/notification/' + incidentId + '/SendAcceptedIncidentNotification');
                    },

                    rejectIncidentNotifications: function(incidentId) {
                        return core.post('/api/notification/' + incidentId + '/SendRejectIncidentNotification');
                    },

                    closeIncidentNotifications: function (incidentId) {
                        return core.post('/api/notification/' + incidentId + '/SendCloseIncidentNotification');
                    },

                    createActionNotifications: function (incidentId, actionId) {
                        return core.post('/api/notification/' + incidentId + '/SendNewActionNotification/' + actionId);
                    },
                    
                    editActionNotifications: function (incidentId, actionId) {
                        return core.post('/api/notification/' + incidentId + '/SendEditActionNotification/' + actionId);
                    },

                    closeActionNotifications: function (incidentId, actionId) {
                        return core.post('/api/notification/' + incidentId + '/SendCloseActionNotification/' + actionId);
                    },
                    
                    sendWorkNoteNotification: function(incidentId) {
                        return core.post('/api/notification/'+incidentId+'/SendWorkNoteNotification');
                    },

                    createAction: function(model) {
                        return core.post('/api/IncidentAction', model);
                    },
                    
                    saveAction: function (model) {
                        return core.put('/api/IncidentAction/' + model.id, model);
                    },

                    getSchema: function (url) {

                        var deferred = $q.defer();

                        var onSuccess = function (payload) {

                            var schemaAugments = _.reduce(payload.data, function (augments, augment) {

                                var schema = _.reduce(augment.fields, function (agg, fieldSchema) {
                                    agg[fieldSchema.field] = fieldSchema;
                                    return agg;
                                }, {});

                                augments[augment.name] = schema;
                                return augments;
                            }, {});

                            deferred.resolve({ data: schemaAugments });
                        }

                        core.get(url)
                            .then(onSuccess, function (payload) {
                                deferred.reject(payload);
                            });

                        return deferred.promise;
                    },

                    removeOrphenFiles: function(orphenDocuments) {
                        angular.forEach(orphenDocuments, function (e) {
                            core.delete('/api/deletedocument/' + e.id);
                        });
                    },

                    removeUploadedFile: function(fileId) {
                        return core.delete('/api/deletedocument/' + fileId);
                    },

                    documentsForAction: function(actionId) {
                        return core.get('/api/documentsByActionId/' + actionId);
                    }
                };

                angular.extend(container, core);

                return container;

            }]);
})()