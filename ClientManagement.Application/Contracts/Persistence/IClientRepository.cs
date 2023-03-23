using ClientManagement.Domain;

namespace ClientManagement.Application.Contracts.Persistence;

public interface IClientRepository : IGenericRepository<Client>
{
    Task<bool> IsClientUnique(string name);
}
