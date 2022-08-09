using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess;
 
public class FriendsRepo : IFriendsDAO
{
    private readonly wearelosingsteamContext _context;
    private readonly IUserDAO _userRepo;

    public FriendsRepo(wearelosingsteamContext context, IUserDAO userRepo)
    {
        _context = context;
        _userRepo = userRepo;
    }

    public List<Friends> GetAllRelationships()
    {
        return _context.Friends.ToList();
    }

    public List<Friends> ViewAllFriends(int userIdToLookup)
    {
        User userToLookup = _context.Users.Find(userIdToLookup);

        IEnumerable<Friends> friendsQuery =
            (from Friends in _context.Friends
            where (Friends.UserIdTo == userToLookup.UserId) || (Friends.UserIdFrom == userToLookup.UserId)
            select Friends);

        //if(friendsQuery == null)
        //{
            //throw new ResourceNotFound("This is one lonely user. At this point they should simply quit social media.");
        //}

        try
        {       
            List<Friends> friendsList = friendsQuery.ToList();
            
            if(friendsList.Count() < 1)
            {
                throw new Exception("This user has no friends. IDK how this got this far");
            }

            return friendsList;
        }
        catch(Exception E)
        {
            throw;
        }                                  
    }
    
    /*
    public User AcceptFriendRequest(string username)
    {   
        User newFriend = GetUserByUserName(username);
        
        _context.Companions.Add(newCompanion);

        _context.SaveChanges();

        _context.ChangeTracker.Clear(); 

        return newCompanion;  
    }

    public User RemoveFriend(string username)
    {   
        User newFriend = GetUserByUserName(username);
        
        _context.Companions.Add(newCompanion);

        _context.SaveChanges();

        _context.ChangeTracker.Clear(); 

        return newCompanion;  
    }

    public User BlockUser(string username)
    {   
        User newFriend = GetUserByUserName(username);
        
        _context.Companions.Add(newCompanion);

        _context.SaveChanges();

        _context.ChangeTracker.Clear(); 

        return newCompanion;  
    }    
    */
    
    public List<Friends> ViewRelationShipsByStatus(string status)
    {
        List<Friends> relationsList = new List<Friends>();

        switch(status) 
        {
        case "Pending":
            try
            {
            IEnumerable<Friends> statusQuery =
                from Friends in _context.Friends
                where Friends.Status == RelationshipStatus.Pending.ToString()
                select Friends;

            foreach(Friends friendsReturn in statusQuery)
            {
                relationsList.Add(friendsReturn);
            }  

            if(relationsList.Count() < 1)
            {
                throw new Exception("There are no relationships with this status.");
            }

            return relationsList;
            }
            catch(Exception E)
            {
                throw;
            }
            break;
        case "Accepted":
            try
            {
            IEnumerable<Friends> statusQuery =
                from Friends in _context.Friends
                where Friends.Status == RelationshipStatus.Accepted.ToString()
                select Friends;

            foreach(Friends friendsReturn in statusQuery)
            {
                relationsList.Add(friendsReturn);
            }  

            if(relationsList.Count() < 1)
            {
                throw new Exception("There are no relationships with this status.");
            }

            return relationsList;
            }
            catch(Exception E)
            {
                throw;
            }
            break;
        case "Removed":
            try
            {
            IEnumerable<Friends> statusQuery =
                from Friends in _context.Friends
                where Friends.Status == RelationshipStatus.Removed.ToString()
                select Friends;

            foreach(Friends friendsReturn in statusQuery)
            {
                relationsList.Add(friendsReturn);
            }  

            if(relationsList.Count() < 1)
            {
                throw new Exception("There are no relationships with this status.");
            }

            return relationsList;
            }
            catch(Exception E)
            {
                throw;
            }
            break;
        case "Blocked":
            try
            {
            IEnumerable<Friends> statusQuery =
                from Friends in _context.Friends
                where Friends.Status == RelationshipStatus.Blocked.ToString()
                select Friends;

            foreach(Friends friendsReturn in statusQuery)
            {
                relationsList.Add(friendsReturn);
            }  

            if(relationsList.Count() < 1)
            {
                throw new Exception("There are no relationships with this status.");
            }

            return relationsList;
            }
            catch(Exception E)
            {
                throw;
            }
            break;    
        default:
            try
            {
            IEnumerable<Friends> statusQuery =
                from Friends in _context.Friends
                where Friends.Status == RelationshipStatus.Accepted.ToString()
                select Friends;

            foreach(Friends friendsReturn in statusQuery)
            {
                relationsList.Add(friendsReturn);
            }  

            if(relationsList.Count() < 1)
            {
                throw new Exception("There are no relationships with this status.");
            }

            return relationsList;
            }
            catch(Exception E)
            {
                throw;
            }
            break;
        };

        return relationsList;                  
    }
/*
    public bool AddFriend(int requesterId, int addedId)
    {
        Friends editingFriend = _context.Friends.Find(requesterId);
        Friends friend2BeAdded = _context.Friends.Find(addedId);*/
/*
        Friends newRelationship = 
        (from RelationshipId in _context.Friends
        where (userIdFrom == editingFriend.UserId) && (IV.FoodStatsIdFk == Food2Add.FoodStatsId)
        select IV).FirstOrDefault();
  */     /* 
        try
        {
        newRelationship = new Friends
            {
                UserIdFrom = editingFriend,
                UserIdTo = friend2BeAdded,
                Status = "Pending"
            };

        _context.Friends.Add(newRelationship);

        _context.SaveChanges();

        _context.ChangeTracker.Clear();

        }
        catch(Exception E)
        {
            throw;
        }

        return true;                  
    } */   
}