using DataAccess.Entities;
using CustomExceptions;
using Models;
using System;
using System.Data;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
 
public class UserRepo
{
    private readonly wearelosingsteamContext _context;

    public UserRepo(wearelosingsteamContext context)
    {
        _context = context;
    }

    public User CreateUser(User user)
    {
        _context.Users.Add(user); //Add a new user into the table

        _context.SaveChanges(); //persist the change

        _context.ChangeTracker.Clear(); //clear the tracker for the next person

        return user; //return the inputed user info
    }
  
    public User GetUserByUserName(string username) //FirstOrDefault helps to bypass the issues that arise from deferred execution because now the query is being enumerated
    {                                              //selects and returns all of the user information where table username = input username
        return _context.Users.FirstOrDefault(user => user.Username == username) ?? throw new ResourceNotFound("No user with that username was found.");     
    }

   
}