using Models;
using System.Data.SqlClient;
using DataAccess.Entities;

namespace DataAccess;
/*
public interface ICompanionDAO
{
    Companion GenerateCompanion(Companion newCompanion);
}
*/

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