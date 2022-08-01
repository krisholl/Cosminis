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

builder.Services.AddDbContext<wearelosingsteamContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Server=tcp:p2dbs.database.windows.net,1433;Initial Catalog=wearelosingsteam;Persist Security Info=False;User ID=wearelosingsteam;Password=weL0stSteam;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")));
builder.Services.AddScoped<User>();
builder.Services.AddScoped<UserRepo>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.Run();
