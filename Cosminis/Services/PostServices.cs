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
    	int charCount = newPost.Content.Length;  //gets the length of each post's content

        if (charCount <= 10)
        {
        	Weight = 0;
        }
        else if (charCount > 10 && charCount <= 80)
        {
        	Weight = 50;
        }
    	else if (charCount > 80)
    	{
    		Weight = 100;
    	}
 	
       _resourceRepo.AddFood(shellUser, Weight);
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