using System.ComponentModel.DataAnnotations;

namespace BankingMicroservices.RabbitMQ.Demo.Application.Dtos;

public abstract record BaseDto
{
    public int Id { get; set; }
    public byte[] RowVersion { get; set; } = [];
}
