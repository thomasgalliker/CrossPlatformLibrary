using System;

namespace CrossPlatformLibrary.Forms.Validation.Rules
{
    public class EmailAddressDomainFilterRule : IValidationRule
    {
        private readonly Func<string> emailAddresse;

        public EmailAddressDomainFilterRule(Func<string> emailAddresse)
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
            return !str.EndsWith("@hotmail.ru");
        }
    }
}