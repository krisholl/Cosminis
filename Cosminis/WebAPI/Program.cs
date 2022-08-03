using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;
using Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<wearelosingsteamContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString()));
builder.Services.AddScoped<IUserDAO, UserRepo>();
builder.Services.AddScoped<IPostDAO, PostRepo>();
builder.Services.AddScoped<IResourceGen, ResourceRepo>();

builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<PostServices>();

builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<PostController>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Welcome to Cosminis!");

//this is a query parameter, it has a parameter to actually be implemented
app.MapGet("/searchFriend", (string username, UserController controller) => 
{
	return controller.SearchFriend(username);
});

/*
app.MapPost("/createUser", (User user, IUserDAO repo) => //to be replaced by registerUser
{
	return repo.CreateUser(user);
});*/

app.MapPost("/submitPost", (Post post, PostController controller) => 
{
	return controller.SubmitPostResourceGen(post);
});

app.Run();