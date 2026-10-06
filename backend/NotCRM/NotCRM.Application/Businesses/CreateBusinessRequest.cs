namespace NotCRM.Application.Businesses;

public record CreateBusinessRequest(
    string Name,
    string? Description);