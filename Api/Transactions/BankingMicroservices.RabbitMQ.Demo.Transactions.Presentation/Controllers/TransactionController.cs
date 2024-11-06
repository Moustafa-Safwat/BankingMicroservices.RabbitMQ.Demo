using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Presentation.Controllers;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Presentation.Controllers;

[Route("api/[controller]")]
public class TransactionController(
    ISender sender,
    IMapper mapper
    ) 
    :ApiController(sender,mapper)
{
    // POST : api/transaction
    [HttpPost]
    public async Task<IActionResult> Add(CreateTransactionCommand createTransactionCommand
        , CancellationToken cancellationToken)
    {
        var result = await Sender.Send(createTransactionCommand, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
