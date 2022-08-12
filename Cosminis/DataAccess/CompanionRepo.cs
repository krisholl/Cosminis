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
        int creatureRoulette = randomCreature.Next(3,9);        
        
        Companion newCompanion = new Companion()
        {
            UserFk = userIdInput,
            SpeciesFk = creatureRoulette,
            Emotion = SetCompanionMood(),
            Hunger = 100,
            Mood = 75,
            CompanionBirthday = DateTime.Now
        };
        _context.Companions.Add(newCompanion);

        _context.SaveChanges();

        _context.ChangeTracker.Clear(); 

        return newCompanion;                                                        
    }
    
    public int SetCompanionMood()
    {
        Random randomMood = new Random();
        int companionMood = randomMood.Next(1,4);

        return companionMood;
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
   
    public List<Companion> GetCompanionByUser(int userId)
    {
        try
        { 
            List<Companion> companionList = new List<Companion>();

            IEnumerable<Companion> companionQuery =
                from Companions in _context.Companions
                where Companions.UserFk == userId
                select Companions;
        
            foreach(Companion companionReturn in companionQuery)
            {
                companionList.Add(companionReturn);
            }        

            if(companionList.Count() < 1)
            {
                throw new Exception("This user has no companions.");
            }

            return companionList;
        }
        catch(Exception E)
        {
            throw;
        }
    }

    public Companion GetCompanionByCompanionId(int companionId)
    {
            return _context.Companions.FirstOrDefault(companionToBeFound => companionToBeFound.CompanionId == companionId) ?? throw new ResourceNotFound("No companion with this ID exists.");
    }
}