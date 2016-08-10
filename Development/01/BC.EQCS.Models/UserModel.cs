using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BC.EQCS.Models
{
    public class UserModel
    {
        [JsonIgnore]
        public Guid ObjectGuid { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string DisplayName { get; set; }

        public IEnumerable<String> ApplicationRoles { get; set; }

        public AdminUnitModel AdminStructure { get; set; }

        public IncidentClassModel AvailableIncidentClasses { get; set; }

        public IncidentClassModel ViewableIncidentClasses { get; set; }
    }
}
