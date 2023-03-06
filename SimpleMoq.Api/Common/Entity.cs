using System.ComponentModel.DataAnnotations;

namespace SimpleMoq.Api.Common;

public interface IEntity<T>
{
    [Key]
    public T Id { get; set; } 
}

public abstract class EntityBase<T> : IEntity<T>
{
    [Key] 
    public T Id { get; set; } = default!;
}