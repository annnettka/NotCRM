namespace NotCRM.Application.Customers;

// DTO — те, що ми повертаємо frontend.
public record CustomerDto(
    Guid Id,
    Guid BusinessId,
    string Name,
    string? Phone,
    string? Email,
    DateTime CreatedAt);