using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Application.Exceptions;
using MediatR;

namespace ClientManagement.Application.Features.Client.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Unit>
{
    private readonly IClientRepository _clientRepository;

    public DeleteClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Unit> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
    {
        // retrieve domain entity object
        var clientToDelete = await _clientRepository.GetByIdAsync(command.Id);

        // verify that record exists
        if (clientToDelete == null)
            throw new NotFoundException(nameof(clientToDelete), command.Id);

        // remove from database
        await _clientRepository.DeleteAsync(clientToDelete);

        // retun record id
        return Unit.Value;
    }
}
