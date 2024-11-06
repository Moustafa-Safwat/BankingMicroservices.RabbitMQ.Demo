using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.TransactionCommands;
using BankingMicroservices.RabbitMQ.Demo.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Presentation.Controllers;

[Route("api/[controller]")]
public class TransactionController(
    ISender sender,
    IMapper mapper
    )
    : ApiController(sender, mapper)
{

    // POST : api/transaction/dispose
    [HttpPost("dispose")]
    public async Task<IActionResult> Dispose(DisposeMoneyCommand disposeMoneyCommand, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(disposeMoneyCommand, cancellationToken);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    // POST : api/transaction/withdrawal
    [HttpPost("withdrawal")]
    public async Task<IActionResult> WithDrawal(WithDrawalMoneyCommand withDrawalMoneyCommand, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(withDrawalMoneyCommand, cancellationToken);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }
}
