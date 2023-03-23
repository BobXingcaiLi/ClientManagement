using MediatR;

namespace ClientManagement.Application.Features.Client.Commands.UpdateClient;

public class UpdateClientCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
}
