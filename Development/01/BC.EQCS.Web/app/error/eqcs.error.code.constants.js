var errorCodeConstants = {
    RaceConditionConflict : 'Unfortunately this Incident has been updated by another user, therefore your changes can no longer be applied. Please cancel these changes and re-open the record. If you wish to keep a copy of your data, you can utilities copy and paste functionality to save re-typing on the updated record.',
};

var EQCS_Error_Parser = function(code, source){
    switch(code){
        case 'RaceConditionConflict':
            return errorCodeConstants.RaceConditionConflict.replace('Incident', source);
    }
};