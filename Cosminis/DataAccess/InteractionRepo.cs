using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess;

public class InteractionRepo : Interactions
{
    private readonly wearelosingsteamContext _context;
    private readonly IResourceGen _ResourceRepo;

    public InteractionRepo(wearelosingsteamContext context, IResourceGen ResourceRepo)
    {
        _context = context;
        _ResourceRepo = ResourceRepo;
    }  
    /// <summary>
    /// Method that modify the mood value of a particular companion
    /// </summary>
    /// <param name="companionID"></param>
    /// <param name="amount"></param>
    public bool SetCompanionMoodValue(int companionID, int amount)
    {
        
        //Retrieve companion object from database by the given CompanionID
        //Modify the mood value
        //save changes
        //return true after successful operation   
        return false;
    }

    /// <summary>
    /// Method that modify the hunger value of a particular companion
    /// </summary>
    /// <param name="companionID"></param>
    /// <param name="amount"></param>
    public bool SetCompanionHungerValue(int companionID, int amount)
    {
        Companion companionToStarve = _context.Companions.Find(companionID);  //Retrieve companion object from database by the given CompanionID
        if(companionToStarve == null)
        {
            throw new ResourceNotFound();
        }
        companionToStarve.Hunger = companionToStarve.Hunger + amount;//Modify the hunger value
        companionToStarve.TimeSinceLastChangedHunger = DateTime.Now;
        _context.SaveChanges();//save changes
        _context.ChangeTracker.Clear();
        return true;
    }

    /// <summary>
    /// Rolls from a pool of possible moods and assigns it to a companion
    /// </summary>
    /// <param name="companionID"></param>
    /// <param name="amount"></param>
    public bool RollCompanionEmotion(int companionID)
    {
        //Retrieve companion object from database by the given CompanionID
        //Roll a weighted die
        //Use the result of the weighted die to determine the quality of the companion's emotion
        //The weight is based on the companion's mood value
        //Use that quality to query the chart of emotions
        //set companion emotion equal to the resulting key
        //save changes
        //return true after successful operation   
        return false;
    }

