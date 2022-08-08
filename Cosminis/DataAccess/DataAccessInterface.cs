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


public interface IUserDAO
{
    public User CreateUser(User user);
    public User GetUserByUserName(string username); 
}

public interface IPostDAO
{
    public Post SubmitPost(Post post);  
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

public interface ILikeIt
{
    public bool RemoveLikes(int UserID, int PostID);
    public bool AddLikes(int UserID, int PostID);
    public int LikeCount(int PostID);
}