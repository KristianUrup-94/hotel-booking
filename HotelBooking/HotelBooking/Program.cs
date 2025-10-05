using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rooms;
using Rooms.Entity;
using Rooms.Infrastructure;
using Rooms.Services;
using Shared.Interfaces;
using Shared.Interfaces.BaseClasses;
using Shared.Interfaces.Implementation;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Microservice specific
builder.Services.AddDbContext<RoomsDbContext>((options) => {
    
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
builder.Services.AddScoped<IRepository<Room>, Repository<Room>>((services) => {
    return new Repository<Room>(services.GetRequiredService<RoomsDbContext>());
});
builder.Services.AddScoped<ISimpleService<Room>, Service>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        RoomsDbContext context = scope.ServiceProvider.GetRequiredService<RoomsDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.MockData();
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
