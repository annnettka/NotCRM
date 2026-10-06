using NotCRM.Application.Common.Interfaces;
using NotCRM.Domain.Entities;

namespace NotCRM.Application.Businesses;

public class BusinessService
{
    private readonly IBusinessRepository _businessRepository;

    public BusinessService(
        IBusinessRepository businessRepository)
    {
        _businessRepository = businessRepository;
    }

    public async Task<BusinessDto> CreateAsync(
        CreateBusinessRequest request,
        CancellationToken cancellationToken = default)
    {
        var business = new Business(
            request.Name,
            request.Description);

        await _businessRepository.AddAsync(
            business,
            cancellationToken);

        await _businessRepository.SaveChangesAsync(
            cancellationToken);

        return ToDto(business);
    }

    public async Task<List<BusinessDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var businesses =
            await _businessRepository.GetAllAsync(
                cancellationToken);

        return businesses
            .Select(ToDto)
            .ToList();
    }

    public async Task<BusinessDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var business =
            await _businessRepository.GetByIdAsync(
                id,
                cancellationToken);

        return business is null
            ? null
            : ToDto(business);
    }

    private static BusinessDto ToDto(Business business)
    {
        return new BusinessDto(
            business.Id,
            business.Name,
            business.Description,
            business.CreatedAt);
    }
}