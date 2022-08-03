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
        try
        {
            Post postInfo = _postServices.SubmitPostResourceGen(post);
            return Results.Ok(postInfo); 
        }
        catch(ResourceNotFound)
        {
            return Results.BadRequest("Such a user does not exist"); 
        }   
    }
}