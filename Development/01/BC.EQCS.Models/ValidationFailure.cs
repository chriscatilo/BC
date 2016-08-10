namespace BC.EQCS.Models
{
    public class ValidationFailure
    {
        public ValidationFailure(string propertyName, string errorMessage, dynamic attemptedValue)
        {
            this.PropertyName = propertyName;

            this.ErrorMessage = errorMessage;

            this.AttemptedValue = attemptedValue;
        }

        public string PropertyName { get; private set; }

        public string ErrorMessage { get; private set; }

        public dynamic AttemptedValue { get; private set; }
    }
}