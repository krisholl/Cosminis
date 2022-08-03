using Models;
using System.Data.SqlClient;
using DataAccess.Entities;

namespace DataAccess;

public interface ICompanionDAO
{
    public Companion GenerateCompanion(int userIdInput);
    public Companion SetCompanionMood(int companionId);
    public Companion SetCompanionNickname(int companionId, string? nickname);
    public List<Companion> GetAllCompanions();
    public Companion GetCompanionsByUser(int userId);
    public Companion GetCompanionByCompanionId(int companionId);
}


public interface IResourceGen
{
    bool AddGold(User User, int Amount);
    bool AddEgg(User User, int Amount);
    public bool AddFood(User User, int Amount, FoodStat Food2Add);
}

public interface ISpeciesDAO
{
    public Species GenerateBaseStats(int creatureId);
}