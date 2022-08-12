using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Companion
    {
        public Companion()
        {
            Users = new HashSet<User>();
        }

        public int CompanionId { get; set; }
        public int UserFk { get; set; }
        public int SpeciesFk { get; set; }
        public string? Nickname { get; set; }
        public string Mood { get; set; } = null!;
        public int? Hunger { get; set; }
        public DateTime? TimeSinceLastChangedMood { get; set; }
        public DateTime? TimeSinceLastChangedHunger { get; set; }
        public DateTime? CompanionBirthday { get; set; }

        public virtual Species SpeciesFkNavigation { get; set; } = null!;
        public virtual User UserFkNavigation { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
    }
}
