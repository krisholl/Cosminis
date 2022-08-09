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
    private readonly IResourceGen _ResourceRepo;

    public FriendServices(IFriendsDAO friendsRepo, IResourceGen ResourceRepo)
    {
        _friendsRepo = friendsRepo;
        _ResourceRepo = ResourceRepo;
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

    public List<Friends> CheckRelationshipStatusByUserId(int searchingId, string status)
    {
        try
        {
            List<Friends> checkRelationship = _friendsRepo.CheckRelationshipStatusByUserId(searchingId, status);
            if(checkRelationship == null)
            {
                throw new ResourceNotFound("No relationship with this status exists.");
            }
            return _friendsRepo.CheckRelationshipStatusByUserId(searchingId, status);
        }
        catch (Exception E)
        {
            throw;
        }
    }            

    public List<Friends> CheckRelationshipStatusByUsername(string username, string status)
    {
        try
        {
            List<Friends> checkRelationship = _friendsRepo.CheckRelationshipStatusByUsername(username, status);
            if(checkRelationship == null)
            {
                throw new ResourceNotFound("No relationship with this status exists.");
            }
            return _friendsRepo.CheckRelationshipStatusByUsername(username, status);
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
    public Friends AddFriend(int requesterId, int addedId)
    {
        User user2Add2 = new User();
        user2Add2.UserId = addedId;
        try
        {
            Friends checkRelationship = _friendsRepo.AddFriend(requesterId, addedId);
            /*
            if(checkRelationship == null)
            {
                throw new DuplicateFriends();
            }
            */
            /*
            int goldToAdd = 5;

            _ResourceRepo.AddGold(user2Add2, goldToAdd);

            return checkRelationship;
        }
        catch (Exception E)
        {
            throw;
        }
    } */  
}