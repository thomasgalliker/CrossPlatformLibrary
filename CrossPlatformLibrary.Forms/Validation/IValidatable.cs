namespace CrossPlatformLibrary.Forms.Validation
{
    /// <summary>
    /// Interface used to validate the object data.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Setup the validation rules for client side validations.
        /// </summary>
        //void SetupValidationRules();

        /// <summary>
        /// Validates the data.
        /// </summary>
        /// <returns>True if the data is valid, otherwise false.</returns>
        bool IsValid();
    }
}