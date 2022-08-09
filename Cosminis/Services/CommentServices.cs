using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class CommentServices
{
	private readonly ICommentDAO _commentRepo;
	private readonly IResourceGen _resourceRepo;

    public CommentServices(ICommentDAO commentRepo, IResourceGen resourceRepo)
    {
        _commentRepo = commentRepo;
        _resourceRepo = resourceRepo;
    }

    public Comment SubmitCommentResourceGen(Comment comment)
    {
    	User shellUser = new User();
    	shellUser.UserId = comment.UserIdFk; //this sets the shellUser's id to the comment's useridkfk, now shellUser actually has some useful info (a user ID) 

    	int goldToAdd = 0;
    	int charCount = comment.Content.Length;  //gets the length of each comment's content

        if (charCount <= 10)
        {
        	goldToAdd = 0;
        }
        else if (charCount > 10 && charCount <= 80)
        {
        	goldToAdd = 3;
        }
    	else if (charCount > 80)
    	{
    		goldToAdd = 6;
    	}
 	
        _resourceRepo.AddGold(shellUser, goldToAdd);
        _commentRepo.AddToPostOwner(comment);
        try
        {
            return _commentRepo.SubmitComment(comment);
        }
        catch(Exception)
        {
            throw;
        }
    }

    public List<Comment> GetCommentsByPostId(int postId)
    {
        List<Comment> comments = _commentRepo.GetCommentsByPostId(postId);
        if (comments.Count < 1) 
        {
            throw new CommentsNotFound();
        }
        return comments; 
    }


    public bool RemoveComment(int commentId)
    {
        try
        {
            return _commentRepo.RemoveComment(commentId);
        }
        catch(CommentsNotFound)
        {
            throw;
        }
        return false;
    }
}