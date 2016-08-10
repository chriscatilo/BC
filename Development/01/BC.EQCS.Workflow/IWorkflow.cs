using System;

namespace BC.EQCS.Workflow
{
    public interface IWorkflow<in TWorkflowModel> : IWorkflow
    {
        void Execute(int id, TWorkflowModel workflowModel);
    }

    public interface IWorkflow
    {
        Type ForModel { get; }
    }
}