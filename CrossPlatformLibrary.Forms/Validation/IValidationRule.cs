namespace CrossPlatformLibrary.Forms.Validation
{
    /// <summary>
    ///     Validation rule marker interface.
    ///     Use <see cref="IValidationRule{T}" /> to implement custom validation rules.
    /// </summary>
    public interface IValidationRule
    {
    }

    /// <summary>
    ///     Validation rule provides a method to validate against a custom validation logic.
    /// </summary>
    /// <typeparam name="T">Property type for which the validation is set up.</typeparam>
    public interface IValidationRule<in T> : IValidationRule
    {
        /// <summary>
        ///     Runs the custom validation logic using the provided property value <paramref name="value" />.
        /// </summary>
        /// <param name="value">Property value for which the validation is set up.</param>
        /// <returns><c>True</c> if the provided value is valid, otherwise <c>false</c>.</returns>
        bool IsValid(T value);
    }
}