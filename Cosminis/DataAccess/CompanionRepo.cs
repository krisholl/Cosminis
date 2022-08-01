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
    /*
    public Companion GenerateCompanion(Companion newCompanion)
    {
        Random randomCreature = new Random();
        int creatureRoulette = randomCreature.Next(6);

        User newUser = new User();

        if(newUser.Companion)

        //MoodMethod();

        throw new Exception();
    }
    */
    /// <summary>
    /// Generates a companion for the user. If the user is a new user, the companion will be generated from an egg one minute after the egg has been obtained.
    /// Otherwise any eggs will hatch 72 hours after they have been generated.
    /// </summary>
    /// <param mood="Daily mood of the companion"></param>
    /// <returns>Will create and add a companion into the users inventory.</returns>
    /// <exception cref="Exception">exception descriptions</exception>
    public Companion SetCompanionMood(MoodCompanion mood)
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
    public List<Companion> GetCompanionsByUserId(User username)
    {
        return _context.Companions.ToList();                           
    }
}