using CustomExceptions;
using Models;
using Services;
using DataAccess.Entities;

namespace Controllers;

public class CompanionController
{
    private readonly CompanionServices _service;

    public CompanionController(CompanionServices service)
    {
        _service = service;
    }

    public IResult GetAllCompanions()
    {
        List<Companion> companionList = _service.GetAllCompanions();
        return Results.Ok(companionList);
    }

    public IResult SearchForCompanionById(int companionId)
    {
        try
        {   
            Companion queriedCompanion = _service.GetCompanionByCompanionId(companionId);
            return Results.Ok(queriedCompanion);
        }
        catch(Exception e)
        {
            return Results.BadRequest("There is no companion with this ID");
        }
    }

    public IResult SearchForCompanionByUserId(int userId)
    {
        try
        {   
            List<Companion> queriedCompanion = _service.GetCompanionByUser(userId);
            return Results.Ok(queriedCompanion);
        }
        catch(Exception e)
        {
            return Results.BadRequest("There is no companion with this ID");
        }
    }

    public IResult NicknameCompanion(int companionId, string? nickname)
    {
        try
        {   
            Companion companionToName = _service.SetCompanionNickname(companionId, nickname);
            return Results.Created("/companions/Nickname", companionToName);
        }
        catch(Exception)
        {
            return Results.BadRequest("Invalid Input");
        }
    }  

    public IResult GenerateCompanion(string username)
    {
        try
        {   
            int? createdCompanion = _service.HatchCompanion(username);
            return Results.Created("/companions/GenerateCompanion", createdCompanion);
        }
        catch(UserNotFound)
        {
            return Results.BadRequest("There is no user with this ID");
        }
        catch(TooFewResources)
        {
            return Results.BadRequest("This user has no eggs");
        }                
        catch(Exception)
        {
            return Results.BadRequest("Invalid Input");
        }
    }  

    public IResult DeleteCompanion(int companionId)
    {
        try
        {   
            _service.DeleteCompanion(companionId);
            return Results.Ok(companionId);
        }
        catch(Exception)
        {
            return Results.NotFound("No Companion with this ID exists");
        }
    }     
}