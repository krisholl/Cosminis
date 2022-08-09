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
            return Results.BadRequest("Something went very wrong here."); 
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
/*
    public IResult AddFriend(int requesterId, int addedId)
    {
    	try
    	{
    		_friendServices.AddFriend(requesterId, addedId);
    		return Results.Created("/AddFriend", friendsList); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.Conflict("You are either friends or never will be!!"); 
        }	
    }*/
}