using DataAccess.Entities;
using CustomExceptions;
using Models;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
 
public class CompanionRepo : ICompanionDAO
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
    public Companion GenerateCompanion(int userIdInput)
    {
        Companion newCompanion = new Companion();
        User identifiedUser = new User();

        newCompanion.UserFk = userIdInput;

        Random randomCreature = new Random();
        int creatureRoulette = randomCreature.Next(6);

        newCompanion.SpeciesFk = creatureRoulette;

        newCompanion.Mood = (SetCompanionMood(newCompanion.CompanionId)).ToString();
        newCompanion.Hunger = 100;
        newCompanion.CompanionBirthday = DateTime.Now;

        identifiedUser.Companions.Add(newCompanion);

         _context.SaveChanges();

        _context.ChangeTracker.Clear(); 

        return newCompanion;                                                        
    }
    
    /// <summary>
    /// Generates a companion for the user. If the user is a new user, the companion will be generated from an egg one minute after the egg has been obtained.
    /// Otherwise any eggs will hatch 72 hours after they have been generated.
    /// </summary>
    /// <param mood="Daily mood of the companion"></param>
    /// <returns>Will create and add a companion into the users inventory.</returns>
    /// <exception cref="Exception">exception descriptions</exception>
    public Companion SetCompanionMood(int companionId)
    {
        Random randomMood = new Random();
        int companionMood = randomMood.Next(7);

        Companion companionNeedsMood = GetCompanionByCompanionId(companionId);

        switch(companionMood) 
        {
        case 0:
            companionNeedsMood.Mood = MoodCompanion.Happy.ToString();
            break;
        case 1:
            companionNeedsMood.Mood = MoodCompanion.Sad.ToString();
            break;
        case 2:
            companionNeedsMood.Mood = MoodCompanion.Angry.ToString();
            break;
        case 3:
            companionNeedsMood.Mood = MoodCompanion.Tired.ToString();
            break;
        case 4:
            companionNeedsMood.Mood = MoodCompanion.Anxious.ToString();
            break;
        case 5:
            companionNeedsMood.Mood = MoodCompanion.Excited.ToString();
            break;
        case 6:
            companionNeedsMood.Mood = MoodCompanion.Chill.ToString();
            break;        
        default:
            companionNeedsMood.Mood = MoodCompanion.Happy.ToString();
            break;
        };

        _context.SaveChanges();

        _context.ChangeTracker.Clear();        

        return companionNeedsMood;
    }

    /// <summary>
    /// Generates a companion for the user. If the user is a new user, the companion will be generated from an egg one minute after the egg has been obtained.
    /// Otherwise any eggs will hatch 72 hours after they have been generated.
    /// </summary>
    /// <param mood="Daily mood of the companion"></param>
    /// <returns>Will create and add a companion into the users inventory.</returns>
    /// <exception cref="Exception">exception descriptions</exception>    
    public Companion SetCompanionNickname(int companionId, string? nickname)
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