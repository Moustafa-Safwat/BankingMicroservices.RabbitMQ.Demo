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
        var result = await Sender.Send(new AccountQuery { Id = id }, cancellationToken);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    // POST : api/account
    [HttpPost]
    public async Task<IActionResult> Add(CreateAccountCommand createAccountCommand, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(createAccountCommand, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    // UPDATE : api/account/{id}
    [HttpPut]
    public async Task<IActionResult> Update(UpdateAccountCommand updateAccountCommand, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(updateAccountCommand, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    // DELETE : api/account/{id}
    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteAccountCommand(id), cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}