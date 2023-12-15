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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200", "https://aitanarus.github.io/client-movie-app-devops", "https://aitanarus.github.io")
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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

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
}, ServiceLifetime.Scoped);

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
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Controllers"));
}
app.UsePathBase("/api");
app.UseCors("AllowSpecificOrigin");
app.MapControllers();
app.Run();
