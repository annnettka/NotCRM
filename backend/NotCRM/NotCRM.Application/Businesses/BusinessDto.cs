namespace NotCRM.Application.Businesses;

public record BusinessDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt);