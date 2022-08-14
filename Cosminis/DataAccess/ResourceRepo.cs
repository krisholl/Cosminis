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
    /// Attach a sudo-random amount of one random food type to a certain user's inventory
    /// </summary>
    /// <param name="User"></param>
    /// <param name="Amount"></param>
    /// <param name="Food2Add"></param>
    /// <returns>1 if operation is successful, it will never return false at the moment</returns>*/
    public bool AddFood(User User, int Weight) //remember to check for input validation in the services layer
    {
        Random randomStat = new Random(); //I didn't copy homework from Lor's methods I swear
        int Amount = 1; //default to 1 for now
        int seed = randomStat.Next(1, Weight); //this weights the amounts of food anyone can get, should spice things up

        FoodStat Food2Add = _context.FoodStats.Find(randomStat.Next(1, 9)); //Our foodstat table has non consecutive IDs, so this will work weird
        while(Food2Add == null) //Hopefully this fixes above issue
        {
            Food2Add = _context.FoodStats.Find(randomStat.Next(1, 9));
        }
        if(Food2Add == null)
        {
            return false; //if somehow the while loop failed, return exit failure
        }

        if(seed <= 10) //May the RNGesus bless you
        {
            Amount = 0;
        }
        else if(seed<= 30) //this is also exponential scaling, STFU
        {
            Amount = 1;
        }
        else if(seed<= 90)
        {
            Amount = 2;
        }
        else if(seed<= 270)
        {
            Amount = 3;
        }
        else
        {
            Amount = 0;
        }

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

    /*/// <summary>
    /// Remove a certain amount of food from the food inventory attached to the user
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="FoodID"></param>
    /// <returns></returns>*/
    public bool RemoveFood(int userId, int foodId)
    {
        User userInfo = _context.Users.Find(userId);
        if (userInfo == null) //such user does not exist
        {
            throw new UserNotFound();
        }
        FoodInventory foodInfo = _context.FoodInventories.Find(userInfo.UserId, foodId); //the primary key for this table is composite key
        if (foodInfo == null || foodInfo.FoodCount == 0) //that user does not own that food at all or has none of that food left in their inventory
        {
            throw new FoodNotFound();
        }
        foodInfo.FoodCount = foodInfo.FoodCount - 1; 
        _context.SaveChanges(); //persist the change
        _context.ChangeTracker.Clear(); //clear the tracker for the next food transaction
        return true;
    }

    public List<FoodInventory> GetFoodInventoryByUserId(int userId)
    {
        return _context.FoodInventories.Where(food => food.UserIdFk == userId).ToList();
    }
}