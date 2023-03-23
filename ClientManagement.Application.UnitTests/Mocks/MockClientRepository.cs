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
            
            mockRepo.Setup(r => r.IsClientUnique(It.IsAny<string>()))
                .ReturnsAsync((string name) => { 
                    return !clients.Any(q => q.Name == name);
                });

            return mockRepo;
        }
    }
}
