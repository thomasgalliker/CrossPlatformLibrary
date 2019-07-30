using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CrossPlatformLibrary.Forms.Validation
{
    public class DelegateValidation : IValidation
    {
        private static readonly Dictionary<string, List<string>> EmptyErrorsCollection = new Dictionary<string, List<string>>(0);

        private readonly TaskDelayer taskDelayer = new TaskDelayer();
        private readonly TimeSpan delay = TimeSpan.FromMilliseconds(200);
        private Func<Task<Dictionary<string, List<string>>>> errorFunction;

        public DelegateValidation(string[] propertyNames)
        {
            this.PropertyNames = propertyNames;
        }

        public void Show(Func<Task<Dictionary<string, List<string>>>> function)
        {
            this.EnsureErrorFunction();

            this.errorFunction = function;
        }

        private void EnsureErrorFunction()
        {
            if (this.errorFunction != null)
            {
                throw new InvalidOperationException("You can only set the message once.");
            }
        }

        public string[] PropertyNames { get; }

        public async Task<Dictionary<string, List<string>>> GetErrors()
        {
            Debug.WriteLine($"GetErrors");
            var result = await this.taskDelayer.RunWithDelay(this.delay, () => this.errorFunction(), () => EmptyErrorsCollection);
            Debug.WriteLine($"GetErrors (result={result.Count})");

            return result;
        }
    }
}