using System;
using System.Web;

namespace BC.EQCS.Web.Utils
{
    public class RolesDebugHelper
    {
        public RolesDebugHelper()
        {
            if (HttpContext.Current.Request.QueryString["RolesDebug"] == null)
            {
                return;
            }
            
            bool activateRolesDebug;
            Boolean.TryParse(HttpContext.Current.Request.QueryString["RolesDebug"], out activateRolesDebug);
            HttpContext.Current.Session["rolesDebug"] = activateRolesDebug;
        }

        public bool IsRolesDebugActivated
        {
            get {
                if (HttpContext.Current.Session["rolesDebug"] == null)
                {
                    return false;
                }

                bool rolesDebug;
                Boolean.TryParse(HttpContext.Current.Session["rolesDebug"].ToString(), out rolesDebug);
                return rolesDebug;
            }
        }
    }
}