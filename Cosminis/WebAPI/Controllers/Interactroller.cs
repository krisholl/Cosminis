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
}