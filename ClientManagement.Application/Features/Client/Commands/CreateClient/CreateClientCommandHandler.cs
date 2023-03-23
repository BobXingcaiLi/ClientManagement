using AutoMapper;
using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Application.Exceptions;
using MediatR;

namespace ClientManagement.Application.Features.Client.Commands.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public CreateClientCommandHandler(IMapper mapper, IClientRepository clientRepository)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<int> Handle(CreateClientCommand command, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateClientCommandValidator(_clientRepository);
        var validationResult = await validator.ValidateAsync(command);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Client", validationResult);

        // convert to domain entity object
        var clientToCreate = _mapper.Map<Domain.Client>(command);

        // add to database
        await _clientRepository.CreateAsync(clientToCreate);

        // retun record id
        return clientToCreate.Id;
    }
}
