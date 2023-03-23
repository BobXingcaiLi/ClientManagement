using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Application.Exceptions;
using MediatR;

namespace ClientManagement.Application.Features.Client.Queries.GetClientDetail;

public class GetClientDetailQueryHandler : IRequestHandler<GetClientDetailQuery, ClientDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public GetClientDetailQueryHandler(IMapper mapper, IClientRepository clientRepository)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<ClientDetailDto> Handle(GetClientDetailQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var client = await _clientRepository.GetByIdAsync(request.Id);

        // verify that record exists
        if (client == null)
            throw new NotFoundException(nameof(client), request.Id);

        // convert data object to DTO object
        var data = _mapper.Map<ClientDetailDto>(client);

        // return DTO object
        return data;
    }
}
