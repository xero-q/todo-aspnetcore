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
    public async Task<IActionResult> GetPaginated(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10, string? filterByTitle=null, string? filterByDescription = null, int? filterByIsCompleted=null)
    {
        var getPaginatedTodosQuery = new GetPaginatedTodosQuery(pageNumber, pageSize, filterByTitle,filterByDescription,filterByIsCompleted);
        var response = await Mediator.Send(getPaginatedTodosQuery, cancellationToken);
        
        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] int id,CancellationToken cancellationToken)
    {
        var getTodoByIdQuery = new GetTodoByIdQuery(id);
        var response = await Mediator.Send(getTodoByIdQuery, cancellationToken);
        
        return Ok(response);
    }
    
    // [HttpPost]
    // [Authorize]
    // public async Task<IActionResult> Create([FromBody] CreatePropertyRequest request,CancellationToken cancellationToken)
    // {
    //     var createPropertyCommand = new CreatePropertyCommand(request.HostId, request.Name, request.Location,
    //         request.PricePerNight, request.Status);
    //     
    //     var response = await Mediator.Send(createPropertyCommand, cancellationToken);
    //     
    //     return CreatedAtAction(
    //         nameof(GetById),    
    //         new { id = response.Id },     
    //         response                        
    //     );
    // }
    //
    // [HttpPut("{id:int}")]
    // [Authorize]
    // public async Task<IActionResult> Create([FromBody] UpdatePropertyRequest request,[FromRoute] int id, CancellationToken cancellationToken)
    // {
    //     var updatePropertyCommand = new UpdatePropertyCommand(id, request.HostId, request.Name, request.Location,
    //         request.PricePerNight, request.Status);
    //     
    //     var response = await Mediator.Send(updatePropertyCommand, cancellationToken);
    //     
    //     return Ok(response);
    // }
    //
    // [HttpDelete("{id:int}")]
    // [Authorize]
    // public async Task<IActionResult> Create([FromRoute] int id, CancellationToken cancellationToken)
    // {
    //     var deletePropertyCommand = new DeletePropertyCommand(id);
    //     
    //     await Mediator.Send(deletePropertyCommand, cancellationToken);
    //     
    //     return Ok();
    // }
}

