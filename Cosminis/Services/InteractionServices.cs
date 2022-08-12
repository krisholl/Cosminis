using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class InteractionService
{
    //Honey wake up, it is time for you to write your dependacy injection!
    public bool SetCompanionMoodValue(int companionID, int amount)
    {
        //int minutes = DateTime.Now - Companion.TimeSinceLastChangedMood, remember to update this time each moment it is updated
        //mood value decreases over time 
        //Retrieve the companion object from the database using the the method GetCompanionByCompanionId()
        //check if the companion is being showcased
        //if yes: Modify the amount appropriately such that the showcases companion has their mood decrease at a slower rate
        //if not: Go on
        //Check the last time it has been modified
        //do arithmetic to determine the amount of time (in minutes) that has passed since last time 
        //reduce companion mood base the time that has passed since last time
        return false;
    }
    public bool SetCompanionHungerValue(int companionID, int amount)
    {
        //int minutes = DateTime.Now - Companion.TimeSinceLastChangedHunger, remember to update this time each moment it is updated
        //hunger decreases over time
        //Retrieve the companion object from the database using the the method GetCompanionByCompanionId()
        //check if the companion is being showcased
        //if yes: Modify the amount appropriately such that the showcases companion has their hunger decrease at a more rapid rate
        //if not: Go on
        //Check the last time it has been modified
        //do arithmetic to determine the amount of time (in minutes) that has passed since last time 
        //reduce companion hunger base the time that has passed since last time 
        return false;
    }
    public bool RollCompanionEmotion(int companionID, int amount)
    {
        return false;
    }
    public bool FeedCompanion(int feederID, int companionID, int foodID)
    {
        //feed companion

        //Exponential decay with reroll chance on the y axis and mood on the x axis, such that the higher mood will result in lower chance of rerolling
        //100e^(-0.05x) where x is the mood value
        //random value that off sets between [-10,10]
        //bool reroll = Random.Next(100) < 
        //re-roll emotion
        //If friend or stranger, make post [Companions user_FK]; if it is your own, pat yourself on the back.
        //define post properties (This person came up and feed my companion!).
        return false;
    }
    public bool PetCompanion(int petterID, int companionID)
    {
        //check what companion it is [GetCompanionByCompanionId()],
        //check what user it is [GetUserByUserId()],
        //compare user and companion to see if it is: 1. The users 2. A friend's, or 3. A stranger's,
        
        //invoke PetCompanion(companionId),
        //If friend or stranger, make post [Companions user_FK]; if it is your own, pat yourself on the back.
        //define post properties (This person came up and pet my companion!).

        return false;
    }
    public bool ShowCaseCompanion(int userID, int companionID)
    {
        return false;
    }

    public string PullConvo(int CompanionID)
    {
        //return _InterationsRepo.PullConvo(int CompanionID);
    }
}