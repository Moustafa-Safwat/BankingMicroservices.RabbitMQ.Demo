using BankingMicroservices.RabbitMQ.Demo.Core.Entities;
using System.Globalization;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    // Navigation property
    public ICollection<Account> Accounts { get; set; } = [];
}   
