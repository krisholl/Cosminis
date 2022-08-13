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