using BC.EQCS.Security.Models;

namespace BC.EQCS.Security.Service
{
    public class ContextResolver : IContextResolver
    {
        public ContextResolver(SecurityUserModel userModel)
        {
            CurrentUser = userModel;
        }

        public SecurityUserModel CurrentUser { get; private set; }

        
    }
}