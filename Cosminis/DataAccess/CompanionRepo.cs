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
        _context.Companions.Add(newCompanion);

        _context.SaveChanges();

        _context.ChangeTracker.Clear(); 

        return newCompanion;                                                        
    }
    
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

    public Companion SetCompanionNickname(int companionId, string? nickname)
    {
        Companion selectCompanion = GetCompanionByCompanionId(companionId);

        selectCompanion.Nickname = nickname;

        _context.SaveChanges();

        _context.ChangeTracker.Clear();

        return selectCompanion;                                
    }

    public List<Companion> GetAllCompanions()
    {
        return _context.Companions.ToList();                                  
    }
   
    public Companion GetCompanionByUser(int userId)
    {
            return _context.Companions.FirstOrDefault(companionUser => companionUser.UserFk == userId) ?? throw new ResourceNotFound("User has no companion to display.");
    }

    public Companion GetCompanionByCompanionId(int companionId)
    {
            return _context.Companions.FirstOrDefault(companionToBeFound => companionToBeFound.CompanionId == companionId) ?? throw new ResourceNotFound("No companion with this ID exists.");
    }
}