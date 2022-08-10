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

    public Post SubmitPostResourceGen(string Content, int PosterID)
    {
        Post newPost = new Post()
        {
            Content = Content,
            UserIdFk = PosterID
        };
    	User shellUser = new User();
    	shellUser.UserId = newPost.UserIdFk; //this sets the shellUser's id to the post's useridkfk, now shellUser actually has some useful info (a user ID) 

    	int Weight = 30;
        int goldToAdd = 0;
    	int charCount = newPost.Content.Length;  //gets the length of each post's content

        if (charCount <= 10) //linear scaling
        {
        	Weight = 20; //exponential scaling
        }
        else if (charCount <= 50)
        {
        	Weight = 40;
        }
        else if (charCount <= 90)
        {
        	Weight = 80;
        }
    	else if (charCount <= 130)
    	{
    		Weight = 160;
    	}
        else if (charCount > 140)
    	{
    		Weight = 320;
    	}

        if (charCount <= 10)
        {
        	goldToAdd = 0;
        }
        else if (charCount <= 80)
        {
        	goldToAdd = 5;
        }
    	else if (charCount > 80)
    	{
    		goldToAdd = 10;
    	}

        if(!_resourceRepo.AddFood(shellUser, Weight))
        {
            throw new ResourceNotFound("Something had gone wrong when adding food, your companion boutta starve");
        }

        try
        {
            _resourceRepo.AddGold(shellUser, goldToAdd);
        }
        catch(Exception)
        {
            throw;
        }
        
        return _postRepo.SubmitPost(newPost);
    }

    public List<Post> GetPostsByUserId(int userId)
    {
        List<Post> posts = _postRepo.GetPostsByUserId(userId);
        if (posts.Count < 1) 
        {
            throw new PostsNotFound();
        }
        return posts; 
    }

    public List<Post> GetPostsByUsername(string username)
    {
        List<Post> posts = _postRepo.GetPostsByUsername(username);
        if (posts.Count < 1) 
        {
            throw new PostsNotFound();
        }
        return posts;
    }
}