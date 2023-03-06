using Bogus;
using FluentAssertions;
using Moq;
using SimpleMoq.Api.Data.Entities;
using SimpleMoq.Api.Data.Repositories;
using SimpleMoq.Api.Services;
using Xunit;

namespace SimpleMoq.Api.Tests;

public class CustomerServiceTests
{
    private readonly CustomerService _sut; // system under test
    private readonly Mock<ICustomerRepository> _customerRepoMock = new();
    private readonly Mock<ILoggingService> _loggingMock = new();
    private readonly Faker<Customer> _faker;

    public CustomerServiceTests()
    {
        _sut = new CustomerService(_customerRepoMock.Object, _loggingMock.Object);
        _faker = new Faker<Customer>()
            .RuleFor(x => x.Id, y => Guid.NewGuid())
            .RuleFor(x => x.FirstName, y => y.Name.FirstName())
            .RuleFor(x => x.LastName, y => y.Name.LastName())
            .RuleFor(x => x.Age, y => y.Random.Int(1, 99));
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCustomer_WhenCustomerExists()
    {
        var fake = _faker.Generate();

        _customerRepoMock
            .Setup(x => x.GetByIdAsync(fake.Id, false))
            .ReturnsAsync(fake);

        var customer = await _sut.GetCustomerByIdAsync(fake.Id);

        customer!.Id.Should().Be(fake.Id);
        customer.FirstName.Should().Be(fake.FirstName);
        customer.LastName.Should().Be(fake.LastName);
        customer.Age.Should().Be(fake.Age);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenCustomerDoesNotExists()
    {
        _customerRepoMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), false))
            .ReturnsAsync(() => null);

        var customer = await _sut.GetCustomerByIdAsync(Guid.NewGuid());
        customer.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnCustomer_WhenCustomerExists()
    {
        var fake = _faker.Generate();

        _customerRepoMock
            .Setup(x => x.GetByIdAsync(fake.Id, false))
            .ReturnsAsync(fake);

        _customerRepoMock
            .Setup(x => x.UpdateAsync(fake))
            .ReturnsAsync(fake);

        var customer = await _sut.UpdateCustomerAsync(fake);

        customer!.Id.Should().Be(fake.Id);
        customer.FirstName.Should().Be(fake.FirstName);
        customer.LastName.Should().Be(fake.LastName);
        customer.Age.Should().Be(fake.Age);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenCustomerDoesNotExists()
    {
        var fake = _faker.Generate();
        _customerRepoMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), false))
            .ReturnsAsync(() => null);

        var customer = await _sut.UpdateCustomerAsync(fake);
        customer.Should().BeNull();
    }
}