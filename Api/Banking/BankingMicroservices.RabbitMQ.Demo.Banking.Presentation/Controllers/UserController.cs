using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.UserCommands;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.UserQueries;
using BankingMicroservices.RabbitMQ.Demo.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Presentation.Controllers;

[Route("api/[controller]")]
public class UserController(
    IMapper mapper,
    ISender sender
    )
    : ApiController(sender, mapper)
{

    // GET : api/user/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int  id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new UserQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    // POST : api/user
    [HttpPost]
    public async Task<IActionResult> Add(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(createUserCommand, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    // UPDATE : api/user
    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(updateUserCommand, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
