using Application.Contracts.Responses;
using Application.Features.Todos.Commands.Create;
using Domain.Todos;

namespace Application.Mappings;

public static class ContractMappings
{
    // public static Host MapToHost(this CreateHostRequest request)
    // {
    //     return new Host
    //     {
    //         FullName = request.FullName,
    //         Email = request.Email,
    //         Phone = request.Phone
    //     };
    // }
    //
    
    public static Todo MapToTodo(this CreateTodoCommand request)
    {
        return new Todo
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            IsCompleted = false
        };
    }
    public static TodoResponse MapToResponse(this Todo todo)
    {
        return new TodoResponse
        {
            TodoId = todo.TodoId,
            Title = todo.Title,
            Description = todo.Description,
            DueDate = DateTime.SpecifyKind(todo.DueDate, DateTimeKind.Utc),
            IsCompleted = todo.IsCompleted
            
        };
    }
    // public static PropertyResponse MapToResponse(this Property property)
    // {
    //     return new PropertyResponse
    //     {
    //         Id = property.Id,
    //         HostId = property.HostId,
    //         Name = property.Name,
    //         Location = property.Location,
    //         PricePerNight = property.PricePerNight,
    //         Status = property.Status,
    //         CreatedAt = DateTime.SpecifyKind(property.CreatedAt, DateTimeKind.Utc),
    //         Host = property.Host.MapToResponse()
    //     };
    // }
    //
    // public static DomainEvent MapToDomainEvent(this CreateDomainEventCommand request)
    // {
    //     return new DomainEvent
    //     {
    //         PropertyId = request.PropertyId,
    //         EventType = request.EventType,
    //         PayloadJSON = request.PayloadJSON,
    //         CreatedAt = DateTime.UtcNow
    //     };
    // }
    //
    // public static DomainEventResponse MapToResponse(this DomainEvent domainEvent)
    // {
    //     return new DomainEventResponse
    //     {
    //         Id = domainEvent.Id,
    //         EventType = domainEvent.EventType,
    //         PayloadJSON = domainEvent.PayloadJSON,
    //         CreatedAt = DateTime.SpecifyKind(domainEvent.CreatedAt, DateTimeKind.Utc),
    //         PropertyId = domainEvent.PropertyId
    //     };
    // }
}