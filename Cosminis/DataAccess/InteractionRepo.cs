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
    private readonly IResourceGen _ResourceRepo;
    
    public InteractionRepo(wearelosingsteamContext context, IUserDAO userRepo, ICompanionDAO compRepo, IResourceGen ResourceRepo)
    {
        _context = context;
        _userRepo = userRepo;
        _compRepo = compRepo;
        _ResourceRepo = ResourceRepo;
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
            throw new CompNotFound();
        }
        /*
        if(companionToDepress.TimeSinceLastChangedMood == null)                //Maybe we don't need to worry about this from here out?
        {
            companionToDepress.TimeSinceLastChangedMood = DateTime.Now;     
        }        
        if(companionToDepress.Mood == null)                                                   //checking null
        {
            companionToDepress.Mood = 75;
        }        
        */
        bool companionShowcase = false;            //Setting this to check if showcase companion

        User companionUserCheck = _userRepo.GetUserByUserId(companionToDepress.UserFk);
        if(companionUserCheck == null)
        {
            throw new UserNotFound();
        }

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
        Companion companionToStarve = _context.Companions.Find(companionID);  //Retrieve companion object from database by the given CompanionID
        if(companionToStarve == null)
        {
            throw new ResourceNotFound();
        }
        companionToStarve.Hunger = companionToStarve.Hunger + amount;//Modify the hunger value
        if(companionToStarve.Hunger<0) //fix the minimum hunger value
        {
            companionToStarve.Hunger = 0;
        }
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
    public bool RollCompanionEmotion(int companionID) //Use the result of the weighted die to determine the quality of the companion's emotion
    {
        Random randomEmotion = new Random();          //The weight is based on the companion's mood value (I'll also add current emotion as a mod) This may be hard to determine the best num

        int baseEmotionRand = randomEmotion.Next(4, 7);
        int emotionToSet = 0;                         //This is the default state of the emotion to set, and is actually part of the random number generator seed
        int emotionToSetMod = 0;                      //This is a modifier to the result of the randomly generated number after it is generated based on the current mood
        int moodMod = 0;

        Companion companionEmotionToSet = _compRepo.GetCompanionByCompanionId(companionID); //Grabbing the companion
        if(companionEmotionToSet == null)                                                   //Checking null
        {
            throw new CompNotFound();
        }
        if(companionEmotionToSet.Emotion == null)                                                  //checking null
        {
            companionEmotionToSet.Emotion = 0;
        }         
        if(companionEmotionToSet.Mood == null)                                                   //checking null
        {
            companionEmotionToSet.Mood = 75;
        }

        int emotionIdentifier = companionEmotionToSet.Emotion; //getting the current emotion of the companion so that we can create a modifer based on emotion quality

        EmotionChart emotionToFind = _context.EmotionCharts.Find(emotionIdentifier);            

        try
        {      
            if(emotionToFind.Quality <= 2)                     //Modify tables based on current emotion state
            {
                emotionToSetMod = -2;
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
                emotionToSetMod = 2;
            } 

            if(companionEmotionToSet.Mood <= 15)               //Modify tables based on current mood level
            {
                moodMod = -2;
            }
            else if(companionEmotionToSet.Mood <= 35)
            {
                moodMod = -1;
            }
            else if(companionEmotionToSet.Mood <= 50)
            {
                moodMod = -0;
            }            
            else if(companionEmotionToSet.Mood <= 75)
            {
                moodMod = 1;
            }
            else if(companionEmotionToSet.Mood >= 85)
            {
                moodMod = 2;
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
        
        bool love = (species2check.FoodElementIdFk == food2Feed.FoodStatsId);
        bool hate = (species2check.OpposingEle == food2Feed.FoodStatsId);
        int baseAmountHunger = 0; //neither of these numbers make any damm sense
        int baseAmountMood = 0; 
        if(love)
        {
            baseAmountHunger = RNGjesusManifested.Next(25,31);
            baseAmountMood = RNGjesusManifested.Next(25,31);
        }
        else if(hate)
        {
            baseAmountHunger = RNGjesusManifested.Next(-25,11);
            baseAmountMood = RNGjesusManifested.Next(-25,11);
        }
        else
        {
            baseAmountHunger = RNGjesusManifested.Next(-15,31);
            baseAmountMood = RNGjesusManifested.Next(-15,31);
        }

        double HungerModifier = 1;
        double MoodModifier = 1;
        if(love || hate) //I know all of these can be compress into the if else block above, I am keeping them seperated for my own sanity sake, STFU
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
        if(love) //I know all of these can be compress into the if else block above, I am keeping them seperated for my own sanity sake, STFU
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
    public bool PetCompanion(int userID, int companionID)//limit petting to every 3-5min
    {
        Random agitationRoll = new Random();             //For our random roll later

        Companion companionToPet = _compRepo.GetCompanionByCompanionId(companionID); //grabbing the companion
        if(companionToPet == null)                                                   //checking null
        {
            throw new CompNotFound();
        }
        if(companionToPet.Mood == null)                                                   //checking null
        {
            companionToPet.Mood = 75;
            RollCompanionEmotion(companionToPet.CompanionId);
        }

        User userToPet = _userRepo.GetUserByUserId(userID);  //grabbing the user
        if(userToPet == null)                                //checking null
        {
            throw new UserNotFound();
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
        Companion companionToTalk = _context.Companions.Find(CompanionID);  //Retrieve companion object from database by the given CompanionID
        if(companionToTalk==null)
        {
            throw new ResourceNotFound();
        }
        Random RNGjesusManifested = new Random();  
        int offsetQual = 0; //[-3,3]
        int baseQual = RNGjesusManifested.Next(3,8); //The PDF of this method aint gonna be continuous, but I am just too stupid to figure a decent way to implement a continuous distribution
        if(companionToTalk.Mood<15) // 14*7=98
        {
            offsetQual = -3;
        }
        else if(companionToTalk.Mood<29)
        {
            offsetQual = -2;
        }
        else if(companionToTalk.Mood<43)
        {
            offsetQual = -1;
        }
        else if(companionToTalk.Mood<47)
        {
            offsetQual = 0;
        }
        else if(companionToTalk.Mood<71)
        {
            offsetQual = 1;
        }
        else if(companionToTalk.Mood<85)
        {
            offsetQual = 2;
        }
        else
        {
            offsetQual = 3;
        }
        int endQual = baseQual + offsetQual; //[0,10]

        IEnumerable<Conversation> checkForSpecies = //copped this code whole sale from FriendsRepo
            (from Conversation in _context.Conversations
            where (Conversation.SpeciesFk == companionToTalk.SpeciesFk) && (Conversation.Quality == endQual)
            select Conversation);
        Conversation endConvo = checkForSpecies.FirstOrDefault(); //Retrieve A list of conversation that matches the given species.
        if(endConvo == null)
        {
            return "I want sometime for my self now, why don't you go touch some grass?";
        }
        else
        {
            return endConvo.Message;
        }
    }
}