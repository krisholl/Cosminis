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

public interface IResourceGen
{
    bool AddGold(User User, int Amount);
    bool AddEgg(User User, int Amount);
    public bool AddFood(User User, int Amount, FoodStat Food2Add);
}