namespace NotCRM.Application.Customers;

// record тут використовується як простий
// контейнер даних.
//
// Він не містить бізнес-логіки.
//
// Це просто те, що прийде
// від frontend/API.
public record CreateCustomerRequest(
    string Name,
    string? Phone,
    string? Email);