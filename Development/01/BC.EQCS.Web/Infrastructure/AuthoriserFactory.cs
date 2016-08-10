using System.Web;
using BC.EQCS.Security.Service;
using StructureMap;

namespace BC.EQCS.Web.Infrastructure
{
    public class AuthoriserFactory
    {
        public IAssetAuthoriser Create()
        {
            if(HttpContext.Current == null)
            {
                return null;
            } 
           
            return ObjectFactory.GetInstance<IAssetAuthoriser>();
        }
    }
}