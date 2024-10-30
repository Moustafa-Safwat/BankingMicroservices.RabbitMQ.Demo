using System.ComponentModel.DataAnnotations;

namespace BankingMicroservices.RabbitMQ.Demo.Application.Dtos;

public record BaseDto
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; } = [];
}
