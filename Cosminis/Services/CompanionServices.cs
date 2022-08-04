using DataAccess;
using CustomExceptions;
using Models;
using DataAccess.Entities;

namespace Services;

public class CompanionServices
{
    private readonly ICompanionDAO _CompanionRepo;
    
    private readonly UserRepo _UserRepo;

    public CompanionServices(ICompanionDAO CompanionRepo, UserRepo UserRepo)
    {
        _CompanionRepo = CompanionRepo;
        _UserRepo = UserRepo;
    }

    public int? HatchCompanion(string username)
    {
        try
        {
            User newUser = _UserRepo.GetUserByUserName(username);
            if(newUser == null)
            {
                throw new ResourceNotFound("No user with this username exists");
            }

            Companion companionToGenerate = _CompanionRepo.GenerateCompanion((int)newUser.UserId);


            return companionToGenerate.CompanionId;

        }
        catch (Exception E)
        {
            throw;
        }
    }

    public Companion SetCompanionNickname(int companionId, string? nickname)
    {
        try
        {
            Companion checkCompanion = _CompanionRepo.GetCompanionByCompanionId(companionId);
            if(checkCompanion == null)
            {
                throw new ResourceNotFound("No companion with this ID exists.");
            }
            return _CompanionRepo.SetCompanionNickname(companionId, nickname);
        }
        catch (Exception E)
        {
            throw;
        }
    }

    public List<Companion> GetAllCompanions()
    {
        return _CompanionRepo.GetAllCompanions();
    }

    public Companion GetCompanionByUser(int userId)
    {
        try
        {
            Console.WriteLine("We are in services");
            Companion checkUser = _CompanionRepo.GetCompanionByUser(userId);
            if(checkUser == null)
            {
                throw new ResourceNotFound("No user with this Id exists.");
            }
            return _CompanionRepo.GetCompanionByUser(userId);
        }
        catch (Exception E)
        {
            throw;
        }
    }

    public Companion GetCompanionByCompanionId(int companionId)
    {
        try
        {
            Console.WriteLine("We are in services");
            Companion checkCompanion = _CompanionRepo.GetCompanionByCompanionId(companionId);
            if(checkCompanion == null)
            {
                throw new ResourceNotFound("No companion with this ID exists.");
            }
            return _CompanionRepo.GetCompanionByCompanionId(companionId);
        }
        catch (Exception E)
        {
            throw;
        }
    }        
}