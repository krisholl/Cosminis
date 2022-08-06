using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class LikeRepo:ILikeIt
{
    private readonly wearelosingsteamContext _context;

    public LikeRepo(wearelosingsteamContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Takes a user and a post (ID), add a like to the post by the perticular user
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="PostID"></param>
    /// <returns>True of operation is successful, cannot return false ATM</returns>
    /// <exception cref="ResourceNotFound">Occurs if no user exist matching the given User ID or if no post exist matching the given post ID</exception>*/
    /// <exception cref="DuplicateLikes">Occurs if the perticular post has already been liked by the perdicular user</exception>*/
    public bool AddLikes(int UserID, int PostID)
    {
        User LikingUser = _context.Users.Find(UserID); //Fetch and link the user
        Post LikedPost = _context.Posts.Find(PostID); //Fetch and link the post
        _context.Entry(LikingUser).Collection("PostIdFks").Load();
        _context.Entry(LikedPost).Collection("UserIdFks").Load();

        if(LikingUser==null || LikedPost==null) //check if Fetching has been successful
        {
            throw new ResourceNotFound("Either the user or the post does not exist");
        }
        if(LikingUser.PostIdFks.Contains(LikedPost) || LikedPost.UserIdFks.Contains(LikingUser)) //check if such many to many relationship already exist
        {
            throw new DuplicateLikes("This post has already been liked by this user");
        }

        try
        {
            LikingUser.PostIdFks.Add(LikedPost);
            LikedPost.UserIdFks.Add(LikingUser);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return true;
        }
        catch(Exception)
        {
            throw;
        }
        
        return false;
    }
    /// <summary>
    /// Takes a user and a post (ID), remove a like to the post by the perticular user
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="PostID"></param>
    /// <returns>True of operation is successful, cannot return false ATM</returns>
    /// <exception cref="ResourceNotFound">Occurs if no user exist matching the given User ID or if no post exist matching the given post ID</exception>*/
    public bool RemoveLikes(int UserID, int PostID)
    {
        User UnLikingUser = _context.Users.Find(UserID); //Fetch and link the user
        Post UnLikedPost = _context.Posts.Find(PostID); //Fetch and link the post
        _context.Entry(UnLikedPost).Collection("UserIdFks").Load(); //This loads the FKs into the collection
        _context.Entry(UnLikingUser).Collection("PostIdFks").Load(); //This loads the FKs into the collection

        if(UnLikingUser==null || UnLikedPost==null) //check if Fetching has been successful
        {
            throw new ResourceNotFound("Either the user or the post does not exist");
        }
        if(UnLikingUser.PostIdFks.Contains(UnLikedPost) || UnLikedPost.UserIdFks.Contains(UnLikingUser)) //check if such many to many relationship already exist
        {
            try
            {
                UnLikedPost.UserIdFks.Remove(UnLikingUser); //updates one should update em all
                _context.SaveChanges();
                _context.ChangeTracker.Clear();
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }
        throw new LikeDoesNotExist("This user didn't like this post in the first place");
        return false;
    }
    /// <summary>
    /// Counts the amount of likes a perticular post has
    /// </summary>
    /// <param name="PostID"></param>
    /// <returns>The number of likes matching the given postID</returns>
    /// <exception cref="ResourceNotFound">Occurs if no post exist matching the given post ID</exception>*/
    public int LikeCount(int PostID)
    {
        Post CheckingPost = _context.Posts.Find(PostID); //Fetch and link the post
        if(CheckingPost==null) //check if Fetching has been successful
        {
            throw new ResourceNotFound("The post does not exist");
        }
        _context.Entry(CheckingPost).Collection("UserIdFks").Load(); //This loads the FKs into the collection
        return CheckingPost.UserIdFks.Count();
    }
}