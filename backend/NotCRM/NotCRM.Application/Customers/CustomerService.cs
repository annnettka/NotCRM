using NotCRM.Application.Common.Interfaces;
using NotCRM.Domain.Entities;

namespace NotCRM.Application.Customers;

public class CustomerService
{
    // Repository Customer.
    private readonly ICustomerRepository _customerRepository;

    // Нам також потрібен BusinessRepository,
    // щоб перед створенням Customer перевірити:
    //
    // чи існує Business взагалі.
    private readonly IBusinessRepository _businessRepository;

    public CustomerService(
        ICustomerRepository customerRepository,
        IBusinessRepository businessRepository)
    {
        _customerRepository = customerRepository;
        _businessRepository = businessRepository;
    }

    public async Task<CustomerDto?> CreateAsync(
        Guid businessId,
        CreateCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        // Спочатку перевіряємо,
        // чи існує Business.
        var business =
            await _businessRepository.GetByIdAsync(
                businessId,
                cancellationToken);

        // Якщо такого Business нема,
        // Customer створювати не можна.
        if (business is null)
        {
            return null;
        }

        // Тепер створюємо Domain Entity.
        //
        // Сам Customer перевірить:
        // - BusinessId
        // - Name
        var customer = new Customer(
            businessId,
            request.Name,
            request.Phone,
            request.Email);

        // Говоримо repository:
        // додай Customer.
        await _customerRepository.AddAsync(
            customer,
            cancellationToken);

        // Фізично зберігаємо в PostgreSQL.
        await _customerRepository.SaveChangesAsync(
            cancellationToken);

        // Entity напряму назовні не віддаємо.
        // Перетворюємо її в DTO.
        return ToDto(customer);
    }

    public async Task<List<CustomerDto>> GetAllAsync(
        Guid businessId,
        CancellationToken cancellationToken = default)
    {
        // Repository повертає List<Customer>.
        var customers =
            await _customerRepository
                .GetAllByBusinessIdAsync(
                    businessId,
                    cancellationToken);

        // Кожну Entity перетворюємо
        // в DTO для API/frontend.
        return customers
            .Select(ToDto)
            .ToList();
    }

    public async Task<CustomerDto?> GetByIdAsync(
        Guid businessId,
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var customer =
            await _customerRepository.GetByIdAsync(
                businessId,
                id,
                cancellationToken);

        // Якщо нема Customer:
        // повертаємо null.
        if (customer is null)
        {
            return null;
        }

        return ToDto(customer);
    }

    private static CustomerDto ToDto(Customer customer)
    {
        return new CustomerDto(
            customer.Id,
            customer.BusinessId,
            customer.Name,
            customer.Phone,
            customer.Email,
            customer.CreatedAt);
    }
}