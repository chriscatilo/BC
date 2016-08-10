namespace BC.Security.Internal
{
    /// <summary>
    /// This is the default factory for the WindowsPrincipalHandler. 
    /// Replace this implementation if you want to supply an inherited version of the handler
    /// </summary>
    public class WindowsPrincipalHandlerFactory : IWindowsPrincipalHandlerFactory
    {
        public WindowsPrincipalHandler Create()
        {
            return new WindowsPrincipalHandler();
        }
    }
}