using System;

namespace BC.EQCS.Domain.Exceptions
{
    public class InvalidWorkflowTransitionException : ApplicationException
    {
        public InvalidWorkflowTransitionException(string message)
            : base(message)
        { }
    }
}
