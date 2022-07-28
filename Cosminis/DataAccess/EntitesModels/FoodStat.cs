using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class FoodStat
    {
        public override string ToString()
        {
            return
                $"FoodStatsId: {this.FoodStatsId}, " + 
                $"FoodElementFk: {this.FoodElementFk}, " + 
                $"Description: {this.Description}, " + 
                $"FoodName: {this.FoodName}, " + 
                $"HungerRestore: {this.HungerRestore}, ";
        }
            
    }
}