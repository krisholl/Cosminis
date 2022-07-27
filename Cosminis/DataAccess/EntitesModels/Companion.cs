using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public enum MoodCompanion                            
    {
        Happy, Sad, Angry, Tired, Anxious, Excited, Chill
    }
    public partial class Companion
    {
        public override string ToString()
        { 
            return $"CreatureId: {this.CreatureId}, Nickname: {this.Nickname}, Mood: {this.Mood}, Hunger: {this.Hunger}";
        }  
    }
}
