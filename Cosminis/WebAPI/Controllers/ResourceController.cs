using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Controllers;

public class ResourceController
{
	private readonly ResourceServices _resourceServices;

    public ResourceController(ResourceServices resourceServices)
    {
        _resourceServices = resourceServices;
    }

    public IResult GetFoodInventoryByUserId(int userId)
    {
        try 
        {
            List<FoodInventory> food = _resourceServices.GetFoodInventoryByUserId(userId);
            return Results.Ok(food); 
        }
        catch(ResourceNotFound)
        {
            return Results.NotFound("That user has no food"); 
        }
    }
}