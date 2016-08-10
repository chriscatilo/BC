using System.Collections.Generic;

namespace BC.EQCS.Domain
{
    public interface ICommandAvailabilityManager<TCommand>
    {
        IEnumerable<TCommand> GetByModelId(int id);

        IEnumerable<TCommand> GetForNewModel();

        bool IsAvailable(int id, TCommand command);
    }
}