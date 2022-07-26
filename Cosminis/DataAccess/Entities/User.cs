using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class User
    {
        public User()
        {
            Companions = new HashSet<Companion>();
            FoodInventories = new HashSet<FoodInventory>();
            Posts = new HashSet<Post>();
            PostIdFks = new HashSet<Post>();
            UserIdFk1s = new HashSet<User>();
            UserIdFk2s = new HashSet<User>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? GoldCount { get; set; }
        public int? EggCount { get; set; }
        public int? EggTimer { get; set; }

        public virtual ICollection<Companion> Companions { get; set; }
        public virtual ICollection<FoodInventory> FoodInventories { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Post> PostIdFks { get; set; }
        public virtual ICollection<User> UserIdFk1s { get; set; }
        public virtual ICollection<User> UserIdFk2s { get; set; }
    }
}
