
namespace BC.EQCS.Contracts
{
    public interface INotificationTemplateRepository<TModel> : IRepository<TModel>
    {
        TModel GetTemplateBasedOnEventId(int eventId);
        TModel GetTemplateBasedOnActionIdEventId(int eventId, int actionId);
    }
}