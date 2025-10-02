using Bookings.Entity;
using Bookings.Services;
using Microsoft.EntityFrameworkCore;
using Rooms;
using Rooms.Infrastructure;
using Shared.Interfaces;
using Shared.Interfaces.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Microservice specific
builder.Services.AddDbContext<BookingsDbContext>((options) => {

options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IRepository<Booking>, Repository<Booking>>((services) => {
    return new Repository<Booking>(services.GetRequiredService<BookingsDbContext>());
});
builder.Services.AddScoped<ISimpleService<Booking>, Service>();


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
