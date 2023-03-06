#nullable disable

namespace SimpleMoq.Api.Dto;

public class CreateCustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}