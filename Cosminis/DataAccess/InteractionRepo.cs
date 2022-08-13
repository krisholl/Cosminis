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

            companionToDepress.TimeSinceLastChangedMood = DateTime.Now;

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