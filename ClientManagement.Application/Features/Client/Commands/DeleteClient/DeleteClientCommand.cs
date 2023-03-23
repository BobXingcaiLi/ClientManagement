using MediatR;

namespace ClientManagement.Application.Features.Client.Commands.DeleteClient;

public class DeleteClientCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
