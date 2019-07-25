using System.Text.RegularExpressions;

namespace CrossPlatformLibrary.Forms.Validation.Rules
{
    public class EmailAddressValidationRule : IValidationRule<string>, IValidationMessage<string>
    {
        public string GetErrorMessage(string emailAddress)
        {
            return $"Email address '{emailAddress}' is not valid.";
        }

        public bool IsValid(string emailAddress)
        {
            if (emailAddress == null)
            {
                return false;
            }

            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(emailAddress);

            return match.Success;
        }
    }
}
