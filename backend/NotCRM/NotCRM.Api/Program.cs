using NotCRM.Infrastructure;
using NotCRM.Application.Businesses;
using NotCRM.Application.Customers;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' was not found.");

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddScoped<BusinessService>();
builder.Services.AddScoped<CustomerService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //допомагає зібрати інформацію про endpoints.
builder.Services.AddSwaggerGen(); //реєструє генератор Swagger

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())//Swagger запускаємо тільки коли проєкт працює в Development environment.
{
    app.UseSwagger();//бере цей JSON і малює нам сторінку з: get post etc
    app.UseSwaggerUI(); //Тобто Swagger UI — фактично frontend для тестування нашого backend API.
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();//«Знайди всі класи з [ApiController] і підключи їх маршрути».

app.Run();
