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
    public static Result Failures(IReadOnlyList<Error> errors)
    {
        if (errors == null || errors.Count == 0)
        {
            throw new ArgumentException("The errors collection must not be null or empty.", nameof(errors));
        }

        var result = new Result(false, errors[0]);
        for (int i = 1; i < errors.Count; i++)
        {
            if (errors[i] == null)
            {
                throw new ArgumentException("The errors collection must not contain null values.", nameof(errors));
            }
            result.AppendFailure(errors[i]);
        }
        return result;
    }
    public Result AppendFailure(Error error)
    {
        _errors.Add(error);
        return this;
    }
}
