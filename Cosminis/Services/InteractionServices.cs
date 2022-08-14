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

    private readonly wearelosingsteamContext _context;
    private readonly ICompanionDAO _compRepo;
    private readonly Interactions _interRepo;
	  private readonly IUserDAO _userRepo;
    private readonly IPostDAO _PostRepo;

    public InteractionService(wearelosingsteamContext context, ICompanionDAO compRepo, IUserDAO userRepo, Interactions interRepo, IPostDAO postRepo)
    {
        _context = context;
        _interRepo = interRepo;
        _compRepo = compRepo;
        _userRepo = userRepo;
        _PostRepo = postRepo;
    }   

    public bool DecrementCompanionMoodValue(int companionID)
    {
        Companion companionMoodToShift = _compRepo.GetCompanionByCompanionId(companionID); //grabbing the companion
        if(companionMoodToShift == null)                                         //checking null
        {
            throw new CompNotFound();
        }
        /*
        if(companionMoodToShift.Mood == null)
        {
            companionMoodToShift.Mood = 75;
        }           

        if(companionMoodToShift.TimeSinceLastChangedMood == null)                
        {
            companionMoodToShift.TimeSinceLastChangedMood = DateTime.Now;     
        }
        */
        try
        {
            TimeSpan minuteDifference = (TimeSpan)(DateTime.Now - companionMoodToShift.TimeSinceLastChangedMood);//diff between now and 'then'

            double totalMinutes = minuteDifference.TotalMinutes;  //converting minutes to a double

            if(totalMinutes <= 5)                  //if 0 we can end the function because in theory we already changed the mood?
            {
                throw new TooSoon();               //maybe not good? lol
            }

            int convertedTime = (int)(Math.Floor(totalMinutes));                //converting to int for easier use

            int hungerMod = 0; //this value will modify the chance for a greater mood decrement based on hunger
            if(companionMoodToShift.Hunger <= 25)
            {
                hungerMod = 90;
            }
            else if(companionMoodToShift.Hunger <= 50)
            {
                hungerMod = 30;
            }
            else if(companionMoodToShift.Hunger <= 75)
            {
                hungerMod = 10;
            }

            int emotionIdentifier = companionMoodToShift.Emotion; //getting the emotion so that I can create a modifer based on emotion quality

            EmotionChart emotionToFind = _context.EmotionCharts.Find(emotionIdentifier);

            int emotionMod = 0; //this value will modify the chance for a greater mood decrement based on emotion state
            if(emotionToFind.Quality <= 2)
            {
                emotionMod = 150;
            }
            else if(emotionToFind.Quality <= 4)
            {
                emotionMod = 90;
            }
            else if(emotionToFind.Quality <= 6)
            {
                emotionMod = 30;
            }
            else if(emotionToFind.Quality >= 7)
            {
                emotionMod = 0;
            }           
            
            int numInstances = convertedTime / 5; //calculating how many times to tick mood decrements

            for(int x = numInstances; x > 0; x--) //as long as there are instances greater than 0 it will decrement mood based on
            {                                     // (a random value) + (mod based on hunger) + (mod based on current emotion quality)
                Random randomNum = new Random();  //these values affect the CHANCE of a change being more or less dramatic
                                                  //defining whether a showcase companion is more or less affected is in DataAccess
                int moodShift = randomNum.Next(90);

                int weight = moodShift + hungerMod + emotionMod;

                //Console.WriteLine(hungerMod, moodShift, weight);

                _interRepo.SetCompanionMoodValue(companionID, weight);

                _interRepo.RollCompanionEmotion(companionID); //idk if we want to do this EVERY time but it's kinda cool.

                return true;
            }
        }
        catch(Exception)
        {
            throw new TooSoon();
        }

        return false;
    }
    
    public bool DecrementCompanionHungerValue(int companionID)
    {
        Companion companionHungerToShift = _compRepo.GetCompanionByCompanionId(companionID); //grabbing the companion
        User companionUser = _userRepo.GetUserByUserId(companionHungerToShift.UserFk); //grabbing the owner of the companion
        bool isDisplay = (companionID == companionUser.ShowcaseCompanionFk); //check if the companion is on display
        int amount = 0;
        if(companionHungerToShift == null || companionUser==null)//checking null
        {
            throw new ResourceNotFound();
        }
        if(companionHungerToShift.Hunger == null)
        {
            companionHungerToShift.Mood = 0;
        }
        if(companionHungerToShift.TimeSinceLastChangedHunger == null)//if this is the first instance, set it to now
        {
            companionHungerToShift.TimeSinceLastChangedHunger = DateTime.Now;     
        }

        try
        {
            DateTime notNullableDate = companionHungerToShift.TimeSinceLastChangedHunger ?? DateTime.Now; //who thought this was a good idea?
            double totalMinutes = DateTime.Now.Subtract(notNullableDate).TotalMinutes;  //converting minutes to a double
            if(isDisplay)//determine the amount
            {
                amount = (int)Math.Floor(totalMinutes * 0.0347 * 1.3); //SOMEONE PLEASE NORMALIZED THE NUMBERS
            }
            else
            {
                amount = (int)Math.Floor(totalMinutes * 0.0347); 
            }
            Console.WriteLine("Calling Repo");
            return _interRepo.SetCompanionHungerValue(companionID,amount);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        return false;
    }
    public bool RollCompanionEmotion(int companionID)
    {
        try
        {
            _interRepo.RollCompanionEmotion(companionID);
            if(companionID == null)
            {
                throw new ResourceNotFound();
            }
            return _interRepo.RollCompanionEmotion(companionID);
        }
        catch (ResourceNotFound)
        {
            throw;
        }        
        
        return false;
    }
    public bool FeedCompanion(int feederID, int companionID, int foodID)
    {
        try
        {
            _interRepo.FeedCompanion(feederID, companionID, foodID); //first thing first
        }
        catch(Exception)
        {
            throw;
        }

        Random RNGjesusManifested = new Random();  
        Companion checkingComp = _compRepo.GetCompanionByCompanionId(companionID);
        if(checkingComp == null || checkingComp.Mood == null)
        {
            throw new ResourceNotFound();
        }

        int offSet = RNGjesusManifested.Next(-10,11);
        double compMood = checkingComp.Mood ?? 75; //whoever set the mood and hunger to be nullable in the database needs to be condemned 
        double chanceRR = (100 * Math.Exp(-0.05*compMood)) + offSet; //Maths
        bool RR = (RNGjesusManifested.Next(100) < chanceRR); //see of the emotion gets re rolled
        if(RR)
        {
            try
            {
                _interRepo.RollCompanionEmotion(companionID);
            }
            catch(Exception)
            {
                throw;
            }
        }

        if(checkingComp.UserFk != feederID) //If friend or stranger, make post [Companions user_FK]; if it is your own, pat yourself on the back.
        {
            Post Post = new Post()//define post properties (This person came up and feed my companion!).
            {
                UserIdFk = checkingComp.UserFk,
                Content = "Someone fed my companion while I was away, thank you!"
            };

            try
            {
                _PostRepo.SubmitPost(Post);
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }

        //we gone all the way down here, operation must be completed successfully by now
        return false;
    }
    
    public bool PetCompanion(int userID, int companionID)
    {
        try
        {
            _interRepo.PetCompanion(userID, companionID);
            if(userID == null)
            {
                throw new UserNotFound();
            }
            if(companionID == null)
            {
                throw new CompNotFound();
            }            
            return _interRepo.PetCompanion(userID, companionID);
        }
        catch (ResourceNotFound)
        {
            throw;
        }        

        return false;
    }
    
    public bool SetShowcaseCompanion(int userId, int companionId)
    {
        try
        {
            return _interRepo.SetShowcaseCompanion(userId, companionId);
        }
        catch(CompNotFound)
        {
            throw;
        }
        catch(UserNotFound)
        {
            throw;
        }
    }

    public string PullConvo(int companionID)
    {
        return "Your ISP sucks";
        //return _InterationsRepo.PullConvo(int CompanionID);
    }
}