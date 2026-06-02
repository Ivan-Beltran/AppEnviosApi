using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
  


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("connection")
        )
    );

builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped < IUserRepository, UserRepository>();
builder.Services.AddScoped < IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<BranchService>();



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
