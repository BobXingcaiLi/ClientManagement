using AutoMapper;
using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Application.Exceptions;
using ClientManagement.Application.Features.Client.Commands.CreateClient;
using ClientManagement.Application.Features.Client.Commands.DeleteClient;
using ClientManagement.Application.Features.Client.Commands.UpdateClient;
using ClientManagement.Application.Features.Client.Queries.GetAllClients;
using ClientManagement.Application.MappingProfiles;
using ClientManagement.Application.UnitTests.Mocks;
using ClientManagement.Domain;
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
        public async Task Handle_ValidClient_ClientUpdated()
        {
            var handler = new UpdateClientCommandHandler(_mapper, _mockRepo.Object);

            await handler.Handle(new UpdateClientCommand() { Id = 1, Name = "Alice ABC", EmailAddress = "AliceABC@Test.com"
            }, CancellationToken.None);

            var client = await _mockRepo.Object.GetByIdAsync(1);
            client.Name.ShouldBe("Alice ABC");
            client.EmailAddress.ShouldBe("AliceABC@Test.com");
        }

        [Fact]
        public async Task Handle_InvalidClientByIdNotAvailable_ThrowBadRequestException()
        {
            var handler = new UpdateClientCommandHandler(_mapper, _mockRepo.Object);

            Should.Throw<BadRequestException>(async () => await handler.Handle(new UpdateClientCommand()
            {
                Name = "Alice ABC",
                EmailAddress = "AliceABC@Test.com"
            }, CancellationToken.None))
            .Message.ShouldBe("Invalid Client");
        }

        [Fact]
        public async Task Handle_InvalidClientByNameNotAvailable_ThrowBadRequestException()
        {
            var handler = new UpdateClientCommandHandler(_mapper, _mockRepo.Object);

            Should.Throw<BadRequestException>(async () => await handler.Handle(new UpdateClientCommand()
            {
                Id = 1,
                Name = string.Empty,
                EmailAddress = "AliceABC@Test.com"
            }, CancellationToken.None))
            .Message.ShouldBe("Invalid Client");
        }

        [Fact]
        public async Task Handle_InvalidClientByNameTooLong_ThrowBadRequestException()
        {
            var handler = new UpdateClientCommandHandler(_mapper, _mockRepo.Object);

            Should.Throw<BadRequestException>(async () => await handler.Handle(new UpdateClientCommand()
            {
                Id = 1,
                Name = "12345678901234567890123456789012345678901234567890123456789012345678901234567890",
                EmailAddress = "AliceABC@Test.com"
            }, CancellationToken.None))
            .Message.ShouldBe("Invalid Client");
        }

        [Fact]
        public async Task Handle_InvalidClientByInvalidEmail_ThrowBadRequestException()
        {
            var handler = new UpdateClientCommandHandler(_mapper, _mockRepo.Object);

            Should.Throw<BadRequestException>(async () => await handler.Handle(new UpdateClientCommand()
            {
                Id = 1,
                Name = "Alice A",
                EmailAddress = "AliceABCTest.com"
            }, CancellationToken.None))
            .Message.ShouldBe("Invalid Client");
        }
    }
}
