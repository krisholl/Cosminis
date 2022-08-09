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
                throw new ResourceNotFound();
            }
            return _friendsRepo.GetAllRelationships();
        }
        catch (ResourceNotFound)
        {
            throw;
        }
    }

    public Friends SearchByRelationshipId(int relationshipId)
    {
        try
        {
            Friends checkRelationship = _friendsRepo.SearchByRelationshipId(relationshipId);
            if(checkRelationship == null)
            {
                throw new ResourceNotFound();
            }
            return _friendsRepo.SearchByRelationshipId(relationshipId);
        }
        catch (ResourceNotFound)
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
                throw new ResourceNotFound();
            }
            return _friendsRepo.ViewRelationShipsByStatus(status);
        }
        catch (ResourceNotFound)
        {
            throw;
        }
    } 

    public Friends FriendsByUserIds(int searchingUserId, int user2BeSearchedFor)
    {
        try
        {
            Friends checkFriendship = _friendsRepo.FriendsByUserIds(searchingUserId, user2BeSearchedFor);
            if(checkFriendship == null)
            {
                throw new ResourceNotFound();
            }
            return _friendsRepo.FriendsByUserIds(searchingUserId, user2BeSearchedFor);
        }
        catch (ResourceNotFound)
        {
            throw;
        }
    }

    public Friends EditFriendship(int editingUserID, int user2BeEdited, string status)
    {
        try
        {
            Friends editFriendship = _friendsRepo.EditFriendship(editingUserID, user2BeEdited, status);
            if(editFriendship == null)
            {
                throw new ResourceNotFound();
            }
            return _friendsRepo.EditFriendship(editingUserID, user2BeEdited, status);
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
                throw new ResourceNotFound();
            }
            return _friendsRepo.CheckRelationshipStatusByUserId(searchingId, status);
        }
        catch (ResourceNotFound)
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
                throw new ResourceNotFound();
            }
            return _friendsRepo.ViewAllFriends(userIdToLookup);
        }
        catch (ResourceNotFound)
        {
            throw;
        }
    }   

    public Friends AddFriendByUserId(int requesterId, int addedId)
    {
        User user2Add2 = new User();
        user2Add2.UserId = addedId;
        try
        {
            Friends checkRelationship = _friendsRepo.AddFriendByUserId(requesterId, addedId);
            
            int goldToAdd = 5;

            _ResourceRepo.AddGold(user2Add2, goldToAdd);

            return checkRelationship;
        }
        catch (Exception E)
        {
            throw;
        }
    }

    public Friends AddFriendByUsername(string requesterUsername, string addedUsername)
    {
        User user2Add2 = new User();
        user2Add2.Username = addedUsername;
        try
        {
            Friends checkRelationship = _friendsRepo.AddFriendByUsername(requesterUsername, addedUsername);
            
            int goldToAdd = 5;

            _ResourceRepo.AddGold(user2Add2, goldToAdd);

            return checkRelationship;
        }
        catch (Exception E)
        {
            throw;
        }
    }      
}