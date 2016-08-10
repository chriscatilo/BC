using System;

namespace BC.EQCS.Domain.Exceptions
{
    public class TransitionNotAllowed : ApplicationException
    {
        public TransitionNotAllowed(string msg, params object[] args)
            : base(string.Format(msg, args))
        {

        }

    }
}