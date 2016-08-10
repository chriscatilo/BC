using BC.EQCS.Models;

namespace BC.EQCS.Contracts
{
    public interface IUserContext
    {
        UserModel CurrentUser { get; }
    }
}