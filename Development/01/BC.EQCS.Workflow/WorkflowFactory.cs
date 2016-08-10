using System;
using System.Collections.Generic;
using System.Linq;

namespace BC.EQCS.Workflow
{
    public class WorkflowFactory : IWorkflowFactory
    {
        private readonly IDictionary<Type,IWorkflow> _workflows;

        public WorkflowFactory(IEnumerable<IWorkflow> workflows)
        {
            _workflows = workflows.ToDictionary(workflow => workflow.ForModel);
        }

        public IWorkflow<TWorkflowModel> Create<TWorkflowModel>() where TWorkflowModel : class
        {
            return (IWorkflow<TWorkflowModel>)_workflows[typeof(TWorkflowModel)];
        }
    }
}