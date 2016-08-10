using System;

namespace BC.EQCS.Entities.Models.Base
{
    public class LastUpdate
    {
        public LastUpdate()
        {
            LastUpdated = DateTime.Now;
        }

        public DateTime LastUpdated { get; set; }
    }
}
