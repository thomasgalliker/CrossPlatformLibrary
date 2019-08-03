using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossPlatformLibrary.Forms.Validation
{
    public abstract class AbstractValidation : IValidation
    {
        protected static readonly Dictionary<string, List<string>> EmptyErrorsCollection = new Dictionary<string, List<string>>(0);

        protected AbstractValidation(string[] propertyNames)
        {
            this.PropertyNames = propertyNames;
        }

        public string[] PropertyNames { get; }

        public abstract Task<Dictionary<string, List<string>>> GetErrors();
    }
}