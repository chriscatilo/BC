using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.Domain.Incident
{
    // TODO Chris: to many interfaces being implemented
    public class IncidentAvailableTransitions :
        ICommandTransitionMaps<IncidentCommand, IncidentStatus>,
        ILookup<IncidentStatus, IncidentStatus>,
        ILookup<IncidentStatus, IncidentCommand>
    {
        private static readonly IList<Transition> _maps = new[]
        {
            // commands with pre and post status transistions
            new Transition(IncidentStatus.None, IncidentCommand.Save, IncidentStatus.Draft),
            new Transition(IncidentStatus.None, IncidentCommand.Raise, IncidentStatus.Submitted),
            new Transition(IncidentStatus.Draft, IncidentCommand.Save, IncidentStatus.Draft),
            new Transition(IncidentStatus.Draft, IncidentCommand.Raise, IncidentStatus.Submitted),
            new Transition(IncidentStatus.Submitted, IncidentCommand.Save, IncidentStatus.Submitted),
            new Transition(IncidentStatus.Submitted, IncidentCommand.Accept, IncidentStatus.InProgress),
            new Transition(IncidentStatus.Submitted, IncidentCommand.Reject, IncidentStatus.Rejected),
            new Transition(IncidentStatus.InProgress, IncidentCommand.Save, IncidentStatus.InProgress),
            new Transition(IncidentStatus.InProgress, IncidentCommand.Close, IncidentStatus.Closed),
            new Transition(IncidentStatus.Closed, IncidentCommand.ReOpen, IncidentStatus.InProgress),

            // commands without pre to post status transitions
            new Transition(IncidentStatus.Draft, IncidentCommand.Delete),
            new Transition(IncidentStatus.Draft, IncidentCommand.AddCandidate),
            new Transition(IncidentStatus.Submitted, IncidentCommand.AddCandidate),
            new Transition(IncidentStatus.InProgress, IncidentCommand.AddCandidate)
        };

        public IEnumerator<TransitionMap<IncidentCommand, IncidentStatus>> GetEnumerator()
        {
            return _maps.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<IGrouping<IncidentStatus, IncidentCommand>> IEnumerable<IGrouping<IncidentStatus, IncidentCommand>>.
            GetEnumerator()
        {
            return _maps.GroupBy(t => t.PreStatus, arg => arg.Command).GetEnumerator();
        }

        IEnumerable<IncidentCommand> ILookup<IncidentStatus, IncidentCommand>.this[IncidentStatus key]
        {
            get { return _maps.Where(i => i.PreStatus == key).Select(i => i.Command); }
        }

        IEnumerator<IGrouping<IncidentStatus, IncidentStatus>> IEnumerable<IGrouping<IncidentStatus, IncidentStatus>>.
            GetEnumerator()
        {
            var values = _maps
                .Where(i => i.PostStatus != null)
                .GroupBy(t => t.PreStatus, arg => arg.PostStatus ?? IncidentStatus.None)
                .GetEnumerator();

            return values;
        }

        public bool Contains(IncidentStatus key)
        {
            return _maps.Any(i => i.PreStatus == key);
        }

        public int Count
        {
            get { return _maps.Count; }
        }

        IEnumerable<IncidentStatus> ILookup<IncidentStatus, IncidentStatus>.this[IncidentStatus key]
        {
            get
            {
                var values = _maps
                    .Where(i => i.PreStatus == key && i.PostStatus != null)
                    .Select(i => i.PostStatus ?? IncidentStatus.None);

                return values;
            }
        }

        public class Transition : TransitionMap<IncidentCommand, IncidentStatus>
        {
            public Transition(IncidentStatus preStatus, IncidentCommand command, IncidentStatus? postStatus = null)
                : base(preStatus, command, postStatus)
            {
            }

            public Transition(IncidentStatus preStatus, IncidentStatus postStatus)
                : base(preStatus, IncidentCommand.None, postStatus)
            {
            }
        }
    }
}