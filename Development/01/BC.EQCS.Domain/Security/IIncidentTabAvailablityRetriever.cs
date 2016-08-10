using System;

namespace BC.EQCS.Domain.Security
{
    public interface IIncidentTabAvailablityRetriever
    {
        String[] Get(int id);
    }
}