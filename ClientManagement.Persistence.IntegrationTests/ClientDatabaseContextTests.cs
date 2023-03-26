using ClientManagement.Domain;
using ClientManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace ClientManagement.Persistence.IntegrationTests;


public class ClientDatabaseContextTests
{
    private ClientDatabaseContext _clientDatabaseContext;

    public ClientDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<ClientDatabaseContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _clientDatabaseContext = new ClientDatabaseContext(dbOptions);
        
    }

    [Fact]
    public async void Save_SetDateCreatedValue()
    {
        // Arrange
        var client = new Client
        {
            Id = 1,
            EmailAddress = "AliceABC@test.com",
            Name = "Alice ABC"
        };

        // Act
        await _clientDatabaseContext.Clients.AddAsync(client);
        await _clientDatabaseContext.SaveChangesAsync();

        // Assert
        client.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async void Save_SetDateModifiedValue()
    {
        // Arrange
        var client = new Client
        {
            Id = 1,
            EmailAddress = "AliceABC@test.com",
            Name = "Alice ABCD"
        };

        // Act
        await _clientDatabaseContext.Clients.AddAsync(client);
        await _clientDatabaseContext.SaveChangesAsync();

        // Assert
        client.DateModified.ShouldNotBeNull();
    }
}