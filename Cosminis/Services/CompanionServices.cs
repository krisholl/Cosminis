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

    public Companion SetCompanionMood(int companionId)
    {
        try
        {
            Companion checkCompanion = _CompanionRepo.GetCompanionByCompanionId(companionId);
            if(checkCompanion == null)
            {
                throw new ResourceNotFound("No companion with this ID exists.");
            }
            return _CompanionRepo.SetCompanionMood(companionId);
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
        try
        {
            Companion checkUser = _CompanionRepo.GetCompanionsByUser(userId);
            if(checkUser == null)
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