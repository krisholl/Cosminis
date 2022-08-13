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

    public InteractionService(wearelosingsteamContext context, ICompanionDAO compRepo, IUserDAO userRepo, Interactions interRepo)
    {
        _context = context;
        _compRepo = compRepo;
        _interRepo = interRepo;
        _userRepo = userRepo;
    }    

    public bool DecrementCompanionMoodValue(int companionID)
    {
        Companion companionMoodToShift = _compRepo.GetCompanionByCompanionId(companionID); //grabbing the companion
        if(companionMoodToShift == null)                                         //checking null
        {
            throw new ResourceNotFound();
        }  

        if(companionMoodToShift.TimeSinceLastChangedMood == null)                //if this is the first instance, set it to now
        {
            companionMoodToShift.TimeSinceLastChangedMood = DateTime.Now;     
        }

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

                return true;
            }
        }
        catch(Exception)
        {
            throw new TooSoon();
        }

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
        return "ServerError";
    }
}