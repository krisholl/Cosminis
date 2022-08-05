using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Controllers;

public class PostController
{
	private readonly PostServices _postServices;

    public PostController(PostServices postServices)
    {
        _postServices = postServices;
    }

    public IResult SubmitPostResourceGen(Post post)
    { 
        if (post.Content.Length > 255) 
        {
            return Results.Conflict("Posts' content cannot be greater than 255 characters."); //processing the request would create a conflict within the resource
        }
        try
        {
            Post postInfo = _postServices.SubmitPostResourceGen(post);
            return Results.Created("/submitPost", postInfo);   
        }
        catch(ResourceNotFound)
        {
            return Results.BadRequest("Such a user does not exist"); 
        }   

    }
}