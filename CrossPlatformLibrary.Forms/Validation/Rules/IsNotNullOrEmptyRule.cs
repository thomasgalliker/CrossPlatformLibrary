namespace CrossPlatformLibrary.Forms.Validation.Rules
{
    public class IsNotNullOrEmptyRule : IValidationRule<object>, IValidationMessage
    {
        public bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return !string.IsNullOrWhiteSpace(str);
        }

        public string GetErrorMessage()
        {
            return "Value must not be null or empty";
        }
    }
}