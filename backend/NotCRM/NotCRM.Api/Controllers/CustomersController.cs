using Microsoft.AspNetCore.Mvc;
using NotCRM.Application.Customers;

namespace NotCRM.Api.Controllers;

[ApiController]

// Зверни увагу на URL:
//
// /api/businesses/{businessId}/customers
//
// Таким чином кожен Customer
// завжди знаходиться в контексті Business.
[Route("api/businesses/{businessId:guid}/customers")]
public class CustomersController : ControllerBase
{
    private readonly CustomerService _customerService;

    // ASP.NET DI автоматично передасть
    // сюди CustomerService.
    public CustomersController(
        CustomerService customerService)
    {
        _customerService = customerService;
    }

    // GET:
    //
    // /api/businesses/{businessId}/customers
    //
    // Отримуємо всіх клієнтів Business.
    [HttpGet]
    public async Task<ActionResult<List<CustomerDto>>> GetAll(
        Guid businessId,
        CancellationToken cancellationToken)
    {
        var customers =
            await _customerService.GetAllAsync(
                businessId,
                cancellationToken);

        // HTTP 200 OK.
        return Ok(customers);
    }

    // GET:
    //
    // /api/businesses/{businessId}/customers/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerDto>> GetById(
        Guid businessId,
        Guid id,
        CancellationToken cancellationToken)
    {
        var customer =
            await _customerService.GetByIdAsync(
                businessId,
                id,
                cancellationToken);

        // Якщо Customer не знайдено:
        // HTTP 404 Not Found.
        if (customer is null)
        {
            return NotFound();
        }

        // Інакше HTTP 200.
        return Ok(customer);
    }

    // POST:
    //
    // /api/businesses/{businessId}/customers
    [HttpPost]
    public async Task<ActionResult<CustomerDto>> Create(
        Guid businessId,
        CreateCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var customer =
            await _customerService.CreateAsync(
                businessId,
                request,
                cancellationToken);

        // Якщо Business не існує,
        // Service поверне null.
        if (customer is null)
        {
            return NotFound(
                $"Business '{businessId}' was not found.");
        }

        // 201 Created.
        //
        // CreatedAtAction ще й додає URL,
        // за яким можна отримати створеного Customer.
        return CreatedAtAction(
            nameof(GetById),
            new
            {
                businessId,
                id = customer.Id
            },
            customer);
    }
}