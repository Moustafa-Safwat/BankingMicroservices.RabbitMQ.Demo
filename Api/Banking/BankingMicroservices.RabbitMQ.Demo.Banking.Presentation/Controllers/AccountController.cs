using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.AccountQueries;
using BankingMicroservices.RabbitMQ.Demo.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Presentation.Controllers;

[Route("api/[controller]")]
public class AccountController(
    ISender sender,
    IMapper mapper
    )
    : ApiController(sender, mapper)
{

    // GET : api/account/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountById(int id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new AccountQuery { AccountId = id }, cancellationToken);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return NotFound(result);
    }

    // POST : api/account
    [HttpPost]
    public async Task<IActionResult> Add(CreateAccountCommand createAccountCommand, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(createAccountCommand, cancellationToken);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}