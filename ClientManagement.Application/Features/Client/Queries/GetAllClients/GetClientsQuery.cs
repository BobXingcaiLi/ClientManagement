using MediatR;

namespace ClientManagement.Application.Features.Client.Queries.GetAllClients;

public record GetClientsQuery : IRequest<List<ClientDto>>;
