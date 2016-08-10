namespace BC.EQCS.Workflow
{
    public interface IWorkflowActivityLogger<out TStatus, in TWorkflowModel>
    {
        TStatus ForIncidentStatus { get; }
        void Log(int modelId, TWorkflowModel workflowModel);
    }
}