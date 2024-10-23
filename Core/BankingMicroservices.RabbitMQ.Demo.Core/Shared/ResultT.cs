using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankingMicroservices.RabbitMQ.Demo.Core.Shared;

public class Result<TValue>(TValue value, bool isSuccess, Error error)
    : Result(isSuccess, error)
{
    public TValue Value => IsSuccess
        ? value
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static Result<TValue> Success(TValue value) => new(value, true, Error.None);

    public static new Result<TValue> Failure(Error error) => new(default!, false, error);

    public static Result<TValue> Create(TValue? value) => value is not null
        ? Success(value)
        : Failure(Error.NullValue);

    public static implicit operator Result<TValue>(TValue? value) => Create(value);

    public new Result<TValue> AppendFailure(Error error)
    {
        base.AppendFailure(error);
        return this;
    }
}
