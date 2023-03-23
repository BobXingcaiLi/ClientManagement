using ClientManagement.Application.Contracts.Persistence;
using ClientManagement.Persistence.DatabaseContext;
using ClientManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ClientDatabaseContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("ClientDatabaseConnectionString"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IClientRepository, ClientRepository>();

        return services;
    }
}