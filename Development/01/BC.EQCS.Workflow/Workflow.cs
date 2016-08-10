using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;

namespace BC.EQCS.Workflow
{
    public abstract class Workflow<TEntity, TWorkflowModel, TStatus> : IWorkflow<TWorkflowModel>
        where TEntity : class
        where TWorkflowModel : IWorkflowModel<TStatus>
    {
        private readonly ILookup<TStatus, TStatus> _availableTransitions;
        private readonly IEntityFactory _entityFactory;
        private readonly IDictionary<TStatus, IWorkflowActivityLogger<TStatus, TWorkflowModel>> _workflowActivityLoggers;

        protected Workflow(
            IEntityFactory entityFactory,
            ILookup<TStatus, TStatus> availableTransitions,
            IEnumerable<IWorkflowActivityLogger<TStatus, TWorkflowModel>> workflowActivityLoggers)
        {
            _entityFactory = entityFactory;
            _availableTransitions = availableTransitions;
            _workflowActivityLoggers = workflowActivityLoggers.ToDictionary(logger => logger.ForIncidentStatus);
        }

        protected EqcsEntities Context
        {
            get { return _entityFactory.Create(); }
        }

        protected Func<TEntity, TStatus> GetStatus { get; set; }
        protected Action<TEntity, TStatus> SetStatus { get; set; }

        public void Execute(int id, TWorkflowModel workflowModel)
        {
            var entity = Context.Set<TEntity>().Find(id);

            if (entity == null)
            {
                throw new ModelNotFoundException();
            }

            var fromStatus = GetStatus(entity);

            if (!_availableTransitions.Contains(fromStatus))
            {
                throw new TransitionNotAllowed("Cannot transition from {0}", fromStatus);
            }

            if (_availableTransitions[fromStatus].All(status => status != (dynamic) workflowModel.Status))
            {
                throw new TransitionNotAllowed("Cannot transition from {0} to {1}", fromStatus, workflowModel.Status);
            }

            OnExecution(id, workflowModel);

            var toStatus = _availableTransitions[fromStatus].First(status => status == (dynamic) workflowModel.Status);

            SetStatus(entity, toStatus);

            if (_workflowActivityLoggers.ContainsKey(workflowModel.Status))
            {
                var activityLogger = _workflowActivityLoggers[workflowModel.Status];

                activityLogger.Log(id, workflowModel);
            }

            Context.SaveChanges();
        }

        public Type ForModel
        {
            get { return typeof (TWorkflowModel); }
        }

        protected abstract void OnExecution(int id, TWorkflowModel workflowModel);
    }
}