using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;
using Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<wearelosingsteamContext>(options => options.UseSqlServer());
builder.Services.AddScoped<ICompanionDAO, CompanionRepo>();
builder.Services.AddScoped<IFriendsDAO, FriendsRepo>();
builder.Services.AddScoped<IUserDAO, UserRepo>();
builder.Services.AddScoped<IPostDAO, PostRepo>();
builder.Services.AddScoped<IResourceGen, ResourceRepo>();
builder.Services.AddScoped<ILikeIt, LikeRepo>();

builder.Services.AddScoped<CompanionServices>();
builder.Services.AddScoped<FriendServices>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<PostServices>();
builder.Services.AddScoped<LikeServices>();

builder.Services.AddScoped<CompanionController>();
builder.Services.AddScoped<FriendsController>();
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<PostController>();
builder.Services.AddScoped<LikeController>();

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

//this is a route parameter
app.MapGet("/postsBy/{userId}", (int userId, PostController controller) => 
{
	return controller.GetPostsByUserId(userId);
});

app.MapGet("/postsByUser/{username}", (string username, PostController controller) => 
{
	return controller.GetPostsByUsername(username);
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

app.MapGet("/GetAllCompanions", (CompanionController CompControl) => 
{
	return CompControl.GetAllCompanions();
});

app.MapGet("/companions/SearchByCompanionId", (int companionId, CompanionController CompControl) => 
{
	return CompControl.SearchForCompanionById(companionId);
});

app.MapGet("/companions/SearchByUserId", (int userId, CompanionController CompControl) => 
{
	return CompControl.SearchForCompanionByUserId(userId);
});

app.MapPost("/companions/Nickname", (int companionId, string? nickname, CompanionController CompControl) => 
{
	return CompControl.NicknameCompanion(companionId, nickname);
});

app.MapPost("/companions/GenerateCompanion", (string username, CompanionController CompControl) => 
{
	return CompControl.GenerateCompanion(username);
});

app.MapGet("/FriendsList", (int userIdToLookup, FriendsController FriendsControl) => 
{
	return FriendsControl.ViewAllFriends(userIdToLookup);
});

app.MapGet("/ViewAllRelationships", (FriendsController FriendsControl) => 
{
	return FriendsControl.ViewAllRelationships();
});

app.MapGet("/ViewRelationshipsByStatus", (string status, FriendsController FriendsControl) => 
{
	return FriendsControl.ViewRelationshipsByStatus(status);
});

app.MapPost("/Liking", (int UserID, int PostID, LikeController _LikeCon) => 
{
	return _LikeCon.AddLikes(UserID,PostID);
});

app.MapPut("/Unliking", (int UserID, int PostID, LikeController _LikeCon) => 
{
	return _LikeCon.RemoveLikes(UserID,PostID);
});

app.MapGet("/Checking", (int PostID, LikeController _LikeCon) => 
{
	return _LikeCon.LikeCount(PostID);
});

app.Run();
