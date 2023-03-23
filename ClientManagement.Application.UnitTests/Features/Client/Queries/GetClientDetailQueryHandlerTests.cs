using AutoMapper;
using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Application.Exceptions;
using ClientManagement.Application.Features.Client.Commands.CreateClient;
using ClientManagement.Application.Features.Client.Queries.GetClientDetail;
using ClientManagement.Application.MappingProfiles;
using ClientManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace ClientManagement.Application.UnitTests.Features.Client.Queries
{
    public class GetClientDetailQueryHandlerTests
    {
        private readonly Mock<IClientRepository> _mockRepo;
        private IMapper _mapper;

        public GetClientDetailQueryHandlerTests()
        {
            _mockRepo = MockClientRepository.GetMockClientRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ClientProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetClientDetailTest_ValidClientId_ReturnValidClient()
        {
            var handler = new GetClientDetailQueryHandler(_mapper, _mockRepo.Object);

            var result = await handler.Handle(new GetClientDetailQuery(1), CancellationToken.None);

            result.ShouldBeOfType<ClientDetailDto>();
            result.Id.ShouldBe(1);
            result.Name.ShouldBe("Alice A");
            result.EmailAddress.ShouldBe("Alice@test.com");
        }

        [Fact]
        public void GetClientDetailTest_InvalidClientId_ThrowNotFoundException()
        {
            var handler = new GetClientDetailQueryHandler(_mapper, _mockRepo.Object);

            Should.Throw<NotFoundException>(async () => await handler.Handle(new GetClientDetailQuery(10), CancellationToken.None))
            .Message.ShouldBe("client (10) was not found");
        }
    }
}
