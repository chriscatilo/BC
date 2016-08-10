using Microsoft.Owin.Security;

namespace BC.Security.Internal
{
    public class WindowsPrincipalAuthenticationOptions : AuthenticationOptions
    {
        public const string OptionsAuthenticationType = "BC.Windows.ExtendedAuthentication";
        public WindowsPrincipalAuthenticationOptions()
            : base(OptionsAuthenticationType)
        {
            AuthenticationMode = AuthenticationMode.Active;
        }

        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }
    }
}