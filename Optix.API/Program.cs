using Microsoft.EntityFrameworkCore;
using Optix.Repository.Contexts;
using Optix.Repository.Seeding;

var builder = WebApplication.CreateBuilder(args);

// set up SQLite instance
var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

// Add services to the container.
builder.Services.AddDbContext<MovieDbContext>(
    options => options.UseSqlite($"Data Source={Path.Join(path, "mymovie.db")}")
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
    );

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

    using var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;
    var dbContext = services.GetService<MovieDbContext>();

    if (dbContext != null)
        DataSeeder.Seed(dbContext);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
