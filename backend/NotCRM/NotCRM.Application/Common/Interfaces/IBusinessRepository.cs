using NotCRM.Domain.Entities;

namespace NotCRM.Application.Common.Interfaces;

public interface IBusinessRepository
{
    Task<List<Business>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<Business?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Business business,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}