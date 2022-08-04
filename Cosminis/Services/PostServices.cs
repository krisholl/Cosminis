using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class PostServices
{
	private readonly IPostDAO _postRepo;
	private readonly IResourceGen _resourceRepo;

    public PostServices(IPostDAO postRepo, IResourceGen resourceRepo)
    {
        _postRepo = postRepo;
        _resourceRepo = resourceRepo;
    }

    public Post SubmitPostResourceGen(Post post)
    {
    	User shellUser = new User();
    	shellUser.UserId = post.UserIdFk; //this sets the shellUser's id to the post's useridkfk, now shellUser actually has some useful info (a user ID) 

    	int goldToAdd = 0;
    	int charCount = post.Content.Length;  //gets the length of each post's content

        if (charCount <= 10)
        {
        	goldToAdd = 0;
        }
        else if (charCount > 10 && charCount <= 80)
        {
        	goldToAdd = 5;
        }
    	else if (charCount > 80)
    	{
    		goldToAdd = 10;
    	}
 	
       _resourceRepo.AddGold(shellUser, goldToAdd);
       return _postRepo.SubmitPost(post);
    }





}