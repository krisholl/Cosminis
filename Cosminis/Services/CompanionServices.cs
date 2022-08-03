using DataAccess;
using CustomExceptions;
using Models;
using DataAccess.Entities;

namespace Services;

public class CompanionServices
{
    private readonly ICompanionDAO _CompanionRepo;

    private readonly ISpeciesDAO _SpeciesRepo;
    
    private readonly UserRepo _UserRepo;

    public CompanionServices(ICompanionDAO CompanionRepo, ISpeciesDAO SpeciesRepo, UserRepo UserRepo) //This line also will ask for the user repo when it is done
    {
        _CompanionRepo = CompanionRepo;
        _SpeciesRepo = SpeciesRepo;
        _UserRepo = UserRepo;
    }

    public Companion GenerateStartingCompanion(string username)
    {
        try
        {
            User newUser = _UserRepo.GetUserByUserName(username);
            if(newUser == null)
            {
                throw new ResourceNotFound("No user with this username exists");
            }

            int companionGenerationInput = (int)newUser.UserId;

            Companion companionToGenerate = _CompanionRepo.GenerateCompanion(companionGenerationInput);

            _SpeciesRepo.GenerateBaseStats(companionToGenerate.SpeciesFk);

            return companionToGenerate;
        }
        catch (Exception E)
        {
            throw;
        }
    }

    //newCompanion.UserFk = userIdInput;// == null ? default(int) : usersId.Value;
    
        //int? usersId = identifiedUser.UserId;

    public Companion SetCompanionNicknme(int companionId, string? nickname)
    {
        //Check if the user actually exist
        try
        {
            Companion checkCompanion = _CompanionRepo.GetCompanionByCompanionId(companionId);
            if(checkCompanion == null) //if such user does not exist, throw an exception
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

    public Companion GetCompanionsByUser(int userId)
    {
        //Check if the user actually exist
        try
        {
            Companion checkUser = _CompanionRepo.GetCompanionsByUser(userId);
            if(checkUser == null) //if such user does not exist, throw an exception
            {
                throw new ResourceNotFound("No companion with this ID exists.");
            }
            return _CompanionRepo.GetCompanionsByUser(userId);
        }
        catch (Exception E)
        {
            throw;
        }
    }

    public Companion GetCompanionsByCompanionId(int companionId)
    {
        Companion checkCompanion = _CompanionRepo.GetCompanionByCompanionId(companionId);
        if(checkCompanion == null)
        {
            throw new ResourceNotFound("No companion with this ID exists.");
        }
        return _CompanionRepo.GetCompanionsByUser(companionId);
    }        
}