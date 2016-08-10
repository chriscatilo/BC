namespace BC.EQCS.Workflow
{
    public interface IWorkflowFactory
    {
        IWorkflow<TWorkflowModel> Create<TWorkflowModel>() where TWorkflowModel : class;
    }
}