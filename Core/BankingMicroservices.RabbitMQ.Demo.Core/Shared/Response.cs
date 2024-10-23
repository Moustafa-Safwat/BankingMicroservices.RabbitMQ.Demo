namespace BankingMicroservices.RabbitMQ.Demo.Core.Shared;

public class Response<T>
{
    public bool Success { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public T? Payload { get; private set; }
    public StatusDetail? StatusCode { get; private set; }
    public IList<Error> Errors { get; private set; } = [];

    private Response() { }

    public static Response<T> Create(Result<T> result, StatusCode statusCode = Shared.StatusCode.None)
    {
        ValidateResult(result);

        return new Response<T>
        {
            Success = result.IsSuccess,
            Message = result.IsSuccess ? "Operation completed successfully." : result.Errors[0].Message,
            Payload = result.IsSuccess ? result.Value : default,
            StatusCode = new StatusDetail(statusCode, StatusCodeDictionary.StatusCodes[statusCode]),
            Errors = result.Errors.AsEnumerable().ToList()
        };
    }

    public static Response<T> Create(Result result, StatusCode statusCode = Shared.StatusCode.None)
    {
        ValidateResult(result);

        return new Response<T>
        {
            Success = result.IsSuccess,
            Message = result.IsSuccess ? "Operation completed successfully." : result.Errors[0].Message,
            Payload = default,
            StatusCode = new StatusDetail(statusCode, StatusCodeDictionary.StatusCodes[statusCode]),
            Errors = result.Errors.AsEnumerable().ToList()
        };
    }

    public static Response<T> Create(T? value, StatusCode statusCode = Shared.StatusCode.None)
    {
        return new Response<T>
        {
            Success = value != null,
            Message = value != null ? "Operation completed successfully." : "Operation failed.",
            Payload = value,
            StatusCode = new StatusDetail(statusCode, StatusCodeDictionary.StatusCodes[statusCode]),
            Errors = []
        };
    }

    public static implicit operator Response<T>(Result<T> result) => Create(result);
    public static implicit operator Response<T>(T? value) => Create(value);

    private static void ValidateResult(Result result)
    {
        if ((result.IsSuccess && result.IsFailure) || (!result.IsSuccess && !result.IsFailure))
        {
            throw new InvalidOperationException("Invalid combination of isSuccess and error.");
        }
    }
}
