using AD = System.DirectoryServices.ActiveDirectory;

namespace BC.EQCS.Integration.Utils
{
    public static class DomainChecker
    {
        public const string DefaultDomainName = "CORPORATE";

        public static bool IsInDomain(string domainName = DefaultDomainName)
        {

            AD.DirectoryContext context = new AD.DirectoryContext(AD.DirectoryContextType.Domain, domainName);
       
            try
            {
                AD.Domain.GetDomain(context);
            }
            catch (AD.ActiveDirectoryObjectNotFoundException)
            {
                return false;
            }

            return true;

        }
    }
}
