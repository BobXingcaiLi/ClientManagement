using AutoMapper;
using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Application.Exceptions;
using ClientManagement.Application.Features.Client.Commands.CreateClient;
using ClientManagement.Application.MappingProfiles;
using ClientManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace ClientManagement.Application.UnitTests.Features.Client.Commands
{
    public class CreateClientCommandTests
    {
        private readonly IMapper _mapper;
        private Mock<IClientRepository> _mockRepo;

        public CreateClientCommandTests()
        {
            _mockRepo = MockClientRepository.GetMockClientRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ClientProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidClient_ClientAddedToRepo()
        {
            var handler = new CreateClientCommandHandler(_mapper, _mockRepo.Object);

            await handler.Handle(new CreateClientCommand() { Name = "Test1", EmailAddress = "Test1@test.com"
            }, CancellationToken.None);

            var clients = await _mockRepo.Object.GetAsync();
            clients.Count.ShouldBe(4);
        }

        [Fact]
        public void Handle_InvalidClient_ThrowBadReqeustException()
        {
            var handler = new CreateClientCommandHandler(_mapper, _mockRepo.Object);

            Should.Throw<BadRequestException>(async () => await handler.Handle(new CreateClientCommand()
                {
                    Name = "Alice A",
                    EmailAddress = "Alice@test.com"
                }, CancellationToken.None))
            .Message.ShouldBe("Invalid Client");
        }
    }
}
