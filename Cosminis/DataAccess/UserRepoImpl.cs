using DataAccess.Entities;
using CustomExceptions;
using Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess;
 
public class UserRepo
{
    public string testString = "Testing Merge";
    
    /// <summary>
    /// Retrieves and displays a list of all users contained in the users table of WALS_P2 database.
    /// </summary>
    /// <param userName="userName"></param>
    /// <returns>returns(user list)</returns>
    /// <exception cref="RecordNotFoundException">exception descriptions</exception>
    public User GetUserByUserName()
    {
        throw new ResourceNotFound();
    }

    public string StinkyPooPoo = "Pudding";
}