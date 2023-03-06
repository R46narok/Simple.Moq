using Microsoft.EntityFrameworkCore;
using SimpleMoq.Api.Data.Entities;

#nullable disable

namespace SimpleMoq.Api.Data.Persistence;

public class SimpleMoqDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public SimpleMoqDbContext(DbContextOptions<SimpleMoqDbContext> options) : base(options)
    {
        
    }

    public SimpleMoqDbContext()
    {
        
    }
}