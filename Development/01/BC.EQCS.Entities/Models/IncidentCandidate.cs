using System;
using System.ComponentModel.DataAnnotations;

namespace BC.EQCS.Entities.Models
{
    public class IncidentCandidate
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public string Number { get; set; }
        public string Surname { get; set; }
        public string Firstnames { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string IdDocumentNumber { get; set; }
        public string TrfNumber { get; set; }
        public DateTime? DateTrfCancelled { get; set; }
        public string UKVIRefNumber { get; set; }
        public int? NationalityId { get; set; }
        public virtual Country Nationality { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}