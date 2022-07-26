using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class FoodElement
    {
        public FoodElement()
        {
            FoodStats = new HashSet<FoodStat>();
            Species = new HashSet<Species>();
        }

        public int FoodElementId { get; set; }
        public string FoodElement1 { get; set; } = null!;

        public virtual ICollection<FoodStat> FoodStats { get; set; }
        public virtual ICollection<Species> Species { get; set; }
    }
}
