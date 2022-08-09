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
    //public Friends AddFriend(int requestedId, int addedId);
    public List<Friends> GetAllRelationships();
    public List<Friends> ViewAllFriends(int userIdToLookup);
    public List<Friends> ViewRelationShipsByStatus(string status);
    //public User EditFriendShip(string username);
    public List<Friends> CheckRelationshipStatusByUsername(string username, string status);
    public List<Friends> CheckRelationshipStatusByUserId(int userId, string status);

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


public interface IResourceGen
{
    public bool AddGold(User User, int Amount);
    public bool AddEgg(User User, int Amount);
    public bool AddFood(User User, int Amount, FoodStat Food2Add);
}
/* Currently unnecessary code
public interface ISpeciesDAO
{
    public Species GenerateBaseStats(int creatureId);
}
*/ 

public interface ILikeIt //I hate this (jkjk i love this but I thought the joke was funny XDXD)
{
    public bool RemoveLikes(int UserID, int PostID);
    public bool AddLikes(int UserID, int PostID);
    public int LikeCount(int PostID);
}