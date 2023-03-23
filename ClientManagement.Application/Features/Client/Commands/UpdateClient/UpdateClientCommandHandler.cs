using AutoMapper;
using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Application.Exceptions;
using MediatR;

namespace ClientManagement.Application.Features.Client.Commands.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandHandler(IMapper mapper, IClientRepository clientRepository)
    {
        _mapper = mapper;
        _clientRepository = clientRepository;
    }

    public async Task<Unit> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateClientCommandValidator(_clientRepository);
        var validationResult = await validator.ValidateAsync(command);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Client", validationResult);
        }

        // convert to domain entity object
        var clientToUpdate = _mapper.Map<Domain.Client>(command);

        // add to database
        await _clientRepository.UpdateAsync(clientToUpdate);

        // return Unit value
        return Unit.Value;
    }
}
