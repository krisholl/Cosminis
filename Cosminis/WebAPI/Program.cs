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
builder.Services.AddScoped<ICommentDAO, CommentRepo>();
builder.Services.AddScoped<IResourceGen, ResourceRepo>();
builder.Services.AddScoped<ILikeIt, LikeRepo>();
builder.Services.AddScoped<Interactions,InteractionRepo>();

builder.Services.AddScoped<ResourceServices>();
builder.Services.AddScoped<CompanionServices>();
builder.Services.AddScoped<FriendServices>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<PostServicces>();
builder.Services.AddScoped<CommentServices>();
builder.Services.AddScoped<LikeServices>();
builder.Services.AddScoped<InteractionService>();

builder.Services.AddScoped<ResourceController>();
builder.Services.AddScoped<CompanionController>();
builder.Services.AddScoped<FriendsController>();
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<PostController>();
builder.Services.AddScoped<CommentController>();
builder.Services.AddScoped<LikeController>();
builder.Services.AddScoped<Interactroller>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Welcome to Cosminis!");

app.MapPut("/interactions/ModifyHunger", (int companionID, Interactroller Interactroller) => 
{
	return Interactroller.DecrementCompanionHungerValue(companionID);
});

app.MapGet("/interactions/Talk", (int companionID, Interactroller Interactroller) => 
{
	return Interactroller.PullConvo(companionID);
});

app.MapGet("/interactions/Feed", (int feederID, int companionID, int foodID, Interactroller Interactroller) => //this end point does not work yet
{
	return Interactroller.FeedCompanion(feederID, companionID, foodID);
});

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

app.MapGet("/foodsUnder/{userId}", (int userId, ResourceController controller) => 
{
	return controller.GetFoodInventoryByUserId(userId);
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

app.MapPost("/submitPost", (string Content, int PosterID, PostController controller) => 
{
	return controller.SubmitPostResourceGen(Content, PosterID);
});

app.MapPost("/submitComment", (int commenterID, int postsID, string content, CommentController controller) =>
{
	return controller.SubmitCommentResourceGen(commenterID, postsID, content);
});

app.MapGet("/companions/GetAll", (CompanionController CompControl) => 
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

app.MapGet("/Friends/FriendsList", (int userIdToLookup, FriendsController FriendsControl) => 
{
	return FriendsControl.ViewAllFriends(userIdToLookup);
});

app.MapGet("/Friends/ViewAllRelationships", (FriendsController FriendsControl) => 
{
	return FriendsControl.ViewAllRelationships();
});

app.MapGet("/Friends/SearchByRelationshipId", (int relationshipId, FriendsController FriendsControl) => 
{
	return FriendsControl.SearchByRelationshipId(relationshipId);
});

app.MapGet("/Friends/FriendsByUserIds", (int searchingUserId, int user2BeSearchedFor, FriendsController FriendsControl) => 
{
	return FriendsControl.FriendsByUserIds(searchingUserId, user2BeSearchedFor);
});

app.MapGet("/Friends/ViewAllRelationshipsByStatus", (string status, FriendsController FriendsControl) => 
{
	return FriendsControl.ViewRelationshipsByStatus(status);
});

app.MapGet("/Friends/RelationshipStatusByUserId", (int searchingId, string status, FriendsController FriendsControl) => 
{
	return FriendsControl.CheckRelationshipStatusByUserId(searchingId, status);
});

app.MapGet("/Friends/RelationshipStatusByUsername", (string username, string status, FriendsController FriendsControl) => 
{
	return FriendsControl.CheckRelationshipStatusByUsername(username, status);
});

app.MapPut("/Friends/EditFriendshipStatus", (int editingUserID, int user2BeEdited, string status, FriendsController FriendsControl) => 
{
	return FriendsControl.EditStatus(editingUserID, user2BeEdited, status);
});

app.MapPost("/Friends/AddFriendByUserId", (int requesterId, int addedId, FriendsController FriendsControl) => 
{
	return FriendsControl.AddFriendByUserId(requesterId, addedId);
});

app.MapPost("/Friends/AddFriendByUsername", (string requesterUsername, string addedUsername, FriendsController FriendsControl) => 
{
	return FriendsControl.AddFriendByUsername(requesterUsername, addedUsername);
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
