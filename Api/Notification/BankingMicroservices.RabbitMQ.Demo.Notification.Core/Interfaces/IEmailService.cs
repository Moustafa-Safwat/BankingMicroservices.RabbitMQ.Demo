using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Notification.Core.Interfaces;

public interface IEmailService
{
    Task<Result> SendEmailAsync(string toEmail, string subject, string body,CancellationToken cancellationToken);
}
