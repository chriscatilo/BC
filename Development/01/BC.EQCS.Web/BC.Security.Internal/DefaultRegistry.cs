using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace BC.Security.Internal
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.SingleImplementationsOfInterface();
                });
        }
    }
}