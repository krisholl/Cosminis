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

    public FriendsRepo(wearelosingsteamContext context)
    {
        _context = context;
    }

    public List<Friends> GetAllRelationships()
    {
        return _context.Friends.ToList();
    }
/*
    public List<Friends> ViewAllFriends(int userId)
    {
        Friends friendsInstance = _context.Friends.Find(userId);       
        if(friendsInstance == null)
        {
            throw new ResourceNotFound("No user with this ID exists.");
        }

        try
        {       
            List<Friends> friendsList = new List<Friends>();

            _context.Entry(friendsInstance).Collection("UserIdFrom").Load();
            _context.Entry(friendsInstance).Collection("UserIdTo").Load();
            
            foreach(User friendReturn in userInstance.UserIdFk2s)
            {
                if(userInstance.UserIdFk1s == userInstance)
                {
                    return null;
                }
                friendsList.Add(friendReturn);
            } 

            foreach(User friendReturn in userInstance.UserIdFk1s)
            {
                if(userInstance.UserIdFk2s == userInstance)
                {
                    return null;
                }
                friendsList.Add(friendReturn);
            } 

            //List<User> friendsList = userInstance.UserIdFk2s.ToList();

            //friendsList2 = userInstance.UserIdFk2s.ToList();

            //friendsList.AddRange(friendsList2);
            //friendsList = friendsList.Concat(userInstance.UserIdFk2s);

            if(friendsList.Count() < 1)
            {
                throw new Exception("This user has no friends.");
            }

            return friendsList;
        }
        catch(Exception E)
        {
            throw;
        }                                  
    }
*/
    /*
    public User AddFriend()
    {
        Console.WriteLine("DAO1");

        User userInstance = _context.Users.Find(userId);       
        
        if(userInstance == null)
        {
            throw new ResourceNotFound("No user with this ID exists.");
        }

        try
        {       
            _context.Entry(userInstance).Collection("UserIdFk1s").Load();
            _context.Entry(userInstance).Collection("UserIdFk2s").Load();
            
            Console.WriteLine("DAO2");

            //List<User> friendsList = userInstance.UserIdFk2s.ToList();

            //friendsList2 = userInstance.UserIdFk2s.ToList();

            //friendsList.AddRange(friendsList2);
            //friendsList = friendsList.Concat(userInstance.UserIdFk2s);

            if(friendsList.Count() < 1)
            {
                throw new Exception("This user has no friends.");
            }
            
            Console.WriteLine("DAO3");
            
            return friendsList;
        }
        catch(Exception E)
        {
            throw;
        }                                  
    }
    */
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
        List<Friends> relationsLits = new List<Friends>();

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
                relationsLits.Add(friendsReturn);
            }  

            if(relationsLits.Count() < 1)
            {
                throw new Exception("There are no relationships with this status.");
            }

            return relationsLits;
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
                relationsLits.Add(friendsReturn);
            }  

            if(relationsLits.Count() < 1)
            {
                throw new Exception("There are no relationships with this status.");
            }

            return relationsLits;
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
                relationsLits.Add(friendsReturn);
            }  

            if(relationsLits.Count() < 1)
            {
                throw new Exception("There are no relationships with this status.");
            }

            return relationsLits;
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
                relationsLits.Add(friendsReturn);
            }  

            if(relationsLits.Count() < 1)
            {
                throw new Exception("There are no relationships with this status.");
            }

            return relationsLits;
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
                relationsLits.Add(friendsReturn);
            }  

            if(relationsLits.Count() < 1)
            {
                throw new Exception("There are no relationships with this status.");
            }

            return relationsLits;
            }
            catch(Exception E)
            {
                throw;
            }
            break;
        };

        return relationsLits;                  
    }
}