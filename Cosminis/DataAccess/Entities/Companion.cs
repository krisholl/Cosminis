using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Companion
    {
        public int CompanionId { get; set; }
        public int UserFk { get; set; }
        public int SpeciesFk { get; set; }
        public string? Nickname { get; set; }
        public string Mood { get; set; } = null!;
        public int? Hunger { get; set; }
        public DateTime? CompanionBirthday { get; set; }

        public virtual Species SpeciesFkNavigation { get; set; } = null!;
        public virtual User UserFkNavigation { get; set; } = null!;
    }
}
