namespace BankingMicroservices.RabbitMQ.Demo.Core.Shared;

public class Result
{
    private Error _error = new(string.Empty, string.Empty);
    public Error Error
    {
        get => _error;
        protected set
        {
            _error = value;
            Errors.Add(value);
        }
    }
    public IList<Error> Errors { get; private set; } = [];
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new InvalidOperationException("Invalid combination of isSuccess and error.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public Result AppendFailure(Error error)
    {
        Error = error;
        return this;
    }
}
