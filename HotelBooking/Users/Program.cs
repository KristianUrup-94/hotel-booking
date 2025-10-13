using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using Shared.Interfaces.Implementation;
using Shared.Interfaces.Services;
using Users.Entity;
using Users.Infrastructure;
using Users.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UsersDbContext>((options) => {

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IRepository<User>, Repository<User>>((services) => {
    return new Repository<User>(services.GetRequiredService<UsersDbContext>());
});
builder.Services.AddScoped<ISimpleService<User>, Service>();
builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "test",
        policy =>
        {
            policy.WithOrigins("http://localhost:44337",
                               "http://localhost:44336");
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        UsersDbContext context = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
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
