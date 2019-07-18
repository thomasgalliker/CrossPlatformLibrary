using System;
using CrossPlatformLibrary.Forms.Validation.Rules;

namespace CrossPlatformLibrary.Forms.Validation
{
    public class PropertyValidation
	{
	    private Func<bool> validCriteria;
        private Func<string> errorFunction;
		private string errorMessage;

        public PropertyValidation(string propertyName)
        {
			this.PropertyName = propertyName;
		}

        /// <summary>
        /// Whens the specified validation criteria.
        /// </summary>
        /// <param name="validationCriteria">The validation criteria.</param>
        /// <returns>The property validation object.</returns>
        /// <exception cref="System.InvalidOperationException">You can only set the validation criteria once.</exception>
        public PropertyValidation When(Func<bool> validationCriteria)
        {
            if (this.validCriteria != null)
            {
                throw new InvalidOperationException("You can only set the validation criteria once.");
            }

            this.validCriteria = validationCriteria;
            
            return this;
        }

        public PropertyValidation When(IValidationRule validationRule)
        {
            if (this.validCriteria != null)
            {
                throw new InvalidOperationException("You can only set the validation criteria once.");
            }

            this.validCriteria = () => !validationRule.IsValid();

            return this;
        }

        /// <summary>
        /// Shows the specified error message.
        /// Use this function if the error message is static and it is not subject to change throughout the lifetime of the user interface.
        /// Otherwise use the Show(Function) method.
        /// </summary>
        /// <param name="message">The message to show.</param>
        /// <returns>The property validation object.</returns>
        /// <exception cref="System.InvalidOperationException">You can only set the message once.</exception>
        public PropertyValidation Show(string message)
		{
            if (this.errorMessage != null && this.errorFunction == null)
            {
                throw new InvalidOperationException("You can only set the message once.");
            }

			this.errorMessage = message;
			return this;
		}

        public PropertyValidation ShowNewLine()
        {
            if (this.errorMessage != null && this.errorFunction == null)
            {
                throw new InvalidOperationException("You can only set the message once.");
            }

            this.errorMessage = String.Empty;
            return this;
        }

        /// <summary>
        /// Shows the specified error function.
        /// Use this function if the error message is subject to change,
        /// e.g. you're using variables that might change during the lifetime of the validated user interface.
        /// </summary>
        /// <param name="function">The error function.</param>
        /// <returns>The property validation object.</returns>
        /// <exception cref="System.InvalidOperationException">You can only set the message once.</exception>
        public PropertyValidation Show(Func<string> function)
        {
            if (this.errorFunction != null && this.errorMessage == null)
            {
                throw new InvalidOperationException("You can only set the message once.");
            }

            this.errorFunction = function;
            return this;
        }

        /// <summary>
        /// Determines whether the specified presentation model is invalid.
        /// </summary>
        /// <returns><c>true</c> if the specified presentation model is invalid; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.InvalidOperationException">No criteria have been provided for this validation. (Use the 'When(..)' method.).</exception>
        public bool IsInvalid()
		{
            if (this.validCriteria == null)
            {
                throw new InvalidOperationException("No criteria have been provided for this validation. (Use the 'When(..)' method.)");
            }

			return this.validCriteria();
		}

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <returns>An error message.</returns>
        /// <exception cref="System.InvalidOperationException">No error message has been set for this validation. (Use the 'Show(..)' method.).</exception>
		public string GetErrorMessage()
		{
            if (this.errorMessage == null && this.errorFunction == null)
            {
                throw new InvalidOperationException("No error message has been set for this validation. (Use the 'Show(..)' method.)");
            }

			return this.errorMessage ?? this.errorFunction.Invoke();
		}

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
		public string PropertyName { get; }
	}
}