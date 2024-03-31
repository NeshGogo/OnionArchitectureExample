using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OnionArchitectureExample.Middlewares;
using Persistence;
using Persistence.Repositories;
using Services;
using Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// --> Register the controllers that are in the presentation class library (Presentation layer)
builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

// --> Registering the services Manager and Repository Manager
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddDbContextPool<RepositoryDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    opt.UseNpgsql(connectionString);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// --> Configuring API Doc
builder.Services.AddSwaggerGen(c => 
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1"}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
