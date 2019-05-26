using System;
using System.Text.RegularExpressions;

namespace CrossPlatformLibrary.Forms.Validation.Rules
{
    public class EmailAddressValidationRule : IValidationRule
    {
        private readonly Func<string> emailAddresse;

        public EmailAddressValidationRule(Func<string> emailAddresse)
        {
            this.emailAddresse = emailAddresse;
        }

        public string ValidationMessage { get; set; }

        public bool IsValid()
        {
            var str = this.emailAddresse();
            if (str == null)
            {
                return false;
            }

            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(str);

            return match.Success;
        }
    }
}
