namespace Domain.Common.Exceptions;

public class EntityNotFoundException : DomainException
{
    public EntityNotFoundException(string entityName, int id) : base($"Entity '{entityName}' (id: {id}) was not found")
    {
        
    }
}