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
        Random randomCreature = new Random();
        int creatureRoulette = randomCreature.Next(1,6);        
        
        Companion newCompanion = new Companion()
        {
            UserFk = userIdInput,
            SpeciesFk = creatureRoulette,
            Mood = SetCompanionMood(),
            Hunger = 100,
            CompanionBirthday = DateTime.Now
        };
        Console.WriteLine(newCompanion);

        User identifiedUser = new User();

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
    public string SetCompanionMood()
    {
        Random randomMood = new Random();
        int companionMood = randomMood.Next(7);

        string setMood = "Happy";

        switch(companionMood) 
        {
        case 0:
            setMood = MoodCompanion.Happy.ToString();
            break;
        case 1:
            setMood = MoodCompanion.Sad.ToString();
            break;
        case 2:
            setMood = MoodCompanion.Angry.ToString();
            break;
        case 3:
            setMood = MoodCompanion.Tired.ToString();
            break;
        case 4:
            setMood = MoodCompanion.Anxious.ToString();
            break;
        case 5:
            setMood = MoodCompanion.Excited.ToString();
            break;
        case 6:
            setMood = MoodCompanion.Chill.ToString();
            break;        
        default:
            setMood = MoodCompanion.Happy.ToString();
            break;
        };

        return setMood;
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
    public Companion GetCompanionByUser(int userId)
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