using System;
using System.Reflection;

namespace CrossPlatformLibrary.Forms.Validation
{
    public class PropertyValidation
    {
        private Func<bool> validCriteria;
        private Func<string> errorFunction;
        private PropertyInfo propertyInfo;
        private object baseViewModel;
        private IValidationRule validationRule;

        public PropertyValidation(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        ///     Whens the specified validation criteria.
        /// </summary>
        /// <param name="validationCriteria">The validation criteria.</param>
        /// <returns>The property validation object.</returns>
        /// <exception cref="System.InvalidOperationException">You can only set the validation criteria once.</exception>
        public PropertyValidation When(Func<bool> validationCriteria)
        {
            this.EnsureValidationCriteria();

            this.validCriteria = validationCriteria;

            return this;
        }

        public PropertyValidation When<T>(IValidationRule<T> validationRule)
        {
            this.EnsureValidationCriteria();

            this.validationRule = validationRule;

            this.validCriteria = () => !validationRule.IsValid((T)this.GetPropertyValue());

            return this;
        }

        private void EnsureValidationCriteria()
        {
            if (this.validCriteria != null)
            {
                throw new InvalidOperationException("You can only set the validation criteria once.");
            }
        }

        /// <summary>
        ///     Shows the specified error message.
        ///     Use this function if the error message is static and it is not subject to change throughout the lifetime of the
        ///     user interface.
        ///     Otherwise use the Show(Function) method.
        /// </summary>
        /// <param name="message">The message to show.</param>
        /// <returns>The property validation object.</returns>
        /// <exception cref="System.InvalidOperationException">You can only set the message once.</exception>
        public PropertyValidation Show(string message)
        {
            this.EnsureErrorFunction();

            this.errorFunction = () => message;
            return this;
        }

        public PropertyValidation ShowNewLine()
        {
            this.EnsureErrorFunction();

            this.errorFunction = () => string.Empty;
            return this;
        }

        /// <summary>
        ///     Shows the specified error function.
        ///     Use this function if the error message is subject to change,
        ///     e.g. you're using variables that might change during the lifetime of the validated user interface.
        /// </summary>
        /// <param name="function">The error function.</param>
        /// <returns>The property validation object.</returns>
        /// <exception cref="System.InvalidOperationException">You can only set the message once.</exception>
        public PropertyValidation Show(Func<string> function)
        {
            this.EnsureErrorFunction();

            this.errorFunction = function;
            return this;
        }

        public PropertyValidation Show(Func<object, string> function)
        {
            this.EnsureErrorFunction();

            this.errorFunction = () => function(this.GetPropertyValue());
            return this;
        }

        private void EnsureErrorFunction()
        {
            if (this.errorFunction != null)
            {
                throw new InvalidOperationException("You can only set the message once.");
            }
        }

        internal void SetContext(object baseViewModel)
        {
            this.baseViewModel = baseViewModel;
            this.propertyInfo = baseViewModel.GetType().GetProperty(this.PropertyName);
        }

        private object GetPropertyValue()
        {
            return this.propertyInfo.GetValue(this.baseViewModel);
        }

        /// <summary>
        ///     Determines whether the specified presentation model is invalid.
        /// </summary>
        /// <returns><c>true</c> if the specified presentation model is invalid; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.InvalidOperationException">
        ///     No criteria have been provided for this validation. (Use the
        ///     'When(..)' method.).
        /// </exception>
        public bool IsInvalid()
        {
            if (this.validCriteria != null)
            {
                return this.validCriteria();
            }

            throw new InvalidOperationException("No criteria have been provided for this validation. (Use the 'When(..)' method.)");

        }

        /// <summary>
        ///     Gets the error message.
        /// </summary>
        /// <returns>An error message.</returns>
        /// <exception cref="System.InvalidOperationException">
        ///     No error message has been set for this validation. (Use the
        ///     'Show(..)' method.).
        /// </exception>
        public string GetErrorMessage()
        {
            if (this.errorFunction != null)
            {
                return this.errorFunction();
            }

            // Get parameter-less error message
            if (this.validationRule is IValidationMessage validationMessage)
            {
                return validationMessage.GetErrorMessage();
            }

            // Get error message with property value as parameter
            if (this.validationRule != null)
            {
                var getErrorMessageMethod = this.validationRule.GetType().GetMethod(nameof(IValidationMessage<object>.GetErrorMessage));
                if (getErrorMessageMethod != null)
                {
                    return getErrorMessageMethod.Invoke(this.validationRule, new[] { this.GetPropertyValue() }) as string;
                }
            }

            throw new InvalidOperationException("No error message has been set for this validation. (Use the 'Show(..)' method.)");
        }

        /// <summary>
        ///     Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; }
    }
}