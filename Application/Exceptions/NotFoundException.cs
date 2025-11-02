namespace Application.Exceptions;

public class NotFoundException(string name, object id) : Exception($"{name} with Id '{id}' was not found.");
