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
    private readonly DbContext _context;

    public UserRepo(DbContext context)
    {
        _context = context;
    }

    public User CreateUser(User user)
    {
        _context.Add(user);

        _context.SaveChanges();

        _context.ChangeTracker.Clear();

        return user;
    }
  
   /* public User GetUserByUserName()
    {
        throw new ResourceNotFound();
    }*/

   
}