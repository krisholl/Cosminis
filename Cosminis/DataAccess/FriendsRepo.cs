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

        if(friendsQuery == null)
        {
            throw new ResourceNotFound("This is one lonely user. At this point they should simply quit social media.");
        }

        try
        {       
            List<Friends> friendsList = friendsQuery.ToList();
            
            if(friendsList.Count() < 1)
            {
                throw new ResourceNotFound();
            }

            return friendsList;
        }
        catch(Exception E)
        {
            throw;
        }                                  
    }
    
    /*
    public User EditFriendShip(string username) //this will be for accepting, blocking, and removing. Thus adding, blocking, removing, and accepting will be done.
    {   
        User newFriend = GetUserByUserName(username);
        
        _context.Companions.Add(newCompanion);

        _context.SaveChanges();

        _context.ChangeTracker.Clear(); 

        return newCompanion;  
    }
*/
    public List<Friends> CheckRelationshipStatusByUsername(string username, string status)
    {   
        User quieriedUser = _userRepo.GetUserByUserName(username);

        List<Friends> quieriedUsersList = ViewAllFriends((int)quieriedUser.UserId);

        List<Friends> statusList = new List<Friends>();

        if(quieriedUsersList == null)
        {
            throw new ResourceNotFound();
        }
        foreach(Friends friendSearch in quieriedUsersList)
        {
            if(friendSearch.Status.ToString() == status)
            {
                statusList.Add(friendSearch);
            }
        }
        if(statusList.Count() < 1)
        {
            throw new ResourceNotFound();
        }

        return statusList;
    }

    public List<Friends> CheckRelationshipStatusByUserId(int searchingId, string status)
    {   
        User quieriedUser = _context.Users.Find(searchingId);

        List<Friends> quieriedUsersList = ViewAllFriends((int)quieriedUser.UserId);

        List<Friends> statusList = new List<Friends>();

        if(quieriedUsersList == null)
        {
            throw new ResourceNotFound();
        }
        foreach(Friends friendSearch in quieriedUsersList)
        {
            if(friendSearch.Status.ToString() == status)
            {
                statusList.Add(friendSearch);
            }
        }
        if(statusList.Count() < 1)
        {
            throw new ResourceNotFound();
        }

        return statusList;
    }    
/* MAKING THE ONE I AM WORKING ON A LIST OF ALL FRIENDS BY STATUS OF A PARTICULAR USER
    public Friends CheckRelationshipStatusByUserId(int searchingId, int friendsId, string status)
    {   
        User quieriedUser = _context.Users.Find(searchingId);
        User friendOfUser = _context.Users.Find(friendsId);

        List<Friends> quieriedUsersList = ViewAllFriends(quieriedUser.UserId);

        if(quieriedUsersList == null)
        {
            throw new ResourceNotFound();
        }
        else
        {
            foreach(Friends friendSearch in quieriedUsersList)
            {
                if(friendSearch.UserIdFrom == friend2BeAdded.UserId || friendSearch.UserIdTo == friend2BeAdded.UserId)
                {
                    Friends friendReturn = ViewRelationShipsByStatus(status);
                    return friendReturn;
                }
            }
        }
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
    public Friends AddFriend(int requesterId, int addedId)
    {
        User addingFriend = _context.Users.Find(requesterId);
        User friend2BeAdded = _context.Users.Find(addedId);

        IEnumerable<Friends> friendsQuery =
            (from Friends in _context.Friends
            where (Friends.UserIdTo == addingFriend.UserId) || (Friends.UserIdFrom == addingFriend.UserId)
            select Friends);        
  
        List<Friends> addingFriendList = friendsQuery.ToList();;
        
        if(addingFriendList == null)
        {
           try
            {  
                Friends newRelationship = new Friends
                    {
                        UserIdFrom = (int)addingFriend.UserId,
                        UserIdTo = (int)friend2BeAdded.UserId,
                        Status = "Pending"
                    };

                _context.Friends.Add(newRelationship);

                _context.SaveChanges();

                _context.ChangeTracker.Clear();

                return newRelationship;
            }
            catch(Exception E)
            {
                throw;
            }
        }
        else
        {
            foreach(Friends friendSearch in addingFriendList)
            {
                if(friendSearch.UserIdFrom == friend2BeAdded.UserId || friendSearch.UserIdTo == friend2BeAdded.UserId)
                {
                    throw new DuplicateFriends();
                }
                else
                {
                    try
                    {  
                        Friends newRelationship = new Friends
                            {
                                UserIdFrom = (int)addingFriend.UserId,
                                UserIdTo = (int)friend2BeAdded.UserId,
                                Status = "Pending"
                            };

                        _context.Friends.Add(newRelationship);

                        _context.SaveChanges();

                        _context.ChangeTracker.Clear();

                        return newRelationship;
                    }
                    catch(Exception E)
                    {
                        throw;
                    }                    
                }    
            }
        }

    }*/ 
}