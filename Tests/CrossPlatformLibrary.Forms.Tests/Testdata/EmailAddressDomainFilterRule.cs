using System.Collections.Generic;
using System.Linq;
using CrossPlatformLibrary.Forms.Validation;

namespace CrossPlatformLibrary.Forms.Tests.Testdata
{
    public class EmailAddressDomainFilterRule : IValidationRule<string>, IValidationMessage
    {
        private static readonly List<string> DomainBlacklist = new List<string>
        {
            "hotmail.ru",
            "hotmail.tv"
        };

        public bool IsValid(string value)
        {
            return value != null && DomainBlacklist.All(d => !value.EndsWith(d));
        }

        public string GetErrorMessage()
        {
            return "Domain is black-listed.";
        }
    }
}