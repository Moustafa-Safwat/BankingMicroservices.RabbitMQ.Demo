using System.ComponentModel.DataAnnotations;

namespace BankingMicroservices.RabbitMQ.Demo.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    [Timestamp] // Cancelation Token for concurency update
    public byte[] RowVersion { get; set; } = [];
}
