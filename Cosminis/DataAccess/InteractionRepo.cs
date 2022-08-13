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

            if(companionToDepress.Mood <= 0)
            {
                companionToDepress.Mood = 0; //preventing negative numbers
            }

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

            companionEmotionToSet.Emotion = emotionToSet; //You can get less than 0 and greater than 10 but I figure we will figure this out together

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
    public bool PetCompanion(int userID, int companionID)//limit petting to every 3-5min
    {
        Random agitationRoll = new Random();             //For our random roll later

        Companion companionToPet = _compRepo.GetCompanionByCompanionId(companionID); //grabbing the companion
        if(companionToPet == null)                                                   //checking null
        {
            throw new ResourceNotFound();
        }

        User userToPet = _userRepo.GetUserByUserId(userID);  //grabbing the user
        if(userToPet == null)                                //checking null
        {
            throw new ResourceNotFound();
        }

        int moodToOffset = 0;

        try
        {
            int hungerMod = 0;       //this value will modify the chance for companion agitation based on hunger
            if(companionToPet.Hunger <= 15)
            {
                hungerMod = -30;        //"roll a agitation threshold based on hunger (if the pet is hungry, the agitation threshold should be weighted to roll high)"
            }
            else if(companionToPet.Hunger <= 35)
            {
                hungerMod = -20;
            }
            else if(companionToPet.Hunger <= 60)
            {
                hungerMod = -10;
            }        
            else if(companionToPet.Hunger <= 75)
            {
                hungerMod = -5;
            }
            else if(companionToPet.Hunger >= 90)
            {
                hungerMod = 0;
            }                  

            int moodMod = 0;       //this value will modify the chance for companion agitation based on mood
            if(companionToPet.Mood <= 15)
            {
                moodMod = -5;       
            }
            else if(companionToPet.Mood <= 35)
            {
                moodMod = 0;
            }
            else if(companionToPet.Mood <= 60)
            {
                moodMod = 5;
            }        
            else if(companionToPet.Mood <= 75)
            {
                moodMod = 15;
            }
            else if(companionToPet.Mood >= 90)
            {
                moodMod = 30;
            }        

            bool companionShowcase = false;            //Setting this to check if showcase companion
            int showcaseMod = 0;                       //If bool = true, check gets +10 to succeed on agitation threshold check
            if(userToPet.ShowcaseCompanionFk == companionToPet.CompanionId)//Checking whether it is or not
            {
                companionShowcase = true;     
            }

            if(companionShowcase == true)              //Setting Bonus if true
            {
                showcaseMod = 10;
            }

            int agitationBaseRoll = agitationRoll.Next(15, 35);//Rolling base roll with previously set random number generator

            int totalRoll = agitationBaseRoll + hungerMod + moodMod + showcaseMod;

            if(totalRoll < 50)
            {
                if(companionToPet.Mood <= 15)
                {
                    moodToOffset = -7; //This is a weird one.... because the number is already soooo low it'll probably hit 0 or close to it anyway...      
                }
                else if(companionToPet.Mood <= 35)
                {
                    moodToOffset = -20;     //This number is bigger than the above one because if it is agitated with a low mood we want the change obvious
                }
                else if(companionToPet.Mood <= 60)
                {
                    moodToOffset = -10;     //Sucks to suck and it is noticable but not too bad 
                }        
                else if(companionToPet.Mood <= 75)
                {
                    moodToOffset = 0;       //These numbers are pretty harsh but also I weighed it very likely for petting to be a positive result.
                }
                else if(companionToPet.Mood >= 90)
                {
                    moodToOffset = 5;       //I mean does it really need to be much happier?
                }  
            }
            else if(totalRoll >= 50)
            {
                if(companionToPet.Mood <= 15)
                {
                    moodToOffset = 30; //Give them a big bonus because it could be risky and they need it the most (we could make this a random range too if we want in theory)      
                }
                else if(companionToPet.Mood <= 35)
                {
                    moodToOffset = 20;     //This number is bigger than the above one because if it is agitated with a low mood we want the change obvious
                }
                else if(companionToPet.Mood <= 60)
                {
                    moodToOffset = 15;     //Numbers becoming less since the companion is already in a relatively good mood. Obvi we can change them.
                }        
                else if(companionToPet.Mood <= 75)
                {
                    moodToOffset = 10;       
                }
                else if(companionToPet.Mood >= 90)
                {
                    moodToOffset = 5;       //I mean does it really need to be much happier?
                } 
            }

            companionToPet.Mood = companionToPet.Mood + moodToOffset; //I think rolling for agitation is good, but the actual numbers may wanna be changed in the end.

            if(companionToPet.Mood <= 0) //preventing negatives and values over 100
            {
                companionToPet.Mood = 0;
            }
            if(companionToPet.Mood >= 100)
            {
                companionToPet.Mood = 100;
            }            

            _context.SaveChanges(); //Maybe this method could also have a percentage change to reroll the emotion? A greater chance to change emotion if mood is low or emotion quality is poor

            _context.ChangeTracker.Clear();

            return true;
        }
        catch
        {
            throw new ResourceNotFound();//This might be changed to "TooSoon" if we set a petCompanionTimer value somewhere
        }

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