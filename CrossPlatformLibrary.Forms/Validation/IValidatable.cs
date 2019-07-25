namespace CrossPlatformLibrary.Forms.Validation
{
    /// <summary>
    ///     Interface used to validate the object data.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        ///     Validates against the set of validation rules.
        /// </summary>
        /// <returns>True if the data is valid, otherwise false.</returns>
        bool IsValid();
    }
}