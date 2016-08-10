using System;

namespace BC.EQCS.Repositories.Security
{
    public class ActiveDirectoryResultsLimitExceededException : Exception
    {
        private const string MessageTemplate = "Search results exceeded maximum limit of {0}";
      
        public ActiveDirectoryResultsLimitExceededException(int maxResults) : base(string.Format(MessageTemplate, maxResults))
        {
        }
    }
}