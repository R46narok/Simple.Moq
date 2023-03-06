using Microsoft.AspNetCore.Mvc;
using SimpleMoq.Api.Data.Entities;
using SimpleMoq.Api.Dto;
using SimpleMoq.Api.Services;

namespace SimpleMoq.Api.Controllers;

[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service = service;
    }

    [HttpGet("customer")]
    public async Task<IActionResult> GetAllCustomersAsync()
    {
        return Ok(await _service.GetAllCustomersAsync());
    }

    [HttpGet("customer/{id}")]
    public async Task<IActionResult> GetCustomerByIdAsync([FromRoute] Guid id)
    {
        var customer = await _service.GetCustomerByIdAsync(id);
        return customer is not null ? Ok(customer) : NotFound();
    }

    [HttpPost("customer")]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerDto dto)
    {
        var result = await _service.CreateCustomerAsync(dto);
        return Ok(result);
    }

    [HttpPatch("customer")]
    public async Task<IActionResult> UpdateCustomerAsync([FromBody] Customer customer)
    {
        var result = await _service.UpdateCustomerAsync(customer);
        return result is not null ? Ok(result) : NotFound();
    }


    [HttpDelete("customer/{id}")]
    public async Task<IActionResult> DeleteCustomerAsync([FromRoute] Guid id)
    {
        var result = await _service.DeleteCustomerAsync(id);
        return result is not null ? Ok(result) : NotFound();
    }
    
}