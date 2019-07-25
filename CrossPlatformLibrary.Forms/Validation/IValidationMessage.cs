namespace CrossPlatformLibrary.Forms.Validation
{
    /// <summary>
    /// Extends a validation rule with a custom validation error message.
    /// </summary>
    public interface IValidationMessage
    {
        string GetErrorMessage();
    }

    /// <summary>
    /// Extends a validation rule with a custom validation error message.
    /// </summary>
    /// <typeparam name="T">Property type for which the validation is set up.</typeparam>
    public interface IValidationMessage<in T>
    {
        string GetErrorMessage(T value);
    }

}