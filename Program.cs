using Database;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services;
using Microsoft.OpenApi.Models;
using Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();   
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Controllers", Version = "v1" });
});
builder.Services.AddDbContext<MovieAppDbContext>(options =>
{
    options.UseNpgsql("Host=db;Port=5432;Database=movie-app-db;Username=postgres;Password=postgres;",
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
builder.Services.AddScoped<IMovieListRepository, MovieListRepositoryImpl>();
builder.Services.AddScoped<IMovieListService, MovieListServiceImpl>();

// Add logging and other services as needed...
var secretKeyConfiguration = builder.Configuration.GetSection("AppSettings:Token:SecretKey");

if (secretKeyConfiguration.Exists() && !string.IsNullOrEmpty(secretKeyConfiguration.Value))
{
    var secretKey = Encoding.UTF8.GetBytes(secretKeyConfiguration.Value);

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateLifetime = true
            };
        });
}
else
{
    // Handle the case where the configuration key is not found or is empty
    Console.WriteLine("SecretKey configuration key is missing or empty.");
}
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Controllers"));
}
app.UsePathBase("/api");
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.MapControllers();
app.Run();
