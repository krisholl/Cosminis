
using System.Collections.Generic;
using DataAccess;
namespace DataAccess.Entities;
    public partial class FoodInventory
    {
        public override string ToString()
        {
            return
            $"FoodCount: {this.FoodCount}";
        }
    }
