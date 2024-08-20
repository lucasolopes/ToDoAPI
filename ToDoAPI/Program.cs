using Domain.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnKanBan.Domain.Entities;
using OnKanBan.Persistence;
using Persistence.Repositories;
using Services;
using Services.Abstractions;
using Shared.Requests;
using ToDoAPI.Middleeare;
using Validators;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddScoped<IValidator<WhiteBoardRequest>, WhiteBoardValidator>();

builder.Services.AddDbContextPool<RepositoryDbContext>(b =>
{

    var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
    var dbName = Environment.GetEnvironmentVariable("DB_NAME");
    var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
    var connectionString = $"Server={dbHost};Database={dbName};User Id=sa;Password={dbPassword};";

    b.UseSqlServer(connectionString);
});




// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();
    db.Database.Migrate();
}


app.Run();
