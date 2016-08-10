using System;
using System.Collections.Generic;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Contracts
{
    public interface IPermissionsRepository
    {
        ICollection<SecurityUserModel> AllUsersWhoCanViewIncidentByIncidentId(Int32 incidentId);
    }
}
