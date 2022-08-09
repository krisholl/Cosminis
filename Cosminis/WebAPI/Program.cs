using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;
using Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<wearelosingsteamContext>(options => options.UseSqlServer("Server=tcp:p2dbs.database.windows.net,1433;Initial Catalog=wearelosingsteam;Persist Security Info=False;User ID=wearelosingsteam;Password=weL0stSteam;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
builder.Services.AddScoped<ICompanionDAO, CompanionRepo>();
builder.Services.AddScoped<IUserDAO, UserRepo>();
builder.Services.AddScoped<IPostDAO, PostRepo>();
builder.Services.AddScoped<ICommentDAO, CommentRepo>();
builder.Services.AddScoped<IResourceGen, ResourceRepo>();
builder.Services.AddScoped<ILikeIt, LikeRepo>();

builder.Services.AddScoped<CompanionServices>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<PostServices>();
builder.Services.AddScoped<CommentServices>();
builder.Services.AddScoped<LikeServices>();

builder.Services.AddScoped<CompanionController>();
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<PostController>();
builder.Services.AddScoped<CommentController>();
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

app.MapGet("/commentsUnder/{postId}", (int postId, CommentController controller) => 
{
	return controller.GetCommentsByPostId(postId);
});

app.MapDelete("/commentsBy/{commentId}", (int commentId, CommentController controller) => 
{
	return controller.RemoveComment(commentId);
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

app.MapPost("/submitComment", (Comment comment, CommentController controller) =>
{
	return controller.SubmitCommentResourceGen(comment);
});

app.MapGet("/GetAllCompanions", (CompanionController CompControl) => CompControl.GetAllCompanions());

app.MapGet("/companions/SearchByCompanionId", (int companionId, CompanionController CompControl) => CompControl.SearchForCompanionById(companionId));

app.MapGet("/companions/SearchByUserId", (int userId, CompanionController CompControl) => CompControl.SearchForCompanionByUserId(userId));

app.MapPost("/companions/Nickname", (int companionId, string? nickname, CompanionController CompControl) => CompControl.NicknameCompanion(companionId, nickname));

app.MapPost("/companions/GenerateCompanion", (string username, CompanionController CompControl) => CompControl.GenerateCompanion(username));

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
