using AutoMapper;
using ClientManagement.Application.Features.Client.Commands.CreateClient;
using ClientManagement.Application.Features.Client.Commands.UpdateClient;
using ClientManagement.Application.Features.Client.Queries.GetAllClients;
using ClientManagement.Application.Features.Client.Queries.GetClientDetail;
using ClientManagement.Domain;

namespace ClientManagement.Application.MappingProfiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientDto, Client>().ReverseMap();
        CreateMap<Client, ClientDetailDto>();
        CreateMap<CreateClientCommand, Client>();
        CreateMap<UpdateClientCommand, Client>();
    }
}
