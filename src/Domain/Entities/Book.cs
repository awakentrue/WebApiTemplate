using Domain.Common;

namespace Domain.Entities;

public class Book : IEntity
{
    public int Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Genre { get; set; }
}