using DataAccess.Entities;
using CustomExceptions;
using Models;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
 
public class CompanionRepo// : ICompanionDAO
{
    private readonly wearelosingsteamContext _context;

    public CompanionRepo(wearelosingsteamContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Generates a companion for the user. If the user is a new user, the companion will be generated from an egg one minute after the egg has been obtained.
    /// Otherwise any eggs will hatch 72 hours after they have been generated.
    /// </summary>
    /// <param newCompanion="New companion to be generated."></param>
    /// <returns>Will create and add a companion into the users inventory.</returns>
    /// <exception cref="Exception">exception descriptions</exception>
     
    public User GenerateCompanion(string username)
    {
        Random randomCreature = new Random();
        int creatureRoulette = randomCreature.Next(6);

        Random randomStat = new Random();

        int baseStr = randomStat.Next(3, 18);
        int baseDex = randomStat.Next(3, 18);
        int baseInt = randomStat.Next(3, 18);

        User identifiedUser = GetUserByUserName(username);

        usersId = identifiedUser.UserId;

        Companion companionToAdd = new Companion(UserFk usersId, SpeciesFk creatureRoulette, string? nickname, MoodCompanion Happy, 69);

        identifiedUser.Companions = Insert(companionToAdd);

        return identifiedUser;                                                        
    }
    
    /// <summary>
    /// Generates a companion for the user. If the user is a new user, the companion will be generated from an egg one minute after the egg has been obtained.
    /// Otherwise any eggs will hatch 72 hours after they have been generated.
    /// </summary>
    /// <param mood="Daily mood of the companion"></param>
    /// <returns>Will create and add a companion into the users inventory.</returns>
    /// <exception cref="Exception">exception descriptions</exception>
    public Companion SetCompanionMood(Companion moodToBeSet)
    {
        Random randomMood = new Random();
        int moodOfCreature = randomMood.Next(7);



        throw new Exception();
    }

    /// <summary>
    /// Generates a companion for the user. If the user is a new user, the companion will be generated from an egg one minute after the egg has been obtained.
    /// Otherwise any eggs will hatch 72 hours after they have been generated.
    /// </summary>
    /// <param mood="Daily mood of the companion"></param>
    /// <returns>Will create and add a companion into the users inventory.</returns>
    /// <exception cref="Exception">exception descriptions</exception>    
    public Companion SetCompanionNicknme(int companionId, string? nickname)
    {
        Companion selectCompanion = GetCompanionByCompanionId(companionId);

        selectCompanion.Nickname = nickname;

        _context.SaveChanges();

        _context.ChangeTracker.Clear();

        return selectCompanion;                                
    }

    /// <summary>
    /// Generates a companion for the user. If the user is a new user, the companion will be generated from an egg one minute after the egg has been obtained.
    /// Otherwise any eggs will hatch 72 hours after they have been generated.
    /// </summary>
    /// <param mood="Daily mood of the companion"></param>
    /// <returns>Will create and add a companion into the users inventory.</returns>
    /// <exception cref="Exception">exception descriptions</exception>    
    public List<Companion> GetAllCompanions()
    {
        return _context.Companions.ToList();                                  
    }

    /// <summary>
    /// Generates a companion for the user. If the user is a new user, the companion will be generated from an egg one minute after the egg has been obtained.
    /// Otherwise any eggs will hatch 72 hours after they have been generated.
    /// </summary>
    /// <param mood="Daily mood of the companion"></param>
    /// <returns>Will create and add a companion into the users inventory.</returns>
    /// <exception cref="Exception">exception descriptions</exception>    
    public Companion GetCompanionsByUser(int userId)
    {
        return _context.Companions.FirstOrDefault(companionUser => companionUser.UserFk == userId) ?? throw new ResourceNotFound("User has no companion to display.");
    }

    /// <summary>
    /// Generates a companion for the user. If the user is a new user, the companion will be generated from an egg one minute after the egg has been obtained.
    /// Otherwise any eggs will hatch 72 hours after they have been generated.
    /// </summary>
    /// <param mood="Daily mood of the companion"></param>
    /// <returns>Will create and add a companion into the users inventory.</returns>
    /// <exception cref="Exception">exception descriptions</exception>
    public Companion GetCompanionByCompanionId(int companionId)
    {
        return _context.Companions.FirstOrDefault(companionToBeFound => companionToBeFound.CompanionId == companionId) ?? throw new ResourceNotFound("No companion with this ID exists.");
    }
}