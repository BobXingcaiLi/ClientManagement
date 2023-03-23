using MediatR;

namespace ClientManagement.Application.Features.Client.Commands.CreateClient;

public class CreateClientCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
}
