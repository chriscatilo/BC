namespace BC.EQCS.Models
{
    public interface IAddress
    {
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string Town { get; set; }
        string State { get; set; }
        string PostCode { get; set; }
    }
}