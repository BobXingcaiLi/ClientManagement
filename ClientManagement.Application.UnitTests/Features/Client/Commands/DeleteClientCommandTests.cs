using AutoMapper;
using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Application.Exceptions;
using ClientManagement.Application.Features.Client.Commands.CreateClient;
using ClientManagement.Application.Features.Client.Commands.DeleteClient;
using ClientManagement.Application.Features.Client.Queries.GetAllClients;
using ClientManagement.Application.Features.Client.Queries.GetClientDetail;
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
    public class DeleteClientCommandTests
    {
        private readonly IMapper _mapper;
        private Mock<IClientRepository> _mockRepo;

        public DeleteClientCommandTests()
        {
            _mockRepo = MockClientRepository.GetMockClientRepository();

            //var mapperConfig = new MapperConfiguration(c =>
            //{
            //    c.AddProfile<ClientProfile>();
            //});

            //_mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidClient_ClientDeleted()
        {
            var handler = new DeleteClientCommandHandler(_mockRepo.Object);

            await handler.Handle(new DeleteClientCommand() { Id = 1 }, CancellationToken.None);

            var clients = await _mockRepo.Object.GetAsync();
            clients.Count.ShouldBe(2);
            clients.Any(c => c.Id == 1).ShouldBe(false);
        }

        [Fact]
        public void Handle_InValidClient_ThrowNotFoundException()
        {
            var handler = new DeleteClientCommandHandler(_mockRepo.Object);

            Should.Throw<NotFoundException>(async () => await handler.Handle(new DeleteClientCommand()
            {
                Id = 10
            }, CancellationToken.None))
            .Message.ShouldBe("clientToDelete (10) was not found");
        }
    }
}
