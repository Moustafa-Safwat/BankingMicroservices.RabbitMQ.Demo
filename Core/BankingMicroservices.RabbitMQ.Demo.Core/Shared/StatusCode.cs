namespace BankingMicroservices.RabbitMQ.Demo.Core.Shared;

public class StatusDetail
{
    public StatusCode Code { get; set; }
    public string Message { get; set; } = string.Empty;

    public StatusDetail()
    {

    }
    public StatusDetail(StatusCode code, string message)
    {
        Code = code;
        Message = message;
    }
}

public enum StatusCode
{
    None = 0,
    Ok = 200,
    Created = 201,
    BadRequest = 400,
    NotFound = 404

}

public static class StatusCodeDictionary
{
    public static readonly Dictionary<StatusCode, string> StatusCodes = new()
    {
        {StatusCode.None, "" },
        {StatusCode.Ok, "Request successful. The server has responded as required." },
        {StatusCode.Created,"A new resource was created successfully." },
        {StatusCode.BadRequest,"The server could not understand the request. Maybe a bad syntax?" },
        {StatusCode.NotFound,"Requested resource could not be found." }
    };
}
