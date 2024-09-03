using Application.Exceptions;
using Application.Mappings;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Repositories;
using WebApiCleanArchitectureNet8;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


string oracleConnectionString = builder.Configuration.GetConnectionString("OracleDb"); //hac

// Add services to the container.
builder.Services.AddScoped<ICustomerRepository>(provider => new CustomerRepository(oracleConnectionString)); //hac
builder.Services.AddScoped<CustomerService>(); //hac


// Agregar AutoMapper al contenedor de servicios
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly); //hac


builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura el middleware y el pipeline de solicitudes
app.UseMiddleware<ExceptionHandlingMiddleware>(); //hac


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
