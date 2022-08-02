using DataAccess;
using CustomExceptions;
using Models;
using DataAccess.Entities;

namespace Services;

public class ResourceServices
{
    private readonly IResourceGen _ResourceRepo;
    private readonly UserRepo _UserRepo;
    public ResourceServices(IResourceGen ResourceRepo, UserRepo UserRepo) //This line also will ask for the user repo when it is done
    {
        _ResourceRepo = ResourceRepo;
        _UserRepo = UserRepo;
    }

    public bool AddGold(User User, int Amount)
    {
        //Check if the user actually exist
        try
        {
            User checkUser = _UserRepo.GetUserByUserName(User.Username);
            if(checkUser == null) //if such user does not exist, throw an exception
            {
                throw new ResourceNotFound("Such User Does Not Exist");
            }
            return _ResourceRepo.AddGold(User,Amount);
        }
        catch (Exception E)
        {
            throw;
        }
        
        return false;
    }

    public bool AddEgg(User User, int Amount)
    {
        //Check if the user actually exist
        try
        {
            User checkUser = _UserRepo.GetUserByUserName(User.Username);
            if(checkUser == null) //if such user does not exist, throw an exception
            {
                throw new ResourceNotFound("Such User Does Not Exist");
            }
            return _ResourceRepo.AddEgg(User,Amount);
        }
        catch (Exception E)
        {
            throw;
        }

        return false;
    }

    public bool AddFood(User User, int Amount, FoodStat Food2Add)
    {
        //Check if the user actually exist
        try
        {
            User checkUser = _UserRepo.GetUserByUserName(User.Username);
            if(checkUser == null) //if such user does not exist, throw an exception
            {
                throw new ResourceNotFound("Such User Does Not Exist");
            }
            return _ResourceRepo.AddFood(User,Amount,Food2Add);
        }
        catch (Exception E)
        {
            throw;
        }
  
        return false;
    }
}