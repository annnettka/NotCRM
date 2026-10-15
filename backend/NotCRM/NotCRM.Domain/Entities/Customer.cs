namespace NotCRM.Domain.Entities;

public class Customer
{
    // EF Core потрібен порожній конструктор,
    // щоб він міг створити Customer,
    // коли читає запис із бази даних.
    //
    // private означає, що звичайний код
    // не може створити Customer без наших правил.
    private Customer()
    {
    }

    // Це нормальний конструктор, через який
    // наш Application буде створювати нового клієнта.
    public Customer(
        Guid businessId,
        string name,
        string? phone = null,
        string? email = null)
    {
        // Guid.Empty означає фактично "ID не заданий".
        // Customer без Business у нашій системі
        // існувати не повинен.
        if (businessId == Guid.Empty)
        {
            throw new ArgumentException(
                "Business id is required.",
                nameof(businessId));
        }

        // Клієнт без імені теж не має сенсу.
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Customer name is required.",
                nameof(name));
        }

        // Створюємо унікальний ID клієнта.
        Id = Guid.NewGuid();

        // Запам'ятовуємо, якому Business
        // належить цей Customer.
        BusinessId = businessId;

        // Trim прибирає пробіли на початку та в кінці.
        Name = name.Trim();

        // Якщо phone пустий або пробіли,
        // зберігаємо null.
        Phone = string.IsNullOrWhiteSpace(phone)
            ? null
            : phone.Trim();

        // Аналогічно для Email.
        Email = string.IsNullOrWhiteSpace(email)
            ? null
            : email.Trim();

        // Час створення клієнта.
        CreatedAt = DateTime.UtcNow;
    }

    // Унікальний ID Customer.
    public Guid Id { get; private set; }

    // Foreign Key на Business.
    //
    // Саме завдяки цьому ми знаємо,
    // якому бізнесу належить клієнт.
    public Guid BusinessId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? Phone { get; private set; }

    public string? Email { get; private set; }

    public DateTime CreatedAt { get; private set; }
}