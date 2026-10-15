using NotCRM.Domain.Entities;

namespace NotCRM.Application.Common.Interfaces;

public interface ICustomerRepository
{
    // Отримати всіх Customer
    // конкретного Business.
    Task<List<Customer>> GetAllByBusinessIdAsync(
        Guid businessId,
        CancellationToken cancellationToken = default);

    // Знайти одного Customer.
    //
    // Зверни увагу:
    // перевіряємо і id Customer,
    // і businessId.
    //
    // Це важливо для multi-tenancy.
    Task<Customer?> GetByIdAsync(
        Guid businessId,
        Guid id,
        CancellationToken cancellationToken = default);

    // Додати Customer до DbContext.
    Task AddAsync(
        Customer customer,
        CancellationToken cancellationToken = default);

    // Фізично зберегти зміни в PostgreSQL.
    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}