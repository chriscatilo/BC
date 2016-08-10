using System;
using System.Collections.Generic;

namespace BC.EQCS.Models
{
    [Serializable]
    public class ValidationResult
    {
        public ValidationResult(bool isValid, IEnumerable<ValidationFailure> failures = null)
        {
            this.IsValid = isValid;

            this.Errors = failures ?? new List<ValidationFailure>();
        }

        public bool IsValid { get; private set; }

        public IEnumerable<ValidationFailure> Errors { get; private set; }
    }
}