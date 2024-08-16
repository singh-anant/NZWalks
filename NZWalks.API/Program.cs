using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;

var builder = WebApplication.CreateBuilder(args);

/*
Dependency Injection (DI) is a design pattern used in software development to manage and inject dependencies into classes.
It’s a way to achieve Inversion of Control (IoC) between classes and their dependencies, meaning that instead of the class creating its dependencies,
they are passed to the class from the outside.
*/

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Here used a dependency injection.....
builder.Services.AddDbContext<NZWalksDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"))
);

//then EF Migration.

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use routing and map controllers
app.UseRouting();
app.UseAuthorization();

//just added this to add the controller....
app.MapControllers();

app.Run();
