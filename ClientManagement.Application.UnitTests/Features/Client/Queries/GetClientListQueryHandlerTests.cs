using AutoMapper;
using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Application.Features.Client.Queries.GetAllClients;
using ClientManagement.Application.MappingProfiles;
using ClientManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace ClientManagement.Application.UnitTests.Features.Client.Queries
{
    public class GetClientListQueryHandlerTests
    {
        private readonly Mock<IClientRepository> _mockRepo;
        private IMapper _mapper;

        public GetClientListQueryHandlerTests()
        {
            _mockRepo = MockClientRepository.GetMockClientRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ClientProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetClientListTest()
        {
            var handler = new GetClientsQueryHandler(_mapper, _mockRepo.Object);

            var result = await handler.Handle(new GetClientsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<ClientDto>>();
            result.Count.ShouldBe(3);
        }
    }
}
