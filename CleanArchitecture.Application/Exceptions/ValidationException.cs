namespace CleanArchitecture.Application.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public ValidationException(IEnumerable<ValidateError> errors)
        {
            Errors = errors;
        }
        public IEnumerable<ValidateError> Errors { get; }
    }
}
