using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotCRM.Application.Common.Interfaces;
using NotCRM.Infrastructure.Persistence;
using NotCRM.Infrastructure.Persistence.Repositories;

namespace NotCRM.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IBusinessRepository, BusinessRepository>();

        services.AddScoped<ICustomerRepository,CustomerRepository>();

        return services;
    }
}