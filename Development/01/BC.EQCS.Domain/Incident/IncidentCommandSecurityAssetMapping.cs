using System;
using System.Collections.Generic;
using BC.EQCS.Domain.Utils;
using BC.EQCS.Security.Constants;

namespace BC.EQCS.Domain.Incident
{
    public class IncidentCommandSecurityAssetMapping : KeyValueTable<IncidentCommand, String>
    {
        private static readonly IDictionary<IncidentCommand, String> _maps
            = new Dictionary<IncidentCommand, String>
            {
                {IncidentCommand.Save, AssetType.IncidentDraftRaise},
                {IncidentCommand.Delete, AssetType.IncidentDelete},
                {IncidentCommand.Raise, AssetType.IncidentDraftRaise},
                {IncidentCommand.Accept, AssetType.IncidentAcceptReject},
                {IncidentCommand.Reject, AssetType.IncidentAcceptReject},
                {IncidentCommand.Close, AssetType.IncidentClose},
                {IncidentCommand.ReOpen, AssetType.IncidentReopen},
                {IncidentCommand.AddCandidate, AssetType.IncidentDraftRaise}
            };

        public IncidentCommandSecurityAssetMapping() : base(_maps)
        {
        }
    }
}