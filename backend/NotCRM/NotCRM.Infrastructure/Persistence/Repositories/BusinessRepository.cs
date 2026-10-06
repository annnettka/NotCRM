using Microsoft.EntityFrameworkCore;
using NotCRM.Application.Common.Interfaces;
using NotCRM.Domain.Entities;

namespace NotCRM.Infrastructure.Persistence.Repositories;

public class BusinessRepository : IBusinessRepository
{
    private readonly AppDbContext _dbContext;

    public BusinessRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Business>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Businesses
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Business?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Businesses
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task AddAsync(
        Business business,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Businesses
            .AddAsync(business, cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}