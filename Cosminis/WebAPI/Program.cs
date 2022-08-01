using DataAccess.Entities;
using CustomExceptions;
using Models;
using System;
using System.Data;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<wearelosingsteamContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString()));
builder.Services.AddScoped<User>();
builder.Services.AddScoped<UserRepo>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapPost("/createUser", (User user, UserRepo repo) => 
{
	return repo.CreateUser(user);
});

app.Run();
