using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
 
public class PostRepo
{
    private readonly wearelosingsteamContext _context;

    public PostRepo(wearelosingsteamContext context)
    {
        _context = context;
    }

     public Post SubmitPost(Post post)
    {
        _context.Posts.Add(post); //Add a new post into the table

        _context.SaveChanges(); //persist the change

        _context.ChangeTracker.Clear(); //clear the tracker for the next post

        return post; //return the inputed post info
    }
}