namespace BC.EQCS.ActivityLog.Logger
{
    //[Serializable]
    //public class ActivityLoggerAspect : OnMethodBoundaryAspect, IInstanceScopedAspect
    //{
    //    //private IRepository<IncidentModel> _incidentRepository;
    //    //private IRepository<IncidentActivityLogModel> _activityLogRepository;
    //    //private ILogEntryExtrapolator _logEntryExtrapolator;
    //    //private int _logType;


    //    //public override void RuntimeInitialize(MethodBase method)
    //    //{
    //    //    _incidentRepository = DependanciesForAspect.GetIncidentRepository();
    //    //    _activityLogRepository = DependanciesForAspect.GetIncidentActivityLogRepository();
    //    //    _logEntryExtrapolator = new IncidentLogEntryExtrapolator();

    //    //}
    //    ///// <summary>
    //    ///// 
    //    ///// </summary>
    //    ///// <param name="logType">If logtype is 0 or not present then the logger will generate a 'diff' log, if it is not 0 then the logger will generate a merged 'all value' log</param>
    //    //public ActivityLoggerAspect(int logType = 0)
    //    //{
    //    //    _logType = logType;
    //    //}

    //    //public object CreateInstance(AdviceArgs adviceArgs)
    //    //{
    //    //    return this.MemberwiseClone();
    //    //}

    //    //public void RuntimeInitializeInstance()
    //    //{
    //    //}


    //    //public override void OnEntry(MethodExecutionArgs args)
    //    //{
    //    //    try
    //    //    {
    //    //        // - Get the current object from the args
    //    //        var argFirst = args.Arguments.FirstOrDefault();
    //    //        var argLast = args.Arguments.LastOrDefault() as IncidentModel;


    //    //        if (argFirst != null && argLast != null)
    //    //        {
    //    //            var idPassedIn = argFirst is Int32 ? (Int32) argFirst : argLast.Id;
    //    //            argLast.Id = idPassedIn;

    //    //            _logEntryExtrapolator.AddNewValuesModel(argLast);

    //    //            //Check if this is an update
    //    //            if (idPassedIn != 0)
    //    //            {
    //    //                //Get the original object model from the repository
    //    //                var prevIncidentModel = _incidentRepository.GetById(argLast.Id);

    //    //                //Pass it to the log creator
    //    //                _logEntryExtrapolator.AddPreviousValuesModel(prevIncidentModel);
    //    //            }
    //    //        }
    //    //    }
    //    //    catch (Exception)
    //    //    {
    //    //        //Squashing exceptions is done deliberately here, so the aspect doesn't have any knock on effects to any items it is attached to
    //    //    }


    //    //}

    //    //public override void OnSuccess(MethodExecutionArgs args)
    //    //{
    //    //    try
    //    //    {
    //    //        var expectedFailedResultReturnText = "ValidationFailedResult";

    //    //        //If the endpoint did not store the value then return
    //    //        if (args.ReturnValue.GetType().Name.Equals(expectedFailedResultReturnText))
    //    //            return;

    //    //        //Get the user
    //    //        var userModel = Mapper.Map<UserModel>(DependanciesForAspect.GetCurrentUser());

    //    //        // - Get the current object from the args
    //    //        var argFirst = args.Arguments.FirstOrDefault();
    //    //        var argLast = args.Arguments.LastOrDefault() as IncidentModel;


    //    //        if (argFirst != null && argLast != null)
    //    //        {
    //    //            var idPassedIn = argFirst is Int32 ? (Int32) argFirst : argLast.Id;
    //    //            argLast.Id = idPassedIn;

    //    //            if (idPassedIn != 0)
    //    //            {
    //    //                ICollection<IncidentActivityLogModel> logEntries = new List<IncidentActivityLogModel>();

    //    //                //Get the colleciton of log entries to go into the database
    //    //                if (_logType == 0)
    //    //                {
    //    //                     logEntries = _logEntryExtrapolator.GetChangedItemLogEntries(userModel);
    //    //                }
    //    //                else
    //    //                {
    //    //                    logEntries = _logEntryExtrapolator.GetAllFieldsWithValueMergedLogEntries(userModel);
    //    //                }

    //    //                //Call up the log repo and pass in the log entries
    //    //                foreach (var incidentActivityLogModel in logEntries)
    //    //                {
    //    //                    _activityLogRepository.Create(incidentActivityLogModel);
    //    //                }
    //    //            }
    //    //        }
    //    //    }
    //    //    catch (Exception)
    //    //    {
    //    //        //Squashing exceptions is done deliberately here, so the aspect doesn't have any knock on effects to any items it is attached to
    //    //    }
    //    //}

    //}
}

