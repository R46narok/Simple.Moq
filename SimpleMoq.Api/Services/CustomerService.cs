using SimpleMoq.Api.Data.Entities;
using SimpleMoq.Api.Data.Repositories;
using SimpleMoq.Api.Dto;

namespace SimpleMoq.Api.Services;

public interface ICustomerService
{
    public Task<Guid?> DeleteCustomerAsync(Guid id);
    public Task<Customer?> UpdateCustomerAsync(Customer customer);
    public Task<List<Customer>> GetAllCustomersAsync();
    public Task<Customer?> GetCustomerByIdAsync(Guid id);

    public Task<Customer> CreateCustomerAsync(CreateCustomerDto dto);

}

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly ILoggingService _loggingService;

    public CustomerService(ICustomerRepository repository, ILoggingService loggingService)
    {
        _repository = repository;
        _loggingService = loggingService;
    }

    public async Task<Customer> CreateCustomerAsync(CreateCustomerDto dto)
    {
        var customer = new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Age = dto.Age
        };
        
        var entity = await _repository.CreateAsync(customer);
        _loggingService.LogInformation("Created user with id {Id}", entity.Id);
        return entity;
    }

    public async Task<Customer?> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _repository.GetByIdAsync(id, false);
        return customer;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _repository.GetAll();
    }

    public async Task<Customer?> UpdateCustomerAsync(Customer customer)
    {
        if (await _repository.GetByIdAsync(customer.Id, false) is null)
            return null;
        
        await _repository.UpdateAsync(customer);
        _loggingService.LogInformation("Updated user with id {Id}", customer.Id);
        return customer;
    }
    
    public async Task<Guid?> DeleteCustomerAsync(Guid id)
    {
        if (await _repository.GetByIdAsync(id, false) is null)
            return null;
        
        await _repository.DeleteAsync(new Customer {Id = id});
        _loggingService.LogInformation("Deleted user with id {Id}", id);
        return id;
    }
}