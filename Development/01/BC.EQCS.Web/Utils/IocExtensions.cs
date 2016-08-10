using System.Web;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.Web.Pipeline;

namespace BC.EQCS.Web.Utils
{
    public static class IocExtensions
    {
        public static void SetHttpLifeCycleIfIis(this HasLifecycle smartInstance)
        {
            if (HttpContext.Current != null)
            {
                smartInstance.SetLifecycleTo<HttpContextLifecycle>();
            }
            else
            {
                smartInstance.SetLifecycleTo<TransientLifecycle>();
            }
        }
    }
}