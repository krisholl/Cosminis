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

builder.Services.AddDbContext<wearelosingsteamContext>(options => options.UseSqlServer());
builder.Services.AddScoped<CompanionController>();
builder.Services.AddScoped<CompanionServices>();
builder.Services.AddScoped<User>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<ICompanionDAO, CompanionRepo>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapGet("/GetAllCompanions", (CompanionController CompControl) => CompControl.GetAllCompanions());

app.MapGet("/companions/SearchByCompanionId", (int companionId, CompanionController CompControl) => CompControl.SearchForCompanionById(companionId));

app.MapGet("/companions/SearchByUserId", (int userId, CompanionController CompControl) => CompControl.SearchForCompanionByUserId(userId));

app.MapPost("/companions/Nickname", (int companionId, string? nickname, CompanionController CompControl) => CompControl.NicknameCompanion(companionId, nickname));

app.MapPost("/companions/GenerateCompanion", (string username, CompanionController CompControl) => CompControl.GenerateCompanion(username));

app.Run();
