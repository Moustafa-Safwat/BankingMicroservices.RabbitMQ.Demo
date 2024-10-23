namespace BankingMicroservices.RabbitMQ.Demo.Core.Shared;

public class Result
{
    private readonly List<Error> _errors = [];
    public IReadOnlyList<Error> Errors => _errors;
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
        if (error != Error.None)
        {
            _errors.Add(error);
        }
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public Result AppendFailure(Error error)
    {
        _errors.Add(error);
        return this;
    }
}
