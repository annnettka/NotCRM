using Microsoft.EntityFrameworkCore;
using NotCRM.Application.Common.Interfaces;
using NotCRM.Domain.Entities;

namespace NotCRM.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    // Через DbContext ми працюємо
    // з PostgreSQL через EF Core.
    private readonly AppDbContext _dbContext;

    // Dependency Injection передасть сюди
    // готовий AppDbContext.
    public CustomerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Customer>> GetAllByBusinessIdAsync(
        Guid businessId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers

            // Нам потрібні Customer
            // тільки конкретного Business.
            .Where(x => x.BusinessId == businessId)

            // Ми лише читаємо дані.
            // EF не треба слідкувати за змінами.
            //
            // Це трохи швидше та економніше.
            .AsNoTracking()

            // Сортуємо за ім'ям.
            .OrderBy(x => x.Name)

            // Виконуємо SQL запит.
            .ToListAsync(cancellationToken);
    }

    public async Task<Customer?> GetByIdAsync(
        Guid businessId,
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers
            .AsNoTracking()

            // Customer має одночасно:
            //
            // правильний Id
            // І
            // належати правильному Business.
            .FirstOrDefaultAsync(
                x => x.Id == id &&
                     x.BusinessId == businessId,
                cancellationToken);
    }

    public async Task AddAsync(
        Customer customer,
        CancellationToken cancellationToken = default)
    {
        // Поки що EF просто починає
        // відстежувати Customer як "Added".
        await _dbContext.Customers.AddAsync(
            customer,
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        // А ось тут EF реально виконує
        // INSERT/UPDATE/DELETE у PostgreSQL.
        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }
}