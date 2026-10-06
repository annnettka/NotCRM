using Microsoft.AspNetCore.Mvc;
using NotCRM.Application.Businesses;

namespace NotCRM.Api.Controllers;

[ApiController]
[Route("api/businesses")]
public class BusinessesController : ControllerBase
{
    private readonly BusinessService _businessService;

    public BusinessesController(
        BusinessService businessService)
    {
        _businessService = businessService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BusinessDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var businesses =
            await _businessService.GetAllAsync(cancellationToken);

        return Ok(businesses);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BusinessDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var business =
            await _businessService.GetByIdAsync(
                id,
                cancellationToken);

        if (business is null)
            return NotFound();

        return Ok(business);
    }

    [HttpPost]
    public async Task<ActionResult<BusinessDto>> Create(
        CreateBusinessRequest request,
        CancellationToken cancellationToken)
    {
        var business =
            await _businessService.CreateAsync(
                request,
                cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = business.Id },
            business);
    }
}