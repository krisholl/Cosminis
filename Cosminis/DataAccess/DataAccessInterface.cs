using Models;
using System.Data.SqlClient;
using DataAccess.Entities;

namespace DataAccess;

public interface ICompanionDAO
{
    public Companion GenerateCompanion(int userIdInput);
    public string SetCompanionMood();
    public Companion SetCompanionNickname(int companionId, string? nickname);
    public List<Companion> GetAllCompanions();
    public List<Companion> GetCompanionByUser(int userId);
    public Companion GetCompanionByCompanionId(int companionId);
}

public interface IFriendsDAO
{
    public Friends AddFriendByUsername(string requesterUsername, string addedUsername);
    public Friends AddFriendByUserId(int requestedId, int addedId);
    public List<Friends> GetAllRelationships();
    public List<Friends> ViewAllFriends(int userIdToLookup);
    public List<Friends> ViewRelationShipsByStatus(string status);
    public Friends EditFriendship(int editingUserID, int user2BeEdited, string status);
    public List<Friends> CheckRelationshipStatusByUsername(string username, string status);
    public List<Friends> CheckRelationshipStatusByUserId(int userId, string status);
    public Friends FriendsByUserIds(int searchingUserId, int user2BeSearchedFor);
    public Friends SearchByRelationshipId(int relationshipId);
}

public interface IUserDAO
{
    public User CreateUser(User user);
    public User GetUserByUserName(string username);
    public User GetUserByUserId(int userId); 
}

public interface IPostDAO
{
    public Post SubmitPost(Post post); 
    public List<Post> GetPostsByUserId(int? userId);
    public List<Post> GetPostsByUsername(string username);
}

public interface ICommentDAO
{
    public Comment SubmitComment(Comment comment);
    public bool AddToPostOwner(Comment comment);
    public List<Comment> GetCommentsByPostId(int postId);
    public bool RemoveComment(int commentId);
}


public interface IResourceGen
{
    public bool AddGold(User User, int Amount);
    public bool AddEgg(User User, int Amount);
    public bool AddFood(User User, int Weight);
    public bool RemoveFood(int UserID, int FoodID);
}

public interface Interactions
{
    public bool SetCompanionMoodValue(int companionID, int amount);
    public bool SetCompanionHungerValue(int companionID, int amount);
    public bool RollCompanionEmotion(int companionID);
    public bool FeedCompanion(int feederID, int companionID, int foodID);
    public bool PetCompanion(int petterID, int companionID);
    public bool ShowCaseCompanion(int userID, int companionID);
}

public interface ILikeIt //I hate this (jkjk i love this but I thought the joke was funny XDXD)
{
    public bool RemoveLikes(int UserID, int PostID);
    public bool AddLikes(int UserID, int PostID);
    public int LikeCount(int PostID);
}