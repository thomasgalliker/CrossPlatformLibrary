using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CrossPlatformLibrary.Forms.Validation
{
    public class DelegateValidation : AbstractValidation
    {
        private readonly TaskDelayer taskDelayer = new TaskDelayer();
        private TimeSpan delay;
        private Func<Task<Dictionary<string, List<string>>>> errorFunction;

        public DelegateValidation(string[] propertyNames) : base(propertyNames)
        {
            this.errorFunction = () => throw new InvalidOperationException($"Use Validate(...) method to setup validation delegation.");
        }

        public DelegateValidation Validate(Func<Task<Dictionary<string, List<string>>>> function, TimeSpan? validationDelay = null)
        {
            this.errorFunction = function;
            return this.WithDelay(validationDelay ?? TimeSpan.Zero);
        }

        public DelegateValidation WithDelay(TimeSpan validationDelay)
        {
            this.delay = validationDelay;

            return this;
        }

        public override async Task<Dictionary<string, List<string>>> GetErrors()
        {
            Debug.WriteLine($"GetErrors");

            Dictionary<string, List<string>> result;
            if (this.delay > TimeSpan.Zero)
            {
                result = await this.taskDelayer.RunWithDelay(this.delay, () => this.errorFunction(), () => EmptyErrorsCollection);
            }
            else
            {
                result = await this.errorFunction();
            }

            Debug.WriteLine($"GetErrors (result={(result == null ? "null" : $"{result.Count}")})");

            return result;
        }
    }
}