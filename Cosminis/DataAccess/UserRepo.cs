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
        _context.Add(user); //Add a new user into the table

        _context.SaveChanges(); //persist the change

        _context.ChangeTracker.Clear(); //clear the tracker for the next person

        return user; //return the inputed user info
    }
  
   /* public User GetUserByUserName()
    {
        throw new ResourceNotFound();
    }*/

   
}