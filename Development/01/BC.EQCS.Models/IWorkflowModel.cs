namespace BC.EQCS.Models
{
    public interface IWorkflowModel<out TStatus>
    {
        TStatus Status { get; }
    }
}