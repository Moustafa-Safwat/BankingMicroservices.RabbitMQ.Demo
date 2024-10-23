using BankingMicroservices.RabbitMQ.Demo.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;

public class Account : BaseEntity
{
    public decimal Balance { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
