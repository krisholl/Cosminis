using DataAccess.Entities;
using CustomExceptions;
using Models;
using System.Data.SqlClient;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess;
//check FoodInventory by userID methods
//Need showcase comapnion in user table
//Need mood value in companion table
//remove food from resource 
//Feed pet and rerolling mood
//Lower hunger in services
    //The showcased companion gets hungry faster

//check if the companion belong to the feeder/petter
        //if feeding own companion: 
        //if feeding freinds companion: 
        //if feeding strangers companion:
public class InteractionRepo
{
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
    public bool RollCompanionMood(int companionID)
    {
        //Retrieve companion object from database by the given CompanionID
        //Roll a weighted die to determine the companion Mood
        //The weight is based on the companion's mood value
        //Modify the mood type
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
}