using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using BankingMicroservices.RabbitMQ.Demo.Notification.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Notification.Core.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BankingMicroservices.RabbitMQ.Demo.Notification.Infra.Data.EmailServices;

/// <summary>
/// Service for sending emails using Gmail's SMTP server.
/// </summary>
public sealed class GmailService(IOptions<EmailSettings> emailSettings) : IEmailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="toEmail">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the email sending operation.</returns>
    public async Task<Result> SendEmailAsync(string toEmail, string subject, string body, CancellationToken cancellationToken)
    {
        try
        {
            using var smtpClient = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SmtpUser, _emailSettings.SmtpPass),
                EnableSsl = true
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SmtpUser),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new("SEND_EMAIL_FAILED", $"{ex.Message}\n{ex.InnerException}"));
        }
    }
}
