using BC.EQCS.Security.Models;

namespace BC.EQCS.Security.Service
{
    // TODO Chris: retionalise name of this interface - too similar to IUserContext
    public interface IContextResolver
    {
        SecurityUserModel CurrentUser { get; }
    }
}