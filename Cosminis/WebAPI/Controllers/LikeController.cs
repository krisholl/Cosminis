using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Controllers;

public class LikeController
{
	private readonly LikeServices _LikeServices;

    public LikeController(LikeServices LikeServices)
    {
        _LikeServices = LikeServices;
    }

    public IResult RemoveLikes(int UserID, int PostID)
    {
        try
        {
            _LikeServices.RemoveLikes(UserID, PostID);
            return Results.Accepted("/UnLiked");
        }
        catch(Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    public IResult AddLikes(int UserID, int PostID)
    {
        try
        {
            return Results.Created("/Liked", _LikeServices.AddLikes(UserID, PostID));
        }
        catch(Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    public IResult LikeCount(int PostID)
    {
        try
        {
            return Results.Accepted("LikeCount",_LikeServices.LikeCount(PostID));
        }
        catch(Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

}