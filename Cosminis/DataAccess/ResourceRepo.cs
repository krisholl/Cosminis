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
    public bool AddGold(User User, int Amount)
    {
        User User2Add2 = _context.Users.Find(User.UserId); //let us hope this works
        if(User == null) //such user does not exist
        {
            throw new ResourceNotFound();
        }

        User2Add2.GoldCount = User2Add2.GoldCount + Amount; //make the change
        _context.SaveChanges(); //persist the change
        _context.ChangeTracker.Clear(); //clear the tracker for the next person
        return true;
    }
    public bool AddEgg(User User, int Amount)
    {   
        User User2Add2 = _context.Users.Find(User.UserId); //let us hope this works
        if(User == null) //such user does not exist
        {
            throw new ResourceNotFound();
        }

        User2Add2.EggCount = User2Add2.EggCount + Amount; //make the change
        _context.SaveChanges(); //persist the change
        _context.ChangeTracker.Clear(); //clear the tracker for the next person
        return true;
    }
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