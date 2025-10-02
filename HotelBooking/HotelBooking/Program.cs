using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rooms;
using Shared.Interfaces;
using Shared.Interfaces.BaseClasses;
using Shared.Interfaces.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RoomsDbContext>((options) => {
    
    options.UseSqlServer(@"Server=.\mssqllocaldb;Database=Test;ConnectRetryCount=0");
    });
builder.Services.AddScoped<IRepository<Room>, Repository<Room>>((services) => {
    return new Repository<Room>(services.GetRequiredService<RoomsDbContext>());
});
//builder.Services.AddScoped<ISimpleService<Room>, BaseService<Room>>();
builder.Services.AddScoped<ISimpleService<Room>, Service>();

var app = builder.Build();

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
