using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class ResourceServices
{
    private readonly IResourceGen _resourceRepo;

    public ResourceServices(IResourceGen resourceRepo)
    {
        _resourceRepo = resourceRepo;
    }
  
    public List<FoodInventory> GetFoodInventoryByUserId(int userId)
    {
        List<FoodInventory> food = _resourceRepo.GetFoodInventoryByUserId(userId);
        if (food.Count < 1) 
        {
            throw new ResourceNotFound();
        }
        return food; 
    }
}