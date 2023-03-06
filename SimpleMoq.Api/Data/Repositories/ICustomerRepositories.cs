using Microsoft.EntityFrameworkCore;
using SimpleMoq.Api.Common;
using SimpleMoq.Api.Data.Entities;
using SimpleMoq.Api.Data.Persistence;

namespace SimpleMoq.Api.Data.Repositories;

public interface ICustomerRepository : IRepository<Customer, Guid>
{
    
}

public class CustomerRepository : RepositoryBase<Customer, Guid>, ICustomerRepository
{
    public CustomerRepository(SimpleMoqDbContext context) : base(context)
    {
    }
}

