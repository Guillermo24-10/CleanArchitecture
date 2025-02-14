namespace CleanArchitecture.Application.Exceptions
{
    public sealed record ValidateError(string PropertyName, string ErrorMessage);
}
