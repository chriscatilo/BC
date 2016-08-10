using System;
using System.Collections.Generic;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Domain.Security
{
    /// <summary>
    /// This object has one purpose and one purpose only, to return a list of the tabs that are available, so please dont throw any other public methods in here
    /// There are other pointless "managers" else where for that or better still create a new object with a specfic purpose, If you do this then this object will conform to the single 
    /// responibility principle 
    /// </summary>
    public class IncidentTabAvailablityRetriever : IIncidentTabAvailablityRetriever
    {
        private readonly IAssetAuthoriser _authoriser;
        private readonly IRepository<IncidentMasterModel> _incidentRepository;
        private List<string> _availableIncidentsTabs = new List<String>();
        private int _id;

        public IncidentTabAvailablityRetriever(IAssetAuthoriser authoriser, IRepository<IncidentMasterModel> incidentRepository)
        {
            _authoriser = authoriser;
            _incidentRepository = incidentRepository;
        }

        public String[] Get(int id)
        {
            _id = id;
          
            AddIncidentsTab();

            AddActionsTab();

            AddActivityTab();

            return _availableIncidentsTabs.ToArray();
        }

        private void AddActivityTab()
        {
            if (_authoriser.IsAuthorised(AssetType.IncidentActivityViewList, _id))
            {
                _availableIncidentsTabs.Add("activity");
            }
        }

        private void AddActionsTab()
        {
            if (_authoriser.IsAuthorised(AssetType.IncidentViewListActions, _id))
            {
                _availableIncidentsTabs.Add("actions");
            }
        }

        private void AddIncidentsTab()
        {
            if (_authoriser.IsAuthorised(AssetType.IncidentViewListIncidents, _id))
            {
                _availableIncidentsTabs.Add("incidents");
            }
        }
    }
}
