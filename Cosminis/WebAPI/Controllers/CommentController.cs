using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Controllers;

public class CommentController
{
	private readonly CommentServices _commentServices;

    public CommentController(CommentServices commentServices)
    {
        _commentServices = commentServices;
    }

    public IResult SubmitCommentResourceGen(Comment comment)
    { 
        if (comment.Content.Length > 255) 
        {
            return Results.Conflict("Comments' content cannot be greater than 255 characters."); //processing the request would create a conflict within the resource
        }
        try
        {
            Comment commentInfo = _commentServices.SubmitCommentResourceGen(comment);
            return Results.Created("/submitComment", commentInfo);   
        }
        catch(ResourceNotFound)
        {
            return Results.NotFound("Such a user does not exist"); 
        }
        catch(PostsNotFound)
        {
            return Results.NotFound("Such a post does not exist"); 
        }

    }

    public IResult GetCommentsByPostId(int postId)
    {
        try 
        {
            List<Comment> comments = _commentServices.GetCommentsByPostId(postId);
            return Results.Ok(comments); 
        }
        catch(CommentsNotFound)
        {
            return Results.NotFound("There are no comments associated with that post ID"); 
        }
    }

    public IResult RemoveComment(int commentId)
    {
        try
        {
            return Results.Ok(_commentServices.RemoveComment(commentId));
        }
        catch(CommentsNotFound)
        {
            return Results.NotFound("Such a comment does not exist"); 
        }
    }
}