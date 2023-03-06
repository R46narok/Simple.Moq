using SimpleMoq.Api.Common;

#nullable disable

namespace SimpleMoq.Api.Data.Entities;

public class Customer : EntityBase<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    
    public string FullName => $"{FirstName} {LastName}";
}