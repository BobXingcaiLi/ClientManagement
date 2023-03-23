using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Domain;
using ClientManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ClientManagement.Persistence.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(ClientDatabaseContext context) : base(context)
    {
        
    }

    public async Task<bool> IsClientUnique(string name)
    {
        return await _context.Clients.AnyAsync(q => q.Name == name) == false;
    }
}
