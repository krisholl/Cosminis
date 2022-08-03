using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class UserServices
{
	private readonly IUserDAO _userRepo;

    public UserServices(IUserDAO userRepo)
    {
        _userRepo = userRepo;
    }

    public User SearchFriend(string username)
    {
    	try
    	{
    		return _userRepo.GetUserByUserName(username);
    	}
    	catch(ResourceNotFound)
        {
            throw; //is this right?
        }	
    }
}