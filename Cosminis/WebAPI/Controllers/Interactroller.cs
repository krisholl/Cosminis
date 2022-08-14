using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Controllers;

public class Interactroller
{
    private readonly InteractionService _interactionService;

    public Interactroller(InteractionService interactionService)
    {
        _interactionService = interactionService;
    }

    public IResult DecrementCompanionHungerValue(int companionID)
    {
        try
        {
            if(_interactionService.DecrementCompanionHungerValue(companionID))
            {
                return Results.Accepted();
            }
            else
            {
                return Results.BadRequest();
            }
        }
        catch(ResourceNotFound)
        {
            return Results.NotFound();
        }
        catch(Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    public IResult FeedCompanion(int feederID, int companionID, int foodID)
    {
        try
        {
            if(_interactionService.FeedCompanion(feederID, companionID, foodID))
            {
                return Results.Accepted();
            }
            else
            {
                return Results.BadRequest();
            }
        }
        catch(ResourceNotFound)
        {
            return Results.NotFound();
        }
        catch(Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    public IResult SetShowcaseCompanion(int userId, int companionId)
    {
        try
        {
            if(_interactionService.SetShowcaseCompanion(userId, companionId))
            {
                return Results.Ok(true); 
            }
            else
            {
                return Results.Conflict("You cannot set your showcase companion to a companion that you do not own.");
            }
        }
        catch(CompNotFound)
        {
            return Results.NotFound("Such a companion does not exist"); 
        }
        catch(UserNotFound)
        {
            return Results.NotFound("Such a user does not exist"); 
        }    

    public IResult PullConvo(int companionID)
    {
        try
        {
            return Results.Ok(_interactionService.PullConvo(companionID));
        }
        catch(ResourceNotFound)
        {
            return Results.NotFound();
        }
        catch(Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}