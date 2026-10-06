using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotCRM.Infrastructure.Persistence;
using NotCRM.Application.Common.Interfaces;
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

                //ЦЕЙ рядок означає якщо хтось попросить IBusinessRepository, дай йому BusinessRepository
                services.AddScoped<IBusinessRepository, BusinessRepository>();

        return services;
    }
}