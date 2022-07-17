namespace CofD_NPC
{
    [Serializable]
    public class NPC
    {
        public long ID { get; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Virtue { get; set; }
        public string? Vice { get; set; }
        public string? Concept { get; set; }
        public int? HealthDots { get; set; }
        public int[]? HealthStates { get; set; }
        public int? WillpowerDots { get; set; }
        public int? WillpowerCurrent { get; set; }
        public int? Integrity { get; set; }
        public int? Size { get; set; }
        public int? Speed { get; set; }
        public int? Defense { get; set; }
        public string? Armor { get; set; }
        public int? Initiative { get; set; }
        public string? Description { get; set; }
        public int? Intelligence { get; set; }
        public int? Wits { get; set; }
        public int? Resolve { get; set; }
        public int? Strength { get; set; }
        public int? Dexterity { get; set; }
        public int? Stamina { get; set; }
        public int? Presence { get; set; }
        public int? Manipulation { get; set; }
        public int? Composure { get; set; }
        public string[]? Skills { get; set; }
        public int[]? SkillDots { get; set; }
        public string[]? Merits { get; set; }
        public int[]? MeritDots { get; set; }
        public string? Conditions { get; set; }
        public string? Aspirations { get; set; }

        public NPC()
        {
            ID = DateTime.Now.Ticks;
            Name = "";
            Age = 0;
            Virtue = "";
            Vice = "";
            Concept = "";
            HealthDots = 0;
            HealthStates = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            WillpowerDots = 0;
            WillpowerCurrent = 0;
            Integrity = 0;
            Size = 0;
            Speed = 0;
            Defense = 0;
            Armor = "";
            Initiative = 0;
            Description = "";
            Intelligence = 0;
            Wits = 0;
            Resolve = 0;
            Strength = 0;
            Dexterity = 0;
            Stamina = 0;
            Presence = 0;
            Manipulation = 0;
            Composure = 0;
            Skills = new string[12] { "", "", "", "", "", "", "", "", "", "", "", "" };
            SkillDots = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Merits = new string[8] { "", "", "", "", "", "", "", "" };
            MeritDots = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            Conditions = "";
            Aspirations = ""; 
        }
    }
}