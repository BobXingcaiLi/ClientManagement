using AutoMapper;
using ClientManagement.Application.Contracts.Persistence;
using MediatR;

namespace ClientManagement.Application.Features.Client.Queries.GetAllClients;

public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, List<ClientDto>>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public GetClientsQueryHandler(IMapper mapper, IClientRepository clientRepository)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<List<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var clients = await _clientRepository.GetAsync();

        // convert data objects to DTO objects
        var data = _mapper.Map<List<ClientDto>>(clients);

        // return list of DTO object
        return data;
    }
}
