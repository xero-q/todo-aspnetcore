using Application.Contracts.Requests;
using Application.Features.Todos.Commands.Create;
using Application.Features.Todos.Commands.Delete;
using Application.Features.Todos.Commands.Update;
using Application.Features.Todos.Queries.GetById;
using Application.Features.Todos.Queries.GetPaginated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
public class TodosController : BaseApiController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPaginated(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10, string? filterByTitle = null, string? filterByDescription = null, int? filterByIsCompleted = null)
    {
        var getPaginatedTodosQuery = new GetPaginatedTodosQuery(pageNumber, pageSize, filterByTitle, filterByDescription, filterByIsCompleted);
        var response = await Mediator.Send(getPaginatedTodosQuery, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var getTodoByIdQuery = new GetTodoByIdQuery(id);
        var response = await Mediator.Send(getTodoByIdQuery, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateTodoRequest request, CancellationToken cancellationToken)
    {
        var createTodoCommand = new CreateTodoCommand(request.Title, request.Description, request.DueDate);

        var response = await Mediator.Send(createTodoCommand, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.TodoId },
            response
        );
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] UpdateTodoRequest request, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var updateTodoCommand = new UpdateTodoCommand(id, request.Title, request.Description, request.DueDate,
            request.IsCompleted);

        var response = await Mediator.Send(updateTodoCommand, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var deleteTodoCommand = new DeleteTodoCommand(id);

        await Mediator.Send(deleteTodoCommand, cancellationToken);

        return Ok();
    }
}

