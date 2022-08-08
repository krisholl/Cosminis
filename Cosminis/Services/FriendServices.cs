using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class FriendServices
{
	private readonly IFriendsDAO _friendsRepo;

    public FriendServices(IFriendsDAO friendsRepo)
    {
        _friendsRepo = friendsRepo;
    }

    public List<Friends> ViewAllRelationships()
    {
        try
        {
            List<Friends> checkRelationship = _friendsRepo.GetAllRelationships();
            if(checkRelationship== null)
            {
                throw new ResourceNotFound("No relationship with this Id exists.");
            }
            return _friendsRepo.GetAllRelationships();
        }
        catch (Exception E)
        {
            throw;
        }
    }

    public List<Friends> ViewRelationShipsByStatus(string status)
    {
        try
        {
            List<Friends> checkRelationship = _friendsRepo.ViewRelationShipsByStatus(status);
            if(checkRelationship== null)
            {
                throw new ResourceNotFound("No relationship with this status exists.");
            }
            return _friendsRepo.ViewRelationShipsByStatus(status);
        }
        catch (Exception E)
        {
            throw;
        }
    }        
/*
    public List<Friends> ViewAllFriends(int userId)
    {
        try
        {
            List<Friends> checkUser = _friendsRepo.ViewAllFriends(userId);
            if(checkUser == null)
            {
                throw new ResourceNotFound("No user with this Id exists.");
            }
            return _friendsRepo.ViewAllFriends(userId);
        }
        catch (Exception E)
        {
            throw;
        }
    }*/
}