    /// <summary>
    /// Allow a user to feed a particular type of food to a particular companion
    /// </summary>
    /// <param name="feederID"></param>
    /// <param name="companionID"></param>
    /// <param name="foodID"></param>
    /// <returns></returns>
    public bool FeedCompanion(int feederID, int companionID, int foodID)
    {
        Companion companionToStarve = _context.Companions.Find(companionID);  //Retrieve companion object from database by the given CompanionID
        User user2Check = _context.Users.Find(feederID); //Retrieve user object from database by the given FeederID
        FoodStat food2Feed = _context.FoodStats.Find(foodID); //Retrieve foodStats object from database by the given CompanionID
        Species species2check = _context.Species.Find(companionToStarve.SpeciesFk); //Retrieve Species object from database by the given CompanionID
        Random RNGjesusManifested = new Random();  
        if(companionToStarve == null || user2Check == null || food2Feed == null || species2check==null)
        {
            throw new ResourceNotFound();
        }
        if(companionToStarve.Hunger>90)
        {
            throw new TooSoon("Your buddy ain't hungy yet!");
        }
        
        bool match = (species2check.FoodElementIdFk == food2Feed.FoodStatsId);
        int baseAmountHunger = 0; //neither of these numbers make any damm sense
        int baseAmountMood = 0; 
        if(match)
        {
            baseAmountHunger = RNGjesusManifested.Next(25,31);
            baseAmountMood = RNGjesusManifested.Next(25,31);
        }
        else
        {
            baseAmountHunger = RNGjesusManifested.Next(-15,31);
            baseAmountMood = RNGjesusManifested.Next(-15,31);
        }

        double HungerModifier = 1;
        double MoodModifier = 1;
        if(match) //I know all of these can be compress into the if else block above, I am keeping them seperated for my own sanity sake, STFU
        {
            HungerModifier = HungerModifier + RNGjesusManifested.NextDouble();
            MoodModifier = MoodModifier + RNGjesusManifested.NextDouble();
            if(HungerModifier>1.90) //great success
            {
                HungerModifier = HungerModifier + (RNGjesusManifested.NextDouble()*0.2);
            }
            if(MoodModifier>1.90) //great success
            {
                MoodModifier = MoodModifier + (RNGjesusManifested.NextDouble()*0.2);
            }
            if(MoodModifier>1.95 && HungerModifier>1.95) //critical success! It also makes your companion invincible for two days when we get to the dungeon crawling feature
            {
                HungerModifier = HungerModifier + (RNGjesusManifested.NextDouble()*0.1);
                MoodModifier = MoodModifier + (RNGjesusManifested.NextDouble()*0.1);
            }
        }
        else
        {
            HungerModifier = HungerModifier - RNGjesusManifested.NextDouble();
            MoodModifier = MoodModifier - RNGjesusManifested.NextDouble();
            if(HungerModifier<0.15) //great failure
            {
                HungerModifier = HungerModifier - (RNGjesusManifested.NextDouble()*0.2);
            }
            if(MoodModifier<0.15) //great failure
            {
                MoodModifier = MoodModifier - (RNGjesusManifested.NextDouble()*0.2);
            }
            if(MoodModifier<0.05 && HungerModifier<0.05) //critical failure! It also makes your companion deal 50% less damage for the next two days
            {
                HungerModifier = HungerModifier - (RNGjesusManifested.NextDouble()*0.1);
                MoodModifier = MoodModifier - (RNGjesusManifested.NextDouble()*0.1);
            }
        }

        int moodAmount = 0;
        int hungerAmount = 0;
        if(match) //I know all of these can be compress into the if else block above, I am keeping them seperated for my own sanity sake, STFU
        {
            moodAmount = (int)Math.Ceiling(baseAmountMood*MoodModifier);
            hungerAmount = (int)Math.Ceiling(baseAmountHunger*HungerModifier);
        }
        else
        {
            moodAmount = (int)Math.Floor(baseAmountMood*MoodModifier);
            hungerAmount = (int)Math.Floor(baseAmountHunger*HungerModifier);
        }

        try
        {
            SetCompanionHungerValue(companionID,hungerAmount);
            SetCompanionMoodValue(companionID,moodAmount);
        }
        catch(Exception)
        {
            throw;
        }

        try
        {
            _ResourceRepo.RemoveFood(feederID,foodID); //last step
            return true; //operation success
        }
        catch(Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Allows a user to pet a companion, changing the said companion's mood base on their hunger level
    /// </summary>
    /// <param name="petterID"></param>
    /// <param name="companionID"></param>
    /// <returns></returns>
    public bool PetCompanion(int petterID, int companionID)
    {
        //Retrieve companion object from database by the given CompanionID
        //Retrieve user object from database by the given petterID

        //roll a agitation threshold based on hunger (if the pet is hungry, the agitation threshold should be weighted to roll high)
        //check if Companion mood value is under agitation threshold
        //if yes: the below die is weighted so that it is more likely for the companion to lose mood value
        //if no: the below die is weighted so that it is more likely for the companion to gain mood value

        //Roll a weight die and change companion mood value based on the result
        //call SetCompanionMoodValue passing in companionID and the result of the die roll
        //return true after successful operation  
        return false;
    }

    /// <summary>
    /// Changing the showcased companion field of a perticular user
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="companionID"></param>
    /// <returns></returns>
    public bool ShowCaseCompanion(int userID, int companionID)
    {
        //Retrieve companion object from database by the given CompanionID
        //Retrieve user object from database by the given userID

        //Set the showcase companion value in the user table to the given companionID 
        //return true after successful operation  
        return false;
    }

    public string PullConvo(int CompanionID)
    {
        string returnString = "Network error, go bother your ISP";

        //Retrieve companion object from database by the given CompanionID
        //Check for 1.Companion's species/element 2.Companion's Mood
        //Retrieve A list of conversation that matches the given species.
        //Pull from that list, ONE random conversation based on the mood of the companion
        //If the companion has a high mood value, it should be more likely that a high quality conversation gets chosen
        //return the conversation as string

        return returnString;
    }
}