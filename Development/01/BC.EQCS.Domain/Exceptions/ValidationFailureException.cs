using System;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Exceptions
{
    [Serializable]
    public class ValidationFailureException : ApplicationException
    {
        public ValidationFailureException(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; set; }
    }
}