using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Controllers;

public class FriendsController
{
	private readonly FriendServices _friendServices;

    public FriendsController(FriendServices friendServices)
    {
        _friendServices = friendServices;
    }

    public IResult ViewAllRelationships()
    {
    	try
    	{
    		List<Friends> friendsList = _friendServices.ViewAllRelationships();
    		return Results.Ok(friendsList); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.BadRequest("Something went very wrong here."); 
        }	
    }

    public IResult ViewAllFriends(int userIdToLookup)
    {
    	try
    	{
    		List<Friends> friendsList = _friendServices.ViewAllFriends(userIdToLookup);
    		return Results.Ok(friendsList); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.BadRequest("This user has no friends!!! Pretty sad for a social media website if you ask me... Definitely no Jimmy."); 
        }	
    }

    public IResult SearchByRelationshipId(int relationshipId)
    {
    	try
    	{
    		Friends relationship = _friendServices.SearchByRelationshipId(relationshipId);
    		return Results.Ok(relationship); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.BadRequest("This relationship does not exist."); 
        }	
    }

    public IResult ViewRelationshipsByStatus(string status)
    {
    	try
    	{
    		List<Friends> friendsList = _friendServices.ViewRelationShipsByStatus(status);
    		return Results.Ok(friendsList); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.BadRequest("Something went very wrong here."); 
        }	
    }

    public IResult CheckRelationshipStatusByUserId(int searchingId, string status)
    {
    	try
    	{
    		List<Friends> friendsList = _friendServices.CheckRelationshipStatusByUserId(searchingId, status);
    		return Results.Ok(friendsList); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.BadRequest("This user has no relationships with this status."); 
        }	
    }  

    public IResult FriendsByUserIds(int searchingUserId, int user2BeSearchedFor)
    {
    	try
    	{
    		Friends friendsReturn = _friendServices.FriendsByUserIds(searchingUserId, user2BeSearchedFor);
    		return Results.Ok(friendsReturn); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.BadRequest("These users have no established relationship."); 
        }	
    }

    public IResult EditStatus(int editingUserID, int user2BeEdited, string status)
    {
    	try
    	{
    		Friends editFriendship = _friendServices.EditFriendship(editingUserID, user2BeEdited, status);
    		return Results.Created("/Friends/EditStatus", editFriendship); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.BadRequest("These users have no established relationship."); 
        }	
    }    

    public IResult CheckRelationshipStatusByUsername(string username, string status)
    {
    	try
    	{
    		List<Friends> friendsList = _friendServices.CheckRelationshipStatusByUsername(username, status);
    		return Results.Ok(friendsList); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.BadRequest("This user has no relationships with this status."); 
        }	
    }      

    public IResult AddFriend(int requesterId, int addedId)
    {
    	try
    	{
    		Friends newFriends = _friendServices.AddFriend(requesterId, addedId);
    		return Results.Created("/Friends/AddFriend", newFriends); 
    	}
        catch(ResourceNotFound)
        {
            return Results.BadRequest("There is no user with this ID in our system.");
        }
    	catch(DuplicateFriends)
        {
            return Results.Conflict("You are either friends or never will be!!"); 
        }	
    }
}