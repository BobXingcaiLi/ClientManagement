using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Domain;
using Moq;

namespace ClientManagement.Application.UnitTests.Mocks
{
    public class MockClientRepository
    {
        public static Mock<IClientRepository> GetMockClientRepository()
        {
            var clients = new List<Client>
            {
                new Client
                {
                    Id = 1,
                    EmailAddress = "Alice@test.com",
                    Name = "Alice A"
                },
                new Client
                {
                    Id = 2,
                    EmailAddress = "Bob@test.com",
                    Name = "Bob B"
                },
                new Client
                {
                    Id = 3,
                    EmailAddress = "Cathy@test.com",
                    Name = "Cathy C"
                }
            };

            var mockRepo = new Mock<IClientRepository>();

            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(clients);
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    return clients.FirstOrDefault(x => x.Id == id);
                });

            mockRepo.Setup(r => r.CreateAsync(It.IsAny<Client>()))
                .Returns((Client client) =>
                {
                    clients.Add(client);
                    return Task.CompletedTask;
                });

            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Client>()))
                .Returns((Client client) =>
                {
                    if (clients.Any(x => x.Id == client.Id))
                    {
                        clients.Remove(client);
                    }                    
                    return Task.CompletedTask;
                });

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Client>()))
                .Returns((Client client) =>
                {
                    var existingClient = clients.FirstOrDefault(x => x.Id == client.Id);
                    if (existingClient != null)
                    {
                        existingClient.Name = client.Name;
                        existingClient.EmailAddress = client.EmailAddress;
                    }
                    return Task.CompletedTask;
                });

            mockRepo.Setup(r => r.IsClientUnique(It.IsAny<string>()))
                .ReturnsAsync((string name) => { 
                    return !clients.Any(q => q.Name == name);
                });

            return mockRepo;
        }
    }
}
