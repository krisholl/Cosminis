using DataAccess.Entities;
using CustomExceptions;
using Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess;
 
public class CompanionRepo// : ICompanionDAO
{
    /// <summary>
    /// Generates a companion for the user. If the user is a new user, the companion will be generated from an egg one minute after the egg has been obtained.
    /// Otherwise any eggs will hatch 72 hours after they have been generated.
    /// </summary>
    /// <param randomCreature="Name for the random number generator itself."></param>
    /// <param creatureRoulette="Integer stores number of ^randomCreature as a result for generating a specific companion."></param>
    /// <returns>Will create and add a companion into the users inventory.</returns>
    /// <exception cref="Exception">exception descriptions</exception>
    public Companion GenerateCompanion(Companion newCompanion)
    {
        Random randomCreature = new Random();
        int creatureRoulette = randomCreature.Next(6);

        //MoodMethod();

        throw new Exception();
    }

    /// <summary>
    /// Generates a companion for the user. If the user is a new user, the companion will be generated from an egg one minute after the egg has been obtained.
    /// Otherwise any eggs will hatch 72 hours after they have been generated.
    /// </summary>
    /// <param randomCreature="Name for the random number generator itself."></param>
    /// <param creatureRoulette="Integer stores number of ^randomCreature as a result for generating a specific companion."></param>
    /// <returns>Will create and add a companion into the users inventory.</returns>
    /// <exception cref="Exception">exception descriptions</exception>
    public Companion SetCompanionMood(MoodCompanion mood)
    {
        Random randomMood = new Random();
        int moodOfCreature = randomMood.Next(7);



        throw new Exception();
    }
}