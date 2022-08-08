using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Controllers;

public class UserController
{
	private readonly UserServices _userServices;

    public UserController(UserServices userServices)
    {
        _userServices = userServices;
    }

    public IResult SearchFriend(string username)
    {
    	try
    	{
    		User userInfo = _userServices.SearchFriend(username);
    		return Results.Ok(userInfo); 
    	}
    	catch(ResourceNotFound)
        {
            return Results.NotFound("No user with that username was found."); 
        }	
    }
}