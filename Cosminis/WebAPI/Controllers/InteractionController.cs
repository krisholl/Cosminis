using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Controllers;

public class InteractionController
{
	private readonly InteractionService _interactionService;

    public InteractionController(InteractionService interactionService)
    {
        _interactionService = interactionService;
    }

    public IResult DecrementCompanionMoodValue(int companionID)
    {
    	try
    	{
    		_interactionService.DecrementCompanionMoodValue(companionID);
    		return Results.Accepted("/Interactions/IncrementalDecrement"); 
    	}
        catch(TooSoon)
        {
            return Results.BadRequest("It has been less than five minutes since the last time the mood was changed.");
        }
    	catch(CompNotFound)
        {
            return Results.NotFound("No companion with this ID exists."); 
        }
    	catch(UserNotFound)
        {
            return Results.NotFound("No user with this ID exists."); 
        }	        
    	catch(ResourceNotFound)
        {
            return Results.NotFound("There was nothing to decrement."); 
        }	
    }

    public IResult RollCompanionEmotion(int companionID)
    {
    	try
    	{
    		_interactionService.RollCompanionEmotion(companionID);
    		return Results.Accepted("/Interactions/RerollEmotion"); 
    	}
    	catch(CompNotFound)
        {
            return Results.NotFound("No companion with this ID exists."); 
        }        
    	catch(ResourceNotFound)
        {
            return Results.NotFound("No companion with this ID exists."); 
        }	
    }    

    public IResult PetCompanion(int userID, int companionID)
    {
    	try
    	{
    		_interactionService.PetCompanion(userID, companionID);
    		return Results.Accepted("/Interactions/PetCompanion"); 
    	}          
    	catch(CompNotFound)
        {
            return Results.NotFound("No companion with this ID exists."); 
        }	  
    	catch(UserNotFound)
        {
            return Results.NotFound("No user with this ID exists."); 
        }	
    	catch(ResourceNotFound)
        {
            return Results.NotFound("We couldn't find the specified information."); 
        }	
    }     
}