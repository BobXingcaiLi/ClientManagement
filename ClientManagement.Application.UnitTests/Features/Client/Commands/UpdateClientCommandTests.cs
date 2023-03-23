using AutoMapper;
using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Application.Features.Client.Commands.CreateClient;
using ClientManagement.Application.Features.Client.Queries.GetAllClients;
using ClientManagement.Application.MappingProfiles;
using ClientManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.Application.UnitTests.Features.Client.Commands
{
    public class UpdateClientCommandTests
    {
        private readonly IMapper _mapper;
        private Mock<IClientRepository> _mockRepo;

        public UpdateClientCommandTests()
        {
            _mockRepo = MockClientRepository.GetMockClientRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ClientProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidClient()
        {
            var handler = new CreateClientCommandHandler(_mapper, _mockRepo.Object);

            await handler.Handle(new CreateClientCommand() { Name = "Test1", EmailAddress = "Test1@test.com"
            }, CancellationToken.None);

            var Clients = await _mockRepo.Object.GetAsync();
            Clients.Count.ShouldBe(4);
        }
    }
}
