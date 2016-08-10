using System.Collections.Generic;

namespace BC.EQCS.Domain.Incident
{
    public interface ICommandTransitionMaps<TCommand, TStatus> : IEnumerable<TransitionMap<TCommand, TStatus>>
        where TStatus : struct
        where TCommand : struct
    {
    }
}