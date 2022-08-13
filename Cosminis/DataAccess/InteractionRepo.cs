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
    private readonly IUserDAO _userRepo;
    private readonly ICompanionDAO _compRepo;

    public InteractionRepo(wearelosingsteamContext context, IUserDAO userRepo, ICompanionDAO compRepo)
    {
        _context = context;
        _userRepo = userRepo;
        _compRepo = compRepo;
    }    

    /// <summary>
    /// Method that modify the mood value of a particular companion
    /// </summary>
    /// <param name="companionID"></param>
    /// <param name="amount"></param>
    public bool SetCompanionMoodValue(int companionID, int hungerDeterminer)
    {
        Random moodDecrementer = new Random();                                 //creating random number

        int moodDecrementAmount = 1;                                           //This will be the amount that actually gets taken

        int moodAdjust = moodDecrementer.Next(1, hungerDeterminer);            //"Weight" of moodDecrement determined by hungerlvl

        Companion companionToDepress = _context.Companions.Find(companionID);  //Get comp followed by checkifnull
        if(companionToDepress == null)
        {
            throw new ResourceNotFound();
        }
        /*      
        if(companionToDepress.Mood == null)
        {
            companionToDepress.Mood = 75;
        } 
        */
        bool companionShowcase = false;            //Setting this to check if showcase companion

        User companionUserCheck = _userRepo.GetUserByUserId(companionToDepress.UserFk);
        if(companionUserCheck.ShowcaseCompanionFk == companionToDepress.CompanionId)//Checking whether it is or not
        {
            companionShowcase = true;              //If this is true, mood decreases at a lessened rate
        }

        try
        {
            if(moodAdjust <= 10)                   //Completely original numbers (this is "chance")
            {
                moodDecrementAmount = 1;           //Actually original numbers (this is "static amt")
                if(companionShowcase == true)
                {
                    moodDecrementAmount = 0;
                }
            }
            else if(moodAdjust <= 30)
            {
                moodDecrementAmount = 2;
                if(companionShowcase == true)
                {
                    moodDecrementAmount = 1;
                }
            }
            else if(moodAdjust <= 90)
            {
                moodDecrementAmount = 5;
                if(companionShowcase == true)
                {
                    moodDecrementAmount = 3;
                }
            }
            else if(moodAdjust <= 270)
            {
                moodDecrementAmount = 10;
                if(companionShowcase == true)
                {
                    moodDecrementAmount = 5;
                }
            }

            companionToDepress.Mood = companionToDepress.Mood - moodDecrementAmount; //adjust mood based on determined amount

            companionToDepress.TimeSinceLastChangedMood = DateTime.Now;              //resetting the mood timer on the companion

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return true;
        }
        catch(Exception)
        {
            throw new ResourceNotFound();
        }

        return false;
    }

    /// <summary>
    /// Method that modify the hunger value of a particular companion
    /// </summary>
    /// <param name="companionID"></param>
    /// <param name="amount"></param>
    public bool SetCompanionHungerValue(int companionID, int amount)
    {
        //Retrieve companion object from database by the given CompanionID
        //Modify the hunger value
        //save changes
        return false;
    }

    /// <summary>
    /// Rolls from a pool of possible moods and assigns it to a companion
    /// </summary>
    /// <param name="companionID"></param>
    /// <param name="amount"></param>
    public bool RollCompanionEmotion(int companionID) //Use the result of the weighted die to determine the quality of the companion's emotion
    {
        Random randomEmotion = new Random();          //The weight is based on the companion's mood value (I'll also add current emotion as a mod) This may be hard to determine the best num

        int baseEmotionRand = randomEmotion.Next(3, 4);
        int emotionToSet = 0;                         //This is the default state of the emotion to set, and is actually part of the random number generator seed
        int emotionToSetMod = 0;                      //This is a modifier to the result of the randomly generated number after it is generated based on the current mood
        int moodMod = 0;

        Companion companionEmotionToSet = _compRepo.GetCompanionByCompanionId(companionID); //Grabbing the companion
        if(companionEmotionToSet == null)                                                   //Checking null
        {
            throw new ResourceNotFound();
        } 

        int emotionIdentifier = companionEmotionToSet.Emotion; //getting the current emotion of the companion so that we can create a modifer based on emotion quality

        EmotionChart emotionToFind = _context.EmotionCharts.Find(emotionIdentifier);            

        try
        {      
            if(emotionToFind.Quality <= 2)                     //Modify tables based on current emotion state
            {
                emotionToSetMod = -3;
            }
            else if(emotionToFind.Quality <= 4)
            {
                emotionToSetMod = -1;
            }
            else if(emotionToFind.Quality <= 6)
            {
                emotionToSetMod = 1;
            }
            else if(emotionToFind.Quality >= 7)
            {
                emotionToSetMod = 3;
            } 

            if(companionEmotionToSet.Mood <= 15)               //Modify tables based on current mood level
            {
                moodMod = -4;
            }
            else if(companionEmotionToSet.Mood <= 35)
            {
                moodMod = -2;
            }
            else if(companionEmotionToSet.Mood <= 50)
            {
                moodMod = -0;
            }            
            else if(companionEmotionToSet.Mood <= 75)
            {
                moodMod = 2;
            }
            else if(companionEmotionToSet.Mood >= 85)
            {
                moodMod = 4;
            }   

            emotionToSet = baseEmotionRand + emotionToSetMod + moodMod; //Adding the mods! This is the big moment (I guess)

            companionEmotionToSet.Emotion = emotionToSet;

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return true;
        }
        catch(Exception)
        {
            throw new ResourceNotFound();
        }
          
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
        //Retrieve companion object from database by the given CompanionID
        //Retrieve user object from database by the given FeederID
        //Retrieve foodStats object from database by the given CompanionID

        //check if the user has that kind of food (query foodInventory)
        //if the user does not, return false
        //if the user does, call removeFood from ResourceRepo passing in the FeederID and foodID

        //check element of the companion and the food
        //if they match: increase x mood and increase a*y fullness
        //if they don't match: increase k mood and increase a*j fullness
        //call SetCompanionMoodValue and SetCompanionHungerValue passing in the CompanionID and respective mood value/hunger value
        //Where j<y, k<x, k is the set of all integers and x, y and j are positive integers

        //return true after successful operation        
        return false;
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