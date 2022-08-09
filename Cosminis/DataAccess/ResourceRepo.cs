using Models;
using System.Data.SqlClient;
using DataAccess.Entities;
using CustomExceptions;

namespace DataAccess;

public class ResourceRepo : IResourceGen
{
    private readonly wearelosingsteamContext _context;

    public ResourceRepo (wearelosingsteamContext context)
    {
        _context = context;
    }
    /*/// <summary>
    /// Add a certain amount of gold to a user
    /// </summary>
    /// <param name="User"></param>
    /// <param name="Amount"></param>
    /// <returns>1 if operation is successful, it will never return false at the moment</returns>
    /// <exception cref="ResourceNotFound">Occurs if no user exist matching the given User object</exception>*/
    public bool AddGold(User User, int Amount)
    {
        User User2Add2 = _context.Users.Find(User.UserId); //let us hope this works
        if(User2Add2 == null) //such user does not exist
        {
            throw new ResourceNotFound();
        }
        
        User2Add2.GoldCount = User2Add2.GoldCount + Amount; //make the change
        _context.SaveChanges(); //persist the change
        _context.ChangeTracker.Clear(); //clear the tracker for the next person
        return true;
    }
    /*/// <summary>
    /// Add a certain amount of Egg to an user
    /// </summary>
    /// <param name="User"></param>
    /// <param name="Amount"></param>
    /// <returns>1 if operation is successful, it will never return false at the moment</returns>
    /// <exception cref="ResourceNotFound">Occurs if no user exist matching the given User object</exception>*/
    public bool AddEgg(User User, int Amount)
    {   
        User User2Add2 = _context.Users.Find(User.UserId); //let us hope this works
        if(User2Add2 == null) //such user does not exist
        {
            throw new ResourceNotFound();
        }

        User2Add2.EggCount = User2Add2.EggCount + Amount; //make the change
        _context.SaveChanges(); //persist the change
        _context.ChangeTracker.Clear(); //clear the tracker for the next person
        return true;
    }
    /*/// <summary>
    /// Attach a certain amount of one perticular food to a certain user's inventory
    /// </summary>
    /// <param name="User"></param>
    /// <param name="Amount"></param>
    /// <param name="Food2Add"></param>
    /// <returns>1 if operation is successful, it will never return false at the moment</returns>*/
    public bool AddFood(User User, int Amount, FoodStat Food2Add) //remember to check for input validation in the services layer
    {
        FoodInventory Inventory2Add2 = 
        (from IV in _context.FoodInventories
        where (IV.UserIdFk == User.UserId) && (IV.FoodStatsIdFk == Food2Add.FoodStatsId)
        select IV).FirstOrDefault(); //this whole thing returns either a foodInvertory or null

        if(Inventory2Add2 == null) //if user does not have this kind of food yet
        {
            Inventory2Add2 = new FoodInventory
            {
                UserIdFk = (int) User.UserId,
                FoodStatsIdFk = Food2Add.FoodStatsId,
                FoodCount = Amount
            };
            _context.Add(Inventory2Add2); //Add a new item onto the table
            _context.SaveChanges(); //persist the change
            _context.ChangeTracker.Clear(); //clear the tracker for the next person
            return true;
        }

        Inventory2Add2.FoodCount = Inventory2Add2.FoodCount + Amount; //if the user already own this kind of food
        _context.SaveChanges(); //persist the change
        _context.ChangeTracker.Clear(); //clear the tracker for the next person
        return true;
    }
}