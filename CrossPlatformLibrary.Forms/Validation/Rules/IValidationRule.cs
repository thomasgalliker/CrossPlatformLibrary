namespace CrossPlatformLibrary.Forms.Validation.Rules
{
    public interface IValidationRule
    {
        string ValidationMessage { get; }

        bool IsValid();
    }

    public interface IValidationRule<in T>
    {
        string ValidationMessage { get; }

        bool Check(T value);
    }
}
