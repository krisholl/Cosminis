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
            if(checkRelationship == null)
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
            if(checkRelationship == null)
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

    public List<Friends> ViewAllFriends(int userIdToLookup)
    {
        try
        {
            List<Friends> checkRelationship = _friendsRepo.ViewAllFriends(userIdToLookup);
            if(checkRelationship == null)
            {
                throw new ResourceNotFound("This user has no friends.");
            }
            return _friendsRepo.ViewAllFriends(userIdToLookup);
        }
        catch (Exception E)
        {
            throw;
        }
    }   
/*
    public bool AddFriend(int requesterId, int addedId)
    {
        try
        {
            bool checkRelationship = _friendsRepo.AddFriend(requesterId, addedId);
            if(checkRelationship == false)
            {
                throw new DuplicateFriends("No relationship with this status exists.");
            }

            int goldToAdd = 5;

            _resourceRepo.AddGold(addedId, goldToAdd);
        }
        catch (Exception E)
        {
            throw;
        }

         return true;//maybe later return the new relationship w/Id
    }  */  
}