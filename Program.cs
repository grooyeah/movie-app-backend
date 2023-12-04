using Database;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services;
using Microsoft.OpenApi.Models;
using Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();   
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Controller", Version = "v1" });
});
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=movie-app-db;Username=postgres;Password=postgres;",
        builder => builder.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null
        )
    );
});
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IReviewRepository, ReviewRepositoryImpl>();
builder.Services.AddScoped<IProfileRepository, ProfileRepositoryImpl>();



builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IReviewService, ReviewServiceImpl>();
builder.Services.AddScoped<IProfileService, ProfileServiceImpl>();
builder.Services.AddScoped<IAuthService, AuthServiceImpl>();

// Add logging and other services as needed...

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Controller"));
}
app.UsePathBase("/api");
app.UseAuthorization();

app.MapControllers();

app.Run();
